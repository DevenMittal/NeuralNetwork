using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    internal class Layer
    {
        public Neuron[] Neurons { get; }
        public double[] Outputs { get; }

        public Layer(ActivationFunction activation, int neuronCount, Layer previousLayer) { ... }
        public void Randomize(Random random, double min, double max)
        {

        }
        public double[] Compute()
        {
            return null;
        }
    }
}
