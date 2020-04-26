using NeuralNetworkHelperPack.Initializers;
using NeuralNetworkHelperPack.NeuralNetworkStructure.TSK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperRadialBasisNeuralNetwork.TSK.TSKNeuralNetworkStructure
{
    public class TSKSecondLayer : ITSKSecondLayer
    {
        public TSKSecondLayer(ITSKLayerInitializer tskLayerInitializer)
        {
            M = tskLayerInitializer.M;
            N = tskLayerInitializer.N;
        }
        public int M { get; private set; }
        public int N { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstLayerOutput">Первый индекс - номер правила вывода</param>
        /// <returns></returns>
        public double[] Calculate(double[][] firstLayerOutput)
        {
            if (M != firstLayerOutput.Length || N != firstLayerOutput[0].Length)
                throw new Exception();

            var output = new List<double>();

            for (int i = 0; i < M; i++)
            {
                var v = 1.0;
                for (int j = 0; j < N; j++)
                {
                    v = v * firstLayerOutput[i][j];
                }
                output.Add(v);
            }
            return output.ToArray();
        }
    }
}
