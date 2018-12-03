using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisionGroup2._0.DomainClasses;
using VisionGroup2._0.Interfaces;

namespace VisionGroup2._0.Catalogs
{
    public class ProjectCatalog : ICatalog<Project>
    {
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
                db.SaveChanges();
            }
        }

        public void Remove(Project item)
        {
            using (var db = new DbContextVisionGroup())
            {
                db.Projects.Remove(item);
                db.SaveChanges();
            }
        }




        public void Update(Project item)
        {
            using (var db = new DbContextVisionGroup())
            {
                db.Projects.Update(item);
                db.SaveChanges();
            }
        }

        public void Load()
        {
            using (var db = new DbContextVisionGroup())
            {
                ProjectList = db.Projects.ToList();
            }
        }
    }
}
