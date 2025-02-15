using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;

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
