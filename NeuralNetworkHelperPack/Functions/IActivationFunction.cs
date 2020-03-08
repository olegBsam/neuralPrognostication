namespace NeuralNetworkHelperPack.Functions
{
    public interface IRBFActivationFunction
    {
        double Calculate(double[] center, double[] radius, double weight, double[] inputVector);
    }
}