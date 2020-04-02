using Accord;
using Accord.Math;
using Accord.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NeuralNetworkHelperPack.Functions
{
    public class GaussActivationFunction : IRBFActivationFunction
    {
        public double Calculate(double[] center, double[] radius, double[] inputVector)
        {
            var rad = new double[inputVector.Length, inputVector.Length];
            for (int i = 0; i < inputVector.Length; i++)
            {
                rad[i, 0] = radius[i];
            }


            var u = 0.0;
            var v1 = rad.Multiply(inputVector.Subtract(center));
            var f = v1.Multiply(v1.Transpose())[0] * (-0.5);
            
            u = Math.Exp(f);
            return u;
        }

        public double dEdCCoef(double[] centers, double[] radiuses, double[] previousSet, int j)
        {
            var v = (previousSet[j] - centers[j]) / Math.Pow(radiuses[j], 2);
            return v * Calculate(centers, radiuses, previousSet);
        }

        public double dEdRCoef(double[] centers, double[] radiuses, double[] previousSet, int j)
        {
            var v = (previousSet[j] - centers[j]) / Math.Pow(radiuses[j], 3);
            return v * Calculate(centers, radiuses, previousSet);
        }

        public double dEdWCoef(double[] centers, double[] radiuses, double[] previousSet)
        {
            return Calculate(centers, radiuses, previousSet);
        }
    }
}
