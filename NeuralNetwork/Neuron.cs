using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
   
       
    public class Neuron
    {
        public double bias;
        public Dendrite[] dendrites;
        public double Output { get; set; }
        public double Input { get; private set; }
        public ActivationFunction Activation { get; set; }

        public Neuron(ActivationFunction activation, Neuron[] previousNerons) 
        {  
            Activation = activation;
            if (previousNerons != null)
            {
                dendrites = new Dendrite[previousNerons.Length];
                for (int i = 0; i < dendrites.Length; i++)
                {
                    dendrites[i] = new Dendrite(previousNerons[i], this, 0);
                }
                Randomize(new Random(), -1.0, 1.0);
            }
            else
            {
                dendrites = null;
            }
        }
        public void Randomize(Random random, double min, double max)
        {
            for (int i = 0; i < dendrites.Length; i++)
            {
                dendrites[i].Weight = random.NextDouble() * (max - min) + min;
            }
        }
        public double Compute()
        {
            Input = bias;
            for (int i = 0; i < dendrites.Length; i++)
            {
                Input += dendrites[i].Compute();
            }
            if (Input<0)
            {
                ;
            }
            Output = Activation.Function(Input);
            return  Output;
        }
    }
}