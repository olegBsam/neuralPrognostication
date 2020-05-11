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
            this.learningDataSet = this.dataSetPreprocessor.Process(sourcesDataSet, neuralNetwork.InputVectorDimension, neuralNetwork.OutputVectorDimension);
        }

        public (List<double[]> RealOutput, List<double[]> TestOutput) Test(int volume)
        {
            var realOutput = new List<double[]>();
            var testOutput = new List<double[]>();

            var testSet = learningDataSet.GetRange(learningDataSet.Count() - volume, volume);

            for (int i = 0; i < testSet.Count; i++)
            {
                realOutput.Add(neuralNetwork.CalculateOutput(testSet[i].PreviousSet));
                testOutput.Add(testSet[i].PrognosticationValue);
            }

            return (realOutput, testOutput);
        }

        public List<double> Learning(int iterationCount, double learningCoef, ILearningCoefProcessor coefProcessor)
        {
            var result = new List<double>();
            var currentLearningCoef = coefProcessor.Init(learningCoef);

            for (int currentLearningIteration = 0; currentLearningIteration < iterationCount; currentLearningIteration++)
            {
                var oneStepErrors = new List<double>();
                for (int i = 0; i < learningDataSet.Count(); i++)
                {
                    currentLearningCoef = coefProcessor.Get(currentLearningCoef, currentLearningIteration * i);
                    oneStepErrors.Add(OneLearningStep(currentLearningCoef, currentLearningIteration * i, learningDataSet[i]).Error);
                }
                result.Add(oneStepErrors.Max());
            }
            return result;
        }

        //public LearningResultSet Learning(int iterationCount, double learningCoef, ILearningCoefProcessor coefProcessor)
        //{
        //    var result = new LearningResultSet();
        //    var currentLearningCoef = coefProcessor.Init(learningCoef);

        //    for (int currentLearningIteration = 0; currentLearningIteration < iterationCount; currentLearningIteration++)
        //    {
        //        for (int i = 0; i < learningDataSet.Count(); i++)
        //        {
        //            currentLearningCoef = coefProcessor.Get(currentLearningCoef, currentLearningIteration * i);
        //            var oneStepResult = OneLearningStep(currentLearningCoef, currentLearningIteration * i, learningDataSet[i]);
        //            result.Add(oneStepResult);
        //        }
        //    }


        //    return result;
        //}

        /// <summary>
        /// Добавляет получаемые прогнозы в окно прогнозирования
        /// </summary>
        public LearningResultSet PrognosticationLearning(int iterationCount, double learningCoef, ILearningCoefProcessor coefProcessor)
        {
            var result = new LearningResultSet();
            var currentLearningCoef = coefProcessor.Init(learningCoef);

            for (int currentLearningIteration = 0; currentLearningIteration < iterationCount; currentLearningIteration++)
            {
                var prognosticFrame = learningDataSet[0].PreviousSet.ToList();
                for (int i = 0; i < learningDataSet.Count(); i++)
                {

                    currentLearningCoef = coefProcessor.Get(currentLearningCoef, currentLearningIteration * i);
                    var oneStepResult = OneLearningStep(currentLearningCoef, currentLearningIteration * i, (prognosticFrame.ToArray(), learningDataSet[i].PrognosticationValue));
                    result.Add(oneStepResult);

                    //Добавляем в конец окна прогнозирования значения, полученные на предыдущем этапе
                    prognosticFrame.AddRange(oneStepResult.NnOutputs);
                    prognosticFrame.RemoveRange(0, oneStepResult.NnOutputs.Length);
                }

            }
            return result;
        }

        private (double Error, double[] NnOutputs, double[] RealOutputs) OneLearningStep(double currentLearningCoef, int currentLearningIteration, (double[] PreviousSet, double[] PrognosticationValue) currentLearningSet)
        { 
            var nnOutput = neuralNetwork.CalculateOutput(currentLearningSet.PreviousSet);
            var error = neuralNetworkParamEditor.Edit(nnOutput, currentLearningSet, currentLearningCoef, currentLearningIteration, errorCalculator);

            return (error, nnOutput, currentLearningSet.PrognosticationValue); ;
        }
    }
}
