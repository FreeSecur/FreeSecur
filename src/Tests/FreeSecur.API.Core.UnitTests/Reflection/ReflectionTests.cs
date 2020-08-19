using FreeSecur.API.Core.Reflection;
using FreeSecur.API.Core.Validation.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FreeSecur.API.Core.UnitTests.Reflection
{
    [TestClass]
    public class ReflectionTests
    {
        [TestMethod]
        public void GetElementTypeShouldReturnElementType()
        {
            var listType = typeof(List<ComplexModel>);

            var target = new ReflectionService();

            var actual = target.GetElementType(listType);
            var expected = typeof(ComplexModel);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetElementTypeShouldReturnException()
        {
            var classType = typeof(ComplexModel);

            var target = new ReflectionService();

            Assert.ThrowsException<ArgumentException>(() => target.GetElementType(classType));
        }

        [TestMethod]
        public void ListShouldReturnIsCollection()
        {
            var target = new ReflectionService();

            var listType = typeof(List<ComplexModel>);

            var actual = target.IsCollection(listType);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void StringShouldNotReturnIsCollection()
        {
            var target = new ReflectionService();
            var stringType = typeof(string);
            var actual = target.IsCollection(stringType);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void GetMetadataTypeShouldReturnType()
        {
            var target = new ReflectionService();
            var type = typeof(ComplexModel);
            var actual = target.GetMetadataType(type);
            var expected = typeof(ComplexMetadata);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetMetadataTypeShouldNotReturnType()
        {
            var target = new ReflectionService();
            var type = typeof(TestModel);
            var actual = target.GetMetadataType(type);
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetMetadataTypeShouldNostReturnType()
        {
            var target = new ReflectionService();
            var type = typeof(ComplexModel);
            var actual = target.GetReflectionInfo(type);
            var passwordProperty = actual.Properties.Single(x => x.Property.Name == nameof(ComplexModel.Password));
            var attributeName = passwordProperty.CustomAttributes.Single().GetType().Name;

            Assert.AreEqual(typeof(ComplexMetadata), actual.MetaDataType);
            Assert.AreEqual(1, actual.CustomAttributes.Count);
            Assert.IsTrue(actual.Properties.Any(x => x.Property.Name == nameof(ComplexModel.Email)));
            Assert.IsTrue(actual.Properties.Any(x => x.Property.Name == nameof(ComplexModel.Password)));
            Assert.AreEqual(nameof(FsMinLengthAttribute), attributeName);
        }

        [TestMethod]
        public void GetValueForKeyShouldReturnCorrectValue()
        {
            var target = new ReflectionService();
            var key = "ComplexFields[0].Email";

            var expected = "testEmail";
            var testModel = CreateComplexModel();
            testModel.ComplexFields[0].Email = expected;

            var actual = target.GetValueForKey(key, testModel);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetValueForKey_ShouldThrowExceptionOnInvalidKey()
        {
            var target = new ReflectionService();
            var testModel = CreateComplexModel();
            var key = "Email..test";

            Assert.ThrowsException<ArgumentException>(() => target.GetValueForKey(key, testModel));
            Assert.ThrowsException<ArgumentNullException>(() => target.GetValueForKey(null, testModel));
            Assert.ThrowsException<ArgumentNullException>(() => target.GetValueForKey("", testModel));
        }

        private static TestModel CreateComplexModel()
        {
            var complexModel = new ComplexModel("testEmail", "testPassword");
            var testModel = new TestModel("testRequired", new List<ComplexModel> { complexModel });
            return testModel;
        }
    }

    public interface ITest
    {

    }
    internal class TestModel : ITest
    {
        public TestModel(string requiredField, List<ComplexModel> complexFields)
        {
            RequiredField = requiredField;
            ComplexFields = complexFields;
        }

        [FsRequired]
        public string RequiredField { get; set; }
        public List<ComplexModel> ComplexFields { get; set; }
    }

    [MetadataType(typeof(ComplexMetadata))]
    internal class ComplexModel
    {
        public ComplexModel(string email, string password)
        {
            Email = email;
            Password = password;
        }

        [FsEmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }

    internal class ComplexMetadata
    {
        public ComplexMetadata(string password)
        {
            Password = password;
        }

        [FsMinLength(10)]
        public string Password { get; }
    }
}
