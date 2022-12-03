using src.Models.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace src.Models.ExampleModels
{
    public class GoodUserUpdateExampleDetailsForCustomer : IMultipleExamplesProvider<CustomerAccountForCreationDto>
    {

        public IEnumerable<SwaggerExample<CustomerAccountForCreationDto>> GetExamples()
        {
            yield return SwaggerExample.Create("Good input will return 200 + Review is successfully updated",

                new CustomerAccountForCreationDto()
                {
                  Email = "usermail@gmail.com",
                  BusinessEntityName = "userBuisinessName",
                  Password = "UserPassword"
                });
        }
    }

    public class BadUserUpdateExampleDetailsForCustomer : IMultipleExamplesProvider<CustomerAccountForCreationDto>
    {
        public IEnumerable<SwaggerExample<CustomerAccountForCreationDto>> GetExamples()
        {
            yield return SwaggerExample.Create("Bad input will return 400 BadRequest, \"The field input is required\"",

                new CustomerAccountForCreationDto()
                {
                    Email = "usermail@gmail.com",
                    BusinessEntityName = "userBuisinessName",
                    Password = "UserPassword"
                });
        }
    }
}
