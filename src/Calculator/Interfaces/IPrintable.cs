namespace RamanM.Properti.Calculator.Interfaces
{
    /// <summary>
    /// Defines the methods for printing.
    /// </summary>
    public interface IPrintable
    {
        /// <summary>
        /// Prints the object to a <see cref="string"/>.
        /// </summary>
        /// <returns>A <see cref="String"/> representation of the object.</returns>
        string Print();

        /// <summary>
        /// Prints the object using natural language sentence.
        /// </summary>
        /// <returns>A <see cref="String"/> representation of the object's sentence.</returns>
        string PrintSentence();
    }
}
