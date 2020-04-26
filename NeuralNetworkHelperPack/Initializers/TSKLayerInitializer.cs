using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkHelperPack.Initializers
{
    public class TSKLayerInitializer: ITSKLayerInitializer
    {
        public TSKLayerInitializer(
              int m
            , int n
            , ICenterInitializer centerInitializer
            , IRadiusInitializer radiusInitializer
            , IWeightInitializer weightInitializer
            , IPInitializer pInitializer
            )
        {
            M = m;
            N = n;
            CenterInitializer = centerInitializer;
            RadiusInitializer = radiusInitializer;
            WeightInitializer = weightInitializer;
            PInitializer = pInitializer;




            C = new double[M][];
            Sigma = new double[M][];
            B = new double[M][];
            P = new double[M][];

            var rnd = new Random();

            for (int i = 0; i < M; i++)
            {
                C[i] = new double[N];
                Sigma[i] = new double[N];
                B[i] = new double[N];
                P[i] = new double[N + 1];

                for (int j = 0; j < N; j++)
                {
                    C[i][j] = rnd.NextDouble();
                    Sigma[i][j] = rnd.NextDouble();
                    B[i][j] = rnd.NextDouble();
                }
                P[i][P[i].Length - 1] = rnd.NextDouble();
            }
        }

        public int M { get; private set; }
        public int N { get; private set; }
        public double[][] Sigma { get; private set; }
        public double[][] C { get; private set; }
        public double[][] B { get; private set; }
        public double[][] P { get; private set; }

        public ICenterInitializer CenterInitializer { get; private set; }
        public IRadiusInitializer RadiusInitializer { get; private set; }
        public IWeightInitializer WeightInitializer { get; private set; }
        public IPInitializer PInitializer { get; private set; }

        
    }
}
