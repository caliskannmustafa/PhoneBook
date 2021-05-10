using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Phonebook.EventBus;
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
        private readonly IEventBus _eventBus;

        public ReportController(IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
        }

        [HttpGet]
        [Route("report/Get")]
        public IEnumerable<Entity.Report> Get()
        {
            return _unitOfWork.ReportRepository.Get().Take(1000).OrderByDescending(t => t.CreateDate);
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
            report.ReportDate = DateTime.Now;
            _unitOfWork.ReportRepository.Insert(report);
            _unitOfWork.Save();

            var message = new
            {
                ReportId = report.Id.ToString(),
                Latitude = report.Location.Latitude,
                Longitude = report.Location.Longitude
            };

            _eventBus.PushMessage("report_create", JsonConvert.SerializeObject(message));

            return report.Id;
        }
    }
}
