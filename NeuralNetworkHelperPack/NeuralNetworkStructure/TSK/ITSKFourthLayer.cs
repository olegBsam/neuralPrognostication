using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkHelperPack.NeuralNetworkStructure.TSK
{
    public interface ITSKFourthLayer
    {
        (double f1, double f2) Calculate(double[] secondLayerOutput, double[] thirdLayerOutput);
    }
}
