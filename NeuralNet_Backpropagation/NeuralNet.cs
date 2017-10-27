﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet_Backpropagation
{
    public class NeuralNet
    {
        private const double LearningRate = 0.5;
        private readonly int _inputsNum;
        private readonly int _k;

        private Neuron[] _layer1;
        private Neuron[] _layer2;
        private Neuron _outputNeuron;

        private readonly Random _random;

        public NeuralNet(int inputsNum, int neuronsPerLayer, Random random)
        {
            _inputsNum = inputsNum;
            _k = neuronsPerLayer;
            _random = random;

            BuildNet();
        }

        public double Learn(double[] input, double target)
        {
            var layer1Answers = new double[_k];
            for (int j = 0; j < _k; j++)
            {
                layer1Answers[j] = _layer1[j].GetAnswer(input);
            }

            var layer2Answers = new double[_k];
            for (int j = 0; j < _k; j++)
            {
                layer2Answers[j] = _layer2[j].GetAnswer(layer1Answers);
            }

            var outf = _outputNeuron.GetAnswer(layer2Answers);

            var deByDnet = (outf - target) * outf * (1 - outf);

            for (int j = 0; j < _k; j++)
            {
                _outputNeuron.Weights[j] = _outputNeuron.Weights[j] - LearningRate * deByDnet * layer2Answers[j];
            }

            for (int j = 0; j < _k; j++)
            {
                for (int m = 0; m < _k; m++)
                {
                    _layer2[j].Weights[m] = _layer2[j].Weights[m] -
                                            LearningRate * deByDnet * _outputNeuron.Weights[j] * layer2Answers[j] *
                                            (1 - layer2Answers[j]) * layer1Answers[m];
                }
            }

            for (int j = 0; j < _k; j++)
            {
                for (int m = 0; m < _inputsNum; m++)
                {
                    var sum = 0.0;
                    for (int k = 0; k < _k; k++)
                    {
                        sum += _outputNeuron.Weights[k] * layer2Answers[k] * (1 - layer2Answers[k]) * _layer2[k].Weights[j];
                    }
                    _layer1[j].Weights[m] = _layer1[j].Weights[m] -
                                            LearningRate * sum * deByDnet * layer1Answers[j] * (1 - layer1Answers[j]) *
                                            input[m];
                }
            }

            return outf;
        }

        private void BuildNet()
        {
            _layer1 = new Neuron[_k];
            _layer2 = new Neuron[_k];

            for (int j = 0; j < _k; j++)
            {
                var inputs1 = new double[_inputsNum];

                for (int k = 0; k < _inputsNum; k++)
                {
                    inputs1[k] = _random.NextDouble() * 2 - 1;
                }
                _layer1[j] = new Neuron(_inputsNum, ActivationFunctions.Sigma)
                {
                    Weights = inputs1.ToList(),
                    Bias = 0
                };

                var inputs2 = new double[_k];
                for (int k = 0; k < _k; k++)
                {
                    inputs2[k] = _random.NextDouble() * 2 - 1;
                }
                _layer2[j] = new Neuron(_k, ActivationFunctions.Sigma)
                {
                    Weights = inputs2.ToList(),
                    Bias = 0
                };
            }
            var inputs = new double[_k];

            for (int k = 0; k < _k; k++)
            {
                inputs[k] = _random.NextDouble() * 2 - 1;
            }
            _outputNeuron = new Neuron(_k, ActivationFunctions.Sigma)
            {
                Weights = inputs.ToList(),
                Bias = 0
            };
        }
    }
}
