using RegistrationAppEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationAppDataAccess
{
    public class RegistrationDataAccess
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();

        public bool Add(RegistrationEntity entity)
        {
            bool retValue = false;
            {
                con.ConnectionString = "Data Source =.; Initial Catalog = Practice; Integrated Security = True";
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "Insert into Registration(FirstName,LastName,DeptId,Gender) values('" + entity.StrFirstName + "','" + entity.StrLastName + "','" + entity.IntDepartment + "','" + entity.BoolGender + "') ";
                retValue = Convert.ToBoolean(cmd.ExecuteNonQuery());
                con.Close();
                return retValue;
            }
        }

        public bool UpdateRecord(RegistrationEntity entity, int regid)
        {
            bool retValue = false;
            {
                con.ConnectionString = "Data Source =.; Initial Catalog = Practice; Integrated Security = True";
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "Update Registration set FirstName='" + entity.StrFirstName + "',LastName='" + entity.StrLastName + "',DeptId=" + entity.IntDepartment + ",Gender='" + entity.BoolGender + "' where  id=" + regid + "";
                retValue = Convert.ToBoolean(cmd.ExecuteNonQuery());
                con.Close();
                return retValue;
            }
        }

        public DataTable getGridData()
        {
            DataTable dtGrid = new DataTable();
            con.ConnectionString = " Data Source =.; Initial Catalog =Practice; Integrated Security= True";
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "Select a.Id , a.FirstName,a.LastName, b.Department, case when(a.Gender=1)" +
                                                                                 " then 'Male'" +
                                                                                  " when(a.Gender = 0)" +
                                                                                  " then 'Female'" +
                                                                                  " end 'Gender' " +
                                " from[Registration] a,[Dept] b where a.DeptId = b.Id and a.delflag=0";
            da.SelectCommand = cmd;
            da.Fill(dtGrid);
            con.Close();
            return dtGrid;
        }

        public DataTable getDDLData()
        {
            DataTable dtDDL = new DataTable();
            con.ConnectionString = " Data Source =.; Initial Catalog =Practice; Integrated Security= True";
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "Select Id,Department from Dept ";
            da.SelectCommand = cmd;
            da.Fill(dtDDL);
            con.Close();
            return dtDDL;
        }

        public DataTable getEditData(int regid)
        {
            DataTable dtReg = new DataTable();
            con.ConnectionString = " Data Source =.; Initial Catalog =Practice; Integrated Security= True";
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "Select Id , FirstName,LastName, DeptId,Gender from Registration where id=" + regid + "";
            da.SelectCommand = cmd;
            da.Fill(dtReg);
            con.Close();
            return dtReg;
        }

        public bool DeleteRecord(int regid)
        {
            bool retValue = false;
            {
                con.ConnectionString = "Data Source =.; Initial Catalog = Practice; Integrated Security = True";
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "Update Registration set delflag=1 where  id=" + regid + "";
                retValue = Convert.ToBoolean(cmd.ExecuteNonQuery());
                con.Close();
                return retValue;
            }
        }
    }
}
