﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

            _eventBus.PushMessage("report_create", report.Id.ToString());

            return report.Id;
        }
    }
}
