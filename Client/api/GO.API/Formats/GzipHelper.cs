using System.IO.Compression;

namespace GO.API.Formats
{
    public class GzipHelper
    {
        public static string Decompress(string input)
        {
            byte[] inputBytes = Convert.FromBase64String(input);

            using (MemoryStream inputStream = new MemoryStream(inputBytes))
            using (GZipStream gzipStream = new GZipStream(inputStream, CompressionMode.Decompress))
            using (StreamReader reader = new StreamReader(gzipStream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
