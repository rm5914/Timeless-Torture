using System;
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
        int height;
        int width;
        public List<PictureBox> list = new List<PictureBox>();
        Color color;

        public Form1()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            height = 20;
            width = 20;
            textBox1.Enabled = false;

            for (int x = 0; x < (height * width); x++)
            {
                list.Add(new PictureBox());
            }

            int count = 0;

            for (int i = 0; i < height; i++)
            {
                list[count].Location = new System.Drawing.Point(176, (23 + 20 * i));

                for (int j = 0; j < width; j++)
                {
                    list[count].Size = new System.Drawing.Size(20, 20);
                    list[count].Location = new System.Drawing.Point((176 + 20 * j), (23 + 20 * i));

                    //Differenciating between load files with edited picture boxes and new files with default picture boxes
                    if (list[count].BackColor == DefaultBackColor)
                    {
                        list[count].BackColor = Color.Black;
                    }

                    list[count].Visible = true;
                    this.Controls.Add(this.list[count]);
                    list[count].Click += new EventHandler(PictureBox_Click);

                    count++;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            color = Color.Black;
            button9.BackColor = color;
            textBox1.Text = "Blocked area";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            color = Color.White;
            button9.BackColor = color;
            textBox1.Text = "Space";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            color = Color.Gray;
            button9.BackColor = color;
            textBox1.Text = "Dimly lit area";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            color = Color.SkyBlue;
            button9.BackColor = color;
            textBox1.Text = "Objective";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            color = Color.Red;
            button9.BackColor = color;
            textBox1.Text = "Fireplace";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            color = Color.DarkOliveGreen;
            button9.BackColor = color;
            textBox1.Text = "Bed/Spawn";
        }

        private void PictureBox_Click(Object sender, System.EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            p.BackColor = color;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = @"C:\";
            sfd.Filter = "Level Files| *.level";
            sfd.Title = "Save a level file";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FileStream outStream = File.OpenWrite(sfd.FileName);
                StreamWriter output = new StreamWriter(outStream);
                int count = 0;

                //Saving the height and width of the grid in the first two lines
                output.WriteLine(width);
                output.WriteLine(height);

                foreach (PictureBox var in list)
                {
                    output.WriteLine(ColorTranslator.ToHtml(list[count].BackColor));
                    count++;
                }

                System.Windows.Forms.MessageBox.Show("File has been succesfully saved", "Save",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Information);

                output.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Loading an already existing file
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = @"C:\";
            dialog.RestoreDirectory = true;
            dialog.Title = "Open a Level File";
            dialog.DefaultExt = "Level Files| *.level";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                FileStream inStream = null;
                StreamReader input = null;

                inStream = File.OpenRead(dialog.FileName);
                input = new StreamReader(inStream);

                Form1 editor = new Form1();

                height = 20;
                width = 20;

                for (int x = 0; x < (height * width); x++)
                {
                    editor.list.Add(new PictureBox());
                }

                int count = 0;

                for (int i = 0; i < height; i++)
                {
                    editor.list[count].Location = new System.Drawing.Point(176, (23 + 20 * i));

                    for (int j = 0; j < width; j++)
                    {
                        editor.list[count].Size = new System.Drawing.Size(20, 20);
                        editor.list[count].Location = new System.Drawing.Point((176 + 20 * j), (23 + 20 * i));
                        editor.list[count].BackColor = ColorTranslator.FromHtml(input.ReadLine());
                        editor.list[count].Visible = true;
                        this.Controls.Add(editor.list[count]);

                        count++;
                    }
                }

                editor.ShowDialog();

                System.Windows.Forms.MessageBox.Show("File has been succesfully Loaded", "Load",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Information);

                this.Close();
            }
        }
    }
}
