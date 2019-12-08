using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DBModel;

namespace WebAPI_Server.Controllers
{
    public class CotizacionesController : ApiController
    {
        private DolarEntities db = new DolarEntities();

        // GET: api/Cotizaciones
        public IQueryable<Cotizaciones> GetCotizaciones()
        {
            return db.Cotizaciones;
        }

        // GET: api/Cotizaciones/5
        [ResponseType(typeof(Cotizaciones))]
        public IHttpActionResult GetCotizaciones(int id)
        {
            Cotizaciones cotizaciones = db.Cotizaciones.Find(id);
            if (cotizaciones == null)
            {
                return NotFound();
            }

            return Ok(cotizaciones);
        }

        // GET: api/Cotizaciones/5
        [ResponseType(typeof(Cotizaciones))]
        public IHttpActionResult GetCotizaciones(DateTime fecha)
        {
            var query = from a in db.Cotizaciones where a.Dia == fecha select a;
            Cotizaciones cotizaciones = query.FirstOrDefault();
            if (cotizaciones == null)
            {
                return NotFound();
            }

            return Ok(cotizaciones);
        }

        // PUT: api/Cotizaciones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCotizaciones(int id, Cotizaciones cotizaciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cotizaciones.IdCotizacion)
            {
                return BadRequest();
            }

            db.Entry(cotizaciones).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CotizacionesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Cotizaciones
        [ResponseType(typeof(Cotizaciones))]
        public IHttpActionResult PostCotizaciones(Cotizaciones cotizaciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cotizaciones.Add(cotizaciones);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cotizaciones.IdCotizacion }, cotizaciones);
        }

        // DELETE: api/Cotizaciones/5
        [ResponseType(typeof(Cotizaciones))]
        public IHttpActionResult DeleteCotizaciones(int id)
        {
            Cotizaciones cotizaciones = db.Cotizaciones.Find(id);
            if (cotizaciones == null)
            {
                return NotFound();
            }

            db.Cotizaciones.Remove(cotizaciones);
            db.SaveChanges();

            return Ok(cotizaciones);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CotizacionesExists(int id)
        {
            return db.Cotizaciones.Count(e => e.IdCotizacion == id) > 0;
        }
    }
}