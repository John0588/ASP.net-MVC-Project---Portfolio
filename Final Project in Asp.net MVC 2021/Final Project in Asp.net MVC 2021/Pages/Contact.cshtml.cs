using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Final_Project_in_Asp.net_MVC_2021.MyModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;


namespace Final_Project_in_Asp.net_MVC_2021.Pages
{
    public class ContactModel : PageModel
    {      

        [BindProperty]
        public MyContact contact { get; set; }

        [BindProperty(SupportsGet = true)]
        public string fullname { get; set; }

        public IActionResult OnPost()
        {

            // Data Base Process
            SqlConnection contactData = new SqlConnection("Data Source=(localdb)\\ProjectsV13;Initial Catalog=mvcprojectdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            string contactInsert = "INSERT INTO [mvcprojectdb].[dbo].[Contact]" + "([FullName],[Email],[Subject],[Message]) VALUES ('" + contact.fullname.Trim() + "', '" + contact.email.Trim().ToLower() + "','" + contact.subject.Trim() + "','" + contact.message.Trim() + "')";
            SqlCommand myCmd = new SqlCommand(contactInsert, contactData);
            contactData.Open();
            myCmd.ExecuteNonQuery();
            contactData.Close();

            return RedirectToPage("/Contact", new { contact.fullname });
        }

        /*public async Task OnPost()
        {
            // Sending Mail to Email
            string FullName = contact.fullname;
            string Email = contact.email;
            string Subject = contact.subject;
            string Message = contact.message;

            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(Email);
            mailMessage.Subject = Subject;
            mailMessage.FullName = FullName;
            mailMessage.Message = Message;
            mailMessage.IsBodyHtml = false;
            mailMessage.From = new MailAddress("test.jognaliah0529@gmail.com");

            SmtpClient contactClient = new SmtpClient("smtp@gmail.com");
            contactClient.Port = 587;
            contactClient.UseDefaultCredentials = true;
            contactClient.EnableSsl = true;
            contactClient.Credentials = new System.Net.NetworkCredential("test.jognaliah0529@gmail.com", "AliahKeziah2917");
            await contactClient.SendMailAsync(mailMessage);
            ViewData["Message"] = "Your succesfully send your mail! " + contact.email;
        }*/
    }
}
