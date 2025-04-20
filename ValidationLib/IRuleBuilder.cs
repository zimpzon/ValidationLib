namespace ValidationLib
{
	internal interface IRuleBuilder
	{
		IEnumerable<Action> Steps { get; }
	}
}
