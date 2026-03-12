using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Elevate.Models;
using Elevate.Services;
using System.Collections.ObjectModel;

namespace Elevate.ViewModels
{

    public partial class TestingDragAndDropViewModel : ObservableObject
    {
        public TestingDragAndDropViewModel()
        {

        }

        public ObservableCollection<string> Field1 = new ObservableCollection<string> { "test1", "test2", "test3", "test4", "test5" };
        public ObservableCollection<string> Field2 = new ObservableCollection<string> { "test1", "test2", "test3", "test4", "test5" };
        public ObservableCollection<string> Field3 = new ObservableCollection<string> { };
        public ObservableCollection<string> Field4 = new ObservableCollection<string> { };
    }
}