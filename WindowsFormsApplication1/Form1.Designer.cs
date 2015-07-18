namespace WindowsFormsApplication1
{
    partial class Form1
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
            if (disposing && (components != null))
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdoEllipse = new System.Windows.Forms.RadioButton();
            this.rdoRect = new System.Windows.Forms.RadioButton();
            this.rdoLine = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdoEllipse);
            this.panel1.Controls.Add(this.rdoRect);
            this.panel1.Controls.Add(this.rdoLine);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(267, 24);
            this.panel1.TabIndex = 0;
            // 
            // rdoEllipse
            // 
            this.rdoEllipse.AutoSize = true;
            this.rdoEllipse.Location = new System.Drawing.Point(196, 5);
            this.rdoEllipse.Name = "rdoEllipse";
            this.rdoEllipse.Size = new System.Drawing.Size(57, 16);
            this.rdoEllipse.TabIndex = 0;
            this.rdoEllipse.TabStop = true;
            this.rdoEllipse.Text = "Ellipse";
            this.rdoEllipse.UseVisualStyleBackColor = true;
            // 
            // rdoRect
            // 
            this.rdoRect.AutoSize = true;
            this.rdoRect.Location = new System.Drawing.Point(94, 5);
            this.rdoRect.Name = "rdoRect";
            this.rdoRect.Size = new System.Drawing.Size(74, 16);
            this.rdoRect.TabIndex = 0;
            this.rdoRect.TabStop = true;
            this.rdoRect.Text = "Rectangle";
            this.rdoRect.UseVisualStyleBackColor = true;
            // 
            // rdoLine
            // 
            this.rdoLine.AutoSize = true;
            this.rdoLine.Checked = true;
            this.rdoLine.Location = new System.Drawing.Point(15, 5);
            this.rdoLine.Name = "rdoLine";
            this.rdoLine.Size = new System.Drawing.Size(44, 16);
            this.rdoLine.TabIndex = 0;
            this.rdoLine.TabStop = true;
            this.rdoLine.Text = "Line";
            this.rdoLine.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 404);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rdoEllipse;
        private System.Windows.Forms.RadioButton rdoRect;
        private System.Windows.Forms.RadioButton rdoLine;

    }
}

