using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using funcApp.model;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;

namespace funcApp
{
    
    public static class Function1
    {
        const string conn = "Server=tcp:funcapi.database.windows.net,1433;Initial Catalog=funcapidatabase;Persist Security Info=False;User ID=luigi;Password=Senha1234!@#$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        [FunctionName("funcGet")]
        public static async Task<HttpResponseMessage> RunGet([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route =  "Example/{parameter}")]HttpRequestMessage req,string parameter, TraceWriter log)
        {
            log.Info($"C# HTTP trigger function processed a request {parameter}");

            using (var db = new contextPost(conn))
            {
                var post = db.PostData.Where(o => o.name == parameter);

                return post == null
                    ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
                    : req.CreateResponse(HttpStatusCode.OK, post.SingleOrDefault());
            }




            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }

        [FunctionName("funcPost")]
        public static async Task<HttpResponseMessage> RunPost([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Example/")]HttpRequestMessage req, TraceWriter log)
        {
//            PostData data = await req.Content.ReadAsAsync<PostData>();

            var content = req.Content;
            string JsonContent = content.ReadAsStringAsync().Result;



            log.Info($"C# HTTP trigger function processed a request ");

            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }

        [FunctionName("funcPut")]
        public static async Task<HttpResponseMessage> RunPut([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "Example/")]HttpRequestMessage req, TraceWriter log)
        {
            

            var content = req.Content;
            

            string JsonContent = content.ReadAsStringAsync().Result;
            
            PostData data = JsonConvert.DeserializeObject<PostData>(JsonContent);

            try
            {
                using (var db = new contextPost(conn))
                {
                    db.PostData.Add(data);
                    db.SaveChanges();
                }
            }
            catch (System.Exception ex)
            {

                throw;
            }

                log.Info($"C# HTTP trigger function processed a request ");

            return new HttpResponseMessage(HttpStatusCode.Accepted);
        }

    }
}
