using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AzureFunctionDemo.Mappers
{

    public enum RequestDataSource
    {
        Body = 0,
        Query = 1
    }
    public interface IConverter
    {
        Task<T> ReadFromBody<T>(HttpRequest req);
        string ReadFromQuery(HttpRequest req, string name);
    }

    public class Converter : IConverter
    {
        public async Task<T> ReadFromBody<T>(HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            dynamic data = JsonConvert.DeserializeObject<T>(requestBody);
            return data;
        }
        public string ReadFromQuery(HttpRequest req, string name)
        {
            var queries = req.Query.TryGetValue(name, out var value);
            return value;
        }
    }
}
