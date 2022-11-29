using src.Models.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace src.Models.ExampleModels
{
    public class CreateReviewForCustomersExample
    {
        public class GoodCreateReviewExample : IMultipleExamplesProvider<ReviewForCreationDto>
        {
            public IEnumerable<SwaggerExample<ReviewForCreationDto>> GetExamples()
            {
                yield return SwaggerExample.Create("Good input that will return Ok + your auth token",
                    new ReviewForCreationDto()
                    {
                        ReviewString = "The bad review",
                        Status = Entities.StatusType.Successful,
                        Priority = Entities.PriorityType.High
                    });
            }
        }

        public class BadCreateReviewExample : IMultipleExamplesProvider<ReviewForCreationDto>
        {
            public IEnumerable<SwaggerExample<ReviewForCreationDto>> GetExamples()
            {
                yield return SwaggerExample.Create("Bad input that will return 400 BadRequest, \"Review not added\"",
                    new ReviewForCreationDto()
                    {
                        ReviewString = null,
                        Status = Entities.StatusType.Successful,
                        Priority = Entities.PriorityType.High
                    });
            }
        }
    }
}
