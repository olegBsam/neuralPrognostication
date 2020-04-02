using NeuralNetworkHelperPack.LearningAlgorithms;
using System;
using System.Collections.Generic;

namespace MagDiplom
{
    public class NonePreprocessor : IDataSetPreprocessor
    {
        public System.Collections.Generic.List<(double[] PreviousSet, double[] PrognosticationValue)> Process(
              double[] sourcesDataSet
            , int inputVectorDimension
            , int outputVectorDimension
        )
        {
            var resultSet = new List<(double[] PreviousSet, double[] PrognosticationValue)>();

            for (int i = 0; i < sourcesDataSet.Length - inputVectorDimension - outputVectorDimension; i+= outputVectorDimension)
            {
                var currentPrevSet = new double[inputVectorDimension];
                var currentPrognosticSet = new double[outputVectorDimension];

                Array.Copy(sourcesDataSet, i, currentPrevSet, 0, inputVectorDimension);
                Array.Copy(sourcesDataSet, i + inputVectorDimension, currentPrognosticSet, 0, outputVectorDimension);

                resultSet.Add((currentPrevSet, currentPrognosticSet));
            }

            return resultSet;
        }
    }
}