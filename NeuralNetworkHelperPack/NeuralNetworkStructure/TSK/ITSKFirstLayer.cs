using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkHelperPack.NeuralNetworkStructure.TSK
{
    public interface ITSKFirstLayer
    {
        int M { get; set; }
        int N { get; set; }
        double[][] C { get; set; }
        double[][] Sigma { get; set; }
        double[][] B { get; set; }

        double[][] Calculate(double[] previousSet);
    }
}
