namespace DailyDilbertViewer
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox_dilbertComic = new System.Windows.Forms.PictureBox();
            this.button_date_back = new System.Windows.Forms.Button();
            this.button_date_forward = new System.Windows.Forms.Button();
            this.listBox_Dates = new System.Windows.Forms.ListBox();
            this.listBox_tags = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_dilbertComic)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_dilbertComic
            // 
            this.pictureBox_dilbertComic.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_dilbertComic.Name = "pictureBox_dilbertComic";
            this.pictureBox_dilbertComic.Size = new System.Drawing.Size(484, 329);
            this.pictureBox_dilbertComic.TabIndex = 0;
            this.pictureBox_dilbertComic.TabStop = false;
            this.pictureBox_dilbertComic.SizeChanged += new System.EventHandler(this.pictureBox_dilbertComic_SizeChanged);
            this.pictureBox_dilbertComic.Click += new System.EventHandler(this.pictureBox_dilbertComic_Click);
            // 
            // button_date_back
            // 
            this.button_date_back.Location = new System.Drawing.Point(34, 12);
            this.button_date_back.Name = "button_date_back";
            this.button_date_back.Size = new System.Drawing.Size(195, 23);
            this.button_date_back.TabIndex = 1;
            this.button_date_back.Text = "- 1 Day";
            this.button_date_back.UseVisualStyleBackColor = true;
            this.button_date_back.Visible = false;
            this.button_date_back.Click += new System.EventHandler(this.button_date_back_Click);
            // 
            // button_date_forward
            // 
            this.button_date_forward.Location = new System.Drawing.Point(235, 12);
            this.button_date_forward.Name = "button_date_forward";
            this.button_date_forward.Size = new System.Drawing.Size(171, 23);
            this.button_date_forward.TabIndex = 2;
            this.button_date_forward.Text = "+ 1 day";
            this.button_date_forward.UseVisualStyleBackColor = true;
            this.button_date_forward.Visible = false;
            this.button_date_forward.Click += new System.EventHandler(this.button_date_forward_Click);
            // 
            // listBox_Dates
            // 
            this.listBox_Dates.FormattingEnabled = true;
            this.listBox_Dates.Location = new System.Drawing.Point(12, 41);
            this.listBox_Dates.Name = "listBox_Dates";
            this.listBox_Dates.Size = new System.Drawing.Size(129, 329);
            this.listBox_Dates.TabIndex = 3;
            this.listBox_Dates.Visible = false;
            this.listBox_Dates.SelectedIndexChanged += new System.EventHandler(this.listBox_Dates_SelectedIndexChanged);
            // 
            // listBox_tags
            // 
            this.listBox_tags.FormattingEnabled = true;
            this.listBox_tags.Location = new System.Drawing.Point(147, 41);
            this.listBox_tags.Name = "listBox_tags";
            this.listBox_tags.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBox_tags.Size = new System.Drawing.Size(129, 329);
            this.listBox_tags.TabIndex = 4;
            this.listBox_tags.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1050, 460);
            this.Controls.Add(this.listBox_tags);
            this.Controls.Add(this.listBox_Dates);
            this.Controls.Add(this.button_date_forward);
            this.Controls.Add(this.button_date_back);
            this.Controls.Add(this.pictureBox_dilbertComic);
            this.Name = "Form1";
            this.Text = "Comic Viewer";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_dilbertComic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_dilbertComic;
        private System.Windows.Forms.Button button_date_back;
        private System.Windows.Forms.Button button_date_forward;
        private System.Windows.Forms.ListBox listBox_Dates;
        private System.Windows.Forms.ListBox listBox_tags;
    }
}

