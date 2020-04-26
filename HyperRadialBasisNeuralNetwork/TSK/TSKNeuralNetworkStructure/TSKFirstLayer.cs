using NeuralNetworkHelperPack.Initializers;
using NeuralNetworkHelperPack.NeuralNetworkStructure.TSK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperRadialBasisNeuralNetwork.TSK.TSKNeuralNetworkStructure
{
    public class TSKFirstLayer : ITSKFirstLayer
    {
        public TSKFirstLayer(
              ITSKLayerInitializer tskLayerInitializer
            )
        {
            M = tskLayerInitializer.M;
            N = tskLayerInitializer.N;
            C = tskLayerInitializer.C;
            Sigma = tskLayerInitializer.Sigma;
            B = tskLayerInitializer.B;
        }

        public int M { get; set; }
        public int N { get; set; }
        public double[][] C { get; set; }
        public double[][] Sigma { get; set; }
        public double[][] B { get; set; }

        public double[][] Calculate(double[] previousSet)
        {
            var v = new double[M][];

            for (int i = 0; i < M; i++)
            {
                v[i] = new double[N];
                for (int j = 0; j < N; j++)
                {
                    var dx = Math.Abs(previousSet[j] - C[i][j]);
                    dx = dx / Sigma[i][j];
                    var pow = 2.0 * B[i][j];
                    v[i][j] = 1.0 / (1.0 + Math.Pow(dx, pow));
                }
            }
            return v;
        }
    }
}
