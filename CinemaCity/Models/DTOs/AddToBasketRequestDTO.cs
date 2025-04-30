namespace CinemaCity.Models.DTOs
{
    public class AddToBasketRequestDTO
    {
        public FilmDTO Film { get; set; }
        public int Session_Id { get; set; }
        public string Theater { get; set; }
        public string Date { get; set; }
        public List<SeatDto> Seats { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
