using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

using VisionGroup2._0.Views.Domain;

using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238
namespace VisionGroup2._0.Views.New_objects
{
    using Windows.System;

    using VisionGroup2._0.ViewModels.Create;

    using FocusState = Windows.UI.Xaml.FocusState;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewCostumer : Page
    {
        public NewCostumer()
        {
            this.InitializeComponent();
            createViewModel = new CreateCostumerViewModel();
            DataContext = createViewModel;
        }
        CreateCostumerViewModel createViewModel { get; set; }
        

        public void Button_navigation_CostumerView(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CostumerView), null);
        }

        private void TextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}
