using Elevate.ViewModels;
using Microsoft.Maui.ApplicationModel.DataTransfer;

namespace Elevate.Views;

public partial class TestingDragAndDropPage : ContentPage
{
    public TestingDragAndDropPage(TestingDragAndDropViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

   
}