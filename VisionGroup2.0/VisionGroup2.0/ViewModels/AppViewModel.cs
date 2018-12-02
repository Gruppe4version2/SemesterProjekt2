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
        /// Propertien bliver brugt til at lave databinding via navigationviewet
        /// Som kontrollerer MainPage viewet
        /// </summary>
        public NavigationViewItem SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set
            {
                _selectedMenuItem = value;

                if (_selectedMenuItem == null) return;
                // Man trækker _selectedMenuItem's Tag værdi ud over giver den en variable(tag)
                // Denne værdi burde matche en af værdierne fra NavigationCommands dictonarien
                string tag = _selectedMenuItem.Tag.ToString();

                // Hvis tag ikke passer med en key, laver vi følgende exception
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
        /// Metoden bliver brugt til at lave specifikke navigationer i applikationen
        /// Nøglen i dictionarien skal passe med Tag værdien i MainView

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
