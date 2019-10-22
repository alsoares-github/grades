﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
                enrollment.DataSource = cl.students;

                enrollment.Columns["name"].ReadOnly = true;
                enrollment.Columns["name"].HeaderText = "Nome";
                enrollment.Columns["name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                enrollment.Columns["id"].Visible = false;
                enrollment.Columns["section"].HeaderText = "Turma";
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
    }
}