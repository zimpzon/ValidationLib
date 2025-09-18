using ValidationLib;

namespace ValidationLibTest
{
    public class StringValidationTests
    {
        [Fact]
        public void StartsWith_Valid_ShouldPass()
        {
            var person = new TestPerson { Name = "JohnDoe" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).StartsWith("John");

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void StartsWith_Invalid_ShouldFail()
        {
            var person = new TestPerson { Name = "Peter" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).StartsWith("John");

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must start with 'John'", result.Messages.First());
        }

        [Fact]
        public void EndsWith_Valid_ShouldPass()
        {
            var person = new TestPerson { Name = "JohnDoe" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).EndsWith("Doe");

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void EndsWith_Invalid_ShouldFail()
        {
            var person = new TestPerson { Name = "JohnSmith" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).EndsWith("Doe");

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must end with 'Doe'", result.Messages.First());
        }

        [Fact]
        public void NotContains_Valid_ShouldPass()
        {
            var person = new TestPerson { Name = "JohnDoe" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).NotContains("Smith");

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void NotContains_Invalid_ShouldFail()
        {
            var person = new TestPerson { Name = "JohnSmith" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).NotContains("Smith");

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must not contain 'Smith'", result.Messages.First());
        }

        [Fact]
        public void IsNumeric_Valid_ShouldPass()
        {
            var person = new TestPerson { Name = "12345" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).IsNumeric();

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void IsNumeric_Invalid_ShouldFail()
        {
            var person = new TestPerson { Name = "John123" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).IsNumeric();

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must contain only numeric characters", result.Messages.First());
        }

        [Fact]
        public void IsAlphabetic_Valid_ShouldPass()
        {
            var person = new TestPerson { Name = "JohnDoe" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).IsAlphabetic();

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void IsAlphabetic_Invalid_ShouldFail()
        {
            var person = new TestPerson { Name = "John123" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).IsAlphabetic();

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must contain only alphabetic characters", result.Messages.First());
        }

        [Fact]
        public void IsAlphaNumeric_Valid_ShouldPass()
        {
            var person = new TestPerson { Name = "John123" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).IsAlphaNumeric();

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void IsAlphaNumeric_Invalid_ShouldFail()
        {
            var person = new TestPerson { Name = "John-123" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).IsAlphaNumeric();

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must contain only alphanumeric characters", result.Messages.First());
        }

        [Fact]
        public void IsOneOf_Valid_ShouldPass()
        {
            var person = new TestPerson { Name = "John" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).IsOneOf("John", "Jane", "Bob");

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void IsOneOf_Invalid_ShouldFail()
        {
            var person = new TestPerson { Name = "Peter" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).IsOneOf("John", "Jane", "Bob");

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must be one of ['John', 'Jane', 'Bob']", result.Messages.First());
        }

        [Fact]
        public void IsNotOneOf_Valid_ShouldPass()
        {
            var person = new TestPerson { Name = "Peter" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).IsNotOneOf("John", "Jane", "Bob");

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void IsNotOneOf_Invalid_ShouldFail()
        {
            var person = new TestPerson { Name = "John" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).IsNotOneOf("John", "Jane", "Bob");

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must not be one of ['John', 'Jane', 'Bob']", result.Messages.First());
        }
    }

    public class TestStringValidator : ValidatorBase<TestPerson>
    {
    }
}