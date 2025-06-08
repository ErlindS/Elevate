namespace Elevate
{
	
    public partial class Sequence : ContentPage
    {
        public Sequence()
        {
            InitializeComponent();

        }

        string draggedItem;
        /*
        void OnDragStarting(object sender, DragStartingEventArgs e)
        {
            draggedItem = (sender as Element)?.BindingContext as string;
            e.Data.Properties.Add("Item", draggedItem);
        }

        void OnDragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Move;
        }

        void OnDrop(object sender, DropEventArgs e)
        {
            if (e.Data.Properties.TryGetValue("Item", out object itemObj)
                && itemObj is string droppedItem)
            {
                var collection = (BindingContext as MainViewModel)?.Items;
                if (collection == null)
                    return;

                // Optional: Neue Position berechnen, z. B. basierend auf Drop-Location
                // Hier: ans Ende schieben
                collection.Remove(droppedItem);
                collection.Add(droppedItem);
            }
        }
        */

    }
}