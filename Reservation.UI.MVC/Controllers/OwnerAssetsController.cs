﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Reservation.DATA.EF;
using Reservation.UI.MVC.Models;

namespace Reservation.UI.MVC.Controllers
{
    public class OwnerAssetsController : Controller
    {
        private ReservationEntities db = new ReservationEntities();

        // GET: OwnerAssets
        [Authorize(Roles = "Employee, Admin")]
        public ActionResult Index()
        {
            var ownerAssets = db.OwnerAssets.Include(o => o.UserDetail);
            return View(ownerAssets.ToList());
        }

        // GET: OwnerAssets/Details/5
        [Authorize(Roles = "Employee, Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnerAsset ownerAsset = db.OwnerAssets.Find(id);
            if (ownerAsset == null)
            {
                return HttpNotFound();
            }
            return View(ownerAsset);
        }

        // GET: OwnerAssets/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.OwnerId = new SelectList(db.UserDetails, "UserId", "FirstName");
            return View();
        }

        // POST: OwnerAssets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OwnerAssetId,AssetName,OwnerId,AssetPhoto,SpecialNotes,IsActive,DateAdded,Relationship")] OwnerAsset ownerAsset, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                #region File Upload
                string imageName = "noImage.png";

                if (image != null)
                {

                    //get the file name for image 
                    imageName = image.FileName;

                    string ext = imageName.Substring(imageName.LastIndexOf('.'));

                    //create a list of valid extensions
                    string[] goodExts = { ".jpg", ".jpeg", ".png", ".gif" };

                    if (goodExts.Contains(ext.ToLower()) && image.ContentLength <= 4194304)
                    {
                        imageName = Guid.NewGuid() + ext;

                        image.SaveAs(Server.MapPath("~/Content/images" + imageName));
                    }

                    else
                    {
                        imageName = "noImage.png";
                    }
                    ownerAsset.AssetPhoto = imageName;
                }
                #endregion

                db.OwnerAssets.Add(ownerAsset);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OwnerId = new SelectList(db.UserDetails, "UserId", "FirstName", ownerAsset.OwnerId);
            return View(ownerAsset);
        }

        // GET: OwnerAssets/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnerAsset ownerAsset = db.OwnerAssets.Find(id);
            if (ownerAsset == null)
            {
                return HttpNotFound();
            }
            ViewBag.OwnerId = new SelectList(db.UserDetails, "UserId", "FirstName", ownerAsset.OwnerId);
            return View(ownerAsset);
        }

        // POST: OwnerAssets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OwnerAssetId,AssetName,OwnerId,AssetPhoto,SpecialNotes,IsActive,DateAdded,Relationship")] OwnerAsset ownerAsset, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                #region File Upload
                string imageName = ownerAsset.AssetPhoto;

                if (image != null)
                {

                    //get the file name for image 
                    imageName = image.FileName;

                    string ext = imageName.Substring(imageName.LastIndexOf('.'));

                    //create a list of valid extensions
                    string[] goodExts = { ".jpg", ".jpeg", ".png", ".gif" };

                    if (goodExts.Contains(ext.ToLower()) && image.ContentLength <= 4194304)
                    {
                        imageName = Guid.NewGuid() + ext;

                        image.SaveAs(Server.MapPath("~/Content/images/" + imageName));
                    }

                    else
                    {
                        imageName = "noImage.png";
                    }
                    ownerAsset.AssetPhoto = imageName;
                }
                #endregion
                db.Entry(ownerAsset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerId = new SelectList(db.UserDetails, "UserId", "FirstName", ownerAsset.OwnerId);
            return View(ownerAsset);
        }

        // GET: OwnerAssets/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OwnerAsset ownerAsset = db.OwnerAssets.Find(id);
            if (ownerAsset == null)
            {
                return HttpNotFound();
            }
            return View(ownerAsset);
        }

        // POST: OwnerAssets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OwnerAsset ownerAsset = db.OwnerAssets.Find(id);
            db.OwnerAssets.Remove(ownerAsset);
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