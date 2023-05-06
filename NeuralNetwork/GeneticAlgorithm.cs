﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class GeneticAlgorithm
    {
        public void Mutate(NeuralNet net, Random random, double mutationRate)
        {
            int count = 0;
            foreach (Layer layer in net.layers)
            {
                count++;
                if (count != 1)
                {
                    foreach (Neuron neuron in layer.Neurons)
                    {
                        //Mutate the Weights
                        for (int i = 0; i < neuron.dendrites.Length; i++)
                        {
                            if (random.NextDouble() < mutationRate)
                            {
                                if (random.Next(2) == 0)
                                {
                                    neuron.dendrites[i].Weight *= random.NextDouble() * (1.5 - .5) + .5; //scale weight
                                }
                                else
                                {
                                    neuron.dendrites[i].Weight *= -1; //flip sign
                                }
                            }
                        }

                        //Mutate the Bias
                        if (random.NextDouble() < mutationRate)
                        {
                            if (random.Next(2) == 0)
                            {
                                neuron.bias *= random.NextDouble() * (1.5 - .5) + .5; //scale weight
                            }
                            else
                            {
                                neuron.bias *= -1; //flip sign
                            }
                        }
                    }
                }
            }
        }
        public void Crossover(NeuralNet winner, NeuralNet loser, Random random)
        {
            for (int i = 1; i < winner.layers.Length; i++)
            {
                //References to the Layers
                Layer winLayer = winner.layers[i];
                Layer childLayer = loser.layers[i];

                int cutPoint = random.Next(winLayer.Neurons.Length); //calculate a cut point for the layer
                bool flip = random.Next(2) == 0; //randomly decide which side of the cut point will come from winner

                //Either copy from 0->cutPoint or cutPoint->Neurons.Length from the winner based on the flip variable
                for (int j = (flip ? 0 : cutPoint); j < (flip ? cutPoint : winLayer.Neurons.Length); j++)
                {
                    //References to the Neurons
                    Neuron winNeuron = winLayer.Neurons[j];
                    Neuron childNeuron = childLayer.Neurons[j];

                    //Copy the winners Weights and Bias into the loser/child neuron
                    //winNeuron.dendrites.CopyTo(childNeuron.dendrites, 0);
                    //IS it OK to just COPY the Dentrites

                    childNeuron.bias = winNeuron.bias;
                    for (int d = 0; d < winNeuron.dendrites.Length; d++)
                    {
                        childNeuron.dendrites[d].Weight = winNeuron.dendrites[d].Weight;
                    }
                }
            }
        }

        public void Train((NeuralNet net, double fitness)[] population, Random random, double mutationRate)
        {
            Array.Sort(population, (a, b) => b.fitness.CompareTo(a.fitness));

            int start = (int)(population.Length * 0.1);
            int end = (int)(population.Length * 0.9);

            //Notice that this process is only called on networks in the middle 80% of the array
            for (int i = start; i < end; i++)
            {
                Crossover(population[random.Next(start)].net, population[i].net, random);
                Mutate(population[i].net, random, mutationRate);
            }

            //Removes the worst performing networks
            for (int i = end; i < population.Length; i++)
            {
                population[i].net.Randomize(random, -1.0, 1.0);
            }
        }














    }
}
