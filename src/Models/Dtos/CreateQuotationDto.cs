using System.ComponentModel.DataAnnotations;

namespace src.Models.Dtos
{
    public class CreateQuotationDto
    {
        [Required]
        public Guid ReviewId { get; set; }

        [Required] 
        public string Emailbody { get; set; }
        
        [Required]
        public decimal? Price { get; set; }
    }
}


