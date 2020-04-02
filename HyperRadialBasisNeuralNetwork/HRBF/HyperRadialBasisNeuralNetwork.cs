using NeuralNetworkHelperPack.Functions;
using NeuralNetworkHelperPack.NeuralNetworkStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperRadialBasisNeuralNetwork
{
    public class HyperRadialBasisNeuralNetwork : IHRBFNeuralNetwork
    {
        IHRBFHiddenLayer hiddenLayer;

        public IHRBFHiddenLayer HiddenLayer { get => hiddenLayer; }

        public IHRBFActivationFunction ActivationFunction { get => hiddenLayer.ActivationFunction; }

        public int HiddenNeuronsCount { get => hiddenLayer.HiddenNeuronCount; }

        public int OutputVectorDimension { get => hiddenLayer.OutputVectorDimension; }

        public int InputVectorDimension { get => hiddenLayer.InputVectorDimension; }

        public HyperRadialBasisNeuralNetwork(IHRBFHiddenLayer hiddenLayer)
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
                    currentOutput += hiddenLayer.GetOffsetNeuronsWeight();
                }

                for (int neuronIndex = 0; neuronIndex < hiddenLayer.HiddenNeuronCount; neuronIndex++)
                {
                    (double[] center, double[,] q, double weight) = hiddenLayer.GetNeuronParamByIndex(neuronIndex);
                    currentOutput += weight * ActivationFunction.Calculate(
                              center
                            , q
                            , inputVector
                        );
                }
                outputVector[outputIndex] = currentOutput;
            }
            return outputVector;
        }
    }
}
