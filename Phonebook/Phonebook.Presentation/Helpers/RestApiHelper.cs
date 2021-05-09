using Newtonsoft.Json;
using Phonebook.Presentation.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Presentation.Helpers
{
    public class RestApiHelper
    {
        private readonly string _apiUri = "";
        public RestApiHelper(string apiUri)
        {
            _apiUri = apiUri;
        }

        public T CallService<T>(object requestModel, Method method, string actionPath)
        {
            try
            {
                var client = new RestClient(_apiUri);
                var request = new RestRequest(actionPath, method);
                if (method == Method.POST || method == Method.PUT)
                {
                    if (requestModel != null)
                        request.AddJsonBody(requestModel);
                }

                var response = client.Execute(request); //var response = client.Execute<ApiResponseModel<T>>(request); //CAST EDERKEN BAZI ALANLARI DOLDURMUYOR

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var data = JsonConvert.DeserializeObject<T>(response.Content);
                    return data;
                }
            }
            catch (Exception ex)
            {
            }

            T obj = (T)Activator.CreateInstance(typeof(T));
            return obj;
        }
    }
}
