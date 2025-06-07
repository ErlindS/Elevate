namespace Elevate
{
	
    public partial class Sequence : ContentPage
    {
        public Sequence()
        {
            InitializeComponent();

        }

        private void OnDragStarting(object sender, DragStartingEventArgs e)
        {
            e.Data.Text = "Task A"; // Or some unique ID or object info
        }

        private void OnDragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
        }

        private async void OnDrop(object sender, DropEventArgs e)
        {
            var data = await e.Data.GetTextAsync();

            if (sender is Border targetBorder)
            {
                targetBorder.Content = new Label
                {
                    Text = data,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };
            }
        }
    }
}