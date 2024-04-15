using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Data.SqlClient;
using System.Windows.Forms;
using Label = System.Web.UI.WebControls.Label;
using TextBox = System.Web.UI.WebControls.TextBox;
using System.ComponentModel;
using System.Configuration;

//using System.Web.UI.WebControls.WebParts;
//using TextBox = System.Web.UI.WebControls.TextBox;

//using System.Windows.Forms;



//public static class MsgBox
//{   //this Page Page,
//    public static void Show( String sMessage)
//    {
//        //Page.ClientScript.RegisterStartupScript(
//        //   Page.GetType(),
//        //   "MessageBox",
//        //   "<script language='javascript'>alert('" + Message + "');</script>");
//        string msg = "<script language=\"javascript\">";
//        msg += "alert('" + sMessage + "');";
//        msg += "</script>";
//        Response.Write(msg);

//    }
//}

//public static void MsgBox(string sMessage)
//{
//    string msg = "<script language=\"javascript\">";
//    msg += "alert('" + sMessage + "');";
//    msg += "</script>";
//    Response.Write(msg);
//}
//public class Exception1 : System.Exception
//{
//    public class Class1
//    {
//        public static void logger(Exception ex) //getting error here
//        {


//        }
//    }
//}

public partial class Default2 : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(@"Data Source = 192.168.1.103; User ID = dev ; Password = Sq1@Ho34s ; Database = Demo ; Integrated Security = False ");
    SqlCommand cmd;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            displayData();
        }
    }



    public void displayData()
    {
        //con.Open();
        //DataTable dt = new DataTable();
        //SqlDataAdapter adapt = new SqlDataAdapter("SELECT * FROM EMPLOYEE", con);
        //adapt.Fill(dt);
        //table_data_grid.DataSource = dt;
        //con.Close();
        //con.Open();
        con.Open();
        try
        {
            string query = "SELECT * FROM Employee";
            SqlDataAdapter adapt = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            BoundField bfield = new BoundField();
            adapt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                table_data_grid.DataSource = dt;

                table_data_grid.DataBind();

            }

            else
            {
                table_data_grid.DataSource = null;
                table_data_grid.DataBind();

            }

           
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            con.Close();
        }
        
    }

    public void clearData()
    {
        emp_name.Text = "";
        job_desg.Text = "";
        mgr_id.Text = "";
        dept_no.Text = "";
        salary.Text = "";
    }


    protected void insert_btn_Click(object sender, EventArgs e)
    {


        //if (emp_name.Text == "" || job_desg.Text == "" || emp_id.Text == "" || mgr_id.Text == "" || dept_no.Text == "" || salary.Text == "")
        //{
        //    MessageBox.Show("Enter all the details");
        //}

        //else
        //{
        //try
        //{
        //string name_entered = emp_name.Text;

        //Label emp_name = (Label)table_data_grid.Rows[e.RowIndex].FindControl("emp_name");
        //Label designation = (Label)table_data_grid.Rows[e.RowIndex].FindControl("job_desg");
        //Label mgr_id = (Label)table_data_grid.Rows[e.RowIndex].FindControl("mgr_id");
        //Label dept_no = (Label)table_data_grid.Rows[e.RowIndex].FindControl("dept_no");
        //Label salary = (Label)table_data_grid.Rows[e.RowIndex].FindControl("salary");




        ////if(emp_name.Text != DbType.Object.)
        //string query = "IF NOT EXIST (SELECT * FROM EMPLOYEE WHERE EMP_NAME = '@EMP_NAME' " +
        //    "INSERT INTO EMPLOYEE ( EMP_NAME, JOB, MANAGER, DEPT_NO, SALARY ) VALUES('" 
        //    + emp_name.Text + "','" + job_desg.Text + "'," + int.Parse(mgr_id.Text) + "," 
        //    + int.Parse(dept_no.Text) + "," + int.Parse(salary.Text) + ")";



        //string query = "IF NOT EXISTS (SELECT * FROM EMPLOYEE WHERE EMP_NAME = "+ emp_name.Text +")"+
        //    "BEGIN INSERT INTO EMPLOYEE (EMP_NAME, JOB, MANAGER, DEPT_NO, SALARY)"+
        //    "VALUES ('" + emp_name.Text + "', '" + designation.Text + "', " + int.Parse(mgr_id.Text) + ", " + int.Parse(dept_no.Text) 
        //    + ", " + int.Parse(salary.Text) ")" +
        //    "END"+
        //    "ELSE"+
        //    "BEGIN PRINT 'RECORD ALREADY EXISTS'"+ 
        //    "END";

        //con.Open();

        //string query = "DECLARE @returnval INT" +
        //    "EXEC @returnval = EmpRecNotExst "+ emp_name.Text + "', '" + job_desg.Text + "', " + int.Parse(mgr_id.Text) + ", " + int.Parse(dept_no.Text)   
        //    + ", " + int.Parse(salary.Text);
        
        string message;
        int returnval;

        con.Open();
        SqlCommand cmd = new SqlCommand("EmpExists", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@emp_name", SqlDbType.NVarChar).Value = emp_name.Text;
        cmd.Parameters.Add("@job", SqlDbType.NVarChar).Value = job_desg.Text;
        cmd.Parameters.Add("@mngr", SqlDbType.NVarChar).Value = mgr_id.Text;
        cmd.Parameters.Add("@dept_no", SqlDbType.NVarChar).Value = dept_no.Text;
        cmd.Parameters.Add("@salary", SqlDbType.NVarChar).Value = salary.Text;

        object o = cmd.ExecuteScalar();

        if (o != null)
        {
            returnval = 1;
        }
        else
        {
            returnval = 0;
        }

        if (returnval == 1)
        {
            message = "Record Added Successfully!";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(message);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        }

        else if (returnval == 0)
        {
            message = "Record Already Present!";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(message);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        }

        else
        {
            message = "Something went wrong! Record could not be added.";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(message);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        }

        con.Close();

        displayData();
        clearData();
        //string constr = ConfigurationManager.ConnectionStrings["Myconnection"].ToString();
        //using (SqlConnection connec = new SqlConnection(constr))
        //{
        //    using (SqlCommand command = new SqlCommand("EmpExists", connec))
        //    {
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.Parameters.Add("@emp_name", SqlDbType.NVarChar).Value = emp_name.Text;
        //        command.Parameters.Add("@job", SqlDbType.NVarChar).Value = job_desg.Text;
        //        command.Parameters.Add("@mngr", SqlDbType.NVarChar).Value = mgr_id.Text;
        //        command.Parameters.Add("@dept_no", SqlDbType.NVarChar).Value = dept_no.Text;
        //        command.Parameters.Add("@salary", SqlDbType.NVarChar).Value = salary.Text;

        //        connec.Open();
        //        object o = command.ExecuteScalar();

        //        if (o != null)
        //        {
        //            returnval = int.Parse(o.ToString());
        //            //message = "Record Added Successfully!";
        //            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //            //sb.Append("<script type = 'text/javascript'>");
        //            //sb.Append("window.onload=function(){");
        //            //sb.Append("alert('");
        //            //sb.Append(message);
        //            //sb.Append("')};");
        //            //sb.Append("</script>");
        //            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        //            //connec.Close();
        //            //displayData();
        //            //clearData();
        //        }
        //        else
        //        {
        //            returnval = 0;
        //        }


        //        if (returnval == 1)
        //        {
        //            message = "Record Added Successfully!";
        //            System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //            sb.Append("<script type = 'text/javascript'>");
        //            sb.Append("window.onload=function(){");
        //            sb.Append("alert('");
        //            sb.Append(message);
        //            sb.Append("')};");
        //            sb.Append("</script>");
        //            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        //        }

        //        else if (returnval == 0)
        //        {
        //            message = "Record Already Present!";
        //            System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //            sb.Append("<script type = 'text/javascript'>");
        //            sb.Append("window.onload=function(){");
        //            sb.Append("alert('");
        //            sb.Append(message);
        //            sb.Append("')};");
        //            sb.Append("</script>");
        //            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        //        }

        //        else
        //        {
        //            message = "Something went wrong! Record could not be added.";
        //            System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //            sb.Append("<script type = 'text/javascript'>");
        //            sb.Append("window.onload=function(){");
        //            sb.Append("alert('");
        //            sb.Append(message);
        //            sb.Append("')};");
        //            sb.Append("</script>");
        //            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        //        }
        //        //else
        //        //{
        //        //    message = "Record already present!";
        //        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //        //    sb.Append("<script type = 'text/javascript'>");
        //        //    sb.Append("window.onload=function(){");
        //        //    sb.Append("alert('");
        //        //    sb.Append(message);
        //        //    sb.Append("')};");
        //        //    sb.Append("</script>");
        //        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        //        //    connec.Close();
        //        //    displayData();
        //        //    clearData();
        //        //}
        //    }
        //}

        //using (SqlCommand command = new SqlCommand("EmpExists", connec))
        //    {
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.Parameters.Add("@emp_name", SqlDbType.NVarChar).Value = emp_name.Text;
        //        command.Parameters.Add("@job", SqlDbType.NVarChar).Value = job_desg.Text;
        //        command.Parameters.Add("@mngr", SqlDbType.NVarChar).Value = mgr_id.Text;
        //        command.Parameters.Add("@dept_no", SqlDbType.NVarChar).Value = dept_no.Text;
        //        command.Parameters.Add("@salary", SqlDbType.NVarChar).Value = salary.Text;

        //        connec.Open();
        //        object o = command.ExecuteScalar();

        //        if (o != null)
        //        {
        //            message = "Record Added Successfully!";
        //            System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //            sb.Append("<script type = 'text/javascript'>");
        //            sb.Append("window.onload=function(){");
        //            sb.Append("alert('");
        //            sb.Append(message);
        //            sb.Append("')};");
        //            sb.Append("</script>");
        //            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        //            connec.Close();
        //            displayData();
        //            clearData();
        //        }
        //        else
        //        {
        //            message = "Record already present!";
        //            System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //            sb.Append("<script type = 'text/javascript'>");
        //            sb.Append("window.onload=function(){");
        //            sb.Append("alert('");
        //            sb.Append(message);
        //            sb.Append("')};");
        //            sb.Append("</script>");
        //            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        //            connec.Close();
        //            displayData();
        //            clearData();
        //        }
        //    }
        //string message; 

        //SqlCommand cmd = new SqlCommand(query, con);
        //SqlParameter param = new SqlParameter("@returnval", System.Data.SqlDbType.Int);
        //param.Direction = System.Data.ParameterDirection.ReturnValue;
        //cmd.Parameters.Add(param);
        //cmd.ExecuteNonQuery();

        //if (param == 1)
        //{
        //    message = "Record Added Successfully!";
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    sb.Append("<script type = 'text/javascript'>");
        //    sb.Append("window.onload=function(){");
        //    sb.Append("alert('");
        //    sb.Append(message);
        //    sb.Append("')};");
        //    sb.Append("</script>");
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        //}

        //else if (param == 0)
        //{
        //    message = "Record Already Present!";
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    sb.Append("<script type = 'text/javascript'>");
        //    sb.Append("window.onload=function(){");
        //    sb.Append("alert('");
        //    sb.Append(message);
        //    sb.Append("')};");
        //    sb.Append("</script>");
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        //}

        //else
        //{
        //    message = "Something went wrong! Record could not be added.";
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //    sb.Append("<script type = 'text/javascript'>");
        //    sb.Append("window.onload=function(){");
        //    sb.Append("alert('");
        //    sb.Append(message);
        //    sb.Append("')};");
        //    sb.Append("</script>");
        //    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        //}






        //string query = "INSERT INTO EMPLOYEE (Emp_nwordo, Emp_name, job, manager, dept_no, salary ) VALUES (@emp_name, @job, @emp_id, @manager, @dept_no, @salary)";

        //cmd.Parameters.AddWithValue("@EMP_NAME", emp_name.Text);

        //cmd.Parameters.AddWithValue("@emp_name", emp_name.Text);
        //cmd.Parameters.AddWithValue("@job", job_desg.Text);    
        //cmd.Parameters.AddWithValue("@emp_id", emp_id);
        //cmd.Parameters.AddWithValue("@manager", mgr_id.Text);
        //cmd.Parameters.AddWithValue("@dept_no", dept_no.Text);
        //cmd.Parameters.AddWithValue("@salary", salary.Text);



        // string message = "Simple MessageBox";
        //MsgBox.Show(this,message);
        //MessageBox.Show("Record Updated Successfully");


        //Console.WriteLine("your data has been saved");

        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine(ex);
        //}



        //string message = "Record Added!";
        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //sb.Append("<script type = 'text/javascript'>");
        //sb.Append("window.onload=function(){");
        //sb.Append("alert('");
        //sb.Append(message);
        //sb.Append("')};");
        //sb.Append("</script>");
        //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
    }


    //protected void update_btn_Click(object sender, EventArgs e)
        //{
        //    con.Open();

        //    //if (emp_name.Text == "" || job_desg.Text == "" || emp_id.Text == "" || mgr_id.Text == "" || dept_no.Text == "" || salary.Text == "")
        //    //{
        //    //    MessageBox.Show("Enter all the details");
        //    //}

        //    //else
        //    //{
        //    try
        //    {
        //        //UPDATE Customers
        //        //SET ContactName = 'Alfred Schmidt', City = 'Frankfurt'
        //        //WHERE CustomerID = 1;
        //        string query = "UPDATE INTO EMPLOYEE ( EMP_NAME, JOB,EMP_NO, MANAGER, DEPT_NO, SALARY )
        //        VALUES('" + emp_name.Text + "','" + job_desg.Text + "'," + int.Parse(emp_id.Text) + ","
        //        + int.Parse(mgr_id.Text) + "," + int.Parse(dept_no.Text) + "," + int.Parse(salary.Text) + ")";
        //        //string query = "INSERT INTO EMPLOYEE (Emp_nwordo, Emp_name, job, manager, dept_no, salary )
        //        VALUES (@emp_name, @job, @emp_id, @manager, @dept_no, @salary)";
        //        cmd = new SqlCommand(query, con);

        //        //cmd.Parameters.AddWithValue("@emp_name", emp_name.Text);
        //        //cmd.Parameters.AddWithValue("@job", job_desg.Text);    
        //        //cmd.Parameters.AddWithValue("@emp_id", emp_id);
        //        //cmd.Parameters.AddWithValue("@manager", mgr_id.Text);
        //        //cmd.Parameters.AddWithValue("@dept_no", dept_no.Text);
        //        //cmd.Parameters.AddWithValue("@salary", salary.Text);

        //        cmd.ExecuteNonQuery();
        //        // string message = "Simple MessageBox";
        //        //MsgBox.Show(this,message);
        //        con.Close();
        //        Console.WriteLine("your data has been saved");

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }
        //    con.Close();

        //}
    protected void table_data_grid_RowEditing(object sender, GridViewEditEventArgs e)
    {
       table_data_grid.EditIndex = e.NewEditIndex;
       displayData();
    }

    protected void table_data_grid_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            //GridViewRow row = (GridViewRow)table_data_grid.Rows[e.RowIndex]; 
            //TextBox emp_no = (TextBox)row.FindControl("txt_Emp_no");
            //string emp_no1 = (table_data_grid.Rows[e.RowIndex].FindControl("lblemp_no") as Label).Text;
            //TextBox emp_no = (TextBox)table_data_grid.Rows[e.RowIndex].FindControl("txt_Emp_no");
            Label employee_no = (Label)table_data_grid.Rows[e.RowIndex].FindControl("lbl_emp_no");
            TextBox employee_name = (TextBox)table_data_grid.Rows[e.RowIndex].FindControl("txt_Emp_name");
            TextBox designation = (TextBox)table_data_grid.Rows[e.RowIndex].FindControl("txt_Designation");
            TextBox mgr_id = (TextBox)table_data_grid.Rows[e.RowIndex].FindControl("txt_mgr_id");
            TextBox dept_no = (TextBox)table_data_grid.Rows[e.RowIndex].FindControl("txt_dept_no");
            TextBox salary = (TextBox)table_data_grid.Rows[e.RowIndex].FindControl("txt_Salary");

            con.Open();



            //string query = "update employee set emp_name ='" + employee_name.Text +
            //    "', job = '" + designation.Text + "', manager = " + int.Parse(mgr_id.Text) + ", dept_no = " + int.Parse(dept_no.Text)
            //    +", salary = " + int.Parse(salary.Text) + " where emp_no = " + int.Parse(employee_no.Text);

            //string query = "UPDATE EMPLOYEE SET EMP_NAME = '" + employee_name.Text + "', JOB = '" + designation.Text + "' , " +
            //    "MANAGER = " + int.Parse(mgr_id.Text) + ", DEPT_NO = " + int.Parse(dept_no.Text) + 
            //    ", SALARY= " +int.Parse(salary.Text) +" WHERE EMP_NO = " + int.Parse(employee_no.Text);




            if (string.IsNullOrEmpty(employee_name.Text))
            {
                //query = "UPDATE EMPLOYEE (EMP_NAME) VALUES (@empty) WHERE EMP_NO = " + employee_no.Text;
                //SqlCommand cmd1;
                //cmd1 = new SqlCommand(query, con);
                //cmd1.Parameters.AddWithValue("@empty", SqlDbType.VarChar).Value = DBNull.Value;
                //cmd1.ExecuteNonQuery();
                //table_data_grid.EditIndex = -1;

                //employee_name.Text = "";
                employee_name.Text = string.Empty;
            }

            if (string.IsNullOrEmpty(designation.Text))
            {
                //query = "UPDATE EMPLOYEE (JOB) VALUES (@empty) WHERE EMP_NO = " + employee_no.Text;
                //SqlCommand cmd1;
                //cmd1 = new SqlCommand(query, con);
                //cmd1.Parameters.AddWithValue("@empty", SqlDbType.VarChar).Value = DBNull.Value;
                //cmd1.ExecuteNonQuery();
                //table_data_grid.EditIndex = -1;
                //designation.Text = "";
                designation.Text = string.Empty;

            }

            if (string.IsNullOrEmpty(mgr_id.Text))
            {
                //query = "UPDATE EMPLOYEE (MANAGER) VALUES (@empty) WHERE EMP_NO = " + employee_no.Text;
                //SqlCommand cmd1;
                //cmd1 = new SqlCommand(query, con);
                //cmd1.Parameters.AddWithValue("@empty", SqlDbType.VarChar).Value = DBNull.Value;
                //cmd1.ExecuteNonQuery();
                //table_data_grid.EditIndex = -1;
                mgr_id.Text = "0";

            }

            if (string.IsNullOrEmpty(dept_no.Text))
            {
                //query = "UPDATE EMPLOYEE (DEPT_NO) VALUES (@empty) WHERE EMP_NO = " + employee_no.Text;
                //SqlCommand cmd1;
                //cmd1 = new SqlCommand(query, con);
                //cmd1.Parameters.AddWithValue("@empty", SqlDbType.VarChar).Value = DBNull.Value;
                //cmd1.ExecuteNonQuery();
                //table_data_grid.EditIndex = -1;

                //dept_no.Text = "";
                dept_no.Text = "0";

            }

            if (string.IsNullOrEmpty(salary.Text))
            {
                //query = "UPDATE EMPLOYEE (SALARY) VALUES (@empty) WHERE EMP_NO = " + employee_no.Text;
                //SqlCommand cmd1;
                //cmd1 = new SqlCommand(query, con);
                //cmd1.Parameters.AddWithValue("@empty", SqlDbType.VarChar).Value = DBNull.Value;
                //cmd1.ExecuteNonQuery();
                //table_data_grid.EditIndex = -1;

                //salary.Text = "";
                salary.Text = "0";

            }



            SqlCommand cmd = new SqlCommand("UPDATE EMPLOYEE SET EMP_NAME = '" + employee_name.Text +
                   "', JOB = '" + designation.Text +
                   "' , MANAGER = " + int.Parse(mgr_id.Text) +
                   ", DEPT_NO = " + int.Parse(dept_no.Text) +
                   ", SALARY = " + int.Parse(salary.Text) +
                   " WHERE EMP_NO = " + int.Parse(employee_no.Text), con);

            cmd.ExecuteNonQuery();
            string message = "Record Updated!";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(message);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

        }

        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        finally
        {
            table_data_grid.EditIndex = -1;
            con.Close();
            displayData();
        }
    }


    protected void table_data_grid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        table_data_grid.EditIndex = -1;
        displayData();
    }


    protected void table_data_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Label employee_no = (Label)table_data_grid.Rows[e.RowIndex].FindControl("lbl_emp_no");
        con.Open();
        SqlCommand cmd = new SqlCommand("EXEC Emp_record_delete " + int.Parse(employee_no.Text), con);
        cmd.ExecuteNonQuery();


        string message = "Record Deleted!";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.onload=function(){");
        sb.Append("alert('");
        sb.Append(message);
        sb.Append("')};");
        sb.Append("</script>");
        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        con.Close();
        displayData();


            //TextBox employee_name = (TextBox)table_data_grid.Rows[e.RowIndex].FindControl("txt_Emp_name");
            //con.Open();
            //////SqlCommand cmd = new SqlCommand("DELETE FROM EMPLOYEE WHERE EMP_ID = " + int.Parse(employee_no.Text),con);
            //string delete_query = "delete from employee where emp_id = " + int.Parse(employee_no.Text);

            //SqlCommand cmd = new SqlCommand(delete_query, con);
            //cmd.ExecuteNonQuery();

            //SqlParameter p = new SqlParameter("@ID", int.Parse(employee_no.Text));
            //cmd.Parameters.Add(p);
            //cmd.Parameters.AddWithValue("@Id", int.Parse(employee_no.Text));


            //int.Parse(employee_no.Text)

            //table_data_grid.DataSource = dt;


            ////cmd.ExecuteNonQuery();
            //con.Close();
            //table_data_grid.EditIndex = -1;




            //int index = Convert.ToInt32(e.RowIndex);
            //DataTable dt = ViewState["dt"] as DataTable;
            //dt.Rows[index].Delete();
            //ViewState["dt"] = dt;
            //DataBind();




    }



    //protected void table_data_grid_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        foreach (TableCell cell in e.Row.Cells)
    //        {
    //            if (string.IsNullOrEmpty(cell.Text))
    //            {
    //                bool v = cell.Text == "--";

    //            }
    //        }
    //    }
    //}

    protected void table_data_grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label display_Emp_name = (Label)table_data_grid.Rows[e.RowIndex].FindControl("display_Emp_name");
                //Label display_Designation = (Label)table_data_grid.Rows[e.RowIndex].FindControl("display_Emp_name");
                //Label display_Emp_name = (Label)table_data_grid.Rows[e.RowIndex].FindControl("display_Emp_name");
                //Label display_Emp_name = (Label)table_data_grid.Rows[e.RowIndex].FindControl("display_Emp_name");
                //Label display_Emp_name = (Label)table_data_grid.Rows[e.RowIndex].FindControl("display_Emp_name");
                
                Label display_Emp_no = (Label)e.Row.Cells[0].FindControl("lbl_emp_no");
                Label display_Emp_name = (Label)e.Row.Cells[0].FindControl("lbl_Emp_name");
                Label display_Designation = (Label)e.Row.Cells[0].FindControl("lbl_Designation");
                Label display_mgr_id = (Label)e.Row.Cells[0].FindControl("lbl_mgr_id");
                Label display_dept_no = (Label)e.Row.Cells[0].FindControl("lbl_dept_no");
                Label display_Salary = (Label)e.Row.Cells[0].FindControl("lbl_Salary");
            
                

                //if (display_Emp_name.Text == "")
                //{
                //    display_Emp_name.Text = "--";
                //}

                //if (display_Designation.Text == "")
                //{
                //    display_Designation.Text = "--";
                //}

                //if (display_mgr_id.Text == "")
                //{
                //    display_mgr_id.Text = "--";
                //}

                //if (display_dept_no.Text == "")
                //{
                //    display_dept_no.Text = "--";
                //}

                //if (display_Salary.Text == "")
                //{
                //    display_Salary.Text = "--";
                //}

                if (string.IsNullOrEmpty(display_Emp_name.Text))
                {
                    display_Emp_name.Text = "--";
                    //query = "INSERT INTO EMPLOYEE (JOB) VALUES (@empty) WHERE EMP_NO = " + display_Emp_no.Text;
                    //cmd = new SqlCommand(query, con);
                    //cmd.Parameters.AddWithValue("@empty", SqlDbType.VarChar).Value = DBNull.Value;
                    //cmd.ExecuteNonQuery();

                }
                if (string.IsNullOrEmpty(display_Designation.Text))
                {
                    display_Designation.Text = "--";
                    //query = "INSERT INTO EMPLOYEE (JOB) VALUES (@empty) WHERE EMP_NO = " + display_Emp_no.Text;
                    //cmd = new SqlCommand(query, con);
                    //cmd.Parameters.AddWithValue("@empty", SqlDbType.VarChar).Value = DBNull.Value;
                    //cmd.ExecuteNonQuery();
                }
                if (int.Parse(display_mgr_id.Text) == 0)
                {
                    //string.IsNullOrEmpty(display_mgr_id.Text)
                    display_mgr_id.Text = "--";
                    //query = "INSERT INTO EMPLOYEE (MANAGER) VALUES (@empty) WHERE EMP_NO = " + display_Emp_no.Text;
                    //cmd = new SqlCommand(query, con);
                    //cmd.Parameters.AddWithValue("@empty", SqlDbType.Int).Value = DBNull.Value;
                    //cmd.ExecuteNonQuery();
                }
                if (int.Parse(display_dept_no.Text) == 0)
                {
                    display_dept_no.Text = "--";
                    //query = "INSERT INTO EMPLOYEE (DEPT_NO) VALUES (@empty) WHERE EMP_NO = " + display_Emp_no.Text;
                    //cmd = new SqlCommand(query, con);
                    //cmd.Parameters.AddWithValue("@empty", SqlDbType.Int).Value = DBNull.Value;
                    //cmd.ExecuteNonQuery();

                }
                if (int.Parse(display_Salary.Text) == 0)
                {
                    display_Salary.Text = "--";
                    //query = "INSERT INTO EMPLOYEE (SALARY) VALUES (@empty) WHERE EMP_NO = " + display_Emp_no.Text;
                    //cmd = new SqlCommand(query, con);
                    //cmd.Parameters.AddWithValue("@empty", SqlDbType.Int).Value = DBNull.Value;
                    //cmd.ExecuteNonQuery();

                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        finally
        {
            table_data_grid.EditIndex = -1;
        }
        
        

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    foreach (TableCell cell in e.Row.Cells)
        //    {
        //        if (string.IsNullOrEmpty(cell.Text))
        //        {
        //            cell.Text = "--";

        //        }
        //    }
        //    displayData();
        //}
    }
}
