using src.Models;
using src.Models.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace src.Models.ExampleModels
{
    public class BadSignInDetailsForCustomer : IMultipleExamplesProvider<UserLoginModel>
    {
        public IEnumerable<SwaggerExample<UserLoginModel>> GetExamples()
        {
            yield return SwaggerExample.Create("Bad input that will 400 + \"user email does not exist\"",
                new UserLoginModel()
                {
                    Email = "emailaddressthatdoesnotexist@example.com",
                    Password = "Secret123$"
                });

            yield return SwaggerExample.Create("Bad input that will 400 + \"Email and password do not match.\"",
              new UserLoginModel()
              {
                  Email = "goodEmail@example.com",
                  Password = "IncorrectPassword"
              });
        }
    }
   
}
