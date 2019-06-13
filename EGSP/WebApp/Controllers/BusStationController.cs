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
using WebApp.DTO;
using WebApp.Models;
using WebApp.Persistence;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Controllers
{

    [Authorize(Roles = "Admin")]
    [RoutePrefix("api/BusStation")]
    public class BusStationController : ApiController
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        IDemoUnitOfWork uow = new DemoUnitOfWork(new ApplicationDbContext());

        // GET: api/BusStation
        [AllowAnonymous]
        public IEnumerable<BusStation> GetBusStations()
        {
            return uow.BusStationRepository.GetAll();
        }

        // GET: api/BusStation/5
        [ResponseType(typeof(BusStation))]
        [AllowAnonymous]
        public IHttpActionResult GetBusStation(int id)
        {
            BusStation busStation = uow.BusStationRepository.Get(id);
            if (busStation == null)
            {
                return NotFound();
            }

            return Ok(busStation);
        }

        // PUT: api/BusStation/5
        [ResponseType(typeof(BusStation))]
        public IHttpActionResult PutBusStation(int id, BusStationDataDTO busStationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<BusLine> busLines = new List<BusLine>();
            if (busStationDTO.BusLines != null)
            {
                foreach (var line in busStationDTO.BusLines)
                {
                    var busLine = uow.BusLineRepository.Get(line.Id);
                    if (busLine == null)
                    {
                        return BadRequest("No bus line with id: " + line.Id);
                    }
                    busLines.Add(busLine);
                }
            }

            BusStation busStation = uow.BusStationRepository.Get(id);
            if (busStation == null)
            {
                return NotFound();
            }

            busStation.Address = busStationDTO.Address;
            busStation.Latitude = busStationDTO.Latitude;
            busStation.Longitude = busStationDTO.Longitude;
            busStation.Name = busStationDTO.Name;

            busStation.BusLines.Clear();

            foreach (var busLine in busLines)
            {
                busStation.BusLines.Add(busLine);
            }

            uow.Complete();
            return Ok(busStation);
        }

        // POST: api/BusStation
        [ResponseType(typeof(BusStation))]
        public IHttpActionResult PostBusStation(BusStationDataDTO busStationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<BusLine> busLines = new List<BusLine>();
            if (busStationDTO.BusLines != null)
            {
                foreach (var line in busStationDTO.BusLines)
                {
                    var busLine = uow.BusLineRepository.Get(line.Id);
                    if (busLine == null)
                    {
                        return BadRequest("No bus line with id: " + line.Id);
                    }
                    busLines.Add(busLine);
                }
            }

            BusStation busStation = new BusStation()
            {
                Address = busStationDTO.Address,
                Latitude = busStationDTO.Latitude,
                Longitude = busStationDTO.Longitude,
                Name = busStationDTO.Name
            };

            uow.BusStationRepository.Add(busStation);
            if(busStation.BusLines == null)
            {
                busStation.BusLines = new List<BusLine>();
            }

            foreach (var busLine in busLines)
            {
                busStation.BusLines.Add(busLine);
            }

            uow.Complete();

            return CreatedAtRoute("DefaultApi", new { id = busStation.Id }, busStation);
        }

        // DELETE: api/BusStation/5
        [ResponseType(typeof(BusStation))]
        public IHttpActionResult DeleteBusStation(int id)
        {
            BusStation busStation = uow.BusStationRepository.Get(id);
            if (busStation == null)
            {
                return NotFound();
            }

            uow.BusStationRepository.Remove(busStation);
            uow.Complete();

            return Ok(busStation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                uow.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BusStationExists(int id)
        {
            return uow.BusStationRepository.Get(id) != null;
        }
    }
}