using Auto_Layers_SCR.Models;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Auto_Layers_SCR.ViewModels
{
    /// <summary>
    /// ViewModel of HomePage.
    /// </summary>
    public class Layer(string layername, bool display, bool freeze, bool @lock, bool printing, string color, string linetype, string lineweight, string permeability, string description)
    {
        public string Layername { get; set; } = layername;
        public bool Display { get; set; } = display;
        public bool Freeze { get; set; } = freeze;
        public bool Lock { get; set; } = @lock;
        public bool Printing { get; set; } = printing;
        public string Color { get; set; } = color;
        public string Linetype { get; set; } = linetype;
        public string Lineweight { get; set; } = lineweight;
        public string Permeability { get; set; } = permeability;
        public string Description { get; set; } = description;
    }

    internal partial class HomePageViewModel
    {
        int currentLineNumber = 1;

        public static ObservableCollection<Layer> Layers { get; set; } = [];

        public HomePageViewModel()
        {
            Layers = [new Layer("01_", true, false, false, true, "7", "", "", "", "")];
        }

        [RelayCommand]
        private void AddRowClick()
        {
            Layers.Add(new Layer(string.Format("{0:D2}_", currentLineNumber + 1), true, false, false, true, "7", "", "", "", ""));
            currentLineNumber++;
        }

        public static void LoadJSON(string json)
        {
            var newLayers = LoadJSONModel.LoadJSON(json);
            Layers.Clear();
            for (int i = 0; i < newLayers.Count; i++)
            {
                Layers.Add((Layer)newLayers[i]);
            }

        }

        public static void DeleteRow(int rowNumber)
        {
            Layers.RemoveAt(rowNumber);
        }

        public static (bool, string, string, string) CreateSCR()
        {
            var result = CreateSCRModel.CreateSCR(Layers);
            return result;
        }

        public static (bool, string, string, string) CreateJSON()
        {
            var result = CreateJSONModel.CreateJSON(Layers);
            return result;
        }
    }
}
