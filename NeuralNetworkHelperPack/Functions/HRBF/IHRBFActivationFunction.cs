namespace NeuralNetworkHelperPack.Functions
{
    public interface IHRBFActivationFunction
    {
        double Calculate(double[] center, double[,] q, double[] inputVector);
        double dEdWi(double[] previousSet, double error, (double[] Center, double[,] Q, double Weight) neuronParams);
        double dEdCij(double[] previousSet, double error, (double[] Center, double[,] Q, double Weight) neuronParams, int j);
        double dEdQijr(double[] previousSet, double error, (double[] Center, double[,] Q, double Weight) neuron, int j, int r);
    }
}