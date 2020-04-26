using NeuralNetworkHelperPack.Initializers;
using NeuralNetworkHelperPack.NeuralNetworkStructure.TSK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperRadialBasisNeuralNetwork.TSK.TSKNeuralNetworkStructure
{
    public class TSKThirdLayer: ITSKThirdLayer
    {
        public double[][] P { get; private set; }
        public TSKThirdLayer(ITSKLayerInitializer tskLayerInitializer)
        {
            P = tskLayerInitializer.P;
        }

        public double[] Calculate(double[] previousSet, double[] secondLayerOutput, int m, int n)
        {
            var y = new double[P.Length];

            for (int i = 0; i < y.Length; i++)
            {
                y[i] = P[i][0];
                for (int j = 0; j < previousSet.Length; j++)
                {
                    y[i] += P[i][j + 1] * previousSet[j];
                }
            }
            return y;
        }
    }
}
