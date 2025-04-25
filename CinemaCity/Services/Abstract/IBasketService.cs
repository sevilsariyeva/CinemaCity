namespace CinemaCity.Services.Abstract
{
    public interface IBasketService
    {
        Task AddTicketToBasketAsync(int userId, string seatNumber, int sessionId);
    }
}
