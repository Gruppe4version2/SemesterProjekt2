using System.Collections.Generic;
using VisionGroup.Interfaces;

namespace VisionGroup
{
    public class CostumerCatalog : ICatalog<Costumer>
    {

        public List<Costumer> CostumerList { get; set; }
        public void Add(Costumer item)
        {
            CostumerList.Add(item);
        }

        public void Remove(Costumer item)
        {
            CostumerList.Remove(item);
        }



        // Finder index for gamle item, som har et bestemt ProjectId
        // Den erstatter med ny item med samme ProjectId
        public void Update(Costumer item)
        {
            CostumerList[CostumerList
                .FindIndex(a => a.CostumerId == item.CostumerId)] = item;
        }

        public void Load(Costumer item)
        {
            throw new System.NotImplementedException();
        }
    }
}