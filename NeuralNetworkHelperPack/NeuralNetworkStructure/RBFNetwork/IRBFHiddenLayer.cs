namespace NeuralNetworkHelperPack.NeuralNetworkStructure
{
    public interface IRBFHiddenLayer
    {
        /// <summary>
        /// K
        /// </summary>
        int HiddenNeuronCount { get; }
        bool IsOffsetNeuron { get; }
        Functions.IRBFActivationFunction ActivationFunction { get; }
        int OutputVectorDimension { get; }
        /// <summary>
        /// N
        /// </summary>
        int InputVectorDimension { get; }

        (double[] Center, double[] Radius, double Weight) GetNeuronParamByIndex(int neuronIndex, int outputIndex = 0);
        double GetOffsetNeuronsWeight(int outputIndex = 0);
        void SetOffsetNeuronsWeight(double weight, int outputIndex = 0);
        void SetNeuronByIndex(double[] center, double[] radius, double weight, int neuronIndex, int outputIndex = 0);
        void SetNeuronByIndex(double[] center, double[] radius, int neuronIndex, int outputIndex = 0);
        void SetNeuronByIndex(double weight, int neuronIndex, int outputIndex = 0);
    }
}