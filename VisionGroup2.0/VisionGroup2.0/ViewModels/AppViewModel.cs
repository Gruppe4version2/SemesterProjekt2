namespace VisionGroup2._0.ViewModels
{
    using System;

    using VisionGroup2._0.Commands;
    using VisionGroup2._0.Views.Domain;

    using Windows.UI.Xaml.Controls;

    public class AppViewModel : AppViewModelBase
    {
        private NavigationViewItem _selectedMenuItem;

        public AppViewModel()
        {
            this._selectedMenuItem = null;
        }

        /// <summary>
        ///     Propertien bliver brugt til at lave databinding via navigationviewet
        ///     Som kontrollerer MainPage viewet
        /// </summary>
        public NavigationViewItem SelectedMenuItem
        {
            get
            {
                return this._selectedMenuItem;
            }

            set
            {
                this._selectedMenuItem = value;

                if (this._selectedMenuItem == null)
                {
                    return;
                }

                // Man trækker _selectedMenuItem's Tag værdi ud over giver den en variable(tag)
                // Denne værdi burde matche en af værdierne fra NavigationCommands dictonarien
                string tag = this._selectedMenuItem.Tag.ToString();

                // Hvis tag ikke passer med en key, laver vi følgende exception
                if (!this.NavigationCommands.ContainsKey(tag))
                {
                    throw new ArgumentException($"Menu entry {tag} has no matching navigation command");
                }

                this.NavigationCommands[tag].Execute(null);
            }
        }

        /// <summary>
        ///     Metoden bliver brugt til at lave specifikke navigationer i applikationen
        ///     Nøglen i dictionarien skal passe med Tag værdien i MainView
        /// </summary>
        public override void AddCommands()
        {
            this.NavigationCommands.Add("OpenProjectView", new NavigationCommand(this.AppFrame, typeof(ProjectView)));
            this.NavigationCommands.Add("OpenCostumerView", new NavigationCommand(this.AppFrame, typeof(CostumerView)));
            this.NavigationCommands.Add("OpenEmployeeView", new NavigationCommand(this.AppFrame, typeof(Employee)));
        }
    }
}