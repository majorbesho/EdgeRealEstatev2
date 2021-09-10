using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EdgeRealEstate.Entities;
using EdgeRealEstate.Models;

namespace EdgeRealEstate.Controllers
{
    public class CustomerSelectFlatsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CustomerSelectFlats
        public ActionResult GetAllFlattype(int? flatId)
        {
            var GetFlatTypes = db.Flats.Where(a => a.FlatTypeId == 5 && a.id==flatId);

            //var AllPayment = db.CustPaperPayments.Where(a => a.customerId == custId).Sum(x => x.paid);
            //var Allricet = db.CustPaperReceipts.Where(a => a.customerId == custId).Sum(x => x.paid);
            //var valforCUst =AllPayment - Allricet ;

            return View();

        }
        public async Task<ActionResult> Index()
        {
            var FlatType1 = db.FlatTypes.ToListAsync();
            var FlatType = db.FlatTypes.ToListAsync();

            var customerSelectFlat = db.CustomerSelectFlat.Include(c => c.Flat);
            return View(await customerSelectFlat.ToListAsync());
        }

        // GET: CustomerSelectFlats/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerSelectFlat customerSelectFlat = await db.CustomerSelectFlat.FindAsync(id);
            if (customerSelectFlat == null)
            {
                return HttpNotFound();
            }
            return View(customerSelectFlat);
        }

        // GET: CustomerSelectFlats/Create
        public ActionResult Create(int? Id)
        {
           // GetAllCustomerPayment(custId);

            ViewBag.Customer = new SelectList(db.Customers, "Id", "ARName");
            var AllPayment = db.CustPaperPayments.Where(a => a.customerId == Id).Sum(x => x.paid);
            ViewBag.AllPayment = new SelectList(db.Customers, "Id", "ARName");
            //ViewBag.Customer = new SelectList(db.Customers, "Id", "ARName");
            ViewBag.FlatId = new SelectList(db.Flats, "id", "Aname",Id);
            return View();
        }

        // POST: CustomerSelectFlats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create( CustomerSelectFlat customerSelectFlat)
        {
            if (ModelState.IsValid)
            {
                db.CustomerSelectFlat.Add(customerSelectFlat);
                
                await db.SaveChangesAsync();
                Flat flat = db.Flats.Where(a => a.id == customerSelectFlat.FlatId).SingleOrDefault();
                flat.FlatTypeId = 5;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.Customer = new SelectList(db.Customers, "Id", "ARName");
            ViewBag.FlatId = new SelectList(db.Flats, "id", "Aname", customerSelectFlat.FlatId);
            return View(customerSelectFlat);
        }

        // GET: CustomerSelectFlats/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerSelectFlat customerSelectFlat = await db.CustomerSelectFlat.FindAsync(id);
            if (customerSelectFlat == null)
            {
                return HttpNotFound();
            }
            ViewBag.Customer = new SelectList(db.Customers, "Id", "ARName");
            ViewBag.FlatId = new SelectList(db.Flats, "id", "Aname", customerSelectFlat.FlatId);
            return View(customerSelectFlat);
        }

        // POST: CustomerSelectFlats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CustomerSelectFlat customerSelectFlat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerSelectFlat).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FlatId = new SelectList(db.Flats, "id", "Aname", customerSelectFlat.FlatId);
            return View(customerSelectFlat);
        }

        // GET: CustomerSelectFlats/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerSelectFlat customerSelectFlat = await db.CustomerSelectFlat.FindAsync(id);
            if (customerSelectFlat == null)
            {
                return HttpNotFound();
            }
            return View(customerSelectFlat);
        }

        // POST: CustomerSelectFlats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CustomerSelectFlat customerSelectFlat = await db.CustomerSelectFlat.FindAsync(id);
            db.CustomerSelectFlat.Remove(customerSelectFlat);
            await db.SaveChangesAsync();
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
