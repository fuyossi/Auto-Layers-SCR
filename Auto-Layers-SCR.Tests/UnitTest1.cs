using Auto_Layers_SCR.Models;
using Auto_Layers_SCR.ViewModels;
using System.Collections.ObjectModel;

namespace Auto_Layers_SCR.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void TestCreateSCR()
        {
            ObservableCollection<Layer> Layers = [new Layer("01_", true, false, false, true, "7", "", "", "", "")];
            (bool, string, string, string) result = CreateSCRModel.CreateSCR(Layers);
            Assert.Equal("-LAYER\r\nn\r\n\"01_\"\r\nt\r\n\"01_\"\r\nu\r\n\"01_\"\r\np\r\np\r\n\"01_\"\r\nc\r\n7\r\n\"01_\"\r\non\r\n\"01_\"\r\n\r\nqsave\r\n", result.Item2);
        }

        [Fact]
        public void TestCreateJSON()
        {
            ObservableCollection<Layer> Layers = [new Layer("01_", true, false, false, true, "7", "", "", "", "")];
            (bool, string, string, string) result = CreateJSONModel.CreateJSON(Layers);
            Assert.Equal("[{\"Layername\":\"01_\",\"Display\":true,\"Freeze\":false,\"Lock\":false,\"Printing\":true,\"Color\":\"7\",\"Linetype\":\"\",\"Lineweight\":\"\",\"Permeability\":\"\",\"Description\":\"\"}]", result.Item2);
        }

        [Fact]
        public void TestLoadJSON()
        {
            var Layers = LoadJSONModel.LoadJSON("[{\"Layername\":\"01_\",\"Display\":true,\"Freeze\":false,\"Lock\":false,\"Printing\":true,\"Color\":\"7\",\"Linetype\":\"\",\"Lineweight\":\"\",\"Permeability\":\"\",\"Description\":\"\"}]");
            (bool, string, string, string) result = CreateSCRModel.CreateSCR(Layers);
            Assert.Equal("-LAYER\r\nn\r\n\"01_\"\r\nt\r\n\"01_\"\r\nu\r\n\"01_\"\r\np\r\np\r\n\"01_\"\r\nc\r\n7\r\n\"01_\"\r\non\r\n\"01_\"\r\n\r\nqsave\r\n", result.Item2);
        }
    }
}
