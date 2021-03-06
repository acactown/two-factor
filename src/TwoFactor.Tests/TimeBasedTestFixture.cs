﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TwoFactor.Tests
{
    [NUnit.Framework.TestFixture]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class TimeBasedTestFixture
    {
        private void Test(string sharedSecret, long seconds, string expected)
        {
            var calculated = TimeBasedOneTimePassword.GetPassword(sharedSecret, TimeBasedOneTimePassword.UNIX_EPOCH + TimeSpan.FromSeconds(seconds), TimeBasedOneTimePassword.UNIX_EPOCH, 30, 8);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, calculated);
            NUnit.Framework.Assert.AreEqual(expected, calculated);
        }

        /// <summary>
        /// This test comes from Table 1 in RFC 6238 which defines expected values for a given time.
        /// </summary>
        [NUnit.Framework.Test]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void TestRfc()
        {
            string sharedSecret = "12345678901234567890";

            Dictionary<long, string> expectedResults = new Dictionary<long, string>()
            {
                { 59, "94287082" },
                { 1111111109, "07081804" },
                { 1111111111, "14050471" },
                { 1234567890, "89005924" },
                { 2000000000, "69279037" },
                { 20000000000, "65353130" },
            };

            foreach (var x in expectedResults)
            {
                Test(sharedSecret, x.Key, x.Value);
            }
        }
    }
}
