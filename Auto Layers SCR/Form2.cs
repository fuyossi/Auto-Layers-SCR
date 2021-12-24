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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            if (IntPtr.Size == 4)
            {
                label1.Text = label1.Text + " (x86)";
            }
            else if (IntPtr.Size == 8)
            {
                label1.Text = label1.Text + " (x64)";
            }
            else
            {
                MessageBox.Show("Error: Not all of the if branches are applicable. There may be a problem with branching depending on whether the application is 32 or 64 bit.", "Auto Layers SCR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start("https://opensource.org/licenses/mit-license.php");
        }
        //Copyright (c) 2021 yossi
        //Released under the MIT license
        //https://opensource.org/licenses/mit-license.php
    }
}
