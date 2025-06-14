using Auto_Layers_SCR.Utils;
using Auto_Layers_SCR.ViewModels;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Auto_Layers_SCR.Models
{
    /// <summary>
    /// Create JSON content from layer table.
    /// </summary>
    internal class CreateJSONModel : CheckLayerTableError
    {
        public static (bool /* isError */, string /* content */, string  /* briefErrorMessage */, string /* fullErrorMessage */) CreateJSON(ObservableCollection<Layer> Layers)
        {
            var error = CheckError(Layers);
            if (!error.Item1)
            {
                string json = JsonSerializer.Serialize(Layers);
                return (false, json, "", "");
            }
            else
            {
                return (true, "", error.Item2, error.Item3);
            }
        }
    }
}
