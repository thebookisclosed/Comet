namespace Comet.UI
{
    partial class HandlerChoice
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
            this.TclMain = new System.Windows.Forms.TabControl();
            this.TpgHandlers = new System.Windows.Forms.TabPage();
            this.PtbDrive = new System.Windows.Forms.PictureBox();
            this.LvwHandlers = new System.Windows.Forms.ListView();
            this.ClnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ClnSpace = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GbxDesc = new System.Windows.Forms.GroupBox();
            this.BtnAdvanced = new System.Windows.Forms.Button();
            this.BtnElevate = new System.Windows.Forms.Button();
            this.LblDesc = new System.Windows.Forms.Label();
            this.LblSavingsNum = new System.Windows.Forms.Label();
            this.LblSavings = new System.Windows.Forms.Label();
            this.LblFiles = new System.Windows.Forms.Label();
            this.LblIntro = new System.Windows.Forms.Label();
            this.BtnOk = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.TclMain.SuspendLayout();
            this.TpgHandlers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PtbDrive)).BeginInit();
            this.GbxDesc.SuspendLayout();
            this.SuspendLayout();
            // 
            // TclMain
            // 
            this.TclMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TclMain.Controls.Add(this.TpgHandlers);
            this.TclMain.Location = new System.Drawing.Point(6, 7);
            this.TclMain.Name = "TclMain";
            this.TclMain.SelectedIndex = 0;
            this.TclMain.Size = new System.Drawing.Size(361, 379);
            this.TclMain.TabIndex = 0;
            this.TclMain.TabStop = false;
            // 
            // TpgHandlers
            // 
            this.TpgHandlers.Controls.Add(this.PtbDrive);
            this.TpgHandlers.Controls.Add(this.LvwHandlers);
            this.TpgHandlers.Controls.Add(this.GbxDesc);
            this.TpgHandlers.Controls.Add(this.LblSavingsNum);
            this.TpgHandlers.Controls.Add(this.LblSavings);
            this.TpgHandlers.Controls.Add(this.LblFiles);
            this.TpgHandlers.Controls.Add(this.LblIntro);
            this.TpgHandlers.Location = new System.Drawing.Point(4, 22);
            this.TpgHandlers.Name = "TpgHandlers";
            this.TpgHandlers.Padding = new System.Windows.Forms.Padding(3);
            this.TpgHandlers.Size = new System.Drawing.Size(353, 353);
            this.TpgHandlers.TabIndex = 0;
            this.TpgHandlers.Text = "Disk Cleanup";
            this.TpgHandlers.UseVisualStyleBackColor = true;
            // 
            // PtbDrive
            // 
            this.PtbDrive.Location = new System.Drawing.Point(11, 0);
            this.PtbDrive.Name = "PtbDrive";
            this.PtbDrive.Size = new System.Drawing.Size(32, 32);
            this.PtbDrive.TabIndex = 6;
            this.PtbDrive.TabStop = false;
            // 
            // LvwHandlers
            // 
            this.LvwHandlers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LvwHandlers.CheckBoxes = true;
            this.LvwHandlers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ClnName,
            this.ClnSpace});
            this.LvwHandlers.FullRowSelect = true;
            this.LvwHandlers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.LvwHandlers.Location = new System.Drawing.Point(11, 70);
            this.LvwHandlers.Name = "LvwHandlers";
            this.LvwHandlers.Size = new System.Drawing.Size(332, 93);
            this.LvwHandlers.TabIndex = 0;
            this.LvwHandlers.UseCompatibleStateImageBehavior = false;
            this.LvwHandlers.View = System.Windows.Forms.View.Details;
            this.LvwHandlers.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.LvwHandlers_ItemChecked);
            this.LvwHandlers.SelectedIndexChanged += new System.EventHandler(this.LvwHandlers_SelectedIndexChanged);
            // 
            // ClnName
            // 
            this.ClnName.Text = "Name";
            this.ClnName.Width = 231;
            // 
            // ClnSpace
            // 
            this.ClnSpace.Text = "Space";
            this.ClnSpace.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ClnSpace.Width = 80;
            // 
            // GbxDesc
            // 
            this.GbxDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GbxDesc.Controls.Add(this.BtnAdvanced);
            this.GbxDesc.Controls.Add(this.BtnElevate);
            this.GbxDesc.Controls.Add(this.LblDesc);
            this.GbxDesc.Location = new System.Drawing.Point(11, 190);
            this.GbxDesc.Name = "GbxDesc";
            this.GbxDesc.Size = new System.Drawing.Size(332, 153);
            this.GbxDesc.TabIndex = 1;
            this.GbxDesc.TabStop = false;
            this.GbxDesc.Text = "Description";
            // 
            // BtnAdvanced
            // 
            this.BtnAdvanced.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnAdvanced.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BtnAdvanced.Location = new System.Drawing.Point(231, 119);
            this.BtnAdvanced.Name = "BtnAdvanced";
            this.BtnAdvanced.Size = new System.Drawing.Size(90, 23);
            this.BtnAdvanced.TabIndex = 1;
            this.BtnAdvanced.Text = "&View Files";
            this.BtnAdvanced.UseVisualStyleBackColor = true;
            this.BtnAdvanced.Click += new System.EventHandler(this.BtnAdvanced_Click);
            // 
            // BtnElevate
            // 
            this.BtnElevate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnElevate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BtnElevate.Location = new System.Drawing.Point(10, 119);
            this.BtnElevate.Name = "BtnElevate";
            this.BtnElevate.Size = new System.Drawing.Size(138, 23);
            this.BtnElevate.TabIndex = 0;
            this.BtnElevate.Text = "Clean up &system files";
            this.BtnElevate.UseVisualStyleBackColor = true;
            this.BtnElevate.Click += new System.EventHandler(this.BtnElevate_Click);
            // 
            // LblDesc
            // 
            this.LblDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblDesc.Location = new System.Drawing.Point(6, 20);
            this.LblDesc.Name = "LblDesc";
            this.LblDesc.Size = new System.Drawing.Size(315, 96);
            this.LblDesc.TabIndex = 2;
            // 
            // LblSavingsNum
            // 
            this.LblSavingsNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.LblSavingsNum.Location = new System.Drawing.Point(195, 171);
            this.LblSavingsNum.Name = "LblSavingsNum";
            this.LblSavingsNum.Size = new System.Drawing.Size(144, 13);
            this.LblSavingsNum.TabIndex = 5;
            this.LblSavingsNum.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // LblSavings
            // 
            this.LblSavings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblSavings.AutoSize = true;
            this.LblSavings.Location = new System.Drawing.Point(8, 171);
            this.LblSavings.Name = "LblSavings";
            this.LblSavings.Size = new System.Drawing.Size(181, 13);
            this.LblSavings.TabIndex = 4;
            this.LblSavings.Text = "Total amount of disk space you gain:";
            // 
            // LblFiles
            // 
            this.LblFiles.AutoSize = true;
            this.LblFiles.Location = new System.Drawing.Point(8, 50);
            this.LblFiles.Name = "LblFiles";
            this.LblFiles.Size = new System.Drawing.Size(75, 13);
            this.LblFiles.TabIndex = 3;
            this.LblFiles.Text = "&Files to delete:";
            // 
            // LblIntro
            // 
            this.LblIntro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblIntro.Location = new System.Drawing.Point(50, 11);
            this.LblIntro.Name = "LblIntro";
            this.LblIntro.Size = new System.Drawing.Size(267, 39);
            this.LblIntro.TabIndex = 2;
            this.LblIntro.Text = "You can use Disk Cleanup to free up to {0} of disk space on {1}.";
            // 
            // BtnOk
            // 
            this.BtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOk.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BtnOk.Location = new System.Drawing.Point(211, 392);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(75, 23);
            this.BtnOk.TabIndex = 1;
            this.BtnOk.Text = "OK";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.BtnCancel.Location = new System.Drawing.Point(292, 392);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(75, 23);
            this.BtnCancel.TabIndex = 2;
            this.BtnCancel.Text = "Cancel";
            this.BtnCancel.UseVisualStyleBackColor = true;
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // HandlerChoice
            // 
            this.AcceptButton = this.BtnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(373, 422);
            this.Controls.Add(this.BtnOk);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.TclMain);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HandlerChoice";
            this.Text = "Managed Disk Cleanup for ";
            this.Load += new System.EventHandler(this.HandlerChoice_Load);
            this.TclMain.ResumeLayout(false);
            this.TpgHandlers.ResumeLayout(false);
            this.TpgHandlers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PtbDrive)).EndInit();
            this.GbxDesc.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TclMain;
        private System.Windows.Forms.TabPage TpgHandlers;
        private System.Windows.Forms.ListView LvwHandlers;
        private System.Windows.Forms.ColumnHeader ClnName;
        private System.Windows.Forms.ColumnHeader ClnSpace;
        private System.Windows.Forms.GroupBox GbxDesc;
        private System.Windows.Forms.Button BtnAdvanced;
        private System.Windows.Forms.Button BtnElevate;
        private System.Windows.Forms.Label LblDesc;
        private System.Windows.Forms.Label LblSavingsNum;
        private System.Windows.Forms.Label LblSavings;
        private System.Windows.Forms.Label LblFiles;
        private System.Windows.Forms.Label LblIntro;
        private System.Windows.Forms.Button BtnOk;
        private System.Windows.Forms.Button BtnCancel;
        private System.Windows.Forms.PictureBox PtbDrive;
    }
}

