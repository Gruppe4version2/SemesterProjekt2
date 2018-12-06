using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisionGroup2._0.Catalogs;
using VisionGroup2._0.ViewModels;

namespace VisionGroup2._0.Commands
{
    class CreateCommand : CommandBase
    {

        private ProjectCatalog _projectCatalog;
        private ProjectViewModel _projectViewModel;

        public CreateCommand(ProjectCatalog projectCatalog)
        {
            _projectCatalog = projectCatalog;
            _projectViewModel = new ProjectViewModel();
        }

        protected override void Execute()
        {

            _projectCatalog.Add(_projectViewModel.SelectedProject);
            _projectViewModel.SelectedProject = null;
            _projectViewModel.Refresh();

        }
    }
}
