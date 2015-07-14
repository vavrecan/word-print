namespace Word_Print
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonInstall = new System.Windows.Forms.Button();
            this.richTextBoxInformations = new System.Windows.Forms.RichTextBox();
            this.buttonTest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonInstall
            // 
            this.buttonInstall.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonInstall.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonInstall.Location = new System.Drawing.Point(12, 12);
            this.buttonInstall.Name = "buttonInstall";
            this.buttonInstall.Size = new System.Drawing.Size(551, 33);
            this.buttonInstall.TabIndex = 0;
            this.buttonInstall.Text = "Install";
            this.buttonInstall.UseVisualStyleBackColor = true;
            this.buttonInstall.Click += new System.EventHandler(this.buttonInstall_Click);
            // 
            // richTextBoxInformations
            // 
            this.richTextBoxInformations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxInformations.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBoxInformations.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxInformations.Location = new System.Drawing.Point(12, 51);
            this.richTextBoxInformations.Name = "richTextBoxInformations";
            this.richTextBoxInformations.ReadOnly = true;
            this.richTextBoxInformations.Size = new System.Drawing.Size(551, 283);
            this.richTextBoxInformations.TabIndex = 1;
            this.richTextBoxInformations.Text = "";
            // 
            // buttonTest
            // 
            this.buttonTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTest.Location = new System.Drawing.Point(421, 51);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(142, 25);
            this.buttonTest.TabIndex = 2;
            this.buttonTest.Text = "Test - Print Preview";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 346);
            this.Controls.Add(this.buttonTest);
            this.Controls.Add(this.richTextBoxInformations);
            this.Controls.Add(this.buttonInstall);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Word Print Protocol";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonInstall;
        private System.Windows.Forms.RichTextBox richTextBoxInformations;
        private System.Windows.Forms.Button buttonTest;
    }
}

