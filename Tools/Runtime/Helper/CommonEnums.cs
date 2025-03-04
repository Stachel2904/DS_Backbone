namespace DivineSkies.Tools.Helper
{
    public enum ExtendedBoolean
    {
        True,
        Neutral,
        False
    }

    public static class ExtendedBooleanExtensions
    {
        public static bool? ToBool(this ExtendedBoolean source) => source switch
        {
            ExtendedBoolean.True => true,
            ExtendedBoolean.False => false,
            _ => null
        };
    }
}