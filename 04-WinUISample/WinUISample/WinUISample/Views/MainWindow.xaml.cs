using System;
using System.IO;
using System.Linq;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using WinUISample.Services;
using System.Reflection;
using WinUISample.ViewModels;

namespace WinUISample.Views
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainPageViewModel ViewModel => splitView.DataContext as MainPageViewModel;

        public MainWindow()
        {
            this.InitializeComponent();
            this.Activated += MainWindow_Activated;
            splitView.Loaded += SplitView_Loaded;
        }

        private void SplitView_Loaded(object sender, RoutedEventArgs e)
        {
            string currentTheme = splitView.RequestedTheme == ElementTheme.Default ? App.Current.RequestedTheme.ToString() : splitView.RequestedTheme.ToString();
            themePanel.Children.Cast<RadioButton>().FirstOrDefault(c => c?.Tag?.ToString() == currentTheme).IsChecked = true;
        }

        private async void MainWindow_Activated(object sender, WindowActivatedEventArgs args)
        {
            RegistryService registryService = new RegistryService();
            if (registryService.IsFirstTimeLaunch())
            {
                FirstLaunchTip.IsOpen = true;
            }
            await ViewModel.LoadData();
        }
     

        void OnThemeRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            var selectedTheme = ((RadioButton)sender)?.Tag?.ToString();
            if (selectedTheme != null)
            {
                ((sender as RadioButton).XamlRoot.Content as SplitView).RequestedTheme = GetEnum<ElementTheme>(selectedTheme);
            }
        }

        private TEnum GetEnum<TEnum>(string text) where TEnum : struct
        {
            if (!typeof(TEnum).GetTypeInfo().IsEnum)
            {
                throw new InvalidOperationException("Generic parameter 'TEnum' must be an enum.");
            }
            return (TEnum)Enum.Parse(typeof(TEnum), text);
        }

        private void Hero_ImageOpened(object sender, RoutedEventArgs e)
        {
            MainContent.Margin = new Thickness(MainContent.Margin.Left, HeroImage.Height + 50, MainContent.Margin.Right, MainContent.Margin.Bottom);
            HeroContainer.SizeChanged += HeroImage_SizeChanged;
        }

        private void HeroImage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MainContent.Margin = new Thickness(MainContent.Margin.Left, HeroImage.Height + 50, MainContent.Margin.Right, MainContent.Margin.Bottom);
        }
    }
}
