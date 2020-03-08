namespace NeuralNetworkHelperPack.NeuralNetworkStructure
{
    public interface IHiddenLayer
    {
        int HiddenNeuronCount { get; }
        bool IsOffsetNeuron { get; }
        Functions.IRBFActivationFunction ActivationFunction { get; }
        int OutputVectorDimension { get; }
        int InputVectorDimension { get; }

        (double[] Center, double[] Radius, double Weight) GetNeuronByIndex(int neuronIndex, int outputIndex = 0);
        double GetOffsetNeuronsWeight(int outputIndex = 0);
        void SetNeuronByIndex(double[] center, double[] radius, double weight, int neuronIndex, int outputIndex = 0);
    }
}