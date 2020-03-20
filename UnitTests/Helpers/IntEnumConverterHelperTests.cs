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
        [Test]
        public void IntEnumConvert_Should_Pass()
        {
            var myConverter = new IntEnumConverter();

            var myObject = ItemLocationEnum.Feet;
            var Result = myConverter.Convert(myObject, null, null, null);
            var Expected = 40;

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void StringEnumConvert_String_Should_Pass()
        {
            var myConverter = new StringEnumConverter();

            var myObject = "Feet";
            var Result = myConverter.Convert(myObject, typeof(ItemLocationEnum), null, null);
            var Expected = ItemLocationEnum.Feet;

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void StringEnumConvert_Enum_Should_Pass()
        {
            var myConverter = new StringEnumConverter();

            var myObject = ItemLocationEnum.Feet;
            var Result = myConverter.Convert(myObject, null, null, null);
            var Expected = 40;

            Assert.AreEqual(Expected, Result, TestContext.CurrentContext.Test.Name);
        }
    }
}
