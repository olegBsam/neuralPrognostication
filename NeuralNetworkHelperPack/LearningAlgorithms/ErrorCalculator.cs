using NeuralNetworkHelperPack.LearningAlgorithms;
using System;

namespace MagDiplom
{
    public class ErrorCalculator : IErrorCalculator
    {
        public double Calculate(double[] nnOutput, double[] prognosticationValue)
        {
            var e = 0.0;

            for (int i = 0; i < nnOutput.Length; i++)
            {
                e += Math.Pow(nnOutput[i] - prognosticationValue[i], 2);
            }
            return Math.Sqrt(e);
        }

        public double Calculate(double nnOutput, double prognosticationValue)
        {
            throw new System.NotImplementedException();
        }
    }
}