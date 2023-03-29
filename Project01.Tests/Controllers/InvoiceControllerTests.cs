using BusinessLayer.Abstract;
using BusinessLayer.AutoMappers.InvoiceViewModels;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Project01.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.TestHelper;
using BusinessLayer.Concrete.Validators.InvoiceValidators;
using FluentAssertions;
using BusinessLayer.AutoMappers.CustomerViewModels;
using EntityLayer.Concrete;

namespace Project01.Tests.Controllers
{
    public class InvoiceControllerTests
    {
        private readonly IInvoiceService _invoiceService;
        private readonly InvoiceController _controller;
        private readonly InvoiceValidator _validator;

        public InvoiceControllerTests()
        {
            _invoiceService = A.Fake<IInvoiceService>();
            _controller = new InvoiceController(_invoiceService);
            _validator = new InvoiceValidator();
        }

        [Fact]
        public void Add_ReturnsOkResult()
        {
            // Arrange
            var invoiceViewModel = new InvoiceViewModel
            {
                InvoiceId = 1,
                InvoiceNumber = "002",
                InvoiceAmount = 140,
                CustomerId = 1
            };
            var expectedResponse = new
            {
                status = true,
                message = "Fatura Başarıyla eklendi"
            };
            A.CallTo(() => _invoiceService.Add(invoiceViewModel)).Returns("Fatura Başarıyla eklendi");

            // Act
            var result = _controller.Add(invoiceViewModel);

            // Assert
            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Add_ReturnsBadResult()
        {
            // Arrange
            var invoiceViewModel = new InvoiceViewModel
            {
                InvoiceId = 1,
                InvoiceNumber = "0085",
                InvoiceAmount = 175
            };
            A.CallTo(() => _invoiceService.Add(invoiceViewModel)).Returns("Fatura Başarıyla eklendi");

            // Act
            var result = _controller.Add(invoiceViewModel);

            // Assert
            var okResult =Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void Add_ReturnsValidationErrorResult()
        {
            var invoiceViewModel = new InvoiceViewModel
            {
                InvoiceId = 1,
                InvoiceNumber = "2", // boş olmamalı,minimum uzunluk 2 karakter olmalı
                                     //InvoiceAmount = , //boş olmamalı ve 0 dan büyük olmalı
                /* CustomerId = 0*/ // boş olmamalı
            };

            var result = _validator.TestValidate(invoiceViewModel);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.InvoiceAmount);
            result.ShouldHaveValidationErrorFor(x => x.CustomerId);
            result.ShouldHaveValidationErrorFor(x => x.InvoiceNumber);

        }
        [Fact]
        public void Add_ReturnsNotHaveValidationError()
        {
            var invoiceViewModel = new InvoiceViewModel
            {
                InvoiceId = 1,
                InvoiceNumber = "002", // boş olmamalı,minimum uzunluk 2 karakter olmalı
                InvoiceAmount = 142, //boş olmamalı ve 0 dan büyük olmalı
                CustomerId = 2 // boş olmamalı
            };

            var result = _validator.TestValidate(invoiceViewModel);

            //Assert
            result.ShouldNotHaveValidationErrorFor(x => x.InvoiceAmount);
            result.ShouldNotHaveValidationErrorFor(x => x.CustomerId);
            result.ShouldNotHaveValidationErrorFor(x => x.InvoiceNumber);
            result.ShouldNotHaveValidationErrorFor(x => x.InvoiceId);

        }

        [Fact]
        public void GetInvoiceLists_ReturnsOkResultWithInvoiceList()
        {
            // Arrange
            var expectedInvoiceList = new List<InvoiceViewModel>
        {
            new InvoiceViewModel
            {
                InvoiceId = 1,
                InvoiceNumber = "002",
                InvoiceAmount = 142,
                CustomerId = 2
            },
            new InvoiceViewModel
            {
                 InvoiceId = 2,
                InvoiceNumber = "003",
                InvoiceAmount = 140,
                CustomerId = 1
            }
        };

            A.CallTo(() => _invoiceService.GetAll(null)).Returns(expectedInvoiceList);

            var controller = new InvoiceController(_invoiceService);
            var result = controller.GetInvoiceLists();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var ınvoiceList = okResult.Value;
            var data = (List<InvoiceViewModel>)ınvoiceList.GetType().GetProperty("data").GetValue(ınvoiceList);

            Assert.NotEmpty(data);
            Assert.Equal(expectedInvoiceList, data);
        }
        [Fact]
        public void GetByID_ReturnsInvoice_WhenInvoiceExists()
        {
            int id = 1;
            // Arrange
            var invoice = new Invoice
            {
                InvoiceId = id,
                InvoiceNumber = "002",
                InvoiceAmount = 142,
                CustomerId = 2
            };
            A.CallTo(() => _invoiceService.GetById(id)).Returns(invoice);

            // Act
            var result = _controller.GetByID(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(result);
            var actualCustomer = okResult.Value as Invoice;
            Assert.Equal(invoice.InvoiceId, actualCustomer.InvoiceId);
            Assert.Equal(invoice.InvoiceNumber, actualCustomer.InvoiceNumber);
            Assert.Equal(invoice.InvoiceAmount, actualCustomer.InvoiceAmount);
            Assert.Equal(invoice.CustomerId, actualCustomer.CustomerId);
        }

        [Fact]
        public void GetByID_ReturnsNotFound_WhenInvoiceDoesNotExist()
        {
            // Arrange
            int id = 0;
            A.CallTo(() => _invoiceService.GetById(id)).Returns(null);

            // Act
            var result = _controller.GetByID(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Update_ReturnsOkResult_WithUpdatedInvoiceAndSuccessMessage()
        {
            int id = 1;
            // Arrange
            var updatedInvoice = new InvoiceViewModel
            {
                InvoiceId = id,
                InvoiceNumber = "002",
                InvoiceAmount = 142,
                CustomerId = 2
            };
            A.CallTo(() => _invoiceService.Update(updatedInvoice)).Returns("Invoice Updated Successfully");
            var expectedResponse = new
            {
                message = "Invoice Updated Successfully",
                data = updatedInvoice
            };

            // Act
            var result = _controller.Update(updatedInvoice);
            var okResult = result as OkObjectResult;
            dynamic actualResponse = okResult.Value;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedResponse.message, (string)actualResponse.GetType().GetProperty("message").GetValue(actualResponse, null));
            Assert.Equal(updatedInvoice, actualResponse.GetType().GetProperty("data").GetValue(actualResponse, null));
        }

        [Fact]
        public void Update_ReturnsBadResult_WithUpdatedInvoice()
        {
            int id = 0;
            // Arrange
            var updatedInvoice = new InvoiceViewModel
            {
                InvoiceId = id,
                InvoiceNumber = "0",
                InvoiceAmount = 0,
                CustomerId = 2
            };
            A.CallTo(() => _invoiceService.Update(updatedInvoice)).Returns("Invoice Updated Successfully");
            var expectedResponse = new
            {
                message = "Invoice Cannot found",
                data = updatedInvoice
            };

            // Act
            var result = _controller.Update(updatedInvoice);
            var okResult = result as OkObjectResult;
            dynamic actualResponse = okResult.Value;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.NotEqual(expectedResponse.message, (string)actualResponse.GetType().GetProperty("message").GetValue(actualResponse, null));
            Assert.Equal(updatedInvoice, actualResponse.GetType().GetProperty("data").GetValue(actualResponse, null));
        }
    }
}
