using System;
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
    public partial class LoginDialog : Form
    {
        GradesBrowser browser;

        public string cpf
        {
            get
            {
                return textBoxCPF.Text;
            }
        }

        public string password
        {
            get
            {
                return textBoxSenha.Text;
            }
        }
        public LoginDialog(GradesBrowser browser)
        {
            InitializeComponent();
            this.browser = browser;
        }
    }
}
