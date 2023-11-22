using ProductManage.DAL;
using ProductManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManage.Controllers
{
    public class ProductController : Controller
    {

        Product_DAL product_DAL=new Product_DAL();
        // GET: Admin
        
        // GET: Product
        public ActionResult Index(string searchBy, string searchValue) {

            try
            {
                var productList = product_DAL.GetAllProducts();
                if (productList.Count == 0)
                {
                    TempData["InfoMessage"] = "Products Are not Available";
                }
                else
                {
                    if (string.IsNullOrEmpty(searchValue))
                    {
                        TempData["InfoMessage"] = "Provide Search value ";
                    }
                    else
                    {
                        if (searchBy.ToLower() == "productname")
                        {
                            var searchByProductName = productList.Where(p => p.ProductName.ToLower().Contains(searchValue.ToLower()));
                            return View(searchByProductName);
                        }
                        else if (searchBy.ToLower() == "price")
                        {
                            var searchByPrice = productList.Where(p => p.Price == decimal.Parse(searchValue));
                            return View(searchByPrice);


                        }
                    }
                }
                return View(productList);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var product = product_DAL.GetProductByID(id).FirstOrDefault();
                if (product == null)
                {
                    TempData["InfoMessage"] = "Product is not available with Id" + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(product);

            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            bool IsInserted = false;
            try
            {
                if (ModelState.IsValid)
                {
                    IsInserted = product_DAL.InsertProduct(product);
                    if (IsInserted)
                    {
                        TempData["SuccessMessage"] = "Product details saved successfully";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to save Product Details";
                    }

                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            var products = product_DAL.GetProductByID(id).FirstOrDefault(); 
            if (products == null)
            {
                TempData["InfoMessage"] = "Product is not available with Id" + id.ToString();
                return RedirectToAction("Index");
            }
            return View(products);
      
        }

        // POST: Product/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdate = product_DAL.UpdateProduct(product);
                    if (IsUpdate)
                    {
                        TempData["InfoMessage"] = "Product Updated ";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Unable to Update Product Details";

                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var product =  product_DAL.GetProductByID(id).FirstOrDefault();
                if (product == null)
                {
                    TempData["InfoMessage"] = "Product is not available with Id" + id.ToString();
                    return RedirectToAction("Index");
                }
                return View(product);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id, FormCollection collection)
        {
            try
            {
                string result = product_DAL.DeleteProduct(id);
                if (result.Contains("delete"))
                {
                    TempData["SuccessMessage"] = result;
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
    }
}
