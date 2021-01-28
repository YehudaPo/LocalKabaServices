using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace LocalKabaRest.Controllers
{
    [Route("api")]
    public class GetPrvCaseDetailsController : ApiController
    {
        /*
        [HttpGet]
        [Route("kabaApi/GetPrvCaseDetails")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value6", "value2" };
        }
        [HttpGet]
        [Route("kabaApi/GetPrvCaseDetails/{id}")]
        public string Get(int id)
        {
            return "value";
        }
        */
        [HttpGet]
        [Route("api/GetPrvCaseDetails")]///{ExternalInterfaceKey}&{Longitude}&{Latitude}&{Radius}")]
        public IHttpActionResult Get(int ExternalInterfaceKey,
                                    float Longitude,
                                    float Latitude,
                                    int Radius)
        {
            
            Models.NetworkStructs.GetPrvCaseDetailsResponse response =
                new Dals.PrvDals().GetPrvCaseDetails(ExternalInterfaceKey, Longitude, Latitude, Radius);

            return Json(response);
            // return Json(classList);
        }

        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}