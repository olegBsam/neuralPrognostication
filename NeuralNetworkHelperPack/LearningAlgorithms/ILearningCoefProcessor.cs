namespace NeuralNetworkHelperPack.LearningAlgorithms
{
    public interface ILearningCoefProcessor
    {
        double Init(double learningCoef);
        double Get(double currentLearningCoef, int currentLearningIteration);
    }
}