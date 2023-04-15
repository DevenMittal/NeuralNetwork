using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    internal class NeuralNet
    {
        Layer[] layers;
        ErrorFunction errorFunc;

        public NeuralNetwork(ActivationFunction activation, ErrorFunction errorFunc, params int[] neuronsPerLayer)
        { 
        
        }
        public void Randomize(Random random, double min, double max)
        {

        }
        public double[] Compute(double[] inputs)
        {

        }
        public double GetError(double[] inputs, double[] desiredOutputs)
        {

        }
    }
}
