using NeuralNetworkHelperPack.NeuralNetworkStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkHelperPack.LearningAlgorithms
{
    public class RBFFastDescendParamEditor : INeuralNetworkParamEditor
    {
        private readonly IRBFNeuralNetwork neuralNetwork;

        public RBFFastDescendParamEditor(IRBFNeuralNetwork neuralNetwork)
        {
            this.neuralNetwork = neuralNetwork;
        }
        public double Edit(double[] nnOutput, (double[] PreviousSet, double[] PrognosticationValue) currentLearningSet, double currentLearningCoef, int currentLearningIteration, IErrorCalculator errorCalculator)
        {
            var error = errorCalculator.Calculate(nnOutput, currentLearningSet.PrognosticationValue);
            var activationFunction = neuralNetwork.ActivationFunction;

            var outputError = CalculateOutputError(nnOutput, currentLearningSet.PreviousSet);
            var outCoef = CalculateWeightedError(nnOutput, currentLearningSet.PrognosticationValue);

            var dEdW0 = new double[neuralNetwork.OutputVectorDimension];
            var dEdW = new double[neuralNetwork.HiddenNeuronsCount, neuralNetwork.OutputVectorDimension];
            var dEdC = new double[neuralNetwork.HiddenNeuronsCount, neuralNetwork.InputVectorDimension];
            var dEdR = new double[neuralNetwork.HiddenNeuronsCount, neuralNetwork.InputVectorDimension];

            dEdW0 = outputError;

            for (int i = 0; i < neuralNetwork.HiddenNeuronsCount; i++)
            {
                for (int s = 0; s < neuralNetwork.OutputVectorDimension; s++)
                {
                    (var c,var r, var w) = neuralNetwork.HiddenLayer.GetNeuronParamByIndex(i, s);
                    dEdW[i, s] = activationFunction.dFdW(c, r, currentLearningSet.PreviousSet) * outputError[s] * currentLearningCoef;
                }
            }

            for (int i = 0; i < neuralNetwork.HiddenNeuronsCount; i++)
            {
                for (int j = 0; j < neuralNetwork.InputVectorDimension; j++)
                {
                    var coef = outCoef[i] * currentLearningCoef;
                    (var c, var r, _) = neuralNetwork.HiddenLayer.GetNeuronParamByIndex(i);
                    dEdC[i, j] = coef * activationFunction.dFdC(c, r, currentLearningSet.PreviousSet, j);
                    dEdR[i, j] = coef * activationFunction.dFdR(c, r, currentLearningSet.PreviousSet, j);
                }
            }

            //Присваивание параметров


            for (int s = 0; s < neuralNetwork.OutputVectorDimension; s++)
            {
                var offsetNeuronWeight = neuralNetwork.HiddenLayer.GetOffsetNeuronsWeight(s);
                neuralNetwork.HiddenLayer.SetOffsetNeuronsWeight(offsetNeuronWeight - currentLearningCoef * outputError[s], s);
            }

            for (int i = 0; i < neuralNetwork.HiddenNeuronsCount; i++)
            {
                for (int s = 0; s < neuralNetwork.OutputVectorDimension; s++)
                {
                    var w = neuralNetwork.HiddenLayer.GetNeuronParamByIndex(i, s).Weight;
                    neuralNetwork.HiddenLayer.SetNeuronByIndex(w - currentLearningCoef * dEdW[i, s], i, s);
                }
            }

            for (int i = 0; i < neuralNetwork.HiddenNeuronsCount; i++)
            {
                (var c, var r, _) = neuralNetwork.HiddenLayer.GetNeuronParamByIndex(i);
                var newC = new double[neuralNetwork.InputVectorDimension];
                var newR = new double[neuralNetwork.InputVectorDimension];
                for (int j = 0; j < neuralNetwork.InputVectorDimension; j++)
                {
                    newC[j] = c[j] - currentLearningCoef * dEdC[i, j];
                    newR[j] = r[j] - currentLearningCoef * dEdR[i, j];
                }
                neuralNetwork.HiddenLayer.SetNeuronByIndex(newC, newR, i);
            }
            return error;
        }

        private double[] CalculateOutputError(double[] nnOutput, double[] PreviousSet)
        {
            var dEdW0 = new double[neuralNetwork.OutputVectorDimension];
            for (int i = 0; i < neuralNetwork.OutputVectorDimension; i++)
            {
                dEdW0[i] = nnOutput[i] - PreviousSet[i];
            }
            return dEdW0;
        }

        private double[] CalculateWeightedError(double[] nnOutput, double[] prognosticationValue)
        {
            var outCoef = new double[neuralNetwork.HiddenNeuronsCount];
            for (int i = 0; i < neuralNetwork.HiddenNeuronsCount; i++)
            {
                outCoef[i] = 0;
                for (int s = 0; s < neuralNetwork.OutputVectorDimension; s++)
                {
                    var w_is = neuralNetwork.HiddenLayer.GetNeuronParamByIndex(i, s).Weight;
                    outCoef[i] += (nnOutput[s] - prognosticationValue[s]) * w_is;
                }
            }
            return outCoef;
        }
    }
}
