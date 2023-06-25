using System.ComponentModel.DataAnnotations;

namespace MarketerAPI.ViewModels
{
    public class OfferDto
    {

        public int Id { get; set; }
        [StringLength(200, ErrorMessage = "Max length is 200 chars")]
        public string Title { get; set; }

        [StringLength(350, ErrorMessage = "Max length is 350 chars")]
        public string Description { get; set; }
        public double Price { get; set; }

        public string Operation { get; set; } 


    } 
}
