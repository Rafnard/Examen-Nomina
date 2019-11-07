﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExamenNomina.Models;

namespace ExamenNomina.Controllers
{
    public class NominaController : Controller
    {
        private NominaDBEntities db = new NominaDBEntities();

        // GET: Nomina
        public ActionResult Index()
        {
            var nominas = db.Nominas.Include(n => n.Empleado).Where(n => n.Empleado.Activo == true);
            return View(nominas.ToList());
        }

        // GET: Nomina/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nomina nomina = db.Nominas.Find(id);
            if (nomina == null)
            {
                return HttpNotFound();
            }
            return View(nomina);
        }

        // GET: Nomina/Create
        public ActionResult Create()
        {
        
            ViewBag.IdEmpleado = new SelectList(db.Empleadoes, "IdEmpleado", "NomEmpleado");
            return View();
        }

        // POST: Nomina/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdNomina,IdEmpleado,Fecha,SualdoDia,Dias,SueldoQuincenal")] Nomina nomina)
        {
            if (ModelState.IsValid)
            {
                db.Nominas.Add(nomina);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdEmpleado = new SelectList(db.Empleadoes, "IdEmpleado", "NomEmpleado", nomina.IdEmpleado);
            return View(nomina);
        }

        // GET: Nomina/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nomina nomina = db.Nominas.Find(id);
            if (nomina == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEmpleado = new SelectList(db.Empleadoes, "IdEmpleado", "NomEmpleado", nomina.IdEmpleado);
            return View(nomina);
        }

        // POST: Nomina/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdNomina,IdEmpleado,Fecha,SualdoDia,Dias,SueldoQuincenal")] Nomina nomina)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nomina).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdEmpleado = new SelectList(db.Empleadoes, "IdEmpleado", "NomEmpleado", nomina.IdEmpleado);
            return View(nomina);
        }

        // GET: Nomina/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nomina nomina = db.Nominas.Find(id);
            if (nomina == null)
            {
                return HttpNotFound();
            }
            return View(nomina);
        }

        // POST: Nomina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nomina nomina = db.Nominas.Find(id);
            db.Nominas.Remove(nomina);
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
