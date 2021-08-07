using Newtonsoft.Json;
using SEE_Test_SAVICS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SEE_Test_SAVICS.Controllers
{

    public class RecordController : ApiController
    {
        [HttpGet]
        [Route("api/1.0/emr")]
        public async Task<HttpResponseMessage> Get()
        {
            try
            {
                List<Record> Records = null;
                await Task.Run(() =>
                {
                    Records = JsonConvert.DeserializeObject<List<Record>>(File.ReadAllText(
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "DB.json"), Encoding.UTF8));
                });
                return Request.CreateResponse(HttpStatusCode.OK, Records);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(e.Message)
                });
            }
        }

        [HttpGet]
        [Route("api/1.0/emr")]
        public async Task<HttpResponseMessage> Find([FromUri] string q, [FromUri] int? minors)
        {
            try
            {
                List<Record> Records = null;
                await Task.Run(() =>
                {
                    Records = JsonConvert.DeserializeObject<List<Record>>(File.ReadAllText(
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "DB.json"), Encoding.UTF8));
                    if (!String.IsNullOrWhiteSpace(q))
                        Records = Records.Where(p => p.LastName.Contains(q) || p.FirstName.Contains(q) || (p.LastName + " " + p.FirstName).Contains(q)).ToList();
                    if (minors.HasValue && minors.Value == 1)
                        Records = Records.Where(p => p.Age < 18).ToList();
                });
                return Request.CreateResponse(HttpStatusCode.OK, Records);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(e.Message)
                });
            }
        }

        [HttpPost]
        [Route("api/1.0/emr")]
        public async Task<HttpResponseMessage> Post(Record input)
        {
            try
            {
                await Task.Run(() =>
                {
                    List<Record> Records = JsonConvert.DeserializeObject<List<Record>>(File.ReadAllText(
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "DB.json"), Encoding.UTF8));
                    Records.Add(input);
                    string JSON = JsonConvert.SerializeObject(Records.ToArray());
                    File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "DB.json"), JSON);
                });
                return Request.CreateResponse(HttpStatusCode.OK, input);
            }
            catch (Exception e)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(e.Message)
                });
            }
        }

    }
}
