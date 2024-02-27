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


namespace VatCalculationApi.Controller.Tests
{
    [TestClass()]
    public class VatCalculatorControllerTests
    {
        [TestMethod()]
        public void CalculateTest_WithValidNetAmount_ShouldReturnCorrectResponse()
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
    }
}