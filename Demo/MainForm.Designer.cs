namespace Demo
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
         this.btnSave = new System.Windows.Forms.Button();
         this.pictureBoxDisplay = new System.Windows.Forms.PictureBox();
         this.comboBoxCameras = new System.Windows.Forms.ComboBox();
         this.btnConfig = new System.Windows.Forms.Button();
         this.btnStop = new System.Windows.Forms.Button();
         this.btnStart = new System.Windows.Forms.Button();
         this.cameraPropertyValue = new System.Windows.Forms.ComboBox();
         this.cameraPropertyTitle = new System.Windows.Forms.Label();
         this.label1 = new System.Windows.Forms.Label();
         this.cameraPropertyRangeTitle = new System.Windows.Forms.Label();
         this.cameraPropertyRangeValue = new System.Windows.Forms.Label();
         this.cameraPropertyValueTypeSelection = new System.Windows.Forms.ComboBox();
         this.cameraPropertyValueValue = new System.Windows.Forms.NumericUpDown();
         this.cameraPropertyValueAuto = new System.Windows.Forms.CheckBox();
         ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDisplay)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.cameraPropertyValueValue)).BeginInit();
         this.SuspendLayout();
         // 
         // btnSave
         // 
         this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.btnSave.Location = new System.Drawing.Point(516, 515);
         this.btnSave.Name = "btnSave";
         this.btnSave.Size = new System.Drawing.Size(87, 23);
         this.btnSave.TabIndex = 14;
         this.btnSave.Text = "Save current";
         this.btnSave.UseVisualStyleBackColor = true;
         this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
         // 
         // pictureBoxDisplay
         // 
         this.pictureBoxDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.pictureBoxDisplay.Location = new System.Drawing.Point(12, 12);
         this.pictureBoxDisplay.Name = "pictureBoxDisplay";
         this.pictureBoxDisplay.Size = new System.Drawing.Size(684, 497);
         this.pictureBoxDisplay.TabIndex = 13;
         this.pictureBoxDisplay.TabStop = false;
         // 
         // comboBoxCameras
         // 
         this.comboBoxCameras.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.comboBoxCameras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.comboBoxCameras.FormattingEnabled = true;
         this.comboBoxCameras.Location = new System.Drawing.Point(185, 515);
         this.comboBoxCameras.Name = "comboBoxCameras";
         this.comboBoxCameras.Size = new System.Drawing.Size(153, 21);
         this.comboBoxCameras.TabIndex = 12;
         this.comboBoxCameras.SelectedIndexChanged += new System.EventHandler(this.comboBoxCameras_SelectedIndexChanged);
         // 
         // btnConfig
         // 
         this.btnConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.btnConfig.Location = new System.Drawing.Point(609, 515);
         this.btnConfig.Name = "btnConfig";
         this.btnConfig.Size = new System.Drawing.Size(87, 23);
         this.btnConfig.TabIndex = 10;
         this.btnConfig.Text = "Configuration";
         this.btnConfig.UseVisualStyleBackColor = true;
         this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
         // 
         // btnStop
         // 
         this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.btnStop.Location = new System.Drawing.Point(93, 515);
         this.btnStop.Name = "btnStop";
         this.btnStop.Size = new System.Drawing.Size(75, 23);
         this.btnStop.TabIndex = 9;
         this.btnStop.Text = "Stop";
         this.btnStop.UseVisualStyleBackColor = true;
         this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
         // 
         // btnStart
         // 
         this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.btnStart.Location = new System.Drawing.Point(12, 515);
         this.btnStart.Name = "btnStart";
         this.btnStart.Size = new System.Drawing.Size(75, 23);
         this.btnStart.TabIndex = 8;
         this.btnStart.Text = "Start";
         this.btnStart.UseVisualStyleBackColor = true;
         this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
         // 
         // cameraPropertyValue
         // 
         this.cameraPropertyValue.FormattingEnabled = true;
         this.cameraPropertyValue.Location = new System.Drawing.Point(64, 542);
         this.cameraPropertyValue.Name = "cameraPropertyValue";
         this.cameraPropertyValue.Size = new System.Drawing.Size(156, 21);
         this.cameraPropertyValue.TabIndex = 15;
         this.cameraPropertyValue.SelectedIndexChanged += new System.EventHandler(this.cameraPropertyValue_SelectedIndexChanged);
         // 
         // cameraPropertyTitle
         // 
         this.cameraPropertyTitle.AutoSize = true;
         this.cameraPropertyTitle.Location = new System.Drawing.Point(12, 545);
         this.cameraPropertyTitle.Name = "cameraPropertyTitle";
         this.cameraPropertyTitle.Size = new System.Drawing.Size(46, 13);
         this.cameraPropertyTitle.TabIndex = 16;
         this.cameraPropertyTitle.Text = "Property";
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(417, 545);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(34, 13);
         this.label1.TabIndex = 17;
         this.label1.Text = "Value";
         // 
         // cameraPropertyRangeTitle
         // 
         this.cameraPropertyRangeTitle.AutoSize = true;
         this.cameraPropertyRangeTitle.Location = new System.Drawing.Point(299, 545);
         this.cameraPropertyRangeTitle.Name = "cameraPropertyRangeTitle";
         this.cameraPropertyRangeTitle.Size = new System.Drawing.Size(39, 13);
         this.cameraPropertyRangeTitle.TabIndex = 18;
         this.cameraPropertyRangeTitle.Text = "Range";
         // 
         // cameraPropertyRangeValue
         // 
         this.cameraPropertyRangeValue.AutoSize = true;
         this.cameraPropertyRangeValue.Location = new System.Drawing.Point(344, 545);
         this.cameraPropertyRangeValue.Name = "cameraPropertyRangeValue";
         this.cameraPropertyRangeValue.Size = new System.Drawing.Size(45, 13);
         this.cameraPropertyRangeValue.TabIndex = 19;
         this.cameraPropertyRangeValue.Text = "<value>";
         // 
         // cameraPropertyValueTypeSelection
         // 
         this.cameraPropertyValueTypeSelection.Enabled = false;
         this.cameraPropertyValueTypeSelection.FormattingEnabled = true;
         this.cameraPropertyValueTypeSelection.Items.AddRange(new object[] {
            "Value",
            "Percentage"});
         this.cameraPropertyValueTypeSelection.Location = new System.Drawing.Point(457, 542);
         this.cameraPropertyValueTypeSelection.Name = "cameraPropertyValueTypeSelection";
         this.cameraPropertyValueTypeSelection.Size = new System.Drawing.Size(55, 21);
         this.cameraPropertyValueTypeSelection.TabIndex = 20;
         this.cameraPropertyValueTypeSelection.SelectedIndexChanged += new System.EventHandler(this.cameraPropertyValueTypeSelection_SelectedIndexChanged);
         // 
         // cameraPropertyValueValue
         // 
         this.cameraPropertyValueValue.Enabled = false;
         this.cameraPropertyValueValue.Location = new System.Drawing.Point(518, 543);
         this.cameraPropertyValueValue.Name = "cameraPropertyValueValue";
         this.cameraPropertyValueValue.Size = new System.Drawing.Size(61, 20);
         this.cameraPropertyValueValue.TabIndex = 21;
         this.cameraPropertyValueValue.ValueChanged += new System.EventHandler(this.cameraPropertyValueValue_ValueChanged);
         this.cameraPropertyValueValue.EnabledChanged += new System.EventHandler(this.cameraPropertyValueValue_EnabledChanged);
         // 
         // cameraPropertyValueAuto
         // 
         this.cameraPropertyValueAuto.AutoSize = true;
         this.cameraPropertyValueAuto.Enabled = false;
         this.cameraPropertyValueAuto.Location = new System.Drawing.Point(585, 544);
         this.cameraPropertyValueAuto.Name = "cameraPropertyValueAuto";
         this.cameraPropertyValueAuto.Size = new System.Drawing.Size(54, 17);
         this.cameraPropertyValueAuto.TabIndex = 22;
         this.cameraPropertyValueAuto.Text = "Auto?";
         this.cameraPropertyValueAuto.UseVisualStyleBackColor = true;
         this.cameraPropertyValueAuto.CheckedChanged += new System.EventHandler(this.cameraPropertyValueAuto_CheckedChanged);
         // 
         // MainForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(708, 590);
         this.Controls.Add(this.cameraPropertyValueAuto);
         this.Controls.Add(this.cameraPropertyValueValue);
         this.Controls.Add(this.cameraPropertyValueTypeSelection);
         this.Controls.Add(this.cameraPropertyRangeValue);
         this.Controls.Add(this.cameraPropertyRangeTitle);
         this.Controls.Add(this.label1);
         this.Controls.Add(this.cameraPropertyTitle);
         this.Controls.Add(this.cameraPropertyValue);
         this.Controls.Add(this.btnSave);
         this.Controls.Add(this.pictureBoxDisplay);
         this.Controls.Add(this.comboBoxCameras);
         this.Controls.Add(this.btnConfig);
         this.Controls.Add(this.btnStop);
         this.Controls.Add(this.btnStart);
         this.MinimumSize = new System.Drawing.Size(640, 520);
         this.Name = "MainForm";
         this.Text = "WebCam Demo";
         this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
         this.Load += new System.EventHandler(this.MainForm_Load);
         ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDisplay)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.cameraPropertyValueValue)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.PictureBox pictureBoxDisplay;
        private System.Windows.Forms.ComboBox comboBoxCameras;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ComboBox cameraPropertyValue;
        private System.Windows.Forms.Label cameraPropertyTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label cameraPropertyRangeTitle;
        private System.Windows.Forms.Label cameraPropertyRangeValue;
        private System.Windows.Forms.ComboBox cameraPropertyValueTypeSelection;
        private System.Windows.Forms.NumericUpDown cameraPropertyValueValue;
        private System.Windows.Forms.CheckBox cameraPropertyValueAuto;
    }
}

