﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NiceHashMinerLegacy.Extensions.Tests
{
    [TestClass]
    public class StringTest
    {
        [TestMethod]
        public void GetHashrateAfterShouldParse()
        {
            // xmr-stak
            "[2019-01-06 21:18:33] : Benchmark Total: 756.2 H/S".TryGetHashrateAfter("Benchmark Total:", out var hash);
            Assert.AreEqual(756.2, hash);

            // Claymore 
            "ETH - Total Speed: 32.339 Mh/s, Total Shares: 0, Rejected: 0, Time: 00:00"
                .TryGetHashrateAfter("Total Speed:", out hash);
            Assert.AreEqual(32339000, hash);

            // T-rex
            "20190109 23:53:41 Total: 71.89 MH/s".TryGetHashrateAfter("Total:", out hash);
            Assert.AreEqual(71890000, hash);

            // GMiner
            "15:14:18 Total Speed: 27 Sol/s Shares Accepted: 0 Rejected: 0 Power: 199W 0.14 Sol/W"
                .TryGetHashrateAfter("Total Speed:", out hash);
            Assert.AreEqual(27, hash);

            // ccminer read exception
            "[2019-01-29 12:16:21]\u001b[01;37m Total: 803.28 kH/s\u001b[0m"
                .TryGetHashrateAfter("Total:", out hash);
            Assert.AreEqual(803280, hash);
        }

        [TestMethod]
        public void GetHashrateAfterShouldReturnNull()
        {
            Assert.IsFalse("[2019-01-06 21:18:33] : Benchmark Total: --- H/S"
                .TryGetHashrateAfter("Benchmark Total:", out var hash));
            Assert.AreEqual(0, hash);
        }
        
        [TestMethod]
        public void AfterFirstNumberShouldMatch()
        {
            var a = "NVIDIA GTX 1080 Ti".AfterFirstOccurence("NVIDIA ");
            Assert.AreEqual("GTX 1080 Ti", a);

            var b = "blarg".AfterFirstOccurence("NV ");
            Assert.AreEqual("", b);

            var c = "NVIDIA GTX 750 Ti".AfterFirstOccurence("GTX");
            Assert.AreEqual(" 750 Ti", c);
        }
    }
}
