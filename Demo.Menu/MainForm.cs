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

namespace Demo.Menu
{
    public partial class MainForm : Form
    {
        string file = null;
        string filters = "All files (*.*)|*.*|Text files (*.txt)|*.txt|rtf files(*.rtf)|*.rtf";
        //Encoding enc = Encoding.GetEncoding(1251);
        object mask = null;
        Encoding enc = ASCIIEncoding.ASCII;
        public MainForm()
        {
            InitializeComponent();
            mask = RTextBox.Text;
        }
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            if (!Save_check())
                Save_me(sender, null);
            openFileDialog.DefaultExt = "txt|rtf";
            openFileDialog.Filter = filters;
            openFileDialog.FilterIndex = 2;
            openFileDialog.ReadOnlyChecked = true;
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                using (StreamReader reader = new StreamReader(openFileDialog.OpenFile(), enc))
                    FileOpen(reader);
                MessageBox.Show("Setup backcolor");
                toolStripButton6_Click(sender, null);
                MessageBox.Show("Setup text color");
                toolStripButton5_Click(sender, null);
                RTextBox.ReadOnly = true;
            }
            mask = RTextBox.Text;
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Save_check())
                Save_me(sender, null);
            openFileDialog.DefaultExt = "txt|rtf";
            openFileDialog.Filter = filters;
            openFileDialog.FilterIndex = 2;
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                using (StreamReader reader = new StreamReader(openFileDialog.OpenFile(), enc))
                    FileOpen(reader);
                file = openFileDialog.FileName;
                mask = RTextBox.Text;
            }
        }
        private void FileOpen(StreamReader reader)
        {
            string t = openFileDialog.FileName;
            if (t[t.Count() - 1] == 'f')
                RTextBox.Rtf = reader.ReadToEnd();
            else
                RTextBox.Text = reader.ReadToEnd();
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Save_check())
                Save_me(sender, null);
            RTextBox.Text = null;
            mask = RTextBox.Text ;
            RTextBox.Rtf = null;
            file = null;
        }
        private bool Save_check()
        {
            if (RTextBox.Text.Equals(mask)) return true;
            return false;
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save_it(sender, e);
        }
        private void Save_it(object sender, EventArgs e)
        {
            if (file != null)
                using (StreamWriter writer = new StreamWriter(file))
                    FileWriter(file, writer);
            else
                saveAsToolStripMenuItem_Click(sender, e);
        }
        private void FileWriter(string path, StreamWriter writer)
        {
            string t = path;
            if (t[t.Count() - 1] == 'f')
                writer.Write(RTextBox.Rtf);
            else
                writer.Write(RTextBox.Text);
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = filters;
            saveFileDialog.FilterIndex = 2;
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
                using (StreamWriter writer = new StreamWriter(saveFileDialog.OpenFile()))
                    FileWriter(saveFileDialog.FileName, writer);
            mask = RTextBox.Text;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainForm_FormClosing(sender, (FormClosingEventArgs)e);
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog.Font = RTextBox.Font;
            if (fontDialog.ShowDialog(this) == DialogResult.OK)
            {
                if (RTextBox.SelectedText != null)
                    RTextBox.SelectionFont = fontDialog.Font;
                else
                    RTextBox.Font = fontDialog.Font;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Save_check())
            {
                Save_me(sender, null);
                mask = RTextBox.Text;
                Close(); return;
            }
        }
        private bool Save_me(object sender, EventArgs e)
        {
            RTextBox.ReadOnly = false;
            if (!Save_check())
                if (MessageBox.Show("Save file?", "Confirm",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Save_it(sender, null);
                    return true;
                }
            return false;
        }
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            ColorChose CC = new ColorChose();
            if (CC.ShowDialog(this) == DialogResult.OK)
            {
                if (RTextBox.SelectedText != null)
                    RTextBox.SelectionColor = CC.temp;
                else
                    RTextBox.ForeColor = CC.temp;
            }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            ColorChose CC = new ColorChose();
            if (CC.ShowDialog(this) == DialogResult.OK)
                RTextBox.BackColor = CC.temp;
        }
    }
}
