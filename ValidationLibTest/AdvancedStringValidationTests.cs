using System.Text.RegularExpressions;
using ValidationLib;

namespace ValidationLibTest
{
    public class AdvancedStringValidationTests
    {
        [Fact]
        public void MatchesRegex_Valid_ShouldPass()
        {
            var person = new TestPerson { Name = "test123" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).MatchesRegex(@"^[a-z]+\d+$");

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void MatchesRegex_Invalid_ShouldFail()
        {
            var person = new TestPerson { Name = "TEST123" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).MatchesRegex(@"^[a-z]+\d+$");

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must match pattern", result.Messages.First());
        }

        [Fact]
        public void MatchesRegex_WithOptions_Valid_ShouldPass()
        {
            var person = new TestPerson { Name = "TEST123" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).MatchesRegex(@"^[a-z]+\d+$", RegexOptions.IgnoreCase);

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Must_CustomPredicate_Valid_ShouldPass()
        {
            var person = new TestPerson { Name = "hello" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).WithMessage("must have all lowercase letters").Must(s => s.All(char.IsLower));

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Must_CustomPredicate_Invalid_ShouldFail()
        {
            var person = new TestPerson { Name = "Hello" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).WithMessage("must have all lowercase letters").Must(s => s.All(char.IsLower));

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must have all lowercase letters", result.Messages.First());
        }

        [Fact]
        public void IsEmpty_Valid_ShouldPass()
        {
            var person = new TestPerson { Name = "" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).IsEmpty();

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void IsEmpty_Invalid_ShouldFail()
        {
            var person = new TestPerson { Name = "test" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).IsEmpty();

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must be empty", result.Messages.First());
        }

        [Fact]
        public void IsNotEmpty_Valid_ShouldPass()
        {
            var person = new TestPerson { Name = "test" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).IsNotEmpty();

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void IsNotEmpty_Invalid_ShouldFail()
        {
            var person = new TestPerson { Name = "" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).IsNotEmpty();

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must not be empty", result.Messages.First());
        }

        [Fact]
        public void IsBlank_Valid_ShouldPass()
        {
            var person = new TestPerson { Name = "   " };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).IsBlank();

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void IsBlank_Invalid_ShouldFail()
        {
            var person = new TestPerson { Name = "test" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).IsBlank();

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must be blank", result.Messages.First());
        }

        [Fact]
        public void IsNotBlank_Valid_ShouldPass()
        {
            var person = new TestPerson { Name = "test" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).IsNotBlank();

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void IsNotBlank_Invalid_ShouldFail()
        {
            var person = new TestPerson { Name = "   " };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).IsNotBlank();

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must not be blank", result.Messages.First());
        }

        [Fact]
        public void IsTrimmed_Valid_ShouldPass()
        {
            var person = new TestPerson { Name = "test" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).IsTrimmed();

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void IsTrimmed_Invalid_ShouldFail()
        {
            var person = new TestPerson { Name = " test " };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).IsTrimmed();

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must be trimmed", result.Messages.First());
        }

        [Fact]
        public void ContainsIgnoreCase_Valid_ShouldPass()
        {
            var person = new TestPerson { Name = "Hello World" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).ContainsIgnoreCase("HELLO");

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void ContainsIgnoreCase_Invalid_ShouldFail()
        {
            var person = new TestPerson { Name = "Hello World" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).ContainsIgnoreCase("test");

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must contain 'test' (case-insensitive)", result.Messages.First());
        }

        [Fact]
        public void IsUpperCase_Valid_ShouldPass()
        {
            var person = new TestPerson { Name = "HELLO" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).IsUpperCase();

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void IsUpperCase_Invalid_ShouldFail()
        {
            var person = new TestPerson { Name = "Hello" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).IsUpperCase();

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must be uppercase", result.Messages.First());
        }

        [Fact]
        public void IsLowerCase_Valid_ShouldPass()
        {
            var person = new TestPerson { Name = "hello" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).IsLowerCase();

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void IsLowerCase_Invalid_ShouldFail()
        {
            var person = new TestPerson { Name = "Hello" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).IsLowerCase();

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must be lowercase", result.Messages.First());
        }

        [Fact]
        public void IsTitleCase_Valid_ShouldPass()
        {
            var person = new TestPerson { Name = "Hello World" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).IsTitleCase();

            var result = validator.Validate(person);
            Assert.True(result.IsValid);
        }

        [Fact]
        public void IsTitleCase_Invalid_ShouldFail()
        {
            var person = new TestPerson { Name = "hello world" };
            var validator = new TestStringValidator();
            validator.RulesFor(x => x.Name).IsTitleCase();

            var result = validator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains("must be in title case", result.Messages.First());
        }
    }
}