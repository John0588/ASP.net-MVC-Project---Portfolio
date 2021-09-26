using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Final_Project_in_Asp.net_MVC_2021.MyModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Final_Project_in_Asp.net_MVC_2021.Pages
{
    public class SignUpModel : PageModel
    {
        public const string emailSession = "e_Mail";
        public const string fNameSession = "f_Name";
        public const string lNameSession = " l_Name";

        [BindProperty]
        public MyUser user { get; set; }

        [BindProperty(SupportsGet = true)]
        public string signUpError { get; set; }

        [BindProperty(SupportsGet = true)]
        public string logOutMessage { get; set; } = "You are succesfully Log Out!";
        public void OnGet()
        {
        }

        // SignUp Process
        public IActionResult OnPost()
        {         
            SqlConnection signUpData = new SqlConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=mvcprojectdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string readData = "SELECT * FROM [mvcprojectdb].[dbo].[Users] WHERE Email = '" + user.email.Trim().ToLower() + "'";
            SqlCommand readCmd = new SqlCommand(readData, signUpData);
            signUpData.Open();
            SqlDataReader signUpReader = readCmd.ExecuteReader();

            if (signUpReader.Read())
            {
                signUpError = "Ooopsss... Your Email is already exist, Please Sign In!";
                return RedirectToPage("/SignUp", new { signUpError });
            }
            else
            {
                signUpData.Close();
                string myHashed = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(user.password))).Replace("-", "");
                string insertData = "INSERT INTO [mvcprojectdb].[dbo].[Users]" + "([FirstName],[LastName],[Email],[Password])" + " VALUES ('" + user.fname.Trim() + "','" + user.lname.Trim() + "', '" + user.email.Trim().ToLower() + "', '" + myHashed + "')";
                SqlCommand myCmd = new SqlCommand(insertData, signUpData);
                signUpData.Open();
                myCmd.ExecuteNonQuery();
                signUpData.Close();

                //Session Process
                HttpContext.Session.SetString(emailSession, user.email);
                HttpContext.Session.SetString(fNameSession, user.fname);
                HttpContext.Session.SetString(lNameSession, user.lname);              

                return RedirectToPage("/SignIn", new { user.fname });
            }           
        }
    }
}
