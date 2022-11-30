using ClientGenerated;
using Newtonsoft.Json;

HttpClient httpClient = new HttpClient();
var apiClient = new swaggerClient("https://localhost:7226/", httpClient);
var result = await apiClient.GetdatabasesAsync();
foreach(var db in result)
{
    string dss = JsonConvert.SerializeObject(db, Formatting.Indented);
    Console.WriteLine(dss);
}
