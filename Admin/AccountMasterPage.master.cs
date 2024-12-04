using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_AccountMasterPage : System.Web.UI.MasterPage
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
            //01/10/2021

            lblusername.Text = Session["ProductionName"].ToString();

            if (lblusername.Text == "Admin")
            {
                liAdminlink.Visible = true;
            }
            else
            {
                liAdminlink.Visible = false;
            }
			 string count = "";
            SqlCommand cmd = new SqlCommand("select count(id) as Cnt from tblPaymentModule where IsComplete is null", con);
            con.Open();
            count = cmd.ExecuteScalar().ToString();

            lblReqCount.InnerText = count;
            con.Close();
            //string roleName = Session["RoleName"].ToString();

            //liDrawing.Visible = false;
            //liLaserProgramming.Visible = false;
            //liLaserCutting.Visible = false;
            //liBending.Visible = false;
            //liWelding.Visible = false;
            //liPowderCoating.Visible = false;
            //liassembly.Visible = false;
            //li1.Visible = false;

            //foreach (string item in roleName.Split(new char[] { ',' }))
            //{
            //    if (item.Trim() == "drawing")
            //    {
            //        liDrawing.Visible = true;
            //        //liLaserProgramming.Visible = false;
            //        //liLaserCutting.Visible = false;
            //        //liBending.Visible = false;
            //        //liWelding.Visible = false;
            //        //liPowderCoating.Visible = false;
            //        //liassembly.Visible = false;
            //        //li1.Visible = false;
            //    }
            //    else if (item.Trim() == "laserprogramming")
            //    {
            //        //liDrawing.Visible = false;
            //        liLaserProgramming.Visible = true;
            //        //liLaserCutting.Visible = false;
            //        //liBending.Visible = false;
            //        //liWelding.Visible = false;
            //        //liPowderCoating.Visible = false;
            //        //liassembly.Visible = false;
            //        //li1.Visible = false;
            //    }
            //    else if (item.Trim() == "lasercutting")
            //    {
            //        //liDrawing.Visible = false;
            //        //liLaserProgramming.Visible = false;
            //        liLaserCutting.Visible = true;
            //        //liBending.Visible = false;
            //        //liWelding.Visible = false;
            //        //liPowderCoating.Visible = false;
            //        //liassembly.Visible = false;
            //        //li1.Visible = false;
            //    }
            //    else if (item.Trim() == "bending")
            //    {
            //        //liDrawing.Visible = false;
            //        //liLaserProgramming.Visible = false;
            //        //liLaserCutting.Visible = false;
            //        liBending.Visible = true;
            //        //liWelding.Visible = false;
            //        //liPowderCoating.Visible = false;
            //        //liassembly.Visible = false;
            //        //li1.Visible = false;
            //    }
            //    else if (item.Trim() == "welding")
            //    {
            //        //liDrawing.Visible = false;
            //        //liLaserProgramming.Visible = false;
            //        //liLaserCutting.Visible = false;
            //        //liBending.Visible = false;
            //        liWelding.Visible = true;
            //        //liPowderCoating.Visible = false;
            //        //liassembly.Visible = false;
            //        //li1.Visible = false;
            //    }
            //    else if (item.Trim() == "powdercoating")
            //    {
            //        //liDrawing.Visible = false;
            //        //liLaserProgramming.Visible = false;
            //        //liLaserCutting.Visible = false;
            //        //liBending.Visible = false;
            //        //liWelding.Visible = false;
            //        liPowderCoating.Visible = true;
            //        //liassembly.Visible = false;
            //        //li1.Visible = false;
            //    }
            //    else if (item.Trim() == "assembly")
            //    {
            //        //liDrawing.Visible = false;
            //        //liLaserProgramming.Visible = false;
            //        //liLaserCutting.Visible = false;
            //        //liBending.Visible = false;
            //        //liWelding.Visible = false;
            //        //liPowderCoating.Visible = false;
            //        //li1.Visible = false;
            //        liassembly.Visible = true;

            //    }
            //    else if (item.Trim() == "Admin")
            //    {
            //        liDrawing.Visible = true;
            //        liLaserProgramming.Visible = true;
            //        liLaserCutting.Visible = true;
            //        liBending.Visible = true;
            //        liWelding.Visible = true;
            //        liPowderCoating.Visible = true;
            //        liassembly.Visible = true;
            //        li1.Visible = true;
            //    }
            //}
        }
    }

    protected void adminDashboard_Click(object sender, EventArgs e)
    {
        Response.Redirect("../Admin/AdminDashboard.aspx");
    }
}
