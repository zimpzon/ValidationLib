namespace ValidationLibTest
{
    public class UnitTest1
    {
		// Simple synchronous validator test
		
        [Fact]
        public void IsValid()
        {
            var person = new TestPerson() { Name = "John Doe", Age = 25 };

			var personValidator = new TestPersonValidator(person);
            Assert.True(personValidator.IsValid);
            Assert.Empty(personValidator.Messages());
		}
	
		[Fact]
		public void InvalidAge()
		{
			var person = new TestPerson() { Name = "John Doe", Age = 15 };

			var personValidator = new TestPersonValidator(person);
			Assert.False(personValidator.IsValid);
			Assert.Single(personValidator.Messages());
			Assert.Contains(personValidator.Messages(), m => m.Contains("age must be between"));
		}

		[Fact]
		public void InvalidAgeAndName()
		{
			var person = new TestPerson() { Name = "This Name Is Too Long", Age = 15 };

			var personValidator = new TestPersonValidator(person);
			Assert.False(personValidator.IsValid);
			Assert.Contains(personValidator.Messages(), m => m.Contains("name must be between"));
			Assert.Contains(personValidator.Messages(), m => m.Contains("name must contain"));
			Assert.Contains(personValidator.Messages(), m => m.Contains("age must be between"));
		}
	}
}
