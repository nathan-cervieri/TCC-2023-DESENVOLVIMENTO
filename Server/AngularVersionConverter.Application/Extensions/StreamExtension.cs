namespace AngularVersionConverter.Application.Extensions
{
    public static class StreamExtension
    {
        public static string ReadStreamToEnd(this Stream stream)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            if (!stream.CanRead) throw new ArgumentException("Stream cannot be read");

            stream.Position = 0;
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
