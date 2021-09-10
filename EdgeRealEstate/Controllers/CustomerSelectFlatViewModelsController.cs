using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EdgeRealEstate.Models;
using EdgeRealEstate.Models.ViewModels;

namespace EdgeRealEstate.Controllers
{
    public class CustomerSelectFlatViewModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CustomerSelectFlatViewModels
        public async Task<ActionResult> Index()
        {
            return View(await db.CustomerSelectFlatViewModels.ToListAsync());
        }

        // GET: CustomerSelectFlatViewModels/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerSelectFlatViewModel customerSelectFlatViewModel = await db.CustomerSelectFlatViewModels.FindAsync(id);
            if (customerSelectFlatViewModel == null)
            {
                return HttpNotFound();
            }
            return View(customerSelectFlatViewModel);
        }

        // GET: CustomerSelectFlatViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerSelectFlatViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,CustomerId,CustomerName,FlatId,Flatname,BuildingId,BuildingCode,Projectid,ProjectName,CurrntlyDate,CostMony")] CustomerSelectFlatViewModel customerSelectFlatViewModel)
        {
            if (ModelState.IsValid)
            {
                db.CustomerSelectFlatViewModels.Add(customerSelectFlatViewModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(customerSelectFlatViewModel);
        }

        // GET: CustomerSelectFlatViewModels/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerSelectFlatViewModel customerSelectFlatViewModel = await db.CustomerSelectFlatViewModels.FindAsync(id);
            if (customerSelectFlatViewModel == null)
            {
                return HttpNotFound();
            }
            return View(customerSelectFlatViewModel);
        }

        // POST: CustomerSelectFlatViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,CustomerId,CustomerName,FlatId,Flatname,BuildingId,BuildingCode,Projectid,ProjectName,CurrntlyDate,CostMony")] CustomerSelectFlatViewModel customerSelectFlatViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customerSelectFlatViewModel).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(customerSelectFlatViewModel);
        }

        // GET: CustomerSelectFlatViewModels/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomerSelectFlatViewModel customerSelectFlatViewModel = await db.CustomerSelectFlatViewModels.FindAsync(id);
            if (customerSelectFlatViewModel == null)
            {
                return HttpNotFound();
            }
            return View(customerSelectFlatViewModel);
        }

        // POST: CustomerSelectFlatViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CustomerSelectFlatViewModel customerSelectFlatViewModel = await db.CustomerSelectFlatViewModels.FindAsync(id);
            db.CustomerSelectFlatViewModels.Remove(customerSelectFlatViewModel);
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
