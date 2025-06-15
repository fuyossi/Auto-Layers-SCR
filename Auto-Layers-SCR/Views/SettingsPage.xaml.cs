using Microsoft.UI.Xaml.Controls;
using System;

namespace Auto_Layers_SCR.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();

            if (IntPtr.Size == 4)
            {
                appVersion.Text += " (x86)";
            }
            else
            {
                appVersion.Text += " (x64)";
            }

            thirdPartyLicenseText.Text = """
                This application uses the following third-party libraries:
                CommunityToolkit.Mvvm (MIT License)
                Copyright (c) .NET Foundation and Contributors
                https://github.com/CommunityToolkit/dotnet/blob/main/License.md

                CommunityToolkit.WinUI.UI.Controls.DataGrid (MIT License)
                Copyright (c) .NET Foundation and Contributors
                https://github.com/CommunityToolkit/WindowsCommunityToolkit/blob/main/License.md

                Microsoft.WindowsAppSDK (MIT License)
                Copyright (c) Microsoft Corporation.
                https://github.com/microsoft/WindowsAppSDK/blob/main/LICENSE
                """;
        }
    }
}
