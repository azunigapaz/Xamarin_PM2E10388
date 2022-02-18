using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace PM2E10388.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMaps : ContentPage
    {
        String mapEtiqueta, mapDireccion;
        float mapLongitud, mapLatitud;

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        public PageMaps(String mapEtiquetaCt, String mapDireccionCt, float mapLongitudCt, float mapLatitudCt)
        {
            InitializeComponent();
            mapEtiqueta = mapEtiquetaCt;
            mapDireccion = mapDireccionCt;
            mapLongitud = mapLongitudCt;
            mapLatitud = mapLatitudCt;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Pin ubicacion = new Pin();
            ubicacion.Label = mapEtiqueta;
            ubicacion.Address = mapDireccion;
            ubicacion.Type = PinType.Place;
            ubicacion.Position = new Position(mapLongitud, mapLatitud);
            mapa.Pins.Add(ubicacion);
        }
    }
}