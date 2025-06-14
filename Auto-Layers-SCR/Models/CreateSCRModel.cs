using Auto_Layers_SCR.Utils;
using Auto_Layers_SCR.ViewModels;
using System.Collections.ObjectModel;

namespace Auto_Layers_SCR.Models
{
    /// <summary>
    /// Create content of SCR file from layer table.
    /// </summary>
    internal class CreateSCRModel : CheckLayerTableError
    {
        public static (bool /* isError */, string /* content */, string  /* briefErrorMessage */, string /* fullErrorMessage */) CreateSCR(ObservableCollection<Layer> Layers)
        {
            var error = CheckError(Layers);
            if (!error.Item1)
            {
                string content;
                int rowcount = Layers.Count;
                content = "-LAYER\r\n";
                for (int i = 0; i < rowcount; i++)
                {
                    Layer rowData = Layers[i];
                    content += "n\r\n";
                    content = content + "\"" + rowData.Layername + "\"\r\n";
                    if (rowData.Freeze)
                    {
                        content += "f\r\n";
                        content = content + "\"" + rowData.Layername + "\"\r\n";
                    }
                    else
                    {
                        content += "t\r\n";
                        content = content + "\"" + rowData.Layername + "\"\r\n";
                    }
                    if (rowData.Lock)
                    {
                        content += "lo\r\n";
                        content = content + "\"" + rowData.Layername + "\"\r\n";
                    }
                    else
                    {
                        content += "u\r\n";
                        content = content + "\"" + rowData.Layername + "\"\r\n";
                    }
                    if (rowData.Printing)
                    {
                        content += "p\r\n";
                        content += "p\r\n";
                        content = content + "\"" + rowData.Layername + "\"\r\n";
                    }
                    else
                    {
                        content += "p\r\n";
                        content += "n\r\n";
                        content = content + "\"" + rowData.Layername + "\"\r\n";
                    }
                    if (rowData.Color != "")
                    {
                        content += "c\r\n";
                        content = content + rowData.Color + "\r\n";
                        content = content + "\"" + rowData.Layername + "\"\r\n";
                    }
                    if (rowData.Linetype != "")
                    {
                        content += "l\r\n";
                        content = content + rowData.Linetype + "\r\n";
                        content = content + "\"" + rowData.Layername + "\"\r\n";
                    }
                    if (rowData.Lineweight != "")
                    {
                        content += "lw\r\n";
                        content = content + rowData.Lineweight + "\r\n";
                        content = content + "\"" + rowData.Layername + "\"\r\n";
                    }
                    if (rowData.Permeability != "")
                    {
                        content += "tr\r\n";
                        content = content + rowData.Permeability + "\r\n";
                        content = content + "\"" + rowData.Layername + "\"\r\n";
                    }
                    if (rowData.Description != "")
                    {
                        content += "d\r\n";
                        content = content + rowData.Description + "\r\n";
                        content = content + "\"" + rowData.Layername + "\"\r\n";
                    }
                    if (rowData.Display)
                    {
                        content += "on\r\n";
                        content = content + "\"" + rowData.Layername + "\"\r\n";
                    }
                    else
                    {
                        content += "off\r\n";
                        content = content + "\"" + rowData.Layername + "\"\r\n";
                    }
                }
                content += "\r\n";
                content += "qsave\r\n";

                return (false, content, "", "");
            }
            else
            {
                return (true, "", error.Item2, error.Item3);
            }
        }
    }
}
