namespace Prueba_Completa_NET.Exceptions
{
    public class ValidationException: Exception
    {
        public List<string> Errors { get; }
        public ValidationException(IEnumerable<string> errors)
            : base("Error de validación")
        {
            Errors = errors.ToList();
        }
    }
}
