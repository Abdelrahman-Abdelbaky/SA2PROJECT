using MarketerAPI.DTOS;
using SA2Project.Models;

namespace SA2Project.Services
{
    public interface IOfferService
    {
        Task<Offer> GetByName(string title);
        Task<IEnumerable<Offer>> GetAll();
        Task<Offer> GetById(int id);
        Task<Offer> Add(Offer offer);
        Task<Offer> Update(Offer offer);  
        void Delete(Offer offer);
    }
}
