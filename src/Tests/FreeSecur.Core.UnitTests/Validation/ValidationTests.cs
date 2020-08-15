using FreeSecur.Core.Reflection;
using FreeSecur.Core.Validation;
using FreeSecur.Core.Validation.Attributes;
using FreeSecur.Core.Validation.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FreeSecur.Core.UnitTests.Validation
{
    [TestClass]
    public class ValidationTests
    {
        private ReflectionService _reflectionService;
        public ValidationTests()
        {
            _reflectionService = new ReflectionService();
        }

        [TestMethod]
        public void ValidationShouldSucceed()
        {
            var target = new FsModelValidator(_reflectionService);

            var complexModel1 = new ComplexModel("test@test.nl", "aaaaaaaaaaa");
            var complexModel2 = new ComplexModel("test@tes2.nl", "bbbbbbbbbbb");
            var complexModels = new List<ComplexModel> { complexModel1, complexModel2 };
            var testModel = new TestModel("nice", complexModels);

            var validationResult = target.Validate(testModel);
            var actual = !validationResult.Any();

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void ListValidationShouldFailWithErrorCode()
        {
            var target = new FsModelValidator(_reflectionService);

            var complexModel1 = new ComplexModel("test@test.nl", "aaaaaaaaaaa");
            var complexModel2 = new ComplexModel("test@tes2.nl", "bbbb");
            var complexModels = new List<ComplexModel> { complexModel1, complexModel2 };
            var testModel = new TestModel("nice", complexModels);

            var validationResult = target.Validate(testModel);

            var errorCode = validationResult[0]
                .SubFieldValidationResults[0]
                .ErrorCodes[0];

            Assert.AreEqual(FieldValidationErrorCode.MinLength, errorCode);
            Assert.IsFalse(validationResult[0].IsValid);
        }

        [TestMethod]
        public void EmailValidationShouldFailWithErrorCode()
        {
            var target = new FsModelValidator(_reflectionService);

            var complexModel = new ComplexModel("notavalidmail", "bbbbbbbbbbb");

            var validationResult = target.Validate(complexModel);

            var errorCode = validationResult[0]
                .ErrorCodes[0];

            Assert.AreEqual(FieldValidationErrorCode.EmailAddress, errorCode);
            Assert.IsFalse(validationResult[0].IsValid);
        }
    }

    internal class TestModel
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
