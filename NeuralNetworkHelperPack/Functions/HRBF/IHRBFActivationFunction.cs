namespace NeuralNetworkHelperPack.Functions
{
    public interface IHRBFActivationFunction
    {
        double Calculate(double[] center, double[,] q, double[] inputVector);
    }
}