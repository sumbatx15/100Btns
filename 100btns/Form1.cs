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
using System.Diagnostics;
using System.Drawing.Text;

namespace _100btns
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        List<Tuple<int, Button, string>> fbtns = new List<Tuple<int, Button, string>>();
        Form4 frm4;
        public bool thisedited;
        public Button selectedBtn;
        public int btnsAmount;
        public Control[] ctrls;
        public Control[] ctrls2;
        public Control[] tabsCollection;
        string file = @"C:\Users\" + Environment.UserName + @"\Desktop\GVC-btnsNAME.txt";
        int tabs;
        int nameNumber;
        StreamReader sr;
        string[] btnsNames;
        bool wrong;

        Color bc = Color.Coral;
        private void Form1_Load(object sender, EventArgs e)
        {
            frm4 = new Form4(selectedBtn);
            btnsAmount = 250;
            double tabAmount = (double)btnsAmount / 10;
            tabs = (int)Math.Ceiling(tabAmount);

            ctrls = new Control[btnsAmount];
            tabsCollection = new Control[tabs];
            for (int i = 0; i < tabs; i++)
            {
                string name = "Page" + (i + 1).ToString();
                TabPage tp = new TabPage();
                tp.AllowDrop = true;
                tp.Name = name;
                tp.TabIndex = i + 1;
                tp.Text = name;
                tp.UseVisualStyleBackColor = true;
                tabControl1.TabPages.Add(tp);
                tabsCollection[i] = tp;
            }
            foreach (TabPage t in tabControl1.TabPages)
            {
                int x = 10;
                int y = 10;
                for (int i = 0; i < 10; i++)
                {
                    nameNumber++;
                    string name = "button" + nameNumber.ToString();

                    Button btn = new Button();
                    btn.Click += Btn_Click;
                    btn.Location = new Point(x, y);
                    btn.FlatStyle = FlatStyle.Popup;
                    btn.BackColor = Color.LightSeaGreen;
                    btn.Name = name;
                    btn.MinimumSize = new Size(100, 30);
                    btn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                    btn.AutoSize = true;
                    btn.TabIndex = i;
                    btn.Text = name;
                    btn.UseVisualStyleBackColor = true;
                    y = y + btn.MinimumSize.Height + 2;
                    if (nameNumber - 1 >= btnsAmount)
                    {
                        break;
                    }
                    t.Controls.Add(btn);
                    ctrls[nameNumber - 1] = btn;
                    fbtns.Add(new Tuple<int, Button, string>(1, btn, ""));
                }
            }

            if (File.Exists(file))
            {
                sr = new StreamReader(file);
                for (int i = 0; i < btnsAmount; i++)
                {
                    string text = sr.ReadLine();
                    if (text.Length > 50)
                    {
                        ctrls[i].Text = ctrls[i].Name;
                        wrong = true;
                    }
                    else
                    {
                        ctrls[i].Text = text;
                    }

                }
                sr.Close();
                sr = null;

                MessageBox.Show("Button Names Loaded Successfully!");
                if (wrong)
                {
                    MessageBox.Show("Some button names changed to original names.\r\nMax length is 50.\r\n\r\nIts OK nothing bad happened, If you want to edit the file, close this, and open again after editing the file, or you can leave it like that.");
                }
            }

        }

        private void Btn_Click(object sender, EventArgs e)
        {
            selectedBtn = (Button)sender;
            selectedBtn.BackColor = Color.DeepPink;

            foreach (Control tp in tabControl1.Controls)
            {
                foreach (Control btn in tp.Controls)
                {
                    if (btn is Button)
                    {
                        if (btn != selectedBtn)
                        {
                            btn.BackColor = Color.LightSeaGreen;
                        }
                    }
                }
            }
        }
        bool edited;
        private void editButtonsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3(btnsAmount, ctrls, tabControl1, selectedBtn);
            frm3.ShowDialog();
            edited = frm3.edited;
        }
        private void saveButtonsNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(file))
                {
                    File.WriteAllText(file, string.Empty);
                }
                else
                {
                    File.Create(file).Close();
                }

                btnsNames = new string[btnsAmount];

                for (int i = 0; i < btnsAmount; i++)
                {
                    btnsNames[i] = ctrls[i].Text;
                }
                File.WriteAllLines(file, btnsNames);

                MessageBox.Show("Successfully Saved!");
                thisedited = false;
                edited = false;
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void resetButtonsNamesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            thisedited = true;

            foreach (var btn in ctrls)
            {
                btn.Text = btn.Name;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (edited || thisedited)
            {
                DialogResult d = MessageBox.Show("Do you want to save changes?", "Confirmation", MessageBoxButtons.YesNoCancel);

                switch (d)
                {
                    case DialogResult.Yes:
                        saveButtonsNameToolStripMenuItem.PerformClick();
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }

        private void tabControl1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.E)
            {
                if (tabControl1.SelectedTab.Controls.Contains(selectedBtn))
                {
                    frm4 = new Form4(selectedBtn);
                    var bX = selectedBtn.Location.X;
                    var bY = selectedBtn.Location.Y;
                    var tX = tabControl1.Location.X;
                    var dX = selectedBtn.Width;
                    var tY = tabControl1.Location.Y;
                    Point p = tabControl1.PointToScreen(selectedBtn.Location);
                    frm4.Location = new Point(p.X + selectedBtn.Width + 15, p.Y + 15);
                    frm4.Show();
                }
            }
            if (e.KeyCode == Keys.R)
            {
                if (tabControl1.SelectedTab.Controls.Contains(selectedBtn))
                {
                    selectedBtn.Text = selectedBtn.Name;
                }
            }

        }
    }
}
