using PM2E16502.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E16502.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListSites : ContentPage
    {
        public ListSites()
        {
            InitializeComponent();
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            ListSite.ItemsSource = await App.DBase.getListSite();
        }

        private void ListSite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}