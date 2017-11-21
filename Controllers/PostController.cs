using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D_of_C_Blog.Data;
using D_of_C_Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace D_of_C_Blog.Controllers
{
    public class PostController : Controller
    {

        private readonly ApplicationDbContext _context;

        public PostController (ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "Author")]
        public IActionResult CreatePost()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(CreatePostViewModel model)
        {

            if (ModelState.IsValid) {
                var post = new Post()
                {
                    Title = model.PostTitle,
                    Body = model.PostBody,
                    ShortDescription = model.PostShortDescription,
                    TimeCreated = DateTime.Now,
                    PostAuthor = _context.Users.Where(a => a.Email == "frank.rodriguez.dev@gmail.com").FirstOrDefault()
                };

                var tagsSeparated = Helpers.ExtractAllTags(model.Tags);

                foreach (string t in tagsSeparated)
                {
                    var a = _context.Tags.Any(tag => tag.Name == t);

                    if (a)
                    {
                        var newPostTag = new PostTag()
                        {
                            Post = post,
                            Tag = _context.Tags.Where(ta => ta.Name == t).FirstOrDefault()
                        };

                        post.PostTags.Add(newPostTag);
                        _context.Posts.Add(post);

                    }
                    else
                    {
                        var newTag = new Tag() { Name = t };

                        var newPostTag2 = new PostTag()
                        {
                            Post = post,
                            Tag = newTag
                        };

                        post.PostTags.Add(newPostTag2);
                        newTag.PostTags.Add(newPostTag2);
                        _context.Posts.Add(post);
                        _context.Tags.Add(newTag);
                    }
                }

                _context.SaveChanges();

                return View();
            }

            return View(model);
        }

        public IActionResult IndividualPost(string postName)
        {
            var APost = _context.Posts.Where(p => p.Title == postName).FirstOrDefault();

            return View(APost);
        }
    }
}