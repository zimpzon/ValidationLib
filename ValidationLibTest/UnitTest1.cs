namespace ValidationLibTest
{
    public class UnitTest1
    {
        // Simple synchronous validator test
        
        [Fact]
        public void IsValid()
        {
            var person = new TestPerson() { Name = "John Doe", Age = 25 };

            var personValidator = new TestPersonValidator();
            var result = personValidator.Validate(person);
            Assert.True(result.IsValid);
            Assert.Empty(result.Messages);
        }
    
        [Fact]
        public void InvalidAge()
        {
            var person = new TestPerson() { Name = "John Doe", Age = 15 };

            var personValidator = new TestPersonValidator();
            var result = personValidator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Single(result.Messages);
            Assert.Contains(result.Messages, m => m.Contains("must be between"));
        }

        [Fact]
        public void InvalidAgeAndName()
        {
            var person = new TestPerson() { Name = "This Name Is Too Long", Age = 15 };

            var personValidator = new TestPersonValidator();
            var result = personValidator.Validate(person);
            Assert.False(result.IsValid);
            Assert.Contains(result.Messages, m => m.Contains("must be between 2 and"));
            Assert.Contains(result.Messages, m => m.Contains("must contain"));
            Assert.Contains(result.Messages, m => m.Contains("must be between 20 and"));
        }
    }
}
