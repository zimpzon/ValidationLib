namespace ValidationLib
{
    public class StringRuleBuilder
    {
		private readonly string _value;
		private readonly Action<string> _setCurrentMessage;
		private readonly Action _addCurrentMessage;

		internal StringRuleBuilder(string value, Action<string> setCurrentMessage, Action addCurrentMessage)
        {
            _value = value;
            _setCurrentMessage = setCurrentMessage;
            _addCurrentMessage = addCurrentMessage;
        }

		public StringRuleBuilder WithMessage(string message)
        {
            _setCurrentMessage(message);
            return this;
        }

        public StringRuleBuilder NotNullOrWhitespace()
        {
            if (string.IsNullOrWhiteSpace(_value)) _addCurrentMessage();
            return this;
        }

		public StringRuleBuilder Contains(string contains)
		{
			if (!_value?.Contains(contains) ?? false) _addCurrentMessage();
			return this;
		}

		public StringRuleBuilder LengthMax(int maxLength)
        {
            if (_value?.Length > maxLength) _addCurrentMessage();
			return this;
        }

		public StringRuleBuilder LengthMin(int minLength)
		{
			if (_value?.Length < minLength) _addCurrentMessage();
			return this;
		}

		public StringRuleBuilder LengthBetween(int minLength, int maxLength)
		{
            LengthMax(maxLength);
            LengthMin(minLength);
			return this;
		}

		public StringRuleBuilder IsTrue(Func<bool> func)
        {
            if (!func()) _addCurrentMessage();
			return this;
        }
    }
}
