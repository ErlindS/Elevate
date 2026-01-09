namespace Elevate.Models
{
    public interface IElevateTaskComponent
    {
        string Name { get; }
        int Id { get; }
        IElevateTaskComponent ParentTask { get; }
        List<IElevateTaskComponent> SubTasks { get; }
    }

}
