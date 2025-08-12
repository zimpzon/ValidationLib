using ValidationLib;

namespace ValidationLibTest
{
    public class TestPersonValidator : ValidatorBase<TestPerson>
    {
        public TestPersonValidator()
        {
            RulesFor(person => person.Name).
                WithMessage("name must not be empty").
                NotNullOrWhitespace().
                WithMessage("name must be between 2 and 20 characters").
                LengthBetween(2, 20).
                WithDefaultMessage().
                Contains("John");

            RulesFor(person => person.Age).Between(20, 200);

            WithCustomRules().
                Satisfies(IntPropertyMustMatchStringProperty).
                Satisfies(person => (success: true, errorMessage: null!)); // Chaining example
        }

        static (bool success, string errorMessage) IntPropertyMustMatchStringProperty(TestPerson person)
        {
            bool success = person.CustomStringValue == person.CustomIntValue.ToString();

            string errorMessage = success ?
                null! : $"Custom rule failed: {nameof(TestPerson.CustomIntValue)} must match {nameof(TestPerson.CustomStringValue)}";

            return (success, errorMessage);
        }
    }

    public class TestPerson
    {
        public int Id { get; set; } = 1;
        public string Name { get; set; } = "Test Person";
        public int Age { get; set; } = 25;
        public int CustomIntValue { get; set; } = 1234;
        public string CustomStringValue { get; set; } = "1234";
    }
}