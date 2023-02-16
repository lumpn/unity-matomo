//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using Lumpn.Matomo.Utils;
using NUnit.Framework;

namespace Lumpn.Matomo.Test
{
    public sealed class HashUtilsTest
    {
        [Test]
        public void BytesToMD5()
        {
            var bytes = new byte[] { 0x0D, 0x15, 0xEA, 0x5E };
            var expected = new byte[] { 0xBA, 0x94, 0x26, 0x5B, 0xF1, 0xDD, 0xE2, 0x40, 0xB5, 0xFA, 0x57, 0x31, 0xEE, 0x99, 0x0D, 0x55 };

            var actual = HashUtils.HashMD5(bytes);
            UnityEngine.Debug.Log(HexUtils.ToString(actual));

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void StringToMD5()
        {
            var str = "Test";
            var expected = new byte[] { 0x0C, 0xBC, 0x66, 0x11, 0xF5, 0x54, 0x0B, 0xD0, 0x80, 0x9A, 0x38, 0x8D, 0xC9, 0x5A, 0x61, 0x5B };

            var actual = HashUtils.HashMD5(str);
            UnityEngine.Debug.Log(HexUtils.ToString(actual));

            Assert.AreEqual(expected, actual);
        }
    }
}
