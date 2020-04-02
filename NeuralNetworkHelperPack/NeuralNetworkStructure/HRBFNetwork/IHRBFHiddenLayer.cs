using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkHelperPack.NeuralNetworkStructure
{
    public interface IHRBFHiddenLayer
    {
        /// <summary>
        /// K
        /// </summary>
        int HiddenNeuronCount { get; }
        bool IsOffsetNeuron { get; }
        Functions.IHRBFActivationFunction ActivationFunction { get; }
        /// <summary>
        /// N
        /// </summary>
        int InputVectorDimension { get; }
        int OutputVectorDimension { get; }

        (double[] Center, double[,] Q, double Weight) GetNeuronParamByIndex(int neuronIndex);

        double GetOffsetNeuronsWeight();
        void SetOffsetNeuronsWeight(double weight);

        void SetNeuronByIndex(double[] center, double[,] q, double weight, int neuronIndex);
        void SetNeuronByIndex(double[] center, double[,] q, int neuronIndex);
        void SetNeuronByIndex(double weight, int neuronIndex);
    }
}
