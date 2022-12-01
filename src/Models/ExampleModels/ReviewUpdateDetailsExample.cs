using src.Models.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace src.Models.ExampleModels
{
    public class GoodReviewUpdateExampleDetailsForCustomer : IMultipleExamplesProvider<ReviewForUpdateDTO>
    {
        IEnumerable<SwaggerExample<ReviewForUpdateDTO>> IMultipleExamplesProvider<ReviewForUpdateDTO>.GetExamples()
        {
            yield return SwaggerExample.Create("Good input will return 200 + Review is successfully updated",

                new ReviewForUpdateDTO()
                {
                   ReviewId = Guid.NewGuid(),
                   ReviewString = "Bad product",
                   Status =0,
                   Priority =0
                });
        } 
    }

    public class BadReviewUpdateExampleDetailsForCustomer : IMultipleExamplesProvider<ReviewForUpdateDTO>
    {
        IEnumerable<SwaggerExample<ReviewForUpdateDTO>> IMultipleExamplesProvider<ReviewForUpdateDTO>.GetExamples()
        {
            yield return SwaggerExample.Create("Bad input that will return 400 BadRequest, \"The ReviewString field is required\"",
               new ReviewForUpdateDTO()
               {
                   ReviewId = Guid.NewGuid(),
                   ReviewString = "",
                   Status = 0,
                   Priority = 0
               });
        }
    }
}
