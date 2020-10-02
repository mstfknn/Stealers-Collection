namespace FoxGrabber
{
    using System.Globalization;

    public class PasswordCheck : IPasswordCheck
    {
        public string EntrySalt { get; private set; }
        public string OID { get; private set; }
        public string Passwordcheck { get; private set; }

        public PasswordCheck(string DataToParse)
        {
            this.EntrySalt = DataToParse.Substring(6, int.Parse(DataToParse.Substring(2, 2), NumberStyles.HexNumber) * 2);

            this.OID = DataToParse.Substring(6 + int.Parse(DataToParse.Substring(2, 2), NumberStyles.HexNumber) * 2 + 36, DataToParse.Length - (6 + int.Parse(DataToParse.Substring(2, 2), NumberStyles.HexNumber) * 2 + 36));

            this.Passwordcheck = DataToParse.Substring(6 + int.Parse(DataToParse.Substring(2, 2), NumberStyles.HexNumber) * 2 + 4 + (DataToParse.Length - (6 + int.Parse(DataToParse.Substring(2, 2), NumberStyles.HexNumber) * 2 + 36)));
        }

        public PasswordCheck()
        {
        }
    }
}
