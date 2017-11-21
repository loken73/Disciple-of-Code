using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using D_of_C_Blog.Models;
using D_of_C_Blog.Data;
using Microsoft.EntityFrameworkCore;
using MailKit;
using MimeKit;
using MailKit.Net.Smtp;

namespace D_of_C_Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? pageNumber)
        {
            int? page;

            var blogPosts = _context.Posts
                            .Include(r => r.PostTags)
                            .ThenInclude(r => r.Tag)
                            .OrderByDescending(o => o.TimeCreated);

            if (pageNumber == null)
                page = 1;
            else
                page = pageNumber;

            int pageSize = 4;
            return View(await PaginatedList<Post>.CreateAsync(blogPosts, page ?? 1, pageSize));
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactFormViewModel model)
        {
            var mail = new MimeMessage();

            mail.From.Add (new MailboxAddress (model.Name, "frank.rodriguez.dev@gmail.com"));
            mail.To.Add (new MailboxAddress (model.Name, "frank.rodriguez.dev@gmail.com"));

            mail.Subject = "A message has been sent from your blog.";

            mail.Body = new TextPart ("plain") 
            {
                Text = "From: " + model.Email + ".\n\n" + model.Message
            };

            using (var client = new SmtpClient()) {
				// For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
				//client.ServerCertificateValidationCallback = (s,c,h,e) => true;

				client.Connect ("smtp.gmail.com", 587);

				// Note: since we don't have an OAuth2 token, disable
				// the XOAUTH2 authentication mechanism.
				client.AuthenticationMechanisms.Remove ("XOAUTH2");

				// Note: only needed if the SMTP server requires authentication
				client.Authenticate ("frank.rodriguez.dev@gmail.com", Helpers.GPassword);

				client.Send (mail);
                Console.WriteLine("The mail has been sent successfully !!"); 
                Console.ReadLine(); 
				client.Disconnect (true);
			}

            return View(model);
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}