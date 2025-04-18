namespace CinemaCity.Exceptions
{
    public class WeakPasswordException : Exception
    {
        public WeakPasswordException(string message) : base(message) { }
    }
}
