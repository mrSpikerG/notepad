using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace notepad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.ContextMenuStrip = contextMenuStrip1;
            this.richTextBox1.ContextMenuStrip = contextMenuStrip1;
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void шрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FontDialog dialog = new FontDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.richTextBox1.Font = dialog.Font;
                }
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.richTextBox1.Text = File.ReadAllText($"{dialog.FileName}{dialog.Filter}");
                    //this.Text = dialog.FileName;

                }
            }
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "text|*.txt|docs|*.doc||";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (this.Name[0] == '*')
                    {
                        this.Name.Remove(0, 1);
                    }
                    File.WriteAllText(dialog.FileName, this.richTextBox1.Text);
                }
            }
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (PrintDialog dialog = new PrintDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                }
            }
        }

        private void новоеОкноToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "";

        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void увеличитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Font = new Font(this.richTextBox1.Font.FontFamily, this.richTextBox1.Font.Size + 1);
            this.toolStripStatusLabel3.Text = $"{Convert.ToInt32(toolStripStatusLabel3.Text.Replace("%", "")) + 10}%";
        }

        private void уменьшитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.richTextBox1.Font.Size > 4)
            {
                this.richTextBox1.Font = new Font(this.richTextBox1.Font.FontFamily, this.richTextBox1.Font.Size - 1);
                this.toolStripStatusLabel3.Text = $"{Convert.ToInt32(toolStripStatusLabel3.Text.Replace("%", "")) - 10}%";
            }
        }
        private void восстановитьМасштабПоУмолчаниюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Font = new Font(this.richTextBox1.Font.FontFamily, 9);
            this.toolStripStatusLabel3.Text = "100%";
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Copy();
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Paste();
        }

        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Copy();
            this.richTextBox1.SelectedText = "";
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.SelectedText = "";
        }

        private void найтиToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void выделитьВсеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.SelectAll();
        }

        private void времяИДатаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Text += $"{DateTime.Now.ToShortTimeString()} {DateTime.Now.ToShortDateString()}";
        }

        private void отменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Undo();
        }

        private void найтиРанееToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Select(this.richTextBox1.Text.IndexOf(this.richTextBox1.SelectedText), this.richTextBox1.SelectedText.Length);
        }

        private void найтиДалееToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.richTextBox1.Text.f
            // this.richTextBox1.Select(this.richTextBox1.Text.LastIndexOf(,this.richTextBox1.SelectedText), this.richTextBox1.SelectedText.Length);

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            this.Name = $"*{this.Name}";


        }
        private void selectionchanged(object sender, EventArgs e)
        {
            string[] lines = this.richTextBox1.Text.Split('\n');


            int inputIndexator = this.richTextBox1.SelectionStart;
            int row = 1;
            int column = 1;
            for (int i = 0; i < lines.Length; i++)
            {
                if (inputIndexator > lines[i].Length)
                {
                    inputIndexator -= lines[i].Length;
                    inputIndexator--;
                    
                }
                else
                {
                    row = i + 1;
                    column = inputIndexator+1;

                }
            }
            //row = i + 1;
            this.toolStripStatusLabel2.Text = $"Стр {row}, стлб {column}";
        }

        private void переносПоСловамToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.WordWrap = this.переносПоСловамToolStripMenuItem.Checked;
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            сохранитьКакToolStripMenuItem_Click(sender, e);
        }

        private void строкаСостоянияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.statusStrip1.Visible = this.строкаСостоянияToolStripMenuItem.Checked;
        }

        private void поискСПомощьюBingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Process.Start($"https://www.bing.com/search?q={this.richTextBox1.SelectedText}&form=NPCTXT");
        }

        private void просмотретьСправкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start($"https://www.bing.com/search?q=get+help+with+notepad+in+windows&filters=guid:4466414");
       
        }

        private void отправитьОтзывToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void правкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.richTextBox1.SelectedText.Length > 1)
            {
                this.вырезатьToolStripMenuItem.Enabled = true;
                this.копироватьToolStripMenuItem.Enabled = true;
                this.удалитьToolStripMenuItem.Enabled = true;
                this.поискСПомощьюBingToolStripMenuItem.Enabled = true;
                this.перейтиToolStripMenuItem.Enabled = true;
            }
            else
            {
                this.вырезатьToolStripMenuItem.Enabled = false;
                this.копироватьToolStripMenuItem.Enabled = false;
                this.удалитьToolStripMenuItem.Enabled = false;
                this.поискСПомощьюBingToolStripMenuItem.Enabled = false;
                this.перейтиToolStripMenuItem.Enabled = false;
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (this.richTextBox1.SelectedText.Length > 1)
            {
                this.вырезатьToolStripMenuItem1.Enabled = true;
                this.копироватьToolStripMenuItem1.Enabled = true;
                this.удалитьToolStripMenuItem1.Enabled = true;
                this.вставитьToolStripMenuItem1.Enabled = true;
            }
            else
            {
                this.вырезатьToolStripMenuItem1.Enabled = false;
                this.копироватьToolStripMenuItem1.Enabled = false;
                this.удалитьToolStripMenuItem1.Enabled = false;
                this.вставитьToolStripMenuItem1.Enabled = false;
            }
        }

        private void отменаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.отменитьToolStripMenuItem_Click(sender, e);
        }

        private void вырезатьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.вырезатьToolStripMenuItem_Click(sender, e);
        }

        private void копироватьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.копироватьToolStripMenuItem_Click(sender, e);
        }

        private void вставитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.вставитьToolStripMenuItem_Click(sender, e);
        }

        private void удалитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.удалитьToolStripMenuItem_Click(sender, e);
        }

        private void выделитьВсеToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.выделитьВсеToolStripMenuItem_Click(sender, e);
        }
    }
}
