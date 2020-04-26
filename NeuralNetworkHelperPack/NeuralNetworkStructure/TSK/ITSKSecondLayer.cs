using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkHelperPack.NeuralNetworkStructure.TSK
{
    public interface ITSKSecondLayer
    {
        double[] Calculate(double[][] firstLayerOutput);
    }
}
