namespace NeuralNetworkHelperPack.NeuralNetworkStructure
{
    public interface INeuralNetwork
    {
        double[] CalculateOutput(double[] previousSet);
        int OutputVectorDimension { get; }
        int InputVectorDimension { get; }
    }
}