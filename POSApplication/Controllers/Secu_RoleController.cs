using POSApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;



namespace POSApplication.Controllers
{
    [Authorize]
    public class Secu_RoleController : Controller
    {
        private SCMSContext db = new SCMSContext();

        // GET: Secu_Role
        public ActionResult Index()
        {
            return View(db.Secu_Role.ToList());
        }



       
        public ActionResult Create()
        {
           

            ViewBag.CloneRole = new SelectList(db.Secu_Role, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Secu_Role secu_Role, int? CloneRole)
        {
            if (ModelState.IsValid)
            {
                using (var dbContextTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        secu_Role.OpBy = 1;
                        secu_Role.OpOn = DateTime.Now;
                        db.Secu_Role.Add(secu_Role);
                        db.SaveChanges();

                        //if (CloneRole != null)
                        //{
                        //    var rolePerMas = new Secu_RolePermissionMas()
                        //    {
                        //        Id = 0,
                        //        Secu_RoleId = secu_Role.Id,
                        //        OpBy = Convert.ToInt32(Session["uid"].ToString()),
                        //        OpOn = DateTime.Now
                        //    };

                        //    db.Secu_RolePermissionMas.Add(rolePerMas);
                        //    //db.Entry(rolePerMas).State = EntityState.Added;
                        //    db.SaveChanges();

                        //    var getRolePerm = db.Secu_RolePermissionDet.AsNoTracking().Where(x => x.Secu_RolePermissionMas.Secu_RoleId == CloneRole).ToList();

                        //    foreach (var item in getRolePerm)
                        //    {
                        //        var roleDet = new Secu_RolePermissionDet()
                        //        {
                        //            //Id = 0,
                        //            Secu_RolePermissionMasId = rolePerMas.Id,
                        //            Secu_FormsId = item.Secu_FormsId,
                        //            DataViewPerm = item.DataViewPerm,
                        //            DataAddPerm = item.DataAddPerm,
                        //            DataEditPerm = item.DataEditPerm,
                        //            DataDeletePerm = item.DataDeletePerm
                        //        };

                        //        db.Secu_RolePermissionDet.Add(roleDet);
                        //        //db.Entry(roleDet).State = EntityState.Added;
                        //        db.SaveChanges();
                        //    }
                        //}

                        dbContextTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();

                    }
                }

                return RedirectToAction("Index");
            }

            return View(secu_Role);
        }

        // GET: Secu_Role/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Secu_Role secu_Role = db.Secu_Role.Find(id);
            if (secu_Role == null)
            {
                return HttpNotFound();
            }
            return View(secu_Role);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Secu_Role secu_Role)
        {
            if (ModelState.IsValid)
            {
                secu_Role.OpBy = 1;
                secu_Role.OpOn = DateTime.Now;
                db.Entry(secu_Role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(secu_Role);
        }

        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Secu_Role secu_Role = db.Secu_Role.Find(id);
        //    if (secu_Role == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(secu_Role);
        //}


        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Secu_Role secu_Role = db.Secu_Role.Find(id);
        //    db.Secu_Role.Remove(secu_Role);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}


        public JsonResult GetNames()
        {
            var data = db.Secu_Role.Select(y => new { Name = y.Name, Id = y.Id }).ToList();

            return Json(data, JsonRequestBehavior.AllowGet);

        }


        public JsonResult DeleteItem(int Id)
        {
            var result = new
            {
                flag = false,
                message = "Error occured. !"
            };

            var checkData = db.Secu_User.Where(x => x.RoleId == Id).ToList();

           


            var Item = db.Secu_Role.SingleOrDefault(x => x.Id == Id);
                db.Secu_Role.Remove(Item);
                db.SaveChanges();

                try
                {
                    using (var dbContextTransaction = db.Database.BeginTransaction())
                    {
                        try
                        {
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