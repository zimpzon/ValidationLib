using System.Numerics;

namespace ValidationLib
{
    public class ValidatorBase<T> where T : class
    {
        private readonly List<string> _messages = [];
		private string _currentMessage = string.Empty;

		public ValidatorBase()
        {
        }

		public async Task<ValidationResult> Validate()
		{
			return await Task.FromResult(new ValidationResult());
		}

		public bool IsValid
			=> _messages.Count == 0;

		public IEnumerable<string> Messages()
        {
            return _messages;
        }

		internal void AddCurrentMessage()
		{
			if (string.IsNullOrWhiteSpace(_currentMessage))
				return;

			if (_messages.Exists(m => m == _currentMessage))
				return;

			_messages.Add(_currentMessage);
		}

		internal void SetCurrentMessage(string message)
		{
			_currentMessage = message;
		}

		public StringRuleBuilder Validate(string value)
        {
            return new StringRuleBuilder(value, SetCurrentMessage, AddCurrentMessage);
		}

		public NumberRuleBuilder<TNumber> Validate<TNumber>(TNumber number) where TNumber : INumber<TNumber>
		{
			return new NumberRuleBuilder<TNumber>(number, SetCurrentMessage, AddCurrentMessage);
		}
	}
}
