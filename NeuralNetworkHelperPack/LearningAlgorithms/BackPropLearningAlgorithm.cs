using NeuralNetworkHelperPack.NeuralNetworkStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningDataSet = System.Collections.Generic.List<(double[] PreviousSet, double[] PrognosticationValue)>;
using LearningResultSet = System.Collections.Generic.List<(double Error, double[] NnOutputs, double[] RealOutputs)>;

namespace NeuralNetworkHelperPack.LearningAlgorithms
{
    public class BackPropLearningAlgorithm
    {
        private readonly INeuralNetwork neuralNetwork;
        private readonly double[] sourcesDataSet;
        private readonly LearningDataSet learningDataSet;
        private readonly IDataSetPreprocessor dataSetPreprocessor;
        private readonly IErrorCalculator errorCalculator;
        private readonly INeuralNetworkParamEditor neuralNetworkParamEditor;

        public BackPropLearningAlgorithm(
              INeuralNetwork neuralNetwork
            , double[] sourcesDataSet
            , IDataSetPreprocessor dataSetPreprocessor
            , IErrorCalculator errorCalculator
            , INeuralNetworkParamEditor neuralNetworkParamEditor
        ) {
            this.neuralNetwork = neuralNetwork;
            this.dataSetPreprocessor = dataSetPreprocessor;
            this.errorCalculator = errorCalculator;
            this.neuralNetworkParamEditor = neuralNetworkParamEditor;
            this.learningDataSet = this.dataSetPreprocessor.Process(sourcesDataSet);
        }

        public LearningResultSet Learning(int iterationCount, double learningCoef, ILearningCoefProcessor coefProcessor)
        {
            var result = new LearningResultSet();
            var currentLearningCoef = coefProcessor.Init(learningCoef);

            for (int currentLearningIteration = 0; currentLearningIteration < iterationCount; currentLearningIteration++)
            {
                currentLearningCoef = coefProcessor.Get(currentLearningCoef);
                var oneStepResult = OneLearningStep(currentLearningCoef, currentLearningIteration, learningDataSet[currentLearningIteration]);
                result.Add(oneStepResult);
            }
            return result;
        }


        /// <summary>
        /// ///ДОБАВЛЕНИЕ В КОНЕЦ РЕЗУЛЬТАТА РАБОТЫ НЕЙРОНКИ
        /// </summary>
        /// <param name="iterationCount"></param>
        /// <param name="learningCoef"></param>
        /// <param name="coefProcessor"></param>
        /// <returns></returns>
        public LearningResultSet PrognosticationLearning(int iterationCount, double learningCoef, ILearningCoefProcessor coefProcessor)
        {
            var result = new LearningResultSet();
            var currentLearningCoef = coefProcessor.Init(learningCoef);
            var prognosticFrame = new List<double>;

            for (int currentLearningIteration = 0; currentLearningIteration < iterationCount; currentLearningIteration++)
            {
                currentLearningCoef = coefProcessor.Get(currentLearningCoef);

                var currentLearningSet = learningDataSet[currentLearningIteration];



                var oneStepResult = OneLearningStep(currentLearningCoef, currentLearningIteration, currentLearningSet);
                result.Add(oneStepResult);
            }
            return result;
        }

        private (double Error, double[] NnOutputs, double[] RealOutputs) OneLearningStep(object currentLearningCoef, int currentLearningIteration, (double[] PreviousSet, double[] PrognosticationValue) currentLearningSet)
        { 
            var nnOutput = neuralNetwork.CalculateOutput(currentLearningSet.PreviousSet);
            var error = neuralNetworkParamEditor.Edit(nnOutput, currentLearningSet, currentLearningCoef, currentLearningIteration, errorCalculator);

            return (error, nnOutput, currentLearningSet.PrognosticationValue); ;
        }
    }
}
