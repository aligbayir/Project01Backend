﻿using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Add(Customer customer)
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
        public IActionResult Update([FromBody] Customer updatedCustomer)
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
        [Route("GetById/{id}")]
        public Customer GetByID(int id)
        {
            return _customerService.GetById(id);
        }

    }
}