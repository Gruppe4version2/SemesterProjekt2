﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    using VisionGroup2._0.Catalogs;
    using VisionGroup2._0.ViewModels.Create;
    using VisionGroup2._0.Views.Domain;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewProject : Page
    {
        public NewProject()
        {
            this.InitializeComponent();
            this.CreateViewModel = new CreateProjectViewModel();
            this.DataContext = this.CreateViewModel;
        }

        public CreateProjectViewModel CreateViewModel { get; set; }


        public void Button_navigation_ProjectView(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ProjectView), null);
        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // Only get results when it was a user typing,
            // otherwise assume the value got filled in by TextMemberPath
            // or the handler for SuggestionChosen.
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                //Set the ItemsSource to be your filtered dataset
                var dataset = CostumerCatalog.Instance.CostumerList.Where(n => n.Name.Contains(sender.Text)).Select(p => p.Name).ToList();
                sender.ItemsSource = dataset;
            }
        }


        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            // Set sender.Text. You can use args.SelectedItem to build your text string.
            sender.Text = args.SelectedItem.ToString();
        }
    }
}
