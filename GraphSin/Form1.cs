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
        NeuralNet net;
        GeneticAlgorithm geneticLearning;
        double[][] outputs;
        double[][] trainInputs;
        int[] neuronsPerLayer;
        double[][] DrawInputs;
        (NeuralNet net, double fitness)[] population;
        Func<double, double> functionTan;
        Func<double, double> derivativeTan;
        Func<double, double, double> errorFunc;
        Func<double, double, double> errorFunctionDerivative;
        ActivationFunction activationFunction;
        ErrorFunction errorFunction;
        private void Form1_Load(object sender, EventArgs e)
        {
            canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            timer1.Enabled= true;

            drawingPen = new Pen(Brushes.Black, 2);

            gfx = Graphics.FromImage(canvas);

            gfx.Clear(Color.White);

            // gfx.DrawEllipse(drawingPen, 100, 100, 3, 3);
            DrawAxis();

            pictureBox1.Image = canvas;

            functionTan = (input) => Math.Tanh(input);
            derivativeTan = (input) => 1 - Math.Pow(Math.Tanh(input), 2);

            errorFunc = (output, desired) => Math.Pow(desired - output, 2);
            errorFunctionDerivative = (input, desired) => -2 * (desired - input);

            activationFunction = new ActivationFunction(functionTan, derivativeTan);
            errorFunction = new ErrorFunction(errorFunc, errorFunctionDerivative);
            neuronsPerLayer = new int[]{ 1, 10, 10, 1 };

            // net = new NeuralNet(activationFunction, errorFunction, neuronsPerLayer);
            geneticLearning = new GeneticAlgorithm();
            trainInputs = new double[17][];
            for (int i = 0; i < trainInputs.Length; i++)
            {
                trainInputs[i] = new double[] { (double)i * Math.PI / 6 };
            }
            //{
            //    0,
            //    Math.PI/6,
            //    Math.PI/4,
            //    Math.PI/3,
            //    Math.PI/4,
            //    2*Math.PI/3,
            //    3*Math.PI/4,
            //    5*Math.PI/6,
            //    Math.PI,
            //    7*Math.PI/6,
            //    5*Math.PI/4,
            //    4*Math.PI/3,
            //    3*Math.PI/2,
            //    5*Math.PI/3,
            //    7*Math.PI/4,
            //    11*Math.PI/6,
            //    2*Math.PI,

            //};
            outputs = new double[17][];
            for (int i = 0; i < trainInputs.Length; i++)
            {
                outputs[i] = new double[] { Math.Sin(trainInputs[i][0]) };
            }
            //{
            //    0,
            //    1 / 2,
            //    2 * Math.Sqrt(2),
            //    3 * Math.Sqrt(2),
            //    1,
            //    3 * Math.Sqrt(2),
            //    2 * Math.Sqrt(2),
            //    1 / 2,
            //    0,
            //     -1 / 2,
            //     -2 * Math.Sqrt(2),
            //     -3 * Math.Sqrt(2),
            //     -1,
            //     -3 * Math.Sqrt(2),
            //     -2 * Math.Sqrt(2),
            //     -1 / 2,
            //    0,
            //};


            DrawInputs = new double[200][];
            for (int i = 0; i < DrawInputs.Length; i++)
            {
                DrawInputs[i] = new double[1];
            }
            for (int i = 0; i < DrawInputs.Length; i++)
            {
                DrawInputs[i][0] = 2.0 * (double)i * Math.PI / (double)DrawInputs.Length;
            }

            population = new (NeuralNet net, double fitness)[10];


            for (int i = 0; i < population.Length; i++)
            {
                NeuralNet net = new NeuralNet(activationFunction, errorFunction, neuronsPerLayer);
                for (int j = 0; j < trainInputs.Length; j++)
                {
                    population[i] = (net, net.GetError(trainInputs[i], outputs[i]));
                }
            }
        }

        public void DrawPoints(NeuralNet net)
        {
            gfx.Clear(Color.White);
            //for (int i = 0; i < points.Count; i++)
            //{
            //    gfx.DrawEllipse(drawingPen, (float)points[i].x + pictureBox1.Width / 2, pictureBox1.Height / 2 - (float)points[i].y, 3, 3);
            //}

            for (int i = 0; i < DrawInputs.Length; i++)
            {
                double[] outputs = net.Compute(DrawInputs[i]);
                gfx.DrawEllipse(drawingPen, (float)DrawInputs[i][0] + pictureBox1.Width / 2, pictureBox1.Height / 2 - (float)outputs[0], 1, 1);
            }
            

            DrawAxis();
            //gfx.DrawLine(drawingPen, new Point(-pictureBox1.Width / 2 + pictureBox1.Width / 2, (pictureBox1.Height / 2 + (int)((-perceptron.weights[0] / perceptron.weights[1]) * -pictureBox1.Width / 2 - perceptron.bias / perceptron.weights[1]))), new Point(pictureBox1.Width / 2 + pictureBox1.Width / 2, (pictureBox1.Height / 2 + (int)((-perceptron.weights[0] / perceptron.weights[1]) * pictureBox1.Width / 2 - perceptron.bias / perceptron.weights[1]))));
            pictureBox1.Image = canvas;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //if (textBox1.Text != null && textBox2.Text != null)
            //{
            //    int xPoint = int.Parse(textBox1.Text);
            //    int yPoint = int.Parse(textBox2.Text);
            //    points.Add(new doublePoint(xPoint, yPoint));
            //    double[][] inputs = new double[points.Count][];
            //    double[] desiredOutput = new double[points.Count];
            //    for (int i = 0; i < points.Count; i++)
            //    {
            //        inputs[i] = new double[1];
            //        inputs[i][0] = points[i].x;
            //        //inputs[i][1] = points[i].y;
            //        desiredOutput[i] = points[i].y;
            //    }
            //    perceptron.bias = .5;
            //    perceptron.weights[0] = .5;
            //    perceptron.weights[1] = .5;
            //    for (int i = 0; i < 100000; i++)
            //    {
            //        perceptron.TrainWithHillClimbingGate(inputs, desiredOutput);
            //    }
            //    DrawPoints();
            //}



        }


        public void DrawAxis()
        {
            gfx.DrawLine(drawingPen, new Point(pictureBox1.Width / 2, 0), new Point(pictureBox1.Width / 2, pictureBox1.Height));
            gfx.DrawLine(drawingPen, new Point(0, pictureBox1.Height / 2), new Point(pictureBox1.Width, pictureBox1.Height / 2));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           

            geneticLearning.Train(population, new Random(), 0.01);

            DrawPoints(population[0].net);

        }
    }
}