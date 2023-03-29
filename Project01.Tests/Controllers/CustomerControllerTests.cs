using BusinessLayer.Abstract;
using BusinessLayer.AutoMappers.CustomerViewModels;
using BusinessLayer.AutoMappers.InvoiceViewModels;
using BusinessLayer.Concrete.Validators.CustomerValidators;
using EntityLayer.Concrete;
using FakeItEasy;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Mvc;
using Project01.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project01.Tests.Controllers
{
    public class CustomerControllerTests
    {
        private readonly CustomerController _controller;
        private readonly ICustomerService _customerService;
        public readonly CustomerValidator _validator;

        public CustomerControllerTests()
        {
            _customerService = A.Fake<ICustomerService>();
            _controller = new CustomerController(_customerService);
            _validator = new CustomerValidator();
        }

        [Fact]
        public void Add_ReturnsOkResult()
        {
            // Arrange
            var customerViewModel = new CustomerViewModel
            {
                customerId = 1,
                customerName = "John Doe",
                customerEmail = "johndoe@example.com",
                customerPhone = "1234567890",
                customerIsActive = true,
                createDateTime = DateTime.Now,
                updatedDateTime = null
            };

            A.CallTo(() => _customerService.Add(customerViewModel)).Returns("Customer Başarıyla Eklendi");

            // Act
            var result = _controller.Add(customerViewModel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void GetCustomerLists_ReturnsOkResultWithCustomerList()
        {
            // Arrange
            var expectedCustomerList = new List<CustomerViewModel>
        {
            new CustomerViewModel
            {
                customerId = 1,
                customerName = "John Doe",
                customerEmail = "johndoe@example.com",
                customerPhone = "1234567890",
                customerIsActive = true,
                createDateTime = DateTime.Now,
                updatedDateTime = null
            },
            new CustomerViewModel
            {
                customerId = 2,
                customerName = "Jane Doe",
                customerEmail = "janedoe@example.com",
                customerPhone = "0987654321",
                customerIsActive = true,
                createDateTime = DateTime.Now,
                updatedDateTime = null
            }
        };

            A.CallTo(() => _customerService.GetAll()).Returns(expectedCustomerList);

            var controller = new CustomerController(_customerService);

            // Act
            var result = controller.GetCustomerLists();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var customerList = okResult.Value;
            var data = (List<CustomerViewModel>)customerList.GetType().GetProperty("data").GetValue(customerList);

            Assert.NotEmpty(data);
            Assert.Equal(expectedCustomerList, data);
        }
        [Fact]
        public void GetByID_ReturnsCustomer_WhenCustomerExists()
        {
            // Arrange
            int id = 1;
            var customer = new Customer
            {
                customerId = id,
                customerName = "John Doe",
                customerEmail = "johndoe@example.com",
                customerPhone = "1234567890",
                customerIsActive = true,
                createDateTime = DateTime.Now,
                updatedDateTime = null
            };
            A.CallTo(() => _customerService.GetById(id)).Returns(customer);

            // Act
            var result = _controller.GetByID(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(result);
            var actualCustomer = okResult.Value as Customer;
            Assert.Equal(customer.customerId, actualCustomer.customerId);
            Assert.Equal(customer.customerName, actualCustomer.customerName);
            Assert.Equal(customer.customerEmail, actualCustomer.customerEmail);
            Assert.Equal(customer.customerPhone, actualCustomer.customerPhone);
            Assert.Equal(customer.customerIsActive, actualCustomer.customerIsActive);
            Assert.Equal(customer.createDateTime, actualCustomer.createDateTime);
            Assert.Equal(customer.updatedDateTime, actualCustomer.updatedDateTime);
        }

        [Fact]
        public void GetByID_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange
            int id = 0;
            A.CallTo(() => _customerService.GetById(id)).Returns(null);

            // Act
            var result = _controller.GetByID(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


        [Fact]
        public void Update_ReturnsOkResult_WithUpdatedCustomerAndSuccessMessage()
        {
            // Arrange
            var updatedCustomer = new CustomerViewModel
            {
                customerId = 1,
                customerName = "John Doe",
                customerEmail = "johndoe@example.com",
                customerPhone = "1234567890",
                customerIsActive = true,
                createDateTime = DateTime.Now,
                updatedDateTime = null
            };
            A.CallTo(() => _customerService.Update(updatedCustomer)).Returns("Customer Updated Successfully");
            var expectedResponse = new
            {
                message = "Customer Updated Successfully",
                data = updatedCustomer
            };

            // Act
            var result = _controller.Update(updatedCustomer);
            var okResult = result as OkObjectResult;
            dynamic actualResponse = okResult.Value;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedResponse.message, (string)actualResponse.GetType().GetProperty("message").GetValue(actualResponse, null));
            Assert.Equal(updatedCustomer, actualResponse.GetType().GetProperty("data").GetValue(actualResponse, null));
        }
        [Fact]
        public void Update_ReturnsNotFound_WhenCustomerDoesNotExist()
        {
            // Arrange
            var updatedCustomer = new CustomerViewModel
            {
                customerId = 0,
                customerName = "string",
                customerEmail = "test@test.com",
                customerPhone = "string",
                customerIsActive = true,
                createDateTime = DateTime.Now,
                updatedDateTime = DateTime.Now
            };
            A.CallTo(() => _customerService.Update(updatedCustomer)).Returns("Customer Cannot found");
            var expectedResponse = new
            {
                message = "Customer Cannot found",
                data = updatedCustomer
            };

            // Act
            var result = _controller.Update(updatedCustomer);
            var okResult = result as OkObjectResult;
            var actualResponse = okResult.Value;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedResponse.message, (string)actualResponse.GetType().GetProperty("message").GetValue(actualResponse, null));
            Assert.Equal(updatedCustomer, actualResponse.GetType().GetProperty("data").GetValue(actualResponse, null));
        }

        [Fact]
        public void Add_ReturnsValidationErrorResult()
        {
            var customerViewModel = new CustomerViewModel
            {
                customerId = 0,
                customerName = "",
                customerEmail = "",
                customerPhone = "",
                customerIsActive = true,
                createDateTime = DateTime.Now,
                updatedDateTime = DateTime.Now
            };

            var result = _validator.TestValidate(customerViewModel);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.customerName);
            result.ShouldHaveValidationErrorFor(x => x.customerEmail);
            result.ShouldHaveValidationErrorFor(x => x.customerPhone);

        }
        [Fact]
        public void Add_ReturnsNotHaveValidationError()
        {
            var customerViewModel = new CustomerViewModel
            {
                customerId = 0,
                customerName = "testtr",
                customerEmail = "test@gmail.com",
                customerPhone = "05446662233",
                customerIsActive = true,
                createDateTime = DateTime.Now,
                updatedDateTime = DateTime.Now
            };

            var result = _validator.TestValidate(customerViewModel);

            //Assert
            result.ShouldNotHaveValidationErrorFor(x => x.customerId);
            result.ShouldNotHaveValidationErrorFor(x => x.customerName);
            result.ShouldNotHaveValidationErrorFor(x => x.customerEmail);
            result.ShouldNotHaveValidationErrorFor(x => x.customerPhone);
            result.ShouldNotHaveValidationErrorFor(x => x.customerIsActive);
            result.ShouldNotHaveValidationErrorFor(x => x.createDateTime);
            result.ShouldNotHaveValidationErrorFor(x => x.updatedDateTime);

        }
    }
}

