using PM2E10388.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2E10388.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageListaSitios : ContentPage
    {
        public PageListaSitios()
        {
            InitializeComponent();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            obtenerListaSitios();
        }

        private async void ListaSitios_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                Sitios modelItemLista = (Sitios)e.Item;

                var messageAlert = await DisplayAlert("Opción", "Seleccione una opción", "Ir a la Ubicación", "Eliminar Ubicacion");

                if (messageAlert)
                {
                    var mapDescripcion = await App.SitiosDB.ObtenerDescripcion(modelItemLista.descripcion);
                    var maprLongitud = await App.SitiosDB.ObtenerLongitud(modelItemLista.longitud);
                    var maprLatitud = await App.SitiosDB.ObtenerLatitud(modelItemLista.latitud);

                    // Funcion que utilizar Xamarin.forms.maps
                    var openXamarinMap = new Views.PageMaps("Ubicacion", mapDescripcion.descripcion, maprLongitud.longitud, maprLatitud.latitud);
                    await Navigation.PushAsync(openXamarinMap);

                    // funcion que despliega Google Maps
                    //await Xamarin.Essentials.Map.OpenAsync(maprLongitud.longitud, maprLatitud.latitud, new MapLaunchOptions
                    //{
                    //    Name = mapDescripcion.descripcion
                    //});
                }
                else
                {
                    var resultadoDelete = await App.SitiosDB.SitioEliminar(modelItemLista);

                    if (resultadoDelete != 0)
                    {
                        await DisplayAlert("Aviso", "Sitio eliminado !", "Ok");
                    }
                    else
                    {
                        await DisplayAlert("Aviso", "Ha ocurrido un error !", "Ok");
                    }

                    obtenerListaSitios();
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Aviso", ex.Message, "Ok");
            }
        }

        private async void obtenerListaSitios()
        {
            var listaSitios = await App.SitiosDB.listaSitios();
            //Creamos un colleccion observable para que los cambios que se realizan en el modelo se reflejen de manera automatica en el View
            ObservableCollection<Sitios> observableCollectionFotos = new ObservableCollection<Sitios>();
            ListaSitios.ItemsSource = observableCollectionFotos;
            foreach (Sitios imagen in listaSitios)
            {
                observableCollectionFotos.Add(imagen);
            }
        }
    }
}