using System.Data;
using System.Data.Odbc;
using TestWebApplication.Controllers;
using TestWebApplication.Models;

namespace TestWebApplication.Database
{
    public class CustomerInformationDB
    {
        private readonly ILogger<CustomerInformationDB> _logger;
        private readonly IConfiguration _configuration;

        public CustomerInformationDB(IConfiguration configuration, ILogger<CustomerInformationDB> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public Guid SaveCustomerInformation(string customerFirstName, string customerLastName,
            string policyRef, string customerEmail = "", string customerDateOfBirth = "")
        {
            try
            {
                var dBHelper = new DBHelper(_configuration);
                const string commandText = "Insert into CustomerRegistration(CustomerID, FirstName, LastName, Email, DateOfBirth, PolicyRef) Values(@CustomerID, @CustomerFirstName, @CustomerLastName, @CustomerEmail, @CustomerDateOfBirth, @policyRef)";
                var id = Guid.NewGuid();

                using var connection = dBHelper.GetConnection();
                var command = dBHelper.GetCommand(connection, commandText, CommandType.Text);
                command.Parameters.AddWithValue("CustomerID", id);
                command.Parameters.AddWithValue("CustomerFirstName", customerFirstName);
                command.Parameters.AddWithValue("CustomerLastName",customerLastName);
                command.Parameters.AddWithValue("CustomerEmail", customerEmail);
                command.Parameters.AddWithValue("CustomerDateOfBirth", customerDateOfBirth);
                command.Parameters.AddWithValue("policyRef", policyRef);
           
                command.CommandText = commandText;  
                command.CommandType = CommandType.Text;

                connection.Open();
                command.ExecuteNonQuery();

                return id;

            }
            catch (Exception ex)
            {
                _logger.LogError("SaveCustomerInformation", ex);
                return Guid.Empty;
            }
        }

    }
}
