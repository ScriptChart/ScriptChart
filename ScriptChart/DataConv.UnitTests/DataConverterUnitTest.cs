using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace DataConv.UnitTests
{
    [TestClass]
    public class DataConverterUnitTest
    {
        [TestMethod]
        public void ConvertDataForXTest()
        {
            IDataConverter dataConverter = new DataConverter();
            var actualResult = dataConverter.ConvertData(JToken.Parse(JsonSample), "$..DownloadSpeedInBits");

            List<float> expectedList = new List<float>()
            {
                167772160,
                209715200,
                152520145
            };

            for (int i = 0; i < expectedList.Count; i++)
            {
                if (Math.Abs(expectedList[i] - actualResult[i]) > 0.000001)
                {
                    Assert.Fail($"Expected float value <{expectedList[i]}> does not match the actual <{actualResult[i]}>.");
                }
            }
        }


        [TestMethod]
        public void ConvertDataForXyTest()
        {
            IDataConverter dataConverter = new DataConverter();
            var actualResult = dataConverter.ConvertData(JToken.Parse(JsonSampleForXy), "$..Id", "$..DownloadSpeedInBits");

            List<Tuple<float, float>> expectedList = new List<Tuple<float, float>>
            {
                new Tuple<float, float>(10, 167772160),
                new Tuple<float, float>(20, 167771160),
                new Tuple<float, float>(30, 167773160)
            };

            for (int i = 0; i < expectedList.Count; i++)
            {
                if (Math.Abs(expectedList[i].Item1 - actualResult[i].Item1) > 0.000001 
                    || Math.Abs(expectedList[i].Item2 - actualResult[i].Item2) > 0.000001)
                {
                    Assert.Fail($"Expected float value <{expectedList[i]}> does not match the actual <{actualResult[i]}>.");
                }
            }
        }

        private const string JsonSampleForXy = @"[
    {
        ""Url"":  ""http://stor02haufe.blob.core.windows.net/2017/product-a/zips/200MB"",
        ""ElapsedTime"":  ""00:00:11.0862581"",
        ""Id"":  10,
        ""DownloadSpeedInBits"":  167772160,
    },
    {
        ""Url"":  ""http://stor02haufe.blob.core.windows.net/2017/product-a/zips/200MB"",
        ""ElapsedTime"":  ""00:00:12.0862581"",
        ""Id"":  20,
        ""DownloadSpeedInBits"":  167771160,
    },
    {
        ""Url"":  ""http://stor02haufe.blob.core.windows.net/2017/product-a/zips/200MB"",
        ""ElapsedTime"":  ""00:00:13.0862581"",
        ""Id"":  30,
        ""DownloadSpeedInBits"":  167773160,
    },
]";

        private const string JsonSample = @"[
    {
        ""Url"":  ""http://stor02haufe.blob.core.windows.net/2017/product-a/zips/200MB"",
        ""ElapsedTime"":  ""00:00:10.0862581"",
        ""SizeInBytes"":  209715200,
        ""DownloadSpeedInBits"":  167772160,
        ""LogEntries"":  [
                           ""@{Timestampt=2017-08-29T16:20:37.77414+00:00; Entry=Env var AZURE_FDS_URL_TO_FILE = https://redirect-test-haufe.azurewebsites.net/2017/product-a/zips/200MB}"",
                           ""@{Timestampt=2017-08-29T16:20:37.774142+00:00; Entry=Application started}"",
                           ""@{Timestampt=2017-08-29T16:20:38.777908+00:00; Entry=http://stor02haufe.blob.core.windows.net/2017/product-a/zips/200MB}"",
                           ""@{Timestampt=2017-08-29T16:20:38.937823+00:00; Entry=}"",
                           ""@{Timestampt=2017-08-29T16:20:38.937827+00:00; Entry=Starting download...}"",
                           ""@{Timestampt=2017-08-29T16:20:47.860328+00:00; Entry=Response status: OK}"",
                           ""@{Timestampt=2017-08-29T16:20:47.860415+00:00; Entry=It took 10086 ms to download file.}"",
                           ""@{Timestampt=2017-08-29T16:20:47.860416+00:00; Entry=Application exiting...}""
                       ],
        ""EnvProps"":  {
                         ""ProcessId"":  1,
                         ""CurrentManagedThreadId"":  1,
                         ""MachineName"":  ""ip-10-18-70-143""
                     },
        ""PSComputerName"":  ""localhost"",
        ""RunspaceId"":  ""e8ed75b2-49f1-448d-afa0-113d198dcbbe"",
        ""PSShowComputerName"":  false
    },
    {
        ""Url"":  ""http://stor01haufe.blob.core.windows.net/2017/product-a/zips/200MB"",
        ""ElapsedTime"":  ""00:00:08.9292412"",
        ""SizeInBytes"":  209715200,
        ""DownloadSpeedInBits"":  209715200,
        ""LogEntries"":  [
                           ""@{Timestampt=2017-08-29T16:21:14.073916+00:00; Entry=Env var AZURE_FDS_URL_TO_FILE = https://redirect-test-haufe.azurewebsites.net/2017/product-a/zips/200MB}"",
                           ""@{Timestampt=2017-08-29T16:21:14.073918+00:00; Entry=Application started}"",
                           ""@{Timestampt=2017-08-29T16:21:14.173214+00:00; Entry=http://stor01haufe.blob.core.windows.net/2017/product-a/zips/200MB}"",
                           ""@{Timestampt=2017-08-29T16:21:14.340531+00:00; Entry=}"",
                           ""@{Timestampt=2017-08-29T16:21:14.340537+00:00; Entry=Starting download...}"",
                           ""@{Timestampt=2017-08-29T16:21:23.003118+00:00; Entry=Response status: OK}"",
                           ""@{Timestampt=2017-08-29T16:21:23.003176+00:00; Entry=It took 8929 ms to download file.}"",
                           ""@{Timestampt=2017-08-29T16:21:23.003176+00:00; Entry=Application exiting...}""
                       ],
        ""EnvProps"":  {
                         ""ProcessId"":  1,
                         ""CurrentManagedThreadId"":  1,
                         ""MachineName"":  ""ip-10-18-70-143""
                     },
        ""PSComputerName"":  ""localhost"",
        ""RunspaceId"":  ""169cd65a-3cc8-492e-87df-5047ed3f41a2"",
        ""PSShowComputerName"":  false
    },
    {
        ""Url"":  ""http://stor01haufe.blob.core.windows.net/2017/product-a/zips/200MB"",
        ""ElapsedTime"":  ""00:00:11.1561790"",
        ""SizeInBytes"":  209715200,
        ""DownloadSpeedInBits"":  152520145,
        ""LogEntries"":  [
                           ""@{Timestampt=2017-08-29T16:23:51.141401+00:00; Entry=Env var AZURE_FDS_URL_TO_FILE = https://redirect-test-haufe.azurewebsites.net/2017/product-a/zips/200MB}"",
                           ""@{Timestampt=2017-08-29T16:23:51.141404+00:00; Entry=Application started}"",
                           ""@{Timestampt=2017-08-29T16:23:51.452223+00:00; Entry=http://stor01haufe.blob.core.windows.net/2017/product-a/zips/200MB}"",
                           ""@{Timestampt=2017-08-29T16:23:51.70876+00:00; Entry=}"",
                           ""@{Timestampt=2017-08-29T16:23:51.708765+00:00; Entry=Starting download...}"",
                           ""@{Timestampt=2017-08-29T16:24:02.297526+00:00; Entry=Response status: OK}"",
                           ""@{Timestampt=2017-08-29T16:24:02.297597+00:00; Entry=It took 11156 ms to download file.}"",
                           ""@{Timestampt=2017-08-29T16:24:02.297598+00:00; Entry=Application exiting...}""
                       ],
        ""EnvProps"":  {
                         ""ProcessId"":  1,
                         ""CurrentManagedThreadId"":  1,
                         ""MachineName"":  ""ip-10-18-70-143""
                     },
        ""PSComputerName"":  ""localhost"",
        ""RunspaceId"":  ""2fa3a4f0-1843-4a2d-a98c-9c47532387ae"",
        ""PSShowComputerName"":  false
    }
]";
    }
}
