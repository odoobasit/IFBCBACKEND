using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IListingService
    {
        Task<GetListings?> GetListingsAsync(string DocId);
    }
}
