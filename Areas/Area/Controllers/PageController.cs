using CmsShopping.Models.Data;
using CmsShopping.Models.ViewModels.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CmsShopping.Areas.Area.Controllers
{
    public class PageController : Controller
    {
        // GET: Area/Page
        public ActionResult Index()
        {
           //Declare a list of View Model
                List<PageVM> pagesList;
                using (CartDb db = new CartDb())
                {

                    pagesList = db.Pages.ToArray().OrderBy(x => x.Sorting).Select(x => new PageVM(x)).ToList();
                }
                return View(pagesList);
            
        }
        // GET: Area/Pages/AddPage()
        [HttpGet]
        public ActionResult AddPage()
        {
            return View();
        }
        // POST: Area/Pages/AddPage()
        [HttpPost]
        public ActionResult AddPage(PageVM model)
        {
           if(!ModelState.IsValid)
            {
                return View(model);
            }

           using(CartDb db = new CartDb())
            {
                string slug;
            PageDTO dt = new PageDTO();
                dt.Title = model.Title;
                if(string.IsNullOrWhiteSpace(model.Slug))
                {
                    slug = model.Title.Replace(" ", "-").ToLower();
                }
                else
                {
                    slug = model.Slug.Replace(" ", "-").ToLower();
                }
            
               if(db.Pages.Any(x => x.Title == model.Title) || db.Pages.Any(x => x.Slug == slug))
                {
                    ModelState.AddModelError("", "That tile o slug already added");
                }

                dt.Slug = slug;
                dt.Body = model.Body;
                dt.HasSidebar = model.HasSidebar;
                dt.Sorting = 100;

                db.Pages.Add(dt);
                db.SaveChanges();
            }
            TempData["MS"] = "Page Added Successfully";

            return RedirectToAction("AddPage");
        }
        // Get: Area/Pages/EditPage/id
        [HttpGet]
        public ActionResult EditPage(int id)
        {
            //Declare pageVM
            PageVM model;

            using(CartDb db = new CartDb())
            {
                //Get the page
                PageDTO dto = db.Pages.Find(id);

                //Confirm Page Exist
                if(dto == null)
                {
                    return Content("Page already Exsit");
                }
                //Init pagevm
                model = new PageVM(dto);
            }

            //Return view with model
            return View(model);
        }

        // Post: Area/Pages/EditPage/id
        [HttpPost]
        public ActionResult EditPage(PageVM model)
        {
          
            if(!ModelState.IsValid)

            {

                return View(model);
            }

            
            using (CartDb db = new CartDb())
            {

                int id = model.Id;

                string slug = "home";
                //Get the page
                PageDTO dto = db.Pages.Find(id);

                dto.Title = model.Title;
                if(model.Slug != "home")
                {
                    if(string.IsNullOrWhiteSpace(model.Slug))
                    {
                        slug = model.Title.Replace(" ", "-").ToLower();
                    }
                    else
                    {
                        slug = model.Slug.Replace(" ", "-").ToLower();
                    }
                }

                if(db.Pages.Where(x => x.Id != id).Any(x => x.Title == model.Title) ||
                    db.Pages.Where(x => x.Id != id).Any(x => x.Slug == slug))
                {
                    ModelState.AddModelError("", "Title already exist");
                    return View(model);
                }


                dto.Slug = slug;
                dto.Body = model.Body;
                dto.HasSidebar = model.HasSidebar;

                db.SaveChanges();

            }
            TempData["SM"] = "Page Edited";
            
         
            //Return view with model
            return RedirectToAction("EditPage");
        }

        //Get: Area/Page/PageDetails/id
        
        public ActionResult PageDetails(int id)
        {
            PageVM model;

            using (CartDb db = new CartDb())
            {
                //Get the page
                PageDTO dto = db.Pages.Find(id);
                if (dto == null)
                {
                    return Content("Page already Exsit");
                }
                //Init pagevm
                model = new PageVM(dto);
            }
                return View(model);
        }

        //Get: Area/Page/DeletePage/id
        public ActionResult DeletePage(int id)
        {
           

            using (CartDb db = new CartDb())
            {
                //Get the page
               
               
                    PageDTO dto = db.Pages.Find(id);
                db.Pages.Remove(dto);

                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        // Post: Area/Pages/RecordPages
        [HttpPost]
        public void RecordPages(int[] id)
        {
           
            using(CartDb db = new CartDb())
            {
                int count = 1;
                PageDTO dt;
                foreach(var pageid in id)
                {
                    dt = db.Pages.Find(pageid);
                    dt.Sorting = count;
                    db.SaveChanges();
                    count++;
                }

            }
           // return View();
        }

        //Get: Area/Page/EditSideBar
        [HttpGet]
        public ActionResult EditSideBar()
        {
            SideBarVM model;
            using(CartDb db = new CartDb())
            {

                SidebarDto dt = db.Sidebar.Find(1);
                model = new SideBarVM(dt);
            }
            return View(model);
        }

        //[ValidateInput(false)]
        //Post: Area/Page/EditSideBar
        [HttpPost]
        public ActionResult EditSideBar(SideBarVM model)
        {
            
            using (CartDb db = new CartDb())
            {

                SidebarDto dt = db.Sidebar.Find(1);
                dt.Body = model.Body;
                db.SaveChanges();
                
            }
            TempData["MS"] = "You Edited already";
            return RedirectToAction("EditSideBar");
        }
    }
}