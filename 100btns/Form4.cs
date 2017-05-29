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
    public partial class Form4 : Form
    {
        Button selectedBtn;
        bool b;
        public Form4(Button selectedBtn)
        {
            InitializeComponent();
            this.selectedBtn = selectedBtn;
        }
        Form1 frm1 = Application.OpenForms["Form1"] as Form1;

        private void Form4_Load(object sender, EventArgs e)
        {
            textBox1.Text = selectedBtn.Text;
            ActiveControl = textBox1;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                selectedBtn.Text = textBox1.Text;
                frm1.thisedited = true;
                Close();
            }
            if (e.KeyChar == (char)Keys.Escape)
            {
                Close();
            }
        }

        private void Form4_Deactivate(object sender, EventArgs e)
        {
            Form1 frm = ParentForm as Form1;
            Close();
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            b = true;
        }
    }
}
