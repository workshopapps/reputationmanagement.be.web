using Swashbuckle.AspNetCore.Filters;

namespace src.Models.ExampleModels
{
    public class GoodSignInDetailsForCustomer:IMultipleExamplesProvider<UserLoginModel>
    {
            public IEnumerable<SwaggerExample<UserLoginModel>> GetExamples()
            {
                yield return SwaggerExample.Create("Good input that will return Ok + your auth token",
                    new UserLoginModel()
                    {
                        Email = "testcustomer@example.com",
                        Password = "Secret123$"
                    });
            }
        
    }
   
}
