using System.Numerics;

namespace ValidationLib
{
    public class NumberRuleBuilder<T> where T : INumber<T>
	{
        private readonly T _value;
		private readonly Action<string> _setCurrentMessage;
		private readonly Action _addCurrentMessage;

		internal NumberRuleBuilder(T value, Action<string> setCurrentMessage, Action addCurrentMessage)
        {
            _value = value;
            _setCurrentMessage = setCurrentMessage;
            _addCurrentMessage = addCurrentMessage;
        }

        public NumberRuleBuilder<T> WithMessage(string message)
        {
            _setCurrentMessage(message);
            return this;
        }

		public NumberRuleBuilder<T> Between(T min, T max)
		{
			if (_value < min || _value > max) _addCurrentMessage();
			return this;
		}
    }
}
