using Phonebook.Report.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Report.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        GenericRepository<Entity.Report> ReportRepository
        {
            get;
        }
        GenericRepository<ReportDetail> ReportDetailRepository
        {
            get;
        }
        void Save();
    }
}
