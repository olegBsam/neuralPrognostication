using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkHelperPack.Initializers
{
    public interface ITSKLayerInitializer
    {
        int M { get; }
        int N { get; }
        ICenterInitializer CenterInitializer { get; }
        IRadiusInitializer RadiusInitializer { get; }
        IWeightInitializer WeightInitializer { get; }
        double[][] Sigma { get; }
        double[][] C { get; }
        double[][] B { get; }
        double[][] P { get; }
    }
}
