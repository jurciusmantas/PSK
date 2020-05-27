namespace PSK.Model.Authorization
{
    public interface ITokenValidator
    {
        bool Validate(string token);
    }
}
