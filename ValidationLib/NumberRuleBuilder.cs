using System.Numerics;

namespace ValidationLib
{
    public class NumberRuleBuilder<T> : IRuleBuilder where T : INumber<T>
    {
        private readonly string _propertyName;
        private string _currentMessage = string.Empty;
        private readonly List<Action> _steps = [];

        private Action<string, string> _onFailure = (_, _) => { };
        private T _value = T.Zero;

        internal NumberRuleBuilder(string propertyName)
        {
            _propertyName = propertyName;
        }

        public void Run(object target, Action<string, string> onFailure)
        {
            _value = (T)Util.GetPropertyValueObject(_propertyName, target);
            _onFailure = onFailure;
            _currentMessage = string.Empty;

            foreach (var step in _steps)
            {
                step();
            }
        }

        private void Failure(string defaultMessage)
        {
            _onFailure(defaultMessage, _currentMessage);
        }

        public NumberRuleBuilder<T> WithMessage(string message)
        {
            _steps.Add(() => _currentMessage = message);
            return this;
        }

        public NumberRuleBuilder<T> WithDefaultMessage()
        {
            _steps.Add(() => _currentMessage = string.Empty);
            return this;
        }

        public NumberRuleBuilder<T> Between(T min, T max)
        {
            _steps.Add(() =>
            {
                if (_value < min || _value > max)
                    Failure($"'{_propertyName}': must be between {min} and {max}, actual: {_value}");
            });
            return this;
        }

        public NumberRuleBuilder<T> GreaterThan(T value)
        {
            _steps.Add(() =>
            {
                if (_value <= value)
                    Failure($"'{_propertyName}': must be greater than {value}, actual: {_value}");
            });
            return this;
        }

        public NumberRuleBuilder<T> GreaterThanOrEqual(T value)
        {
            _steps.Add(() =>
            {
                if (_value < value)
                    Failure($"'{_propertyName}': must be greater than or equal to {value}, actual: {_value}");
            });
            return this;
        }

        public NumberRuleBuilder<T> LessThan(T value)
        {
            _steps.Add(() =>
            {
                if (_value >= value)
                    Failure($"'{_propertyName}': must be less than {value}, actual: {_value}");
            });
            return this;
        }

        public NumberRuleBuilder<T> LessThanOrEqual(T value)
        {
            _steps.Add(() =>
            {
                if (_value > value)
                    Failure($"'{_propertyName}': must be less than or equal to {value}, actual: {_value}");
            });
            return this;
        }

        public NumberRuleBuilder<T> NotEqual(T value)
        {
            _steps.Add(() =>
            {
                if (_value == value)
                    Failure($"'{_propertyName}': must not equal {value}, actual: {_value}");
            });
            return this;
        }

        public NumberRuleBuilder<T> IsPositive()
        {
            _steps.Add(() =>
            {
                if (_value <= T.Zero)
                    Failure($"'{_propertyName}': must be positive, actual: {_value}");
            });
            return this;
        }

        public NumberRuleBuilder<T> IsNegative()
        {
            _steps.Add(() =>
            {
                if (_value >= T.Zero)
                    Failure($"'{_propertyName}': must be negative, actual: {_value}");
            });
            return this;
        }

        public NumberRuleBuilder<T> IsZero()
        {
            _steps.Add(() =>
            {
                if (_value != T.Zero)
                    Failure($"'{_propertyName}': must be zero, actual: {_value}");
            });
            return this;
        }

        public NumberRuleBuilder<T> Must(Func<T, bool> predicate)
        {
            _steps.Add(() =>
            {
                if (!predicate(_value))
                    Failure($"'{_propertyName}': {_currentMessage}, actual: {_value}");
            });
            return this;
        }

        public NumberRuleBuilder<T> IsOneOf(params T[] validValues)
        {
            _steps.Add(() =>
            {
                if (!validValues.Contains(_value))
                    Failure($"'{_propertyName}': must be one of [{string.Join(", ", validValues)}], actual: {_value}");
            });
            return this;
        }

        public NumberRuleBuilder<T> IsNotOneOf(params T[] invalidValues)
        {
            _steps.Add(() =>
            {
                if (invalidValues.Contains(_value))
                    Failure($"'{_propertyName}': must not be one of [{string.Join(", ", invalidValues)}], actual: {_value}");
            });
            return this;
        }

        public NumberRuleBuilder<T> IsInRange(T min, T max, bool inclusive = true)
        {
            _steps.Add(() =>
            {
                if (inclusive)
                {
                    if (_value < min || _value > max)
                        Failure($"'{_propertyName}': must be in range [{min}, {max}] (inclusive), actual: {_value}");
                }
                else
                {
                    if (_value <= min || _value >= max)
                        Failure($"'{_propertyName}': must be in range ({min}, {max}) (exclusive), actual: {_value}");
                }
            });
            return this;
        }

        public NumberRuleBuilder<T> IsOutsideRange(T min, T max)
        {
            _steps.Add(() =>
            {
                if (_value >= min && _value <= max)
                    Failure($"'{_propertyName}': must be outside range [{min}, {max}], actual: {_value}");
            });
            return this;
        }

        public NumberRuleBuilder<T> HasPrecision(int maxDecimalPlaces)
        {
            _steps.Add(() =>
            {
                if (_value != null)
                {
                    var valueStr = _value.ToString("F15", System.Globalization.CultureInfo.InvariantCulture);
                    valueStr = valueStr.TrimEnd('0').TrimEnd('.');
                    var decimalIndex = valueStr.IndexOf('.');
                    if (decimalIndex >= 0)
                    {
                        var decimalPlaces = valueStr.Length - decimalIndex - 1;
                        if (decimalPlaces > maxDecimalPlaces)
                            Failure($"'{_propertyName}': must have at most {maxDecimalPlaces} decimal places, actual: {decimalPlaces}");
                    }
                }
            });
            return this;
        }

        public NumberRuleBuilder<T> IsMultipleOf(T divisor)
        {
            _steps.Add(() =>
            {
                if (_value % divisor != T.Zero)
                    Failure($"'{_propertyName}': must be a multiple of {divisor}, actual: {_value}");
            });
            return this;
        }

        public NumberRuleBuilder<T> IsNotMultipleOf(T divisor)
        {
            _steps.Add(() =>
            {
                if (_value % divisor == T.Zero)
                    Failure($"'{_propertyName}': must not be a multiple of {divisor}, actual: {_value}");
            });
            return this;
        }

        public NumberRuleBuilder<T> IsFinite()
        {
            _steps.Add(() =>
            {
                if (typeof(T) == typeof(double) || typeof(T) == typeof(float) || typeof(T) == typeof(decimal))
                {
                    if (_value is double d && !double.IsFinite(d))
                        Failure($"'{_propertyName}': must be finite (not infinite or NaN), actual: {_value}");
                    else if (_value is float f && !float.IsFinite(f))
                        Failure($"'{_propertyName}': must be finite (not infinite or NaN), actual: {_value}");
                }
            });
            return this;
        }

        public NumberRuleBuilder<T> IsNormal()
        {
            _steps.Add(() =>
            {
                if (typeof(T) == typeof(double) || typeof(T) == typeof(float))
                {
                    if (_value is double d && !double.IsNormal(d))
                        Failure($"'{_propertyName}': must be a normal number, actual: {_value}");
                    else if (_value is float f && !float.IsNormal(f))
                        Failure($"'{_propertyName}': must be a normal number, actual: {_value}");
                }
            });
            return this;
        }
    }
}
