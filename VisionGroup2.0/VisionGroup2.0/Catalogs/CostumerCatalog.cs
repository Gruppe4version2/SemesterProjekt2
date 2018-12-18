namespace VisionGroup2._0.Catalogs
{
    using System.Collections.Generic;
    using System.Linq;

    using VisionGroup2._0.DomainClasses;
    using VisionGroup2._0.Interfaces;

    public class CostumerCatalog : ICatalog<Costumer>
    {
        private static CostumerCatalog _instance;

        private List<Costumer> _costumerList;

        public static CostumerCatalog Instance
        {
            get
            {
                _instance = _instance ?? (_instance = new CostumerCatalog());
                return _instance;
            }
        }

        public List<Costumer> CostumerList
        {
            get
            {
                if (this._costumerList != null)
                {
                    return this._costumerList;
                }

                this.Load();
                return this._costumerList;
            }

            set
            {
                this._costumerList = value;
            }
        }

        public void Add(Costumer item)
        {
            using (DbContextVisionGroup db = new DbContextVisionGroup())
            {
                db.Costumers.Add(item);
                this._costumerList.Add(item);
                db.SaveChanges();
            }
        }

        public void Load()
        {
            using (DbContextVisionGroup db = new DbContextVisionGroup())
            {
                this._costumerList = db.Costumers.ToList();
            }
        }

        public void Remove(Costumer item)
        {
            using (DbContextVisionGroup db = new DbContextVisionGroup())
            {
                db.Costumers.Remove(item);
                this._costumerList.Remove(item);
                db.SaveChanges();
            }
        }

        public void Update(Costumer item)
        {
            using (DbContextVisionGroup db = new DbContextVisionGroup())
            {
                db.Costumers.Update(item);
                this.CostumerList[this.CostumerList.FindIndex(costumer => costumer.CostumerId == item.CostumerId)] =
                    item;
                db.SaveChanges();
            }
        }
    }
}