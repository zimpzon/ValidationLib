namespace ValidationLib
{
    public class ValidatorBase<T> : IValidator where T : class
    {
        private readonly List<string> _messages = [];
        private readonly T _target;
        private List<Rule> _rules = [];

        public ValidatorBase(T target)
        {
            ArgumentNullException.ThrowIfNull(nameof(target));
            _target = target;
        }

        /// <summary>
        /// Per rule: Left to right. When fail add message.
        /// Default message needs property name.
        /// </summary>
        public bool Validate()
        {
            if (_stringRulesBuilders.Count == 0)
                return true;

            _stringRulesBuilders.l
            _stringRulesBuilder.Run();
            return _messages.Count == 0;
        }

        public IEnumerable<string> Messages()
        {
            return _messages;
        }

        public async Task<bool> ValidateAsync()
        {
            return await Task.FromResult(false);
        }

        /// <summary>
        /// Make a Rule instance per Rule() call.
        /// Builder ctor calls back to Rule class, adding to a list.
        /// Validate() runs first to last.
        /// We NEED dependency injection for some rules. So they must be registered as services.
        /// How to call?
        ///   PersonValidator(ValidatorRulePersonIsCustomer rule1)
        ///     Rule(...).IsTrue(rule1());
        ///  note registering concrete classes: cannot be mocked, need interface/base (absgract?) for that.
        /// </summary>
        internal protected StringRuleBuilder Rule(Func<T, string> valueGetter)
        {
            string value = valueGetter(_target);
            string propertyName = 
            return new StringRuleBuilder( value, currentMessage: string.Empty, _messages);
        }
    }
}
