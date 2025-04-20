using System.Numerics;

namespace ValidationLib
{
    public class ValidatorBase<T> where T : class
    {
        private readonly List<string> _messages = [];
		private string _currentMessage = string.Empty;
		private readonly List<IRuleBuilder> _rules = [];

		public ValidationResult Validate()
		{
			_messages.Clear();

			foreach (var rule in _rules)
			{
				_currentMessage = string.Empty;
				ValidateRule(rule);
			};

			var result = new ValidationResult()
			{
				IsValid = _messages.Count == 0,
				Messages = _messages,
			};

			return result;
		}

		private static void ValidateRule(IRuleBuilder ruleBuilder)
		{
			foreach (var step in ruleBuilder.Steps)
				step();
		}

		private void ReportCurrentMessage()
		{
			if (string.IsNullOrWhiteSpace(_currentMessage))
				return;

			if (_messages.Exists(m => m == _currentMessage))
				return;

			_messages.Add(_currentMessage);
		}

		private void SetCurrentMessage(string message)
		{
			_currentMessage = message;
		}

		public StringRuleBuilder RulesFor(string value)
        {
            var builder = new StringRuleBuilder(value, SetCurrentMessage, ReportCurrentMessage);
			_rules.Add(builder);
			return builder;
		}

		public NumberRuleBuilder<TNumber> RulesFor<TNumber>(TNumber number) where TNumber : INumber<TNumber>
		{
			var builder = new NumberRuleBuilder<TNumber>(number, SetCurrentMessage, ReportCurrentMessage);
			_rules.Add(builder);
			return builder;
		}
	}
}
