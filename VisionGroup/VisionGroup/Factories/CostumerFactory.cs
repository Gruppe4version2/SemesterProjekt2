using VisionGroup.Interfaces;

namespace VisionGroup.Factories
{
    public class CostumerFactory : ICostumerFactory
    {
        public Costumer Create()
        {
           return new Costumer();
        }
    }
}