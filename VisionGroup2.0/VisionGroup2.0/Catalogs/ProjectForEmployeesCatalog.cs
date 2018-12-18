namespace VisionGroup2._0.Catalogs
{
    using System.Collections.Generic;
    using System.Linq;

    using VisionGroup2._0.DomainClasses;
    using VisionGroup2._0.Interfaces;

    internal class ProjectForEmployeesCatalog : ICatalog<ProjectsForEmployee>
    {
        private static ProjectForEmployeesCatalog _instance;

        private List<ProjectsForEmployee> _projectsForEmployees;

        public static ProjectForEmployeesCatalog Instance
        {
            get
            {
                _instance = _instance ?? (_instance = new ProjectForEmployeesCatalog());
                return _instance;
            }
        }

        public List<ProjectsForEmployee> ProjectsForEmployeesList
        {
            get
            {
                if (this._projectsForEmployees != null)
                {
                    return this._projectsForEmployees;
                }

                this.Load();
                return this._projectsForEmployees;
            }

            set
            {
                this._projectsForEmployees = value;
            }
        }

        public void Add(ProjectsForEmployee item)
        {
            using (DbContextVisionGroup db = new DbContextVisionGroup())
            {
                db.ProjectsForEmployees.Add(item);
                EmployeeCatalog
                    .Instance
                    .EmployeeList[EmployeeCatalog.Instance.EmployeeList.FindIndex(
                                                                                  employee =>
                                                                                      employee.EmployeeId
                                                                                      == item.EmployeeId)]
                    .ProjectsForEmployees.Add(item);
                db.SaveChanges();
            }
        }

        public void Load()
        {
            using (DbContextVisionGroup db = new DbContextVisionGroup())
            {
                this._projectsForEmployees = db.ProjectsForEmployees.ToList();
            }
        }

        public void Remove(ProjectsForEmployee item)
        {
            using (DbContextVisionGroup db = new DbContextVisionGroup())
            {
                db.ProjectsForEmployees.Remove(item);
                EmployeeCatalog
                    .Instance
                    .EmployeeList[EmployeeCatalog.Instance.EmployeeList.FindIndex(
                                                                                  employee =>
                                                                                      employee.EmployeeId
                                                                                      == item.EmployeeId)]
                    .ProjectsForEmployees.Remove(item);
                db.SaveChanges();
            }
        }

        public void Update(ProjectsForEmployee item)
        {
            using (DbContextVisionGroup db = new DbContextVisionGroup())
            {
                db.ProjectsForEmployees.Update(item);
                db.SaveChanges();
            }
        }
    }
}