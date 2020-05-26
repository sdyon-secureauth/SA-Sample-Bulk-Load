using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecureAuth.Sdk;

namespace SA_Sample_Bulk_Load
{
    class Program
    {
        // 4-16-20 - Sean Dyon
        static void Main(string[] args)
        {

            // Setup the configuration object with your
            // SecureAuth API ID, API Key, and URL
            Configuration saApiConfig = new Configuration()
            {
                AppId = "your_app_id",
                AppKey = "your_app_key",
                SecureAuthRealmUrl = "https://your_url.identity.secureauth.com/SecureAuth1/"
            };

            // Instantiate a service object. Passing in the configuration.
            SecureAuthService saService = new SecureAuthService(saApiConfig);

            // Send the Request to get Authentication Options for User
            GetFactorsResponse res = saService.User.GetFactors("someuser");

            Console.WriteLine("---- Get User Auth Options: ----");
            Console.WriteLine(res.RawJson);
            Console.WriteLine("---- Get User Auth Options Sttatus: ----");
            Console.WriteLine(res.Status);

            /*
             * Example Postman creating user
             * {
                  "userId": "test2",
                  "password": "{{pwnew}}",
                  "properties": {
                    "firstName": "Test",
                    "lastName": "User",
                    "phone1": "123-456-7890",
                    "email1": "someone@something.com"
                  }
                }
             */
             
            // Create User Object
            CreateUserRequest req = new CreateUserRequest();
            req.UserId = "someuser";
            req.Password = "something";
            req.Properties.Add("firstName", "Test");
            req.Properties.Add("lastName", "User");
            req.Properties.Add("phone1", "123-456-7890");
            req.Properties.Add("email1", "someone@something.com");

            // Send the Request
            BaseResponse res2 = saService.User.CreateUser(req);

            Console.WriteLine("---- Creating User: ----");
            Console.WriteLine(res2.Status);

            // Validate password for new user
            ValidatePasswordRequest req2 = new ValidatePasswordRequest(req.UserId, req.Password);

            Console.WriteLine("---- Validating Password: ----");
            Console.WriteLine(res2.Status);
            
        }
    }
}
