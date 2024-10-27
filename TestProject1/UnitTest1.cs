using Auto_Layers_SCR;

namespace TestProject1
{
    public class UnitTest1
    {
        [WpfFact]
        public void TestCreateSCR()
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            System.Threading.Thread.Sleep(1000);
            string result = mainWindow.CreateSCR();
            Assert.Equal("-LAYER\r\nn\r\n01_\r\nt\r\n01_\r\nu\r\n01_\r\np\r\np\r\n01_\r\nc\r\n7\r\n01_\r\non\r\n01_\r\n\r\nqsave\r\n", result);
        }

        [WpfFact]
        public void TestCreateJSON()
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            System.Threading.Thread.Sleep(1000);
            string result = mainWindow.CreateJSON();
            Assert.Equal("[{\"Layername\":\"01_\",\"Display\":true,\"Freeze\":false,\"Lock\":false,\"Printing\":true,\"Color\":\"7\",\"Linetype\":\"\",\"Lineweight\":\"\",\"Permeability\":\"\",\"Description\":\"\"}]", result);
        }

        [WpfFact]
        public void TestLoadJSON()
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            System.Threading.Thread.Sleep(1000);
            MainWindow.LoadJSON("[{\"Layername\":\"01_\",\"Display\":true,\"Freeze\":false,\"Lock\":false,\"Printing\":true,\"Color\":\"7\",\"Linetype\":\"\",\"Lineweight\":\"\",\"Permeability\":\"\",\"Description\":\"\"}]");
            string result = mainWindow.CreateSCR();
            System.Threading.Thread.Sleep(1000);
            Assert.Equal("-LAYER\r\nn\r\n01_\r\nt\r\n01_\r\nu\r\n01_\r\np\r\np\r\n01_\r\nc\r\n7\r\n01_\r\non\r\n01_\r\n\r\nqsave\r\n", result);
        }
    }
}