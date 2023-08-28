namespace InteractiveNaturalDisasterMap.Application.Exceptions
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException(string name, int userId) : base($"userId: {userId} wrong access to Entity - {name}")
        {
        }
    }
}
