using Auto_Layers_SCR.Properties;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace Auto_Layers_SCR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Layer> layers;
        int CurrentLineNumber = 1;

        public MainWindow()
        {
            InitializeComponent();

            layers = new ObservableCollection<Layer>
            {
                new Layer("01_", true, false, false, true, "7", "", "", "", "")
            };

            DataGrid1.ItemsSource = layers;

            RecoverWindowBounds();
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

        private int CheckError(int rowcount)
        {
            int error = 0;
            if (rowcount == 0)
            {
                MessageBox.Show("Error:Please enter layer information.", "Auto Layers SCR", MessageBoxButton.OK, MessageBoxImage.Error);
                error = 1;
            }
            else
            {
                for (int i = 0; i < rowcount; i++)
                {
                    var rowvalue = DataGrid1.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    if (rowvalue == null)
                    {
                        DataGrid1.UpdateLayout();
                        DataGrid1.ScrollIntoView(DataGrid1.Items[i]);
                        rowvalue = DataGrid1.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    }
                    rowvalue ??= new DataGridRow();
                    Layer layer = rowvalue.Item as Layer ?? new Layer("", true, false, false, true, "7", "", "", "", "");
                    if (layer.Layername == "")
                    {
                        MessageBox.Show("Error:Please enter the name of the layer in line " + Convert.ToString(i + 1) + ".", "Auto Layers SCR", MessageBoxButton.OK, MessageBoxImage.Error);
                        error = 1;
                    }
                }
            }
            return error;
        }

        public string CreateSCR()
        {
            int rowcount = DataGrid1.Items.Count;
            int error = CheckError(rowcount);
            if (error == 0)
            {
                string content;
                content = "-LAYER\r\n";
                for (int i = 0; i < rowcount; i++)
                {
                    var rowvalue = DataGrid1.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    if (rowvalue == null)
                    {
                        DataGrid1.UpdateLayout();
                        DataGrid1.ScrollIntoView(DataGrid1.Items[i]);
                        rowvalue = DataGrid1.ItemContainerGenerator.ContainerFromIndex(i) as DataGridRow;
                    }
                    rowvalue ??= new DataGridRow();
                    Layer layer = rowvalue.Item as Layer ?? new Layer("", true, false, false, true, "7", "", "", "", "");
                    content += "n\r\n";
                    content = content + layer.Layername + "\r\n";
                    if (layer.Freeze)
                    {
                        content += "f\r\n";
                        content = content + layer.Layername + "\r\n";
                    }
                    else
                    {
                        content += "t\r\n";
                        content = content + layer.Layername + "\r\n";
                    }
                    if (layer.Lock)
                    {
                        content += "lo\r\n";
                        content = content + layer.Layername + "\r\n";
                    }
                    else
                    {
                        content += "u\r\n";
                        content = content + layer.Layername + "\r\n";
                    }
                    if (layer.Printing)
                    {
                        content += "p\r\n";
                        content += "p\r\n";
                        content = content + layer.Layername + "\r\n";
                    }
                    else
                    {
                        content += "p\r\n";
                        content += "n\r\n";
                        content = content + layer.Layername + "\r\n";
                    }
                    content += "c\r\n";
                    content = content + layer.Color + "\r\n";
                    content = content + layer.Layername + "\r\n";
                    content += "l\r\n";
                    content = content + layer.Linetype + "\r\n";
                    content = content + layer.Layername + "\r\n";
                    content += "lw\r\n";
                    content = content + layer.Lineweight + "\r\n";
                    content = content + layer.Layername + "\r\n";
                    content += "tr\r\n";
                    content = content + layer.Permeability + "\r\n";
                    content = content + layer.Layername + "\r\n";
                    content += "d\r\n";
                    content = content + layer.Description + "\r\n";
                    content = content + layer.Layername + "\r\n";
                    if (layer.Display)
                    {
                        content += "on\r\n";
                        content = content + layer.Layername + "\r\n";
                    }
                    else
                    {
                        content += "off\r\n";
                        content = content + layer.Layername + "\r\n";
                    }
                }
                content += "\r\n";
                content += "qsave\r\n";

                return content;
            }
            else
            {
                return "error";
            }
        }

        public static ObservableCollection<Layer> LoadJSON(string json)
        {
            var layers = JsonSerializer.Deserialize<ObservableCollection<Layer>>(json) ?? new ObservableCollection<Layer>();
            return layers;
        }

        public string CreateJSON()
        {
            int rowcount = DataGrid1.Items.Count;
            int error = CheckError(rowcount);
            if (error == 0)
            {
                var json = JsonSerializer.Serialize(layers);
                return json;
            }
            else
            {
                return "error";
            }
        }

        private static void SaveFile(string content, string filename, string extension, string message)
        {
            SaveFileDialog sfd = new()
            {
                FileName = filename,
                InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                Filter = extension.ToUpper() + "(*." + extension + ")|*." + extension + "|All files(*.*)|*.*",
                FilterIndex = 1,
                Title = "Select a file to save the file to.",
                RestoreDirectory = true,
                OverwritePrompt = true,
                CheckPathExists = true
            };

            if (sfd.ShowDialog() == true)
            {
                System.IO.StreamWriter sw = new(sfd.FileName, false, System.Text.Encoding.UTF8);
                sw.Write(content);
                sw.Close();
                MessageBox.Show(message, "Auto Layers SCR", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private static int GetCurrentRowNumber(DataGrid dg)
        {
            int row;
            try
            {
                row = dg.Items.IndexOf(dg.SelectedItem);
            }
            catch
            {
                row = -1;
            }

            return row;
        }

        void SaveWindowBounds()
        {
            var settings = Settings.Default;
            settings.WindowMaximized = WindowState == WindowState.Maximized;
            WindowState = WindowState.Normal;
            settings.WindowLeft = Left;
            settings.WindowTop = Top;
            settings.WindowWidth = Width;
            settings.WindowHeight = Height;
            settings.Save();
        }

        void RecoverWindowBounds()
        {
            var settings = Settings.Default;
            if (settings.WindowLeft >= 0 &&
                (settings.WindowLeft + settings.WindowWidth) < SystemParameters.VirtualScreenWidth)
            { Left = settings.WindowLeft; }
            if (settings.WindowTop >= 0 &&
                (settings.WindowTop + settings.WindowHeight) < SystemParameters.VirtualScreenHeight)
            { Top = settings.WindowTop; }
            if (settings.WindowWidth > 0 &&
                settings.WindowWidth <= SystemParameters.WorkArea.Width)
            { Width = settings.WindowWidth; }
            if (settings.WindowHeight > 0 &&
                settings.WindowHeight <= SystemParameters.WorkArea.Height)
            { Height = settings.WindowHeight; }
            if (settings.WindowMaximized)
            {
                Loaded += (o, e) => WindowState = WindowState.Maximized;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            SaveWindowBounds();
            base.OnClosing(e);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            layers.Add(new Layer(string.Format("{0:D2}_", CurrentLineNumber + 1), true, false, false, true, "7", "", "", "", ""));
            DataGrid1.ItemsSource = layers;
            CurrentLineNumber++;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string content = CreateSCR();
            SaveFile(content, "filename.scr", "scr", "Generation completed.");
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            string content = CreateSCR();
            SaveFile(content, "filename.scr", "scr", "Generation completed.");
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            string json;
            MessageBoxResult result = MessageBox.Show("The information of the currently displayed layer will be overwritten.\r\nDo you want to continue?", "Auto Layers SCR", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                DataGrid1.ItemsSource = null;
                OpenFileDialog ofd = new()
                {
                    FileName = "JSONfilename.json",
                    InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                    Filter = "JSON(*.json)|*.json|All files(*.*)|*.*",
                    FilterIndex = 1,
                    Title = "Select a file to open.",
                    RestoreDirectory = true,
                    CheckFileExists = true,
                    CheckPathExists = true
                };

                if (ofd.ShowDialog() == true)
                {
                    System.IO.StreamReader sr = new(ofd.FileName, System.Text.Encoding.UTF8);
                    json = sr.ReadToEnd();
                    sr.Close();
                    layers = LoadJSON(json);
                    DataGrid1.ItemsSource = layers;
                    MessageBox.Show("JSON file loading completed.", "Auto Layers SCR", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            string json = CreateJSON();
            SaveFile(json, "filename.json", "json", "JSON file creation completed.");
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new()
            {
                Owner = this
            };
            window1.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int rowNumber = GetCurrentRowNumber(DataGrid1);

            if (rowNumber < 0)
            {
                MessageBox.Show("Error:Please select the row you want to delete.", "Auto Layers SCR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete row " + (rowNumber + 1) + "?", "Auto Layers SCR", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                Layer layer = DataGrid1.Items[rowNumber] as Layer ?? new Layer("", true, false, false, true, "7", "", "", "", "");
                layers.Remove(layer);
            }
        }
    }
}
