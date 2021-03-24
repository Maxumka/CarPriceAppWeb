
namespace CarPriceAppWeb.Infrastructure.Helpers
{
    public sealed class Either<TR, TE> where TE : Error.Error
    {
        public TR Result { get; private set; }

        public TE Error { get; private set; }

        public Either(TR result, TE error) => (Result, Error) = (result, error);
    }
}
