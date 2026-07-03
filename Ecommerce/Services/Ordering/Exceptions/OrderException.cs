namespace Ordering.Exceptions
{
    public class OrderException : Exception
    {
        public OrderException(string entity, object key, string message) : base($"Entity {entity} - {key} - {message}")
        {

        }
    }
}
