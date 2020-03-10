using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetworkHelperPack.Functions;
using NeuralNetworkHelperPack.NeuralNetworkStructure;

namespace HyperRadialBasisNeuralNetwork
{
    public class RadialBasisNeuralNetwork : IRBFNeuralNetwork
    {
        IRBFHiddenLayer hiddenLayer;

        public int InputVectorDimension { get; }
        public int OutputVectorDimension { get; }
        public int HiddenNeuronsCount { get; }
        public IRBFActivationFunction ActivationFunction { get; }
        public IRBFHiddenLayer HiddenLayer { get => hiddenLayer; }

        public RadialBasisNeuralNetwork(
              int inputVectorDimension
            , int hiddenNeuronsCount
            , IRBFHiddenLayer hiddenLayer
            , IRBFActivationFunction activationFunction)
        {
            InputVectorDimension = inputVectorDimension;
            HiddenNeuronsCount = hiddenNeuronsCount;
            this.hiddenLayer = hiddenLayer;
            ActivationFunction = activationFunction;
            OutputVectorDimension = hiddenLayer.OutputVectorDimension;
        }

        public double[] CalculateOutput(double[] inputVector)
        {
            if (inputVector.Length != InputVectorDimension) throw new IndexOutOfRangeException();

            var outputVector = new double[hiddenLayer.OutputVectorDimension];

            for (int outputIndex = 0; outputIndex < hiddenLayer.OutputVectorDimension; outputIndex++)
            {
                var currentOutput = 0.0;

                if (hiddenLayer.IsOffsetNeuron)
                {
                    currentOutput += hiddenLayer.GetOffsetNeuronsWeight(outputIndex);
                }

                for (int neuronIndex = 0; neuronIndex < hiddenLayer.HiddenNeuronCount; neuronIndex++)
                {
                    (double[] center, double[] radius, double weight) = hiddenLayer.GetNeuronParamByIndex(neuronIndex, outputIndex);
                    currentOutput += ActivationFunction.Calculate(
                              center
                            , radius
                            , weight
                            , inputVector
                        );
                }
                outputVector[outputIndex] = currentOutput;
            }
            return outputVector;
        }


    }
}
