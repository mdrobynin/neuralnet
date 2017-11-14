﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNet_Backpropagation
{
    class Result
    {
        public Result()
        {

        }

        public Result(int number, double[] inputs, double target, double output)
        {
            Number = $" i: {number}";
            X = $" x:{inputs[0]}";
            Y = $" y:{inputs[1]}";
            Z = $" z:{inputs[2]}";
            Target = $" target {target}";
            Output = $" result {output.ToString("##0.####")}";
            Delta = $" {((output - target) * 100).ToString("##0.####")}%";
        }

        public void Write()
        {
            Console.WriteLine(Serialize());
        }

        public string Serialize()
        {
            return $"{Number} {X} {Y} {Z} {Target} {Output} {Delta}";
        }

        public string Number { get; set; }
        public string X { get; set; }
        public string Y { get; set; }
        public string Z { get; set; }
        public string Target { get; set; }
        public string Output { get; set; }
        public string Delta { get; set; }
    }
}
