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

        public int InputVectorDimension { get => hiddenLayer.InputVectorDimension; }
        public int OutputVectorDimension { get => hiddenLayer.OutputVectorDimension; }
        public int HiddenNeuronsCount { get => hiddenLayer.HiddenNeuronCount; }
        public IRBFActivationFunction ActivationFunction { get => hiddenLayer.ActivationFunction; }
        public IRBFHiddenLayer HiddenLayer { get => hiddenLayer; }

        public RadialBasisNeuralNetwork(IRBFHiddenLayer hiddenLayer)
        {
            this.hiddenLayer = hiddenLayer;
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
                    currentOutput += weight * ActivationFunction.Calculate(
                              center
                            , radius
                            , inputVector
                        );
                }
                outputVector[outputIndex] = currentOutput;
            }
            return outputVector;
        }


    }
}
