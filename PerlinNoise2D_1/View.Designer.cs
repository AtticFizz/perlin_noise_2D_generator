namespace FormsApp
{
    partial class View
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            this.debugTextbox = new System.Windows.Forms.RichTextBox();
            this.framerateTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // debugTextbox
            // 
            this.debugTextbox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.debugTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.debugTextbox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.debugTextbox.ForeColor = System.Drawing.Color.LimeGreen;
            this.debugTextbox.Location = new System.Drawing.Point(12, 933);
            this.debugTextbox.Name = "debugTextbox";
            this.debugTextbox.ReadOnly = true;
            this.debugTextbox.Size = new System.Drawing.Size(380, 96);
            this.debugTextbox.TabIndex = 0;
            this.debugTextbox.Text = "";
            // 
            // framerateTimer
            // 
            this.framerateTimer.Enabled = true;
            this.framerateTimer.Interval = 17;
            this.framerateTimer.Tick += new System.EventHandler(this.framerateTimer_Tick);
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1041);
            this.Controls.Add(this.debugTextbox);
            this.Name = "View";
            this.Text = "View";
            this.Load += new System.EventHandler(this.View_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private RichTextBox debugTextbox;
        private System.Windows.Forms.Timer framerateTimer;
    }
}