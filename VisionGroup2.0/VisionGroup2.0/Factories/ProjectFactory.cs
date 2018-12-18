namespace VisionGroup2._0.Factories
{
    using System.Linq;

    using VisionGroup2._0.Catalogs;
    using VisionGroup2._0.DomainClasses;
    using VisionGroup2._0.Interfaces;

    public class ProjectFactory : IFactory
    {
        private readonly ProjectCatalog _projectCatalog;

        public ProjectFactory()
        {
            this.NewProject = new Project();
            this._projectCatalog = ProjectCatalog.Instance;
        }

        public Project NewProject { get; set; }

        public bool CanCreate(Project newProject)
        {
            if (newProject.Name == null || newProject.Deadline == null || CostumerCatalog
                                                                          .Instance.CostumerList
                                                                          .Where(
                                                                                 p => p.CostumerId
                                                                                      == newProject.CostumerId).ToList()
                                                                          .Count != 1)
            {
                return false;
            }

            foreach (Project project in this._projectCatalog.ProjectList)
            {
                if (project.Name == newProject.Name)
                {
                    return false;
                }
            }

            this.NewProject = newProject;
            return true;
        }

        public void Create()
        {
            if (this.CanCreate(this.NewProject))
            {
                this._projectCatalog.Add(this.NewProject);
            }
        }
    }
}