using RegistrationAppDataAccess;
using RegistrationAppEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RegistrationApp
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                bindDDL();
                populateGrid();
            }

        }

        private void populateGrid()
        {
            RegistrationDataAccess rda = new RegistrationDataAccess();
            DataTable dt = rda.getGridData();
            gvRegistration.DataSource = dt;
            gvRegistration.DataBind();
            
        }

        private void bindDDL()
        {
            RegistrationDataAccess rda = new RegistrationDataAccess();
            DataTable dt = rda.getDDLData();
            ddlDept.DataValueField = dt.Columns[0].ToString();
            ddlDept.DataTextField = dt.Columns[1].ToString();
            ddlDept.DataSource = dt;
            ddlDept.DataBind();
            ddlDept.Items.Insert(0,"--Select--");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            RegistrationDataAccess rda = new RegistrationDataAccess();
            RegistrationEntity ren = new RegistrationEntity();
            ren.StrFirstName = txtFirstName.Text;
            ren.StrLastName = txtLastName.Text;
            ren.IntDepartment= Convert.ToInt32(ddlDept.SelectedValue);
            if(rdBtnMale.Checked)
                {
                ren.BoolGender = true;
                }
            else
            {
                ren.BoolGender = false;
            }

            if(btnAdd.Text=="UPDATE")
            {
                Int32 intRegId = Convert.ToInt32(Session["regidSession"]);
                if(rda.UpdateRecord(ren, intRegId))
                {
                    lblMsg.Text = "Registration Updated";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMsg.Text = "Updation failed";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }

            }
            else
            {
                if (rda.Add(ren))
                {
                    lblMsg.Text = "Registration done";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMsg.Text = "Registration failed";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            btnAdd.Text = "Add";
            populateGrid();
            ClrControl();
        }

        protected void btnEdit_Command(object sender, CommandEventArgs e)
        {
            Session["regidSession"] = e.CommandArgument;
            Int32 intSession = Convert.ToInt32(Session["regidSession"]);
            RegistrationDataAccess rda = new RegistrationDataAccess();
            DataTable dt = rda.getEditData(intSession); 
            txtFirstName.Text = dt.Rows[0]["FirstName"].ToString();
            txtLastName.Text = dt.Rows[0]["LastName"].ToString();
            ddlDept.SelectedValue = dt.Rows[0]["DeptId"].ToString();
            if (Convert.ToBoolean(dt.Rows[0]["Gender"]) == true)
            {
                rdBtnMale.Checked = true;
            }
            else if (Convert.ToBoolean(dt.Rows[0]["Gender"]) == false)
            {
                rdBtnFemale.Checked = true;
            }
            else
            {
                rdBtnMale.Checked = false;
                rdBtnMale.Checked = false;
            }
            btnAdd.Text = "UPDATE";
        }

        protected void btnDelete_Command(object sender, CommandEventArgs e)
        {
            RegistrationDataAccess rda = new RegistrationDataAccess();
            Int32 intRegid = Convert.ToInt32(e.CommandArgument);
            if (rda.DeleteRecord(intRegid))
            {
                lblMsg.Text = "Record deleted";
                lblMsg.ForeColor = System.Drawing.Color.Green; 
                populateGrid();

            }
            else
            {
                lblMsg.Text = "deletion failed";
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }

        }

        private void ClrControl()
        {
            try
            {
                txtFirstName.Text = "";
                txtLastName.Text = "";
                ddlDept.SelectedIndex = 0;
                rdBtnFemale.Checked = false;
                rdBtnMale.Checked = false;
            }
            catch (Exception ex)
            {
               
            }
        }
    }
}  