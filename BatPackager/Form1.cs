using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatPackager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void darkButton2_Click(object sender, EventArgs e)
        {

        }

        private void darkButton3_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Batch Files(*.bat; *.cmd)|*.bat; *.cmd";
            if (open.ShowDialog() == DialogResult.OK)
            {
                darkTextBox1.Text = open.FileName;
            }
            }

        private void darkButton1_Click(object sender, EventArgs e)
        {


           
        }

        private void darkCheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SaveFileDialog exec = new SaveFileDialog();
            exec.Filter = "Executable Files(*.exe)|*.exe";
            if (exec.ShowDialog() == DialogResult.OK)
            {

                if (File.Exists(darkTextBox1.Text) == false)
                {
                    MessageBox.Show("The input batch file you specified could not be accessed!", "Invalid input file", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                try
                {
                    Path.GetDirectoryName(darkTextBox1.Text);
                    File.WriteAllText(darkTextBox1.Text, darkTextBox2.Text);
                }
                catch
                {
                    
                }


                exec.FileName = Path.ChangeExtension(exec.FileName, "exe");

                if (Packager.Generate(darkTextBox1.Text, exec.FileName, darkTextBox3.Text) == true)
                {
                    MessageBox.Show("Packaged!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("The Executable file failed to generated!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void darkButton1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void darkTextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                darkTextBox2.Text = File.ReadAllText(@darkTextBox1.Text);
            }
            catch (Exception ex)
            {
                
            }
           
            
        }

        private void darkButton1_Click_2(object sender, EventArgs e)
        {
            try
            {
                Path.GetDirectoryName(darkTextBox1.Text);
                File.WriteAllText(darkTextBox1.Text, darkTextBox2.Text);
            }
            catch
            {

            }
            
        }

        private void darkButton1_Click_3(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Icon Files(*.ico)|*.ico";
            if (open.ShowDialog() == DialogResult.OK)
            {
                darkTextBox3.Text = open.FileName;
            }
        }
    }
}
