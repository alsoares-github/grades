using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grades
{
    public partial class Form1 : Form
    {
        GradesBrowser browser;
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            typeof(DataGridView).InvokeMember("DoubleBuffered",
            BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
            null, cls, new object[] { true });

            typeof(DataGridView).InvokeMember("DoubleBuffered",
            BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
            null, enrollment, new object[] { true });

        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            browser.BuildClassesList();
            cls.DataSource = browser.classes;

            cls.Columns["name"].ReadOnly = true;
            cls.Columns["name"].HeaderText = "Disciplina";
            cls.Columns["name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            cls.Columns["hours"].ReadOnly = true;
            cls.Columns["hours"].HeaderText = "Horário";
            cls.Columns["hours"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            cls.Columns["nS"].ReadOnly = true;
            cls.Columns["nS"].HeaderText = "Vagas ocupadas";

            browser.BuildEnrollmentLists();

            btnPushGrades.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            browser = new GradesBrowser();
        }
                
        private void cls_SelectionChanged(object sender, EventArgs e)
        {
            var cl = cls.CurrentRow.DataBoundItem as Class;
            if (cl != null)
            {
                if (cl.students.Count == 0)
                    browser.BuildEnrollmentList(cl);
                enrollment.DataSource = new BindingList<Class.Student>(cl.students);

                enrollment.Columns["name"].ReadOnly = true;
                enrollment.Columns["name"].HeaderText = "Nome";
                enrollment.Columns["name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                enrollment.Columns["id"].Visible = false;
                enrollment.Columns["pfatt"].Visible = false;
                enrollment.Columns["section"].ReadOnly = true;
                enrollment.Columns["section"].HeaderText = "Turma";

                int colsWidth = 75;
                enrollment.Columns["p1"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                enrollment.Columns["p1"].Width = colsWidth;
                enrollment.Columns["p2"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                enrollment.Columns["p2"].Width = colsWidth;
                enrollment.Columns["pf"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                enrollment.Columns["pf"].Width = colsWidth;
                //enrollment.Columns["section"].Visible = false;
            }
        }

        private void btnPushGrades_Click(object sender, EventArgs e)
        {
            var cl = cls.CurrentRow.DataBoundItem as Class;
            if (cl != null)
            {
                browser.SyncClass(cl);
            }       
        }

        private void enrollment_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                var s = enrollment.Rows[e.RowIndex].DataBoundItem as Class.Student;
                var cell = enrollment.Rows[e.RowIndex].Cells[e.ColumnIndex];

                if (s.pfatt)
                {
                    cell.Style.BackColor = Color.Red;
                    cell.Style.ForeColor = Color.White;
                }
                else
                {
                    cell.Style.BackColor = Color.Empty;
                    cell.Style.ForeColor = Color.Empty;
                }
            }
        }

        private void enrollment_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                var s = enrollment.Rows[e.RowIndex].DataBoundItem as Class.Student;

                if (s.pfatt && (s.pf == string.Empty || s.pf == null))
                    s.pfatt = false;
            }
        }

    }
}
