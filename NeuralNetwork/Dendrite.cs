﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class Dendrite
    {
        public Neuron Previous { get; }
        public Neuron Next { get; }
        public double Weight { get; set; }
        public double WeightUpdate { get; set; }


        public Dendrite(Neuron previous, Neuron next, double weight)
        {
            Previous = previous; 
            Next = next; 
            Weight = weight;
        }


        public void ApplyUpdates()
        {

        }


        public double Compute()
        {
            if (Previous.Output*Weight <0)
            {
                ;
            }
            return Previous.Output * Weight;
        }
    }
}
