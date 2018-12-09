using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisionGroup2._0.DomainClasses;
using VisionGroup2._0.Interfaces;

namespace VisionGroup2._0.Factories
{
    using VisionGroup2._0.Catalogs;

    public class CostumerFactory : IFactory
    {
        private CostumerCatalog _costumerCatalog;
        public CostumerFactory()
        {
            this.NewCostumer = new Costumer();
            this._costumerCatalog = CostumerCatalog.Instance;
        }

        public Costumer NewCostumer { get; set; }
        public bool CanCreate(Costumer newCostumer)
        {
            foreach (Costumer costumer in this._costumerCatalog.CostumerList)
            {
                if (newCostumer.Name == null || newCostumer.Email == null)
                {
                    return false;
                }
                if (costumer.CvrNr == newCostumer.CvrNr || costumer.Name == newCostumer.Name || costumer.Email == newCostumer.Email 
                    || costumer.PhoneNr == newCostumer.PhoneNr 
                    || newCostumer.Name.Length < 3 
                    || newCostumer.Email.Length < 3)
                {
                    return false;
                }
            }

            NewCostumer = newCostumer;
            return true;
        }

        public void Create()
        {
            if (this.CanCreate(this.NewCostumer))
            {
                this._costumerCatalog.Add(this.NewCostumer);
            }
        }
    }
}
