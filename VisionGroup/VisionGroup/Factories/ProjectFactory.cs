using VisionGroup.Interfaces;

namespace VisionGroup.Factories
{
    public class ProjectFactory : IProjectFactory
    {
        public Project Create()
        {
            return new Project();
        }
    }
}