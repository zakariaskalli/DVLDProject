using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Data_Layer___DVLDProject;


namespace Business_Layer___DVLDProject
{
    public class clsLoginScreenBusiness
    {

        static public bool IsUserNameAndPasswordExciting(string UserName, string Password)
        {
            return clsLoginScreenData.IsUserNameAndPasswordExciting(UserName, Password);
        }

        static public bool IsAccountActive(string UserName)
        {
            return clsLoginScreenData.IsAccountActive(UserName);
        }

        static public void RememberMe(bool RememberOrNot, string UserName, string Password)
        {


            if (RememberOrNot)
            {
                clsLoginScreenData.DeleteAllDataInRememberMeTable();
                clsLoginScreenData.RememberMeInfo(UserName, Password);
            }
            else
                clsLoginScreenData.DeleteAllDataInRememberMeTable();

        }
    
        static public bool LoadUserNameAndPasswordRememberMe(ref string UserName,ref string Password)
        {
            return clsLoginScreenData.LoadUserNameAndPasswordRememberMe(ref UserName,ref Password);
        }
    }
}





