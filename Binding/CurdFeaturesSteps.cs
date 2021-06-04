using System;
using System.Text;
using System.Net.Http;


using TechTalk.SpecFlow;
using NUnit.Framework;
using Newtonsoft.Json;
using ProjectSpecflowTesting;
using TechTalk.SpecFlow.Assist;
using System.Collections.Generic;



namespace SpecflowAPI_testing.Binding
{

   
    [Binding]
    public class CurdFeaturesSteps
    {
       
        private readonly string url = "http://akashkotnala-001-site1.etempurl.com/api/employees/";

        

        [Given(@"I set POST employee Details api endpoint")]
        public void GivenISetPOSTEmployeeDetailsApiEndpoint()
        {
            ScenarioContext.Current.Add("Post_URL", url); 
        }



     


        [When(@"Set request Body giving information of employee")]
        public void WhenSetRequestBodyGivingInformationOfEmployee(Table table)
        {
            var details = table.CreateInstance<Employee>();

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                Name = details.Name
            };
            var json = System.Text.Json.JsonSerializer.Serialize(employee);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            ScenarioContext.Current.Add("New_Test_Employee", data);
        }








    

        [When(@"Send a POST HTTP request")]
        public async System.Threading.Tasks.Task WhenSendAPOSTHTTPRequestAsync()
        {
            var client = new HttpClient();

            var response = await client.PostAsync(ScenarioContext.Current["Post_URL"].ToString(), (HttpContent)ScenarioContext.Current["New_Test_Employee"]);

            // var result = (int)response.StatusCode;
            ScenarioContext.Current.Add("post_response", response.StatusCode);
        }




        [Then(@"I receive valid HTTP response code (.*)")]
        public void ThenIReceiveValidHttpResponseCode(int status)
         {
            int post_res_status = (int)ScenarioContext.Current["post_response"];
             Assert.AreEqual(status,post_res_status);
         }






        //For Patch or editting the details of employee api api/employee/{id}

        [Given(@"I set Patch employee Details api endpoint")]
        public void GivenISetPatchEmployeeDetailsApiEndpoint(Table table)
        {
            var details = table.CreateInstance<Employee>();
            ScenarioContext.Current.Add("Patch_URL", url+details.Id);
            ScenarioContext.Current.Add("Patch_data",  details.Name);

        }


        [Given(@"Set request Body of Patch request")]
        public async System.Threading.Tasks.Task GivenSetRequestBodyOfPatchRequestAsync()
        {

            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(ScenarioContext.Current["Patch_URL"].ToString());

            if(response.IsSuccessStatusCode)
            {

                var jasonString = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<Employee>(jasonString);


                result.Name = ScenarioContext.Current["Patch_data"].ToString();



                var json = System.Text.Json.JsonSerializer.Serialize(result);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                ScenarioContext.Current.Add("Data_modified", data);

            }

            else
            {
                throw new System.ArgumentException("Id is not matched with any of the employee");
            }

         
        }
        [Given(@"Send a Patch HTTP request")]
        public async System.Threading.Tasks.Task GivenSendAPatchHTTPRequestAsync()
        {
            var client = new HttpClient();
            var Finalresponse = await client.PatchAsync(ScenarioContext.Current["Patch_URL"].ToString(),(HttpContent)ScenarioContext.Current["Data_modified"]);

            ScenarioContext.Current.Add("Patch_response_status", Finalresponse.StatusCode);

           
        }

        [Then(@"I receive valid HTTP Response code (.*)")]
        public void ThenIReceiveValidHTTPResponseCode(int status)
        {
            Assert.AreEqual(status, (int)ScenarioContext.Current["Patch_response_status"]);
        }





        //For delete api api/employee/{id}

        [Given(@"I set Delete employee Details api endpoint")]
        public void GivenISetDeleteEmployeeDetailsApiEndpoint(Table table)
        {
            var details = table.CreateInstance<Employee>();
            ScenarioContext.Current.Add("Delete_URL", url+details.Id);
           
        }


        [When(@"I send Delete HTTP request")]
        public async System.Threading.Tasks.Task WhenISendDeleteHTTPRequestAsync()
        {

            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(ScenarioContext.Current["Delete_URL"].ToString());


            if(response.IsSuccessStatusCode)
            {
                var res = client.DeleteAsync(ScenarioContext.Current["Delete_URL"].ToString()).Result;

                ScenarioContext.Current.Add("DeleteStatusCode", res.StatusCode);
                
            }
            else
            {
                throw new System.ArgumentException("Id is not matched with any of the employee");
            }
           
        }

        [Then(@"I receive valid Http response Code of (.*)")]
        public void ThenIReceiveValidHttpResponseCodeOf(int status)
        {
            Assert.AreEqual(status, (int)ScenarioContext.Current["DeleteStatusCode"]);
        }





        //For checking the employee/id GET request..

        [Given(@"I set GET Employee api endpoint with id")]
       
        public void GivenISetGETEmployeeApiEndpointWithId()
        {
            ScenarioContext.Current.Add("GET", url);
        }

        [When(@"id is given")]
     
        public void WhenIdIsGiven(Table table)
        {
          
            

            var details = table.CreateInstance<Employee>();

             

           string Get_URL = ScenarioContext.Current["GET"].ToString() + details.Id;

           
            ScenarioContext.Current.Add("GET_URL", Get_URL);


        }

        [When(@"send Get Http request")]
       
        public async System.Threading.Tasks.Task WhenSendGetHttpRequestAsync()
        {
            var client = new HttpClient();
            string get_url = ScenarioContext.Current["GET_URL"].ToString();


            var result = await client.GetAsync(get_url);
            Console.WriteLine($"The Status For request.. :== { result.StatusCode}");
           
            Console.WriteLine("--------------------------------The Details we got for employee----------------------------\n");
            ScenarioContext.Current.Add("Response",result.StatusCode);

            var jasonString = await result.Content.ReadAsStringAsync();

            Console.WriteLine(jasonString);

        }


        [Then(@"we receive valid Http response code (.*)")]
        
        public void ThenWeReceiveValidHttpResponseCode(int status)
        {
           int s = (int)(ScenarioContext.Current["Response"]);

            Assert.AreEqual(status, s);
        }



    }
}
