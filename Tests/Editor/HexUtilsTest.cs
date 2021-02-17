//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System.Text;
using NUnit.Framework;
using Lumpn.Matomo.Utils;

namespace Lumpn.Matomo.Test
{
    public sealed class HexUtilsTest
    {
        [Test]
        public void BytesToHex()
        {
            var bytes = new byte[] { 0x0D, 0x15, 0xEA, 0x5E };
            var expected = "0D15EA5E";

            var builder = new StringBuilder();
            HexUtils.AppendHex(builder, bytes);
            var actual = builder.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
