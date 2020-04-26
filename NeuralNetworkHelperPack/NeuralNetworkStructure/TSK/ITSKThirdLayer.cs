using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkHelperPack.NeuralNetworkStructure.TSK
{
    public interface ITSKThirdLayer
    {
        double[][] P { get; }

        double[] Calculate(double[] previousSet, double[] secondLayerOutput, int m, int n);
    }
}
