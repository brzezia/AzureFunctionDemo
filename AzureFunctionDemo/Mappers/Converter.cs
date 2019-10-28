using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AzureFunctionDemo.Mappers
{
    public interface IConverter
    {
        Task<T> Convert<T>(HttpRequest req);
    }

    public class Converter : IConverter
    {
        public async Task<T> Convert<T>(HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            dynamic data = JsonConvert.DeserializeObject<T>(requestBody);
            return data;
        }
    }
}
