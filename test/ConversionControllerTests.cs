﻿using api.Controllers;
using infrastructure.Models;
using infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using service;

namespace test
{
    [TestFixture]
    public class ConversionControllerTests
    {
        private ConversionController _controller;

        [SetUp]
        public void Setup()
        {
            var logger = new Microsoft.Extensions.Logging.Abstractions.NullLogger<ConversionController>();
            var converterService = new ConverterService();
            var historyService = new HistoryService(new ConvRepository(null)); 

            _controller = new ConversionController(logger, converterService, historyService);
        }

        [Test]
        public void ConvertCurrency_ShouldReturnOkResult_WhenConversionIsSuccessful()
        {
            // Arrange
            decimal amount = 100;
            string fromCurrency = "USD";
            string toCurrency = "EUR";

            // Act
            var result = _controller.ConvertCurrency(amount, fromCurrency, toCurrency);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void ConvertCurrency_ShouldReturnBadRequest_WhenInvalidCurrencyIsProvided()
        {
            // Arrange
            decimal amount = 100;
            string fromCurrency = "INVALID";
            string toCurrency = "EUR";

            // Act
            var result = _controller.ConvertCurrency(amount, fromCurrency, toCurrency);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

      


        [Test]
        public void ConvertCurrency_ShouldReturnBadRequest_WhenSourceCurrencyIsNull()
        {
            // Arrange
            decimal amount = 100;
            string fromCurrency = null;
            string toCurrency = "EUR";

            // Act
            var result = _controller.ConvertCurrency(amount, fromCurrency, toCurrency);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [Test]
        public void ConvertCurrency_ShouldReturnBadRequest_WhenTargetCurrencyIsNull()
        {
            // Arrange
            decimal amount = 100;
            string fromCurrency = "USD";
            string toCurrency = null;

            // Act
            var result = _controller.ConvertCurrency(amount, fromCurrency, toCurrency);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }


        [TearDown]
        public void TearDown()
        {
            //_controller.Dispose();
        }
    }
}
