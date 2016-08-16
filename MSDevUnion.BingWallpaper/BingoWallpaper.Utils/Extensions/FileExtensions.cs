using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BingoWallpaper.Extensions
{
    public static class FileExtensions
    {
        public static Task<byte[]> ReadAllBytesAsync(string path)
        {
            return ReadAllBytesAsync(path, CancellationToken.None);
        }

        public static async Task<byte[]> ReadAllBytesAsync(string path, CancellationToken cancellationToken)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            using (var fs = File.OpenRead(path))
            {
                var buffer = new byte[fs.Length];
                await fs.ReadAsync(buffer, 0, buffer.Length, cancellationToken);
                return buffer;
            }
        }

        public static async Task<string> ReadAllTextAsync(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            using (var sr = File.OpenText(path))
            {
                return await sr.ReadToEndAsync();
            }
        }

        public static Task WriteAllBytesAsync(string path, byte[] bytes)
        {
            return WriteAllBytesAsync(path, bytes, CancellationToken.None);
        }

        public static async Task WriteAllBytesAsync(string path, byte[] bytes, CancellationToken cancellationToken)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            using (var fs = File.OpenWrite(path))
            {
                await fs.WriteAsync(bytes, 0, bytes.Length, cancellationToken);
            }
        }

        public static Task WriteAllTextAsync(string path, string contents)
        {
            return WriteAllTextAsync(path, contents, Encoding.UTF8);
        }

        public static async Task WriteAllTextAsync(string path, string contents, Encoding encoding)
        {
            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (contents == null)
            {
                throw new ArgumentNullException(nameof(contents));
            }
            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            using (var fs = File.OpenWrite(path))
            {
                using (var sw = new StreamWriter(fs, encoding))
                {
                    await sw.WriteAsync(contents);
                }
            }
        }
    }
}