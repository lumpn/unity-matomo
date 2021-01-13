using System.Text;

namespace Matomo
{
    public static class HexUtils
    {
        public static void AppendHex(StringBuilder builder, byte[] bytes)
        {
            foreach (var value in bytes)
            {
                var upper = value >> 4;
                var lower = value & 0x0F;

                builder.Append(HalfByteToHex(upper));
                builder.Append(HalfByteToHex(lower));
            }
        }

        private static char HalfByteToHex(int value)
        {
            var result = (value < 10) ? value + 48 : value + 55;
            return (char)result;
        }
    }
}
