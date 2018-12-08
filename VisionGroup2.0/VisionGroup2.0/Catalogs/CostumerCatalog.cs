using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisionGroup2._0.DomainClasses;
using VisionGroup2._0.Interfaces;

namespace VisionGroup2._0.Catalogs
{
    public class CostumerCatalog : ICatalog<Costumer>
    {
        #region Singleton
        private static CostumerCatalog _instance;
        public static CostumerCatalog Instance
        {
            get
            {
                _instance = _instance ?? (_instance = new CostumerCatalog());
                return _instance;
            }
        }
        #endregion
        private List<Costumer>_costumerList;
        public List<Costumer> CostumerList
        {
            get
            {
                if (this._costumerList != null)
                {
                    return this._costumerList;
                }
                else
                {
                    Load();
                    return this._costumerList;

                }
            }
            set
            {
                this._costumerList = value;
            }
        }

       
        public void Add(Costumer item)
        {
            using (var db = new DbContextVisionGroup())
            {
                db.Costumers.Add(item);
                this._costumerList.Add(item);
                db.SaveChanges();
            }
        }
        
        public void Remove(Costumer item)
        {
            using (var db = new DbContextVisionGroup())
            {
                db.Costumers.Remove(item);
                db.SaveChanges();
            }
        }



        
        public void Update(Costumer item)
        {
            using (var db = new DbContextVisionGroup())
            {
                db.Costumers.Update(item);
                db.SaveChanges();
            }
        }

        public void Load()
        {
            using (var db = new DbContextVisionGroup())
            {
                CostumerList = db.Costumers.ToList();
            }
        }
    }
}
