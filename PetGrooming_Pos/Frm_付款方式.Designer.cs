
namespace PetGrooming_Pos
{
    partial class Frm_付款方式
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_付款方式));
            this.btn_Cash = new System.Windows.Forms.Button();
            this.btn_Credit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Cash
            // 
            this.btn_Cash.BackColor = System.Drawing.Color.Khaki;
            this.btn_Cash.Dock = System.Windows.Forms.DockStyle.Left;
            this.btn_Cash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Cash.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_Cash.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btn_Cash.Location = new System.Drawing.Point(0, 0);
            this.btn_Cash.Name = "btn_Cash";
            this.btn_Cash.Size = new System.Drawing.Size(385, 292);
            this.btn_Cash.TabIndex = 0;
            this.btn_Cash.Text = "現金";
            this.btn_Cash.UseVisualStyleBackColor = false;
            this.btn_Cash.Click += new System.EventHandler(this.btn_Cash_Click);
            // 
            // btn_Credit
            // 
            this.btn_Credit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btn_Credit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Credit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Credit.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btn_Credit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.btn_Credit.Location = new System.Drawing.Point(384, 0);
            this.btn_Credit.Name = "btn_Credit";
            this.btn_Credit.Size = new System.Drawing.Size(402, 292);
            this.btn_Credit.TabIndex = 1;
            this.btn_Credit.Text = "信用卡";
            this.btn_Credit.UseVisualStyleBackColor = false;
            this.btn_Credit.Click += new System.EventHandler(this.btn_Credit_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_Cash);
            this.panel1.Controls.Add(this.btn_Credit);
            this.panel1.Location = new System.Drawing.Point(1, 76);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(786, 292);
            this.panel1.TabIndex = 2;
            // 
            // Frm_付款方式
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(214)))), ((int)(((byte)(214)))));
            this.ClientSize = new System.Drawing.Size(784, 457);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frm_付款方式";
            this.Text = "付款方式";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Cash;
        private System.Windows.Forms.Button btn_Credit;
        private System.Windows.Forms.Panel panel1;
    }
}