using System.Linq.Expressions;
using System.Numerics;

namespace ValidationLib
{
    public class ValidatorBase<T> where T : class
    {
        private readonly List<string> _messages = [];
        private readonly List<IRuleBuilder> _rules = [];

        public ValidationResult Validate(T target)
        {
            _messages.Clear();

            foreach (var rule in _rules)
            {
                ValidateRule(rule, target);
            }

            var result = new ValidationResult()
            {
                IsValid = _messages.Count == 0,
                Messages = _messages,
            };

            return result;
        }

        private void ValidateRule(IRuleBuilder ruleBuilder, T target)
        {
            void OnFailure(string defaultMesage, string overriddenMessage)
            {
                string message = string.IsNullOrWhiteSpace(overriddenMessage) ?
                    defaultMesage :
                    overriddenMessage;

                if (string.IsNullOrWhiteSpace(message))
                    return;

                if (_messages.Exists(m => m == message))
                    return;

                _messages.Add(message);
            }

            ruleBuilder.Run(target, OnFailure);
        }

        public StringRuleBuilder RulesFor(Expression<Func<T, string>> property)
        {
            if (property.Body is not MemberExpression member)
                throw new ArgumentException($"{nameof(RulesFor)} must be called like this: x => x.SomeProperty");

            var builder = new StringRuleBuilder(member.Member.Name);
            _rules.Add(builder);
            return builder;
        }

        public NumberRuleBuilder<TNumber> RulesFor<TNumber>(Expression<Func<T, TNumber>> property) where TNumber : INumber<TNumber>
        {
            if (property.Body is not MemberExpression member)
                throw new ArgumentException($"{nameof(RulesFor)} must be called like this: x => x.SomeProperty");

            var builder = new NumberRuleBuilder<TNumber>(member.Member.Name);
            _rules.Add(builder);
            return builder;
        }

        public CustomRuleBuilder<T> WithCustomRules()
        {
            var builder = new CustomRuleBuilder<T>();
            _rules.Add(builder);
            return builder;
        }
    }
}
