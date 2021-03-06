﻿namespace Grades
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cls = new System.Windows.Forms.DataGridView();
            this.btnSync = new System.Windows.Forms.Button();
            this.enrollment = new System.Windows.Forms.DataGridView();
            this.btnPushGrades = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.cls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.enrollment)).BeginInit();
            this.SuspendLayout();
            // 
            // cls
            // 
            this.cls.AllowUserToAddRows = false;
            this.cls.AllowUserToDeleteRows = false;
            this.cls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cls.Location = new System.Drawing.Point(0, 0);
            this.cls.Name = "cls";
            this.cls.ReadOnly = true;
            this.cls.RowHeadersWidth = 82;
            this.cls.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.cls.Size = new System.Drawing.Size(769, 168);
            this.cls.TabIndex = 0;
            this.cls.SelectionChanged += new System.EventHandler(this.cls_SelectionChanged);
            // 
            // btnSync
            // 
            this.btnSync.Location = new System.Drawing.Point(12, 827);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(119, 23);
            this.btnSync.TabIndex = 1;
            this.btnSync.Text = "Sincronizar Turmas";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // enrollment
            // 
            this.enrollment.AllowUserToAddRows = false;
            this.enrollment.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.enrollment.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.enrollment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.enrollment.DefaultCellStyle = dataGridViewCellStyle2;
            this.enrollment.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.enrollment.Location = new System.Drawing.Point(0, 200);
            this.enrollment.Name = "enrollment";
            this.enrollment.RowHeadersWidth = 82;
            this.enrollment.RowTemplate.Height = 30;
            this.enrollment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.enrollment.Size = new System.Drawing.Size(769, 621);
            this.enrollment.TabIndex = 2;
            this.enrollment.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.enrollment_CellFormatting);
            this.enrollment.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.enrollment_CellValueChanged);
            // 
            // btnPushGrades
            // 
            this.btnPushGrades.Enabled = false;
            this.btnPushGrades.Location = new System.Drawing.Point(137, 827);
            this.btnPushGrades.Name = "btnPushGrades";
            this.btnPushGrades.Size = new System.Drawing.Size(146, 23);
            this.btnPushGrades.TabIndex = 3;
            this.btnPushGrades.Text = "Lançar Notas da Turma";
            this.btnPushGrades.UseVisualStyleBackColor = true;
            this.btnPushGrades.Click += new System.EventHandler(this.btnPushGrades_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 859);
            this.Controls.Add(this.btnPushGrades);
            this.Controls.Add(this.enrollment);
            this.Controls.Add(this.btnSync);
            this.Controls.Add(this.cls);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Notas Lanceitor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.cls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.enrollment)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView cls;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.DataGridView enrollment;
        private System.Windows.Forms.Button btnPushGrades;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

