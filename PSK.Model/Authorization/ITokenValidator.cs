namespace PSK.Model.Services
{
    public interface ITokenValidator
    {
        bool Validate(string token);
    }
}
