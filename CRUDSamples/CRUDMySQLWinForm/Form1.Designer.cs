namespace CRUDMySQLWinForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.groupBoxQuery = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtParentIDQ = new System.Windows.Forms.TextBox();
            this.btnClearQ = new System.Windows.Forms.Button();
            this.txtKeyQ = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.btnQuery = new System.Windows.Forms.Button();
            this.cboOrderBy = new System.Windows.Forms.ComboBox();
            this.txtValueQ = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cboKeyList = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.groupBoxEntry = new System.Windows.Forms.GroupBox();
            this.chkReadonly = new System.Windows.Forms.CheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.lblFUpdateTime = new System.Windows.Forms.Label();
            this.lblFCreateTime = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtFSeqNo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtFKey = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtFValue = new System.Windows.Forms.TextBox();
            this.txtFValueB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFNote = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFParentID = new System.Windows.Forms.TextBox();
            this.txtFID = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelInfo.SuspendLayout();
            this.groupBoxQuery.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxEntry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(30, 33);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panelInfo);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(996, 669);
            this.splitContainer1.SplitterDistance = 535;
            this.splitContainer1.TabIndex = 3;
            // 
            // panelInfo
            // 
            this.panelInfo.Controls.Add(this.groupBoxQuery);
            this.panelInfo.Controls.Add(this.groupBoxEntry);
            this.panelInfo.Location = new System.Drawing.Point(29, 18);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(915, 461);
            this.panelInfo.TabIndex = 49;
            // 
            // groupBoxQuery
            // 
            this.groupBoxQuery.Controls.Add(this.groupBox1);
            this.groupBoxQuery.Controls.Add(this.cboKeyList);
            this.groupBoxQuery.Controls.Add(this.label21);
            this.groupBoxQuery.Location = new System.Drawing.Point(12, 3);
            this.groupBoxQuery.Name = "groupBoxQuery";
            this.groupBoxQuery.Size = new System.Drawing.Size(291, 448);
            this.groupBoxQuery.TabIndex = 0;
            this.groupBoxQuery.TabStop = false;
            this.groupBoxQuery.Text = "Query";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtParentIDQ);
            this.groupBox1.Controls.Add(this.btnClearQ);
            this.groupBox1.Controls.Add(this.txtKeyQ);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.lblCount);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.btnQuery);
            this.groupBox1.Controls.Add(this.cboOrderBy);
            this.groupBox1.Controls.Add(this.txtValueQ);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Location = new System.Drawing.Point(11, 92);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(274, 356);
            this.groupBox1.TabIndex = 50;
            this.groupBox1.TabStop = false;
            // 
            // txtParentIDQ
            // 
            this.txtParentIDQ.Location = new System.Drawing.Point(92, 23);
            this.txtParentIDQ.Name = "txtParentIDQ";
            this.txtParentIDQ.Size = new System.Drawing.Size(107, 29);
            this.txtParentIDQ.TabIndex = 0;
            this.txtParentIDQ.Text = "1234567890";
            // 
            // btnClearQ
            // 
            this.btnClearQ.Location = new System.Drawing.Point(108, 313);
            this.btnClearQ.Name = "btnClearQ";
            this.btnClearQ.Size = new System.Drawing.Size(96, 33);
            this.btnClearQ.TabIndex = 5;
            this.btnClearQ.Text = "Clear";
            this.btnClearQ.UseVisualStyleBackColor = true;
            this.btnClearQ.Click += new System.EventHandler(this.btnClearQ_Click);
            // 
            // txtKeyQ
            // 
            this.txtKeyQ.Location = new System.Drawing.Point(92, 58);
            this.txtKeyQ.Name = "txtKeyQ";
            this.txtKeyQ.Size = new System.Drawing.Size(172, 29);
            this.txtKeyQ.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 18);
            this.label12.TabIndex = 71;
            this.label12.Text = "ParentID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 288);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "Rows:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(3, 61);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(36, 18);
            this.label19.TabIndex = 54;
            this.label19.Text = "Key";
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(64, 288);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(48, 18);
            this.lblCount.TabIndex = 7;
            this.lblCount.Text = "Count";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(3, 96);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(49, 18);
            this.label18.TabIndex = 56;
            this.label18.Text = "Value";
            // 
            // btnQuery
            // 
            this.btnQuery.Location = new System.Drawing.Point(6, 313);
            this.btnQuery.Name = "btnQuery";
            this.btnQuery.Size = new System.Drawing.Size(96, 33);
            this.btnQuery.TabIndex = 4;
            this.btnQuery.Text = "&Query";
            this.btnQuery.UseVisualStyleBackColor = true;
            this.btnQuery.Click += new System.EventHandler(this.btnQuery_Click);
            // 
            // cboOrderBy
            // 
            this.cboOrderBy.FormattingEnabled = true;
            this.cboOrderBy.Location = new System.Drawing.Point(93, 134);
            this.cboOrderBy.Name = "cboOrderBy";
            this.cboOrderBy.Size = new System.Drawing.Size(171, 26);
            this.cboOrderBy.TabIndex = 3;
            // 
            // txtValueQ
            // 
            this.txtValueQ.Location = new System.Drawing.Point(92, 93);
            this.txtValueQ.Name = "txtValueQ";
            this.txtValueQ.Size = new System.Drawing.Size(172, 29);
            this.txtValueQ.TabIndex = 2;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 137);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(72, 18);
            this.label13.TabIndex = 68;
            this.label13.Text = "Order By";
            // 
            // cboKeyList
            // 
            this.cboKeyList.FormattingEnabled = true;
            this.cboKeyList.Location = new System.Drawing.Point(98, 28);
            this.cboKeyList.Name = "cboKeyList";
            this.cboKeyList.Size = new System.Drawing.Size(182, 26);
            this.cboKeyList.TabIndex = 0;
            this.cboKeyList.SelectedIndexChanged += new System.EventHandler(this.cboKeyList_SelectedIndexChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(14, 31);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(59, 18);
            this.label21.TabIndex = 32;
            this.label21.Text = "Parents";
            // 
            // groupBoxEntry
            // 
            this.groupBoxEntry.Controls.Add(this.chkReadonly);
            this.groupBoxEntry.Controls.Add(this.btnClear);
            this.groupBoxEntry.Controls.Add(this.lblFUpdateTime);
            this.groupBoxEntry.Controls.Add(this.lblFCreateTime);
            this.groupBoxEntry.Controls.Add(this.label10);
            this.groupBoxEntry.Controls.Add(this.label11);
            this.groupBoxEntry.Controls.Add(this.txtFSeqNo);
            this.groupBoxEntry.Controls.Add(this.label9);
            this.groupBoxEntry.Controls.Add(this.txtFKey);
            this.groupBoxEntry.Controls.Add(this.label8);
            this.groupBoxEntry.Controls.Add(this.txtFValue);
            this.groupBoxEntry.Controls.Add(this.txtFValueB);
            this.groupBoxEntry.Controls.Add(this.label6);
            this.groupBoxEntry.Controls.Add(this.label4);
            this.groupBoxEntry.Controls.Add(this.txtFNote);
            this.groupBoxEntry.Controls.Add(this.label3);
            this.groupBoxEntry.Controls.Add(this.txtFParentID);
            this.groupBoxEntry.Controls.Add(this.txtFID);
            this.groupBoxEntry.Controls.Add(this.btnDelete);
            this.groupBoxEntry.Controls.Add(this.label2);
            this.groupBoxEntry.Controls.Add(this.btnRead);
            this.groupBoxEntry.Controls.Add(this.btnUpdate);
            this.groupBoxEntry.Controls.Add(this.label5);
            this.groupBoxEntry.Controls.Add(this.btnAdd);
            this.groupBoxEntry.Location = new System.Drawing.Point(319, 3);
            this.groupBoxEntry.Name = "groupBoxEntry";
            this.groupBoxEntry.Size = new System.Drawing.Size(569, 448);
            this.groupBoxEntry.TabIndex = 1;
            this.groupBoxEntry.TabStop = false;
            this.groupBoxEntry.Text = "Entry";
            // 
            // chkReadonly
            // 
            this.chkReadonly.AutoSize = true;
            this.chkReadonly.Location = new System.Drawing.Point(274, 165);
            this.chkReadonly.Name = "chkReadonly";
            this.chkReadonly.Size = new System.Drawing.Size(98, 22);
            this.chkReadonly.TabIndex = 6;
            this.chkReadonly.Text = "Readonly";
            this.chkReadonly.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(417, 404);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(96, 33);
            this.btnClear.TabIndex = 12;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lblFUpdateTime
            // 
            this.lblFUpdateTime.AutoSize = true;
            this.lblFUpdateTime.Location = new System.Drawing.Point(85, 365);
            this.lblFUpdateTime.Name = "lblFUpdateTime";
            this.lblFUpdateTime.Size = new System.Drawing.Size(93, 18);
            this.lblFUpdateTime.TabIndex = 9;
            this.lblFUpdateTime.Text = "UpdateTime";
            // 
            // lblFCreateTime
            // 
            this.lblFCreateTime.AutoSize = true;
            this.lblFCreateTime.Location = new System.Drawing.Point(85, 333);
            this.lblFCreateTime.Name = "lblFCreateTime";
            this.lblFCreateTime.Size = new System.Drawing.Size(90, 18);
            this.lblFCreateTime.TabIndex = 7;
            this.lblFCreateTime.Text = "CreateTime";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 365);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 18);
            this.label10.TabIndex = 69;
            this.label10.Text = "Update:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 333);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 18);
            this.label11.TabIndex = 68;
            this.label11.Text = "Create:";
            // 
            // txtFSeqNo
            // 
            this.txtFSeqNo.Location = new System.Drawing.Point(82, 162);
            this.txtFSeqNo.Name = "txtFSeqNo";
            this.txtFSeqNo.Size = new System.Drawing.Size(183, 29);
            this.txtFSeqNo.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 165);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 18);
            this.label9.TabIndex = 64;
            this.label9.Text = "SeqNo";
            // 
            // txtFKey
            // 
            this.txtFKey.Location = new System.Drawing.Point(82, 57);
            this.txtFKey.Name = "txtFKey";
            this.txtFKey.Size = new System.Drawing.Size(183, 29);
            this.txtFKey.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 196);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 18);
            this.label8.TabIndex = 62;
            this.label8.Text = "Note";
            // 
            // txtFValue
            // 
            this.txtFValue.Location = new System.Drawing.Point(82, 127);
            this.txtFValue.Name = "txtFValue";
            this.txtFValue.Size = new System.Drawing.Size(183, 29);
            this.txtFValue.TabIndex = 3;
            // 
            // txtFValueB
            // 
            this.txtFValueB.Location = new System.Drawing.Point(349, 127);
            this.txtFValueB.Name = "txtFValueB";
            this.txtFValueB.Size = new System.Drawing.Size(183, 29);
            this.txtFValueB.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(271, 130);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 18);
            this.label6.TabIndex = 58;
            this.label6.Text = "ValueB";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 18);
            this.label4.TabIndex = 56;
            this.label4.Text = "Value";
            // 
            // txtFNote
            // 
            this.txtFNote.Location = new System.Drawing.Point(9, 226);
            this.txtFNote.Multiline = true;
            this.txtFNote.Name = "txtFNote";
            this.txtFNote.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtFNote.Size = new System.Drawing.Size(533, 100);
            this.txtFNote.TabIndex = 7;
            this.txtFNote.WordWrap = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 18);
            this.label3.TabIndex = 54;
            this.label3.Text = "Key";
            // 
            // txtFParentID
            // 
            this.txtFParentID.Location = new System.Drawing.Point(82, 92);
            this.txtFParentID.Name = "txtFParentID";
            this.txtFParentID.Size = new System.Drawing.Size(183, 29);
            this.txtFParentID.TabIndex = 2;
            this.txtFParentID.Text = "1234567890";
            // 
            // txtFID
            // 
            this.txtFID.Location = new System.Drawing.Point(82, 22);
            this.txtFID.Name = "txtFID";
            this.txtFID.Size = new System.Drawing.Size(183, 29);
            this.txtFID.TabIndex = 0;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(315, 404);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(96, 33);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 18);
            this.label2.TabIndex = 30;
            this.label2.Text = "ID";
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(111, 404);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(96, 33);
            this.btnRead.TabIndex = 9;
            this.btnRead.Text = "&Read";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(213, 404);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(96, 33);
            this.btnUpdate.TabIndex = 10;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 18);
            this.label5.TabIndex = 32;
            this.label5.Text = "ParentID";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(9, 405);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(96, 33);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(28, 15);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 31;
            this.dataGridView1.Size = new System.Drawing.Size(76, 60);
            this.dataGridView1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1152, 737);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelInfo.ResumeLayout(false);
            this.groupBoxQuery.ResumeLayout(false);
            this.groupBoxQuery.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxEntry.ResumeLayout(false);
            this.groupBoxEntry.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.GroupBox groupBoxQuery;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtParentIDQ;
        private System.Windows.Forms.Button btnClearQ;
        private System.Windows.Forms.TextBox txtKeyQ;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button btnQuery;
        private System.Windows.Forms.ComboBox cboOrderBy;
        private System.Windows.Forms.TextBox txtValueQ;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cboKeyList;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.GroupBox groupBoxEntry;
        private System.Windows.Forms.CheckBox chkReadonly;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label lblFUpdateTime;
        private System.Windows.Forms.Label lblFCreateTime;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtFSeqNo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtFKey;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtFValue;
        private System.Windows.Forms.TextBox txtFValueB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFNote;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFParentID;
        private System.Windows.Forms.TextBox txtFID;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

