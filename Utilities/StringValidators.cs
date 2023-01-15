using System.Text.RegularExpressions;

namespace TestWebApplication.Utilities
{
    public class StringValidators
    {
        public bool ValidateCharacterLimit(string str)
        {
            // checks string is between 3 and 50 characters
            Regex rx = new(@"^.*[a - z]{ 3, 50 }$");
            return rx.IsMatch(str);

        }

        public bool ValidatePolicyReferenceNumber(string str)
        {
            // checks policy reference number is in the format XX-999999
            Regex rx = new(@"(?-i)^[A-Z]{2}[._\-:]\d{6}$");
            return rx.IsMatch(str);
        }

        public bool AgeValidation(string str)
        {
            // checks date of birth supplied to see if customer is at least 18
            var isValid = true;
            DateTime? birthDate = DateTime.Parse(str);

            if (birthDate < DateTime.Now.AddYears(-18))
            {
                isValid= false;
                return isValid;
            }
            else
            {
                return isValid;
            }
        }

        public bool ValidateEmailAddress(string str)
        {
            // checks email address contains at least 4 alpha numeric chars followed by @ then 2 alpha numeric chars
            // email address should end in either com or co.uk
            
            Regex rx = new(@"^[a-z0-9]{4}\b[@][A-Z0-9]{2}$");

            return rx.IsMatch(str) && (str.EndsWith(".com") || str.EndsWith(".co.uk"));
           
        }  
    }
}
