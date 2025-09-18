using ValidationLib;

namespace ValidationLibTest
{
    public class AdvancedNumberValidationTests
    {
        [Fact]
        public void IsOneOf_Valid_ShouldPass()
        {
            var person = new TestPerson { Age = 25 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).IsOneOf(20, 25, 30);

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void IsOneOf_Invalid_ShouldFail()
        {
            var person = new TestPerson { Age = 35 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).IsOneOf(20, 25, 30);

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must be one of [20, 25, 30]", result.Messages.First());
        }

        [Fact]
        public void IsNotOneOf_Valid_ShouldPass()
        {
            var person = new TestPerson { Age = 35 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).IsNotOneOf(20, 25, 30);

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void IsNotOneOf_Invalid_ShouldFail()
        {
            var person = new TestPerson { Age = 25 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).IsNotOneOf(20, 25, 30);

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must not be one of [20, 25, 30]", result.Messages.First());
        }

        [Fact]
        public void IsInRange_Inclusive_Valid_ShouldPass()
        {
            var person = new TestPerson { Age = 25 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).IsInRange(20, 30, inclusive: true);

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void IsInRange_Inclusive_Boundary_ShouldPass()
        {
            var person = new TestPerson { Age = 20 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).IsInRange(20, 30, inclusive: true);

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void IsInRange_Exclusive_Valid_ShouldPass()
        {
            var person = new TestPerson { Age = 25 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).IsInRange(20, 30, inclusive: false);

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void IsInRange_Exclusive_Boundary_ShouldFail()
        {
            var person = new TestPerson { Age = 20 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).IsInRange(20, 30, inclusive: false);

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must be in range (20, 30) (exclusive)", result.Messages.First());
        }

        [Fact]
        public void IsOutsideRange_Valid_ShouldPass()
        {
            var person = new TestPerson { Age = 35 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).IsOutsideRange(20, 30);

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void IsOutsideRange_Invalid_ShouldFail()
        {
            var person = new TestPerson { Age = 25 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).IsOutsideRange(20, 30);

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must be outside range [20, 30]", result.Messages.First());
        }

        [Fact]
        public void IsMultipleOf_Valid_ShouldPass()
        {
            var person = new TestPerson { Age = 30 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).IsMultipleOf(5);

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void IsMultipleOf_Invalid_ShouldFail()
        {
            var person = new TestPerson { Age = 23 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).IsMultipleOf(5);

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must be a multiple of 5", result.Messages.First());
        }

        [Fact]
        public void IsNotMultipleOf_Valid_ShouldPass()
        {
            var person = new TestPerson { Age = 23 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).IsNotMultipleOf(5);

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void IsNotMultipleOf_Invalid_ShouldFail()
        {
            var person = new TestPerson { Age = 30 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).IsNotMultipleOf(5);

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must not be a multiple of 5", result.Messages.First());
        }
    }

    public class TestPersonWithDouble
    {
        public double Value { get; set; }
    }

    public class DoubleValidationTests
    {
        [Fact]
        public void HasPrecision_Valid_ShouldPass()
        {
            var obj = new TestPersonWithDouble { Value = 12.34 };
            var validator = new ValidatorBase<TestPersonWithDouble>();
            validator.RulesFor(x => x.Value).HasPrecision(2);

            var result = validator.Validate(obj);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void HasPrecision_Invalid_ShouldFail()
        {
            var obj = new TestPersonWithDouble { Value = 12.345 };
            var validator = new ValidatorBase<TestPersonWithDouble>();
            validator.RulesFor(x => x.Value).HasPrecision(2);

            var result = validator.Validate(obj);
            Assert.False(result.IsValid);
            Assert.Contains("must have at most 2 decimal places", result.Messages.First());
        }

        [Fact]
        public void IsFinite_Valid_ShouldPass()
        {
            var obj = new TestPersonWithDouble { Value = 123.45 };
            var validator = new ValidatorBase<TestPersonWithDouble>();
            validator.RulesFor(x => x.Value).IsFinite();

            var result = validator.Validate(obj);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void IsFinite_Invalid_ShouldFail()
        {
            var obj = new TestPersonWithDouble { Value = double.PositiveInfinity };
            var validator = new ValidatorBase<TestPersonWithDouble>();
            validator.RulesFor(x => x.Value).IsFinite();

            var result = validator.Validate(obj);
            Assert.False(result.IsValid);
            Assert.Contains("must be finite", result.Messages.First());
        }

        [Fact]
        public void IsNormal_Valid_ShouldPass()
        {
            var obj = new TestPersonWithDouble { Value = 123.45 };
            var validator = new ValidatorBase<TestPersonWithDouble>();
            validator.RulesFor(x => x.Value).IsNormal();

            var result = validator.Validate(obj);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void IsNormal_Invalid_ShouldFail()
        {
            var obj = new TestPersonWithDouble { Value = 0.0 };
            var validator = new ValidatorBase<TestPersonWithDouble>();
            validator.RulesFor(x => x.Value).IsNormal();

            var result = validator.Validate(obj);
            Assert.False(result.IsValid);
            Assert.Contains("must be a normal number", result.Messages.First());
        }
    }
}