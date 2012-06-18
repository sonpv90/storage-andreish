﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using Moq;
using Mvc.Entities;
using Mvc.Web;
using Mvc.Web.Controllers;
using Mvc.Web.Converters;
using Mvc.Web.Providers;
using Ninject;
using Ninject.Syntax;

namespace Mvc.Web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private static IProviderFactory _providerFactory;
        private static IConverterFactory _converterFactory;
        private static HomeController _homeController;
        private static IKernel _ninjectKernel;

       [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            _ninjectKernel = new StandardKernel(); 
            _providerFactory = new ProviderFactory(_ninjectKernel);
            _converterFactory = new ConverterFactory(_ninjectKernel);
            _homeController = new HomeController(_providerFactory, _converterFactory);
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void Index()
        {
            // Act
            ViewResult result = _homeController.Index() as ViewResult;

            // Assert
            Assert.AreEqual(null, result.ViewBag.Message);
        }

        [TestMethod]
        public void About()
        {
            // Act
            ViewResult result = _homeController.About() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        /// <summary>
        ///A test for Index
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\_Projects\\Mvc.Web\\Mvc.Web", "/")]
        [UrlToTest("http://localhost:53208/")]
        public void IndexTest()
        {
            var mock = new Mock<InputInfo>();
            InputInfo info = mock.SetupAllProperties().Object;
            info.Provider = "YahooProvider";
            info.Company = "epam";
            JsonResult expected = null; 
            JsonResult actual;
            actual = _homeController.Index(info);
            Assert.AreNotEqual(expected, actual.Data);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Weather
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\_Projects\\Mvc.Web\\Mvc.Web", "/")]
        [UrlToTest("http://localhost:53208/")]
        public void WeatherTest()
        {
            IProviderFactory providerFactory = null; // TODO: Initialize to an appropriate value
            IConverterFactory converterFactory = null; // TODO: Initialize to an appropriate value
            HomeController target = new HomeController(providerFactory, converterFactory); // TODO: Initialize to an appropriate value
            string id = string.Empty; // TODO: Initialize to an appropriate value
            ActionResult expected = null; // TODO: Initialize to an appropriate value
            ActionResult actual;
            actual = target.Weather(id);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
