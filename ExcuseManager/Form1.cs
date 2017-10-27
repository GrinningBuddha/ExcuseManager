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

namespace ExcuseManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            currentExcuse = new Excuse();
            currentExcuse.LastUsed = lastUsed.Value;
        }

        private Excuse currentExcuse;
        private bool formChanged = false;
        public string folderToUse = "";

        private void results_TextChanged(object sender, EventArgs e)
        {
            currentExcuse.Results = results.Text;
            UpdateForm(true);
        }

        private void lastUsed_ValueChanged(object sender, EventArgs e)
        {
            currentExcuse.LastUsed = lastUsed.Value;
            UpdateForm(true);
        }

        private void UpdateForm(bool changed)
        {
            if (!changed)
            {
                this.description.Text = currentExcuse.Description;
                this.results.Text = currentExcuse.Results;
                this.lastUsed.Value = currentExcuse.LastUsed;
                if (!String.IsNullOrEmpty(currentExcuse.ExcusePath))
                    fileDate.Text = File.GetLastWriteTime(currentExcuse.ExcusePath).ToString();
                this.Text = "Excuse Manager";
            }
            else
                this.Text = "Excuse Manager*";
            this.formChanged = changed;
        }

        private void folder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = folderToUse;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                folderToUse = folderBrowserDialog1.SelectedPath;
                save.Enabled = true;
                open.Enabled = true;
                random.Enabled = true;
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(description.ToString()) || string.IsNullOrEmpty(results.ToString()))
            {
                MessageBox.Show("Please specify an excuse and a result.", "Unable to save", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            saveFileDialog1.InitialDirectory = folderToUse;
            saveFileDialog1.Filter = "Text Files (*.txt)|*.txt|" + "All Files (*.*)|*.*";
            saveFileDialog1.FileName = description.Text + ".txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) { 
                currentExcuse.Save(saveFileDialog1.FileName);
                UpdateForm(false);
                MessageBox.Show("Excuse written");
            }
            
        }

        private void open_Click(object sender, EventArgs e)
        {
            if (formChanged)
            {
                DialogResult result = MessageBox.Show("The current excuse has not been saved. Continue?", "Warning", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                    return;
            }
            openFileDialog1.InitialDirectory = folderToUse;
            openFileDialog1.Filter = "Text Files (*.txt)|*.txt|" + "All Files (*.*)|*.*";
            openFileDialog1.FileName = description.Text + ".txt";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                currentExcuse = new Excuse(openFileDialog1.FileName);
                UpdateForm(false);
            }
        }

        private void random_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            if (formChanged)
            {
                DialogResult result = MessageBox.Show("The current excuse has not been saved. Continue?", "Warning", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                    return;
            }
            currentExcuse = new Excuse(random, folderToUse);
            UpdateForm(false);
        }

        private void description_TextChanged(object sender, EventArgs e)
        {
            currentExcuse.Description = description.Text;
            UpdateForm(true);
        }
    }
}
