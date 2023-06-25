using MarketerAPI.DTOS;
using System.Linq;
namespace SA2Project
{
    public class dataBaseOperation
    {


        private readonly IOfferService _offerService;

        public dataBaseOperation(IOfferService offerService)
        {
         _offerService = offerService;
        }
  
        public async Task AddAsync( OfferViewModel model)
        {
    
            var offer = new Offer
            {
                Title = model.Title,
                Description = model.Description,
                Price = model.Price
            };
            
            await _offerService.Add(offer);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine($" ==========> new offer {offer.Title} was added ");
            Console.ResetColor();
        }
       
        public async Task UpdateAsync(int id, OfferViewModel model)
        {
            var offer = new Offer();
            offer.Id = id;
            offer.Title = model.Title;
            offer.Price = model.Price;
            offer.Description = model.Description;
            _offerService.Update(offer);
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"========> offer ID Number {model.Id} Was Updated");
            Console.ResetColor();
        }

        public async Task DeleteAsync(OfferViewModel model)
        {
            var offer = await _offerService.GetById(model.Id);
            
            _offerService.Delete(offer);

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine($"========> offer ID Number {model.Id} was deleted");
            Console.ResetColor();
        }


    }

}
