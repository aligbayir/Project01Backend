using BusinessLayer.Abstract;
using BusinessLayer.AutoMappers.CustomerViewModels;
using BusinessLayer.Concrete.Validators.CustomerValidators;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Project01.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetCustomerLists()
        {
            var customerList = new
            {
                status = true,
                message = "",
                data = _customerService.GetAll()
            };

            return Ok(customerList);
        }
        [HttpPost]
        public IActionResult Add(CustomerViewModel customer)
        {
            var cst = _customerService.Add(customer);
            var response = new
            {
                status = "True",
                message = cst
            };
            return Ok(response);
        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromBody] CustomerViewModel updatedCustomer)
        {
            var upt = _customerService.Update(updatedCustomer);
            var res = new
            {
                message = upt,
                data = updatedCustomer
            };
            return Ok(res);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetByID(int id)
        {
            var customer =_customerService.GetById(id);
            if (customer != null)
            {
                return Ok(customer);
            }
            return NotFound();
        }

    }
}
