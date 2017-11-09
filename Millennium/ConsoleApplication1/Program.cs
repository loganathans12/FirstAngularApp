using System;
using System.Linq;
using mlp.interviews.software.testInterfaces;
using mlp.interviews.software.test;
using mlp.interviews.boxing.problem;

namespace mlp.interviews.software.testMain
{
    class Program
    {
        static private int DisplayMenu()
        {
            Console.WriteLine("Please enter the options..");
            Console.WriteLine();
            Console.WriteLine(" 1. Softare Tests...");
            Console.WriteLine(" 2. Boxing Problem...");
            Console.WriteLine(" 3. Exit...");

            var result = Console.ReadLine();
            return Convert.ToInt32(result);
        }

        static void Main(string[] args)
        {
            int userInput = 0;
            do
            {
                userInput = DisplayMenu();

                switch (userInput)
                {
                    case 1:
                        var challenges = new IChallenge[] {
                                        new NumberCalculator(),
                                        new RunLengthEncodingChallenge()
                        };

                        foreach (var challenge in challenges)
                        {
                            var challengeName = challenge.GetType().Name;

                            var result = challenge.Winner()
                                ? string.Format("You win at challenge {0}", challengeName)
                                : string.Format("You lose at challenge {0}", challengeName);

                            Console.WriteLine(result);
                        }
                        break;
                    case 2:
                        using (Position _netPosition = new Position())
                        {
                            bool _netPositions = _netPosition.CalcNetPositions();
                            bool _boxexPositions = _netPosition.BoxedPositions();
                        }
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }

            } while (userInput != 3);
        }
    }
}
