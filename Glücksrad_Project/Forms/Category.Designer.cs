namespace Glücksrad_Project
{
    partial class Category
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
      this.btnCat1 = new System.Windows.Forms.Button();
      this.btnCat2 = new System.Windows.Forms.Button();
      this.btnCat3 = new System.Windows.Forms.Button();
      this.btnCat4 = new System.Windows.Forms.Button();
      this.btnCat5 = new System.Windows.Forms.Button();
      this.btnCat6 = new System.Windows.Forms.Button();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // btnCat1
      // 
      this.btnCat1.Location = new System.Drawing.Point(6, 19);
      this.btnCat1.Name = "btnCat1";
      this.btnCat1.Size = new System.Drawing.Size(200, 50);
      this.btnCat1.TabIndex = 0;
      this.btnCat1.Text = "Gaming";
      this.btnCat1.UseVisualStyleBackColor = true;
      this.btnCat1.Click += new System.EventHandler(this.btnCat1_Click);
      // 
      // btnCat2
      // 
      this.btnCat2.Location = new System.Drawing.Point(219, 19);
      this.btnCat2.Name = "btnCat2";
      this.btnCat2.Size = new System.Drawing.Size(200, 50);
      this.btnCat2.TabIndex = 1;
      this.btnCat2.Text = "Movies";
      this.btnCat2.UseVisualStyleBackColor = true;
      this.btnCat2.Click += new System.EventHandler(this.btnCat2_Click);
      // 
      // btnCat3
      // 
      this.btnCat3.Location = new System.Drawing.Point(6, 75);
      this.btnCat3.Name = "btnCat3";
      this.btnCat3.Size = new System.Drawing.Size(200, 50);
      this.btnCat3.TabIndex = 2;
      this.btnCat3.Text = "Food";
      this.btnCat3.UseVisualStyleBackColor = true;
      this.btnCat3.Click += new System.EventHandler(this.btnCat3_Click);
      // 
      // btnCat4
      // 
      this.btnCat4.Location = new System.Drawing.Point(219, 75);
      this.btnCat4.Name = "btnCat4";
      this.btnCat4.Size = new System.Drawing.Size(200, 50);
      this.btnCat4.TabIndex = 3;
      this.btnCat4.Text = "Sport";
      this.btnCat4.UseVisualStyleBackColor = true;
      this.btnCat4.Click += new System.EventHandler(this.btnCat4_Click);
      // 
      // btnCat5
      // 
      this.btnCat5.Location = new System.Drawing.Point(6, 131);
      this.btnCat5.Name = "btnCat5";
      this.btnCat5.Size = new System.Drawing.Size(200, 50);
      this.btnCat5.TabIndex = 4;
      this.btnCat5.Text = "Music";
      this.btnCat5.UseVisualStyleBackColor = true;
      this.btnCat5.Click += new System.EventHandler(this.btnCat5_Click);
      // 
      // btnCat6
      // 
      this.btnCat6.Location = new System.Drawing.Point(219, 131);
      this.btnCat6.Name = "btnCat6";
      this.btnCat6.Size = new System.Drawing.Size(200, 50);
      this.btnCat6.TabIndex = 5;
      this.btnCat6.Text = "Random";
      this.btnCat6.UseVisualStyleBackColor = true;
      this.btnCat6.Click += new System.EventHandler(this.btnCat6_Click);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.btnCat4);
      this.groupBox1.Controls.Add(this.btnCat6);
      this.groupBox1.Controls.Add(this.btnCat1);
      this.groupBox1.Controls.Add(this.btnCat5);
      this.groupBox1.Controls.Add(this.btnCat2);
      this.groupBox1.Controls.Add(this.btnCat3);
      this.groupBox1.Location = new System.Drawing.Point(12, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(425, 187);
      this.groupBox1.TabIndex = 6;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Wählen Sie eine Kategorie";
      // 
      // Category
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(451, 205);
      this.ControlBox = false;
      this.Controls.Add(this.groupBox1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.Name = "Category";
      this.Text = "Category";
      this.groupBox1.ResumeLayout(false);
      this.ResumeLayout(false);

        }

    #endregion

    private System.Windows.Forms.Button btnCat1;
    private System.Windows.Forms.Button btnCat2;
    private System.Windows.Forms.Button btnCat3;
    private System.Windows.Forms.Button btnCat4;
    private System.Windows.Forms.Button btnCat5;
    private System.Windows.Forms.Button btnCat6;
    private System.Windows.Forms.GroupBox groupBox1;
  }
}