using System.Text.RegularExpressions;

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

        public StringRuleBuilder StartsWith(string prefix)
        {
            _steps.Add(() =>
            {
                if (!_value?.StartsWith(prefix) ?? true)
                    Failure($"'{_propertyName}': must start with '{prefix}', actual: '{_value ?? string.Empty}'");
            });
            return this;
        }

        public StringRuleBuilder EndsWith(string suffix)
        {
            _steps.Add(() =>
            {
                if (!_value?.EndsWith(suffix) ?? true)
                    Failure($"'{_propertyName}': must end with '{suffix}', actual: '{_value ?? string.Empty}'");
            });
            return this;
        }

        public StringRuleBuilder NotContains(string value)
        {
            _steps.Add(() =>
            {
                if (_value?.Contains(value) ?? false)
                    Failure($"'{_propertyName}': must not contain '{value}', actual: '{_value}'");
            });
            return this;
        }

        public StringRuleBuilder IsNumeric()
        {
            _steps.Add(() =>
            {
                if (!string.IsNullOrEmpty(_value) && !_value.All(char.IsDigit))
                    Failure($"'{_propertyName}': must contain only numeric characters, actual: '{_value}'");
            });
            return this;
        }

        public StringRuleBuilder IsAlphabetic()
        {
            _steps.Add(() =>
            {
                if (!string.IsNullOrEmpty(_value) && !_value.All(char.IsLetter))
                    Failure($"'{_propertyName}': must contain only alphabetic characters, actual: '{_value}'");
            });
            return this;
        }

        public StringRuleBuilder IsAlphaNumeric()
        {
            _steps.Add(() =>
            {
                if (!string.IsNullOrEmpty(_value) && !_value.All(char.IsLetterOrDigit))
                    Failure($"'{_propertyName}': must contain only alphanumeric characters, actual: '{_value}'");
            });
            return this;
        }

        public StringRuleBuilder IsOneOf(params string[] validValues)
        {
            _steps.Add(() =>
            {
                if (!validValues.Contains(_value))
                    Failure($"'{_propertyName}': must be one of [{string.Join(", ", validValues.Select(v => $"'{v}'"))}], actual: '{_value ?? string.Empty}'");
            });
            return this;
        }

        public StringRuleBuilder IsNotOneOf(params string[] invalidValues)
        {
            _steps.Add(() =>
            {
                if (invalidValues.Contains(_value))
                    Failure($"'{_propertyName}': must not be one of [{string.Join(", ", invalidValues.Select(v => $"'{v}'"))}], actual: '{_value ?? string.Empty}'");
            });
            return this;
        }

        public StringRuleBuilder MatchesRegex(string pattern)
        {
            _steps.Add(() =>
            {
                if (!string.IsNullOrEmpty(_value) && !Regex.IsMatch(_value, pattern))
                    Failure($"'{_propertyName}': must match pattern '{pattern}', actual: '{_value}'");
            });
            return this;
        }

        public StringRuleBuilder MatchesRegex(string pattern, RegexOptions options)
        {
            _steps.Add(() =>
            {
                if (!string.IsNullOrEmpty(_value) && !Regex.IsMatch(_value, pattern, options))
                    Failure($"'{_propertyName}': must match pattern '{pattern}', actual: '{_value}'");
            });
            return this;
        }

        public StringRuleBuilder Must(Func<string, bool> predicate)
        {
            _steps.Add(() =>
            {
                if (!predicate(_value ?? string.Empty))
                    Failure($"'{_propertyName}': {_currentMessage}, actual: '{_value ?? string.Empty}'");
            });
            return this;
        }

        public StringRuleBuilder IsEmpty()
        {
            _steps.Add(() =>
            {
                if (_value != string.Empty)
                    Failure($"'{_propertyName}': must be empty, actual: '{_value ?? "null"}'");
            });
            return this;
        }

        public StringRuleBuilder IsNotEmpty()
        {
            _steps.Add(() =>
            {
                if (_value == string.Empty)
                    Failure($"'{_propertyName}': must not be empty");
            });
            return this;
        }

        public StringRuleBuilder IsBlank()
        {
            _steps.Add(() =>
            {
                if (!string.IsNullOrWhiteSpace(_value))
                    Failure($"'{_propertyName}': must be blank (null, empty, or whitespace), actual: '{_value}'");
            });
            return this;
        }

        public StringRuleBuilder IsNotBlank()
        {
            _steps.Add(() =>
            {
                if (string.IsNullOrWhiteSpace(_value))
                    Failure($"'{_propertyName}': must not be blank (null, empty, or whitespace)");
            });
            return this;
        }

        public StringRuleBuilder HasNoLeadingWhitespace()
        {
            _steps.Add(() =>
            {
                if (!string.IsNullOrEmpty(_value) && _value.Length > 0 && char.IsWhiteSpace(_value[0]))
                    Failure($"'{_propertyName}': must not have leading whitespace, actual: '{_value}'");
            });
            return this;
        }

        public StringRuleBuilder HasNoTrailingWhitespace()
        {
            _steps.Add(() =>
            {
                if (!string.IsNullOrEmpty(_value) && _value.Length > 0 && char.IsWhiteSpace(_value[^1]))
                    Failure($"'{_propertyName}': must not have trailing whitespace, actual: '{_value}'");
            });
            return this;
        }

        public StringRuleBuilder IsTrimmed()
        {
            _steps.Add(() =>
            {
                if (_value != null && _value != _value.Trim())
                    Failure($"'{_propertyName}': must be trimmed (no leading or trailing whitespace), actual: '{_value}'");
            });
            return this;
        }

        public StringRuleBuilder ContainsIgnoreCase(string contains)
        {
            _steps.Add(() =>
            {
                if (!_value?.Contains(contains, StringComparison.OrdinalIgnoreCase) ?? true)
                    Failure($"'{_propertyName}': must contain '{contains}' (case-insensitive), actual: '{_value ?? string.Empty}'");
            });
            return this;
        }

        public StringRuleBuilder StartsWithIgnoreCase(string prefix)
        {
            _steps.Add(() =>
            {
                if (!_value?.StartsWith(prefix, StringComparison.OrdinalIgnoreCase) ?? true)
                    Failure($"'{_propertyName}': must start with '{prefix}' (case-insensitive), actual: '{_value ?? string.Empty}'");
            });
            return this;
        }

        public StringRuleBuilder EndsWithIgnoreCase(string suffix)
        {
            _steps.Add(() =>
            {
                if (!_value?.EndsWith(suffix, StringComparison.OrdinalIgnoreCase) ?? true)
                    Failure($"'{_propertyName}': must end with '{suffix}' (case-insensitive), actual: '{_value ?? string.Empty}'");
            });
            return this;
        }

        public StringRuleBuilder EqualsIgnoreCase(string expected)
        {
            _steps.Add(() =>
            {
                if (!string.Equals(_value, expected, StringComparison.OrdinalIgnoreCase))
                    Failure($"'{_propertyName}': must equal '{expected}' (case-insensitive), actual: '{_value ?? string.Empty}'");
            });
            return this;
        }

        public StringRuleBuilder IsOneOfIgnoreCase(params string[] validValues)
        {
            _steps.Add(() =>
            {
                if (!validValues.Any(v => string.Equals(_value, v, StringComparison.OrdinalIgnoreCase)))
                    Failure($"'{_propertyName}': must be one of [{string.Join(", ", validValues.Select(v => $"'{v}'"))}] (case-insensitive), actual: '{_value ?? string.Empty}'");
            });
            return this;
        }

        public StringRuleBuilder IsNotOneOfIgnoreCase(params string[] invalidValues)
        {
            _steps.Add(() =>
            {
                if (invalidValues.Any(v => string.Equals(_value, v, StringComparison.OrdinalIgnoreCase)))
                    Failure($"'{_propertyName}': must not be one of [{string.Join(", ", invalidValues.Select(v => $"'{v}'"))}] (case-insensitive), actual: '{_value ?? string.Empty}'");
            });
            return this;
        }

        public StringRuleBuilder IsUpperCase()
        {
            _steps.Add(() =>
            {
                if (!string.IsNullOrEmpty(_value) && _value != _value.ToUpperInvariant())
                    Failure($"'{_propertyName}': must be uppercase, actual: '{_value}'");
            });
            return this;
        }

        public StringRuleBuilder IsLowerCase()
        {
            _steps.Add(() =>
            {
                if (!string.IsNullOrEmpty(_value) && _value != _value.ToLowerInvariant())
                    Failure($"'{_propertyName}': must be lowercase, actual: '{_value}'");
            });
            return this;
        }

        public StringRuleBuilder IsTitleCase()
        {
            _steps.Add(() =>
            {
                if (!string.IsNullOrEmpty(_value))
                {
                    var titleCase = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(_value.ToLowerInvariant());
                    if (_value != titleCase)
                        Failure($"'{_propertyName}': must be in title case, actual: '{_value}'");
                }
            });
            return this;
        }
    }
}
