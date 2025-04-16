namespace ValidationLib
{
    public interface IValidator
    {
        bool Validate();
        Task<bool> ValidateAsync();
    }
}
