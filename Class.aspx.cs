using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlTypes;

using Microsoft.SqlServer.Server;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;
using System.Data;
using System.Threading;

public partial class _Class : System.Web.UI.Page
{
    string connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\KIIT\Documents\project\App_Data\Class.mdf;Integrated Security=True";


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SqlConnection con = new SqlConnection(connetionString);
            string com = "Select * from class";
            SqlDataAdapter adpt = new SqlDataAdapter(com, con);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            ddlSections.DataSource = dt;
            ddlSections.DataBind();
            ddlSections.DataTextField = "className";
            ddlSections.DataValueField = "value";
            ddlSections.DataBind();
        }
    }


    protected void btnAddC_Click(object sender, EventArgs e)
    {
        Add_Class.Visible = true;
    }

    //Add new class

    protected void btnAddnewClass(object sender, EventArgs e)
    {

        InsertClass();
        newClass.Text = string.Empty;
        Add_Class.Visible = false;
        refreshdata();
    }

    int InsertClass()
    {
        using (SqlConnection myConnection = new SqlConnection(connetionString))
        {
            SqlCommand MyCommand = new SqlCommand("INSERT INTO class (ID, className, value) Values (@id, @classname, @value)", myConnection);
            MyCommand.Parameters.AddWithValue("@id", newClass.Text);
            MyCommand.Parameters.AddWithValue("@value", newClass.Text);
            MyCommand.Parameters.AddWithValue("@classname", newClass.Text);
            myConnection.Open();
            return MyCommand.ExecuteNonQuery();

        }
    }

    //submit class

    protected void btnSubmitC_Click(object sender, EventArgs e)
    {

        classpanel.Visible = false;
        teacherpanel.Visible = true;

        SqlConnection con = new SqlConnection(connetionString);
        string com = "Select * from teacher where [id]='" + (ddlSections.SelectedValue) + "'";
        SqlDataAdapter adpt = new SqlDataAdapter(com, con);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        ddlteacher.DataSource = dt;
        ddlteacher.DataTextField = "teacherName";
        ddlteacher.DataValueField = "value";
        ddlteacher.DataBind();



    }


    protected void btnAddT_Click(object sender, EventArgs e)
    {
        Add_Teach.Visible = true;
    }

    // add new teacher
    protected void btnAddnewTeach(object sender, EventArgs e)
    {

        InsertTeacher();
        newTeach.Text = string.Empty;
        Add_Teach.Visible = false;
        refreshdata();
    }

    int InsertTeacher()
    {
        using (SqlConnection myConnection = new SqlConnection(connetionString))
        {
            SqlCommand MyCommand = new SqlCommand("INSERT INTO teacher (id, teacherName, value) Values (@id, @teacherName, @value)", myConnection);
            MyCommand.Parameters.AddWithValue("@id", ddlSections.SelectedValue);
            MyCommand.Parameters.AddWithValue("@value", (ddlteacher.Items.Count + 1));

            MyCommand.Parameters.AddWithValue("@teacherName", newTeach.Text);
            myConnection.Open();
            return MyCommand.ExecuteNonQuery();

        }
    }

    //Submit teacher

    protected void btnSubmitT_Click(object sender, EventArgs e)
    {
        GridView1.Visible = true;
        SaveAttendance.Visible = true;
        btnAddStud.Visible = true;
        teacherpanel.Visible = false;



        using (SqlConnection con = new SqlConnection(connetionString))
        {
            SqlCommand cmd = new SqlCommand("SELECT DISTINCT [rollno], [studentName], getdate() as attadate FROM [student] WHERE ([class] ='" + (ddlSections.SelectedValue) + "' )");

            SqlDataAdapter sda = new SqlDataAdapter();

            cmd.Connection = con;
            sda.SelectCommand = cmd;
            DataTable dt = new DataTable();

            sda.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }



    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected void btnAddS_Click(object sender, EventArgs e) {
        studpanel.Visible = true;
    }

    //Add new Student
    protected void btnAddnewStud(object sender, EventArgs e)
    {

        InsertStudent();
        newroll.Text = string.Empty;
        newname.Text = string.Empty;
        studpanel.Visible = false;
        refreshdata();
    }

    int InsertStudent()
    {
        using (SqlConnection myConnection = new SqlConnection(connetionString))
        {
            SqlCommand MyCommand = new SqlCommand("INSERT INTO student (rollno, studentName, class) Values (@roll, @student, @class)", myConnection);
            MyCommand.Parameters.AddWithValue("@class", ddlSections.SelectedValue);
            MyCommand.Parameters.AddWithValue("@roll", newroll.Text);

            MyCommand.Parameters.AddWithValue("@student",newname.Text);
            myConnection.Open();
            return MyCommand.ExecuteNonQuery();

        }
    }


    // save attendance

    protected void Save_Attendance(object sender, EventArgs e)
    {
        foreach (GridViewRow row in GridView1.Rows)
        {

            int rollno1 = Convert.ToInt32(row.Cells[0].Text);
            String studentname1 = row.Cells[1].Text;
            CheckBox cb1 = (row.Cells[3].FindControl("present") as CheckBox);

            String status1;
            if (cb1.Checked)
            {
                status1 = "Present";

            }
            else
            {
                status1 = "Absent";
            }
            String dateofclass1 = DateTime.Now.ToShortDateString();
            String sclass1 = ddlSections.SelectedItem.Text;
            saveattendance(rollno1, studentname1,  status1, sclass1);
        }
       
        refreshdata();



    }

    int saveattendance(int rollno, String studentname, String status, String sclass)
    {
        SqlConnection con = new SqlConnection(connetionString);
        SqlCommand cmd = new SqlCommand("INSERT INTO AttendanceSheet (rollno,studentName,date,attendance,class) values (@roll,@name,@date,@status,@class)", con);
        cmd.Parameters.AddWithValue("roll", rollno);
        cmd.Parameters.AddWithValue("name", studentname);
        cmd.Parameters.AddWithValue("date", DateTime.Now.ToShortDateString());
        cmd.Parameters.AddWithValue("status", status);
        cmd.Parameters.AddWithValue("class", sclass);

        con.Open();

        return cmd.ExecuteNonQuery();
     


    }



    // refresh the page
    void refreshdata()
    {
        Response.Redirect(Request.RawUrl);
    }
}