using Accord.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkHelperPack.Functions.HRBF
{
    public class HRBFActivationFunction : IHRBFActivationFunction
    {
        public double Calculate(double[] center, double[,] q, double[] inputVector)
        {
            var result = 0.0;
            var v1 = q.Multiply(inputVector.Subtract(center));
            result = Math.Exp(-0.5 * v1.Multiply(v1.Transpose())[0]);

            return result;
        }

        public double dEdCij(double[] previousSet, double error, (double[] Center, double[,] Q, double Weight) neuronParams, int j) =>
             -dEdWi(previousSet, error, neuronParams) * neuronParams.Weight* neuronParams.Q.GetRow(j)
                    .Select((t1, r) => t1* CalculationZr(neuronParams, previousSet, r))
                    .Sum();

        public double dEdQijr(double[] previousSet, double error, (double[] Center, double[,] Q, double Weight) neuron, int j, int r)
            =>
             -dEdWi(previousSet, error, neuron) * neuron.Weight * (previousSet[j] - neuron.Center[j]) * CalculationZr(neuron, previousSet, r);

        public double dEdWi(double[] previousSet, double error, (double[] Center, double[,] Q, double Weight) neuronParams)
        {
          return Math.Exp(-0.5 * CalculationUi(neuronParams, previousSet)) * error;
        }



        private static double CalculationZr((double[] Center, double[,] Q, double Weight) neuronParams, double[] x, int r)
        {
            var result = 0D;
            for (int j = 0; j < neuronParams.Q.GetLength(0); j++)
            {
                result += neuronParams.Q[j, r] * (x[j] - neuronParams.Center[j]);
            }
            return result;
        }

        private static double CalculationUi((double[] Center, double[,] Q, double Weight) neuronParams, double[] x)
        {
            var result = 0D;
            for (int j = 0; j < neuronParams.Q.GetLength(0); j++)
            {
                result += Math.Pow(CalculationZr(neuronParams, x, j), 2);
            }

            return result;

        }

    }
}
