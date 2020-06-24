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
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new CampaignList());
        }

        protected override void OnStart()
        {
            base.OnStart();

            AppCenter.Start(Helpers.Secrets.android, typeof(Analytics), typeof(Crashes));
            AppCenter.Start(Helpers.Secrets.android, typeof(Distribute));
        }

        protected override void OnSleep()
        {
            base.OnSleep();
        }

        protected override void OnResume()
        {
            base.OnResume();
        }
    }
}
