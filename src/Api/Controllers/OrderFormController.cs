﻿using Application.OrderForms.Commands.CreateOrderForm;
using Application.OrderForms.Queries.GetApproverInformation;
using Application.OrderForms.Queries.GetForms;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    /// <summary>
    /// Form İşlemleri 
    /// </summary>
    [Route("Forms")]
    [ApiController]
    public class FormController : ApiControllerBase
    {
        [SwaggerOperation(
            Summary = "Creates or updates form definition",
            Description = "Form definitons are stored as a form.io schema. All forms stored with tag data. Tags are primary query elements of form"
        )]
        [Route("")]
        [HttpPost]
        [SwaggerResponse(200, "Success, form is updated successfully.", typeof(void))]
        [SwaggerResponse(201, "Success, form is created successfully.", typeof(void))]
        public async Task<IActionResult> CreateOrUpdateFormAsync([FromBody] FormDefinition data)
        {
            return Ok();
        }

        public class FormDefinition
        {
            public string Name { get; set; }
            public string Label { get; set; }
            public string[] Tags { get; set; }
            /// <summary>
            /// If form data is used for rendering a document, render data with dedicated template in template engine.
            /// </summary>
            public string TemplateName { get; set; }
        }

        [SwaggerOperation(
           Summary = "Get forms by tag",
           Description = "Get forms by tag"
       )]
        [Route("")]
        [HttpGet]
        [SwaggerResponse(200, "Success, forms are returned successfully.", typeof(FormDefinition[]))]
        [SwaggerResponse(204, "There is not available any form.", typeof(void))]
        public async Task<IActionResult> GetByTagFormAsync([FromQuery] string[] tags)
        {
            return Ok();
        }
       
        [SwaggerOperation(
           Summary = "Get form by name",
           Description = "Returns form by name"
       )]
        [Route("{name}")]
        [HttpGet]
        [SwaggerResponse(200, "Success, form is returned successfully.", typeof(FormDefinition))]
        [SwaggerResponse(404, "Form not found.", typeof(void))]
        public async Task<IActionResult> GetFormAsync([FromRoute] string  name)
        {
            return Ok();
        }

        /// <summary>
        /// Onaycı Bilgileri Getir
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [SwaggerOperation(
            Summary = "Get approver by name and surname",
            Description = "Returns form by name"
        )]
        [Route("approval-information")]
        [HttpGet]
        [SwaggerResponse(200, "Success, form is returned successfully.", typeof(string))]
        [SwaggerResponse(404, "Approver not found.", typeof(void))]
        public async Task<IActionResult> GetApproverInformationAsync([FromQuery] int type,[FromQuery]string value)
        {
            await Mediator.Send(new GetApproverInformationQuery { Type = type,Value = value});
            return Ok();
        }
        /// <summary>
        /// Form Bilgileri Getir
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [SwaggerOperation(
            Summary = "Get form name",
            Description = "Returns form by name"
        )]
        [Route("form-name")]
        [HttpGet]
        [SwaggerResponse(200, "Success, form name is returned successfully.", typeof(string))]
        [SwaggerResponse(404, "Form Name not found.", typeof(void))]
        public async Task<IActionResult> GetFormAsync()
        {
           var list= await Mediator.Send(new GetFormQuery());
            return Ok(list);
        }
        /// <summary>
        /// Form ile Emir Ekleme
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [SwaggerOperation(
            Summary = "Creates order form definition",
            Description = "Create new  order form. After endorsement is created, process is started immediately."
        )]
        [Route("order-form")]
        [HttpPost]
        [SwaggerResponse(200, "Success, form is updated successfully.", typeof(void))]
        [SwaggerResponse(201, "Success, form is created successfully.", typeof(void))]
        public async Task<IActionResult> CreateNewOrderFormAsync([FromBody] CreateOrderFormCommand data)
        {
            await Mediator.Send(data);
            return Ok();
        }
    }
}
