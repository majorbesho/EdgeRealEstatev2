using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EdgeRealEstate.Models;

namespace EdgeRealEstate.Controllers
{
    public class AssigningContributorsToProjectController0 : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: AssigningContributorsToProject
        // GET: Flats
        public async Task<ActionResult> Index()
        {
           

            ViewBag.ListOfProject = new SelectList(db.Projectes, "id", "Aname");
            ViewBag.Contributors = new SelectList(db.Contributor, "id", "Aname");

            return View(await db.Flats.ToListAsync());
        }

        public async Task<ActionResult> Create()
        {

            return View();
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