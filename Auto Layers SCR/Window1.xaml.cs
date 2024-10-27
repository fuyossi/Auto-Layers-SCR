using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace Auto_Layers_SCR
{
    /// <summary>
    /// Window1.xaml の相互作用ロジック
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            if (IntPtr.Size == 4)
            {
                TextBlock1.Text += " (x86)";
            }
            else
            {
                TextBlock1.Text += " (x64)";
            }
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Hyperlink1.Foreground = new SolidColorBrush(Color.FromRgb(167, 87, 168));
            System.Diagnostics.Process.Start(new ProcessStartInfo(e.Uri.ToString()) { UseShellExecute = true });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
