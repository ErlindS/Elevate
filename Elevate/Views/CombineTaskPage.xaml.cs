using Microsoft.Maui.Controls; // Make sure this is included for ContentPage
using Elevate.ViewModels;
using Elevate.Models;
namespace Elevate
{
    public partial class CombineTaskPage : ContentPage
    {
        public CombineTaskPage(CombineTaskViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel; // This correctly sets the BindingContext via DI
        }
    }
}