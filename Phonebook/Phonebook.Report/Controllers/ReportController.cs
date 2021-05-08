using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Phonebook.Report.DAL;
using Phonebook.Report.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Report.Controllers
{
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [Route("report/Get")]
        public IEnumerable<Entity.Report> Get()
        {
            return _unitOfWork.ReportRepository.Get().ToList();
        }

        [HttpGet]
        [Route("report/GetDetail/{id}")]
        public ReportDetail GetDetail(int id)
        {
            return _unitOfWork.ReportDetailRepository.Get(t => t.ReportId == id).FirstOrDefault();
        }

        [HttpPost]
        [Route("report/Create")]
        public int Create(Entity.Report report)
        {
            report.ReportStatus = Entity.Enums.EnumReportStatus.Processing;
            report.CreateDate = DateTime.Now;
            _unitOfWork.ReportRepository.Insert(report);
            _unitOfWork.Save();

            //TODO: EventBusRabbitMq'ya raporla ilgili istek gönderimi yapılacak..

            return report.Id;
        }
    }
}
