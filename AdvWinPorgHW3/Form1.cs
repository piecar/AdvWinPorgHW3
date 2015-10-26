using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdvWinPorgHW3
{
    public partial class Form1 : Form
    {
        bool drawEllipse = false;
        bool drawLine = false;
        Size offset = new Size(0, 0);
        Bitmap bubs = new Bitmap(@"C:\Users\pieca\Documents\Adv Win Prog\AdvWinPorgHW3\AdvWinPorgHW3\Resources\hotline-miami-wallpaper.jpg");
        int moveAmt = 40;

        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private void solidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.drawLine = !this.drawLine;
            this.Invalidate(true);
        }

        private void tabPens_Paint(object sender, PaintEventArgs e)
        {
            if (!this.drawLine) return;
            else
            {
                using (Pen myPen = new Pen(Color.DarkOrange, 5.0f))
                {
                    Graphics g = e.Graphics;
                    g.DrawEllipse(myPen, this.ClientRectangle);
                }
            }
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void solidToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.drawEllipse = !this.drawEllipse;
            this.Invalidate(true);
        }
        
        private void tabBrushes_Paint(object sender, PaintEventArgs e)
        {
            if (!this.drawEllipse) return;
            else
            {
                using (Brush myBrush = new SolidBrush(Color.DarkOrange))
                {
                    Graphics g = e.Graphics;

                    g.FillEllipse(Brushes.DarkBlue, this.ClientRectangle);
                    //// Set percentages of width where line starts, then space starts,
                    //// then line starts again in alternating pattern
                    //pen.CompoundArray =
                    //  new float[] { 0.0f, 0.25f, 0.45f, 0.55f, 0.75f, 1.0f, };
                    ////pen.DashStyle = DashStyle.Custom;
                    ////pen.DashPattern = new float[] { 1f, 1f, 1f, 2f, 1f, 3f };
                    //g.DrawRectangle(pen, new Rectangle(20, 20, this.ClientRectangle.Width - 40, this.ClientRectangle.Height - 40));
                }
            }
        }

        private void tabPanning_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle destRect = this.tabPanning.ClientRectangle;
            Rectangle srcRect = new Rectangle(this.offset.Width, this.offset.Height, destRect.Width, destRect.Height);
            g.DrawImage(this.bubs, destRect, srcRect, g.PageUnit);
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            offset.Height -= moveAmt;
            this.Invalidate(true);
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            offset.Width -= moveAmt;
            this.Invalidate(true);
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            offset.Height += moveAmt;
            this.Invalidate(true);
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {
            offset.Width += moveAmt;
            this.Invalidate(true);
        }
    }
}
