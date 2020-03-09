namespace NeuralNetworkHelperPack.LearningAlgorithms
{
    public interface IErrorCalculator
    {
        double Calculate(double[] nnOutput, double[] prognosticationValue);
        double Calculate(double nnOutput, double prognosticationValue);
    }
}