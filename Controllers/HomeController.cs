using GovAPIConsuption.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Runtime.InteropServices.WindowsRuntime;

namespace GovAPIConsuption.Controllers
{
    public class HomeController : Controller
    {
        HttpClient httpClient;
        static string BASE_URL = "https://developer.nps.gov/api/v1/";
        static string API_KEY = "9QzH6Eaz2ePobL2iN091lq7H0fZr9PRZq9C7heMR";     //Venkata API Key from https://www.nps.gov/subjects/developer/get-started.htm

        public IActionResult Index()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);     // Is X-Api-Key  same for all? or standard?
            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string NATIONAL_PARK_API_URL = BASE_URL + "/parks?;  //?limit=20";   // limit = 20 is to limit accepting 20 records in each call to the api
            string parksData = "";

            Parks parks = null;

            httpClient.BaseAddress = new Uri(NATIONAL_PARK_API_URL);

            try
            {
                HttpResponseMessage response = httpClient.GetAsync(NATIONAL_PARK_API_URL).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    parksData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }

                    if (!parksData.Equals(""))
                    {
                        parks = JsonConvert.DeserializeObject<Parks>(parksData);
                    }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            return View(parks);
//            return View();
            
        }
    }
}