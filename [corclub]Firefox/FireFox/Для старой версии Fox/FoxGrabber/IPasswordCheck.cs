namespace FoxGrabber
{
    public interface IPasswordCheck
    {
        string EntrySalt { get; }
        string OID { get; }
        string Passwordcheck { get; }
    }
}