using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Auto_Layers_SCR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void DataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells[1].Value = "(ignore)";
            e.Row.Cells[2].Value = "(ignore)";
            e.Row.Cells[3].Value = "(ignore)";
            e.Row.Cells[4].Value = "(ignore)";
        }

        private int CheckError(int rowcount)
        {
            int error = 0;
            if (rowcount == 1)
            {
                MessageBox.Show("Error:Please enter layer information.", "Auto Layers SCR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                error = 1;
            }
            else
            {
                for (int i = 0; i < rowcount - 1; i++)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value == null)
                    {
                        MessageBox.Show("Error:Please enter the name of the layer in line " + Convert.ToString(i + 1) + ".", "Auto Layers SCR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        error = 1;
                    }
                }
            }
            return error;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int rowcount = dataGridView1.RowCount, cellcount = dataGridView1.ColumnCount;
            int error = CheckError(rowcount);
            if (error == 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();

                sfd.FileName = "filename.scr";
                sfd.InitialDirectory = @"C:\";
                sfd.Filter = "SCR(*.scr)|*.scr|All files(*.*)|*.*";
                sfd.FilterIndex = 1;
                sfd.Title = "Select a file to save the file to.";
                sfd.RestoreDirectory = true;
                sfd.OverwritePrompt = true;
                sfd.CheckPathExists = true;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(sfd.FileName, false, System.Text.Encoding.UTF8);
                    sw.WriteLine("-LAYER");
                    for (int i = 0; i < rowcount - 1; i++)
                    {
                        string cell0value = dataGridView1.Rows[i].Cells[0].Value.ToString();
                        for (int j = 0; j < cellcount; j++)
                        {
                            if (j == 0)
                            {
                                sw.WriteLine("n");
                                sw.WriteLine(dataGridView1.Rows[i].Cells[j].Value);
                            }
                            else if (j == 1)
                            {
                                if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "(ignore)")
                                {

                                }
                                else if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "True")
                                {
                                    sw.WriteLine("on");
                                    sw.WriteLine(cell0value);
                                }
                                else if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "False")
                                {
                                    sw.WriteLine("off");
                                    sw.WriteLine(cell0value);
                                }
                                else
                                {
                                    MessageBox.Show("An error occurred in the drop-down list in row " + Convert.ToString(i + 1) + ", column " + Convert.ToString(j + 1) + ". Please contact your application developer. The file will be generated with this drop-down list ignored.", "Auto Layers SCR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else if (j == 2)
                            {
                                if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "(ignore)")
                                {

                                }
                                else if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "True")
                                {
                                    sw.WriteLine("f");
                                    sw.WriteLine(cell0value);
                                }
                                else if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "False")
                                {
                                    sw.WriteLine("t");
                                    sw.WriteLine(cell0value);
                                }
                                else
                                {
                                    MessageBox.Show("An error occurred in the drop-down list in row " + Convert.ToString(i + 1) + ", column " + Convert.ToString(j + 1) + ". Please contact your application developer. The file will be generated with this drop-down list ignored.", "Auto Layers SCR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else if (j == 3)
                            {
                                if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "(ignore)")
                                {

                                }
                                else if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "True")
                                {
                                    sw.WriteLine("lo");
                                    sw.WriteLine(cell0value);
                                }
                                else if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "False")
                                {
                                    sw.WriteLine("u");
                                    sw.WriteLine(cell0value);
                                }
                                else
                                {
                                    MessageBox.Show("An error occurred in the drop-down list in row " + Convert.ToString(i + 1) + ", column " + Convert.ToString(j + 1) + ". Please contact your application developer. The file will be generated with this drop-down list ignored.", "Auto Layers SCR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else if (j == 4)
                            {
                                if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "(ignore)")
                                {

                                }
                                else if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "True")
                                {
                                    sw.WriteLine("p");
                                    sw.WriteLine("p");
                                    sw.WriteLine(cell0value);
                                }
                                else if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "False")
                                {
                                    sw.WriteLine("p");
                                    sw.WriteLine("n");
                                    sw.WriteLine(cell0value);
                                }
                                else
                                {
                                    MessageBox.Show("An error occurred in the drop-down list in row " + Convert.ToString(i + 1) + ", column " + Convert.ToString(j + 1) + ". Please contact your application developer. The file will be generated with this drop-down list ignored.", "Auto Layers SCR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else if (j == 5)
                            {
                                if (dataGridView1.Rows[i].Cells[j].Value != null)
                                {
                                    sw.WriteLine("c");
                                    sw.WriteLine(dataGridView1.Rows[i].Cells[j].Value.ToString());
                                    sw.WriteLine(cell0value);
                                }
                            }
                            else if (j == 6)
                            {
                                if (dataGridView1.Rows[i].Cells[j].Value != null)
                                {
                                    sw.WriteLine("l");
                                    sw.WriteLine(dataGridView1.Rows[i].Cells[j].Value.ToString());
                                    sw.WriteLine(cell0value);
                                }
                            }
                            else if (j == 7)
                            {
                                if (dataGridView1.Rows[i].Cells[j].Value != null)
                                {
                                    sw.WriteLine("lw");
                                    sw.WriteLine(dataGridView1.Rows[i].Cells[j].Value.ToString());
                                    sw.WriteLine(cell0value);
                                }
                            }
                            else if (j == 8)
                            {
                                if (dataGridView1.Rows[i].Cells[j].Value != null)
                                {
                                    sw.WriteLine("tr");
                                    sw.WriteLine(dataGridView1.Rows[i].Cells[j].Value.ToString());
                                    sw.WriteLine(cell0value);
                                }
                            }
                            else if (j == 9)
                            {
                                if (dataGridView1.Rows[i].Cells[j].Value != null)
                                {
                                    sw.WriteLine("d");
                                    sw.WriteLine(dataGridView1.Rows[i].Cells[j].Value.ToString());
                                    sw.WriteLine(cell0value);
                                }
                            }
                        }
                    }
                    sw.WriteLine("");
                    sw.WriteLine("qsave");
                    sw.Close();
                    MessageBox.Show("Generation completed.", "Auto Layers SCR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.ShowDialog(this);
        }

        private void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowcount = dataGridView1.RowCount, cellcount = dataGridView1.ColumnCount;
            int error = CheckError(rowcount);
            if (error == 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();

                sfd.FileName = "filename.scr";
                sfd.InitialDirectory = @"C:\";
                sfd.Filter = "SCR(*.scr)|*.scr|All files(*.*)|*.*";
                sfd.FilterIndex = 1;
                sfd.Title = "Select a file to save the file to.";
                sfd.RestoreDirectory = true;
                sfd.OverwritePrompt = true;
                sfd.CheckPathExists = true;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(sfd.FileName, false, System.Text.Encoding.UTF8);
                    sw.WriteLine("-LAYER");
                    for (int i = 0; i < rowcount - 1; i++)
                    {
                        string cell0value = dataGridView1.Rows[i].Cells[0].Value.ToString();
                        for (int j = 0; j < cellcount; j++)
                        {
                            if (j == 0)
                            {
                                sw.WriteLine("n");
                                sw.WriteLine(dataGridView1.Rows[i].Cells[j].Value);
                            }
                            else if (j == 1)
                            {
                                if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "(ignore)")
                                {

                                }
                                else if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "True")
                                {
                                    sw.WriteLine("on");
                                    sw.WriteLine(cell0value);
                                }
                                else if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "False")
                                {
                                    sw.WriteLine("off");
                                    sw.WriteLine(cell0value);
                                }
                                else
                                {
                                    MessageBox.Show("An error occurred in the drop-down list in row " + Convert.ToString(i + 1) + ", column " + Convert.ToString(j + 1) + ". Please contact your application developer. The file will be generated with this drop-down list ignored.", "Auto Layers SCR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else if (j == 2)
                            {
                                if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "(ignore)")
                                {

                                }
                                else if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "True")
                                {
                                    sw.WriteLine("f");
                                    sw.WriteLine(cell0value);
                                }
                                else if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "False")
                                {
                                    sw.WriteLine("t");
                                    sw.WriteLine(cell0value);
                                }
                                else
                                {
                                    MessageBox.Show("An error occurred in the drop-down list in row " + Convert.ToString(i + 1) + ", column " + Convert.ToString(j + 1) + ". Please contact your application developer. The file will be generated with this drop-down list ignored.", "Auto Layers SCR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else if (j == 3)
                            {
                                if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "(ignore)")
                                {

                                }
                                else if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "True")
                                {
                                    sw.WriteLine("lo");
                                    sw.WriteLine(cell0value);
                                }
                                else if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "False")
                                {
                                    sw.WriteLine("u");
                                    sw.WriteLine(cell0value);
                                }
                                else
                                {
                                    MessageBox.Show("An error occurred in the drop-down list in row " + Convert.ToString(i + 1) + ", column " + Convert.ToString(j + 1) + ". Please contact your application developer. The file will be generated with this drop-down list ignored.", "Auto Layers SCR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else if (j == 4)
                            {
                                if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "(ignore)")
                                {

                                }
                                else if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "True")
                                {
                                    sw.WriteLine("p");
                                    sw.WriteLine("p");
                                    sw.WriteLine(cell0value);
                                }
                                else if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "False")
                                {
                                    sw.WriteLine("p");
                                    sw.WriteLine("n");
                                    sw.WriteLine(cell0value);
                                }
                                else
                                {
                                    MessageBox.Show("An error occurred in the drop-down list in row " + Convert.ToString(i + 1) + ", column " + Convert.ToString(j + 1) + ". Please contact your application developer. The file will be generated with this drop-down list ignored.", "Auto Layers SCR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else if (j == 5)
                            {
                                if (dataGridView1.Rows[i].Cells[j].Value != null)
                                {
                                    sw.WriteLine("c");
                                    sw.WriteLine(dataGridView1.Rows[i].Cells[j].Value.ToString());
                                    sw.WriteLine(cell0value);
                                }
                            }
                            else if (j == 6)
                            {
                                if (dataGridView1.Rows[i].Cells[j].Value != null)
                                {
                                    sw.WriteLine("l");
                                    sw.WriteLine(dataGridView1.Rows[i].Cells[j].Value.ToString());
                                    sw.WriteLine(cell0value);
                                }
                            }
                            else if (j == 7)
                            {
                                if (dataGridView1.Rows[i].Cells[j].Value != null)
                                {
                                    sw.WriteLine("lw");
                                    sw.WriteLine(dataGridView1.Rows[i].Cells[j].Value.ToString());
                                    sw.WriteLine(cell0value);
                                }
                            }
                            else if (j == 8)
                            {
                                if (dataGridView1.Rows[i].Cells[j].Value != null)
                                {
                                    sw.WriteLine("tr");
                                    sw.WriteLine(dataGridView1.Rows[i].Cells[j].Value.ToString());
                                    sw.WriteLine(cell0value);
                                }
                            }
                            else if (j == 9)
                            {
                                if (dataGridView1.Rows[i].Cells[j].Value != null)
                                {
                                    sw.WriteLine("d");
                                    sw.WriteLine(dataGridView1.Rows[i].Cells[j].Value.ToString());
                                    sw.WriteLine(cell0value);
                                }
                            }
                        }
                    }
                    sw.WriteLine("");
                    sw.WriteLine("qsave");
                    sw.Close();
                    MessageBox.Show("Generation completed.", "Auto Layers SCR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void loadFromXMLFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int cellcount = dataGridView1.ColumnCount;
            DialogResult result = MessageBox.Show("The information of the currently displayed layer will be overwritten.\r\nDo you want to continue?", "Auto Layers SCR", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                dataGridView1.Rows.Clear();
                OpenFileDialog ofd = new OpenFileDialog();

                ofd.FileName = "XMLfilename.xml";
                ofd.InitialDirectory = @"C:\";
                ofd.Filter = "XML(*.xml)|*.xml|All files(*.*)|*.*";
                ofd.FilterIndex = 1;
                ofd.Title = "Select a file to save the file to.";
                ofd.RestoreDirectory = true;
                ofd.CheckFileExists = true;
                ofd.CheckPathExists = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    System.Xml.Serialization.XmlSerializer serializer2 = new System.Xml.Serialization.XmlSerializer(typeof(DataGridView1DataClass[])); System.IO.StreamReader sr = new System.IO.StreamReader(ofd.FileName, System.Text.Encoding.UTF8);
                    DataGridView1DataClass[] loadAry;
                    loadAry = (DataGridView1DataClass[])serializer2.Deserialize(sr);
                    sr.Close();
                    for (int i = 0; i < loadAry.Length; i++)
                    {
                        string loadaryLayername = "", loadaryDisplay = "", loadaryFreeze = "", loadaryLock = "", loadaryPrinting = "", loadaryColor = "", loadaryLinetype = "", loadaryLineweight = "", loadaryPermeability = "", loadaryDescription = "";
                        if (loadAry[i] == null)
                        {

                        }
                        else
                        {
                            if (loadAry[i].Layername != null)
                            {
                                loadaryLayername = loadAry[i].Layername;
                            }
                            if (loadAry[i].Display != null)
                            {
                                loadaryDisplay = loadAry[i].Display;
                            }
                            if (loadAry[i].Freeze != null)
                            {
                                loadaryFreeze = loadAry[i].Freeze;
                            }
                            if (loadAry[i].Lock != null)
                            {
                                loadaryLock = loadAry[i].Lock;
                            }
                            if (loadAry[i].Printing != null)
                            {
                                loadaryPrinting = loadAry[i].Printing;
                            }
                            if (loadAry[i].Color != null)
                            {
                                loadaryColor = loadAry[i].Color;
                            }
                            if (loadAry[i].Linetype != null)
                            {
                                loadaryLinetype = loadAry[i].Linetype;
                            }
                            if (loadAry[i].Lineweight != null)
                            {
                                loadaryLineweight = loadAry[i].Lineweight;
                            }
                            if (loadAry[i].Permeability != null)
                            {
                                loadaryPermeability = loadAry[i].Permeability;
                            }
                            if (loadAry[i].Description != null)
                            {
                                loadaryDescription = loadAry[i].Description;
                            }
                            dataGridView1.Rows.Add(new object[] { loadaryLayername, loadaryDisplay, loadaryFreeze, loadaryLock, loadaryPrinting, loadaryColor, loadaryLinetype, loadaryLineweight, loadaryPermeability, loadaryDescription });
                        }
                    }
                    MessageBox.Show("XML file loading completed.", "Auto Layers SCR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void saveAsXMLFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rowcount = dataGridView1.RowCount, cellcount = dataGridView1.ColumnCount;
            int error = CheckError(rowcount);
            if (error == 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();

                sfd.FileName = "XMLfilename.xml";
                sfd.InitialDirectory = @"C:\";
                sfd.Filter = "XML(*.xml)|*.xml|All files(*.*)|*.*";
                sfd.FilterIndex = 1;
                sfd.Title = "Select a file to save the file to.";
                sfd.RestoreDirectory = true;
                sfd.OverwritePrompt = true;
                sfd.CheckPathExists = true;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    DataGridView1DataClass[] ary = new DataGridView1DataClass[10];
                    for (int i = 0; i < rowcount - 1; i++)
                    {
                        ary[i] = new DataGridView1DataClass();
                        for (int j = 0; j < cellcount; j++)
                        {
                            if (j == 0)
                            {
                                ary[i].Layername = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            }
                            else if (j == 1)
                            {
                                ary[i].Display = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            }
                            else if (j == 2)
                            {
                                ary[i].Freeze = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            }
                            else if (j == 3)
                            {
                                ary[i].Lock = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            }
                            else if (j == 4)
                            {
                                ary[i].Printing = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            }
                            else if (j == 5)
                            {
                                if (dataGridView1.Rows[i].Cells[j].Value == null)
                                {
                                    ary[i].Color = "";
                                }
                                else
                                {
                                    ary[i].Color = dataGridView1.Rows[i].Cells[j].Value.ToString();
                                }
                            }
                            else if (j == 6)
                            {
                                if (dataGridView1.Rows[i].Cells[j].Value == null)
                                {
                                    ary[i].Linetype = "";
                                }
                                else
                                {
                                    ary[i].Linetype = dataGridView1.Rows[i].Cells[j].Value.ToString();
                                }
                            }
                            else if (j == 7)
                            {
                                if (dataGridView1.Rows[i].Cells[j].Value == null)
                                {
                                    ary[i].Lineweight = "";
                                }
                                else
                                {
                                    ary[i].Lineweight = dataGridView1.Rows[i].Cells[j].Value.ToString();
                                }
                            }
                            else if (j == 8)
                            {
                                if (dataGridView1.Rows[i].Cells[j].Value == null)
                                {
                                    ary[i].Permeability = "";
                                }
                                else
                                {
                                    ary[i].Permeability = dataGridView1.Rows[i].Cells[j].Value.ToString();
                                }
                            }
                            else if (j == 9)
                            {
                                if (dataGridView1.Rows[i].Cells[j].Value == null)
                                {
                                    ary[i].Description = "";
                                }
                                else
                                {
                                    ary[i].Description = dataGridView1.Rows[i].Cells[j].Value.ToString();
                                }
                            }
                        }
                    }
                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(DataGridView1DataClass[]));
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(sfd.FileName, false, System.Text.Encoding.UTF8);
                    serializer.Serialize(sw, ary);
                    sw.Close();
                    MessageBox.Show("XML file creation completed.", "Auto Layers SCR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }

    public class DataGridView1DataClass
    {
        public string Layername;
        public string Display;
        public string Freeze;
        public string Lock;
        public string Printing;
        public string Color;
        public string Linetype;
        public string Lineweight;
        public string Permeability;
        public string Description;
    }
    //Copyright (c) 2021 yossi
    //Released under the MIT license
    //https://opensource.org/licenses/mit-license.php
}
