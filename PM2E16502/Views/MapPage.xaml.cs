using PM2E16502.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace PM2E16502.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {

        Sitios sitios;

        public MapPage()
        {
            InitializeComponent();
        }

        public MapPage(Sitios data)
        {
            sitios = data;
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewMap(sitios.descripcion, sitios.latitud, sitios.longitud);
        }

        private async void viewMap(string descriptionLocation, double latitude, double longitude)
        {
            var location = await Geolocation.GetLocationAsync();
            var pin = new Pin()
            {
                Position = new Position(latitude, longitude),
                Label = descriptionLocation,
            };


            mapa.Pins.Add(pin);
            if (location != null && latitude == location.Latitude && location.Longitude == longitude)
            {
                mapa.IsShowingUser = true;
            }
            else
            {
                mapa.IsShowingUser = false;
            }


            mapa.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(latitude, longitude), Distance.FromMeters(100)));

        }

        private async void btnShareImage_Clicked(object sender, EventArgs e)
        {
            if (sitios.foto == null || sitios.fotoPath == null)
            {
                await DisplayAlert("Advertencia", "Debes seleccionar una ubicacion con foto", "Ok");
            } else
            {
                try {
                    await Share.RequestAsync(new ShareFileRequest
                    {
                        Title = sitios.descripcion,
                        File = new ShareFile(sitios.fotoPath)
                    });
                } catch (Exception ex)
                {
                    await DisplayAlert("Advertencia", "Se produjo un error al compartir la fotografia", "Ok");
                }
                
            }
        }
    }
}