namespace Task2
{
    /// <summary>
    /// Interface of class that parses string to integer.
    /// </summary>
    public interface INumberParser
    {
        /// <summary>
        /// Parse string to integer.
        /// </summary>
        /// <param name="stringValue">Some number.</param>
        /// <returns>Integer value.</returns>
        int Parse(string stringValue);
    }
}
