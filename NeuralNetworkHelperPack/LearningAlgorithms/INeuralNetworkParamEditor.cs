namespace NeuralNetworkHelperPack.LearningAlgorithms
{
    public interface INeuralNetworkParamEditor
    {
        /// <returns>Возвращает значение ошибки, вычисленное с помощью errorCalculator</returns>
        double Edit(double[] nnOutput, (double[] PreviousSet, double[] PrognosticationValue) currentLearningSet, object currentLearningCoef, int currentLearningIteration, IErrorCalculator errorCalculator);
    }
}