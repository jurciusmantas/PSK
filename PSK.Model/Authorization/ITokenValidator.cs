namespace PSK.Model.Authorization
{
    public interface ITokenValidator
    {
        int Validate(string token);
    }
}
