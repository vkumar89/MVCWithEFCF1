using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCWithEFCF1.Models;
using System.Data.Entity;
namespace MVCWithEFCF1.Controllers
{
    public class CategoryController : Controller
    {
        StoreDbContext dc =new StoreDbContext();


        #region All Catagories
        public ViewResult DisplayCategories()
        {
            var catagories= dc.categories;
            return View(catagories);
        }
        #endregion

        #region AddCategory
        [HttpGet]
        public ViewResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public RedirectToRouteResult AddCategory(Category category)
        {
            dc.categories.Add(category);
            dc.SaveChanges();
            return RedirectToAction("DisplayCategories");
        }
        #endregion


        #region Edit Catagories

        public ViewResult EditCategory(int CategoryId)
        {
            Category category = dc.categories.Find(CategoryId);
            return View(category);
        }



        #endregion

        #region UpdateCategory

      
        public RedirectToRouteResult UpdateCategory(Category category)
        {
            dc.Entry(category).State = EntityState.Modified;
            dc.SaveChanges();
            return RedirectToAction("DisplayCategories");
        }

        #endregion

        #region DeleteCategory

        public RedirectToRouteResult DeleteCategory(int CategoryId)
        {
            Category category=dc.categories.Find(CategoryId);
            dc.categories.Remove(category);
            dc.SaveChanges();
            return RedirectToAction("DisplayCategories");

        }

        #endregion
    }
}