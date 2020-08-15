using FreeSecur.Core.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeSecur.Core.UnitTests.Reflection
{
    [TestClass]
    public class StringInterpolationServiceTests
    {
        private ReflectionService _reflectionService = new ReflectionService();


        [TestMethod]
        public void InterpolateShouldCorrectlyReplaceKeysInString()
        {
            var target = new StringInterpolationService(_reflectionService);
            var testObject = CreateTestObject();

            var inputValue = "{TestString}{TestInt}{TestDate}{TestChild.TestString}";
            var expected = $"{testObject.TestString}{testObject.TestInt}{testObject.TestDate}{testObject.TestChild.TestString}";

            var actual = target.Interpolate(inputValue, testObject);

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void InterpolateShouldEscapeDoubleBracket()
        {
            var testObject = CreateTestObject();

            var inputValue = "{{TestString}{TestInt}{TestDate}{TestChild.TestString}";
            var expected = $"{{{{TestString}}{testObject.TestInt}{testObject.TestDate}{testObject.TestChild.TestString}";

            var target = new StringInterpolationService(_reflectionService);
            var actual = target.Interpolate(inputValue, testObject);

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void InterpolateShouldThrowExceptionOnInvalidKeys()
        {
            var emptyKey = "{}";
            var nonExistingKey = "{hallo}";
            var testObject = CreateTestObject();

            var target = new StringInterpolationService(_reflectionService);

            Assert.ThrowsException<ArgumentNullException>(() => target.Interpolate(emptyKey, testObject));
            Assert.ThrowsException<ArgumentNullException>(() => target.Interpolate(emptyKey, nonExistingKey));
        }

        private DateTime _dateTime = DateTime.Now;
        private string _string = "Wow";
        private int _int = 324;

        private TestClass CreateTestObject()
        {
            var childTestObject = new TestClass(_string, _int, _dateTime);
            var testObject = new TestClass(_string, _int, _dateTime, childTestObject);

            return testObject;
        }
    }

    internal class TestClass
    {
        public TestClass(string testString, int testInt, DateTime testDate, TestClass testChild  = null)
        {
            TestString = testString;
            TestInt = testInt;
            TestDate = testDate;
            TestChild = testChild;
        }

        public string TestString { get; }
        public int TestInt { get; }
        public DateTime TestDate { get; }
        public TestClass TestChild { get; }
    }
}
