using NeuralNetworkHelperPack.NeuralNetworkStructure.TSK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperRadialBasisNeuralNetwork.TSK.TSKNeuralNetworkStructure
{
    public class TSKFifthLayer : ITSKFifthLayer
    {
        public double Calculate(double f1, double f2)
        {
            return f1 / f2;
        }
    }
}
