namespace SCARA
{
    partial class SCARAConfig
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
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LowerLength = new System.Windows.Forms.TextBox();
            this.UpperLength = new System.Windows.Forms.TextBox();
            this.ShoulderResolution = new System.Windows.Forms.TextBox();
            this.ElbowResolution = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(746, 371);
            this.button1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(215, 76);
            this.button1.TabIndex = 0;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(193, 92);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(245, 32);
            this.label2.TabIndex = 52;
            this.label2.Text = "Lower Arm Length";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(193, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 32);
            this.label1.TabIndex = 51;
            this.label1.Text = "Upper Arm Length";
            // 
            // LowerLength
            // 
            this.LowerLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LowerLength.Location = new System.Drawing.Point(460, 87);
            this.LowerLength.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.LowerLength.Name = "LowerLength";
            this.LowerLength.Size = new System.Drawing.Size(180, 39);
            this.LowerLength.TabIndex = 50;
            this.LowerLength.Text = "4.0";
            // 
            // UpperLength
            // 
            this.UpperLength.AcceptsReturn = true;
            this.UpperLength.AcceptsTab = true;
            this.UpperLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpperLength.Location = new System.Drawing.Point(460, 28);
            this.UpperLength.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.UpperLength.Name = "UpperLength";
            this.UpperLength.Size = new System.Drawing.Size(180, 39);
            this.UpperLength.TabIndex = 49;
            this.UpperLength.Text = "6.0";
            // 
            // ShoulderResolution
            // 
            this.ShoulderResolution.Enabled = false;
            this.ShoulderResolution.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ShoulderResolution.Location = new System.Drawing.Point(460, 170);
            this.ShoulderResolution.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.ShoulderResolution.Name = "ShoulderResolution";
            this.ShoulderResolution.Size = new System.Drawing.Size(180, 39);
            this.ShoulderResolution.TabIndex = 53;
            this.ShoulderResolution.Text = "2000";
            // 
            // ElbowResolution
            // 
            this.ElbowResolution.Enabled = false;
            this.ElbowResolution.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ElbowResolution.Location = new System.Drawing.Point(460, 240);
            this.ElbowResolution.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.ElbowResolution.Name = "ElbowResolution";
            this.ElbowResolution.Size = new System.Drawing.Size(180, 39);
            this.ElbowResolution.TabIndex = 54;
            this.ElbowResolution.Text = "2000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(51, 175);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(386, 32);
            this.label3.TabIndex = 55;
            this.label3.Text = "Shoulder Encoder Resolution";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(88, 246);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(349, 32);
            this.label4.TabIndex = 56;
            this.label4.Text = "Elbow Encoder Resolution";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(655, 175);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(252, 32);
            this.label5.TabIndex = 57;
            this.label5.Text = "(counts per radian)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(160, 312);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(665, 31);
            this.label7.TabIndex = 59;
            this.label7.Text = "Must be connected to device to set encdoer resoluton.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(655, 251);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(252, 32);
            this.label6.TabIndex = 60;
            this.label6.Text = "(counts per radian)";
            // 
            // SCARAConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 474);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ElbowResolution);
            this.Controls.Add(this.ShoulderResolution);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LowerLength);
            this.Controls.Add(this.UpperLength);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "SCARAConfig";
            this.Text = "Form3";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox LowerLength;
        private System.Windows.Forms.TextBox UpperLength;
        private System.Windows.Forms.TextBox ShoulderResolution;
        private System.Windows.Forms.TextBox ElbowResolution;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
    }
}