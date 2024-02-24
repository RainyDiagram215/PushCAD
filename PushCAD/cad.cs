using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace PushCAD
{
    public partial class cad : Form
    {
        public int status = 0;
        public Point onePoint = new Point();
        public Point twoPoint = new Point();
        public Point ThreePoint = new Point();
        Image image;
        Graphics GPS;
        Graphics IG;
        Pen gp;
        Label[] labels = new Label[1024];
        int labelint = 0;
        int selectint = -1;
        string codes = "";
        Form1 fr1;
        string fname = "";
        public cad(Form1 f1, string filename)
        {
            InitializeComponent();
            fr1 = f1;
            fname = filename;
        }
        string[] s1 = new String[1024];

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {

            if (status == 1)
            {
                panel1.Cursor = Cursors.Hand;
                toolTip1.Show("请选择开头", (Panel)sender);
            }
            else if (status == 2)
            {
                panel1.Cursor = Cursors.Hand;
                toolTip1.Show("请选择结尾", (Panel)sender);
            }
            else if (status == 3)
            {
                panel1.Cursor = Cursors.Hand;
                toolTip1.Show("请选择开头", (Panel)sender);
            }
            else if (status == 4)
            {
                panel1.Cursor = Cursors.Hand;
                toolTip1.Show("请选择结尾", (Panel)sender);
            }
            else if (status == 5)
            {
                panel1.Cursor = Cursors.Hand;
                toolTip1.Show("点击以创建文本框", (Panel)sender);
            }
            else if (status == 6)
            {
                panel1.Cursor = Cursors.Hand;
                toolTip1.Show("请选择开头", (Panel)sender);
            }
            else if (status == 7)
            {
                panel1.Cursor = Cursors.Hand;
                toolTip1.Show("请选择结尾", (Panel)sender);
            }
            else if (status == 8)
            {
                panel1.Cursor = Cursors.Hand;
                toolTip1.Show("请选择顶点", (Panel)sender);
            }
            else if (status == 9)
            {
                panel1.Cursor = Cursors.Hand;
                toolTip1.Show("请选择第一个底点", (Panel)sender);
            }
            else if (status == 10)
            {
                panel1.Cursor = Cursors.Hand;
                toolTip1.Show("请选择第二个底点", (Panel)sender);
            }
            else if (status == 0)
            {
                panel1.Cursor = Cursors.Default;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            status = 1;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (status == 1)
            {
                onePoint = panel1.PointToClient(Control.MousePosition);
                status = 2;
            }
            else if (status == 2)
            {
                twoPoint = panel1.PointToClient(Control.MousePosition);
                GPS.DrawLine(gp, onePoint, twoPoint);
                IG.DrawLine(gp, onePoint, twoPoint);
                codes = codes + "line " + onePoint.X + "," + onePoint.Y + "," + twoPoint.X + "," + twoPoint.Y + "\n";

                status = 0;
            }
            else if (status == 3)
            {
                onePoint = panel1.PointToClient(Control.MousePosition);
                status = 4;
            }
            else if (status == 4)
            {
                twoPoint = panel1.PointToClient(Control.MousePosition);
                GPS.DrawRectangle(gp, onePoint.X, onePoint.Y, twoPoint.X - onePoint.X, twoPoint.Y - onePoint.Y);
                IG.DrawRectangle(gp, onePoint.X, onePoint.Y, twoPoint.X - onePoint.X, twoPoint.Y - onePoint.Y);
                codes = codes + "rect " + onePoint.X + "," + onePoint.Y + "," + twoPoint.X + "," + twoPoint.Y + "\n";
                status = 0;
            }
            else if (status == 5)
            {
                Point l = panel1.PointToClient(Control.MousePosition);
                string texts = Microsoft.VisualBasic.Interaction.InputBox("字符");
                if (texts == "")
                {
                    return;
                }
                labels[labelint] = new Label();
                labels[labelint].Text = texts;
                labels[labelint].Parent = panel1;
                labels[labelint].Location = l;
                labels[labelint].Visible = true;
                labels[labelint].ForeColor = colorDialog1.Color;
                labels[labelint].BackColor = Color.Transparent;
                labels[labelint].ContextMenuStrip = contextMenuStrip1;
                labels[labelint].MouseDown += new System.Windows.Forms.MouseEventHandler(Downed);
                labels[labelint].Tag = labelint;
                panel1.Controls.Add(labels[labelint]);
                labelint++;
                status = 0;
            }
            else if (status == 6)
            {
                onePoint = panel1.PointToClient(Control.MousePosition);
                status = 7;
            }
            else if (status == 7)
            {
                twoPoint = panel1.PointToClient(Control.MousePosition);
                GPS.DrawEllipse(gp, onePoint.X, onePoint.Y, twoPoint.X - onePoint.X, twoPoint.Y - onePoint.Y);
                IG.DrawEllipse(gp, onePoint.X, onePoint.Y, twoPoint.X - onePoint.X, twoPoint.Y - onePoint.Y);
                codes = codes + "cir " + onePoint.X + "," + onePoint.Y + "," + twoPoint.X + "," + twoPoint.Y + "\n";
                status = 0;
            }
            else if (status == 8)
            {
                onePoint = panel1.PointToClient(Control.MousePosition);
                status = 9;
            }
            else if (status == 9)
            {
                twoPoint = panel1.PointToClient(Control.MousePosition);
                GPS.DrawLine(gp, onePoint, twoPoint);
                IG.DrawLine(gp, onePoint, twoPoint);
                status = 10;
            }
            else if (status == 10)
            {
                ThreePoint = panel1.PointToClient(Control.MousePosition);
                GPS.DrawLine(gp, twoPoint, ThreePoint);
                GPS.DrawLine(gp, ThreePoint, onePoint);
                IG.DrawLine(gp, twoPoint, ThreePoint);
                IG.DrawLine(gp, ThreePoint, onePoint);
                codes = codes + "poly " + onePoint.X + "," + onePoint.Y + "," + twoPoint.X + "," + twoPoint.Y + "," + ThreePoint.X + "," + ThreePoint.Y + "\n";
                status = 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            status = 3;
        }

        private void 导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "位图文件(*.bmp)|*.bmp|PNG文件(*.png)|*.png|JEPG文件(*.jpg)|*.jpg|GIF文件(*.gif)|*.gif|所有文件(*)|*";
            saveFileDialog1.Title = "导出位置";
            saveFileDialog1.ShowDialog();
            if(saveFileDialog1.FileName == "")
            {
                return;
            }
            if (labelint > 0)
            {
                GraphicsState gg = IG.Save();
                for (int i = 0; i < labelint; i++)
                {
                    SolidBrush drawBrush = new SolidBrush(labels[i].ForeColor);
                    IG.DrawString(labels[i].Text, new Font("微软雅黑", 12), drawBrush, labels[i].Location);
                }
                image.Save("1.bmp");
                IG.Restore(gg);
            }
            else if (labelint == 0)
            {
                image.Save(saveFileDialog1.FileName);
            }
        }

        private void cad_Load(object sender, EventArgs e)
        {
            GPS = panel1.CreateGraphics();
            image = new Bitmap(1920, 1080);
            IG = Graphics.FromImage(image);
            IG.Clear(Color.Black);
            gp = new Pen(Color.White, 2f);
            colorDialog1.Color = Color.White;
            this.WindowState = FormWindowState.Maximized;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            gp = new Pen(colorDialog1.Color, 2f);
            codes = codes + "Color " + Convert.ToString(ColorTranslator.ToOle(colorDialog1.Color) + "\n");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            status = 5;
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void 更改文字ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string a = Microsoft.VisualBasic.Interaction.InputBox("新文字:");
            if (a != "")
            {
                labels[selectint].Text = a;
            }
        }
        private void Downed(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Label l = sender as Label;
                int i = Convert.ToInt32(l.Tag);
                selectint = i;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            status = 6;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            status = 8;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                runs(textBox1.Text);
                textBox1.Text = "";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(fname != "")
            {
                if (labelint > 0)
                {
                    for (int i = 0; i < labelint; i++)
                    {
                        codes = codes + "text " + labels[i].Text + "," + labels[i].Location.X.ToString() + "," + labels[i].Location.Y.ToString() + "\n";
                    }
                    File.WriteAllText(fname, EncodeBase64(codes));
                }
                else
                {
                    File.WriteAllText(fname, EncodeBase64(codes));
                }
            }
            else
            {
                saveFileDialog1.Title = "保存为";
                saveFileDialog1.ShowDialog();
                fname = saveFileDialog1.FileName;
                if(fname != "")
                {
                    if (labelint > 0)
                    {
                        for (int i = 0; i < labelint; i++)
                        {
                            codes = codes + "text " + labels[i].Text + "," + labels[i].Location.X.ToString() + "," + labels[i].Location.Y.ToString() + "," + ColorTranslator.ToOle(labels[i].ForeColor).ToString() + "\n";
                        }
                        File.WriteAllText(fname, EncodeBase64(codes));
                    }
                    else
                    {
                        File.WriteAllText(fname, EncodeBase64(codes));
                    }
                }
                else
                {
                    MessageBox.Show("您已取消操作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        public void runs(string code)
        {
            try
            {
                string[] f1 = code.Split(' ');
                string[] f2 = f1[1].Split(',');
                if (f1[0] == "line")
                {
                    GPS.DrawLine(gp, new Point(Convert.ToInt32(f2[0]), Convert.ToInt32(f2[1])), new Point(Convert.ToInt32(f2[2]), Convert.ToInt32(f2[3])));
                    IG.DrawLine(gp, new Point(Convert.ToInt32(f2[0]), Convert.ToInt32(f2[1])), new Point(Convert.ToInt32(f2[2]), Convert.ToInt32(f2[3])));
                    codes = codes + "line " + f2[0] + "," + f2[1] + "," + f2[2] + "," + f2[3] + "\n";
                }
                else if (f1[0] == "rect")
                {
                    GPS.DrawRectangle(gp, Convert.ToInt32(f2[0]), Convert.ToInt32(f2[1]), Convert.ToInt32(f2[2]) - Convert.ToInt32(f2[0]), Convert.ToInt32(f2[3]) - Convert.ToInt32(f2[1]));
                    IG.DrawRectangle(gp, Convert.ToInt32(f2[0]), Convert.ToInt32(f2[1]), Convert.ToInt32(f2[2]) - Convert.ToInt32(f2[0]), Convert.ToInt32(f2[3]) - Convert.ToInt32(f2[1]));
                    codes = codes + "rect " + f2[0] + "," + f2[1] + "," + f2[2] + "," + f2[3] + "\n";
                }
                else if (f1[0] == "cir")
                {
                    GPS.DrawEllipse(gp, Convert.ToInt32(f2[0]), Convert.ToInt32(f2[1]), Convert.ToInt32(f2[2]) - Convert.ToInt32(f2[0]), Convert.ToInt32(f2[3]) - Convert.ToInt32(f2[1]));
                    IG.DrawEllipse(gp, Convert.ToInt32(f2[0]), Convert.ToInt32(f2[1]), Convert.ToInt32(f2[2]) - Convert.ToInt32(f2[0]), Convert.ToInt32(f2[3]) - Convert.ToInt32(f2[1]));
                    codes = codes + "cir " + f2[0] + "," + f2[1] + "," + f2[2] + "," + f2[3] + "\n";
                }
                else if (f1[0] == "poly")
                {
                    Point onePoint = new Point(Convert.ToInt32(f2[0]), Convert.ToInt32(f2[1]));
                    Point twoPoint = new Point(Convert.ToInt32(f2[2]), Convert.ToInt32(f2[3]));
                    Point ThreePoint = new Point(Convert.ToInt32(f2[4]), Convert.ToInt32(f2[5]));
                    GPS.DrawLine(gp, onePoint, twoPoint);
                    IG.DrawLine(gp, onePoint, twoPoint);
                    GPS.DrawLine(gp, twoPoint, ThreePoint);
                    GPS.DrawLine(gp, ThreePoint, onePoint);
                    IG.DrawLine(gp, twoPoint, ThreePoint);
                    IG.DrawLine(gp, ThreePoint, onePoint);
                    codes = codes + "poly " + f2[0] + "," + f2[1] + "," + f2[2] + "," + f2[3] + "," + f2[4] + "," + f2[5] + "\n";
                }else if(f1[0] == "Color")
                {
                    colorDialog1.Color = ColorTranslator.FromOle(Convert.ToInt32(f2[0]));
                    gp = new Pen(colorDialog1.Color, 2f);
                    codes = codes + "Color " + f2[0] + "\n";
                }
                else if (f1[0] == "text")
                {
                    labels[labelint] = new Label();
                    labels[labelint].Text = f2[0];
                    labels[labelint].Parent = panel1;
                    labels[labelint].Location = new Point(Convert.ToInt32(f2[1]), Convert.ToInt32(f2[2]));
                    labels[labelint].Visible = true;
                    labels[labelint].ForeColor = ColorTranslator.FromOle(Convert.ToInt32(f2[3]));
                    labels[labelint].BackColor = Color.Transparent;
                    labels[labelint].ContextMenuStrip = contextMenuStrip1;
                    labels[labelint].MouseDown += new MouseEventHandler(Downed);
                    labels[labelint].Tag = labelint;
                    panel1.Controls.Add(labels[labelint]);
                    labelint++;
                }
            }
            catch
            {
                //Nothing
            }
        }

        private void cad_FormClosing(object sender, FormClosingEventArgs e)
        {
            fr1.Close();
        }

        private void cad_Shown(object sender, EventArgs e)
        {
            try
            {
                if (fname != "")
                {
                    string iis = File.ReadAllText(fname);
                    string[] f = DecodeBase64(iis).Split('\n');
                    foreach (string i in f)
                    {
                        runs(i);
                    }
                }
            }
            catch
            {
                MessageBox.Show("文件无效!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                fr1.Close();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.BackgroundImage = image;
        }
        ///编码
        public static string EncodeBase64(string code)
        {
            string encode = "";
            byte[] bytes = Encoding.GetEncoding(65001).GetBytes(code);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = code;
            }
            return encode;
        }
        ///解码
        public static string DecodeBase64(string code)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(code);
            try
            {
                decode = Encoding.GetEncoding(65001).GetString(bytes);
            }
            catch
            {
                decode = code;
            }
            return decode;
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox1().Show();
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "打开项目文件";
            openFileDialog1.Filter = "项目文件(*.pcproj)|*.pcproj|所有文件(*)|*";
            openFileDialog1.ShowDialog();
            new cad(fr1, openFileDialog1.FileName).Show();
            this.Hide();
        }

        private void 获取帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool it = File.Exists("help.chm");
            if(it == true)
            {
                System.Diagnostics.Process.Start("hh.exe", "help.chm");
            }
            else
            {
                MessageBox.Show("帮助文件丢失!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
