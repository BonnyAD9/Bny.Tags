using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bny.Tags.ID3v2Tags
{
    internal class UnsynchronizedStream : Stream
    {
        private const byte sMask = 0b1110_0000;
        public Stream Stream { get; init; }

        public bool UseUnsinchronization { get; set; } = false;

        public override bool CanRead => Stream.CanRead && Stream.CanSeek;

        public override bool CanSeek => Stream.CanSeek;

        public override bool CanWrite => false;

        public override long Length => Stream.Length;

        public override long Position
        {
            get => Stream.Position;
            set => Stream.Position = value;
        }

        public UnsynchronizedStream(Stream baseStream, bool useUnsinchronization = false)
        {
            Stream = baseStream;
            UseUnsinchronization = useUnsinchronization;
        }

        public override void Flush() => Stream.Flush();

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (!UseUnsinchronization)
                return Stream.Read(buffer, offset, count);

            byte[] innerBuffer = new byte[count + 1];
            int len = Stream.Read(innerBuffer, 0, count + 1);
            int set = 0;
            
            for (int i = 0; i < len - 1; i++, set++)
            {
                buffer[set + count] = innerBuffer[i];
                if (PatternCheck(innerBuffer, i))
                    i++;
            }

            if (len != count + 1)
                return set;

            if (!PatternCheck(innerBuffer, count - 1))
                Position--;

            if (set != count)
                return Read(buffer, offset + set, count - set) + set;
            return set;
        }

        private bool PatternCheck(byte[] buffer, int pos) => buffer[pos] == 0xFF && ((buffer[pos + 1] & sMask) == sMask);

        public override long Seek(long offset, SeekOrigin origin) => Stream.Seek(offset, origin);

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }
    }
}
