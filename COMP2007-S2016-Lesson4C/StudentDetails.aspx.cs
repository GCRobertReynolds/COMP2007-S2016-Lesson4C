using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//Using statements required to connect to Entity Framework Database
using COMP2007_S2016_Lesson4C.Models;
using System.Web.ModelBinding;

namespace COMP2007_S2016_Lesson4C
{
    public partial class StudentDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                this.GetStudent();
            }
        }

        protected void GetStudent()
        {
            //Populate the form with existing data from the database
            int StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);

            //Connect to the Entity Framework Database
            using (DefaultConnection db = new DefaultConnection())
            {
                //Populate a student object instance with the StudentID from the URL Param
                Student updatedStudent = (from student in db.Students
                                          where student.StudentID == StudentID
                                          select student).FirstOrDefault();

                //Map the student properties to the from controls
                if(updatedStudent != null)
                {
                    LastNameTextBox.Text = updatedStudent.LastName;
                    FirstNameTextBox.Text = updatedStudent.FirstMidName;
                    EnrollmentDateTextBox.Text = updatedStudent.EnrollmentDate.ToString("yyyy-MM-dd");
                }
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            //Redirect back to Students page
            Response.Redirect("~/Students.aspx");
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            //Use Entity Framework to connect to the server
            using (DefaultConnection db = new DefaultConnection())
            {
                //Use the Student model to create a new student object and
                //Save a new record
                Student newStudent = new Student();

                int StudentID = 0;

                if(Request.QueryString.Count > 0) //Our URL has a StudentID in it
                {
                    //Get the StudentID from the URL
                    StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);

                    //Get the current student from the Entity Framework Database
                    newStudent = (from student in db.Students
                                  where student.StudentID == StudentID
                                  select student).FirstOrDefault();
                }

                //Add data to the new student record
                newStudent.LastName = LastNameTextBox.Text;
                newStudent.FirstMidName = FirstNameTextBox.Text;
                newStudent.EnrollmentDate = Convert.ToDateTime(EnrollmentDateTextBox.Text);

                //Use LINQ/ADO.NET to Add/Insert new student into the database
                if(StudentID == 0)
                {
                    db.Students.Add(newStudent);
                }
                

                //Save our changes (also updates and inserts)
                db.SaveChanges();

                //Redirect back to the updated students page
                Response.Redirect("~/Students.aspx");
            }
        }
    }
}