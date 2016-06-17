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
    }
}