using System.Web.Http;
using ExampleApi.Models;
using ExampleApi.Services;

namespace ExampleApi.Controllers
{
    [RoutePrefix("api/v1")]
    public class PhonesController : ApiController
    {
        private IPhonesService PhonesSvc { get; }

        public PhonesController()
            : this(new PhonesService())
        { }

        public PhonesController(IPhonesService phonesSvc)
        {
            PhonesSvc = phonesSvc;
        }

        [HttpGet]
        [Route("phones")]
        public IHttpActionResult GetPhones()
        {
            var phones = PhonesSvc.GetAllPhones();
            return Ok(phones);
        }

        [HttpGet]
        [Route("phones")]
        public IHttpActionResult GetPhonesWithManufacturer(string manufacturer)
        {
            var matches = PhonesSvc.GetPhonesByManufacturer(manufacturer);
            return Json(matches);
        }

        [HttpGet]
        [Route("phones")]
        public IHttpActionResult GetPhonesWithModel(string model)
        {
            var matches = PhonesSvc.GetPhonesByModel(model);
            return Json(matches);
        }

        [HttpGet]
        [Route("phones/{id}")]
        public IHttpActionResult GetPhone(long id)
        {
            var phone = PhonesSvc.GetPhoneById(id);
            if (phone == null)
            {
                return NotFound();
            }

            return Ok(phone);
        }

        [HttpPost]
        [Route("phones")]
        public IHttpActionResult AddPhone([FromBody] Phone phone)
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
            return Ok(addedPhone);
        }

        [HttpDelete]
        [Route("phones/{id}")]
        public IHttpActionResult DeletePhone(long id)
        {
            // Make sure the phone we're trying to delete actually exists.
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