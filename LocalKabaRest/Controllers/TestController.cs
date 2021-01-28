using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LocalKabaRest.Controllers
{
    [Route("kabaApi2")]
    public class TestController : ApiController
    {
        // GET api/<controller>
      //  [Route("kabaApi/GetPrvCaseDetails", Name = "GetPrvCaseDetailsRoute1")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value6", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

       // [Route("kabaApi/GetPrvCaseDetails", Name = "GetPrvCaseDetailsRoute")]
        public IHttpActionResult Get(int ExternalInterfaceKey,
                                     float Longitude,
                                     float Latitude,
                                     int Radius)
        {
            List<Models.Class1> classList = new List<Models.Class1>();
            Models.Class1 class1 = new Models.Class1();
            class1.field = "value_";// + str + "_num_" + i.ToString();
            class1.num = 3;
            classList.Add(class1); 
            classList.Add(class1); 
            classList.Add(class1);

            
            Models.NetworkStructs.GetPrvCaseDetailsResponse response =
                new Dals.PrvDals().GetPrvCaseDetails(ExternalInterfaceKey, Longitude, Latitude, Radius);

            return Json(response);
           // return Json(classList);
        }

/*
        [HttpGet]
       // [Route("GetCategory1")] //route constraint
        public IHttpActionResult GetCategory()
        {
            var product = 1;
            return Ok(product);
        }
*/
        // POST api/<controller>
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