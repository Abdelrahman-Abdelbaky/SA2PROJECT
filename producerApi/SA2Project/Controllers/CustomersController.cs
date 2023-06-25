
namespace SA2Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IOfferService offerService;
        private readonly string topic = "test";
        private readonly string groupId = "test_group";
        private readonly string bootstrapServers = "localhost:9092";
        public CustomersController(IOfferService offerService)
        {
            this.offerService = offerService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var offer=await offerService.GetAll();
            if(offer.Count() ==0 )
                return NotFound("Not found any records");
            return Ok(offer);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIDAsync(int id)
        {
            var offer=await offerService.GetById(id);
            if(offer==null)
                return NotFound($"Not found any records with ID= {id}");
            return Ok(offer);
        }

    

      
    }
}
