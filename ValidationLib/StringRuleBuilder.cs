namespace ValidationLib
{
    public class StringRuleBuilder
    {
        private readonly List<string> _messages;
        private string _latestSetMessage;
        private string _propertyName = string.Empty;
        private readonly string _value;

        internal StringRuleBuilder(string value, string currentMessage, List<string> messages)
        {
            ArgumentNullException.ThrowIfNull(nameof(messages));
            _value = value;
            _latestSetMessage = currentMessage;
            _messages = messages;
        }

        private void AddMessage()
        {
            string message = GetCurrentMessage();
            if (string.IsNullOrWhiteSpace(message))
                return;

            if (_messages.Exists(m => m == message))
                return;

            _messages.Add(_latestSetMessage);
        }

        private void SetCurrentMessage(string message)
        {
            _latestSetMessage = message;
        }

        private string GetCurrentMessage()
        {
            // use default message in empty, remember propertyName.
            return _latestSetMessage ?? string.Empty;
        }

        public StringRuleBuilder WithPropertyName(string propertyName)
        {
            _propertyName = propertyName;
            return this;
        }

        public StringRuleBuilder WithMessage(string message)
        {
            SetCurrentMessage(message);
            return this;
        }

        public StringRuleBuilder NotNullWhitespace()
        {
            if (string.IsNullOrWhiteSpace(_value))
                AddMessage();

            return this;
        }

        public StringRuleBuilder MaxLength(int maxLength)
        {
            if (_value.Length > maxLength)
                AddMessage();

            return this;
        }

        public StringRuleBuilder IsTrue(Func<bool> func)
        {
            if (!func())
                AddMessage();

            return this;
        }
    }
}
