namespace ValidationLib
{
    internal interface IRuleBuilder
    {
        void Run(object target, Action<string, string> onFailure);
    }
}
