using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Final_Project_in_Asp.net_MVC_2021.Pages
{
    public class LogOutModel : PageModel
    {

        public const string emailSession = "e_Mail";
        public const string fNameSession = "f_Name";
        public const string lNameSession = " l_Name";

        [BindProperty (SupportsGet = true)]
        public string logOutMessage { get; set; }
        public IActionResult OnPost()
        {

            //Session Process
            HttpContext.Session.SetString(emailSession, "");
            HttpContext.Session.SetString(fNameSession, "");
            HttpContext.Session.SetString(lNameSession, "");
           
            return RedirectToPage("/SignUp", new { logOutMessage });

        }
    }
}
