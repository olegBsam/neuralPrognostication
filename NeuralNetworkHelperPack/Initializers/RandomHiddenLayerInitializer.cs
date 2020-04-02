using NeuralNetworkHelperPack.Initializers;
using System;

namespace MagDiplom
{
    public class RandomHiddenLayerInitializer : IHiddenLayerInitializer
    {
        public void Initialize(
              int inputVectorDimension
            , int outputVectorDimension
            , int hiddenNeuronCount
            , bool isOffsetNeuron
            , ICenterInitializer centerInitializer
            , IRadiusInitializer radiusInitializer
            , IWeightInitializer weightInitializer
            , ref double[][] centers
            , ref double[][] radiuses
            , ref double[][] weights
            , out double[] offsetNeuronWeight)
        {
            centers = new double[hiddenNeuronCount][];
            radiuses = new double[hiddenNeuronCount][];
            weights = new double[outputVectorDimension][];
            offsetNeuronWeight = new double[outputVectorDimension];

            var rnd = new Random();


            for (int i = 0; i < hiddenNeuronCount; i++)
            {
                centers[i] = new double[inputVectorDimension];
                radiuses[i] = new double[inputVectorDimension];
                for (int j = 0; j < inputVectorDimension; j++)
                {
                    centers[i][j] = rnd.NextDouble() / 100.0;
                    radiuses[i][j] = rnd.NextDouble() / 100.0;
                }

            }

            for (int i = 0; i < outputVectorDimension; i++)
            {
                offsetNeuronWeight[i] = rnd.NextDouble();
                weights[i] = new double[hiddenNeuronCount];
                for (int j = 0; j < hiddenNeuronCount; j++)
                {
                    weights[i][j] = rnd.NextDouble() / 100.0;
                }
            }
        }
    }
}