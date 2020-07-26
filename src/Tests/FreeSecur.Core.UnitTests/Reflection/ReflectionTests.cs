using FreeSecur.Core.Reflection;
using FreeSecur.Core.Validation;
using FreeSecur.Core.Validation.Attributes;
using FreeSecur.Core.Validation.Validator;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FreeSecur.Core.UnitTests.Reflection
{
    [TestClass]
    public class ReflectionTests
    {
        public ReflectionTests()
        {
        }

        [TestMethod]
        public void GetElementTypeShouldReturnElementType()
        {
            var listType = typeof(List<ComplexModel>);

            var target = new MetadataReflectionService();

            var actual = target.GetElementType(listType);
            var expected = typeof(ComplexModel);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetElementTypeShouldReturnException()
        {
            var classType = typeof(ComplexModel);

            var target = new MetadataReflectionService();

            Assert.ThrowsException<ArgumentException>(() => target.GetElementType(classType));
        }

        [TestMethod]
        public void ListShouldReturnIsCollection()
        {
            var target = new MetadataReflectionService();

            var listType = typeof(List<ComplexModel>);

            var actual = target.IsCollection(listType);

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void StringShouldNotReturnIsCollection()
        {
            var target = new MetadataReflectionService();
            var stringType = typeof(string);
            var actual = target.IsCollection(stringType);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void GetMetadataTypeShouldReturnType()
        {
            var target = new MetadataReflectionService();
            var type = typeof(ComplexModel);
            var actual = target.GetMetadataType(type);
            var expected = typeof(ComplexMetadata);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetMetadataTypeShouldNotReturnType()
        {
            var target = new MetadataReflectionService();
            var type = typeof(TestModel);
            var actual = target.GetMetadataType(type);
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void GetMetadataTypeShouldNostReturnType()
        {
            var target = new MetadataReflectionService();
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

        //TODO test this code scanner
        //[TestMethod]
        //public void G()
        //{
        //    var target = new MetadataReflectionService();
        //    var type = typeof(ITest);
        //    var actual = target.GetTypesThatImplement(type).Single();

        //    Assert.AreEqual(nameof(TestModel), actual.Name);
        //}
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
        public string RequiredField { get; }
        public List<ComplexModel> ComplexFields { get; }
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
        public string Email { get; }
        public string Password { get; }
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
