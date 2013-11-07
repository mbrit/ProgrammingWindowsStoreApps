using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Windows.UI.Xaml.Controls;

namespace StreetFoo.Client.UI
{
    public static class FrameExtender
    {
        public static void ShowView(this Frame frame, Type viewModelType, object parameter = null)
        {
            // get the concrete handler and as the frame to flip... (note we use the ViewFactory,
            // not the ViewModelFactory here...)

            foreach (var type in typeof(FrameExtender).GetTypeInfo().Assembly.GetTypes())
            {
                var attr = (ViewModelAttribute)type.GetCustomAttribute<ViewModelAttribute>();
                if (attr != null && viewModelType.IsAssignableFrom(attr.ViewModelInterfaceType))
                {
                    // show...
                    frame.Navigate(type, parameter);
                }
            }
        }
    }
}
