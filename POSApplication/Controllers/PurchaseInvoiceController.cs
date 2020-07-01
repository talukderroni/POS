﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using POSApplication.Models;
using System.Web.Security;
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
     
        public JsonResult SavePurchaseInvoice(PurchaseInvoiceMa purchaseInvoiceMa, List<PurchaseInvoiceDet> PurchaseInvoicedDetails)
        {
            var result = new
            {
                flag = false,
                message = "Error occured. !",

            };


            if (ModelState.IsValid)
            {

                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        purchaseInvoiceMa.UserFullName = Session["UserFullName"].ToString();
                      
                        db.PurchaseInvoiceMas.Add(purchaseInvoiceMa);
                        db.SaveChanges();

                        foreach (var item in PurchaseInvoicedDetails)
                        {
                            PurchaseInvoiceDet det = new PurchaseInvoiceDet();

                            det.PurchaseInvoiceMasId = purchaseInvoiceMa.Id;

                            det.ProductCategoryId = item.ProductCategoryId;
                          
                            det.Quantity = item.Quantity;
                            det.PurchasePrize = item.PurchasePrize;
                            det.Amount = item.Amount;
                            det.ProductId = item.ProductId;

                            db.PurchaseInvoiceDets.Add(det);
                            db.SaveChanges();
                        }


                        transaction.Commit();
                        result = new
                        {
                            flag = true,
                            message = "Succes occured. !",

                        };
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        result = new
                        {
                            flag = false,
                            message = "Fail occured. !",

                        };

                        var message = ex.Message;




                    }
                }
            }

            return Json(result,JsonRequestBehavior.AllowGet);
        }
        

             [HttpPost]

        public JsonResult UpdatePurchaseInvoice(PurchaseInvoiceMa purchaseInvoiceMa, List<PurchaseInvoiceDet> PurchaseInvoicedDetails)
        {
            var result = new
            {
                flag = false,
                message = "Error occured. !",

            };


            if (ModelState.IsValid)
            {

                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        purchaseInvoiceMa.UserFullName = Session["UserFullName"].ToString();

                        if (purchaseInvoiceMa.Id > 0)
                        {
                            var data = db.PurchaseInvoiceDets.Where(x => x.PurchaseInvoiceMasId == purchaseInvoiceMa.Id).ToList();
                            db.PurchaseInvoiceDets.RemoveRange(data);
                            db.SaveChanges();
                        }

                      
                        db.Entry(purchaseInvoiceMa).State = EntityState.Modified;
                        db.SaveChanges();
                      

                        foreach (var item in PurchaseInvoicedDetails)
                        {
                            PurchaseInvoiceDet det = new PurchaseInvoiceDet();

                            det.PurchaseInvoiceMasId = purchaseInvoiceMa.Id;

                            det.ProductCategoryId = item.ProductCategoryId;

                            det.Quantity = item.Quantity;
                            det.PurchasePrize = item.PurchasePrize;
                            det.Amount = item.Amount;
                            det.ProductId = item.ProductId;

                            db.PurchaseInvoiceDets.Add(det);
                            db.SaveChanges();
                        }


                        transaction.Commit();
                        result = new
                        {
                            flag = true,
                            message = "Succes occured. !",

                        };
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        result = new
                        {
                            flag = false,
                            message = "Fail occured. !",

                        };

                        var message = ex.Message;




                    }
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);
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
            ViewBag.SupplierId = new SelectList(db.Suppliers.ToList(), "Id", "SupplierName", purchaseInvoiceMa.SupplierId);
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
        //--- Get Saved Data For Edit-----//
        public JsonResult GetSavedData(int? Id)
        {
          var data = db.PurchaseInvoiceDets.Where(x => x.PurchaseInvoiceMasId == Id).Select(x => new
                {
                    ProductCategoryId = x.ProductCategoryId,
                    ProductCatagoryName=x.ProductCategory.CategoryName,
                    ProductId = x.ProductId,
                    ProductName=x.Product.ProductName,
                    Qty=x.Quantity,
                    Amount= x.Amount,
              PurchasePrize=x.PurchasePrize

          
                }).ToList();

                return Json(data, JsonRequestBehavior.AllowGet);
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
        public JsonResult GetProductByCatagoryId(int? ProductCategoryId)
        {

            if (ProductCategoryId == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var data = db.Products.Where(x => x.ProductCategoryId == ProductCategoryId).Select(x => new
                {

                    Value = x.Id,
                    Text = x.ProductName

                }).Distinct().ToList();

                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }


        public JsonResult GetPrizeByProductId(int? ProductId)
        {

            if (ProductId == null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var data = db.Products.Where(x => x.Id == ProductId).Select(x => new
                {


                    PurchasePrize = x.PurchasePrice

                }).FirstOrDefault();

                return Json(data, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
       
        public ActionResult Delete(int Id)
        {
            PurchaseInvoiceMa purchaseInvoiceMa = db.PurchaseInvoiceMas.Find(Id);
            db.PurchaseInvoiceMas.Remove(purchaseInvoiceMa);
            db.SaveChanges();
            return Json(true,JsonRequestBehavior.AllowGet);
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
