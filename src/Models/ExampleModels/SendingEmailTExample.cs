using src.Models.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace src.Models.ExampleModels
{
    public class GoodSendingEmailTExample: IMultipleExamplesProvider<EmailDataDto>
    {
        public IEnumerable<SwaggerExample<EmailDataDto>> GetExamples()
        {
            yield return SwaggerExample.Create("Parameters with example used for sending email (Response -> Success)",
                new EmailDataDto()
                {
                    EmailToId = "useremail@gmail.com",
                    EmailBody = "Content of the meassge goes here"
                });
        }
    }

    public class BadSendingEmailTExample : IMultipleExamplesProvider<EmailDataDto>
    {
        public IEnumerable<SwaggerExample<EmailDataDto>> GetExamples()
        {
            yield return SwaggerExample.Create("Parameters with example used for sending email (Response -> Success)",
                new EmailDataDto()
                {
                    EmailToId = "useremail@gmail.com",
                    EmailBody = "Content of the meassge goes here"
                });
        }
    }
}
