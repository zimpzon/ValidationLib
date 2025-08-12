namespace ValidationLib
{
    public class StringRuleBuilder : IRuleBuilder
    {
        private readonly string _propertyName;
        private string _currentMessage = string.Empty;
        private readonly List<Action> _steps = [];

        private Action<string, string> _onFailure = (_, _) => { };
        private string _value = string.Empty;

        internal StringRuleBuilder(string propertyName)
        {
            _propertyName = propertyName;
        }

        public void Run(object target, Action<string, string> onFailure)
        {
            _value = (string)Util.GetPropertyValueObject(_propertyName, target);
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

        public StringRuleBuilder WithMessage(string message)
        {
            _steps.Add(() => _currentMessage = message);
            return this;
        }

        public StringRuleBuilder WithDefaultMessage()
        {
            _steps.Add(() => _currentMessage = string.Empty);
            return this;
        }

        public StringRuleBuilder NotNullOrWhitespace()
        {
            _steps.Add(() =>
            {
                if (string.IsNullOrWhiteSpace(_value))
                    Failure($"'{_propertyName}': must not be null or whitespace");
            });
            return this;
        }

        public StringRuleBuilder Contains(string contains)
        {
            _steps.Add(() =>
            {
                if (!_value?.Contains(contains) ?? true)
                    Failure($"'{_propertyName}': must contain the string '{contains}', actual: '{_value ?? string.Empty}'");
            });
            return this;
        }

        public StringRuleBuilder LengthMax(int maxLength)
        {
            _steps.Add(() =>
            {
                if (_value?.Length > maxLength)
                    Failure($"'{_propertyName}': length must be at most {maxLength} characters, actual: {_value.Length}");
            });
            return this;
        }

        public StringRuleBuilder LengthMin(int minLength)
        {
            _steps.Add(() =>
            {
                if (_value?.Length < minLength)
                    Failure($"'{_propertyName}': length must be a minimum of {minLength} characters, actual: {_value.Length}");
            });
            return this;
        }

        public StringRuleBuilder LengthBetween(int minLength, int maxLength)
        {
            _steps.Add(() =>
            {
                if (_value?.Length < minLength || _value?.Length > maxLength)
                    Failure($"'{_propertyName}': length must be between {minLength} and {maxLength} characters, actual: {_value.Length}");
            });
            return this;
        }
    }
}
