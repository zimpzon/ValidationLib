using ValidationLib;

namespace ValidationLibTest
{
    public class TestPersonValidator(TestPerson testPerson) : ValidatorBase<TestPerson>(testPerson)
    {
    }

    public class TestPerson
    {
        public int Id { get; set; } = 1;
        public string Name { get; set; } = "Test Person";
        public int Age { get; set; } = 25;
    }
}