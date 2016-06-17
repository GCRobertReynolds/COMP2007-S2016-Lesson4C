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
                //save a new record
                Student newStudent = new Student();


                //Add data to the new student record
                newStudent.LastName = LastNameTextBox.Text;
                newStudent.FirstMidName = FirstNameTextBox.Text;
                newStudent.EnrollmentDate = Convert.ToDateTime(EnrollmentDateTextBox.Text);

                //Use LINQ/ADO.NET to Add/Insert new student into the database
                db.Students.Add(newStudent);

                //Save our changes
                db.SaveChanges();

                //Redirect back to the updated students page
                Response.Redirect("~/Students.aspx");
            }
        }
    }
}