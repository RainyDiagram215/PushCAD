using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PushCAD
{
    public partial class Form1 : Form
    {
        string a = "";
        public Form1(string arg)
        {
            a = arg;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new cad(this,"").Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "打开项目文件";
            openFileDialog1.Filter = "项目文件(*.pcproj)|*.pcproj|所有文件(*)|*";
            openFileDialog1.ShowDialog();
            new cad(this, openFileDialog1.FileName).Show();
            this.Hide();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (a != "")
            {
                new cad(this, a.Replace('"', ' ')).Show();
                this.Hide();
            }
        }
    }
}
