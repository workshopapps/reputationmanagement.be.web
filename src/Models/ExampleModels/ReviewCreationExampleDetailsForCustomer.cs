using src.Models.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace src.Models.ExampleModels
{
    public class GoodReviewCreationExampleDetailsForCustomer : IMultipleExamplesProvider<ReviewForCreationDto>
    {
        IEnumerable<SwaggerExample<ReviewForCreationDto>> IMultipleExamplesProvider<ReviewForCreationDto>.GetExamples()
        {
            yield return SwaggerExample.Create("Good input will return 200 + ReviewForDisplayDTO",

                new ReviewForCreationDto()
                {
                    Email = "bestogbeide@gmail.com",
                    ReviewString = "This is a very good product",
                    Status = 0,
                    Priority = 0

                });
        }

       
    }
    public class BadReviewCreationExampleDetailsForCustomer : IMultipleExamplesProvider<ReviewForCreationDto>
    {
        IEnumerable<SwaggerExample<ReviewForCreationDto>> IMultipleExamplesProvider<ReviewForCreationDto>.GetExamples()
        {
            yield return SwaggerExample.Create("Bad input that will return 400 BadRequest, \"The Email field is not a valid e-mail address.\"",
               new ReviewForCreationDto()
               {
                   Email = "bestogbeide",
                   ReviewString = "This is a very good product",
                   Status = 0,
                   Priority = 0
               });

        }
    }
}


