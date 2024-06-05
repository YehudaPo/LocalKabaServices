using System;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using System.Text;

namespace LocalKabaRest.Controllers
{
    [Route("sms")]
    public class SmsController : ApiController
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
        [Route("sms/SendSms")]///{ExternalInterfaceKey}&{Longitude}&{Latitude}&{Radius}")]
        public async Task<IHttpActionResult> Get(string key, string message,
                                     string recipients, string sender = "KabaApp")
        {

            Models.NetworkStructs.SmsSendResponse sResponse = new Models.NetworkStructs.SmsSendResponse();
            //     new Dals.PrvDals().GetPrvCaseDetails(ExternalInterfaceKey, Longitude, Latitude, Radius);
       

            if (key == General.Constants.SMS_KEY)
            {
      

                string text1 = "<Inforu>" +
                       "<User>" +
                       "<Username>smsapp</Username>" +
                       "<Password>Aa@102102</Password>" +
                       "</User>" +
                       "<Content Type=\"sms\">" +
                       "<Message>" + message + "</Message>" +
                       "</Content>" +
                       "<Recipients>" +
                       "<PhoneNumber>" + recipients + "</PhoneNumber>" +
                       "</Recipients>" +
                       "<Settings>" +
                       "<Sender>" + sender + "</Sender>" +
                       "</Settings>" +
                       "</Inforu>";
               
                HttpClient client = new HttpClient();

                //  http://portal16-service/api/AD?searchparam=EMPLOYEEID&_name=306041914&start=false

                string URL = "https://uapi.inforu.co.il/SendMessageXml.ashx?InforuXML=" + text1;

                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                //client.DefaultRequestHeaders.Accept.Add(
                //new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                //HttpResponseMessage response = client.PostAsync(URL);  // Blocking call! Program will wait here until a response is received or a timeout occurs.

                var httpClient = new HttpClient();
                var someXmlString = text1;
                var stringContent = new StringContent(someXmlString, Encoding.UTF8, "application/xml");
                var response = await httpClient.PostAsync(URL, stringContent);
                
                sResponse.Status = response.StatusCode.ToString(); 
                client.Dispose();
            }
            else {
                sResponse.Status = "Failed";
                sResponse.ErrorMsg = "Bad key";
             
            }


            return Json(sResponse);
            // return Json(classList);
        }

        private async void TestSmsNew2()
        {
            string urlParameters = "";

            string text1 = "<Inforu>" +
                    "<User>" +
                    "<Username>smsapp</Username>" +
                    "<Password>Aa@102102</Password>" +
                    "</User>" +
                    "<Content Type=\"sms\">" +
                    "<Message> This is a test SMS Message </Message>" +
                    "</Content>" +
                    "<Recipients>" +
                    "<PhoneNumber>0547635055</PhoneNumber>" +
                    "</Recipients>" +
                    "<Settings>" +
                    "<Sender>KABANET</Sender>" +
                    "</Settings>" +
                    "</Inforu>";

            HttpClient client = new HttpClient();

            //  http://portal16-service/api/AD?searchparam=EMPLOYEEID&_name=306041914&start=false

            string URL = "https://uapi.inforu.co.il/SendMessageXml.ashx?InforuXML=" + text1;

            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            //client.DefaultRequestHeaders.Accept.Add(
            //new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            //HttpResponseMessage response = client.PostAsync(URL);  // Blocking call! Program will wait here until a response is received or a timeout occurs.

            var httpClient = new HttpClient();
            var someXmlString = text1;
            var stringContent = new StringContent(someXmlString, Encoding.UTF8, "application/xml");
            var response = await httpClient.PostAsync(URL, stringContent);

            client.Dispose();

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