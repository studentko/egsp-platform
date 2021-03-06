﻿using System;
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
    public class BusLineController : ApiController
    {
        //private ApplicationDbContext db = new ApplicationDbContext();
        IDemoUnitOfWork uow = new DemoUnitOfWork(new ApplicationDbContext());

        // GET: api/BusLine
        [AllowAnonymous]
        public IEnumerable<BusLine> GetBusLines()
        {
            return uow.BusLineRepository.GetAll();
        }

        // GET: api/BusLine/5
        [AllowAnonymous]
        [ResponseType(typeof(BusLine))]
        public IHttpActionResult GetBusLine(int id)
        {
            BusLine busLine = uow.BusLineRepository.Get(id);
            if (busLine == null)
            {
                return NotFound();
            }

            return Ok(busLine);
        }

        // PUT: api/BusLine/5
        [ResponseType(typeof(BusLine))]
        public IHttpActionResult PutBusLine(int id, BusLineDataDTO busLineDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IList<BusStation> stations = GetStations(busLineDTO.BusStations);
            if (stations == null)
            {
                return BadRequest("Bad bus station list");
            }

            BusLine busLine = uow.BusLineRepository.Get(id);
            if (busLine == null)
            {
                return NotFound();
            }

            if (busLine.UpdateVersion != busLineDTO.UpdateVersion)
            {
                return BadRequest("Somebody else changed data");
            }

            PutBusLineData(busLine, busLineDTO, stations);

            uow.Complete();

            return Ok(busLine);
        }

        // POST: api/BusLine
        [ResponseType(typeof(BusLine))]
        public IHttpActionResult PostBusLine(BusLineDataDTO busLineDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IList<BusStation> stations = GetStations(busLineDTO.BusStations);
            if (stations == null)
            {
                return BadRequest("Bad bus station list");
            }

            BusLine busLine = new BusLine();
            PutBusLineData(busLine, busLineDTO, stations);

            uow.BusLineRepository.Add(busLine);
            uow.Complete();

            return CreatedAtRoute("DefaultApi", new { id = busLine.Id }, busLine);
        }

        private IList<BusStation> GetStations(IList<BusStation> BusStations)
        {
            IList<BusStation> stations = new List<BusStation>();
            foreach (var s in BusStations)
            {
                var station = uow.BusStationRepository.Get(s.Id);
                if (station == null)
                {
                    return null;
                }
                stations.Add(station);
            }
            return stations;
        }

        private void PutBusLineData(BusLine line, BusLineDataDTO data, IList<BusStation> busStations)
        {
            line.LineNumber = data.LineNumber;
            ++line.UpdateVersion;
            if(line.BusStations == null)
            {
                line.BusStations = new List<BusStation>();
            } else
            {
                line.BusStations.Clear();
            }
            foreach (var station in busStations)
            {
                line.BusStations.Add(station);
            }
        }

        // DELETE: api/BusLine/5
        [ResponseType(typeof(BusLine))]
        public IHttpActionResult DeleteBusLine(int id)
        {
            BusLine busLine = uow.BusLineRepository.Get(id);
            if (busLine == null)
            {
                return NotFound();
            }

            uow.BusLineRepository.Remove(busLine);
            uow.Complete();

            return Ok(busLine);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                uow.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BusLineExists(int id)
        {
            return uow.BusLineRepository.Get(id) != null;
        }
    }
}