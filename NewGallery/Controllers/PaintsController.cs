using System;
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
    public class PaintsController : Controller
    {
        private MyDB db = new MyDB();

        // GET: Paints
        public ActionResult Index()
        {
            var paints = db.Paints.Include(p => p.Artist);
            return View(paints.ToList());
        }

        // GET: Paints/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paint paint = db.Paints.Find(id);
            if (paint == null)
            {
                return HttpNotFound();
            }
            return View(paint);
        }

        // GET: Paints/Create
        public ActionResult Create()
        {
            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "ArtistName");
            return View();
        }

        // POST: Paints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaintID,CreateDate,Size,Price,Type,ArtistID")] Paint paint)
        {
            if (ModelState.IsValid)
            {
                db.Paints.Add(paint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "ArtistName", paint.ArtistID);
            return View(paint);
        }

        // GET: Paints/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paint paint = db.Paints.Find(id);
            if (paint == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "ArtistName", paint.ArtistID);
            return View(paint);
        }

        // POST: Paints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaintID,CreateDate,Size,Price,Type,ArtistID")] Paint paint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "ArtistName", paint.ArtistID);
            return View(paint);
        }

        // GET: Paints/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Paint paint = db.Paints.Find(id);
            if (paint == null)
            {
                return HttpNotFound();
            }
            return View(paint);
        }

        // POST: Paints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Paint paint = db.Paints.Find(id);
            db.Paints.Remove(paint);
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

        public ActionResult Search(string size, string type, int? price)
        {
            List<Paint> Paintlist = new List<Paint>();

            if ((size == null || size == "") && (type == null || type == "") && price == null)
            {
                return RedirectToAction("Index");
            }


            if ((type == null || type == "") && price == null)
            {
                foreach (Paint p in db.Paints)
                {
                    if (p.Size.Contains(size))
                    {
                        Paintlist.Add(p);
                    }
                }
                return View(Paintlist);
            }
            else if ((size == null || size == "") && price == null)
            {
                foreach (Paint p in db.Paints)
                {
                    if (p.Type.Contains(type))
                    {
                        Paintlist.Add(p);
                    }
                }
                return View(Paintlist);
            }
            else if ((size == null || size == "") && (type == null || type == ""))
            {
                foreach (Paint p in db.Paints)
                {
                    if (p.Price <= price)
                    {
                        Paintlist.Add(p);
                    }
                }
                return View(Paintlist);
            }
            else if (size == null || size == "")
            {
                foreach (Paint p in db.Paints)
                {
                    if (p.Price <= price && p.Type.Contains(type))
                    {
                        Paintlist.Add(p);
                    }
                }
                return View(Paintlist);
            }

            else if (type == null || type == "")
            {
                foreach (Paint p in db.Paints)
                {
                    if (p.Size.Contains(size) && p.Price <= price) 
                    {
                        Paintlist.Add(p);
                    }
                }
                return View(Paintlist);
            }
            else if (price == null)
            {
                foreach (Paint p in db.Paints)
                {
                    if (p.Type.Contains(type) && p.Size.Contains(size))
                    {
                        Paintlist.Add(p);
                    }
                }
                return View(Paintlist);
            }
            else
            {
                foreach (Paint p in db.Paints)
                {
                    if (p.Size.Contains(size) && p.Type.Contains(type) && p.Price <= price)
                    {
                        Paintlist.Add(p);
                    }
                }
                return View(Paintlist);
            }

        }


    }
}
