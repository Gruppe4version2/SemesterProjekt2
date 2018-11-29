using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using VisionGroup2._0.Annotations;
using VisionGroup2._0.Commands;

namespace VisionGroup2._0.ViewModels
{
    public class AppViewModel : AppViewModelBase
    {
        #region Instance fields
        private NavigationViewItem _selectedMenuItem;
        #endregion

        #region Constructor
        public AppViewModel()
        {
            _selectedMenuItem = null;
        }
        #endregion

        #region Properties
        /// <summary>
        /// This property is used for Data Binding by the NavigationView
        /// control in the main application view.
        /// </summary>
        public NavigationViewItem SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set
            {
                _selectedMenuItem = value;

                if (_selectedMenuItem == null) return;

                // The value of the Tag property is extracted from the selected Menu item.
                // This value should match one of the available command keys in the
                // NavigationCommands dictionary.
                string tag = _selectedMenuItem.Tag.ToString();
                if (!NavigationCommands.ContainsKey(tag))
                {
                    throw new ArgumentException($"Menu entry {tag} has no matching navigation command");
                }
                NavigationCommands[tag].Execute(null);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// This method is used for adding application-specific navigation
        /// commands into the NavigationCommands dictionary. The keys used
        /// for the commands should match Tag values on menu items from the
        /// main application view.
        /// </summary>
        public override void AddCommands()
        {
            NavigationCommands.Add("OpenProjectView", new NavigationCommand(AppFrame, typeof(Views.Domain.ProjectView)));
            NavigationCommands.Add("OpenCostumerView", new NavigationCommand(AppFrame, typeof(Views.Domain.CostumerView)));
            NavigationCommands.Add("OpenEmployeeView", new NavigationCommand(AppFrame, typeof(Views.Domain.Employee)));


        }
        #endregion
    }
}
