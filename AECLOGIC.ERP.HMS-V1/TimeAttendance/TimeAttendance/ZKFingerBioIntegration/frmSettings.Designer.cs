namespace ZKFingerBioIntegration
{
    partial class frmSettings
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.dgvSettings = new System.Windows.Forms.DataGridView();
            this.setIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mach_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iPAddDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.machPortDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mach_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remarksDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dsSettingsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsSettings = new ZKFingerBioIntegration.dsSettings();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkPrev = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvAttLog = new System.Windows.Forms.DataGridView();
            this.dgvDetails = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsStatusLable = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsStatusBar = new System.Windows.Forms.ToolStripProgressBar();
            this.lblConnect = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.txtNameApp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSet = new System.Windows.Forms.Button();
            this.btnGetData = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSettingsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSettings)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvSettings
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            this.dgvSettings.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSettings.AutoGenerateColumns = false;
            this.dgvSettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSettings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.setIDDataGridViewTextBoxColumn,
            this.Mach_Name,
            this.iPAddDataGridViewTextBoxColumn,
            this.machPortDataGridViewTextBoxColumn,
            this.Mach_No,
            this.colSatus,
            this.remarksDataGridViewTextBoxColumn});
            this.dgvSettings.DataMember = "dtSettings";
            this.dgvSettings.DataSource = this.dsSettingsBindingSource;
            this.dgvSettings.Location = new System.Drawing.Point(3, 65);
            this.dgvSettings.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvSettings.Name = "dgvSettings";
            this.dgvSettings.RowTemplate.Height = 24;
            this.dgvSettings.Size = new System.Drawing.Size(909, 207);
            this.dgvSettings.TabIndex = 6;
            this.dgvSettings.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvSettings_DataError);
            // 
            // setIDDataGridViewTextBoxColumn
            // 
            this.setIDDataGridViewTextBoxColumn.DataPropertyName = "SetID";
            this.setIDDataGridViewTextBoxColumn.HeaderText = "SetID";
            this.setIDDataGridViewTextBoxColumn.Name = "setIDDataGridViewTextBoxColumn";
            this.setIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.setIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // Mach_Name
            // 
            this.Mach_Name.DataPropertyName = "Mach_Name";
            this.Mach_Name.HeaderText = "Mach Name";
            this.Mach_Name.Name = "Mach_Name";
            this.Mach_Name.Width = 180;
            // 
            // iPAddDataGridViewTextBoxColumn
            // 
            this.iPAddDataGridViewTextBoxColumn.DataPropertyName = "IP_Add";
            this.iPAddDataGridViewTextBoxColumn.HeaderText = "IP Address";
            this.iPAddDataGridViewTextBoxColumn.Name = "iPAddDataGridViewTextBoxColumn";
            this.iPAddDataGridViewTextBoxColumn.Width = 180;
            // 
            // machPortDataGridViewTextBoxColumn
            // 
            this.machPortDataGridViewTextBoxColumn.DataPropertyName = "Mach_Port";
            this.machPortDataGridViewTextBoxColumn.HeaderText = "Port";
            this.machPortDataGridViewTextBoxColumn.Name = "machPortDataGridViewTextBoxColumn";
            this.machPortDataGridViewTextBoxColumn.Width = 80;
            // 
            // Mach_No
            // 
            this.Mach_No.DataPropertyName = "Mach_No";
            this.Mach_No.HeaderText = "Mach No";
            this.Mach_No.Name = "Mach_No";
            this.Mach_No.Width = 80;
            // 
            // colSatus
            // 
            this.colSatus.HeaderText = "Status";
            this.colSatus.Name = "colSatus";
            this.colSatus.ReadOnly = true;
            this.colSatus.Width = 140;
            // 
            // remarksDataGridViewTextBoxColumn
            // 
            this.remarksDataGridViewTextBoxColumn.DataPropertyName = "Remarks";
            this.remarksDataGridViewTextBoxColumn.HeaderText = "Remarks";
            this.remarksDataGridViewTextBoxColumn.Name = "remarksDataGridViewTextBoxColumn";
            this.remarksDataGridViewTextBoxColumn.Width = 200;
            // 
            // dsSettingsBindingSource
            // 
            this.dsSettingsBindingSource.DataSource = this.dsSettings;
            this.dsSettingsBindingSource.Position = 0;
            // 
            // dsSettings
            // 
            this.dsSettings.DataSetName = "dsSettings";
            this.dsSettings.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(31, 279);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.chkPrev);
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Controls.Add(this.lblConnect);
            this.panel1.Controls.Add(this.btnConnect);
            this.panel1.Controls.Add(this.txtURL);
            this.panel1.Controls.Add(this.txtNameApp);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnSet);
            this.panel1.Controls.Add(this.btnGetData);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.dgvSettings);
            this.panel1.Location = new System.Drawing.Point(11, 10);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(919, 602);
            this.panel1.TabIndex = 7;
            // 
            // chkPrev
            // 
            this.chkPrev.AutoSize = true;
            this.chkPrev.Location = new System.Drawing.Point(348, 282);
            this.chkPrev.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkPrev.Name = "chkPrev";
            this.chkPrev.Size = new System.Drawing.Size(154, 21);
            this.chkPrev.TabIndex = 18;
            this.chkPrev.Text = "Last Month 10 days";
            this.chkPrev.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitContainer1.Location = new System.Drawing.Point(0, 304);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvAttLog);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dgvDetails);
            this.splitContainer1.Size = new System.Drawing.Size(915, 265);
            this.splitContainer1.SplitterDistance = 438;
            this.splitContainer1.TabIndex = 17;
            // 
            // dgvAttLog
            // 
            this.dgvAttLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAttLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAttLog.Location = new System.Drawing.Point(0, 0);
            this.dgvAttLog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvAttLog.Name = "dgvAttLog";
            this.dgvAttLog.Size = new System.Drawing.Size(438, 265);
            this.dgvAttLog.TabIndex = 6;
            // 
            // dgvDetails
            // 
            this.dgvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDetails.Location = new System.Drawing.Point(0, 0);
            this.dgvDetails.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvDetails.Name = "dgvDetails";
            this.dgvDetails.RowTemplate.Height = 24;
            this.dgvDetails.Size = new System.Drawing.Size(473, 265);
            this.dgvDetails.TabIndex = 16;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsStatusLable,
            this.tsStatusBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 569);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 13, 0);
            this.statusStrip1.Size = new System.Drawing.Size(915, 29);
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsStatusLable
            // 
            this.tsStatusLable.Name = "tsStatusLable";
            this.tsStatusLable.Size = new System.Drawing.Size(151, 24);
            this.tsStatusLable.Text = "toolStripStatusLabel1";
            // 
            // tsStatusBar
            // 
            this.tsStatusBar.Name = "tsStatusBar";
            this.tsStatusBar.Size = new System.Drawing.Size(100, 23);
            // 
            // lblConnect
            // 
            this.lblConnect.AutoSize = true;
            this.lblConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnect.Location = new System.Drawing.Point(477, 41);
            this.lblConnect.Name = "lblConnect";
            this.lblConnect.Size = new System.Drawing.Size(0, 17);
            this.lblConnect.TabIndex = 14;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(391, 38);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(80, 23);
            this.btnConnect.TabIndex = 13;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(91, 37);
            this.txtURL.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(295, 22);
            this.txtURL.TabIndex = 12;
            // 
            // txtNameApp
            // 
            this.txtNameApp.Location = new System.Drawing.Point(91, 11);
            this.txtNameApp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNameApp.Name = "txtNameApp";
            this.txtNameApp.Size = new System.Drawing.Size(295, 22);
            this.txtNameApp.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "URL";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Name";
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(212, 279);
            this.btnSet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(80, 23);
            this.btnSet.TabIndex = 8;
            this.btnSet.Tag = "1";
            this.btnSet.Text = "Set";
            this.toolTip1.SetToolTip(this.btnSet, "Set to AEC ERP");
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // btnGetData
            // 
            this.btnGetData.Location = new System.Drawing.Point(121, 279);
            this.btnGetData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(80, 23);
            this.btnGetData.TabIndex = 7;
            this.btnGetData.Tag = "0";
            this.btnGetData.Text = "Get";
            this.toolTip1.SetToolTip(this.btnGetData, "Get from Bio Metric Machine");
            this.btnGetData.UseVisualStyleBackColor = true;
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 600000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 624);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmSettings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSettingsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsSettings)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSettings;
        //private System.Windows.Forms.DataGridView dgvAttLog;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.BindingSource dsSettingsBindingSource;
        private dsSettings dsSettings;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnGetData;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.TextBox txtNameApp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblConnect;
        private System.Windows.Forms.DataGridViewTextBoxColumn setIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mach_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn iPAddDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn machPortDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mach_No;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn remarksDataGridViewTextBoxColumn;
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsStatusLable;
        private System.Windows.Forms.ToolStripProgressBar tsStatusBar;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvDetails;
        private System.Windows.Forms.DataGridView dgvAttLog;
        private System.Windows.Forms.CheckBox chkPrev;
    }
}