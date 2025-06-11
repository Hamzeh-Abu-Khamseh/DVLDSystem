namespace DVLDSystem
{
    partial class ctrlScheduleTests
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
            this.gbTestType = new System.Windows.Forms.GroupBox();
            this.pbTestTypeImage = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblUserMessage = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.gbRetakeTestInfo = new System.Windows.Forms.GroupBox();
            this.lblTotalFees = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblRetakeTestAppID = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblRetakeTestFees = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblFees = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.lblTrial = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblFullName = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblDrivingClass = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblDrivingLicenseApplicationID = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.gbTestType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTestTypeImage)).BeginInit();
            this.gbRetakeTestInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbTestType
            // 
            this.gbTestType.Controls.Add(this.pbTestTypeImage);
            this.gbTestType.Controls.Add(this.lblTitle);
            this.gbTestType.Controls.Add(this.lblUserMessage);
            this.gbTestType.Controls.Add(this.btnSave);
            this.gbTestType.Controls.Add(this.gbRetakeTestInfo);
            this.gbTestType.Controls.Add(this.dtpDate);
            this.gbTestType.Controls.Add(this.lblFees);
            this.gbTestType.Controls.Add(this.label1);
            this.gbTestType.Controls.Add(this.label24);
            this.gbTestType.Controls.Add(this.lblTrial);
            this.gbTestType.Controls.Add(this.label13);
            this.gbTestType.Controls.Add(this.lblFullName);
            this.gbTestType.Controls.Add(this.label11);
            this.gbTestType.Controls.Add(this.lblDrivingClass);
            this.gbTestType.Controls.Add(this.label9);
            this.gbTestType.Controls.Add(this.lblDrivingLicenseApplicationID);
            this.gbTestType.Controls.Add(this.label7);
            this.gbTestType.Location = new System.Drawing.Point(3, 0);
            this.gbTestType.Name = "gbTestType";
            this.gbTestType.Size = new System.Drawing.Size(515, 562);
            this.gbTestType.TabIndex = 0;
            this.gbTestType.TabStop = false;
            this.gbTestType.Text = "Test Type:";
            // 
            // pbTestTypeImage
            // 
            this.pbTestTypeImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbTestTypeImage.Image = global::DVLDSystem.Properties.Resources.driving_test_512;
            this.pbTestTypeImage.InitialImage = null;
            this.pbTestTypeImage.Location = new System.Drawing.Point(191, 8);
            this.pbTestTypeImage.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.pbTestTypeImage.Name = "pbTestTypeImage";
            this.pbTestTypeImage.Size = new System.Drawing.Size(139, 126);
            this.pbTestTypeImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbTestTypeImage.TabIndex = 191;
            this.pbTestTypeImage.TabStop = false;            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(82, 125);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(354, 60);
            this.lblTitle.TabIndex = 190;
            this.lblTitle.Text = "Schedule Test";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUserMessage
            // 
            this.lblUserMessage.AutoSize = true;
            this.lblUserMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserMessage.ForeColor = System.Drawing.Color.Red;
            this.lblUserMessage.Location = new System.Drawing.Point(50, 185);
            this.lblUserMessage.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblUserMessage.Name = "lblUserMessage";
            this.lblUserMessage.Size = new System.Drawing.Size(432, 20);
            this.lblUserMessage.TabIndex = 189;
            this.lblUserMessage.Text = "Cannot Sechule, Vision Test Should be Passed First.";
            this.lblUserMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSave
            // 
            this.btnSave.Image = global::DVLDSystem.Properties.Resources.Close_32;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(407, 516);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 40);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gbRetakeTestInfo
            // 
            this.gbRetakeTestInfo.Controls.Add(this.lblTotalFees);
            this.gbRetakeTestInfo.Controls.Add(this.label8);
            this.gbRetakeTestInfo.Controls.Add(this.lblRetakeTestAppID);
            this.gbRetakeTestInfo.Controls.Add(this.label5);
            this.gbRetakeTestInfo.Controls.Add(this.lblRetakeTestFees);
            this.gbRetakeTestInfo.Controls.Add(this.label3);
            this.gbRetakeTestInfo.Location = new System.Drawing.Point(6, 408);
            this.gbRetakeTestInfo.Name = "gbRetakeTestInfo";
            this.gbRetakeTestInfo.Size = new System.Drawing.Size(503, 102);
            this.gbRetakeTestInfo.TabIndex = 1;
            this.gbRetakeTestInfo.TabStop = false;
            this.gbRetakeTestInfo.Text = "Retake Test Information:";
            // 
            // lblTotalFees
            // 
            this.lblTotalFees.AutoSize = true;
            this.lblTotalFees.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalFees.Location = new System.Drawing.Point(228, 27);
            this.lblTotalFees.Name = "lblTotalFees";
            this.lblTotalFees.Size = new System.Drawing.Size(39, 13);
            this.lblTotalFees.TabIndex = 40;
            this.lblTotalFees.Text = "[------]";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(151, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 13);
            this.label8.TabIndex = 39;
            this.label8.Text = "Total Fees:";
            // 
            // lblRetakeTestAppID
            // 
            this.lblRetakeTestAppID.AutoSize = true;
            this.lblRetakeTestAppID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRetakeTestAppID.Location = new System.Drawing.Point(117, 68);
            this.lblRetakeTestAppID.Name = "lblRetakeTestAppID";
            this.lblRetakeTestAppID.Size = new System.Drawing.Size(39, 13);
            this.lblRetakeTestAppID.TabIndex = 38;
            this.lblRetakeTestAppID.Text = "[------]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 37;
            this.label5.Text = "R.Test.App.ID:";
            // 
            // lblRetakeTestFees
            // 
            this.lblRetakeTestFees.AutoSize = true;
            this.lblRetakeTestFees.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRetakeTestFees.Location = new System.Drawing.Point(93, 27);
            this.lblRetakeTestFees.Name = "lblRetakeTestFees";
            this.lblRetakeTestFees.Size = new System.Drawing.Size(39, 13);
            this.lblRetakeTestFees.TabIndex = 36;
            this.lblRetakeTestFees.Text = "[------]";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 35;
            this.label3.Text = "R.App.Fees:";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(89, 337);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(88, 20);
            this.dtpDate.TabIndex = 34;
            // 
            // lblFees
            // 
            this.lblFees.AutoSize = true;
            this.lblFees.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFees.Location = new System.Drawing.Point(86, 370);
            this.lblFees.Name = "lblFees";
            this.lblFees.Size = new System.Drawing.Size(39, 13);
            this.lblFees.TabIndex = 33;
            this.lblFees.Text = "[------]";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 370);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Fees:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(20, 343);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(38, 13);
            this.label24.TabIndex = 30;
            this.label24.Text = "Date:";
            // 
            // lblTrial
            // 
            this.lblTrial.AutoSize = true;
            this.lblTrial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrial.Location = new System.Drawing.Point(86, 311);
            this.lblTrial.Name = "lblTrial";
            this.lblTrial.Size = new System.Drawing.Size(39, 13);
            this.lblTrial.TabIndex = 29;
            this.lblTrial.Text = "[------]";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(20, 311);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(36, 13);
            this.label13.TabIndex = 28;
            this.label13.Text = "Trial:";
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFullName.Location = new System.Drawing.Point(86, 284);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(39, 13);
            this.lblFullName.TabIndex = 27;
            this.lblFullName.Text = "[------]";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(20, 284);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "Name:";
            // 
            // lblDrivingClass
            // 
            this.lblDrivingClass.AutoSize = true;
            this.lblDrivingClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDrivingClass.Location = new System.Drawing.Point(86, 250);
            this.lblDrivingClass.Name = "lblDrivingClass";
            this.lblDrivingClass.Size = new System.Drawing.Size(39, 13);
            this.lblDrivingClass.TabIndex = 25;
            this.lblDrivingClass.Text = "[------]";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(20, 250);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "D.Class:";
            // 
            // lblDrivingLicenseApplicationID
            // 
            this.lblDrivingLicenseApplicationID.AutoSize = true;
            this.lblDrivingLicenseApplicationID.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDrivingLicenseApplicationID.Location = new System.Drawing.Point(86, 214);
            this.lblDrivingLicenseApplicationID.Name = "lblDrivingLicenseApplicationID";
            this.lblDrivingLicenseApplicationID.Size = new System.Drawing.Size(39, 13);
            this.lblDrivingLicenseApplicationID.TabIndex = 23;
            this.lblDrivingLicenseApplicationID.Text = "[------]";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(20, 214);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "ID:";
            // 
            // ctrlScheduleTests
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbTestType);
            this.Name = "ctrlScheduleTests";
            this.Size = new System.Drawing.Size(521, 565);
            this.Load += new System.EventHandler(this.ctrlScheduleTests_Load);
            this.gbTestType.ResumeLayout(false);
            this.gbTestType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTestTypeImage)).EndInit();
            this.gbRetakeTestInfo.ResumeLayout(false);
            this.gbRetakeTestInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.GroupBox gbTestType;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label lblTrial;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblDrivingClass;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblDrivingLicenseApplicationID;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblFees;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.GroupBox gbRetakeTestInfo;
        private System.Windows.Forms.Label lblTotalFees;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblRetakeTestAppID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblRetakeTestFees;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PictureBox pbTestTypeImage;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUserMessage;
    }
}
