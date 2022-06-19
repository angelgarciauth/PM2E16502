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


        private void ListSite_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            ListSite.ItemsSource = await App.DBase.getListSite();
        }
    }
}