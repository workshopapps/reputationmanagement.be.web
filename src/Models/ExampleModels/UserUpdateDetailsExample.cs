using src.Models.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace src.Models.ExampleModels
{
    public class GoodUserUpdateExampleDetailsForCustomer : IMultipleExamplesProvider<CustomerUpdateDto>
    {

        public IEnumerable<SwaggerExample<CustomerUpdateDto>> GetExamples()
        {
            yield return SwaggerExample.Create("Good input will return 200 + Review is successfully updated",

                new CustomerUpdateDto()
                {
                  Email = "usermail@gmail.com",
                  BusinessEntityName ="A Unigue business name",
                  PhoneNumber =  "08055667788",
                  FullName = "Customer FullName",
                  BusinessWebsite = "The business website",
                  BusinessDescription = "a Breif description of what the business does"
                });
        }
    }

    public class BadUserUpdateExampleDetailsForCustomer : IMultipleExamplesProvider<CustomerUpdateDto>
    {
        public IEnumerable<SwaggerExample<CustomerUpdateDto>> GetExamples()
        {
            yield return SwaggerExample.Create("Bad input will return 400 BadRequest, \"The field input is required\"",

                new CustomerUpdateDto()
                {
                    Email = "usermail@gmail",
                    BusinessEntityName = "oopojm,nmnbnvbv",
                    PhoneNumber = "",
                    FullName = "",
                    BusinessWebsite = "The business website",
                    BusinessDescription = "a Breif description of what the business does"
                });
        }
    }
}
