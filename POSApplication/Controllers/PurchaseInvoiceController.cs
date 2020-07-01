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
    public class PurchaseInvoiceController : Controller
    {
        private POSDBContext db = new POSDBContext();

        // GET: PurchaseInvoice
        public ActionResult Index()
        {


            return View(db.PurchaseInvoiceMas.ToList());
        }



        // GET: PurchaseInvoice/Create
        public ActionResult Create()
        {
            ViewBag.SupplierId = new SelectList(db.Suppliers.ToList().Distinct(), "Id", "SupplierName");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CompanyName,Phone,Email,Date,Secu_UserId")] PurchaseInvoiceMa purchaseInvoiceMa)
        {
            if (ModelState.IsValid)
            {
                db.PurchaseInvoiceMas.Add(purchaseInvoiceMa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(purchaseInvoiceMa);
        }

        // GET: PurchaseInvoice/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseInvoiceMa purchaseInvoiceMa = db.PurchaseInvoiceMas.Find(id);
            if (purchaseInvoiceMa == null)
            {
                return HttpNotFound();
            }
            return View(purchaseInvoiceMa);
        }

        // POST: PurchaseInvoice/Edit/5


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CompanyName,Phone,Email,Date,Secu_UserId")] PurchaseInvoiceMa purchaseInvoiceMa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseInvoiceMa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(purchaseInvoiceMa);
        }
        public JsonResult GetSupplierRelatedData(int? SupplierId)
        {
            if (SupplierId == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var data = db.Suppliers.Where(x => x.Id == SupplierId).Select(x => new
                {
                    CompanyName = x.CompanyName,
                    Phone = x.Phone,
                    Email = x.Email

                }).FirstOrDefault();

                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult GetProductCatagoryName()
        {

            var data = db.ProductCategories.Distinct().ToList().Select(x => new
            {
           
                Value = x.Id,
                Text = x.CategoryName

            }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);


        }
        public JsonResult GetProductName()
        {

            var data = db.Products.Distinct().ToList().Select(x => new
            {

                Value = x.Id,
                Text = x.ProductName

            }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);


        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchaseInvoiceMa purchaseInvoiceMa = db.PurchaseInvoiceMas.Find(id);
            db.PurchaseInvoiceMas.Remove(purchaseInvoiceMa);
            db.SaveChanges();
            return RedirectToAction("Index");
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
