using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisionGroup2._0.DomainClasses;
using VisionGroup2._0.Interfaces;

namespace VisionGroup2._0.Catalogs
{
    class ProjectForEmployeesCatalog : ICatalog<ProjectsForEmployee>
    {
        #region Singleton
        private static ProjectForEmployeesCatalog _instance;
        public static ProjectForEmployeesCatalog Instance
        {
            get
            {
                _instance = _instance ?? (_instance = new ProjectForEmployeesCatalog());
                return _instance;
            }
        }
        #endregion
        private List<ProjectsForEmployee> _projectsForEmployees;
        public List<ProjectsForEmployee> ProjectsForEmployeesList
        {
            get
            {
                if (this._projectsForEmployees != null)
                {
                    return this._projectsForEmployees;
                }
                else
                {
                    Load();
                    return this._projectsForEmployees;

                }
            }
            set
            {
                this._projectsForEmployees = value;
            }
        }
        public void Add(ProjectsForEmployee item)
        {

            using (var db = new DbContextVisionGroup())
            {
                db.ProjectsForEmployees.Add(item);
                db.SaveChanges();
            }
        }

        public void Remove(ProjectsForEmployee item)
        {
            using (var db = new DbContextVisionGroup())
            {
                db.ProjectsForEmployees.Remove(item);
                db.SaveChanges();
            }
        }

        public void Update(ProjectsForEmployee item)
        {
            using (var db = new DbContextVisionGroup())
            {
                db.ProjectsForEmployees.Update(item);
                db.SaveChanges();
            }
        }

        public void Load()
        {

            using (var db = new DbContextVisionGroup())
            {
                _projectsForEmployees = db.ProjectsForEmployees.ToList();
            }
        }
    }
}
