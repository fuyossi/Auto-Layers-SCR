using Auto_Layers_SCR.ViewModels;
using Microsoft.UI.Xaml.Controls;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace Auto_Layers_SCR.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
        }

        public async void ShowDialog(string title, string content)
        {
            ContentDialog dialog = new()
            {
                // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
                XamlRoot = this.XamlRoot,
                Title = title,
                Content = content,
                CloseButtonText = "OK"
            };
            await dialog.ShowAsync();
            return;
        }

        public async Task<bool> ShowYesNoDialog(string title, string content)
        {
            ContentDialog dialog = new()
            {
                // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
                XamlRoot = this.XamlRoot,
                Title = title,
                Content = content,
                PrimaryButtonText = "Yes",
                CloseButtonText = "No"
            };
            ContentDialogResult dialogresult = await dialog.ShowAsync();
            if (dialogresult == ContentDialogResult.Primary)
            {
                return true;
            }
            return false;
        }

        public async void SaveFile(string content, string extensionName, string extension)
        {
            FileSavePicker savePicker = new()
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary
            };
            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add(extensionName, [extension]);
            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = "New File";

            nint windowHandle = WindowNative.GetWindowHandle(App.m_window);
            InitializeWithWindow.Initialize(savePicker, windowHandle);
            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                // write to file
                using (Stream stream = await file.OpenStreamForWriteAsync())
                using (StreamWriter writer = new(stream, new UTF8Encoding(encoderShouldEmitUTF8Identifier: true)))
                {
                    writer.Write(content);
                }
                ShowDialog("File was saved", "File " + file.Name + " was saved.");
            }
            else
            {
                ShowDialog("File couldn't be saved", "Operation cancelled.");
            }
        }

        private async void LoadJSONButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            var dialogResult = await ShowYesNoDialog("Data will be lost", "The information of the currently displayed layer will be overwritten.\r\nDo you want to continue?");
            if (dialogResult)
            {
                FileOpenPicker openPicker = new()
                {
                    ViewMode = PickerViewMode.Thumbnail,
                    SuggestedStartLocation = PickerLocationId.DocumentsLibrary
                };
                openPicker.FileTypeFilter.Add(".json");

                nint windowHandle = WindowNative.GetWindowHandle(App.m_window);
                InitializeWithWindow.Initialize(openPicker, windowHandle);
                StorageFile file = await openPicker.PickSingleFileAsync();

                if (file != null)
                {
                    string json = await FileIO.ReadTextAsync(file);
                    HomePageViewModel.LoadJSON(json);
                    ShowDialog("Loading completed.", "JSON file loading completed.");
                }
                else
                {
                    ShowDialog("File couldn't be opened", "Operation cancelled.");
                }
            }
        }

        private void DeleteRowButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            int rowNumber = LayerDataGrid.SelectedIndex;

            if (rowNumber < 0)
            {
                ShowDialog("Plese select the row", "Error: Please select the row you want to delete.");
                return;
            }
            HomePageViewModel.DeleteRow(rowNumber);
        }

        private void GenerateSCRButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            var result = HomePageViewModel.CreateSCR();
            if (!result.Item1)
            {
                SaveFile(result.Item2, "AutoCAD Script File", ".scr");
            }
            else
            {
                ShowDialog(result.Item3, result.Item4);
            }
        }

        private void GenerateJSONButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            var result = HomePageViewModel.CreateJSON();
            if (!result.Item1)
            {
                SaveFile(result.Item2, "JSON File", ".json");
            }
            else
            {
                ShowDialog(result.Item3, result.Item4);
            }
        }
    }
}
