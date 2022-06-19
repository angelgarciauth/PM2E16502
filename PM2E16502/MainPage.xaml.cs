using Plugin.Media;
using PM2E16502.Models;
using PM2E16502.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PM2E16502
{
    public partial class MainPage : ContentPage
    {
        Plugin.Media.Abstractions.MediaFile FileFoto = null;
        public MainPage()
        {
            InitializeComponent();
            if (App.DBase ==  null)
            {

            }
        }

        private Byte[] ConvertImageToByteArray()
        {
            if (FileFoto != null)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    Stream stream = FileFoto.GetStream();
                    stream.CopyTo(memory);
                    return memory.ToArray();

                }
            }
            return null;
        }


        private async void Foto_Clicked(object sender, EventArgs e)
        {
            FileFoto = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "MisFotos",
                Name = "test.jpg",
                SaveToAlbum = true


            });



            if (FileFoto != null)
            {
                Foto.Source = ImageSource.FromStream(() =>
                {
                    return FileFoto.GetStream();
                });
            }
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            currentLocation();

        }

        private async void currentLocation()
        {

            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status == PermissionStatus.Granted)
            {
                var localizacion = await Geolocation.GetLocationAsync();
                if (localizacion != null)
                {
                    txtLatitud.Text = Convert.ToString(localizacion.Latitude);
                    txtLongitud.Text = Convert.ToString(localizacion.Longitude);
                } 
                else
                {
                    await DisplayAlert("Advertencia", "No pudimos obtener su ubicacion", "Ok");
                    txtLatitud.Text = Convert.ToString(14.079700596864964);
                    txtLongitud.Text = Convert.ToString(-87.18946052765942);
                }
                
            }
            else
            {
                await DisplayAlert("Advertencia", "Active el GPS para el correcto funcionamiento de la aplicación.", "Ok");
                await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }
        }



        private async void btnSave_Clicked(object sender, EventArgs e)
        {

            if (FileFoto == null)
            {
                await DisplayAlert("Error", "No se ha tomado una fotografia", "OK");
                return;
            }


            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                await DisplayAlert("Error", "No se ha ingresado la descripcion", "OK");
                return;
            }

            var sitio = new Sitios
            {
                id = 0,
                foto = ConvertImageToByteArray(),
                fotoPath = FileFoto.Path,
                latitud = Convert.ToDouble(txtLatitud.Text),
                longitud = Convert.ToDouble(txtLongitud.Text),
                descripcion = txtDescripcion.Text
            };

            var result = await App.DBase.SiteSave(sitio);


            if (result > 0)
            {
                await DisplayAlert("Alert", "Guardado Correctamente", "OK");
            }
            else
            {
                await DisplayAlert("Alert", "Ha ocurrido un error", "OK");
            }
        }

        private async void btnLista_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListSites());
        }

        private void btnExit_Clicked(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();

        }
    }
}
