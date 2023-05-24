using NeuralNetwork;
using System;
using System.Windows.Forms;


namespace GraphSin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Graphics gfx;
        Bitmap canvas;
        Pen drawingPen;
        GeneticAlgorithm geneticLearning;

        double[][] outputs;
        double[][] trainInputs;
        double[][] DrawInputs;

        int[] neuronsPerLayer;

        (NeuralNet net, double fitness)[] population;
        NeuralNet network;

        Func<double, double> functionTan;
        Func<double, double> derivativeTan;
        Func<double, double, double> errorFunc;
        Func<double, double, double> errorFunctionDerivative;

        ActivationFunction activationFunction;
        ErrorFunction errorFunction;

        private void Form1_Load(object sender, EventArgs e)
        {
            canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            timer1.Enabled = true;

            drawingPen = new Pen(Brushes.Black, 2);

            gfx = Graphics.FromImage(canvas);

            gfx.Clear(Color.White);

            DrawAxis();

            pictureBox1.Image = canvas;

            functionTan = (input) => Math.Tanh(input);
            derivativeTan = (input) => 1 - Math.Pow(Math.Tanh(input), 2);

            errorFunc = (output, desired) => Math.Pow(desired - output, 2);
            errorFunctionDerivative = (input, desired) => -2 * (desired - input);

            activationFunction = new ActivationFunction(functionTan, derivativeTan);
            errorFunction = new ErrorFunction(errorFunc, errorFunctionDerivative);
            neuronsPerLayer = new int[] { 1, 10,10, 10, 10, 1 };
            network = new NeuralNet(activationFunction, errorFunction, neuronsPerLayer);


            geneticLearning = new GeneticAlgorithm();
            trainInputs = new double[70][];
            for (int i = 0; i < trainInputs.Length; i++)
            {
                /*(double)(Math.PI / 4))*/
                trainInputs[i] = new double[] { ((double)i * .2 )};
            }

            outputs = new double[70][];
            for (int i = 0; i < trainInputs.Length; i++)
            {
                outputs[i] = new double[] { Math.Sin(trainInputs[i][0]) };
            }

            DrawInputs = new double[1000][];
            for (int i = 0; i < DrawInputs.Length; i++)
            {
                DrawInputs[i] = new double[1];
            }
            for (int i = 0; i < DrawInputs.Length; i++)
            {
                DrawInputs[i][0] = 2.0 * (double)i * 10 * Math.PI / (double)DrawInputs.Length;
            }

            population = new (NeuralNet net, double fitness)[100];


            for (int i = 0; i < population.Length; i++)
            {
                NeuralNet net = new NeuralNet(activationFunction, errorFunction, neuronsPerLayer);
                double error = 0;
                for (int j = 0; j < trainInputs.Length; j++)
                {
                    error += net.GetError(trainInputs[j], outputs[j]);
                }
                population[i] = (net, error);

            }



        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            /*
            for (int i = 0; i < population.Length; i++)
            {
                double error = 0;
                for (int j = 0; j < trainInputs.Length; j++)
                {
                    error += population[i].net.GetError(trainInputs[j], outputs[j]);
                }
                population[i] = (population[i].net, error);

            }

            geneticLearning.TrainGeneticLearning(population, new Random(), 0.01);
            */
            for (int i = 0; i < 10; i++)
            {
                network.TrainGradientDescent(trainInputs, outputs, .01);
            }

            DrawPoints(network);

        }
        public void DrawPoints(NeuralNet net)
        {
            gfx.Clear(Color.White);

            for (int i = 0; i < DrawInputs.Length; i++)
            {
                double[] outputs = net.Compute(DrawInputs[i]);
                gfx.DrawEllipse(drawingPen, ((float)DrawInputs[i][0] * 40 + pictureBox1.Width / 2), pictureBox1.Height / 2 - (float)outputs[0] * 60, 1, 1);
            }

            DrawAxis();
            pictureBox1.Image = canvas;
        }



        public void DrawAxis()
        {
            gfx.DrawLine(drawingPen, new Point(pictureBox1.Width / 2, 0), new Point(pictureBox1.Width / 2, pictureBox1.Height));
            gfx.DrawLine(drawingPen, new Point(0, pictureBox1.Height / 2), new Point(pictureBox1.Width, pictureBox1.Height / 2));
        }
    }
}