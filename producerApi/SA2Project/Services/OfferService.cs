using MarketerAPI.DTOS;
using Microsoft.EntityFrameworkCore;
using SA2Project.Models;

namespace SA2Project.Services
{
    public class OfferService : IOfferService
    {
        private readonly ApplicationDbContext dbContext;

        public OfferService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Offer> GetByName(string title)
        {
            return await dbContext.Offers.FirstOrDefaultAsync(o=>o.Title == title);
        }

        public async Task<IEnumerable<Offer>> GetAll()
        {
            return await dbContext.Offers.ToListAsync();

        }

        public async Task<Offer> GetById(int id)
        {
            return await dbContext.Offers.FirstOrDefaultAsync(o => o.Id == id);
        }
        public async Task<Offer> Add(Offer offer)
        {
            await dbContext.Offers.AddAsync(offer);
            await dbContext.SaveChangesAsync();
            return offer;
        }
        public async Task<Offer> Update(Offer offer)
        {
            var result = dbContext.Offers.SingleOrDefault(c => c.Id == offer.Id);
            if (result is null)
            {
           
            }
            else { 
             
                result.Title = offer.Title;
                result.Price = offer.Price;
                result.Description = offer.Description;
                await dbContext.SaveChangesAsync();
            }
            return offer;
        }

        public void Delete(Offer offer)
        {
            dbContext.Offers.Remove(offer);
            dbContext.SaveChanges();
        }

        
    }
}
