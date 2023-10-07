using GcdCalculator.Services.Interfaces;
using System.Diagnostics;

namespace GcdCalculator.Services.GcdImplementations
{
    /// <inheritdoc/>
    internal class ExtendedAlgorithm : IAlgorithm
    {
        private readonly IAlgorithm algorithm;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedAlgorithm"/> class.
        /// </summary>
        /// <param name="algorithm">Algorithm.</param>
        /// <exception cref="ArgumentNullException">Throw when algorithm is null.</exception>
        public ExtendedAlgorithm(IAlgorithm algorithm) => this.algorithm = algorithm ?? throw new ArgumentNullException(nameof(algorithm));

        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when all numbers are 0 at the same time.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when one or two numbers are int.MinValue.</exception>
        public int Calculate(int first, int second) => this.algorithm.Calculate(first, second);

        /// <summary>
        /// Calculates GCD of three integers from [-int.MaxValue;int.MaxValue].
        /// </summary>
        /// <param name="first">First integer.</param>
        /// <param name="second">Second integer.</param>
        /// <param name="third">Third integer.</param>
        /// <returns>The GCD value.</returns>
        /// <exception cref="ArgumentException">Thrown when all numbers are 0 at the same time.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when one or more numbers are int.MinValue.</exception>
        public int Calculate(int first, int second, int third) =>
            first != 0 ? this.algorithm.Calculate(this.algorithm.Calculate(first, second), third) : this.algorithm.Calculate(second, third);

        /// <summary>
        /// Calculates the GCD of integers from [-int.MaxValue;int.MaxValue].
        /// </summary>
        /// <param name="first">First integer.</param>
        /// <param name="second">Second integer.</param>
        /// <param name="numbers">Other integers.</param>
        /// <returns>The GCD value.</returns>
        /// <exception cref="ArgumentException">Thrown when all numbers are 0 at the same time.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when one or more numbers are int.MinValue.</exception>
        public int Calculate(int first, int second, params int[] numbers)
        {
            int gcd = 0, index = 0;
            if (first != 0)
            {
                gcd = this.algorithm.Calculate(first, second);
            }
            else if (second != 0)
            {
                gcd = this.algorithm.Calculate(second, numbers[index++]);
            }

            for (; index < numbers.Length; index++)
            {
                if (numbers[index] != 0)
                {
                    gcd = this.algorithm.Calculate(gcd, numbers[index]);
                }
            }

            return gcd == 0 ? throw new ArgumentException("All numbers cannot be 0 at the same time.") : gcd;
        }

        /// <summary>
        /// Calculates GCD of two integers from [-int.MaxValue;int.MaxValue] with milliseconds time.
        /// </summary>
        /// <param name="milliseconds">Method execution time in milliseconds.</param>
        /// <param name="first">First integer.</param>
        /// <param name="second">Second integer.</param>
        /// <returns>The GCD value.</returns>
        /// <exception cref="ArgumentException">Thrown when all numbers are 0 at the same time.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when one or two numbers are int.MinValue.</exception>
        public int Calculate(out long milliseconds, int first, int second)
        {
            var stopWatch = Stopwatch.StartNew();
            var gcd = this.algorithm.Calculate(first, second);
            stopWatch.Stop();
            milliseconds = stopWatch.Elapsed.Milliseconds;

            return gcd;
        }

        /// <summary>
        /// Calculates GCD of three integers from [-int.MaxValue;int.MaxValue] with milliseconds time.
        /// </summary>
        /// <param name="milliseconds">Method execution time in milliseconds.</param>
        /// <param name="first">First integer.</param>
        /// <param name="second">Second integer.</param>
        /// <param name="third">Third integer.</param>
        /// <returns>The GCD value.</returns>
        /// <exception cref="ArgumentException">Thrown when all numbers are 0 at the same time.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when one or more numbers are int.MinValue.</exception>
        public int Calculate(out long milliseconds, int first, int second, int third)
        {
            var stopWatch = Stopwatch.StartNew();
            var gcd = this.Calculate(first, second, third);
            stopWatch.Stop();
            milliseconds = stopWatch.Elapsed.Milliseconds;

            return gcd;
        }

        /// <summary>
        /// Calculates the GCD of integers from [-int.MaxValue;int.MaxValue] with milliseconds time.
        /// </summary>
        /// <param name="milliseconds">Method execution time in milliseconds.</param>
        /// <param name="first">First integer.</param>
        /// <param name="second">Second integer.</param>
        /// <param name="numbers">Other integers.</param>
        /// <returns>The GCD value.</returns>
        /// <exception cref="ArgumentException">Thrown when all numbers are 0 at the same time.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when one or more numbers are int.MinValue.</exception>
        public int Calculate(out long milliseconds, int first, int second, params int[] numbers)
        {
            var stopWatch = Stopwatch.StartNew();
            var gcd = this.Calculate(first, second, numbers);
            stopWatch.Stop();
            milliseconds = stopWatch.Elapsed.Milliseconds;

            return gcd;
        }
    }
}
