namespace NeuralNetworkHelperPack.NeuralNetworkStructure
{
    public interface INeuralNetwork
    {
        double[] CalculateOutput(double[] previousSet);
    }
}