namespace GcdCalculator.Models
{
    /// <summary>
    /// Represents a view model of the result to calculates GCD.
    /// </summary>
    public class ResultViewModel
    {
        /// <summary>
        /// Gets or sets the number of integers for which it is necessary to find a common divisor
        /// </summary>
        public string? CountNumbers { get; set; }

        /// <summary>
        /// Gets or sets the first integer
        /// </summary>
        public int? First { get; set; }

        /// <summary>
        /// Gets or sets the second integer
        /// </summary>
        public int? Second { get; set; }

        /// <summary>
        /// Gets or sets the third integer
        /// </summary>
        public int? Third { get; set; }

        /// <summary>
        /// Gets or sets other integers
        /// </summary>
        public string? NumbersOther { get; set; }

        /// <summary>
        /// Gets or sets the algorithm by which it is necessary to perform the calculation
        /// </summary>
        public string? Algorithm { get; set; }

        /// <summary>
        /// Gets or sets the indication of the need to calculate the execution time of the algorithm
        /// </summary>
        public string? Extended { get; set; }

        /// <summary>
        /// Gets or sets the error value
        /// </summary>
        public string? Error { get; set; }

        /// <summary>
        /// Gets or sets the calculated result
        /// </summary>
        public int? Result { get; set; }

        /// <summary>
        /// Gets or sets the calculation time in milliseconds
        /// </summary>
        public long? CalculationTime { get; set; }
    }
}
