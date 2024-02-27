using Microsoft.VisualStudio.TestTools.UnitTesting;
using VatCalculationApi.Controller;
using VatCalculationApi.Service;
using VatCalculationApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace VatCalculationApi.Controller.Tests
{
    [TestClass()]
    public class VatCalculatorControllerTests
    {

        [TestMethod()]
        public void CalculateWithNet()
        {
            // Arrange
            var controller = new VatCalculatorController(new VatCalculatorService());
            var request = new VatCalculationRequest
            {
                Net = 100,
                VatRate = 20
            };

            // Act
            var result = controller.Calculate(request);

            // Assert
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var response = okResult.Value as VatCalculationResponse;
            Assert.IsNotNull(response);

            // Assuming a 20% VAT rate, expect Gross = Net * 1.2
            Assert.AreEqual(120, response.Gross);
            Assert.AreEqual(20, response.VatAmount); // And VAT amount = Net * 0.2
        }
        [TestMethod()]
        public void CalculateWithGross()
        {
            // Arrange
            var controller = new VatCalculatorController(new VatCalculatorService());
            var request = new VatCalculationRequest
            {
                Gross = 240,
                VatRate = 20
            };

            // Act
            var result = controller.Calculate(request);

            // Assert
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var response = okResult.Value as VatCalculationResponse;
            Assert.IsNotNull(response);

            // Assuming a 20% VAT rate, expect Gross = Net * 1.2
            Assert.AreEqual(200, response.Net);
            Assert.AreEqual(40, response.VatAmount); // And VAT amount = Net * 0.2
        }
        [TestMethod()]
        public void CalculateWithVatAmount()
        {
            // Arrange
            var controller = new VatCalculatorController(new VatCalculatorService());
            var request = new VatCalculationRequest
            {
                VatAmount = 20,
                VatRate = 20
            };

            // Act
            var result = controller.Calculate(request);

            // Assert
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var response = okResult.Value as VatCalculationResponse;
            Assert.IsNotNull(response);

            // Assuming a 20% VAT rate, expect Gross = Net * 1.2
            Assert.AreEqual(120, response.Gross);
            Assert.AreEqual(100, response.Net); // And VAT amount = Net * 0.2
        }
        [TestMethod]
        public void Calculate_ShouldReturnBadRequest_WhenMoreThanOneInputProvided()
        {
            var controller = new VatCalculatorController(new VatCalculatorService());
            // Arrange
            var request = new VatCalculationRequest
            {
                Net = 100,
                Gross = 120,
                VatRate = 20
            };

            // Act
            var result = controller.Calculate(request);

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.IsTrue(badRequestResult.Value.ToString().Contains("Please provide exactly one value"));
        }
        [TestMethod]
        public void Calculate_ShouldReturnBadRequest_WhenInvalidAmountInput()
        {
            var controller = new VatCalculatorController(new VatCalculatorService());
            // Arrange
            var requestWithNegativeNet = new VatCalculationRequest
            {
                Net = -100,
                VatRate = 20
            };

            // Act
            var resultWithNegativeNet = controller.Calculate(requestWithNegativeNet);

            // Assert
            Assert.IsInstanceOfType(resultWithNegativeNet, typeof(BadRequestObjectResult));
            var badRequestResult = resultWithNegativeNet as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.IsTrue(badRequestResult.Value.ToString().Contains("Amount input must be a positive number"));
        }

    }
}