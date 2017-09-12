using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Controllers;
using Moq;
using System;

namespace UnitTests.WebApi.UnitTests
{
    [TestClass]
    public class ControllerExtensionsUnitTest
    {
        [TestMethod]
        public void HandleExceptionTest()
        {
            string testMsg = "Test exception";

            var mock = new Mock<LineChartController>();
            var exception = new NullReferenceException(testMsg);

            dynamic actualResult = ControllerExtensions.HandleException(mock.Object, exception);

            Assert.AreEqual(testMsg, actualResult.Error);
        }
    }
}
