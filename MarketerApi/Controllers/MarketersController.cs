
using MarketerAPI.DTOS;

namespace MarketerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class MarketersController : ControllerBase
    {

        private readonly string
        bootstrapServers = "localhost:9092";
        private readonly string topic = "test5";

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostDto orderRequest)
        {
            var offer = new OfferDto();
            offer.Title = orderRequest.Title;
            offer.Description = orderRequest.Description;   
            offer.Price = orderRequest.Price;
            offer.Operation = orderRequest.Operation;
            string message = JsonSerializer.Serialize(offer);
            return Ok(await SendOrderRequest(topic, message));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] offerUpdateDto orderRequest)
        {
            var offer = new OfferDto();
            offer.Id = id;
            offer.Title = orderRequest.Title;
            offer.Price = orderRequest.Price;
            offer.Description = orderRequest.Description;
            offer.Operation = orderRequest.Operation;
            string message = JsonSerializer.Serialize(offer);
            return Ok(await SendOrderRequest(topic, message));
         }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var offer = new OfferDto();
            offer.Id= id;
            offer.Operation= "Delete";
            string message = JsonSerializer.Serialize(offer);
            return Ok(await SendOrderRequest(topic, message));
        }


        //send message to kafka
        private async Task<bool> SendOrderRequest
      (string topic, string message)
        {
            ProducerConfig config = new ProducerConfig
            {
                BootstrapServers = bootstrapServers,
                ClientId = Dns.GetHostName()
            };

            try
            {
                using (var producer = new ProducerBuilder
                <Null, string>(config).Build())
                {
                    var result = await producer.ProduceAsync
                    (topic, new Message<Null, string>
                    {
                        Value = message
                    });

                   // Debug.WriteLine($"Delivery Timestamp:{ result.Timestamp.UtcDateTime}");
                return await Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
            }

            return await Task.FromResult(false);
        }
    }

}
      
    
