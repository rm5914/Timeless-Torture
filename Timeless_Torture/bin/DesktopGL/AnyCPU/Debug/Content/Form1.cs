﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ExternalTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FileStream outStream = File.OpenWrite(@"c:\Downloads\timer.txt");
            StreamWriter output = new StreamWriter(outStream);

            output.WriteLine(numericUpDown1.Value);

            output.Close();
            Close();
        }
    }
}