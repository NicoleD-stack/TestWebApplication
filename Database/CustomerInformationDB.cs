﻿using System.Data;
using System.Text.RegularExpressions;
using TestWebApplication.Controllers;
using TestWebApplication.Utilities;

namespace TestWebApplication.Database
{
    public class CustomerInformationDB
    {
        private readonly ILogger<CustomerInformationController> _logger;
        private readonly IConfiguration _configuration;

        public CustomerInformationDB(IConfiguration configuration, ILogger<CustomerInformationController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public Guid SaveCustomerInformation(string policyRef, string customerFirstName, string customerLastName,
             string customerEmail = "", string customerDateOfBirth = "")
        {
            //validate data first

            var regexValidate = new StringValidators();

            if (!string.IsNullOrEmpty(policyRef))
            {
                if (!regexValidate.ValidatePolicyReferenceNumber(policyRef))
                {
                    return Guid.Empty;
                }
            }

            if (!string.IsNullOrEmpty(customerFirstName))
            {
                if (!regexValidate.ValidateCharacterLimit(customerFirstName))
                {
                    return Guid.Empty;
                }
            }

            if (!string.IsNullOrEmpty(customerLastName))
            {
                if (!regexValidate.ValidateCharacterLimit(customerLastName))
                {
                    return Guid.Empty;
                }
            }


            if (!string.IsNullOrEmpty(customerEmail))
            {
                if (!regexValidate.ValidateEmailAddress(customerEmail))
                {
                    return Guid.Empty;
                }
            }

            if (!string.IsNullOrEmpty(customerDateOfBirth))
            {
                if (!regexValidate.AgeValidation(customerDateOfBirth))
                {
                    return Guid.Empty;
                }
            }

            try
            {
                var dBHelper = new DBHelper(_configuration);
                const string commandText = "Insert into CustomerRegistration(CustomerID, FirstName, LastName, Email, DateOfBirth, PolicyRef) Values(@CustomerID, @CustomerFirstName, @CustomerLastName, @CustomerEmail, @CustomerDateOfBirth, @policyRef)";
                var id = Guid.NewGuid();

                using var connection = dBHelper.GetConnection();
                var command = dBHelper.GetCommand(connection, commandText, CommandType.Text);
                command.Parameters.AddWithValue("CustomerID", id);
                command.Parameters.AddWithValue("CustomerFirstName", customerFirstName);
                command.Parameters.AddWithValue("CustomerLastName", customerLastName);
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
