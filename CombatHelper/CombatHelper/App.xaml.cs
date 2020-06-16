using CombatHelper.Data;
using CombatHelper.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
