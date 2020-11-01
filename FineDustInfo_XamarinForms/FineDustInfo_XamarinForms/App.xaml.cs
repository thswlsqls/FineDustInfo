using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net;
using System.Xml;
using System.IO;

namespace FineDustInfo_XamarinForms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
            //MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
