using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SportsNewsProject.Models.Attributes;
using SportsNewsProject.Models.Enums;
using SportsNewsProject.Models.ORM.Context;
using SportsNewsProject.Models.ORM.Entities;
using SportsNewsProject.Models.VM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SportsNewsProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsController : BaseController
    {
        private readonly SportsNewsContext _newscontext;

        public NewsController(SportsNewsContext newscontext, IMemoryCache memoryCache) : base(newscontext, memoryCache)
        {
            _newscontext = newscontext;
        }

        [RoleControl(EnumRoles.NewsList)]
        public IActionResult Index()
        {
            List<NewsVM> news = _newscontext.News.Where(q => q.IsDeleted == false).Include(q => q.Author).Include(q => q.Category).Select(q => new NewsVM()
            {
                ID = q.ID,
                Title = q.Title,
                SubTitle = q.SubTitle,
                Content = q.Content,
                AuthorName = q.Author.Name,
                CategoryName = q.Category.CategoryName

            }).ToList();

            return View(news);
        }

        [RoleTest(EnumRoles.NewsAdd)]
        public IActionResult Add()
        {
            return View(GetNewsVMForAdd());
        }


        [HttpPost]
        public IActionResult Add(NewsVM model, int authorid, int categoryid)
        {
            List<string> paths = new List<string>();


            string imgpath = "";

            if (model.articleimages != null)
            {
                foreach (var item in model.articleimages)
                {

                    var guid = Guid.NewGuid().ToString();

                    var path = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot/adminsite/assets/articleimg", guid + ".jpg");
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        item.CopyTo(stream);
                    }

                    imgpath = guid + ".jpg";
                    paths.Add(imgpath);
                }

            }

            model.MainImagePath = paths;


            if (ModelState.IsValid)
            {
                News article = new News();
                article.Title = model.Title;
                article.SubTitle = model.SubTitle;
                article.Content = model.Content;
                article.AuthorID = authorid;
                article.CategoryID = categoryid;

                _newscontext.News.Add(article);
                _newscontext.SaveChanges();

                int newsid = article.ID;

                foreach (var item in model.MainImagePath)
                {
                    Pictures image = new Pictures();
                    image.ImagePath = item;
                    image.NewsId = newsid;

                    _newscontext.Pictures.Add(image);
                }

                _newscontext.SaveChanges();
            }
            else
            {
                return View(GetNewsVMForAdd());
            }


            return RedirectToAction("Index", "News");
        }

        [RoleTest(EnumRoles.NewsEdit)]
        public IActionResult Edit(int id)
        {
            return View(GetNewsVMForEdit(id));
        }

        [HttpPost]
        public IActionResult Edit(NewsVM model, int authorid, int categoryid)
        {
            News editarticle = _newscontext.News.FirstOrDefault(q => q.ID == model.ID);

            List<string> paths = new List<string>();

            string imgpath = "";

            if (model.articleimages != null)
            {
                foreach (var item in model.articleimages)
                {
                    var guid = Guid.NewGuid().ToString();

                    var path = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot/adminsite/assets/articleimg", guid + ".jpg");
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        item.CopyTo(stream);
                    }

                    imgpath = guid + ".jpg";

                    paths.Add(imgpath);
                }
            }

            model.MainImagePath = paths;

            if (ModelState.IsValid)
            {

                editarticle.Content = model.Content;
                editarticle.Title = model.Title;
                editarticle.SubTitle = model.SubTitle;
                editarticle.AuthorID = authorid;
                editarticle.CategoryID = categoryid;

                _newscontext.SaveChanges();

                int articleid = editarticle.ID;

                foreach (var item in model.MainImagePath)
                {
                    Pictures images = new Pictures();
                    images.ImagePath = item;
                    images.NewsId = articleid;
                    _newscontext.Pictures.Add(images);
                }

                _newscontext.SaveChanges();
            }
            else
            {
                return View(GetNewsVMForEdit(model.ID));
            }
            return RedirectToAction("Index", "News");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            News deletednews = _newscontext.News.FirstOrDefault(x => x.ID == id);
            deletednews.IsDeleted = true;
            _newscontext.SaveChanges();

            return Json("Article Successfully Deleted!");
        }

        public IActionResult Detail(int id)
        {
            News detail = _newscontext.News.Include(q => q.PictureList).FirstOrDefault(x => x.ID == id);

            return Json(detail);
        }

        NewsVM GetNewsVMForAdd()
        {
            NewsVM model = new NewsVM();
            model.Categories = _newscontext.Categories.Where(q => q.IsDeleted == false).ToList();
            model.Authors = _newscontext.Authors.Where(q => q.IsDeleted == false).ToList();

            return model;
        }

        NewsVM GetNewsVMForEdit(int id)
        {
            News article = _newscontext.News.FirstOrDefault(q => q.ID == id);
            NewsVM model = new NewsVM();

            model.Content = article.Content;
            model.Title = article.Title;
            model.SubTitle = article.SubTitle;
            model.Categories = _newscontext.Categories.Where(q => q.IsDeleted == false).ToList();
            model.Authors = _newscontext.Authors.Where(q => q.IsDeleted == false).ToList();

            return model;
        }
    }
}
