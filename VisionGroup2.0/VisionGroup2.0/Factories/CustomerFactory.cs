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

    public class CostumerFactory : ICostumerFactory
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
                if (costumer.CvrNr == this.NewCostumer.CvrNr || costumer.Name == this.NewCostumer.Name || costumer.Email == this.NewCostumer.Email || costumer.PhoneNr == this.NewCostumer.PhoneNr)
                {
                    return false;
                }

            return true;
        }

        public void Create()
        {
            if (this.CanCreate(NewCostumer))
            {
                this._costumerCatalog.Add(this.NewCostumer);
            }
        }


    }
}
