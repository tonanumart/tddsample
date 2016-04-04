using CarPark.Model;
using CarPark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarPark.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CheckOut()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckOut(int Id)
        {
            using (var db = new CarParkDb())
            {
                Ticket ticket = db.Tickets.Find(Id);
                ticket.DateOut = DateTime.Now;
                db.SaveChanges();
                TempData["LastTicket"] = ticket;
            }
            return RedirectToAction("CheckOut");
        }

        public ActionResult CheckIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckIn(string PlateNo)
        {
            using (CarParkDb db = new CarParkDb())
            {
                Ticket ticket = new Ticket();
                ticket.PlateNo = PlateNo;
                ticket.DateIn = DateTime.Now;
                db.Tickets.Add(ticket);
                db.SaveChanges();
                TempData["LastTicket"] = ticket;
            }
            return RedirectToAction("CheckIn");
        }


    }
}