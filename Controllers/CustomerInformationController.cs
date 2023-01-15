using Microsoft.AspNetCore.Mvc;
using TestWebApplication.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerInformationController : ControllerBase
    {
        private readonly Microsoft.Extensions.Logging.ILogger _logger;
        private readonly IConfiguration _configuration;

        public CustomerInformationController(IConfiguration configuration,ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        // POST api/<CustomerInformationController>
        [HttpPost]
        public Guid Post([FromBody] string policyId, string firstName, string lastName, string emailAddress, string dateOfBirth)
        {
            var customerInformationDB = new CustomerInformationDB(_configuration, _logger);

            var guid = customerInformationDB.SaveCustomerInformation(policyId, firstName, lastName, emailAddress, dateOfBirth);

            return guid;
        }

    }
}
