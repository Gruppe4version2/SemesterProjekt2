namespace VisionGroup2._0.Catalogs
{
    using System.Collections.Generic;
    using System.Linq;

    using VisionGroup2._0.DomainClasses;
    using VisionGroup2._0.Interfaces;

    public class ProjectCatalog : ICatalog<Project>
    {
        private static ProjectCatalog _instance;

        private List<Project> _projectList;

        public static ProjectCatalog Instance
        {
            get
            {
                _instance = _instance ?? (_instance = new ProjectCatalog());
                return _instance;
            }
        }

        public List<Project> ProjectList
        {
            get
            {
                if (this._projectList != null)
                {
                    return this._projectList;
                }

                this.Load();
                return this._projectList;
            }

            set
            {
                this._projectList = value;
            }
        }

        public void Add(Project item)
        {
            using (DbContextVisionGroup db = new DbContextVisionGroup())
            {
                db.Projects.Add(item);
                this.ProjectList.Add(item);
                db.SaveChanges();
            }
        }

        public void Load()
        {
            using (DbContextVisionGroup db = new DbContextVisionGroup())
            {
                this._projectList = db.Projects.ToList();
            }
        }

        public void Remove(Project item)
        {
            using (DbContextVisionGroup db = new DbContextVisionGroup())
            {
                db.Projects.Remove(item);
                this.ProjectList.Remove(item);
                foreach (ProjectsForEmployee projectforEmployee in ProjectForEmployeesCatalog
                                                                   .Instance.ProjectsForEmployeesList
                                                                   .Where(p => p.ProjectId == item.ProjectId).ToList())
                {
                    db.ProjectsForEmployees.Remove(projectforEmployee);
                    ProjectForEmployeesCatalog.Instance.ProjectsForEmployeesList.Remove(projectforEmployee);
                }

                db.SaveChanges();
            }
        }

        public void Update(Project item)
        {
            using (DbContextVisionGroup db = new DbContextVisionGroup())
            {
                db.Projects.Update(item);
                this.ProjectList[this.ProjectList.FindIndex(project => project.ProjectId == item.ProjectId)] = item;
                db.SaveChanges();
            }
        }
    }
}