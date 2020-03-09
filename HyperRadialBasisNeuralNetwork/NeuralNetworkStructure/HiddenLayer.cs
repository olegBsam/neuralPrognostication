using NeuralNetworkHelperPack.Functions;
using NeuralNetworkHelperPack.Initializers;
using NeuralNetworkHelperPack.NeuralNetworkStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperRadialBasisNeuralNetwork.NeuralNetworkStructure
{
    public class HiddenLayer: IRBFHiddenLayer
    {
        private double[][] Centers;
        private double[][] Radiuses;
        private double[][] Weights;

        private double[] OffsetNeuronWeight;

        public int HiddenNeuronCount { get; }
        public bool IsOffsetNeuron { get; }
        public IRBFActivationFunction ActivationFunction { get; }
        public int OutputVectorDimension { get; }
        public int InputVectorDimension { get; }

        public HiddenLayer(
              int hiddenNeuronCount
            , int inputVectorDimension
            , int outputVectorDimension
            , ICenterInitializer centerInitializer
            , IRadiusInitializer radiusInitializer
            , IWeightInitializer weightInitializer
            , IHiddenLayerInitializer hiddenLayerInitializer
            , bool isOffsetNeuron
            , IRBFActivationFunction activationFunction)
        {
            HiddenNeuronCount = hiddenNeuronCount;
            IsOffsetNeuron = isOffsetNeuron;
            InputVectorDimension = inputVectorDimension;
            OutputVectorDimension = outputVectorDimension;

            ActivationFunction = activationFunction;

            hiddenLayerInitializer.Initialize(
                  inputVectorDimension
                , outputVectorDimension
                , hiddenNeuronCount
                , isOffsetNeuron
                , centerInitializer
                , radiusInitializer
                , weightInitializer
                , ref Centers
                , ref Radiuses
                , ref Weights
                , out OffsetNeuronWeight
                );
        }

        public double GetOffsetNeuronsWeight(int outputIndex = 0) =>
           OffsetNeuronWeight[outputIndex];

        public (double[] Center, double[] Radius, double Weight) GetNeuronByIndex(int neuronIndex, int outputIndex = 0) =>
            (Centers[neuronIndex], Radiuses[neuronIndex], Weights[outputIndex][neuronIndex]);

        public void SetNeuronByIndex(double[] center, double[] radius, double weight, int neuronIndex, int outputIndex = 0)
        {
            Centers[neuronIndex] = center;
            Radiuses[neuronIndex] = radius;
            Weights[outputIndex][neuronIndex] = weight;
        }


    }
}
