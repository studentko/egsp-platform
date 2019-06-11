﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Unity;
using WebApp.Persistence.Repository;

namespace WebApp.Persistence.UnitOfWork
{
    public class DemoUnitOfWork : IUnitOfWork, IDemoUnitOfWork
    {
        private readonly DbContext _context;

        private ICustomerRepository customerRepository;
        private ICustomerTypeRepository customerTypeRepository;
        private ITicketRepository ticketRepository;
      
        public DemoUnitOfWork(DbContext context)
        {
            _context = context;
        }

        public ICustomerRepository CustomerRepository
        {
            get
            {
                if (customerRepository == null)
                {
                    customerRepository = new CustomerRepository(_context);
                }
                return customerRepository;
            }
        }

        public ICustomerTypeRepository CustomerTypeRepository
        {
            get
            {
                if (customerTypeRepository == null)
                {
                    customerTypeRepository = new CustomerTypeRepository(_context);
                }
                return customerTypeRepository;
            }
        }

        public ITicketRepository TicketRepository
        {
            get
            {
                if (ticketRepository == null)
                {
                    ticketRepository = new TicketRepository(_context);
                }
                return ticketRepository;
            }
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}