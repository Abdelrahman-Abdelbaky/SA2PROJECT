using SA2Project;

namespace ApacheKafkaConsumerDemo
{
    public class ApacheKafkaConsumerService : BackgroundService
    {
        private readonly string topic = "test5";
        private readonly string groupId = "test_group";
        private readonly string bootstrapServers = "localhost:9092";

        private readonly IServiceProvider _serviceProvider;
       
        public ApacheKafkaConsumerService(IServiceProvider serviceProvider)
        {
            _serviceProvider=serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

           await Task.Delay(5);
           
            var config = new ConsumerConfig
            {
                GroupId = groupId,
                BootstrapServers = bootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            try
            {
                using (var consumerBuilder = new ConsumerBuilder
                <Ignore, string>(config).Build())
                {
                    consumerBuilder.Subscribe(topic);
                    var cancelToken = new CancellationTokenSource();

                    using var SP = _serviceProvider.CreateScope();
                    var _OferServies = SP.ServiceProvider.GetRequiredService<IOfferService>();
                    var DBO = new dataBaseOperation(_OferServies);

                    try
                    {
                        while (true)
                        {
                            var consumer = consumerBuilder.Consume
                               (cancelToken.Token);
                           
                            var orderRequest = JsonSerializer.Deserialize<OfferViewModel>(consumer.Message.Value);
                           
                            
                         
                            
                            
                            if (orderRequest.Operation.Equals("Post"))
                            {  
                            
                                DBO.AddAsync(orderRequest);
                               
                   
                            }
                            if (orderRequest.Operation.Equals("Put"))
                            {
                                DBO.UpdateAsync(orderRequest.Id, orderRequest);
                               
                            }
                            if (orderRequest.Operation.Equals("Delete"))
                            {


                                DBO.DeleteAsync(orderRequest);
                                
                            }

                        }
                    }
                    catch (OperationCanceledException)
                    {
                        consumerBuilder.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }


        }


        public override Task StartAsync(CancellationToken cancellationToken)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"----------------- START CONNECTION to KAFKA in PORT {bootstrapServers} ------------------- ");
            Console.ResetColor();
                
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("STOP CONNECTION");
            return base.StopAsync(cancellationToken);
        }


    }
}