namespace Grades
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cls = new System.Windows.Forms.DataGridView();
            this.btnSync = new System.Windows.Forms.Button();
            this.enrollment = new System.Windows.Forms.DataGridView();
            this.btnPushGrades = new System.Windows.Forms.Button();
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
            this.cls.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.cls.Size = new System.Drawing.Size(801, 168);
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
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray;
            this.enrollment.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.enrollment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.enrollment.Location = new System.Drawing.Point(0, 200);
            this.enrollment.Name = "enrollment";
            this.enrollment.Size = new System.Drawing.Size(801, 621);
            this.enrollment.TabIndex = 2;
            // 
            // btnPushGrades
            // 
            this.btnPushGrades.Location = new System.Drawing.Point(137, 827);
            this.btnPushGrades.Name = "btnPushGrades";
            this.btnPushGrades.Size = new System.Drawing.Size(146, 23);
            this.btnPushGrades.TabIndex = 3;
            this.btnPushGrades.Text = "Lançar Notas da Turma";
            this.btnPushGrades.UseVisualStyleBackColor = true;
            this.btnPushGrades.Click += new System.EventHandler(this.btnPushGrades_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 862);
            this.Controls.Add(this.btnPushGrades);
            this.Controls.Add(this.enrollment);
            this.Controls.Add(this.btnSync);
            this.Controls.Add(this.cls);
            this.Name = "Form1";
            this.Text = "Notas Lanceitor";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.enrollment)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView cls;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.DataGridView enrollment;
        private System.Windows.Forms.Button btnPushGrades;
    }
}

