//----------------------------------------
// MIT License
// Copyright(c) 2021 Jonas Boetel
//----------------------------------------
using System;
using NUnit.Framework;
using UnityEngine.Profiling;

namespace Lumpn.Matomo.Demo
{
    public sealed class UriTest
    {
        [Test]
        public void EscapeDataString()
        {
            Profiler.BeginSample("EscapeDataString");

            Profiler.BeginSample("Plain");
            Uri.EscapeDataString("Plain"); // 0 bytes
            Profiler.EndSample();

            Profiler.BeginSample("ForwardSlash");
            Uri.EscapeDataString("Forward/Slash"); // 342 bytes
            Profiler.EndSample();

            Profiler.EndSample();
        }
    }
}
