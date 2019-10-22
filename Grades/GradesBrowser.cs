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
        public class Student : IComparable
        {
            public string name { get; set; }
            public string id { get; set; }
            public string section { get; set; }
            public string p1 { get; set; }
            public string p2 { get; set; }
            public string pf { get; set; }

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
            driver = new FirefoxDriver();
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

        public Func<IWebDriver, bool> SelectorExists(String query)
        {
            return (driver) =>
            {
                try
                {
                    if ((bool)(driver as FirefoxDriver).ExecuteScript(query))
                        return true;
                    else
                        return false;
                }
                catch (Exception)
                {
                    return false;
                }
            };
        }

        public Func<IWebDriver, bool> ScriptExecutes(String query)
        {
            return (driver) =>
            {
                try
                {
                    (driver as FirefoxDriver).ExecuteScript(query);
                    return true;
                }
                catch (Exception)
                {
                    return false;
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
            var Ax = 2;
        }

        private Course GetCourseInfo(string handle)
        {
            var c = new Course();
            var curr_handle = driver.WindowHandles[0];
            driver.SwitchTo().Window(handle);

            // Name
            var E = wait.Until(AreVisible(By.ClassName("label")));
            IWebElement x = null;
            foreach (var e in E)
            {
                if (e.Text == "Disciplina:")
                {
                    x = e;
                    break;
                }
            }

            x = x.FindElement(By.XPath("./.."));
            c.name = x.Text.Split(new[] { ": " }, System.StringSplitOptions.RemoveEmptyEntries)[1];

            // Class Id
            x = wait.Until<IWebElement>(IsVisible(By.ClassName("topopage")));
            c.id = x.Text.Split(new[] { "  " }, System.StringSplitOptions.RemoveEmptyEntries)[1];

            // Hours
            E = wait.Until(ArePresent(By.XPath("//div[@id='accordion']/*")));
            foreach (var e in E)
            {
                if (e.Text == "Horários")
                {
                    e.Click();
                    break;
                }
            }
            E = wait.Until(AreVisible(By.XPath("//div[@title='Horários']/table/tbody/tr/td")));
            foreach (var e in E)
                c.hours += e.Text + " ";


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
            /*var X = wait.Until(AreVisible(By.XPath("//table[@id='turmasLiberadas']/tbody/tr")));

            foreach (var x in X)
            {
                if (x.Text.Contains(name) && x.Text.Contains(class_id))
                {
                    var Y = x.FindElements(By.XPath("./td[6]/a"));
                    Y[0].Click();
                    return;
                }
            }*/
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
            var students = new List<Class.Student>();
            var X = wait.Until(AreVisible(By.XPath("//td[@id='column-alunos']/table[@id]")));
            foreach (var x in X)
            {
                var s = new Class.Student();
                s.id = x.GetAttribute("id").Substring("table-aluno".Length);
                s.name = x.FindElement(By.ClassName("nome-aluno")).Text;
                s.p1 = wait.Until(IsVisible(By.Id("notaaluno" + s.id + "nota221aval221"))).GetAttribute("value");
                s.p2 = wait.Until(IsVisible(By.Id("notaaluno" + s.id + "nota222aval222"))).GetAttribute("value");
                s.pf = wait.Until(IsVisible(By.Id("inputexamealuno" + s.id))).GetAttribute("value");
                students.Add(s);
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
                var x = wait.Until(IsVisible(By.PartialLinkText("Minhas Turmas")));
                x.Click();
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
            foreach (var s in students)
            {
                if (s.p1 != "")
                {
                    var x = wait.Until(IsVisible(By.Id("notaaluno" + s.id + "nota221aval221")));
                    driver.ExecuteScript($"document.getElementById('notaaluno{s.id}nota221aval221').value='{s.p1}'");
                    driver.ExecuteScript($"document.getElementById('notaaluno{s.id}nota221aval221').dispatchEvent(new Event('blur'))");
                }
                if (s.p2 != "")
                {
                    var x = wait.Until(IsVisible(By.Id("notaaluno" + s.id + "nota222aval222")));
                    driver.ExecuteScript($"document.getElementById('notaaluno{s.id}nota222aval222').value='{s.p2}'");
                    driver.ExecuteScript($"document.getElementById('notaaluno{s.id}nota222aval222').dispatchEvent(new Event('blur'))");
                }
                /*if (s.pf != "")
                {
                    var x = wait.Until(IsVisible(By.Id("inputexamealuno" + s.id)));
                    x.Click();
                    x.SendKeys(Keys.Control + "a");
                    x.SendKeys(s.pf);
                }*/
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
                SyncStudents(studentQuery);
                if ((bool)driver.ExecuteScript("x = hasAlunoSelecionado(); return x;"))
                {
                }
                else
                {
                    var x = wait.Until(IsVisible(By.PartialLinkText("Minhas Turmas")));
                    x.Click();
                }              
            }
        }
    }
}
