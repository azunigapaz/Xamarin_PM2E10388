using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using System.IO;
using PM2E10388.Models;
using Xamarin.Essentials;
using System.Threading;
using static PM2E10388.Controllers.SitiosDB;

namespace PM2E10388.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageAgregarSitio : ContentPage
    {
        public PageAgregarSitio()
        {
            InitializeComponent();
            obtenerCoordenadas();
            txtDescripcion.Text = "";
            img.Source = null;
        }

        byte[] imageToSave;

        private async void btnAgregar_Clicked(object sender, EventArgs e)
        {
            if(txtDescripcion.Text.Length == 0 && img.Source == null)
            {
                await DisplayAlert("Aviso", "Debe tomar una foto y asignar una descripcion ! ", "Ok");
            }
            else
            {
                try
                {
                    var sitios = new Sitios
                    {
                        descripcion = txtDescripcion.Text,
                        latitud = (float)Convert.ToDouble(txtLatitud.Text),
                        longitud = (float)Convert.ToDouble(txtLongitud.Text),
                        imagen = imageToSave
                    };

                    var resultadoInsert = await App.SitiosDB.SitioGuardar(sitios);

                    if (resultadoInsert != 0)
                    {
                        await DisplayAlert("Aviso", "Sitios registrado ! ", "Ok");
                    }
                    else
                    {
                        await DisplayAlert("Aviso", "Ha Ocurrido un Error", "Ok");
                    }

                    //await Navigation.PopAsync();

                    fnLimpiar();
                }
                catch(Exception ex)
                {
                    await DisplayAlert("Advertencia", ex.Message + " Tabla Sitios Creada !", "Ok");
                }
            }
        }

        private async void btnListar_Clicked(object sender, EventArgs e)
        {
            //await DisplayAlert("Aviso", "Ha dado click en el boton Consulta de sitios ", "Ok");
            var abrirPaginaListaSitios = new Views.PageListaSitios();
            await Navigation.PushAsync(abrirPaginaListaSitios);
        }

        private async void btnSalir_Clicked(object sender, EventArgs e)
        {
            //await DisplayAlert("Aviso", "Ha dado click en el boton Salir ", "Ok");
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private async void OnImageNameTapped(object sender, EventArgs args)
        {
            try
            {
                //Code to execute on tapped event

                var takepic = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "PhotoApp",
                    Name = DateTime.Now.ToString() + "_Pic.jpg",
                    SaveToAlbum = true
                });

                await DisplayAlert("Ubicacion de la foto: ", takepic.Path, "Ok");

                if (takepic != null)
                {
                    imageToSave = null;
                    MemoryStream memoryStream = new MemoryStream();

                    takepic.GetStream().CopyTo(memoryStream);
                    imageToSave = memoryStream.ToArray();

                    img.Source = ImageSource.FromStream(() => { return takepic.GetStream(); });
                }

                //await DisplayAlert("Aviso", "Ha dado click en la imagen ", "Ok");
                obtenerCoordenadas();
                txtDescripcion.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async void fnLimpiar()
        {
            obtenerCoordenadas();
            txtDescripcion.Text = "";
            txtDescripcion.Focus();
            img.Source = null;
        }

        public async void obtenerCoordenadas()
        {
            try
            {
                var georequest = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));

                var tokendecancelacion = new CancellationTokenSource();

                var localizacion = await Geolocation.GetLocationAsync(georequest, tokendecancelacion.Token);


                if (localizacion != null)
                {
                    txtLatitud.Text = localizacion.Latitude.ToString();
                    txtLongitud.Text = localizacion.Longitude.ToString();
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Advertencia", "Este dispositivo no soporta GPS", "Ok");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                await DisplayAlert("Advertencia", "Error de Dispositivo", "Ok");
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Advertencia", "Sin Permisos de Geolocalizacion", "Ok");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Advertencia", "Sin Ubicacion", "Ok");
            }
        }


    }
}