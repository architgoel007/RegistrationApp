using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationAppEntity
{
    public class RegistrationEntity
    {
        private string strFirstName;

        public string StrFirstName { get => strFirstName; set => strFirstName = value; }
  
        private string strLastName;

        public string StrLastName { get => strLastName; set => strLastName = value; }
        
        private int intDepartment;

        public int IntDepartment { get => intDepartment; set => intDepartment = value; }
        
        private bool boolGender;

        public bool BoolGender { get => boolGender; set => boolGender = value; }





    }
}
