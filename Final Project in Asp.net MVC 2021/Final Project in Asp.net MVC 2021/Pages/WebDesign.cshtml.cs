using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Final_Project_in_Asp.net_MVC_2021.MyModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;

namespace Final_Project_in_Asp.net_MVC_2021.Pages
{
    public class WebDesignModel : PageModel
    {
        public const string emailSession = "e_Mail";
        public const string fNameSession = "f_Name";
        public const string lNameSession = " l_Name";

        [BindProperty]
        public MyUser user { get; set; }

        [BindProperty]
        public Comment comment { get; set; }

        [BindProperty(SupportsGet = true)]
        public string securityError { get; set; } = "Please Log In First! ";

        [BindProperty(SupportsGet = true)]
        public string email { get; set; }

        [BindProperty(SupportsGet = true)]
        public string fname { get; set; }

        [BindProperty(SupportsGet = true)]
        public string lname { get; set; }

        [BindProperty(SupportsGet = true)]
        public string myResult { get; set; }
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

        // API Process
        public async Task<IActionResult> OnPostAsync()
        {
            var cmt = comment.UserComment.Replace(" " , "%20");
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://twinword-sentiment-analysis.p.rapidapi.com/analyze/?text=" + cmt ),
                Headers =
            {
                { "x-rapidapi-host", "twinword-sentiment-analysis.p.rapidapi.com" },
                { "x-rapidapi-key", "fac988c16fmsh5c7538416f624f6p1b1a7bjsnc9949f3ac306" },
            },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                JObject parsed = JObject.Parse(body);
                myResult = (string)parsed["score"];
                /*Console.WriteLine(body);*/

                return Page();
            }
        }
    }
}

