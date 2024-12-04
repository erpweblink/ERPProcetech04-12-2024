using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Admin_OAReportsForDays : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["name"] == null)
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            if (!this.IsPostBack)
            {
                GetOAreportsData();
            }
        }
    }

    public void GetOAreportsData()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand cmd = new SqlCommand("[SP_OAREPORTSNEW]", connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Action", "GetData"));
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable Dt = new DataTable();
                adapter.Fill(Dt);

                if (Dt.Rows.Count > 0)
                {
                    GVReports.DataSource = Dt;
                    GVReports.DataBind();
                }
            }
        }
    }
}
    
