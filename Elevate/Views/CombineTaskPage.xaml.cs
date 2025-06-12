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

        void OnDragStarting(object sender, DragStartingEventArgs e)
        {
            if (sender is BindableObject bindable &&
                bindable.BindingContext is GroupElevateTask draggedTask)
            {
                e.Data.Properties["DraggedGroup"] = draggedTask;
            }
        }

        void OnDrop(object sender, DropEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnDrop triggered");

            if (e.Data.Properties.TryGetValue("DraggedGroup", out var draggedObj) &&
                draggedObj is GroupElevateTask draggedGroup)
            {
                if (sender is BindableObject bindable &&
                    bindable.BindingContext is GroupElevateTask targetGroup &&
                    !ReferenceEquals(draggedGroup, targetGroup))
                {
                    targetGroup.Add(draggedGroup);
                    System.Diagnostics.Debug.WriteLine($"Added {draggedGroup.Name} to {targetGroup.Name}");

                    if (BindingContext is CombineTaskViewModel vm)
                    {
                        vm.Projects.Remove(draggedGroup);
                    }
                }
            }
        }

    }
}