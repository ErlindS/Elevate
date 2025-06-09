using CommunityToolkit.Maui.Core.Views;
using Microsoft.Maui.Layouts;

namespace Elevate
{
    public partial class Mapping : ContentPage
    {
        public Mapping()
        {
            //InitializeComponent();
        }
        
        private async void OwnDragStarting(object sender, DragStartingEventArgs e)
        {
           //await DisplayAlert("1", "2", "3");
        }
        private async void OwnDropOver(object sender, DragStartingEventArgs e)
        {
           // await DisplayAlert("1", "2", "3");
        }
        private async void OwnDropGesture(object sender, DragStartingEventArgs e)
        {
           // await DisplayAlert("1", "2", "3");
        }
    }

}
