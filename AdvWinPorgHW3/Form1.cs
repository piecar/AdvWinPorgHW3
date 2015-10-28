using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        Point downPoint = Point.Empty;
        Size offset = new Size(0, 0);
        Bitmap bubs = Properties.Resources.kim_holtermand_reflections_1920x1200;
        Bitmap wheel = Properties.Resources.Categories_preferences_system_icon;
        int moveAmt = 40;
        BufferedGraphicsContext bufferContext;

        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.bufferContext = new BufferedGraphicsContext();
            bufferContext.MaximumBuffer = panelPanning.Size;
        }

        private void solidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.drawLine = !this.drawLine;
            if (solidToolStripMenuItem.Checked) solidToolStripMenuItem.Checked = false;
            else solidToolStripMenuItem.Checked = true;
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
                    if(solidToolStripMenuItem.Checked)
                        g.DrawEllipse(myPen, this.ClientRectangle);
                    if(customToolStripMenuItem.Checked)
                    {
                        myPen.DashStyle = DashStyle.Custom;
                        myPen.DashPattern = new float[] { 1f, 1f, 6f, 1f, 3f, 1f };
                        g.DrawEllipse(myPen, this.ClientRectangle);
                    }
                    if(compoundToolStripMenuItem.Checked)
                    {
                        myPen.Width = 20;
                        myPen.CompoundArray = new float[] { 0.0f, 0.10f, 0.35f, 0.60f, 0.85f, 1.0f, };
                        g.DrawEllipse(myPen, new Rectangle(20, 20, this.ClientRectangle.Width - 40, this.ClientRectangle.Height - 150));
                    }
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
            if (solidToolStripMenuItem1.Checked) solidToolStripMenuItem1.Checked = false;
            else solidToolStripMenuItem1.Checked = true;
            this.Invalidate(true);
        }
        
        private void tabBrushes_Paint(object sender, PaintEventArgs e)
        {
            if (!this.drawEllipse) return;
            else
            {
               
                
                    if(solidToolStripMenuItem1.Checked)
                    {
                        using (Brush myBrush = new SolidBrush(Color.DarkOrange))
                        {
                            Graphics g = e.Graphics;
                            g.FillEllipse(myBrush, this.ClientRectangle);
                        }
                            
                    }
                    if(textureToolStripMenuItem.Checked)
                    {
                        using (Brush brush = new TextureBrush(wheel))
                        {
                            Graphics g = e.Graphics;
                            g.FillEllipse(brush, this.ClientRectangle);
                        }                            
                    }
                    if (linearGradientToolStripMenuItem.Checked)
                    {
                        using (LinearGradientBrush bbrush = 
                            new LinearGradientBrush(this.ClientRectangle, Color.DarkGreen, Color.LightSkyBlue, 20.0f))
                        {
                            Graphics g = e.Graphics;
                            ColorBlend myBlend= new ColorBlend();
                            myBlend.Colors = new Color[] { Color.DarkGreen, Color.Maroon, Color.LightSkyBlue };
                            myBlend.Positions = new float[] { 0.0f, 0.50f, 1.0f };
                            bbrush.InterpolationColors = myBlend;
                            g.FillEllipse(bbrush, this.ClientRectangle);
                        }  
                     }
            }
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            if (offset.Height >= moveAmt)
            {
                offset.Height -= moveAmt;
            }
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

        private void panelPanning_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            using (BufferedGraphics frame = bufferContext.Allocate(g, this.panelPanning.ClientRectangle))
            {
                Rectangle destRect = this.panelPanning.ClientRectangle;
                Rectangle srcRect = new Rectangle(this.offset.Width, this.offset.Height, destRect.Width, destRect.Height);
                frame.Graphics.DrawImage(this.bubs, destRect, srcRect, g.PageUnit);
                frame.Render();
            }
            //Rectangle destRect = this.panelPanning.ClientRectangle;
            //Rectangle srcRect = new Rectangle(this.offset.Width, this.offset.Height, destRect.Width, destRect.Height);
            //g.DrawImage(this.bubs, destRect, srcRect, g.PageUnit);
        }

        private void tabPens_Leave(object sender, EventArgs e)
        {
            penItemsToolStripMenuItem.Visible = false;
        }

        private void tabPens_Enter(object sender, EventArgs e)
        {
            penItemsToolStripMenuItem.Visible = true;
        }

        private void tabBrushes_Enter(object sender, EventArgs e)
        {
            brushItemsToolStripMenuItem.Visible = true;
        }

        private void tabBrushes_Leave(object sender, EventArgs e)
        {
            brushItemsToolStripMenuItem.Visible = false;
        }

        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.drawLine = !this.drawLine;
            if (customToolStripMenuItem.Checked) customToolStripMenuItem.Checked = false;
            else customToolStripMenuItem.Checked = true;
            this.Invalidate(true);
        }

        private void compoundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.drawLine = !this.drawLine;
            if (compoundToolStripMenuItem.Checked) compoundToolStripMenuItem.Checked = false;
            else compoundToolStripMenuItem.Checked = true;
            this.Invalidate(true);
        }

        private void tabPanning_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            downPoint = new Point(e.X, e.Y);
        }

        private void tabPanning_MouseMove(object sender, MouseEventArgs e)
        {
            if (downPoint == Point.Empty) return;
                this.offset.Height = this.offset.Height - e.Y + downPoint.Y;
                this.offset.Width = this.offset.Width - e.X + downPoint.X;
                if (this.offset.Height < 0) this.offset.Height = 0;
                if (this.offset.Width < 0) this.offset.Width = 0;
                this.Invalidate(true);
            
        }

        private void tabPanning_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            downPoint = Point.Empty;
        }

        private void textureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.drawEllipse = !this.drawEllipse;
            if (textureToolStripMenuItem.Checked) textureToolStripMenuItem.Checked = false;
            else textureToolStripMenuItem.Checked = true;
            this.Invalidate(true);
        }

        private void linearGradientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.drawEllipse = !this.drawEllipse;
            if (linearGradientToolStripMenuItem.Checked) linearGradientToolStripMenuItem.Checked = false;
            else linearGradientToolStripMenuItem.Checked = true;
            this.Invalidate(true);
        }
    }
}
