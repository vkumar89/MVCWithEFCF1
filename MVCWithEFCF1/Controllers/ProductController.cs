using MVCWithEFCF1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.IO;

namespace MVCWithEFCF1.Controllers
{
    public class ProductController : Controller
    {

        StoreDbContext dc = new StoreDbContext();

        #region all products

        public ViewResult DisplayProducts()
        {

            // dc.Configuration.LazyLoadingEnabled = false;
            var products = dc.products.Include(p => p.Category).Where(p => p.Discontinued == false);

            return View(products);
        }


        #endregion

        #region AddProduct


        public ViewResult AddProduct()
        {
            ViewBag.CategoryId = new SelectList(dc.categories, "CategoryId", "CategoryName");
            return View();
        }
        [HttpPost]
        public RedirectToRouteResult AddProduct(Product product, HttpPostedFileBase selectedFile)
        {
            if (selectedFile != null)
            {
                string DirectoryPath = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(DirectoryPath))
                {
                    Directory.CreateDirectory(DirectoryPath);
                }
                selectedFile.SaveAs(DirectoryPath + selectedFile.FileName);
                BinaryReader br = new BinaryReader(selectedFile.InputStream);
                product.ProductImage = br.ReadBytes(selectedFile.ContentLength);
                product.ProductImageName = selectedFile.FileName;


            }

            dc.products.Add(product);
            dc.SaveChanges();
            return RedirectToAction("DisplayProducts");
        }


        #endregion

        #region display student
        public ViewResult EditProduct(int Id)
        {
            Product product1 = dc.products.Find(Id);
            TempData["ProductImage"] = product1.ProductImage;
            TempData["ProductImageName"] = product1.ProductImageName;
            ViewBag.CategoryId = new SelectList(dc.categories, "CategoryId", "CategoryName", product1.CategoryId);

            return View(product1);
        }
        public RedirectToRouteResult UpdateProduct(Product product, HttpPostedFileBase selectedFile)
        {
            if (selectedFile != null)
            {
                string DirectoryPath = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(DirectoryPath))
                {
                    Directory.CreateDirectory(DirectoryPath);
                }
                selectedFile.SaveAs(DirectoryPath + selectedFile.FileName);
                BinaryReader br = new BinaryReader(selectedFile.InputStream);
                product.ProductImage = br.ReadBytes(selectedFile.ContentLength);
                product.ProductImageName = selectedFile.FileName;

            }
            else if (TempData["ProductImage"] != null && TempData["ProductImageName"] != null)
            {
                product.ProductImage = (byte[])TempData["ProductImage"];
                product.ProductImageName = (string)TempData["ProductImageName"];
            }

            dc.Entry(product);
            dc.SaveChanges();
            return RedirectToAction("DisplayProducts");
        }
        #endregion

        #region

        public RedirectToRouteResult DeleteProduct(int Id)
        {
           Product product= dc.products.Find(Id);
            product.Discontinued = true;
            dc.SaveChanges();
            return RedirectToAction("DisplayProducts");
        }

        #endregion

        #region DisplayProduct

        public ViewResult DisplayProduct(int Id)
        {
            Product product = (dc.products.Include(P => P.Category).Where(P => P.Id == Id && P.Discontinued == false)).Single();

            return View(product);
        }
        #endregion

    }
}