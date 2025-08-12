namespace ValidationLib
{
    public class CustomRuleBuilder<T> : IRuleBuilder
    {
        private string _currentMessage = string.Empty;
        private readonly List<Action> _steps = [];

        private Action<string, string> _onFailure = (_, _) => { };
        private T _target = default!;

        internal CustomRuleBuilder()
        {
        }

        public void Run(object target, Action<string, string> onFailure)
        {
            _target = (T)target;
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

        public CustomRuleBuilder<T> WithMessage(string message)
        {
            _steps.Add(() => _currentMessage = message);
            return this;
        }

        public CustomRuleBuilder<T> WithDefaultMessage()
        {
            _steps.Add(() => _currentMessage = string.Empty);
            return this;
        }

        public CustomRuleBuilder<T> Satisfies(Func<T, (bool success, string errorMessage)> predicate)
        {
            _steps.Add(() =>
            {
                (bool success, string errorMessage) = predicate(_target);
                if (!success)
                    Failure(errorMessage ?? $"Custom validation failed");
            });
            return this;
        }
    }
}
