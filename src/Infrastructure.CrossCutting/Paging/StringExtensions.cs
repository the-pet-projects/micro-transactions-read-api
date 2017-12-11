namespace PetProjects.MicroTransactionsApi.Infrastructure.CrossCutting.Paging
{
    using System;
    using System.Text;

    public static class StringExtensions
    {
        // https://stackoverflow.com/a/311179
        public static string ByteArrayToString(this byte[] ba)
        {
            var hex = new StringBuilder(ba.Length * 2);
            foreach (var b in ba)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }

        // https://stackoverflow.com/a/311179
        public static byte[] StringToByteArray(this string hex)
        {
            var numberChars = hex.Length;
            var bytes = new byte[numberChars / 2];
            for (var i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }
    }
}