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
    public partial class Students : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if loading the page for the first time, populate the student grid
            if(!IsPostBack)
            {
                //Get the student data
                this.GetStudents();
            }
        }

        /**
         * <summary>
         * This method gets the student data from the database
         * </summary>
         * 
         * @method GetStudents
         * @returns {void}
         */
        protected void GetStudents()
        {
            //Connect to Entity Framework
            using (DefaultConnection db = new DefaultConnection())
            {
                //Query the Students Table using Entity Framework and LINQ
                var Students = (from allStudents in db.Students
                                select allStudents);

                //Bind the results to the GridView
                StudentsGridView.DataSource = Students.ToList();
                StudentsGridView.DataBind();
            }
        }

        /**
         * <summary>
         * This event handler deletes a student from the database using entity framework
         * </summary>
         * 
         * @method StudentGridView_RowDeleting
         * @param {object} sender
         * @param {GridviewDeleteEventArgs} e
         * @returns {void}
         * 
         */
        protected void StudentsGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //Store which row was selected
            int selectedRow = e.RowIndex;

            //Get the selected StudentID using the grids DataKey collection
            int StudentID = Convert.ToInt32(StudentsGridView.DataKeys[selectedRow].Values["StudentID"]);

            //Use Entity Framework to find the selected student in the DB and remove it
            using (DefaultConnection db = new DefaultConnection())
            {
                //Create object of the student class and store the query string inside of it
                Student deletedStudent = (from studentRecords in db.Students
                                          where studentRecords.StudentID == StudentID
                                          select studentRecords).FirstOrDefault();

                //Remove the selected student from the database
                db.Students.Remove(deletedStudent);

                //Save changes to database
                db.SaveChanges();

                //Refresh the gridview
                this.GetStudents();
            }
        }

        /**
         * <summary>
         * This event handler allows pagination to occur for the Students page
         * </summary>
         * 
         * @method StudentsGridView_PageIndexChanging
         * @param {object} sender
         * @param {GridViewPageEventArgs} e
         * returns {void}
         * 
         */
        protected void StudentsGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //Set the new page number
            StudentsGridView.PageIndex = e.NewPageIndex;

            //Refresh the grid
            this.GetStudents();
        }

        /**
         * 
         */
        protected void PageSizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Set the new page size
            StudentsGridView.PageSize = Convert.ToInt32(PageSizeDropDownList.SelectedValue);

            //Refresh the grid
            this.GetStudents();
        }
    }
}