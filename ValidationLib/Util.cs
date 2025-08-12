namespace ValidationLib
{
    internal static class Util
    {
        public static object GetPropertyValueObject(string propertyName, object obj)
        {
            return obj.GetType().GetProperty(propertyName)?.GetValue(obj, null) ??
                throw new ArgumentException($"property {propertyName} not found");
        }
    }
}
