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
using Microsoft.AspNet.Identity;
using EdgeRealEstate.Models.ViewModels;

namespace EdgeRealEstate.Controllers
{
    [Authorize]

    public class MessagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Messages
        public async Task<ActionResult> Index()
        {
            var CurrentUserID = User.Identity.GetUserId();
            return View(await db.Messages.Where(i=>i.ReceiverId == CurrentUserID).ToListAsync());
        }

        // GET: Messages/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = await db.Messages.FindAsync(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // GET: Messages/Create
        public ActionResult Create()
        {
            var CurrentUserID = User.Identity.GetUserId();
            ViewBag.Users = db.Users.Where(i=>i.Id != CurrentUserID).ToList();
            return View();
        }

        // POST: Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "MessageContent,ReceiverId")] Message message)
        {
            if (ModelState.IsValid)
            {
                message.SenderId = User.Identity.GetUserId();
                message.SentAt = DateTime.Now;
                db.Messages.Add(message);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(message);
        }
        public JsonResult Add([Bind(Include = "MessageContent,ReceiverId")] Message message)
        {

            if (ModelState.IsValid)
            {
                message.SenderId = User.Identity.GetUserId();
                message.SenderUsername = db.Users.Find(User.Identity.GetUserId()).UserName;
                message.ReceiverUsername = db.Users.Find(message.ReceiverId).UserName;
                message.Seen = false;
                message.SentAt = DateTime.Now;
                db.Messages.Add(message);
                db.SaveChanges();
                return Json(new
                {
                    status = 1,
                    msg = "Successfully Sent"
                });
            }

            return Json(new
            {
                status = 0,
                msg = "Failed To Send"
            });
        }

        public ActionResult GetMessages()
        {

            var CurrentUserID = User.Identity.GetUserId();
            var Messages = db.Messages.Where(i => i.ReceiverId == CurrentUserID).OrderByDescending(i => i.SentAt).ToList();
            return View(Messages);


        }
        public ActionResult GetMessagesForUser(string ID)
        {
            if (string.IsNullOrEmpty(ID))
            {
                return HttpNotFound();

            }
            var CurrentUserID = User.Identity.GetUserId();
            var Messages = db.Messages.Where(i => i.ReceiverId == CurrentUserID && i.SenderId == ID).OrderByDescending(i => i.SentAt).ToList();
            ViewBag.SenderName = db.Users.Find(ID).UserName;
            return View(Messages);


        }
        public ActionResult GetMessagesMenu()
        {
            var CurrentUserID = User.Identity.GetUserId();
            //var Messages = db.Messages.ToList();
            //ViewBag.MSG = db.Messages.ToList();
            ViewBag.MSG = db.Messages.Where(i => i.ReceiverId == CurrentUserID).OrderByDescending(i => i.SentAt).Take(5);

            return PartialView("_MessagesListMenu");
            //return Json(new
            //{
            //    status = 1,
            //    Messages = Messages
            //});

        }
        //public ActionResult ReceivedMessages(string ID)
        //{
        //    if (string.IsNullOrEmpty(ID))
        //    {
        //        return HttpNotFound();

        //    }
        //    var CurrentUserID = User.Identity.GetUserId();
        //    var messages = db.Messages.Where(i => i.ReceiverId == CurrentUserID && i.SenderId == ID).OrderByDescending(i => i.ID).ToList();
        //    ViewBag.SenderName = db.Users.Find(ID).UserName;
        //    return View(messages);


        //}
        public JsonResult GetMessagesNumber()
        {
            var CurrentUserID = User.Identity.GetUserId();
            var MessagesNumber = db.Messages.Where(i => i.Seen == false && i.ReceiverId == CurrentUserID).Count();
            //var MessagesNumber = db.Messages.Where(i => i.ReceiverId == CurrentUserID && i.Seen == false).Count();
            return Json(new
            {
                Number = MessagesNumber

            });


        }
        public JsonResult MarkAsSeen(int ID)
        {
            var CurrentUserID = User.Identity.GetUserId();
            Message MessagesNumber = db.Messages.FirstOrDefault(i => i.Seen == false && i.ReceiverId == CurrentUserID);
            MessagesNumber.Seen = true;
            db.SaveChanges();
            //var MessagesNumber = db.Messages.Where(i => i.ReceiverId == CurrentUserID && i.Seen == false).Count();MarkAsSeen
            return Json(new
            {
                status = 1

            });


        }
        public ActionResult View(int? ID)
        {
            string CurrentUserID = User.Identity.GetUserId();

            if (ID != null || ID != 0)
            {
                Message mes = db.Messages.FirstOrDefault(i=>i.ID == ID);
                return View(mes);

            }
            return HttpNotFound();

        }

        // GET: Messages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = await db.Messages.FindAsync(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,MessageContent,Seen,SenderId,ReceiverId,SentAt")] Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(message);
        }

        // GET: Messages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Message message = await db.Messages.FindAsync(id);
            if (message == null)
            {
                return HttpNotFound();
            }
            return View(message);
        }

        // POST: Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Message message = await db.Messages.FindAsync(id);
            db.Messages.Remove(message);
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
