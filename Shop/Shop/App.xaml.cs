using Shop.Services;
using Shop.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Shop
{
    public partial class App : Application
    {
        private static BoutiqueDataBase db;

        public static BoutiqueDataBase mydataBase
        {
            get
            {
                if (db == null)

                {
                    db = new BoutiqueDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyStore.db3"));
                }
                return db;
            }
        }


        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
