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
    }
}
