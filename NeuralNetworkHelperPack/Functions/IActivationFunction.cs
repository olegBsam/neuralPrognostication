namespace NeuralNetworkHelperPack.Functions
{
    public interface IRBFActivationFunction
    {
        double Calculate(double[] center, double[] radius, double weight, double[] inputVector);
        double dFdW(double[] centers, double[] radiuses, double[] previousSet);
        double dFdC(double[] centers, double[] radiuses, double[] previousSet, int j);
        double dFdR(double[] centers, double[] radiuses, double[] previousSet, int j);
    }
}