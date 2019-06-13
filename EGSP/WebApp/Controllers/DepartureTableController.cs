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
using WebApp.Models;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DepartureTableController : ApiController
    {
        IDemoUnitOfWork uow = new DemoUnitOfWork(new ApplicationDbContext());

        // GET: api/DepartureTable
        [AllowAnonymous]
        public IEnumerable<DepartureTable> GetDepartureTables()
        {
            return uow.DepartureRepository.GetAll();
        }

        // GET: api/DepartureTable/5
        [AllowAnonymous]
        [ResponseType(typeof(DepartureTable))]
        public IHttpActionResult GetDepartureTable(int id)
        {
            DepartureTable departureTable = uow.DepartureRepository.Get(id);
            if (departureTable == null)
            {
                return NotFound();
            }

            return Ok(departureTable);
        }

        // PUT: api/DepartureTable/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDepartureTable(int id, DepartureTable departureTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != departureTable.Id)
            {
                return BadRequest();
            }

            DepartureTable dt = uow.DepartureRepository.Get(id);

            if (dt == null)
            {
                return NotFound();
            }

            dt.BusLineId = departureTable.BusLineId;
            dt.DayOfWeek = departureTable.DayOfWeek;
            dt.DepartureTimes = departureTable.DepartureTimes;

            uow.Complete();

            return Ok(dt);
        }

        // POST: api/DepartureTable
        [ResponseType(typeof(DepartureTable))]
        public IHttpActionResult PostDepartureTable(DepartureTable departureTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            uow.DepartureRepository.Add(departureTable);
            uow.Complete();

            return CreatedAtRoute("DefaultApi", new { id = departureTable.Id }, departureTable);
        }

        // DELETE: api/DepartureTable/5
        [ResponseType(typeof(DepartureTable))]
        public IHttpActionResult DeleteDepartureTable(int id)
        {
            DepartureTable departureTable = uow.DepartureRepository.Get(id);
            if (departureTable == null)
            {
                return NotFound();
            }

            uow.DepartureRepository.Remove(departureTable);
            uow.Complete();

            return Ok(departureTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                uow.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DepartureTableExists(int id)
        {
            return uow.DepartureRepository.Get(id) != null;
        }
    }
}