using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Auto_Layers_SCR
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);

            navView.SelectedItem = navView.MenuItems[0];
            navView.IsSettingsVisible = false;
            NavigateToPage("home");
        }
        private void navView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            string tag = args.InvokedItemContainer.Tag.ToString()!;
            NavigateToPage(tag);
        }

        private void NavigateToPage(string tag)
        {
            Type pageType = null;
            switch (tag)
            {
                case "home":
                    pageType = typeof(HomePage);
                    break;
                case "settings":
                    pageType = typeof(SettingsPage);
                    break;
            }

            if (pageType != null && contentFrame.CurrentSourcePageType != pageType)
            {
                contentFrame.Navigate(pageType);
            }
        }
    }
}
