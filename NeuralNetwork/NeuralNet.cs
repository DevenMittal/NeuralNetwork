using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class NeuralNet
    {
        public Layer[] layers;
        ErrorFunction errorFunc;
       

        public NeuralNet(ActivationFunction activation, ErrorFunction errorFunc, params int[] neuronsPerLayer)
        {          
            this.errorFunc = errorFunc;
            layers = new Layer[neuronsPerLayer.Length];
            Layer previous = null;            

            for (int i = 0; i < neuronsPerLayer.Length; i++)
            {
                layers[i] = new Layer(activation, neuronsPerLayer[i], previous);
                previous = layers[i];
            }            

        }
        public void Randomize(Random random, double min, double max)
        {
            for (int i = 1; i < layers.Length; i++)
            {
                layers[i].Randomize(random, min, max);
            }
        }
        public double[] Compute(double[] inputs)
        {
            if (layers[0].Neurons.Length != inputs.Length)
            {
                throw new Exception("stop");
            }
            for (int i = 0; i < inputs.Length; i++)
            {
                layers[0].Neurons[i].Output = inputs[i];
            }
            double[] outputs = null;
            for (int i = 1; i < layers.Length; i++)
            {
                outputs = layers[i].Compute();
            }
            if (outputs[0] < 0)
            {
                ;
            }
            return outputs;
        }
        public double GetError(double[] inputs, double[] desiredOutputs)
        {
            double sum = 0;
            double[] results = Compute(inputs);
            for (int i = 0; i < inputs.Length; i++)
            {
                sum += errorFunc.Function(results[i], desiredOutputs[i]);
            }
            return sum;
        }
    }
}
