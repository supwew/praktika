#nullable disable
using System.Drawing;
using System.Windows.Forms;

namespace zadanie4
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
            toolbarPanel = new FlowLayoutPanel();
            openButton = new Button();
            zoomInButton = new Button();
            zoomOutButton = new Button();
            fitButton = new Button();
            zoomLabel = new Label();
            scrollPanel = new Panel();
            pictureBox1 = new PictureBox();
            openFileDialog1 = new OpenFileDialog();
            toolbarPanel.SuspendLayout();
            scrollPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // toolbarPanel
            // 
            toolbarPanel.Controls.Add(openButton);
            toolbarPanel.Controls.Add(zoomInButton);
            toolbarPanel.Controls.Add(zoomOutButton);
            toolbarPanel.Controls.Add(fitButton);
            toolbarPanel.Controls.Add(zoomLabel);
            toolbarPanel.Dock = DockStyle.Top;
            toolbarPanel.Location = new Point(0, 0);
            toolbarPanel.Margin = new Padding(3, 4, 3, 4);
            toolbarPanel.Name = "toolbarPanel";
            toolbarPanel.Size = new Size(914, 53);
            toolbarPanel.TabIndex = 0;
            // 
            // openButton
            // 
            openButton.Location = new Point(3, 4);
            openButton.Margin = new Padding(3, 4, 3, 4);
            openButton.Name = "openButton";
            openButton.Size = new Size(86, 31);
            openButton.TabIndex = 0;
            openButton.Text = "Открыть";
            openButton.Click += openButton_Click;
            // 
            // zoomInButton
            // 
            zoomInButton.Location = new Point(95, 4);
            zoomInButton.Margin = new Padding(3, 4, 3, 4);
            zoomInButton.Name = "zoomInButton";
            zoomInButton.Size = new Size(46, 31);
            zoomInButton.TabIndex = 1;
            zoomInButton.Text = "+";
            zoomInButton.Click += zoomInButton_Click;
            // 
            // zoomOutButton
            // 
            zoomOutButton.Location = new Point(147, 4);
            zoomOutButton.Margin = new Padding(3, 4, 3, 4);
            zoomOutButton.Name = "zoomOutButton";
            zoomOutButton.Size = new Size(46, 31);
            zoomOutButton.TabIndex = 2;
            zoomOutButton.Text = "-";
            zoomOutButton.Click += zoomOutButton_Click;
            // 
            // fitButton
            // 
            fitButton.Location = new Point(199, 4);
            fitButton.Margin = new Padding(3, 4, 3, 4);
            fitButton.Name = "fitButton";
            fitButton.Size = new Size(145, 31);
            fitButton.TabIndex = 3;
            fitButton.Text = "По размеру";
            fitButton.Click += fitButton_Click;
            // 
            // zoomLabel
            // 
            zoomLabel.AutoSize = true;
            zoomLabel.Location = new Point(358, 11);
            zoomLabel.Margin = new Padding(11, 11, 0, 0);
            zoomLabel.Name = "zoomLabel";
            zoomLabel.Size = new Size(45, 20);
            zoomLabel.TabIndex = 4;
            zoomLabel.Text = "100%";
            zoomLabel.Click += zoomLabel_Click;
            // 
            // scrollPanel
            // 
            scrollPanel.AutoScroll = true;
            scrollPanel.Controls.Add(pictureBox1);
            scrollPanel.Dock = DockStyle.Fill;
            scrollPanel.Location = new Point(0, 53);
            scrollPanel.Margin = new Padding(3, 4, 3, 4);
            scrollPanel.Name = "scrollPanel";
            scrollPanel.Size = new Size(914, 747);
            scrollPanel.TabIndex = 1;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(114, 67);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // openFileDialog1
            // 
            openFileDialog1.Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp;*.gif|Все файлы|*.*";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 800);
            Controls.Add(scrollPanel);
            Controls.Add(toolbarPanel);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Просмотр изображений";
            toolbarPanel.ResumeLayout(false);
            toolbarPanel.PerformLayout();
            scrollPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel toolbarPanel;
        private Button openButton;
        private Button zoomInButton;
        private Button zoomOutButton;
        private Button fitButton;
        private Label zoomLabel;
        private Panel scrollPanel;
        private PictureBox pictureBox1;
        private OpenFileDialog openFileDialog1;
    }
}