using System.Collections.Generic;
using ExampleApi.Core.Models;
using ExampleApi.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApi.Core.Controllers
{
    [Route("~/api/v1/phones")]
    public class PhonesController : Controller
    {
        private IPhonesService PhonesSvc { get; }

        public PhonesController(IPhonesService phoneSvc)
        {
            PhonesSvc = phoneSvc;
        }

        [HttpGet]
        public IActionResult GetPhones([FromQuery] string manufacturer = null, [FromQuery] string model = null)
        {
            List<Phone> results;
            if (string.IsNullOrWhiteSpace(manufacturer) && string.IsNullOrWhiteSpace(model))
            {
                results = PhonesSvc.GetAllPhones();
                return Json(results);
            }

            // Manufacturer was requested with no model.
            if (!string.IsNullOrWhiteSpace(manufacturer) && string.IsNullOrWhiteSpace(model))
            {
                results = PhonesSvc.GetPhonesByManufacturer(manufacturer);
                return Json(results);
            }

            // Model was request with no manufacturer
            if (!string.IsNullOrWhiteSpace(model) && string.IsNullOrWhiteSpace(manufacturer))
            {
                results = PhonesSvc.GetPhonesByModel(model);
                return Json(results);
            }

            results = PhonesSvc.GetPhonesByManufacturerAndModel(manufacturer, model);
            return Json(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetPhone([FromRoute] long id)
        {
            var phone = PhonesSvc.GetPhoneById(id);
            if (phone == null)
            {
                return NotFound();
            }

            return Json(phone);
        }

        [HttpPost]
        public IActionResult AddPhone([FromBody] Phone phone)
        {
            if (string.IsNullOrWhiteSpace(phone.Manufacturer))
            {
                return BadRequest($"Invalid value for '{nameof(Phone.Manufacturer)}'.");
            }

            if (string.IsNullOrWhiteSpace(phone.Model))
            {
                return BadRequest($"Invalid value for '{nameof(Phone.Model)}'.");
            }

            var addedPhone = PhonesSvc.AddPhone(phone);
            return Json(addedPhone);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePhone([FromRoute] long id)
        {
            // Make sure the phone we're trying to delete actually exists
            var phone = PhonesSvc.GetPhoneById(id);
            if (phone == null)
            {
                return NotFound();
            }

            PhonesSvc.DeletePhone(id);
            return Ok();
        }
    }
}
