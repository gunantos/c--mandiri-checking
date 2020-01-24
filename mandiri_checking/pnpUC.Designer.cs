namespace mandiri_checking
{
    partial class pnpUC
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkbox1 = new System.Windows.Forms.CheckBox();
            this.noreg = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // checkbox1
            // 
            this.checkbox1.AutoSize = true;
            this.checkbox1.Font = new System.Drawing.Font("Microsoft Tai Le", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkbox1.Location = new System.Drawing.Point(15, 13);
            this.checkbox1.Name = "checkbox1";
            this.checkbox1.Size = new System.Drawing.Size(15, 14);
            this.checkbox1.TabIndex = 0;
            this.checkbox1.UseVisualStyleBackColor = true;
            this.checkbox1.CheckedChanged += new System.EventHandler(this.checkbox1_CheckedChanged);
            // 
            // noreg
            // 
            this.noreg.Location = new System.Drawing.Point(621, 13);
            this.noreg.Name = "noreg";
            this.noreg.Size = new System.Drawing.Size(100, 20);
            this.noreg.TabIndex = 1;
            this.noreg.Visible = false;
            // 
            // pnpUC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.noreg);
            this.Controls.Add(this.checkbox1);
            this.Name = "pnpUC";
            this.Size = new System.Drawing.Size(736, 51);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.CheckBox checkbox1;
        public System.Windows.Forms.TextBox noreg;
    }
}
