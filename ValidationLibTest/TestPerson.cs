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
        }
    }

    public class TestPerson
    {
        public int Id { get; set; } = 1;
        public string Name { get; set; } = "Test Person";
        public int Age { get; set; } = 25;
    }
}