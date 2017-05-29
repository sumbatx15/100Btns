using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _100btns
{
    public partial class Form3 : Form
    {
        int btnsCount;
        Control[] ctrls;
        TabControl tabControl;
        Button selectedBtn;

        public Form3(int btnsCount, Control[] ctrls, TabControl tabControl, Button selectedBtn)
        {
            InitializeComponent();
            this.btnsCount = btnsCount;
            this.ctrls = ctrls;
            this.tabControl = tabControl;
            this.selectedBtn = selectedBtn;

        }
        int sIndex;
        private void Form3_Load(object sender, EventArgs e)
        {
            
            for (int i = 0; i < btnsCount; i++)
            {
                cbxBtns.Items.Add("Button" + (i + 1).ToString());
            }
            if(selectedBtn != null)
            {
                sIndex = Array.IndexOf(ctrls, selectedBtn);
                cbxBtns.SelectedIndex = sIndex;
            }
            else
            {
                cbxBtns.SelectedIndex = 0;
            }
            ActiveControl = textBox1;
            
        }
        public bool edited;
        private void btnSetName_Click(object sender, EventArgs e)
        {
            edited = true;
            if (textBox1.Text.Length == 0) 
            {
                ctrls[cbxBtns.SelectedIndex].Text = textBox1.Text;
                tabControl.SelectedTab = ctrls[cbxBtns.SelectedIndex].Parent as TabPage;
                lblSet.Text = "Buttons name is now empty.";
                (ctrls[cbxBtns.SelectedIndex] as Button).PerformClick();


            }
            else
            {
                ctrls[cbxBtns.SelectedIndex].Text = textBox1.Text;
                tabControl.SelectedTab = ctrls[cbxBtns.SelectedIndex].Parent as TabPage;
                lblSet.Text = "Successfully changed!";
                (ctrls[cbxBtns.SelectedIndex] as Button).PerformClick();

            }
        }

        private void cbxBtns_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = ctrls[cbxBtns.SelectedIndex].Text;
            lblSet.Text = "";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                btnSetName.PerformClick();
            }
            if (e.KeyChar == (char)Keys.Escape)
            {
                Hide();
            }
        }
        
    }
}
