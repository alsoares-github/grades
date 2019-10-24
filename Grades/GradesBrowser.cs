using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Data;

namespace Grades
{
    class Course
    {
        public string name { get; set; }
        public string id { get; set; }
        public string hours { get; set; }
    }

    class Class
    {
        public class Student : IComparable, INotifyPropertyChanged
        {
            public string name { get; set; }
            public string id { get; set; }
            public string section { get; set; }
            public string p1 { get; set; }
            public string p2 { get; set; }
            public string pf { get; set; }

            private bool _pfatt;
            public bool pfatt 
            {
                get { return _pfatt; } 
                set
                {
                    _pfatt = value;
                    NotifyPropertyChanged(nameof(pf));
                }
            }

            public Student()
            {
                pfatt = false;
            }

            public event PropertyChangedEventHandler PropertyChanged;
            public void NotifyPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            public int CompareTo(object obj)
            {
                if (obj == null) return 1;
                Student other = obj as Student;
                if (other != null)
                    return this.name.CompareTo(other.name);
                else
                    throw new ArgumentException("Object is not a Student");
            }
        }
 
        public string name { get; set; }
        public string nS { get; set; }
        public string hours { get; set; }
        public List<Student> students = new List<Student>();
        public List<string> sections = new List<string>();

    }
    class GradesBrowser 
    {
        private FirefoxDriver driver;
        private WebDriverWait wait;
        public List<Course> courses;
        public List<Class> classes;

        public GradesBrowser()
        {
            var ds = FirefoxDriverService.CreateDefaultService();
            ds.HideCommandPromptWindow = true;

            driver = new FirefoxDriver(ds, new FirefoxOptions());

            driver.Url = "https://professor.cefet-rj.br";
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            wait.PollingInterval = TimeSpan.FromMilliseconds(5);
            var x = driver.FindElementById("j_username");
            x.Click();
            x.SendKeys("08255864709");
            x = driver.FindElementById("j_password");
            x.Click();
            x.SendKeys("splash00");
            x = driver.FindElementByClassName("button");
            x.Click();
        }

        ~GradesBrowser()
        {
            driver.Quit();
        }

        public Func<IWebDriver, bool> SelectorExists(string query)
        {
            return (driver) =>
            {
                try
                {
                    return (bool)(driver as IJavaScriptExecutor).ExecuteScript("return " + query + ".length != 0;");
                }
                catch (Exception)
                {
                    return false;
                }
            };
        }

        public Func<IWebDriver, bool> ScriptExecutes(string query)
        {
            return (driver) =>
            {
                try
                {
                    (driver as IJavaScriptExecutor).ExecuteScript(query);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            };
        }

        public Func<IWebDriver, Object> ScriptReturns(string query)
        {
            return (driver) =>
            {
                try
                {
                    return (driver as IJavaScriptExecutor).ExecuteScript(query);
                }
                catch (Exception)
                {
                    return null;
                }
            };
        }

        public Func<IWebDriver, IWebElement> IsVisible(By by)
        {
            return (driver) =>
            {
                try
                {
                    var x = driver.FindElement(by);
                    return x.Displayed ? x : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
               
            };
        }

        public Func<IWebDriver, ReadOnlyCollection<IWebElement>> AreVisible(By by)
        {
            return (driver) =>
            {
                try
                {
                    var X = driver.FindElements(by);
                    if (X.Any(e => !e.Displayed))
                        return null;

                    return X.Any() ? X : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }

            };
        }

        public Func<IWebDriver, ReadOnlyCollection<IWebElement>> ArePresent(By by)
        {
            return (driver) =>
            {
                try
                {
                    var X = driver.FindElements(by);
                    return X.Any() ? X : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }

            };
        }
        public void BuildClassesList()
        {
            courses = new List<Course>();

            var X = wait.Until(AreVisible(By.PartialLinkText("T.")));
            foreach (var x in X)
            {
                x.Click();
                while (driver.WindowHandles.Count < 2)
                {

                }
                courses.Add(GetCourseInfo(driver.WindowHandles[1]));
            }
            ProcessCoursesList();
        }

        private Course GetCourseInfo(string handle)
        {
            var c = new Course();
            var curr_handle = driver.WindowHandles[0];
            driver.SwitchTo().Window(handle);

            // Name
            var script = @"x = $("".label:contains('Disciplina')"").parent()[0];
                       return $.trim(x.textContent);";
            c.name = ((String)wait.Until(ScriptReturns(script))).Split(new[] { "\n" }, System.StringSplitOptions.RemoveEmptyEntries)[1];

            // Class Id
            script = @"x = $("".topopage"")[0];
                       return $.trim(x.textContent);";
            c.id = ((String)wait.Until(ScriptReturns(script))).Split(new[] { "\xa0" }, System.StringSplitOptions.RemoveEmptyEntries)[1];

            // Hours
            script = @"X = $(""div[title='Horários'] > table > tbody > tr > td"");
                       l = [];
                       for (i = 0; i < X.length; i++)
                       {
                           l.push($.trim(X[i].textContent));
                       }
                       return l;";
            var H = (ReadOnlyCollection<object>)wait.Until(ScriptReturns(script));
            foreach (string h in H)
            {
                c.hours += h + " ";
            }


            driver.Close();
            driver.SwitchTo().Window(curr_handle);

            return c;
        }

        private void ProcessCoursesList()
        {
            classes = new List<Class>();

            foreach (var c in courses)
            {
                var h = c.hours;
                var cl = HasTimeslot(classes, h);
                if (cl == null)
                {
                    cl = new Class();
                    cl.name = c.name;
                    cl.hours = c.hours;
                    classes.Add(cl);
                }
                cl.sections.Add(c.id);
            }
        }
       
        public void NavigateToSection(string name, string class_id)
        {
            var script = $"$(\"table#turmasLiberadas > tbody > tr:contains('{name}'):contains('{class_id}') > td:eq(5)\").find(\"a\")[0].click();";
            wait.Until(ScriptExecutes(script));
        }

        public Class HasTimeslot(List<Class> classes, string hours)
        {
            foreach (var cl in classes)
            {
                if (cl.hours == hours)
                    return cl;
            }
            return null;
        }
        public List<Class.Student> GetStudentsData()
        {
            wait.Until(SelectorExists("$('td#column-alunos > table[id]')"));
            var script = @"X = $(""td#column-alunos > table[id]"");
                           students = [];
                           for (i = 0; i < X.length; i++)
                           {
                                sid = X[i].id.substring('table-aluno'.length);
                                name = $(X[i]).find("".nome-aluno"")[0].textContent;
                                p1 = $(""#notaaluno"" + sid + ""nota221aval221"")[0].value;
                                p2 = $(""#notaaluno"" + sid + ""nota222aval222"")[0].value;
                                pf = $(""#inputexamealuno"" + sid)[0].value;
                                students.push([sid, name, p1, p2, pf]);
                            }
                            return students;";
            var S = (ReadOnlyCollection<object>)driver.ExecuteScript(script);
            
            var students = new List<Class.Student>();
            foreach (ReadOnlyCollection<object> s in S)
            {
                var student = new Class.Student();
                student.id = (string)s[0];
                student.name = (string)s[1];
                student.p1 = (string)s[2];
                student.p2 = (string)s[3];
                student.pf = (string)s[4];

                students.Add(student);
            }

            return students;
        }

        public void BuildEnrollmentList(Class cl)
        {
            int nS = 0;
            foreach (var s in cl.sections)
            {
                NavigateToSection(cl.name, s);
                var students = GetStudentsData();
                foreach (var st in students)
                    st.section = s;
                cl.students.AddRange(students);
                nS += students.Count;
                wait.Until(ScriptExecutes("$(\"a:contains('Minhas Turmas')\")[0].click();"));
            }
            cl.nS = nS.ToString();
            cl.students.Sort();
        }
        
        
        public void BuildEnrollmentLists()
        {
            if (classes == null)
                throw new Exception("Classes list does not exist; call BuildClassesList() first.");

            foreach (var cl in classes)
            {
                if (cl.students.Count == 0)
                    BuildEnrollmentList(cl);
            }
        }

        public void SyncStudents(IEnumerable<Class.Student> students)
        {
            wait.Until(SelectorExists("$('td#column-alunos > table[id]')"));

            foreach (var s in students)
            {
                if (s.p1 != "" && s.p1 != null)
                {
                    driver.ExecuteScript($"document.getElementById('notaaluno{s.id}nota221aval221').value='{s.p1}'");
                    driver.ExecuteScript($"document.getElementById('notaaluno{s.id}nota221aval221').dispatchEvent(new Event('blur'))");
                }
                if (s.p2 != "" && s.p2 != null)
                {
                    driver.ExecuteScript($"document.getElementById('notaaluno{s.id}nota222aval222').value='{s.p2}'");
                    driver.ExecuteScript($"document.getElementById('notaaluno{s.id}nota222aval222').dispatchEvent(new Event('blur'))");
                }
                if (s.pf != "" && s.pf != null)
                {
                    if ((bool)driver.ExecuteScript($"return enableDisableInputExame({s.id});"))
                    {
                        driver.ExecuteScript($"document.getElementById('inputexamealuno{s.id}').value='{s.pf}'");
                        driver.ExecuteScript($"document.getElementById('inputexamealuno{s.id}').dispatchEvent(new Event('blur'))");
                    }
                    else
                    {
                        MessageBox.Show("Campo PF do aluno " + s.name + " não está habilitado");
                        s.pfatt = true;
                    }
                }
            }
        }

        public void SyncClass(Class cl)
        {
            foreach (var section in cl.sections)
            {
                var studentQuery =
                    from student in cl.students
                    where student.section == section
                    select student;
                NavigateToSection(cl.name, section);
                wait.Until(SelectorExists("$('td#column-alunos > table[id]')"));
                SyncStudents(studentQuery);
                if ((bool)driver.ExecuteScript("x = hasAlunoSelecionado(); return x;"))
                {
                    wait.Until(ScriptExecutes("$(\":button[value='Salvar Lançamento para Alunos Marcados']\")[0].click();"));
                }
                else
                {
                    wait.Until(ScriptExecutes("$(\"a:contains('Minhas Turmas')\")[0].click();"));
                }              
            }
        }
    }
}
