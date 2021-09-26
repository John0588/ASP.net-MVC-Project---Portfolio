using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final_Project_in_Asp.net_MVC_2021.MyModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Final_Project_in_Asp.net_MVC_2021.Pages
{
    public class ResponsiveDesignModel : PageModel
    {
        public const string emailSession = "e_Mail";
        public const string fNameSession = "f_Name";
        public const string lNameSession = " l_Name";

        [BindProperty]
        public MyUser user { get; set; }

        [BindProperty(SupportsGet = true)]
        public string securityError { get; set; } = "Please Log In First! ";

        [BindProperty(SupportsGet = true)]
        public string email { get; set; }

        [BindProperty(SupportsGet = true)]
        public string fname { get; set; }

        [BindProperty(SupportsGet = true)]
        public string lname { get; set; }
        public IActionResult OnGet()
        {
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString(emailSession)))
            {
                return RedirectToPage("/SignIn", new { securityError });
            }
            else
            {
                //Session Process
                email = HttpContext.Session.GetString(emailSession);
                fname = HttpContext.Session.GetString(fNameSession);
                lname = HttpContext.Session.GetString(lNameSession);

                return Page();
            }
        }
    }
}

