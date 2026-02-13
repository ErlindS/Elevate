using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Elevate.Models
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(ElevateTask), "elevateTask")]
    public interface IElevateTaskComponent
    {
        string Name { get; }
        int Id { get; }
        bool IsDone { get; }
        int Priority { get; }
        string Description { get; }
        string Category { get; }
        IElevateTaskComponent ParentTask { get; }
        ObservableCollection<IElevateTaskComponent> SubTasks { get; }
    }
}
