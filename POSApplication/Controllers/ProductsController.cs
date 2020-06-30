using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using POSApplication.Models;

namespace POSApplication.Controllers
{
    public class ProductsController : Controller
    {
        private POSDBContext db = new POSDBContext();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "SupplierName");
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "Id", "CategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductCode,ProductName,SupplierId,ProductCategoryId,PurchasePrice,SalesPrice,Discount")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();

                ViewBag.Message = "Succeess";
              
            }
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "SupplierName");
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "Id", "CategoryName");
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);

            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "SupplierName", product.SupplierId);
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "Id", "CategoryName", product.ProductCategoryId);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductCode,ProductName,SupplierId,ProductCategoryId,PurchasePrice,SalesPrice,Discount")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();

                ViewBag.Message = "Succeess";
           
            }
            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "SupplierName");
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "Id", "CategoryName");

            return View(product);
        }


        // GET: Products/Edit/5
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);

            ViewBag.SupplierId = new SelectList(db.Suppliers, "Id", "SupplierName", product.SupplierId);
            ViewBag.ProductCategoryId = new SelectList(db.ProductCategories, "Id", "CategoryName", product.ProductCategoryId);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Detail([Bind(Include = "Id,ProductCode,ProductName,SupplierId,ProductCategoryId,PurchasePrice,SalesPrice,Discount")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }




        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }




        public JsonResult DeleteItem(int Id)
        {
            var result = new
            {
                flag = false,
                message = "Error occured. !"
            };

            try
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        //var checkData = db.Suppliers.Where(x => x.Id == Id).ToList();
                        //db.Suppliers.RemoveRange(checkData);
                        //db.SaveChanges();

                        var Item = db.Products.SingleOrDefault(x => x.Id == Id);
                        db.Products.Remove(Item);
                        db.SaveChanges();

                        dbContextTransaction.Commit();
                        result = new
                        {
                            flag = true,
                            message = "Update successful !!"
                        };
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();

                        result = new
                        {
                            flag = false,
                            message = ex.Message
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                result = new
                {
                    flag = false,
                    message = ex.Message
                };
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
