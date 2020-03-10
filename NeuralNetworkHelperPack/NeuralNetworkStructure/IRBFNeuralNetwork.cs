namespace NeuralNetworkHelperPack.NeuralNetworkStructure
{
    public interface IRBFNeuralNetwork: INeuralNetwork
    {
        IRBFHiddenLayer HiddenLayer { get; }
        Functions.IRBFActivationFunction ActivationFunction { get; }
        int InputVectorDimension { get; }
        int HiddenNeuronsCount { get; }
        int OutputVectorDimension { get; }
    }
}