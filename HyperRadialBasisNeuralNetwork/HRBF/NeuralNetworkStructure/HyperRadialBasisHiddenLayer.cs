using NeuralNetworkHelperPack.Functions;
using NeuralNetworkHelperPack.Initializers;
using NeuralNetworkHelperPack.NeuralNetworkStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperRadialBasisNeuralNetwork.HRBF.NeuralNetworkStructure
{
    public class HyperRadialBasisHiddenLayer : IHRBFHiddenLayer
    {
        private double[][] Centers;
        private double[][,] Q;
        private double[] Weights;

        private double OffsetNeuronWeight;

        public HyperRadialBasisHiddenLayer(
              int hiddenNeuronCount
            , int inputVectorDimension
            , ICenterInitializer centerInitializer
            , IQInitializer radiusInitializer
            , IWeightInitializer weightInitializer
            , IHiddenLayerInitializer hiddenLayerInitializer
            , bool isOffsetNeuron
            , IHRBFActivationFunction activationFunction)
        {
            HiddenNeuronCount = hiddenNeuronCount;
            IsOffsetNeuron = isOffsetNeuron;
            InputVectorDimension = inputVectorDimension;

            ActivationFunction = activationFunction;

            hiddenLayerInitializer.Initialize(
                  inputVectorDimension
                , hiddenNeuronCount
                , isOffsetNeuron
                , centerInitializer
                , radiusInitializer
                , weightInitializer
                , ref Centers
                , ref Q
                , ref Weights
                , out OffsetNeuronWeight
                );

        }


        public int HiddenNeuronCount { get; }

        public bool IsOffsetNeuron { get; }

        public IHRBFActivationFunction ActivationFunction { get; }

        public int InputVectorDimension { get; }

        public int OutputVectorDimension { get; }


        public (double[] Center, double[,] Q, double Weight) GetNeuronParamByIndex(int neuronIndex) =>
            (Centers[neuronIndex], Q[neuronIndex], Weights[neuronIndex]);

        public double GetOffsetNeuronsWeight() =>
            OffsetNeuronWeight;

        public void SetNeuronByIndex(double[] center, double[,] q, double weight, int neuronIndex)
        {
            Centers[neuronIndex] = center;
            Q[neuronIndex] = q;
            Weights[neuronIndex] = weight;
        }

        public void SetNeuronByIndex(double[] center, double[,] q, int neuronIndex)
        {
            Centers[neuronIndex] = center;
            Q[neuronIndex] = q;
        }

        public void SetNeuronByIndex(double weight, int neuronIndex)
        {
            Weights[neuronIndex] = weight;
        }

        public void SetOffsetNeuronsWeight(double weight)
        {
            OffsetNeuronWeight = weight;
        }
    }
}
