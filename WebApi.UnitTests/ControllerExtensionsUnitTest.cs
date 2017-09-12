using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Controllers;
using Moq;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;

namespace UnitTests.WebApi.UnitTests
{
    [TestClass]
    public class ControllerExtensionsUnitTest
    {
        private readonly Mock<LineChartController> _lineChartControllerMock = new Mock<LineChartController>();

        [TestMethod]
        public void HandleExceptionTest()
        {
            string testMsg = "Test exception";
            var exception = new NullReferenceException(testMsg);
            dynamic actualResult = ControllerExtensions.HandleException(_lineChartControllerMock.Object, exception);

            Assert.AreEqual(testMsg, actualResult.Error);
        }

        [TestMethod]
        public void ConvertHeadersToDictionaryTest()
        {
            Dictionary<string, StringValues> expectedDict = new Dictionary<string, StringValues>()
            {
                { "X-JPATH-FOR-X", new StringValues("$..SomePath") },
                { "X-JPATH-FOR-Y", new StringValues("$..SomePath2") },
                { "Host", new StringValues(new string[] { "Some.host.com", "SomeOther.host.com" }) },
                { "User-Agent", new StringValues("Mozilla/5.0 (X11; Linux x86_64; rv:12.0) Gecko/20100101 Firefox/12.0") },
                { "Content-Type", new StringValues("application/x-www-form-urlencoded") }
            };

            var headersMock = new Mock<IHeaderDictionary>(MockBehavior.Loose);

            headersMock.Setup(param => param.Count).Returns(() => 5);
            headersMock.Setup(param => param.GetEnumerator()).Returns(() => expectedDict.GetEnumerator());
            headersMock.Setup(param => param.Keys).Returns(() => new List<string>()
            {
                "X-JPATH-FOR-X",
                "X-JPATH-FOR-Y",
                "Host", 
                "User-Agent", 
                "Content-Type"
            });

            headersMock.Setup(param => param.Values).Returns(() => new List<StringValues>()
            {
                new StringValues("$..SomePath"),
                new StringValues("$..SomePath2"),
                new StringValues(new string[] { "Some.host.com", "SomeOther.host.com" }),
                new StringValues("Mozilla/5.0 (X11; Linux x86_64; rv:12.0) Gecko/20100101 Firefox/12.0"),
                new StringValues("application/x-www-form-urlencoded")
            });
            
            Dictionary<string, StringValues> actualDict = ControllerExtensions.ConvertHeadersToDictionary(_lineChartControllerMock.Object, headersMock.Object);

            Assert.IsTrue(new DictionaryComparer<string, StringValues>(new StringValuesComparer()).Equals(expectedDict, actualDict));
        }
    }
}
