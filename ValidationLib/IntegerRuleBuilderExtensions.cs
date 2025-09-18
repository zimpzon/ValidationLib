using System.Numerics;

namespace ValidationLib
{
    public static class IntegerRuleBuilderExtensions
    {
        public static NumberRuleBuilder<T> IsEven<T>(this NumberRuleBuilder<T> builder) where T : INumber<T>, IBinaryInteger<T>
        {
            return builder.WithMessage($"must be even").Must(value => value % T.CreateChecked(2) == T.Zero);
        }

        public static NumberRuleBuilder<T> IsOdd<T>(this NumberRuleBuilder<T> builder) where T : INumber<T>, IBinaryInteger<T>
        {
            return builder.WithMessage($"must be odd").Must(value => value % T.CreateChecked(2) != T.Zero);
        }
    }
}