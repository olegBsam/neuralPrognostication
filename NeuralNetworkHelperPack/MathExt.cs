using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkHelperPack
{
    public static class MathExt
    {
        public static double[] Subtract(this double[] v1, double[] v2)
        {
            if (v1.Length != v2.Length) throw new ArgumentOutOfRangeException();

            var result = new double[v1.Length];

            for (int i = 0; i < v1.Length; i++)
            {
                result[i] = v1[i] - v2[i];
            }
            return result;
        }

        public static double[] Sum(this double[] v1, double[] v2)
        {
            if (v1.Length != v2.Length) throw new ArgumentOutOfRangeException();

            var result = new double[v1.Length];

            for (int i = 0; i < v1.Length; i++)
            {
                result[i] = v1[i] + v2[i];
            }
            return result;
        }

        public static double[][] CreateArr2(int i, int j)
        {
            var result = new double[i][];
            for (int k = 0; k < i; k++)
            {
                result[k] = new double[j];
            }
            return result;
        }
    }
}
