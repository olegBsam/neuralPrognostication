namespace NeuralNetworkHelperPack.LearningAlgorithms
{
    public interface ILearningCoefProcessor
    {
        object Init(double learningCoef);
        object Get(object currentLearningCoef);
    }
}