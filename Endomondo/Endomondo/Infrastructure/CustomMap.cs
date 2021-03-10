using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Endomondo.Infrastructure
{
    public class CustomMap : Map
    {
        public static readonly BindableProperty CustomPolylineProperty = BindableProperty.Create("CustomPolyline", typeof(Polyline), typeof(CustomMap),
            propertyChanged: (bindableObject, oldValue, newValue) =>
            {
                Map map = bindableObject as Map;
                map.MapElements.Clear();
                map.MapElements.Add((Polyline)newValue);
            });

        public Polyline CustomPolyline
        {
            get => (Polyline)GetValue(CustomPolylineProperty);
            set => SetValue(CustomPolylineProperty, value);
        }

        public CustomMap() : base()
        {

        }
        public CustomMap(MapSpan region) : base(region)
        {

        }
    }
}
