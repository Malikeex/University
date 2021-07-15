using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using University.NetStandart.Core.Models;
using University.NetStandart.DAL.DbContext;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Runtime.InteropServices;
using University.NetStandart.DAL;

namespace University
{
    public partial class frmMain : Form
    {
        IEnumerable<Students> Students;
        IEnumerable<Lessons> Lessons;
        IEnumerable<StudentList> StudentList;
        BindingSource bindingSource_students;
        IEnumerable<Report1> reports;
        private string fileName = string.Empty;
        private DataTableCollection tableCollection = null;

        DbContextOptionsBuilder<ApplicationDbContext> university_optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        public frmMain()
        {
            InitializeComponent();
            string UniversityConnectionString = "Server=WIN-B4D5RH9A1RQ;Initial Catalog=Students_database;Integrated Security=SSPI;Persist Security Info=False;";

            ToolStripMenuItem deleteMenuItem = new ToolStripMenuItem("Delete");

            contextMenuStrip1.Items.AddRange(new[] { deleteMenuItem });


            university_optionsBuilder.UseSqlServer(UniversityConnectionString);
            RefreshData();
        }





        private void RefreshData()
        {
            using (var unitOfWork = new UnitOfWork(new ApplicationDbContext(university_optionsBuilder.Options)))
            {
                // Students = unitOfWork.Students.GetAll();
                Students = unitOfWork.Students.GetAll();
                Lessons = unitOfWork.Lessons.GetAll();
                var studentListFirstSix = Students.Join(Lessons, x => x.Id, y => y.StudentId, (x, y) => new { LessonName = y.Name, StudentName = x.Name }).Take(6).ToList();
                var studentListLastSix = Students.Join(Lessons, x => x.Id, y => y.StudentId, (x, y) => new { LessonName = y.Name, StudentName = x.Name }).Skip(6).ToList();
                //var studentListFirstSix = unitOfWork.StudentList.GetAll();
                //var studentListLastSix = unitOfWork.StudentList.GetAll().ToList();
                label1.Text = unitOfWork.Students.IsExistWoman() ? "есть" : "нет";
                label2.Text = unitOfWork.Students.GetCountWoman().ToString();
                //var resultList = studentListFirstSix.Intersect(studentListLastSix).ToList();
                //var sort = studentListFirstSix.OrderBy(x=>x.LessonName).ToList();
                //var getDistict = studentListFirstSix.Distinct().ToList();
                //dataGridView1.DataSource = Students;

            }
            bindingSource_students = new BindingSource(Students, null);
            //Sample1();


            //  dataGridView1.DataSource = Students;   
            bindingSource_students.CurrentChanged += new EventHandler(bindingSource_students_CurrentChanged);

        }



        private void my_menu_Clicked(object sender, ToolStripItemClickedEventArgs e)
        {
            using (var unitOfWork = new UnitOfWork(new ApplicationDbContext(university_optionsBuilder.Options)))
            {
                var delete = e.ClickedItem.Name.ToString();
                // var entity = unitOfWork.Students.Delete(delete);

                RefreshData();
            }

        }

        private void bindingSource_students_CurrentChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index >= 0)
            {

                Students student = (Students)bindingSource_students.Current;
                // int studentId=Convert.ToInt32(row.Cells["Id"].Value.ToString());
                using (var unitOfWork = new UnitOfWork(new ApplicationDbContext(university_optionsBuilder.Options)))
                {
                    Lessons = unitOfWork.Lessons.GetListByStudentId(student.Id);

                    dataGridView2.DataSource = Lessons;
                }

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void button_Student_Add_Click(object sender, EventArgs e)
        {
            Students student = new Students
            {
                Name = textBox_Name.Text,
                Sex = comboBox_Sex.Text,
                Course = Convert.ToInt32(textBox_Course.Text)
            };

            try
            {
                using (var unitOfWork = new UnitOfWork(new ApplicationDbContext(university_optionsBuilder.Options)))
                {
                    var entity = unitOfWork.Students.InsertAsync(student);
                    RefreshData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }

        private void Sample1()
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext(university_optionsBuilder.Options);
            //  var s = applicationDbContext.Students.Join(applicationDbContext.Lessons, x => x.Id, y => y.StudentId, (x, y) => new { StudentName = x.Name, LessonName = y.Name }).ToList();
            //  dataGridView1.DataSource = s;
            // var s = applicationDbContext.Students.Intersect(applicationDbContext.Lessons); 


        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Lessons_Add_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index >= 0)
            {
                DataGridViewRow row = this.dataGridView1.CurrentRow;
                int studentId = Convert.ToInt32(row.Cells["Id"].Value.ToString());
                Lessons lesson = new Lessons
                {
                    Name = textBox_Lesson_Name.Text,
                    Point = Convert.ToInt32(TextBox_Point.Text),
                    Credit = Convert.ToInt32(textBox_Credit.Text),
                    StudentId = studentId
                };
                using (var unitOfWork = new UnitOfWork(new ApplicationDbContext(university_optionsBuilder.Options)))
                {
                    var entity = unitOfWork.Lessons.InsertAsync(lesson);
                    RefreshData();
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Point_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void deleteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index >= 0)
            {
                Students student = (Students)bindingSource_students.Current;
                using (var unitOfWork = new UnitOfWork(new ApplicationDbContext(university_optionsBuilder.Options)))
                {
                    unitOfWork.Students.Delete(student);
                    RefreshData();
                }

            }
        }
        string[,] list = new string[50, 5];
        
    
        private void ExcelButton_Click(object sender, EventArgs e)
        {

            string file = ""; //variable for the Excel File Location
            DataTable dt = new DataTable(); //container for our excel data
            DataRow row;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Check if Result == "OK".
            {
                file = openFileDialog1.FileName; //get the filename with the location of the file
                try
                {
                    //Create Object for Microsoft.Office.Interop.Excel that will be use to read excel file

                    Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();

                    Microsoft.Office.Interop.Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(file);

                    Microsoft.Office.Interop.Excel._Worksheet excelWorksheet = excelWorkbook.Sheets[1];

                    Microsoft.Office.Interop.Excel.Range excelRange = excelWorksheet.UsedRange;

                    int rowCount = excelRange.Rows.Count; //get row count of excel data

                    int colCount = excelRange.Columns.Count; // get column count of excel data

                    //Get the first Column of excel file which is the Column Name

                    for (int i = 1; i <= rowCount; i++)
                    {
                        for (int j = 1; j <= colCount; j++)
                        {
                            dt.Columns.Add(excelRange.Cells[i, j].Value2.ToString());
                        }
                        break;
                    }

                    //Get Row Data of Excel

                    int rowCounter; //This variable is used for row index number
                    for (int i = 2; i <= rowCount; i++) //Loop for available row of excel data
                    {
                        row = dt.NewRow(); //assign new row to DataTable
                        rowCounter = 0;
                        for (int j = 1; j <= colCount; j++) //Loop for available column of excel data
                        {
                            //check if cell is empty
                            if (excelRange.Cells[i, j] != null && excelRange.Cells[i, j].Value2 != null)
                            {
                                row[rowCounter] = excelRange.Cells[i, j].Value2.ToString();
                            }
                            else
                            {
                                row[i] = "";
                            }
                            rowCounter++;
                        }
                        dt.Rows.Add(row); //add row to DataTable
                    }

                    dataGridView1.DataSource = dt; //assign DataTable as Datasource for DataGridview

                    //Закрыть и очистить Excel
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    Marshal.ReleaseComObject(excelRange);
                    Marshal.ReleaseComObject(excelWorksheet);
                    //Выход с приложения
                    excelWorkbook.Close();
                    Marshal.ReleaseComObject(excelWorkbook);
                    excelApp.Quit();
                    Marshal.ReleaseComObject(excelApp);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }
    }
}
