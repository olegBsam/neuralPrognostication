namespace NeuralNetworkHelperPack.LearningAlgorithms
{
    public interface IDataSetPreprocessor
    {
        System.Collections.Generic.List<(double[] PreviousSet, double[] PrognosticationValue)> Process(double[] sourcesDataSet);
    }
}