using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_FinalInspectionReport : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
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


            }
        }
    }

    protected void txtsubmit_Click(object sender, EventArgs e)
    {
        try
        {

            if (txtsubmit.Text == "Submit")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("[SP_FinalInspection]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@jo", txtjo.Text);
                cmd.Parameters.AddWithValue("@customerName", txtcustomer.Text);
                cmd.Parameters.AddWithValue("@Po", txtpo.Text);
                cmd.Parameters.AddWithValue("@Project", txtproject.Text);
                cmd.Parameters.AddWithValue("@Item", txtitem.Text);
                cmd.Parameters.AddWithValue("@Qty", txtQty.Text);
                cmd.Parameters.AddWithValue("@DrgNo", txtdrag.Text);
                cmd.Parameters.AddWithValue("@Date", txtdate.Text);
                cmd.Parameters.Add("@Iddd", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.Parameters.AddWithValue("@Action", "Insertdetails");
                cmd.ExecuteNonQuery();
                con.Close();

                int id = Convert.ToInt32(cmd.Parameters["@Iddd"].Value);
                ////Save Product Details 
                   con.Open();

                SqlCommand cmdd = new SqlCommand("SP_FinalInspection", con);
                cmdd.CommandType = CommandType.StoredProcedure;
                cmdd.Parameters.AddWithValue("@Action", "Inseroderdetails");
                cmdd.Parameters.AddWithValue("@HeaderID", id);
                //cmdd.Parameters.AddWithValue("@AssemblyName1", BareEnc.InnerText);
                //cmdd.Parameters.AddWithValue("@AssemblyName2", overalldimensioncheck.InnerText);
                cmdd.Parameters.AddWithValue("@AssemblyName3", BEHEIGHT.InnerText);
                string checkHeght = chekckHeightBE.Checked ? "Pass" : "Fail";
                cmdd.Parameters.AddWithValue("@specification", checkHeght);
                cmdd.Parameters.AddWithValue("@Observation", txtheightobservationBE.Text);
                cmdd.Parameters.AddWithValue("@Result", txtheightresult.Text);
                cmdd.Parameters.AddWithValue("@Test", BareEnc.InnerText);
                cmdd.ExecuteNonQuery();
                cmdd.Parameters.Clear();
                //Width
                SqlCommand cmdd2 = new SqlCommand("SP_FinalInspection", con);
                cmdd2.CommandType = CommandType.StoredProcedure;
                cmdd2.Parameters.AddWithValue("@Action", "Inseroderdetails");
                cmdd2.Parameters.AddWithValue("@HeaderID", id);
                //cmdd2.Parameters.AddWithValue("@AssemblyName1", BareEnc.InnerText);
                //cmdd2.Parameters.AddWithValue("@AssemblyName2", overalldimensioncheck.InnerText);
                cmdd2.Parameters.AddWithValue("@AssemblyName3", BEWIDTH.InnerText);
                string check = txtwidthcheck.Checked ? "Pass" : "Fail";
                cmdd2.Parameters.AddWithValue("@specification", check);
                cmdd2.Parameters.AddWithValue("@Observation", txtwidthobservation.Text);
                cmdd2.Parameters.AddWithValue("@Result", txwidthresult.Text);
                cmdd2.Parameters.AddWithValue("@Test", BareEnc.InnerText);
                //cmdd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                cmdd2.ExecuteNonQuery();

                cmdd2.Parameters.Clear();
                //Depth
                SqlCommand cmdd3 = new SqlCommand("SP_FinalInspection", con);
                cmdd3.CommandType = CommandType.StoredProcedure;
                cmdd3.Parameters.AddWithValue("@Action", "Inseroderdetails");
                cmdd3.Parameters.AddWithValue("@HeaderID", id);
                //cmdd3.Parameters.AddWithValue("@AssemblyName1", BareEnc.InnerText);
                //cmdd3.Parameters.AddWithValue("@AssemblyName2", overalldimensioncheck.InnerText);
                cmdd3.Parameters.AddWithValue("@AssemblyName3", BEDEPTH.InnerText);
                string check1 = chkBEDEPTH.Checked ? "Pass" : "Fail";
                cmdd3.Parameters.AddWithValue("@specification", check1);
                cmdd3.Parameters.AddWithValue("@Observation", txtDepthobser.Text);
                cmdd3.Parameters.AddWithValue("@Result", txtdepthresult.Text);
                cmdd3.Parameters.AddWithValue("@Test", BareEnc.InnerText);

                //cmdd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                cmdd3.ExecuteNonQuery();
         
                cmdd3.Parameters.Clear();

                //Cut Out
                SqlCommand cmdd4 = new SqlCommand("SP_FinalInspection", con);
                cmdd4.CommandType = CommandType.StoredProcedure;
                cmdd4.Parameters.AddWithValue("@Action", "Inseroderdetails");
                cmdd4.Parameters.AddWithValue("@HeaderID", id);
                //cmdd4.Parameters.AddWithValue("@AssemblyName1", BareEnc.InnerText);
                //cmdd4.Parameters.AddWithValue("@AssemblyName2", overalldimensioncheck.InnerText);
                cmdd4.Parameters.AddWithValue("@AssemblyName3", BECUTOUT.InnerText);
                string check2 = CheckBECUTOUT.Checked ? "Pass" : "Fail";
                cmdd4.Parameters.AddWithValue("@specification", check2);
                cmdd4.Parameters.AddWithValue("@Observation", txtcutoutobservation.Text);
                cmdd4.Parameters.AddWithValue("@Result", txtcutoutresult.Text);
                cmdd4.Parameters.AddWithValue("@Test", BareEnc.InnerText);
                //cmdd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                cmdd4.ExecuteNonQuery();
        
                cmdd4.Parameters.Clear();
                //PaintShade
                SqlCommand cmdd5 = new SqlCommand("SP_FinalInspection", con);
                cmdd5.CommandType = CommandType.StoredProcedure;
                cmdd5.Parameters.AddWithValue("@Action", "Inseroderdetails");
                cmdd5.Parameters.AddWithValue("@HeaderID", id);
                //cmdd5.Parameters.AddWithValue("@AssemblyName1", BareEnc.InnerText);
                //cmdd5.Parameters.AddWithValue("@AssemblyName2", overalldimensioncheck.InnerText);
                cmdd5.Parameters.AddWithValue("@AssemblyName3", PaintShade.InnerText);
                string check3 = checkPaintshade.Checked ? "Pass" : "Fail";
                cmdd5.Parameters.AddWithValue("@specification", check2);
                cmdd5.Parameters.AddWithValue("@Observation", txtpainshadeobservation.Text);
                cmdd5.Parameters.AddWithValue("@Result", txtpaintresultpainshit.Text);
                cmdd5.Parameters.AddWithValue("@Test", BareEnc.InnerText);
                //cmdd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                cmdd5.ExecuteNonQuery();

                cmdd5.Parameters.Clear();
                //PaintThickness
                SqlCommand cmdd6 = new SqlCommand("SP_FinalInspection", con);
                cmdd6.CommandType = CommandType.StoredProcedure;
                cmdd6.Parameters.AddWithValue("@Action", "Inseroderdetails");
                cmdd6.Parameters.AddWithValue("@HeaderID", id);
                //cmdd6.Parameters.AddWithValue("@AssemblyName1", BareEnc.InnerText);
                //cmdd6.Parameters.AddWithValue("@AssemblyName2", overalldimensioncheck.InnerText);
                cmdd6.Parameters.AddWithValue("@AssemblyName3", PaintThickness.InnerText);
                string check4 = checkThickness.Checked ? "Pass" : "Fail";
                cmdd6.Parameters.AddWithValue("@specification", check2);
                cmdd6.Parameters.AddWithValue("@Observation", txtobservationThickness.Text);
                cmdd6.Parameters.AddWithValue("@Result", txtresultThickness.Text);
                cmdd6.Parameters.AddWithValue("@Test", BareEnc.InnerText);

                //cmdd.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                cmdd6.ExecuteNonQuery();
          
                cmdd6.Parameters.Clear();


                //B section
                //SqlCommand cmdd7 = new SqlCommand("SP_FinalInspection", con);
                //cmdd7.CommandType = CommandType.StoredProcedure;
                //cmdd7.Parameters.AddWithValue("@Action", "Inseroderdetails");
                //cmdd7.Parameters.AddWithValue("@HeaderID", id);
                //cmdd7.Parameters.AddWithValue("@AssemblyName1", InernalAssembky.InnerText);
                ////cmdd7.Parameters.AddWithValue("@AssemblyName2","Null");
                ////cmdd7.Parameters.AddWithValue("@AssemblyName3", "Null");
                //string checkHeght1= checkMountingplate.Checked ? "Pass" : "Fail";
                //cmdd7.Parameters.AddWithValue("@specification", checkHeght1);
                //cmdd7.Parameters.AddWithValue("@Observation", "");
                //cmdd7.Parameters.AddWithValue("@Result","");
                //cmdd7.ExecuteNonQuery();
                //cmdd7.Parameters.Clear();

                //Mountingplate
                SqlCommand cmdd8 = new SqlCommand("SP_FinalInspection", con);
                cmdd8.CommandType = CommandType.StoredProcedure;
                cmdd8.Parameters.AddWithValue("@Action", "Inseroderdetails");
                cmdd8.Parameters.AddWithValue("@HeaderID", id);
                cmdd8.Parameters.AddWithValue("@AssemblyName1", MountingPlate.InnerText);
                //cmdd8.Parameters.AddWithValue("@AssemblyName2", "Null");
                //cmdd8.Parameters.AddWithValue("@AssemblyName3", "Null");
                string check5 = checkMountingplate.Checked ? "Pass" : "Fail";
                cmdd8.Parameters.AddWithValue("@specification", check5);
                cmdd8.Parameters.AddWithValue("@Observation", txtmountionobservation.Text);
                cmdd8.Parameters.AddWithValue("@Result", txtresultmountiong.Text);
                cmdd8.Parameters.AddWithValue("@Test", InernalAssembky.InnerText);
                cmdd8.ExecuteNonQuery();
                cmdd8.Parameters.Clear();

                //WireSupport
                SqlCommand cmdd9 = new SqlCommand("SP_FinalInspection", con);
                cmdd9.CommandType = CommandType.StoredProcedure;
                cmdd9.Parameters.AddWithValue("@Action", "Inseroderdetails");
                cmdd9.Parameters.AddWithValue("@HeaderID", id);
                cmdd9.Parameters.AddWithValue("@AssemblyName1", WireSupport.InnerText);
                //cmdd9.Parameters.AddWithValue("@AssemblyName2", "Null");
                //cmdd9.Parameters.AddWithValue("@AssemblyName3", "Null");
                string check6 = checkMountingplate.Checked ? "Pass" : "Fail";
                cmdd9.Parameters.AddWithValue("@specification", check6);
                cmdd9.Parameters.AddWithValue("@Observation", txtWireSupportobservation.Text);
                cmdd9.Parameters.AddWithValue("@Result", txtWireSupportresult.Text);
                cmdd9.Parameters.AddWithValue("@Test", InernalAssembky.InnerText);
                cmdd9.ExecuteNonQuery();
                cmdd9.Parameters.Clear();

                //DrawingPacket
                SqlCommand cmd10 = new SqlCommand("SP_FinalInspection", con);
                cmd10.CommandType = CommandType.StoredProcedure;
                cmd10.Parameters.AddWithValue("@Action", "Inseroderdetails");
                cmd10.Parameters.AddWithValue("@HeaderID", id);
                cmd10.Parameters.AddWithValue("@AssemblyName1", DrawingPacket.InnerText);
                //cmd10.Parameters.AddWithValue("@AssemblyName2", "Null");
                //cmd10.Parameters.AddWithValue("@AssemblyName3", "Null");
                string check7 = checkDrawingPacket.Checked ? "Pass" : "Fail";
                cmd10.Parameters.AddWithValue("@specification", check7);
                cmd10.Parameters.AddWithValue("@Observation", txtobservationDrawingPacket.Text);
                cmd10.Parameters.AddWithValue("@Result", txtresultDrawingPacket.Text);
                cmd10.Parameters.AddWithValue("@Test", InernalAssembky.InnerText);
                cmd10.ExecuteNonQuery();
                cmd10.Parameters.Clear();

                //SwitchBrackett
                SqlCommand cmd11 = new SqlCommand("SP_FinalInspection", con);
                cmd11.CommandType = CommandType.StoredProcedure;
                cmd11.Parameters.AddWithValue("@Action", "Inseroderdetails");
                cmd11.Parameters.AddWithValue("@HeaderID", id);
                cmd11.Parameters.AddWithValue("@AssemblyName1", SwitchBrackett.InnerText);
                //cmd11.Parameters.AddWithValue("@AssemblyName2", "Null");
                //cmd11.Parameters.AddWithValue("@AssemblyName3", "Null");
                string check8 = CheckBoxSwitchBrackett.Checked ? "Pass" : "Fail";
                cmd11.Parameters.AddWithValue("@specification", check8);
                cmd11.Parameters.AddWithValue("@Observation", txtobservationSwitchBrackett.Text);
                cmd11.Parameters.AddWithValue("@Result", txtresultSwitchBrackett.Text);
                cmd11.Parameters.AddWithValue("@Test", InernalAssembky.InnerText);
                cmd11.ExecuteNonQuery();
                cmd11.Parameters.Clear();



                //AlignmentofDoors
                SqlCommand cmd12 = new SqlCommand("SP_FinalInspection", con);
                cmd12.CommandType = CommandType.StoredProcedure;
                cmd12.Parameters.AddWithValue("@Action", "Inseroderdetails");
                cmd12.Parameters.AddWithValue("@HeaderID", id);
                cmd12.Parameters.AddWithValue("@AssemblyName1", AlignmentofDoors.InnerText);
                //cmd12.Parameters.AddWithValue("@AssemblyName2", "Null");
                //cmd12.Parameters.AddWithValue("@AssemblyName3", "Null");
                string check9 = CheckBoxSwitchBrackett.Checked ? "Pass" : "Fail";
                cmd12.Parameters.AddWithValue("@specification", check9);
                cmd12.Parameters.AddWithValue("@Observation", txtobservationAlignmentofDoors.Text);
                cmd12.Parameters.AddWithValue("@Result", txtresulttxtobservationAlignmentofDoors.Text);
                cmd12.Parameters.AddWithValue("@Test", InernalAssembky.InnerText);
                cmd12.ExecuteNonQuery();
                cmd12.Parameters.Clear();


                //Liftingarrangement
                SqlCommand cmd13 = new SqlCommand("SP_FinalInspection", con);
                cmd13.CommandType = CommandType.StoredProcedure;
                cmd13.Parameters.AddWithValue("@Action", "Inseroderdetails");
                cmd13.Parameters.AddWithValue("@HeaderID", id);
                cmd13.Parameters.AddWithValue("@AssemblyName1", Liftingarrangement.InnerText);
                //cmd13.Parameters.AddWithValue("@AssemblyName2", "Null");
                //cmd13.Parameters.AddWithValue("@AssemblyName3", "Null");
                string check10 = checkLiftingarrangement.Checked ? "Pass" : "Fail";
                cmd13.Parameters.AddWithValue("@specification", check10);
                cmd13.Parameters.AddWithValue("@Observation", txtobervationLiftingarrangement.Text);
                cmd13.Parameters.AddWithValue("@Result", txtresultLiftingarrangement.Text);
                cmd13.Parameters.AddWithValue("@Test", InernalAssembky.InnerText);
                cmd13.ExecuteNonQuery();
                cmd13.Parameters.Clear();




                //EArthingBolt
                SqlCommand cmd14 = new SqlCommand("SP_FinalInspection", con);
                cmd14.CommandType = CommandType.StoredProcedure;
                cmd14.Parameters.AddWithValue("@Action", "Inseroderdetails");
                cmd14.Parameters.AddWithValue("@HeaderID", id);
                cmd14.Parameters.AddWithValue("@AssemblyName1", EArthingBolt.InnerText);
                //cmd14.Parameters.AddWithValue("@AssemblyName2", "Null");
                //cmd14.Parameters.AddWithValue("@AssemblyName3", "Null");
                string check11 = CheckBoxEArthingBolt.Checked ? "Pass" : "Fail";
                cmd14.Parameters.AddWithValue("@specification", check11);
                cmd14.Parameters.AddWithValue("@Observation", txtobservationEArthingBolt.Text);
                cmd14.Parameters.AddWithValue("@Result", txtresultEArthingBolt.Text);
                cmd14.Parameters.AddWithValue("@Test", InernalAssembky.InnerText);
                cmd14.ExecuteNonQuery();
                cmd14.Parameters.Clear();




                //Gasket
                SqlCommand cmd15 = new SqlCommand("SP_FinalInspection", con);
                cmd15.CommandType = CommandType.StoredProcedure;
                cmd15.Parameters.AddWithValue("@Action", "Inseroderdetails");
                cmd15.Parameters.AddWithValue("@HeaderID", id);
                cmd15.Parameters.AddWithValue("@AssemblyName1", Gasket.InnerText);
                //cmd15.Parameters.AddWithValue("@AssemblyName2", "Null");
                //cmd15.Parameters.AddWithValue("@AssemblyName3", "Null");
                string check12 = CheckBoxGasket.Checked ? "Pass" : "Fail";
                cmd15.Parameters.AddWithValue("@specification", check12);
                cmd15.Parameters.AddWithValue("@Observation", TextBoxGasketobservation.Text);
                cmd15.Parameters.AddWithValue("@Result", TextBoxresultGasket.Text);
                cmd15.Parameters.AddWithValue("@Test", InernalAssembky.InnerText);
                cmd15.ExecuteNonQuery();
                cmd15.Parameters.Clear();



                //Hardware
                SqlCommand cmd16 = new SqlCommand("SP_FinalInspection", con);
                cmd16.CommandType = CommandType.StoredProcedure;
                cmd16.Parameters.AddWithValue("@Action", "Inseroderdetails");
                cmd16.Parameters.AddWithValue("@HeaderID", id);
                cmd16.Parameters.AddWithValue("@AssemblyName1", Hardware.InnerText);
                //cmd16.Parameters.AddWithValue("@AssemblyName2", "Null");
                //cmd16.Parameters.AddWithValue("@AssemblyName3", "Null");
                string check13 = CheckBoxHardware.Checked ? "Pass" : "Fail";
                cmd16.Parameters.AddWithValue("@specification", check13);
                cmd16.Parameters.AddWithValue("@Observation", TextBoxHardwareobservation.Text);
                cmd16.Parameters.AddWithValue("@Result", TextBoxHardwareResult.Text);
                cmd16.Parameters.AddWithValue("@Test", InernalAssembky.InnerText);
                cmd16.ExecuteNonQuery();
                cmd16.Parameters.Clear();



                //Glandplate
                SqlCommand cmd17 = new SqlCommand("SP_FinalInspection", con);
                cmd17.CommandType = CommandType.StoredProcedure;
                cmd17.Parameters.AddWithValue("@Action", "Inseroderdetails");
                cmd17.Parameters.AddWithValue("@HeaderID", id);
                cmd17.Parameters.AddWithValue("@AssemblyName1", Glandplate.InnerText);
                //cmd17.Parameters.AddWithValue("@AssemblyName2", "Null");
                //cmd17.Parameters.AddWithValue("@AssemblyName3", "Null");
                string check14 = CheckBoxGlandplate.Checked ? "Pass" : "Fail";
                cmd17.Parameters.AddWithValue("@specification", check14);
                cmd17.Parameters.AddWithValue("@Observation", TextBoxGlandplateobservation.Text);
                cmd17.Parameters.AddWithValue("@Result", TextBoxGlandplateresult.Text);
                cmd17.Parameters.AddWithValue("@Test", InernalAssembky.InnerText);
                cmd17.ExecuteNonQuery();
                cmd17.Parameters.Clear();


                //IngressProtectionclass
                SqlCommand cmd18 = new SqlCommand("SP_FinalInspection", con);
                cmd18.CommandType = CommandType.StoredProcedure;
                cmd18.Parameters.AddWithValue("@Action", "Inseroderdetails");
                cmd18.Parameters.AddWithValue("@HeaderID", id);
                cmd18.Parameters.AddWithValue("@AssemblyName1", IngressProtectionclass.InnerText);
                //cmd18.Parameters.AddWithValue("@AssemblyName2", "Null");
                //cmd18.Parameters.AddWithValue("@AssemblyName3", "Null");
                string check15 = CheckBoxIngressProtectionclass.Checked ? "Pass" : "Fail";
                cmd18.Parameters.AddWithValue("@specification", check15);
                cmd18.Parameters.AddWithValue("@Observation", TextBoxIngressProtectionclassobservation.Text);
                cmd18.Parameters.AddWithValue("@Result", TextBoxIngressProtectionclassResult.Text);
                cmd18.Parameters.AddWithValue("@Test", IngressProtectionclass.InnerText);
                cmd18.ExecuteNonQuery();
                cmd18.Parameters.Clear();
            



                //VisualInspection
                SqlCommand cmd19 = new SqlCommand("SP_FinalInspection", con);
                cmd19.CommandType = CommandType.StoredProcedure;
                cmd19.Parameters.AddWithValue("@Action", "Inseroderdetails");
                cmd19.Parameters.AddWithValue("@HeaderID", id);
                cmd19.Parameters.AddWithValue("@AssemblyName1", Scratech.InnerText);
                //cmd18.Parameters.AddWithValue("@AssemblyName2", "Null");
                //cmd18.Parameters.AddWithValue("@AssemblyName3", "Null");
                string check16 = CheckBoxVisualInspection.Checked ? "Pass" : "Fail";
                cmd19.Parameters.AddWithValue("@specification", check16);
                cmd19.Parameters.AddWithValue("@Observation", TextBoxVisualInspectionobservation.Text);
                cmd19.Parameters.AddWithValue("@Result", TextBoxVisualInspectionResult.Text);
                cmd19.Parameters.AddWithValue("@Test", VisualInspection.InnerText);
                cmd19.ExecuteNonQuery();
                cmd19.Parameters.Clear();







                //CleaningPacking
                SqlCommand cmd20 = new SqlCommand("SP_FinalInspection", con);
                cmd20.CommandType = CommandType.StoredProcedure;
                cmd20.Parameters.AddWithValue("@Action", "Inseroderdetails");
                cmd20.Parameters.AddWithValue("@HeaderID", id);
                cmd20.Parameters.AddWithValue("@AssemblyName1", CleaningPacking.InnerText);
                //cmd18.Parameters.AddWithValue("@AssemblyName2", "Null");
                //cmd18.Parameters.AddWithValue("@AssemblyName3", "Null");
                string check17 = CheckBoxCleaningPacking.Checked ? "Pass" : "Fail";
                cmd20.Parameters.AddWithValue("@specification", check17);
                cmd20.Parameters.AddWithValue("@Observation", TextBoxCleaningPackingobservation.Text);
                cmd20.Parameters.AddWithValue("@Result", TextBoxCleaningPackingResult.Text);
                cmd20.Parameters.AddWithValue("@Test", CleaningPacking.InnerText);
                
                cmd20.ExecuteNonQuery();
                cmd20.Parameters.Clear();
               

                //WiringDresssing
                SqlCommand cmd21 = new SqlCommand("SP_FinalInspection", con);
                cmd21.CommandType = CommandType.StoredProcedure;
                cmd21.Parameters.AddWithValue("@Action", "Inseroderdetails");
                cmd21.Parameters.AddWithValue("@HeaderID", id);
                cmd21.Parameters.AddWithValue("@AssemblyName1", WiringDresssing.InnerText);
                //cmd18.Parameters.AddWithValue("@AssemblyName2", "Null");
                //cmd18.Parameters.AddWithValue("@AssemblyName3", "Null");
                string check18 = CheckBoxWiringWiringDresssing.Checked ? "Pass" : "Fail";
                cmd21.Parameters.AddWithValue("@specification", check18);
                cmd21.Parameters.AddWithValue("@Observation", TextBoxWiringDresssingobservation.Text);
                cmd21.Parameters.AddWithValue("@Result", TextBoxWiringDresssingResult.Text);
                cmd21.Parameters.AddWithValue("@Test", Wiring.InnerText);
                cmd21.ExecuteNonQuery();
                cmd21.Parameters.Clear();
    

                //AccessabilityofTBAndWires
                SqlCommand cmd22 = new SqlCommand("SP_FinalInspection", con);
                cmd22.CommandType = CommandType.StoredProcedure;
                cmd22.Parameters.AddWithValue("@Action", "Inseroderdetails");
                cmd22.Parameters.AddWithValue("@HeaderID", id);
                cmd22.Parameters.AddWithValue("@AssemblyName1", AccessabilityofTBAndWires.InnerText);
                //cmd18.Parameters.AddWithValue("@AssemblyName2", "Null");
                //cmd18.Parameters.AddWithValue("@AssemblyName3", "Null");
                string check19 = CheckBoxAccessabilityofTBAndWires.Checked ? "Pass" : "Fail";
                cmd22.Parameters.AddWithValue("@specification", check19);
                cmd22.Parameters.AddWithValue("@Observation", TextBoxAccessabilityofTBAndWiresobservation.Text);
                cmd22.Parameters.AddWithValue("@Result", TextBoxAccessabilityofTBAndWiresResult.Text);
                cmd22.Parameters.AddWithValue("@Test", Wiring.InnerText);
                cmd22.ExecuteNonQuery();
                cmd22.Parameters.Clear();
        




                //SizesandColourofwires
                SqlCommand cmd23 = new SqlCommand("SP_FinalInspection", con);
                cmd23.CommandType = CommandType.StoredProcedure;
                cmd23.Parameters.AddWithValue("@Action", "Inseroderdetails");
                cmd23.Parameters.AddWithValue("@HeaderID", id);
                cmd23.Parameters.AddWithValue("@AssemblyName1", SizesandColourofwires.InnerText);
                //cmd18.Parameters.AddWithValue("@AssemblyName2", "Null");
                //cmd18.Parameters.AddWithValue("@AssemblyName3", "Null");
                string check20 = CheckBoxSizesandColourofwires.Checked ? "Pass" : "Fail";
                cmd23.Parameters.AddWithValue("@specification", check20);
                cmd23.Parameters.AddWithValue("@Observation", TextBoxSizesandColourofwiresobservation.Text);
                cmd23.Parameters.AddWithValue("@Result", TextBoxSizesandColourofwiresResult.Text);
                cmd23.Parameters.AddWithValue("@Test", Wiring.InnerText);
                cmd23.ExecuteNonQuery();
                cmd23.Parameters.Clear();
                con.Close();



            

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Order Save Successfully..!!');window.location='salesOrderList.aspx'; ", true);



        }
        catch (Exception ex)
        {

            //throw;
            string errorMsg = "An error occurred : " + ex.Message;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + errorMsg + "');", true);
        }
    }



    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetCustomerList(string prefixText, int count)
    {
        return AutoFillCustomerlist(prefixText);
    }

    public static List<string> AutoFillCustomerlist(string prefixText)
    {
        using (SqlConnection con = new SqlConnection())
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = "select DISTINCT cname from Company where " + "cname like @Search + '%'";

                com.Parameters.AddWithValue("@Search", prefixText);
                com.Connection = con;
                con.Open();
                List<string> Qno = new List<string>();
                using (SqlDataReader sdr = com.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        Qno.Add(sdr["cname"].ToString());
                    }
                }
                con.Close();
                return Qno;
            }
        }
    }
}



