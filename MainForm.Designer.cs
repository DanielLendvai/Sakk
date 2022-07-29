
namespace Sakk
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.FiguraKepek = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // FiguraKepek
            // 
            this.FiguraKepek.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("FiguraKepek.ImageStream")));
            this.FiguraKepek.TransparentColor = System.Drawing.Color.Transparent;
            this.FiguraKepek.Images.SetKeyName(0, "bp.png");
            this.FiguraKepek.Images.SetKeyName(1, "bn.png");
            this.FiguraKepek.Images.SetKeyName(2, "bb.png");
            this.FiguraKepek.Images.SetKeyName(3, "br.png");
            this.FiguraKepek.Images.SetKeyName(4, "bq.png");
            this.FiguraKepek.Images.SetKeyName(5, "bk.png");
            this.FiguraKepek.Images.SetKeyName(6, "wp.png");
            this.FiguraKepek.Images.SetKeyName(7, "wn.png");
            this.FiguraKepek.Images.SetKeyName(8, "wb.png");
            this.FiguraKepek.Images.SetKeyName(9, "wr.png");
            this.FiguraKepek.Images.SetKeyName(10, "wq.png");
            this.FiguraKepek.Images.SetKeyName(11, "wk.png");
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 555);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Sakk";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.MainForm_DragOver);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList FiguraKepek;
    }
}

