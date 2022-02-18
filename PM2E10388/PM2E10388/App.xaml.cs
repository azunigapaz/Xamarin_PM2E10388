using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PM2E10388.Controllers;
using System.IO;

namespace PM2E10388
{
    public partial class App : Application
    {

        static SitiosDB basedatos;

        public static SitiosDB SitiosDB
        {
            get
            {
                if(basedatos == null)
                {
                    basedatos = new SitiosDB(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SitiosDB.db3"));                    
                }
                return basedatos;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.PageAgregarSitio());
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
