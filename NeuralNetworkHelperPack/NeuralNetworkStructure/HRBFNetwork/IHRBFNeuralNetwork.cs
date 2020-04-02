using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkHelperPack.NeuralNetworkStructure
{
    public interface IHRBFNeuralNetwork: INeuralNetwork
    {
        IHRBFHiddenLayer HiddenLayer { get; }
        Functions.IHRBFActivationFunction ActivationFunction { get; }
        int HiddenNeuronsCount { get; }
    }
}
