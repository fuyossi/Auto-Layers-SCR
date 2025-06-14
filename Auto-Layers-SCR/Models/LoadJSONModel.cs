using Auto_Layers_SCR.ViewModels;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Auto_Layers_SCR.Models
{
    /// <summary>
    /// Load JSON.
    /// </summary>
    public class LoadJSONModel
    {
        public static ObservableCollection<Layer> LoadJSON(string json)
        {
            var Layers = JsonSerializer.Deserialize<ObservableCollection<Layer>>(json) ?? [];
            return Layers;
        }
    }
}
