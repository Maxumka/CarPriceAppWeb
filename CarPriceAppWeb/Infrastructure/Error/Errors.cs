
namespace CarPriceAppWeb.Infrastructure.Error
{
    public record Error(int Id, string Message);

    public static class Errors
    {
        public static readonly Error UnknownError = new(0, "Unknown error");

        public static readonly Error UserNotFound = new(1, "User not found");

        public static readonly Error CarWasNull = new(2, "Cars was null");

        public static readonly Error UserWasNull = new(3, "The user war null");

        public static readonly Error UserAlreadyInDb = new(4, "The user is already in the database");
    }
}
