using Swashbuckle.AspNetCore.Filters;
using src.Models.Dtos;

namespace src.Models.ExampleModels
{
    public class GoodCustomerAccountForCreationExample : IMultipleExamplesProvider<CustomerAccountForCreationDto>
    {
        public IEnumerable<SwaggerExample<CustomerAccountForCreationDto>> GetExamples()
        {
            yield return SwaggerExample.Create("Good input that will return Ok + your auth token",
                new CustomerAccountForCreationDto()
                {
                    BusinessEntityName = "Unique Business Entity Name",
                    Email = "Qwerty@example.com",
                    Password = "Secret123$PleaseNotieTheNonAlphanumerics$%^%$"
                });
        }
    }

    public class BadCustomerAccountForCreationExample : IMultipleExamplesProvider<CustomerAccountForCreationDto>
    {
        public IEnumerable<SwaggerExample<CustomerAccountForCreationDto>> GetExamples()
        {
            yield return SwaggerExample.Create("Bad input that will return 400 BadRequest, \"password validation error\"",
                new CustomerAccountForCreationDto()
                {
                    BusinessEntityName = "QwertyUiopddddd@#$%^",
                    Email = "Qwerty@example.com",
                    Password = "Secret123"
                });

            yield return SwaggerExample.Create("Bad input that will return 400 BadRequest, \"email validation error\"",
               new CustomerAccountForCreationDto()
               {
                   BusinessEntityName = "QwertyUiopddddd@#$%^",
                   Email = "Qwerty@example",
                   Password = "Secret123"
               });

        }
    }
}
