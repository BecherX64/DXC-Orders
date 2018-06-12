namespace DemandTrackerv1._1
{
	partial class PrimaryForm1
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
			this.btnLoadData = new System.Windows.Forms.Button();
			this.btnAddRecord = new System.Windows.Forms.Button();
			this.btnModifyRecord = new System.Windows.Forms.Button();
			this.dgwPrimary = new System.Windows.Forms.DataGridView();
			this.dgwSecondary = new System.Windows.Forms.DataGridView();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnSaveRecordToDB = new System.Windows.Forms.Button();
			this.btnReloadData = new System.Windows.Forms.Button();
			this.btnCancelModify = new System.Windows.Forms.Button();
			this.dgwAddRecord = new System.Windows.Forms.DataGridView();
			this.btnAddNewRecord = new System.Windows.Forms.Button();
			this.btnCancelAddNewRecord = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dgwPrimary)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgwSecondary)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dgwAddRecord)).BeginInit();
			this.SuspendLayout();
			// 
			// btnLoadData
			// 
			this.btnLoadData.Location = new System.Drawing.Point(13, 13);
			this.btnLoadData.Name = "btnLoadData";
			this.btnLoadData.Size = new System.Drawing.Size(87, 23);
			this.btnLoadData.TabIndex = 0;
			this.btnLoadData.Text = "LoadData";
			this.btnLoadData.UseVisualStyleBackColor = true;
			this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
			// 
			// btnAddRecord
			// 
			this.btnAddRecord.Location = new System.Drawing.Point(106, 13);
			this.btnAddRecord.Name = "btnAddRecord";
			this.btnAddRecord.Size = new System.Drawing.Size(89, 23);
			this.btnAddRecord.TabIndex = 1;
			this.btnAddRecord.Text = "AddRecord";
			this.btnAddRecord.UseVisualStyleBackColor = true;
			this.btnAddRecord.Click += new System.EventHandler(this.btnAddRecord_Click);
			// 
			// btnModifyRecord
			// 
			this.btnModifyRecord.Location = new System.Drawing.Point(201, 13);
			this.btnModifyRecord.Name = "btnModifyRecord";
			this.btnModifyRecord.Size = new System.Drawing.Size(89, 23);
			this.btnModifyRecord.TabIndex = 2;
			this.btnModifyRecord.Text = "ModifyRecord";
			this.btnModifyRecord.UseVisualStyleBackColor = true;
			this.btnModifyRecord.Click += new System.EventHandler(this.btnModifyRecord_Click);
			// 
			// dgwPrimary
			// 
			this.dgwPrimary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgwPrimary.Location = new System.Drawing.Point(13, 43);
			this.dgwPrimary.Name = "dgwPrimary";
			this.dgwPrimary.Size = new System.Drawing.Size(775, 370);
			this.dgwPrimary.TabIndex = 3;
			this.dgwPrimary.Paint += new System.Windows.Forms.PaintEventHandler(this.dgwPrimary_Paint);
			this.dgwPrimary.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgwPrimary_MouseClick);
			// 
			// dgwSecondary
			// 
			this.dgwSecondary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgwSecondary.Location = new System.Drawing.Point(13, 464);
			this.dgwSecondary.Name = "dgwSecondary";
			this.dgwSecondary.Size = new System.Drawing.Size(775, 62);
			this.dgwSecondary.TabIndex = 4;
			this.dgwSecondary.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgwSecondary_CellBeginEdit);
			this.dgwSecondary.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwSecondary_CellEndEdit);
			this.dgwSecondary.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwSecondary_CellValueChanged);
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(13, 544);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(775, 118);
			this.richTextBox1.TabIndex = 5;
			this.richTextBox1.Text = "";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 665);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "label1";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 528);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "label2";
			// 
			// btnSaveRecordToDB
			// 
			this.btnSaveRecordToDB.Location = new System.Drawing.Point(13, 435);
			this.btnSaveRecordToDB.Name = "btnSaveRecordToDB";
			this.btnSaveRecordToDB.Size = new System.Drawing.Size(111, 23);
			this.btnSaveRecordToDB.TabIndex = 8;
			this.btnSaveRecordToDB.Text = "Save Record";
			this.btnSaveRecordToDB.UseVisualStyleBackColor = true;
			this.btnSaveRecordToDB.Click += new System.EventHandler(this.btnSaveRecordToDB_Click);
			// 
			// btnReloadData
			// 
			this.btnReloadData.Location = new System.Drawing.Point(130, 435);
			this.btnReloadData.Name = "btnReloadData";
			this.btnReloadData.Size = new System.Drawing.Size(111, 23);
			this.btnReloadData.TabIndex = 9;
			this.btnReloadData.Text = "Reload Data";
			this.btnReloadData.UseVisualStyleBackColor = true;
			this.btnReloadData.Click += new System.EventHandler(this.btnReloadData_Click);
			// 
			// btnCancelModify
			// 
			this.btnCancelModify.Location = new System.Drawing.Point(247, 435);
			this.btnCancelModify.Name = "btnCancelModify";
			this.btnCancelModify.Size = new System.Drawing.Size(111, 23);
			this.btnCancelModify.TabIndex = 10;
			this.btnCancelModify.Text = "Cancel";
			this.btnCancelModify.UseVisualStyleBackColor = true;
			this.btnCancelModify.Click += new System.EventHandler(this.btnCancelModify_Click);
			// 
			// dgwAddRecord
			// 
			this.dgwAddRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgwAddRecord.Location = this.dgwSecondary.Location;
			this.dgwAddRecord.Name = "dgwAddRecord";
			this.dgwAddRecord.Size = this.dgwSecondary.Size;
			this.dgwAddRecord.TabIndex = 11;
			this.dgwAddRecord.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwAddRecord_CellValueChanged);
			// 
			// btnAddNewRecord
			// 
			this.btnAddNewRecord.Location = this.btnModifyRecord.Location;
			this.btnAddNewRecord.Name = "btnAddNewRecord";
			this.btnAddNewRecord.Size = new System.Drawing.Size(111, 23);
			this.btnAddNewRecord.TabIndex = 12;
			this.btnAddNewRecord.Text = "Add New Record";
			this.btnAddNewRecord.UseVisualStyleBackColor = true;
			this.btnAddNewRecord.Click += new System.EventHandler(this.btnAddNewRecord_Click);
			// 
			// btnCancelAddNewRecord
			// 
			this.btnCancelAddNewRecord.Location = this.btnCancelModify.Location;
			this.btnCancelAddNewRecord.Name = "btnCancelAddNewRecord";
			this.btnCancelAddNewRecord.Size = new System.Drawing.Size(111, 23);
			this.btnCancelAddNewRecord.TabIndex = 13;
			this.btnCancelAddNewRecord.Text = "Cancel";
			this.btnCancelAddNewRecord.UseVisualStyleBackColor = true;
			this.btnCancelAddNewRecord.Click += new System.EventHandler(this.btnCancelAddNewRecord_Click);
			// 
			// PrimaryForm1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(896, 726);
			this.Controls.Add(this.btnCancelAddNewRecord);
			this.Controls.Add(this.btnAddNewRecord);
			this.Controls.Add(this.dgwAddRecord);
			this.Controls.Add(this.btnCancelModify);
			this.Controls.Add(this.btnReloadData);
			this.Controls.Add(this.btnSaveRecordToDB);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.dgwSecondary);
			this.Controls.Add(this.dgwPrimary);
			this.Controls.Add(this.btnModifyRecord);
			this.Controls.Add(this.btnAddRecord);
			this.Controls.Add(this.btnLoadData);
			this.Name = "PrimaryForm1";
			this.Text = "DemandTracker v1.1";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PrimaryForm1_FormClosing);
			this.Shown += new System.EventHandler(this.PrimaryForm1_Shown);
			this.SizeChanged += new System.EventHandler(this.PrimaryForm1_SizeChanged);
			((System.ComponentModel.ISupportInitialize)(this.dgwPrimary)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgwSecondary)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dgwAddRecord)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnLoadData;
		private System.Windows.Forms.Button btnAddRecord;
		private System.Windows.Forms.Button btnModifyRecord;
		private System.Windows.Forms.DataGridView dgwPrimary;
		private System.Windows.Forms.DataGridView dgwSecondary;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnSaveRecordToDB;
		private System.Windows.Forms.Button btnReloadData;
		private System.Windows.Forms.Button btnCancelModify;
		private System.Windows.Forms.DataGridView dgwAddRecord;
		private System.Windows.Forms.Button btnAddNewRecord;
		private System.Windows.Forms.Button btnCancelAddNewRecord;
	}
}

