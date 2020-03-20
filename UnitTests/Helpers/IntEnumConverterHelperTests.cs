using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Game.Models;
using Game.Helpers;

namespace UnitTests.Helpers
{
    [TestFixture]
    class IntEnumConverterHelperTests
    {
        [Test]
        public void IntEnumConvert_Should_Skip()
        {
            var myConverter = new IntEnumConverter();

            var myObject = "Fake";
            var Result = myConverter.Convert(myObject, null, null, null);
            var Expected = 0;

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }
    }
}
