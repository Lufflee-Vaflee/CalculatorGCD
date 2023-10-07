using GcdCalculator.Services.Interfaces;

namespace GcdCalculator.Services.GcdImplementations
{
    /// <inheritdoc/>
    internal class SteinAlgorithm : IAlgorithm
    {
        /// <inheritdoc/>
        /// <exception cref="ArgumentException">Thrown when all numbers are 0 at the same time.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when one or two numbers are int.MinValue.</exception>
        public int Calculate(int first, int second)
        {
            if (first == 0 && second == 0)
            {
                throw new ArgumentException("All numbers cannot be 0 at the same time.");
            }

            if (first == int.MinValue || second == int.MinValue)
            {
                throw new ArgumentOutOfRangeException($"Number cannot be {int.MinValue}.");
            }

            return this.Func(Math.Abs(first), Math.Abs(second));
        }

        private int Func(int first, int second) => first switch
        {
            _ when first == second => first,
            _ when first == 0 => second,
            _ when second == 0 => first,
            _ when first % 2 == 0 => second switch
            {
                _ when second % 2 == 0 => 2 * this.Func(first / 2, second / 2),
                _ => this.Func(first / 2, second)
            },
            _ when second % 2 == 0 => this.Func(first, second / 2),
            _ when first > second => this.Func((first - second) / 2, second),
            _ => this.Func((second - first) / 2, first)
        };
    }
}
