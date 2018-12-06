using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisionGroup2._0.Catalogs;
using VisionGroup2._0.Factories;

namespace VisionGroup2._0.Commands
{
    class AddCommand : CommandBase

    {
        private CostumerCatalog _costumerCatalog;
        private CostumerFactory _costumerFactory;

        public AddCommand(CostumerCatalog costumerCatalog, CostumerFactory costumerFactory)
        {
            _costumerCatalog = costumerCatalog;
            _costumerFactory = costumerFactory;
        }
        protected override void Execute()
        {
            _costumerCatalog.Add(_costumerFactory.Create());
        }
    }
}
