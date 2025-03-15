using System.Drawing;
using System.Windows.Forms;

namespace ScreenSleeper2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private static ContextMenuStrip cms;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            button1 = new Button();
            label1 = new Label();
            notifyIcon1 = new NotifyIcon(components);
            numericUpDown1 = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(267, 169);
            button1.Name = "button1";
            button1.Size = new Size(272, 99);
            button1.TabIndex = 0;
            button1.Text = "Turn Off Screens";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(380, 290);
            label1.Name = "label1";
            label1.Size = new Size(59, 25);
            label1.TabIndex = 1;
            label1.Text = "label1";
            label1.Click += label1_Click;
            // 
            // notifyIcon1
            // 

            notifyIcon1.Text = "ScreenSleeper";
            notifyIcon1.Icon = Properties.Resources.sunIcon;
            cms = new ContextMenuStrip();

            cms.Items.Add(new ToolStripMenuItem("Quit", null, new EventHandler(Quit_Click), "Quit"));

            notifyIcon1.ContextMenuStrip = cms;
            notifyIcon1.Visible = true;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(319, 337);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(180, 31);
            numericUpDown1.TabIndex = 2;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(numericUpDown1);
            Controls.Add(label1);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

    static void Quit_Click(object? sender, System.EventArgs e)
    {
            // End application though ApplicationContext
            Environment.Exit(0);
    }


        #endregion

        private Button button1;
        private Label label1;
        private NotifyIcon notifyIcon1;
        private NumericUpDown numericUpDown1;
    }
}
