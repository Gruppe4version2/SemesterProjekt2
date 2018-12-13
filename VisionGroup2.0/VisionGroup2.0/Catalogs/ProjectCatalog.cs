using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VisionGroup2._0.Annotations;
using VisionGroup2._0.DomainClasses;
using VisionGroup2._0.Interfaces;

namespace VisionGroup2._0.Catalogs
{
    public class ProjectCatalog : ICatalog<Project>
    {
        #region Singleton

        private static ProjectCatalog _instance;

        public static ProjectCatalog Instance
        {
            get
            {
                _instance = _instance ?? (_instance = new ProjectCatalog());
                return _instance;
            }
        }

        #endregion

        private List<Project> _projectList;

        public List<Project> ProjectList
        {
            get
            {
                if (this._projectList != null)
                {
                    return this._projectList;
                }
                else
                {
                    Load();
                    return this._projectList;

                }
            }
            set
            {
                this._projectList = value;
            }
        }


        public void Add(Project item)
        {
            using (var db = new DbContextVisionGroup())
            {
                db.Projects.Add(item);
                ProjectList.Add(item);
                db.SaveChanges();
            }
        }

        public void Remove(Project item)
        {
            using (var db = new DbContextVisionGroup())
            {
                db.Projects.Remove(item);
                ProjectList.Remove(item);
                foreach (var projectforEmployee in ProjectForEmployeesCatalog.Instance.ProjectsForEmployeesList.Where(p => p.ProjectId == item.ProjectId).ToList())
                {
                    db.ProjectsForEmployees.Remove(projectforEmployee);
                    ProjectForEmployeesCatalog.Instance.ProjectsForEmployeesList.Remove(projectforEmployee);
                }

                db.SaveChanges();
            }
        }




        public void Update(Project item)
        {
            using (var db = new DbContextVisionGroup())
            {
                db.Projects.Update(item);
                this.ProjectList[this.ProjectList.FindIndex(project => project.ProjectId == item.ProjectId)] = item;
                db.SaveChanges();
            }
        }

        public void Load()
        {
            using (var db = new DbContextVisionGroup())
            {
                _projectList = db.Projects.ToList();
            }
        }
    }
}
