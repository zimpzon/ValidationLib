using System.Numerics;

namespace ValidationLib
{
    public class NumberRuleBuilder<T> : IRuleBuilder where T : INumber<T>
	{
        private readonly T _value;
		private readonly Action<string> _setCurrentMessage;
		private readonly Action _addCurrentMessage;
		private readonly List<Action> _steps = [];
        public IEnumerable<Action> Steps => _steps;

		internal NumberRuleBuilder(T value, Action<string> setCurrentMessage, Action addCurrentMessage)
        {
            _value = value;
            _setCurrentMessage = setCurrentMessage;
            _addCurrentMessage = addCurrentMessage;
        }

        public NumberRuleBuilder<T> WithMessage(string message)
        {
            _steps.Add(() => _setCurrentMessage(message));
            return this;
        }

		public NumberRuleBuilder<T> Between(T min, T max)
		{
            _steps.Add(() =>
            {
                if (_value < min || _value > max)
                    _addCurrentMessage();
            });
			return this;
		}
    }
}
