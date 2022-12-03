using src.Models.Dtos;
using src.Entities;
using Swashbuckle.AspNetCore.Filters;

namespace src.Models.ExampleModels
{
        public class GoodReviewByStatusForCustomer : IMultipleExamplesProvider<StatusType>
        {
            public IEnumerable<SwaggerExample<StatusType>> GetExamples()
            {
            yield return SwaggerExample.Create("Good input that will return Ok + your auth token",
                StatusType.PendingReview);
            }

        }

        public class GoodSingleReviewExample : IMultipleExamplesProvider<Guid>
        {
            public IEnumerable<SwaggerExample<Guid>> GetExamples()
            {
                yield return SwaggerExample.Create("Good input that will return Ok + your auth token",
                    Guid.NewGuid());
            }

        }
}
