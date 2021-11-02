namespace SCARA_Example
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
            this.MoveButton = new System.Windows.Forms.Button();
            this.Stopbutton = new System.Windows.Forms.Button();
            this.Disablebutton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.X_textBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Y_textBox = new System.Windows.Forms.TextBox();
            this.Theta_textBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Phi_textBox = new System.Windows.Forms.TextBox();
            this.RightyRadioButton = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.SimMove = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.Teachbutton = new System.Windows.Forms.Button();
            this.MoveNumber = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.Connectbutton = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label11 = new System.Windows.Forms.Label();
            this.DeltaTime = new System.Windows.Forms.TextBox();
            this.ClearPath = new System.Windows.Forms.Button();
            this.SourceScalarBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SCARAConfigDlg_button = new System.Windows.Forms.Button();
            this.MoveDebug = new System.Windows.Forms.Button();
            this.Reset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MoveNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // MoveButton
            // 
            this.MoveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MoveButton.Location = new System.Drawing.Point(1202, 363);
            this.MoveButton.Name = "MoveButton";
            this.MoveButton.Size = new System.Drawing.Size(148, 33);
            this.MoveButton.TabIndex = 5;
            this.MoveButton.Text = "Do Move";
            this.MoveButton.UseVisualStyleBackColor = true;
            this.MoveButton.Click += new System.EventHandler(this.MoveButton_Click);
            // 
            // Stopbutton
            // 
            this.Stopbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Stopbutton.Location = new System.Drawing.Point(1202, 629);
            this.Stopbutton.Name = "Stopbutton";
            this.Stopbutton.Size = new System.Drawing.Size(148, 29);
            this.Stopbutton.TabIndex = 11;
            this.Stopbutton.Text = "Stop";
            this.Stopbutton.UseVisualStyleBackColor = true;
            this.Stopbutton.Click += new System.EventHandler(this.Stopbutton_Click_1);
            // 
            // Disablebutton
            // 
            this.Disablebutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Disablebutton.Location = new System.Drawing.Point(1202, 677);
            this.Disablebutton.Name = "Disablebutton";
            this.Disablebutton.Size = new System.Drawing.Size(148, 33);
            this.Disablebutton.TabIndex = 12;
            this.Disablebutton.Text = "Disable Drive";
            this.Disablebutton.UseVisualStyleBackColor = true;
            this.Disablebutton.Click += new System.EventHandler(this.Disablebutton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitButton.Location = new System.Drawing.Point(1261, 75);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(89, 38);
            this.ExitButton.TabIndex = 15;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(11, 11);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(725, 674);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // X_textBox
            // 
            this.X_textBox.Location = new System.Drawing.Point(133, 28);
            this.X_textBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.X_textBox.Name = "X_textBox";
            this.X_textBox.Size = new System.Drawing.Size(74, 20);
            this.X_textBox.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(104, 31);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "X:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(104, 54);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Y:";
            // 
            // Y_textBox
            // 
            this.Y_textBox.Location = new System.Drawing.Point(133, 51);
            this.Y_textBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Y_textBox.Name = "Y_textBox";
            this.Y_textBox.Size = new System.Drawing.Size(74, 20);
            this.Y_textBox.TabIndex = 19;
            // 
            // Theta_textBox
            // 
            this.Theta_textBox.Location = new System.Drawing.Point(133, 99);
            this.Theta_textBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Theta_textBox.Name = "Theta_textBox";
            this.Theta_textBox.Size = new System.Drawing.Size(74, 20);
            this.Theta_textBox.TabIndex = 23;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 79);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Shoulder Angle (rad):";
            // 
            // Phi_textBox
            // 
            this.Phi_textBox.Location = new System.Drawing.Point(133, 75);
            this.Phi_textBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Phi_textBox.Name = "Phi_textBox";
            this.Phi_textBox.Size = new System.Drawing.Size(74, 20);
            this.Phi_textBox.TabIndex = 21;
            // 
            // RightyRadioButton
            // 
            this.RightyRadioButton.AutoSize = true;
            this.RightyRadioButton.Checked = true;
            this.RightyRadioButton.Location = new System.Drawing.Point(39, 134);
            this.RightyRadioButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.RightyRadioButton.Name = "RightyRadioButton";
            this.RightyRadioButton.Size = new System.Drawing.Size(55, 17);
            this.RightyRadioButton.TabIndex = 24;
            this.RightyRadioButton.TabStop = true;
            this.RightyRadioButton.Text = "Righty";
            this.RightyRadioButton.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(39, 155);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(48, 17);
            this.radioButton2.TabIndex = 25;
            this.radioButton2.Text = "Lefty";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // SimMove
            // 
            this.SimMove.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SimMove.Location = new System.Drawing.Point(1202, 317);
            this.SimMove.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SimMove.Name = "SimMove";
            this.SimMove.Size = new System.Drawing.Size(148, 33);
            this.SimMove.TabIndex = 27;
            this.SimMove.Text = "Simulate Move";
            this.SimMove.UseVisualStyleBackColor = true;
            this.SimMove.Click += new System.EventHandler(this.SimMove_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(28, 99);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Elbow Angle (rad):";
            // 
            // Teachbutton
            // 
            this.Teachbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Teachbutton.Location = new System.Drawing.Point(819, 125);
            this.Teachbutton.Name = "Teachbutton";
            this.Teachbutton.Size = new System.Drawing.Size(129, 33);
            this.Teachbutton.TabIndex = 31;
            this.Teachbutton.Text = "Teach Path";
            this.Teachbutton.UseVisualStyleBackColor = true;
            this.Teachbutton.Click += new System.EventHandler(this.Teachbutton_Click);
            // 
            // MoveNumber
            // 
            this.MoveNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MoveNumber.Location = new System.Drawing.Point(1033, 127);
            this.MoveNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MoveNumber.Name = "MoveNumber";
            this.MoveNumber.Size = new System.Drawing.Size(56, 26);
            this.MoveNumber.TabIndex = 33;
            this.MoveNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MoveNumber.ValueChanged += new System.EventHandler(this.MoveNumber_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(972, 127);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 20);
            this.label10.TabIndex = 34;
            this.label10.Text = "Path #";
            // 
            // Connectbutton
            // 
            this.Connectbutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Connectbutton.Location = new System.Drawing.Point(1149, 11);
            this.Connectbutton.Name = "Connectbutton";
            this.Connectbutton.Size = new System.Drawing.Size(201, 41);
            this.Connectbutton.TabIndex = 36;
            this.Connectbutton.Text = "Connect to PMD Device";
            this.Connectbutton.UseVisualStyleBackColor = true;
            this.Connectbutton.Click += new System.EventHandler(this.Connectbutton_Click);
            // 
            // listView1
            // 
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(794, 262);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(383, 449);
            this.listView1.TabIndex = 38;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(806, 216);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(300, 20);
            this.label11.TabIndex = 39;
            this.label11.Text = "Time between position vectors (seconds):";
            // 
            // DeltaTime
            // 
            this.DeltaTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeltaTime.Location = new System.Drawing.Point(1112, 213);
            this.DeltaTime.Name = "DeltaTime";
            this.DeltaTime.Size = new System.Drawing.Size(65, 26);
            this.DeltaTime.TabIndex = 40;
            this.DeltaTime.Text = "1.0";
            // 
            // ClearPath
            // 
            this.ClearPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearPath.Location = new System.Drawing.Point(819, 164);
            this.ClearPath.Name = "ClearPath";
            this.ClearPath.Size = new System.Drawing.Size(129, 33);
            this.ClearPath.TabIndex = 45;
            this.ClearPath.Text = "Clear Path";
            this.ClearPath.UseVisualStyleBackColor = true;
            this.ClearPath.Click += new System.EventHandler(this.ClearPath_Click);
            // 
            // SourceScalarBox
            // 
            this.SourceScalarBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SourceScalarBox.Location = new System.Drawing.Point(1297, 410);
            this.SourceScalarBox.MinimumSize = new System.Drawing.Size(4, 30);
            this.SourceScalarBox.Name = "SourceScalarBox";
            this.SourceScalarBox.Size = new System.Drawing.Size(55, 26);
            this.SourceScalarBox.TabIndex = 46;
            this.SourceScalarBox.Text = "1.0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1199, 413);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 20);
            this.label3.TabIndex = 47;
            this.label3.Text = "Time Scalar";
            // 
            // SCARAConfigDlg_button
            // 
            this.SCARAConfigDlg_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SCARAConfigDlg_button.Location = new System.Drawing.Point(819, 11);
            this.SCARAConfigDlg_button.Name = "SCARAConfigDlg_button";
            this.SCARAConfigDlg_button.Size = new System.Drawing.Size(192, 45);
            this.SCARAConfigDlg_button.TabIndex = 48;
            this.SCARAConfigDlg_button.Text = "SCARA Configuration";
            this.SCARAConfigDlg_button.UseVisualStyleBackColor = true;
            this.SCARAConfigDlg_button.Click += new System.EventHandler(this.SCARAConfigDlg_button_Click);
            // 
            // MoveDebug
            // 
            this.MoveDebug.Location = new System.Drawing.Point(1213, 469);
            this.MoveDebug.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MoveDebug.Name = "MoveDebug";
            this.MoveDebug.Size = new System.Drawing.Size(160, 41);
            this.MoveDebug.TabIndex = 49;
            this.MoveDebug.Text = "button1";
            this.MoveDebug.UseVisualStyleBackColor = true;
            this.MoveDebug.Click += new System.EventHandler(this.MoveDebug_Click);
            // 
            // Reset
            // 
            this.Reset.Location = new System.Drawing.Point(1278, 134);
            this.Reset.Name = "Reset";
            this.Reset.Size = new System.Drawing.Size(94, 38);
            this.Reset.TabIndex = 50;
            this.Reset.Text = "Reset";
            this.Reset.UseVisualStyleBackColor = true;
            this.Reset.Click += new System.EventHandler(this.Reset_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1401, 658);
            this.Controls.Add(this.Reset);
            this.Controls.Add(this.MoveDebug);
            this.Controls.Add(this.SCARAConfigDlg_button);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SourceScalarBox);
            this.Controls.Add(this.ClearPath);
            this.Controls.Add(this.DeltaTime);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.Connectbutton);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.MoveNumber);
            this.Controls.Add(this.Teachbutton);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.SimMove);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.RightyRadioButton);
            this.Controls.Add(this.Theta_textBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.Phi_textBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Y_textBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.X_textBox);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.Disablebutton);
            this.Controls.Add(this.Stopbutton);
            this.Controls.Add(this.MoveButton);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "SCARA Example";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MoveNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button MoveButton;
        private System.Windows.Forms.Button Stopbutton;
        private System.Windows.Forms.Button Disablebutton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox X_textBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox Y_textBox;
        private System.Windows.Forms.TextBox Theta_textBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox Phi_textBox;
        private System.Windows.Forms.RadioButton RightyRadioButton;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Button SimMove;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button Teachbutton;
        private System.Windows.Forms.NumericUpDown MoveNumber;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button Connectbutton;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox DeltaTime;
        private System.Windows.Forms.Button ClearPath;
        private System.Windows.Forms.TextBox SourceScalarBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button SCARAConfigDlg_button;
        private System.Windows.Forms.Button MoveDebug;
        private System.Windows.Forms.Button Reset;
        //       private System.Windows.Forms.Button SCARAConfigDlg_button;
    }
}

