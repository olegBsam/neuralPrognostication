using NeuralNetworkHelperPack.LearningAlgorithms;

namespace MagDiplom
{
    public class SimpleLearningCoefProcessor : ILearningCoefProcessor
    {
        public double Get(double currentLearningCoef, int currentLearningIteration)
        {
            return currentLearningCoef;
        }

        public double Init(double learningCoef)
        {
            return learningCoef;
        }
    }
}