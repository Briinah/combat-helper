using CombatHelper.Data;
using CombatHelper.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter.Distribute;
using CombatHelper.Helpers;
using System.Threading.Tasks;

namespace CombatHelper
{
    public partial class App : Application
    {
        private static CombatDatabase database;
        public static CombatDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new CombatDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CombatDb.db3"));
                }

                return database;
            }
        }

        private static ResourceManager resourceManager;
        public static ResourceManager ResourceManager
        {
            get
            {
                if(resourceManager == null)
                {
                    resourceManager = new ResourceManager(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)));
                }

                return resourceManager;
            }
        }

        public static event EventHandler OnSleeping;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new CampaignList());

        }

        protected override void OnStart()
        {
            base.OnStart();

            Distribute.ReleaseAvailable = OnReleaseAvailable;
            AppCenter.Start(Helpers.Secrets.android, typeof(Analytics), typeof(Crashes));
            AppCenter.Start(Helpers.Secrets.android, typeof(Distribute));
        }

        private bool OnReleaseAvailable(ReleaseDetails releaseDetails)
        {
            // Look at releaseDetails public properties to get version information, release notes text or release notes URL
            string versionName = releaseDetails.ShortVersion;
            string versionCodeOrBuildNumber = releaseDetails.Version;
            string releaseNotes = releaseDetails.ReleaseNotes;
            Uri releaseNotesUrl = releaseDetails.ReleaseNotesUrl;

            // custom dialog
            var title = "Version " + versionName + " available!";
            Task answer;

            // On mandatory update, user cannot postpone
            if (releaseDetails.MandatoryUpdate)
            {
                answer = Current.MainPage.DisplayAlert(title, releaseNotes, "Download and Install");
            }
            else
            {
                answer = Current.MainPage.DisplayAlert(title, releaseNotes, "Download and Install", "Maybe tomorrow...");
            }
            answer.ContinueWith((task) =>
            {
                // If mandatory or if answer was positive
                if (releaseDetails.MandatoryUpdate || (task as Task<bool>).Result)
                {
                    // Notify SDK that user selected update
                    Distribute.NotifyUpdateAction(UpdateAction.Update);
                }
                else
                {
                    // Notify SDK that user selected postpone (for 1 day)
                    // Note that this method call is ignored by the SDK if the update is mandatory
                    Distribute.NotifyUpdateAction(UpdateAction.Postpone);
                }
            });

            // Return true if you are using your own dialog, false otherwise
            return true;
        }

        protected override void OnSleep()
        {
            OnSleeping?.Invoke(this, new EventArgs());

            base.OnSleep();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }
    }
}
