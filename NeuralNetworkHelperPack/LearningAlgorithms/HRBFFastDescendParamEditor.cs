using Accord.Math;
using NeuralNetworkHelperPack.Functions;
using NeuralNetworkHelperPack.NeuralNetworkStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkHelperPack.LearningAlgorithms
{
    public class HRBFFastDescendParamEditor : INeuralNetworkParamEditor
    {
        private readonly IHRBFNeuralNetwork neuralNetwork;

        public HRBFFastDescendParamEditor(IHRBFNeuralNetwork neuralNetwork)
        {
            this.neuralNetwork = neuralNetwork;
        }

        public double Edit(double[] nnOutput, (double[] PreviousSet, double[] PrognosticationValue) currentLearningSet, double currentLearningCoef, int currentLearningIteration, IErrorCalculator errorCalculator)
        {
            var error = errorCalculator.Calculate(nnOutput, currentLearningSet.PrognosticationValue);

            var difError = nnOutput[0] - currentLearningSet.PrognosticationValue[0];

            var newW = RecalculatedW(difError, currentLearningSet.PreviousSet, currentLearningCoef);
            var newC = RecalculatedC(difError, currentLearningSet.PreviousSet, currentLearningCoef);
            var newQ = RecalculatedQ(difError, currentLearningSet.PreviousSet, currentLearningCoef);
            SetParams(newW, newC, newQ);

            return error;
        }

        private void SetParams(List<double> newW, List<double[]> newC, List<List<List<double>>> newQ)
        {
            if (neuralNetwork.HiddenLayer.IsOffsetNeuron)
            {
                neuralNetwork.HiddenLayer.SetOffsetNeuronsWeight(newW.ElementAt(0));
            }

            for (int i = 0; i < neuralNetwork.HiddenLayer.HiddenNeuronCount; i++)
            {
                var weight = newW[i + 1];
                var center = newC[i];

                var q = new double[newQ[i].Count, newQ[i].Count];
                for (int j = 0; j < newQ[i].Count; j++)
                {
                    for (int r = 0; r < newQ[i].Count; r++)
                    {
                        q[j, r] = newQ[i][j][r];
                    }
                }

                neuralNetwork.HiddenLayer.SetNeuronByIndex(center, q, weight, i);

            }
        }

        private List<double> RecalculatedW(double error, double[] previousSet, double currentLearningCoef)
        {
            var result = new List<double>();
            if (neuralNetwork.HiddenLayer.IsOffsetNeuron)
            {
                result.Add(neuralNetwork.HiddenLayer.GetOffsetNeuronsWeight() - currentLearningCoef * error);
            }

            for (int i = 0; i < neuralNetwork.HiddenLayer.HiddenNeuronCount; i++)
            {
                var neuronParams = neuralNetwork.HiddenLayer.GetNeuronParamByIndex(i);
                var v = neuronParams.Weight - currentLearningCoef * 
                    neuralNetwork.ActivationFunction.dEdWi(previousSet, error, neuronParams);
                result.Add(v);
            }

            return result;
        }

        private List<double[]> RecalculatedC(double error, double[] previousSet, double currentLearningCoef)
        {
            var result = new List<double[]>();

            for (int i = 0; i < neuralNetwork.HiddenLayer.HiddenNeuronCount; i++)
            {
                var c = new double[neuralNetwork.HiddenLayer.InputVectorDimension];
                for (int j = 0; j < neuralNetwork.HiddenLayer.InputVectorDimension; j++)
                {
                    var neuronParams = neuralNetwork.HiddenLayer.GetNeuronParamByIndex(i);
                    c[j] = neuronParams.Center[j] - currentLearningCoef *
                        neuralNetwork.ActivationFunction.dEdCij(previousSet, error, neuronParams, j);
                }
                result.Add(c);
            }

            return result;
        }

        private List<List<List<double>>> RecalculatedQ(double error, double[] previousSet, double currentLearningCoef)
        {
            var result = new List<List<List<double>>>();

            for (int i = 0; i < neuralNetwork.HiddenLayer.HiddenNeuronCount; i++)
            {
                var neuron = neuralNetwork.HiddenLayer.GetNeuronParamByIndex(i);

                result.Add(neuron.Q.GetColumn(0)
                        .Select((_, j) =>
                            neuron.Q.GetRow(j)
                                .Select((q, r) =>
                                    q - currentLearningCoef * neuralNetwork.HiddenLayer.ActivationFunction.dEdQijr(previousSet, error, neuron, j, r))
                                .ToList())
                        .ToList());
            }

            return result;
        }

    }
}
