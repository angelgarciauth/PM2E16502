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
        Sitios currentData;

        public ListSites()
        {
            InitializeComponent();
        }


        private void ListSite_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            currentData = (e.CurrentSelection.FirstOrDefault() as Sitios);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            ListSite.ItemsSource = await App.DBase.getListSite();
        }

        private void btnEliminar_Clicked(object sender, EventArgs e)
        {
            //currentData.id
        }

        private async void btnMapa_Clicked(object sender, EventArgs e)
        {
            if (currentData == null)
            {
                await DisplayAlert("Advertencia", "Seleccione una ubicacion", "Ok");
            } else
            {
                MapPage mapPage = new MapPage(currentData);
                await Navigation.PushAsync(mapPage);
            }
        }
    }
}