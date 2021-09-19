//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System.Text;

namespace Lumpn.Matomo.Utils
{
    internal static class HexUtils
    {
        public static string ToString(byte[] bytes)
        {
            var sb = new StringBuilder();
            AppendHex(sb, bytes);
            return sb.ToString();
        }

        public static void AppendHex(StringBuilder sb, byte[] bytes)
        {
            foreach (var value in bytes)
            {
                var upper = value >> 4;
                var lower = value & 0x0F;

                sb.Append(HalfByteToHex(upper));
                sb.Append(HalfByteToHex(lower));
            }
        }

        private static char HalfByteToHex(int value)
        {
            var result = (value < 10) ? value + 48 : value + 55;
            return (char)result;
        }
    }
}
