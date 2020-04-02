namespace NeuralNetworkHelperPack.NeuralNetworkStructure
{
    public interface IRBFNeuralNetwork: INeuralNetwork
    {
        IRBFHiddenLayer HiddenLayer { get; }
        Functions.IRBFActivationFunction ActivationFunction { get; }
        int HiddenNeuronsCount { get; }  
    }
}