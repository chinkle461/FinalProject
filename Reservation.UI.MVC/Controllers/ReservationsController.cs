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
    public class ReservationsController : Controller
    {
        private ReservationEntities db = new ReservationEntities();

        // GET: Reservations
        [Authorize(Roles = "Employee, Admin")]
        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(r => r.Location).Include(r => r.OwnerAsset);
            return View(reservations.ToList());
        }

        // GET: Reservations/Details/5
        [Authorize(Roles = "Employee, Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation.DATA.EF.Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationName");
            ViewBag.OwnerAssetId = new SelectList(db.OwnerAssets, "OwnerAssetId", "AssetName");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservationId,OwnerAssetId,LocationId,ReservationDate")] Reservation.DATA.EF.Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationName", reservation.LocationId);
            ViewBag.OwnerAssetId = new SelectList(db.OwnerAssets, "OwnerAssetId", "AssetName", reservation.OwnerAssetId);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation.DATA.EF.Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationName", reservation.LocationId);
            ViewBag.OwnerAssetId = new SelectList(db.OwnerAssets, "OwnerAssetId", "AssetName", reservation.OwnerAssetId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservationId,OwnerAssetId,LocationId,ReservationDate")] Reservation.DATA.EF.Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LocationId = new SelectList(db.Locations, "LocationId", "LocationName", reservation.LocationId);
            ViewBag.OwnerAssetId = new SelectList(db.OwnerAssets, "OwnerAssetId", "AssetName", reservation.OwnerAssetId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        [Authorize(Roles = "Employee, Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation.DATA.EF.Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation.DATA.EF.Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
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