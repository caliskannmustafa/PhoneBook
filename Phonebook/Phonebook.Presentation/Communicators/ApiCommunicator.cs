using Phonebook.Presentation.Helpers;
using Phonebook.Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Presentation.Communicators
{
    public class ApiCommunicator
    {
        private readonly string _apiUrl;
        private readonly RestApiHelper _restApiHelper;

        public ApiCommunicator(string apiUrl)
        {
            _apiUrl = apiUrl;
            _restApiHelper = new RestApiHelper(apiUrl);
        }

        public List<PersonViewModel> GetPeopleList()
        {
            return _restApiHelper.CallService<List<PersonViewModel>>(null, RestSharp.Method.GET, "person/Get");
        }

        public PersonViewModel GetPerson(int id)
        {
            return _restApiHelper.CallService<PersonViewModel>(null, RestSharp.Method.GET, "person/Get/" + id);
        }

        public int CreatePerson(PersonViewModel person)
        {
            return _restApiHelper.CallService<int>(person, RestSharp.Method.POST, "person/Create");
        }

        public int UpdatePerson(PersonViewModel person)
        {
            return _restApiHelper.CallService<int>(person, RestSharp.Method.POST, "person/Update");
        }

        public void DeletePerson(int id)
        {
            _restApiHelper.CallService<string>(null, RestSharp.Method.GET, "person/Delete/" + id);
        }

        public int CreateContactInfo(ContactInfoViewModel contact)
        {
            return _restApiHelper.CallService<int>(contact, RestSharp.Method.POST, "contactInfo/Create");
        }

        public void DeleteContactInfo(int id)
        {
            _restApiHelper.CallService<string>(null, RestSharp.Method.GET, "contactInfo/Delete/" + id);
        }

        public List<ReportViewModel> GetReportList()
        {
            return _restApiHelper.CallService<List<ReportViewModel>>(null, RestSharp.Method.GET, "report/Get");
        }

        public ReportDetailViewModel GetReportDetail(int id)
        {
            return _restApiHelper.CallService<ReportDetailViewModel>(null, RestSharp.Method.GET, "report/GetDetail/" + id);
        }

        public int CreateReport(ReportCreateViewModel report)
        {
            return _restApiHelper.CallService<int>(report, RestSharp.Method.POST, "report/Create");
        }

        public string GenerateDummyData(int count)
        {
            return _restApiHelper.CallService<string>(null, RestSharp.Method.GET, "datagenerate/Get/" + count);
        }

    }
}
