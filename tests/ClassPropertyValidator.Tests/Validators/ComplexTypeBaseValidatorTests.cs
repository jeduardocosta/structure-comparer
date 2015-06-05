using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassPropertyValidator.Tests.Fakes.StructureA;
using NUnit.Framework;
using ClassPropertyValidator.Validators;
using FluentAssertions;

namespace ClassPropertyValidator.Tests.Validators
{
    [TestFixture]
    public class ComplexTypeBaseValidatorTests
    {
        private ITypeBaseValidator _complexTypeBaseValidator;

        [SetUp]
        public void SetUp()
        {
            _complexTypeBaseValidator = new ComplexTypeBaseValidator();
        }

        [TestCase(typeof(IEnumerable<FakeCustomer>), typeof(IEnumerable<FakeCustomer>))]
        public void Validate_GivenAValidComplexTypeCollection_ShouldReturnTrueToValidationResult(Type baseType, Type toCompareType)
        {
            var result = _complexTypeBaseValidator.Validate(baseType, toCompareType);

            result.Should().BeTrue();
        }
    }
}