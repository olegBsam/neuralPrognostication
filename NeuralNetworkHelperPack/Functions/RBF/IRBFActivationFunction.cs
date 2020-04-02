namespace NeuralNetworkHelperPack.Functions
{
    public interface IRBFActivationFunction
    {
        double Calculate(double[] center, double[] radius, double[] inputVector);
        double dEdWCoef(double[] centers, double[] radiuses, double[] previousSet);
        double dEdCCoef(double[] centers, double[] radiuses, double[] previousSet, int j);
        double dEdRCoef(double[] centers, double[] radiuses, double[] previousSet, int j);
    }
}