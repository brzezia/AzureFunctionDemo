using System;
using System.Collections.Generic;
using AzureFunctionDemo;
using AzureFunctionDemo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Xunit;

namespace XUnitTest
{
    public class UnitTest1
    {
        private readonly ILogger logger = TestFactory.CreateLogger();

        [Fact]
        public async void Http_trigger_should_return_known_string()
        {
            var request = TestFactory.CreateHttpRequest("name", "Bill");

            var converter = new AzureFunctionDemo.Mappers.Converter();
            StatusCodeResult response = (StatusCodeResult)await new CosmosDemoReceiver(converter).Run(request, null, logger);

            Assert.Equal(StatusCodes.Status400BadRequest, response.StatusCode);
        }
    }
}
