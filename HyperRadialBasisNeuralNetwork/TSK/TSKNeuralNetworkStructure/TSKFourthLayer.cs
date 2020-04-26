using NeuralNetworkHelperPack.NeuralNetworkStructure.TSK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperRadialBasisNeuralNetwork.TSK.TSKNeuralNetworkStructure
{
    public class TSKFourthLayer : ITSKFourthLayer
    {
        public (double f1, double f2) Calculate(double[] secondLayerOutput, double[] thirdLayerOutput)
        {
            var f1 = thirdLayerOutput.Sum();
            var f2 = secondLayerOutput.Sum();
            return (f1: f1, f2: f2);
        }
    }
}
