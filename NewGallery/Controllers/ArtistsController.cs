﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NewGallery.Models;

namespace NewGallery.Controllers
{
    public class ArtistsController : Controller
    {
        private MyDB db = new MyDB();

        // GET: Artists
        public ActionResult Index()
        {
            return View(db.Artists.ToList());
        }

        // GET: Artists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        // GET: Artists/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Artists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArtistID,ArtistName,Email,FavoriteStyle,Rate")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                db.Artists.Add(artist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(artist);
        }

        // GET: Artists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        // POST: Artists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArtistID,ArtistName,Email,FavoriteStyle,Rate")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artist);
        }

        // GET: Artists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Artist artist = db.Artists.Find(id);
            if (artist == null)
            {
                return HttpNotFound();
            }
            return View(artist);
        }

        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Artist artist = db.Artists.Find(id);
            db.Artists.Remove(artist);
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

        public ActionResult Search(string name, string favstyle, int? rate)
        {
            List<Artist> Artistlist = new List<Artist>();

            if ((name == null || name == "") && (favstyle == null || favstyle == "")&& rate==null)
            {
                return RedirectToAction("Index");
            }


            if ((favstyle == null || favstyle == "")&&rate==null)
            {
                foreach (Artist a in db.Artists)
                {
                    if (a.ArtistName.Contains(name))
                    {
                        Artistlist.Add(a);
                    }
                }
                return View(Artistlist);
            }
            else if ((name == null || name == "")&& rate==null)
            {
                foreach (Artist a in db.Artists)
                {
                    if (a.FavoriteStyle.Contains(favstyle))
                    {
                        Artistlist.Add(a);
                    }
                }
                return View(Artistlist);
            }
            else if ((name == null || name == "") && (favstyle == null || favstyle == ""))
            {
                foreach (Artist a in db.Artists)
                {
                    if (a.Rate>=rate)
                    {
                        Artistlist.Add(a);
                    }
                }
                return View(Artistlist);
            }
            else if (name == null || name == "")
            {
                foreach (Artist a in db.Artists)
                {
                    if (a.Rate >= rate && a.FavoriteStyle.Contains(favstyle))
                    {
                        Artistlist.Add(a);
                    }
                }
                return View(Artistlist);
            }

            else if (favstyle == null || favstyle == "")
            {
                foreach (Artist a in db.Artists)
                {
                    if (a.ArtistName.Contains(name)&& a.Rate >= rate)
                    {
                        Artistlist.Add(a);
                    }
                }
                return View(Artistlist);
            }
            else if (rate==null)
            {
                foreach (Artist a in db.Artists)
                {
                    if (a.FavoriteStyle.Contains(favstyle)&& a.ArtistName.Contains(name))
                    {
                        Artistlist.Add(a);
                    }
                }
                return View(Artistlist);
            }
            else
            {
                foreach (Artist a in db.Artists)
                {
                    if (a.FavoriteStyle.Contains(favstyle) && a.ArtistName.Contains(name)&&a.Rate>=rate)
                    {
                        Artistlist.Add(a);
                    }
                }
                return View(Artistlist);
            }

        }

    }
}

