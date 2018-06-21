using System.Net;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, IEnumerable<dynamic> documents, TraceWriter log)
{
    log.Info("C# HTTP trigger function processed a request.");

    int totalDocuments = documents.Count();
    log.Info($"Found {totalDocuments} documents");
    if(totalDocuments == 0){
        return req.CreateResponse(HttpStatusCode.NotFound);
    }
    
    return req.CreateResponse(HttpStatusCode.OK, documents); 
}
