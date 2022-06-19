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


        protected async override void OnAppearing()
        {
           

            if (await App.DBase.getListSite()!=null)
            {
                base.OnAppearing();
                ListSite.ItemsSource = await App.DBase.getListSite();
            }
            else
            {
                await DisplayAlert("Advertencia", "No hay sitios agregados", "Ok");
            }
                
           
        }

        private void ListSite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentData = (e.CurrentSelection.FirstOrDefault() as Sitios);
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            if (currentData == null)
            {
                await DisplayAlert("Advertencia", "Seleccione un sitio", "Ok");
            }
            else
            {
                bool answer = await DisplayAlert("¿Confirmacion?", "¿Esta seguro de eliminar el sitio?", "Si", "No");

                if (answer)
                {
                    await App.DBase.DeleteSite(currentData);
                    
                    ListSite.ItemsSource = await App.DBase.getListSite();
                    await DisplayAlert("Confirmacion", "Sitio eliminado correctamente", "Ok");
                    currentData = null;
                }
                
            }
            
        }

        private async void btnMapa_Clicked(object sender, EventArgs e)
        {
            if (currentData == null)
            {
                await DisplayAlert("Advertencia", "Seleccione una ubicacion", "Ok");
            } else
            {
                bool answer = await DisplayAlert("¿Confirmacion?", "¿Esta seguro de ir a la ubicacion?", "Si", "No");

                if (answer)
                {
                    MapPage mapPage = new MapPage(currentData);
                    await Navigation.PushAsync(mapPage);
                }

            }
        }
    }
}