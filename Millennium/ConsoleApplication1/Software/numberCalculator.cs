using System.Linq;
using mlp.interviews.software.common;
using mlp.interviews.software.testInterfaces;

namespace mlp.interviews.software.test
{
    public class NumberCalculator : BaseClass, IChallenge
    {
        bool disposed = false;
        public int FindMax(int[] numbers)
        {
            return numbers.Max();
        }

        public int[] FindMax(int[] numbers, int n)
        {
            return numbers.OrderByDescending(num => num)
                          .Distinct()
                          .Take(n)
                          .ToArray();
        }

        public int[] Sort(int[] numbers)
        {
            return numbers.OrderBy(num => num)
                          .ToArray();
        }

        public bool Winner()
        {
            var numbers = new[] { 5, 7, 5, 3, 6, 7, 9 };
            var sorted = Sort(numbers);
            var topMaxNumbers = 2;
            var maxes = FindMax(numbers, topMaxNumbers);

            return numbers.Length >= topMaxNumbers  // Added this minimum length to ensure the function "FindMax" executes with out any errors 
                   && sorted.First() == 3
                   && sorted.Last() == 9
                   && FindMax(numbers) == 9
                   && maxes[0] == 9
                   && maxes[1] == 7;
        }

        // Protected implementation of Dispose pattern.
        protected override void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing) { }
            disposed = true;
            // Call the base class implementation.
            base.Dispose(disposing);
        }

        ~NumberCalculator()
        {
            Dispose(false);
        }
    }
}
