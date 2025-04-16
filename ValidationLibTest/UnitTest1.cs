using ValidationLib;

namespace ValidationLibTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var person = new TestPerson();
            var validator = new ValidatorBase<TestPerson>(person);

            validator.Rule(p => p.Name).WithMessage("hey").NotNullWhitespace();

            Assert.True(validator.Validate());
        }
    }
}
