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
    public class SignInModel : PageModel
    {
        public const string emailSession = "e_Mail";
        public const string fNameSession = "f_Name";
        public const string lNameSession = " l_Name";

        [BindProperty]
        public MyUser user { get; set; }

        [BindProperty(SupportsGet = true)]
        public string fname { get; set; }

        [BindProperty(SupportsGet = true)]
        public string signInError { get; set; }

        [BindProperty(SupportsGet = true)]
        public string securityError { get; set; } = "Please Sign In First!";
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            string myHashed = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(user.password))).Replace("-", "");
            SqlConnection signInData = new SqlConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=mvcprojectdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string readMyData = "SELECT * FROM [mvcprojectdb].[dbo].[Users] WHERE Email = '" + user.email.Trim().ToLower() + "' AND Password = '" + myHashed +"'";
            SqlCommand readMyCmd = new SqlCommand(readMyData, signInData);
            signInData.Open();
            SqlDataReader signInReader = readMyCmd.ExecuteReader();

            if (signInReader.Read())
            {
                user.fname = string.Format("{0}", signInReader[1]);
                user.lname = string.Format("{0}", signInReader[2]);

                //Session Process
                HttpContext.Session.SetString(emailSession, user.email);
                HttpContext.Session.SetString(fNameSession, user.fname);
                HttpContext.Session.SetString(lNameSession, user.lname);

                return RedirectToPage("/Services");
            }else
            {
                signInError = "Ooops... the Email or Password is not valid!";
                return Page();
            }

        }
    }
}
