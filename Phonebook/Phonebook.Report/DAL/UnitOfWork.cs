using Microsoft.Extensions.Configuration;
using Phonebook.Report.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Report.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly IConfiguration _configuration;
        private readonly ReportDbContext _context;

        public UnitOfWork(ReportDbContext context)
        {
            _context = context;
        }

        private GenericRepository<Entity.Report> reportRepository;
        private GenericRepository<ReportDetail> reportDetailRepository;

        public GenericRepository<Entity.Report> ReportRepository
        {
            get
            {
                if (this.reportRepository == null)
                {
                    this.reportRepository = new GenericRepository<Entity.Report>(_context);
                }
                return reportRepository;
            }
        }

        public GenericRepository<ReportDetail> ReportDetailRepository
        {
            get
            {

                if (this.reportDetailRepository == null)
                {
                    this.reportDetailRepository = new GenericRepository<ReportDetail>(_context);
                }
                return reportDetailRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
