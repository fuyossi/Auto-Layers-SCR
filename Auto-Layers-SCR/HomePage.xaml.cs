using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace Auto_Layers_SCR
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        private ObservableCollection<Layer> layers;
        int CurrentLineNumber = 1;

        public HomePage()
        {
            this.InitializeComponent();

            layers = new ObservableCollection<Layer>
            {
                new Layer("01_", true, false, false, true, "7", "", "", "", "")
            };

            DataGrid1.ItemsSource = layers;
        }

        public class Layer
        {
            public string Layername { get; set; }
            public bool Display { get; set; }
            public bool Freeze { get; set; }
            public bool Lock { get; set; }
            public bool Printing { get; set; }
            public string Color { get; set; }
            public string Linetype { get; set; }
            public string Lineweight { get; set; }
            public string Permeability { get; set; }
            public string Description { get; set; }

            public Layer(string layername, bool display, bool freeze, bool @lock, bool printing, string color, string linetype, string lineweight, string permeability, string description)
            {
                Layername = layername;
                Display = display;
                Freeze = freeze;
                Lock = @lock;
                Printing = printing;
                Color = color;
                Linetype = linetype;
                Lineweight = lineweight;
                Permeability = permeability;
                Description = description;
            }
        }

        private async void ShowDialog(String title, String content)
        {
            ContentDialog dialog = new ContentDialog
            {
                // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
                XamlRoot = this.XamlRoot,
                Title = title,
                Content = content,
                CloseButtonText = "OK"
            };
            ContentDialogResult dialogresult = await dialog.ShowAsync();
            return;
        }

        private bool CheckError(int rowcount)
        {
            bool isError = false;
            if (rowcount == 0)
            {
                ShowDialog("Please enter layer information", "Error: Please enter layer information.");
                isError = true;
            }
            else
            {
                for (int i = 0; i < rowcount; i++)
                {
                    Layer rowData = layers[i];
                    rowData ??= new Layer(string.Format("{0:D2}_", CurrentLineNumber + 1), true, false, false, true, "7", "", "", "", "");
                    if (rowData.Layername == "")
                    {
                        ShowDialog("Please enter layer information", "Error: Please enter the name of the layer in line " + Convert.ToString(i + 1) + ".");
                        isError = true;
                    }
                }
            }
            return isError;
        }

        public string CreateSCR()
        {
            int rowcount = layers.Count;
            bool isError = CheckError(rowcount);
            if (!isError)
            {
                string content;
                content = "-LAYER\r\n";
                for (int i = 0; i < rowcount; i++)
                {
                    Layer rowData = layers[i];
                    rowData ??= new Layer("", true, false, false, true, "7", "", "", "", "");
                    content += "n\r\n";
                    content = content + rowData.Layername + "\r\n";
                    if (rowData.Freeze)
                    {
                        content += "f\r\n";
                        content = content + rowData.Layername + "\r\n";
                    }
                    else
                    {
                        content += "t\r\n";
                        content = content + rowData.Layername + "\r\n";
                    }
                    if (rowData.Lock)
                    {
                        content += "lo\r\n";
                        content = content + rowData.Layername + "\r\n";
                    }
                    else
                    {
                        content += "u\r\n";
                        content = content + rowData.Layername + "\r\n";
                    }
                    if (rowData.Printing)
                    {
                        content += "p\r\n";
                        content += "p\r\n";
                        content = content + rowData.Layername + "\r\n";
                    }
                    else
                    {
                        content += "p\r\n";
                        content += "n\r\n";
                        content = content + rowData.Layername + "\r\n";
                    }
                    if (rowData.Color != "")
                    {
                        content += "c\r\n";
                        content = content + rowData.Color + "\r\n";
                        content = content + rowData.Layername + "\r\n";
                    }
                    if (rowData.Linetype != "")
                    {
                        content += "l\r\n";
                        content = content + rowData.Linetype + "\r\n";
                        content = content + rowData.Layername + "\r\n";
                    }
                    if (rowData.Lineweight != "")
                    {
                        content += "lw\r\n";
                        content = content + rowData.Lineweight + "\r\n";
                        content = content + rowData.Layername + "\r\n";
                    }
                    if (rowData.Permeability != "")
                    {
                        content += "tr\r\n";
                        content = content + rowData.Permeability + "\r\n";
                        content = content + rowData.Layername + "\r\n";
                    }
                    if (rowData.Description != "")
                    {
                        content += "d\r\n";
                        content = content + rowData.Description + "\r\n";
                        content = content + rowData.Layername + "\r\n";
                    }
                    if (rowData.Display)
                    {
                        content += "on\r\n";
                        content = content + rowData.Layername + "\r\n";
                    }
                    else
                    {
                        content += "off\r\n";
                        content = content + rowData.Layername + "\r\n";
                    }
                }
                content += "\r\n";
                content += "qsave\r\n";

                return content;
            }
            else
            {
                return "";
            }
        }

        private async void SaveFile(string content)
        {
            var window = new Microsoft.UI.Xaml.Window();
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("AutoCAD Script File", new List<string>() { ".scr" });
            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = "New File";

            WinRT.Interop.InitializeWithWindow.Initialize(savePicker, hwnd);
            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                // write to file
                await FileIO.WriteTextAsync(file, content);
                ShowDialog("File was saved", "File " + file.Name + " was saved.");
            }
            else
            {
                ShowDialog("File couldn't be saved", "Operation cancelled.");
            }
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddRowButton_Click(object sender, RoutedEventArgs e)
        {
            layers.Add(new Layer(string.Format("{0:D2}_", CurrentLineNumber + 1), true, false, false, true, "7", "", "", "", ""));
            DataGrid1.ItemsSource = layers;
            CurrentLineNumber++;
        }

        private void DeleteRowButton_Click(object sender, RoutedEventArgs e)
        {
            int rowNumber = DataGrid1.SelectedIndex;

            if (rowNumber < 0)
            {
                ShowDialog("Plese select the row", "Error: Please select the row you want to delete.");

                return;
            }

            layers.RemoveAt(rowNumber);
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            string content = CreateSCR();
            if (content != "")
            {
                SaveFile(content);
            }
        }
    }
}
