using RestSharp;
using System.Net;
using System.Text.Json;

namespace APIAutomationSample1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test, Order(0)]
        public void GetRequestForSpecificRecord()
        {
            string url = "https://jsonplaceholder.typicode.com/users";

            // Step 1: Create a REST client connection
            var client = new RestClient(url);

            // Step 2: Create a GET Request
            var request = new RestRequest();

            // Step 3: Add Parameters
            request.AddParameter("id", "1");

            // Step 4: Get Response
            var response = client.Get(request);
            
            // Step 5: Read the Status-Code from Response
            var statusCode = response.StatusCode;

            Console.WriteLine($"Status Code = {statusCode}");        

            // Print Response to Console
            Console.WriteLine(response.Content);

            // Assert the response is right or not
            Assert.That(statusCode, Is.EqualTo(HttpStatusCode.OK), "Error: Status Code is Wrong. There is some error");
            
        }

        [Test, Order(1)]        
        public void PostRequestMethod1()
        {
            var client = new RestClient("https://jsonplaceholder.typicode.com/users");
            var request = new RestRequest();
            string payload = "{\"id\":11," + "\"name\":\"Jennah\" }";

            request.AddJsonBody(payload);

            var response = client.Post(request);

            var statusCode = response.StatusCode;

            Console.WriteLine($"Status Code= {statusCode}");
            Console.WriteLine(response.Content);

            Assert.That(statusCode, Is.EqualTo(HttpStatusCode.Created), "Error: There is some issue with Status-code");

        }

        [Test, Order(2)]
        public void GetRequestForAllRecords()
        {
            string url = "https://jsonplaceholder.typicode.com/users";
                      
            var client = new RestClient(url);
            var request = new RestRequest();
            var response = client.Get(request);            
            var statusCode = response.StatusCode;
            Console.WriteLine($"Status Code = {statusCode}");

           
            Console.WriteLine(response.Content);           
            Assert.That(statusCode, Is.EqualTo(HttpStatusCode.OK), "Error: Status Code is Wrong. There is some error");

        }

        [Test , Order(4)]
        public void PostRequestMethod2()
        {
            string url = "https://jsonplaceholder.typicode.com/posts";
            var client = new RestClient(url);
            var request = new RestRequest();

            // userId: title : body:
            var payload = new PostModel { UserId = 1 , Title = "Test Three", Body= "Testing the Post Request-3 using RestSharp"};
            
            request.AddJsonBody(payload);

            var response = client.Post(request);
            //var responce = client.ExecutePost(request);
            //var postModel = JsonSerializer.Deserialize<PostModel>(responce.Content!);
            //Console.WriteLine($"jSon Properties: {postModel.UserId}:{postModel.Title}");


            if (response.IsSuccessful)
            {
                Console.WriteLine($"Status Code = {response.StatusCode}");
                Console.WriteLine($"Content: {response.Content}");

                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created), "Error: Status Code is wrong. Post failed");
            }
            else
                Console.WriteLine($"Error: {response.ErrorMessage}");
        }



    }
}