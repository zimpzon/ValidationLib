using ValidationLib;

namespace ValidationLibTest
{
    public class TestPersonValidator : ValidatorBase<TestPerson>
    {
        public TestPersonValidator(TestPerson testPerson)
		{
			Validate(testPerson.Name).
				WithMessage("name must not be empty").
				NotNullOrWhitespace().
				WithMessage("name must be between 2 and 20 characters").
				LengthBetween(2, 20).
				WithMessage("name must contain 'John'").
				Contains("John");

			Validate(testPerson.Age).
				WithMessage("age must be between 20 and 200").
				Between(20, 200);
		}
	}

    public class TestPerson
    {
        public int Id { get; set; } = 1;
        public string Name { get; set; } = "Test Person";
        public int Age { get; set; } = 25;
    }
}