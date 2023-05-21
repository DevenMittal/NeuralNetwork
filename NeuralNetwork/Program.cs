namespace NeuralNetwork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Func<double, double> functionTan;
            Func<double, double> derivativeTan;
            Func<double, double, double> errorFunc;
            Func<double, double, double> errorFunctionDerivative;

            ActivationFunction activationFunction;
            ErrorFunction errorFunction;

            double[][] inputs = new double[][]
              {
                new double[]{0,0},
                new double[]{1,0},
                new double[]{0,1},
                new double[]{1,1}
              };
            double[][] outputs = new double[][]
            {
                new double[]{0},
                new double[]{1},
                new double[]{1},
                new double[]{0}
            };
            Random random = new Random();

            functionTan = (input) => Math.Tanh(input);
            derivativeTan = (input) => 1 - Math.Pow(Math.Tanh(input), 2);

            errorFunc = (output, desired) => Math.Pow(desired - output, 2);
            errorFunctionDerivative = (input, desired) => -2 * (desired - input);

            activationFunction = new ActivationFunction(functionTan, derivativeTan);
            errorFunction = new ErrorFunction(errorFunc, errorFunctionDerivative);
            int[] neuronsPerLayer = new int[] { 2, 2, 1 };

            NeuralNet net = new NeuralNet(activationFunction, errorFunction, neuronsPerLayer);
            net.Randomize(random, -1, 1);

            while (true)
            {
                Console.SetCursorPosition(0, 0);
                for (int i = 0; i < inputs.Length; i++)
                {
                    Console.Write("Inputs: ");
                    for (int j = 0; j < inputs[i].Length; j++)
                    {
                        if (j != 0)
                        {
                            Console.Write(", ");
                        }
                        Console.Write(inputs[i][j]);
                    }

                    Console.Write(" Output: " + Math.Round(net.Compute(inputs[i])[0], 3));
                    Console.WriteLine();
                }
                double error = net.TrainGradientDescent(inputs, outputs, 0.01);
                Console.WriteLine("Error: " + Math.Round(error, 3));
            }


        }
    }
}