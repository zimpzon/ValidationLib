namespace ValidationLib
{
    public class StringRuleBuilder : IRuleBuilder
    {
		private readonly string _value;
		private readonly Action<string> _setCurrentMessage;
		private readonly Action _addCurrentMessage;
        private readonly List<Action> _steps = [];
		public IEnumerable<Action> Steps => _steps;

		internal StringRuleBuilder(string value, Action<string> setCurrentMessage, Action addCurrentMessage)
        {
            _value = value;
            _setCurrentMessage = setCurrentMessage;
            _addCurrentMessage = addCurrentMessage;
        }

        public StringRuleBuilder WithMessage(string message)
        {
            _steps.Add(() =>_setCurrentMessage(message));
            return this;
        }

        public StringRuleBuilder NotNullOrWhitespace()
        {
            _steps.Add(() =>
            {
                if (string.IsNullOrWhiteSpace(_value))
                    _addCurrentMessage();
            });
            return this;
        }

		public StringRuleBuilder Contains(string contains)
		{
			_steps.Add(() =>
			{
				if (!_value?.Contains(contains) ?? true)
					_addCurrentMessage();
			});
			return this;
		}

		public StringRuleBuilder LengthMax(int maxLength)
		{
			_steps.Add(() =>
			{
				if (_value?.Length > maxLength)
					_addCurrentMessage();
			});
			return this;
		}

		public StringRuleBuilder LengthMin(int minLength)
		{
			_steps.Add(() =>
			{
				if (_value?.Length < minLength)
					_addCurrentMessage();
			});
			return this;
		}

		public StringRuleBuilder LengthBetween(int minLength, int maxLength)
		{
			return LengthMin(minLength).LengthMax(maxLength);
		}

		public StringRuleBuilder IsTrue(Func<bool> func)
		{
			_steps.Add(() =>
			{
				if (!func())
					_addCurrentMessage();
			});
			return this;
		}
	}
}
