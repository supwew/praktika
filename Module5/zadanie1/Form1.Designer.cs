#nullable disable
using System.Drawing;
using System.Windows.Forms;

namespace zadanie1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.toolbarPanel = new FlowLayoutPanel();
            this.lineRadioButton = new RadioButton();
            this.circleRadioButton = new RadioButton();
            this.squareRadioButton = new RadioButton();
            this.colorButton = new Button();
            this.clearButton = new Button();
            this.canvasPictureBox = new PictureBox();
            this.toolbarPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvasPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // toolbarPanel
            // 
            this.toolbarPanel.Controls.Add(this.lineRadioButton);
            this.toolbarPanel.Controls.Add(this.circleRadioButton);
            this.toolbarPanel.Controls.Add(this.squareRadioButton);
            this.toolbarPanel.Controls.Add(this.colorButton);
            this.toolbarPanel.Controls.Add(this.clearButton);
            this.toolbarPanel.Dock = DockStyle.Top;
            this.toolbarPanel.Location = new Point(0, 0);
            this.toolbarPanel.Name = "toolbarPanel";
            this.toolbarPanel.Size = new Size(900, 40);
            this.toolbarPanel.TabIndex = 0;
            // 
            // lineRadioButton
            // 
            this.lineRadioButton.AutoSize = true;
            this.lineRadioButton.Checked = true;
            this.lineRadioButton.Name = "lineRadioButton";
            this.lineRadioButton.TabStop = true;
            this.lineRadioButton.Text = "Линия";
            // 
            // circleRadioButton
            // 
            this.circleRadioButton.AutoSize = true;
            this.circleRadioButton.Name = "circleRadioButton";
            this.circleRadioButton.Text = "Круг";
            // 
            // squareRadioButton
            // 
            this.squareRadioButton.AutoSize = true;
            this.squareRadioButton.Name = "squareRadioButton";
            this.squareRadioButton.Text = "Квадрат";
            // 
            // colorButton
            // 
            this.colorButton.Name = "colorButton";
            this.colorButton.Text = "Цвет";
            this.colorButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Name = "clearButton";
            this.clearButton.Text = "Очистить";
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // canvasPictureBox
            // 
            this.canvasPictureBox.Dock = DockStyle.Fill;
            this.canvasPictureBox.Name = "canvasPictureBox";
            this.canvasPictureBox.TabStop = false;
            this.canvasPictureBox.Paint += new PaintEventHandler(this.canvasPictureBox_Paint);
            this.canvasPictureBox.MouseDown += new MouseEventHandler(this.canvasPictureBox_MouseDown);
            this.canvasPictureBox.MouseMove += new MouseEventHandler(this.canvasPictureBox_MouseMove);
            this.canvasPictureBox.MouseUp += new MouseEventHandler(this.canvasPictureBox_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(900, 600);
            this.Controls.Add(this.canvasPictureBox);
            this.Controls.Add(this.toolbarPanel);
            this.Name = "Form1";
            this.Text = "Рисование";
            this.toolbarPanel.ResumeLayout(false);
            this.toolbarPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.canvasPictureBox)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel toolbarPanel;
        private RadioButton lineRadioButton;
        private RadioButton circleRadioButton;
        private RadioButton squareRadioButton;
        private Button colorButton;
        private Button clearButton;
        private PictureBox canvasPictureBox;
    }
}