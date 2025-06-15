using Auto_Layers_SCR.ViewModels;
using System;
using System.Collections.ObjectModel;

namespace Auto_Layers_SCR.Utils
{
    /// <summary>
    /// Check any errors in layer table.
    /// </summary>
    public class CheckLayerTableError
    {
        protected static (bool /* isError */, string /* briefErrorMessage */, string /* fullErrorMessage */) CheckError(ObservableCollection<Layer> layers)
        {
            bool isError = false;
            string briefErrorMessage = "";
            string fullErrorMessage = "";

            int rowcount = layers.Count;
            if (rowcount == 0)
            {
                isError = true;
                briefErrorMessage = "Please enter layer information.";
                fullErrorMessage = "Error: Please enter layer information";
            }
            else
            {
                for (int i = 0; i < rowcount; i++)
                {
                    Layer rowData = layers[i];
                    if (rowData.Layername == "")
                    {
                        isError = true;
                        briefErrorMessage = "Please enter layer information.";
                        fullErrorMessage = "Error: Please enter the name of the layer in line " + Convert.ToString(i + 1) + ".";
                    }
                }
            }
            return (isError, briefErrorMessage, fullErrorMessage);
        }
    }
}
