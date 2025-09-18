namespace ValidationLib
{
    public class ValidationResult
    {
        public bool IsValid { get; init; } = true;
        public IEnumerable<string> Messages { get; init; } = [];
    }
}
