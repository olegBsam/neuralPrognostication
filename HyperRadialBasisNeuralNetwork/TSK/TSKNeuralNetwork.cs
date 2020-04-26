using HyperRadialBasisNeuralNetwork.TSK.TSKNeuralNetworkStructure;
using NeuralNetworkHelperPack.NeuralNetworkStructure.TSK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperRadialBasisNeuralNetwork.TSK
{
    public class TSKNeuralNetwork : ITSKNeuralNetwork
    {
        private int n;

        public TSKNeuralNetwork(
              ITSKFirstLayer firstLayer
            , ITSKSecondLayer secondLayer
            , ITSKThirdLayer thirdLayer
            , ITSKFourthLayer fourthLayer
            , ITSKFifthLayer fifthLayer)
        {
            FirstLayer = firstLayer;
            SecondLayer = secondLayer;
            ThirdLayer = thirdLayer;
            FourthLayer = fourthLayer;
            FifthLayer = fifthLayer;
        }

        /// <summary>
        /// Количество правил вывода
        /// </summary>
        public int M { get { return FirstLayer.M; } set { FirstLayer.M = value; } }
        /// <summary>
        /// Размерность входного вектора
        /// </summary>
        public int N { get { return FirstLayer.N; } set { FirstLayer.N = value; } }
        public int OutputVectorDimension { get; set; } = 1;
        public int InputVectorDimension { get => FirstLayer.N; }



        public ITSKFirstLayer FirstLayer { get; set; }
        public ITSKSecondLayer SecondLayer { get; set; }
        public ITSKThirdLayer ThirdLayer { get; set; }
        public ITSKFourthLayer FourthLayer { get; set; }
        public ITSKFifthLayer FifthLayer { get; set; }

        public double[] CalculateOutput(double[] previousSet)
        {
            var firstLayerOutput = FirstLayer.Calculate(previousSet);
            /// w
            var secondLayerOutput = SecondLayer.Calculate(firstLayerOutput);
            double[] thirdLayerOutput = ThirdLayer.Calculate(previousSet, secondLayerOutput, M, N);
            (double f1, double f2) = FourthLayer.Calculate(secondLayerOutput, thirdLayerOutput);

            double y = FifthLayer.Calculate(f1, f2);
            return new double[] { y };
        }


    }
}
