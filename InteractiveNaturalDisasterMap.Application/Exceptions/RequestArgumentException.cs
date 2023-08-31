namespace InteractiveNaturalDisasterMap.Application.Exceptions
{
    public class RequestArgumentException : Exception
    {
        public RequestArgumentException(string name, object value) : base($"Argument - {name}, wrong value({value})")
        {
        }
    }
}
