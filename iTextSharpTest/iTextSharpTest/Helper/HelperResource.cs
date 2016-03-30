namespace iTextSharpTest.Helper
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Reflection;

    internal static class HelperResource
    {
        public static byte[] ReadResourceByte(string resource)
        {
            var resourceName = Assembly.GetExecutingAssembly().GetManifestResourceNames().FirstOrDefault(x => x.ToUpper().Contains(resource.ToUpper()));

            if (resourceName == null) return null;

            var resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);

            using (MemoryStream ms = new MemoryStream())
            {
                resourceStream.CopyTo(ms);
                return ms.ToArray();
            }
        }

        public static void WriteResource(string path, byte[] data)
        {
            File.WriteAllBytes(path, data);
        }

        public static void WriteResource(string path, MemoryStream stream)
        {
            WriteResource(path, stream.ToArray());
        }
        public static void WriteResource(string path, Stream stream)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                WriteResource(path, ms.ToArray());
            }
        }
    }
}
