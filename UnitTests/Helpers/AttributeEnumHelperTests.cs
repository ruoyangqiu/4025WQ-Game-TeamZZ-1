using System;
using System.Linq;

using NUnit.Framework;

using Game.Models;
using Game.Helpers;

namespace UnitTests.Helpers
{
    [TestFixture]
    class AttributeEnumHelperTests
    {
        [Test]
        public void AttributeEnumHelper_GetListCharacter_Should_Pass()
        {
            // Arrange

            // Instantiate a new Attribute Base, should have default of 1 for all values
            var myDataList = AttributeEnumHelper.GetListCharacter;

            // Get Expected set
            var myList = Enum.GetNames(typeof(AttributeEnum)).ToList();
            var myExpectedList = myList.Where(a =>
                                            a.ToString() != AttributeEnum.Unknown.ToString()
                                        ).ToList();

            // Act

            // Make sure each item is in the list
            foreach (var item in myDataList)
            {
                //if (item == AttributeEnum.Unknown.ToString())
                //{
                //    continue;
                //}

                var found = false;
                foreach (var expected in myExpectedList)
                {
                    if (item == expected)
                    {
                        found = true;
                        break;
                    }
                }

                // Assert
                Assert.AreEqual(true, found, "item : " + item + TestContext.CurrentContext.Test.Name);
            }

            // reverse it, to make sure the list has each item
            // Make sure each item is in the list
            foreach (var expected in myExpectedList)
            {
                var found = false;
                {
                    foreach (var item in myDataList)
                        if (item == expected)
                        {
                            found = true;
                            break;
                        }
                }

                // Assert
                Assert.AreEqual(true, found, "expected : " + expected + TestContext.CurrentContext.Test.Name);
            }

        }
    }
}
