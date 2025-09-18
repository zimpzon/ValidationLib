using ValidationLib;

namespace ValidationLibTest
{
    public class NumberValidationTests
    {
        [Fact]
        public void GreaterThan_Valid_ShouldPass()
        {
            var person = new TestPerson { Age = 25 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).GreaterThan(20);

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void GreaterThan_Invalid_ShouldFail()
        {
            var person = new TestPerson { Age = 15 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).GreaterThan(20);

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must be greater than 20", result.Messages.First());
        }

        [Fact]
        public void GreaterThanOrEqual_Valid_ShouldPass()
        {
            var person = new TestPerson { Age = 20 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).GreaterThanOrEqual(20);

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void GreaterThanOrEqual_Invalid_ShouldFail()
        {
            var person = new TestPerson { Age = 19 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).GreaterThanOrEqual(20);

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must be greater than or equal to 20", result.Messages.First());
        }

        [Fact]
        public void LessThan_Valid_ShouldPass()
        {
            var person = new TestPerson { Age = 15 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).LessThan(20);

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void LessThan_Invalid_ShouldFail()
        {
            var person = new TestPerson { Age = 25 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).LessThan(20);

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must be less than 20", result.Messages.First());
        }

        [Fact]
        public void LessThanOrEqual_Valid_ShouldPass()
        {
            var person = new TestPerson { Age = 20 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).LessThanOrEqual(20);

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void LessThanOrEqual_Invalid_ShouldFail()
        {
            var person = new TestPerson { Age = 25 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).LessThanOrEqual(20);

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must be less than or equal to 20", result.Messages.First());
        }

        [Fact]
        public void NotEqual_Valid_ShouldPass()
        {
            var person = new TestPerson { Age = 25 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).NotEqual(20);

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void NotEqual_Invalid_ShouldFail()
        {
            var person = new TestPerson { Age = 20 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).NotEqual(20);

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must not equal 20", result.Messages.First());
        }

        [Fact]
        public void IsPositive_Valid_ShouldPass()
        {
            var person = new TestPerson { Age = 25 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).IsPositive();

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void IsPositive_Invalid_ShouldFail()
        {
            var person = new TestPerson { Age = -5 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).IsPositive();

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must be positive", result.Messages.First());
        }

        [Fact]
        public void IsNegative_Valid_ShouldPass()
        {
            var person = new TestPerson { Age = -5 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).IsNegative();

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void IsNegative_Invalid_ShouldFail()
        {
            var person = new TestPerson { Age = 25 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).IsNegative();

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must be negative", result.Messages.First());
        }

        [Fact]
        public void IsZero_Valid_ShouldPass()
        {
            var person = new TestPerson { Age = 0 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).IsZero();

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void IsZero_Invalid_ShouldFail()
        {
            var person = new TestPerson { Age = 25 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).IsZero();

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must be zero", result.Messages.First());
        }

        [Fact]
        public void IsEven_Valid_ShouldPass()
        {
            var person = new TestPerson { Age = 24 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).IsEven();

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void IsEven_Invalid_ShouldFail()
        {
            var person = new TestPerson { Age = 25 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).IsEven();

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must be even", result.Messages.First());
        }

        [Fact]
        public void IsOdd_Valid_ShouldPass()
        {
            var person = new TestPerson { Age = 25 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).IsOdd();

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void IsOdd_Invalid_ShouldFail()
        {
            var person = new TestPerson { Age = 24 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).IsOdd();

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must be odd", result.Messages.First());
        }

        [Fact]
        public void Must_CustomPredicate_Valid_ShouldPass()
        {
            var person = new TestPerson { Age = 30 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).WithMessage("must be divisible by 5").Must(x => x % 5 == 0);

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Must_CustomPredicate_Invalid_ShouldFail()
        {
            var person = new TestPerson { Age = 23 };
            var validator = new TestNumberValidator();
            validator.RulesFor(x => x.Age).WithMessage("must be divisible by 5").Must(x => x % 5 == 0);

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must be divisible by 5", result.Messages.First());
        }
    }

    public class TestNumberValidator : ValidatorBase<TestPerson>
    {
    }
}