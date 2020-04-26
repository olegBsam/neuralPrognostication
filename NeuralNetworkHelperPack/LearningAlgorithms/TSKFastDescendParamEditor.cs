using NeuralNetworkHelperPack.NeuralNetworkStructure.TSK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkHelperPack.LearningAlgorithms
{
    public class TSKFastDescendParamEditor : INeuralNetworkParamEditor
    {
        ITSKNeuralNetwork nn;
        public TSKFastDescendParamEditor(ITSKNeuralNetwork nn)
        {
            this.nn = nn;
        }

        public double Edit(
              double[] nnOutput
            , (double[] PreviousSet, double[] PrognosticationValue) currentLearningSet
            , double currentLearningCoef
            , int currentLearningIteration
            , IErrorCalculator errorCalculator
            )
        {
            var x = currentLearningSet.PreviousSet;
            var d = currentLearningSet.PrognosticationValue[0];
            var y = nnOutput[0];
            var e = Error(y, d);

            double[][] correctionP = CorrectP(currentLearningCoef, currentLearningIteration, x, y, d);
            double[][] correctionC = CorrectC(currentLearningCoef, currentLearningIteration, x, y, d);
            double[][] correctionSigma = CorrectSigma(currentLearningCoef, currentLearningIteration, x, y, d);
            double[][] correctionB = CorrectB(currentLearningCoef, currentLearningIteration, x, y, d);

            SetP(correctionP);
            SetC(correctionC, correctionSigma);


            return errorCalculator.Calculate(nnOutput, currentLearningSet.PrognosticationValue);
        }

       

        private void SetC(double[][] correctionC, double[][] correctionSigma)
        {
            for (int i = 0; i < nn.M; i++)
            {
                nn.FirstLayer.C[i] = correctionC[i];
                nn.FirstLayer.Sigma[i] = correctionSigma[i];
            }
        }

        private void SetP(double[][] correctionP)
        {
            for (int i = 0; i < nn.ThirdLayer.P.Length; i++)
            {
                nn.ThirdLayer.P[i] = correctionP[i];
            }
        }

        private double[][] CorrectB(double currentLearningCoef, int currentLearningIteration, double[] x, double y, double d)
        {
            var newB = init(nn.FirstLayer.B.Length, nn.FirstLayer.B.First().Length);

            for (int bIndexI = 0; bIndexI < nn.M; bIndexI++)
            {
                for (int bIndexJ = 0; bIndexJ < nn.N; bIndexJ++)
                {
                    newB[bIndexI][bIndexJ] = nn.FirstLayer.B[bIndexI][bIndexJ] - currentLearningCoef * dEdB(currentLearningIteration, x, y, d, bIndexI, bIndexJ);
                }
            }
            return newB;
        }

     

        private double[][] CorrectP(double currentLearningCoef, int currentLearningIteration, double[] x, double y, double d)
        {
            var newP = init(nn.ThirdLayer.P.Length, nn.ThirdLayer.P.First().Length);

            for (int pIndexI = 0; pIndexI < nn.M; pIndexI++)
            {
                for (int pIndexJ = 0; pIndexJ < nn.N + 1; pIndexJ++)
                {
                    newP[pIndexI][pIndexJ] = nn.ThirdLayer.P[pIndexI][pIndexJ] - currentLearningCoef * dEdP(currentLearningIteration, x, y, d, pIndexI, pIndexJ);
                }
            }
            return newP;
        }

        private double[][] CorrectC(double currentLearningCoef, int currentLearningIteration, double[] x, double y, double d)
        {
            var newC = init(nn.FirstLayer.C.Length, nn.FirstLayer.C.First().Length);

            for (int cIndexI = 0; cIndexI < nn.M; cIndexI++)
            {
                for (int cIndexJ = 0; cIndexJ < nn.N; cIndexJ++)
                {
                    newC[cIndexI][cIndexJ] = nn.FirstLayer.C[cIndexI][cIndexJ] - currentLearningCoef * dEdC(currentLearningIteration, x, y, d, cIndexI, cIndexJ);
                }
            }
            return newC;
        }

        private double[][] CorrectSigma(double currentLearningCoef, int currentLearningIteration, double[] x, double y, double d)
        {
            var newSigma = init(nn.FirstLayer.C.Length, nn.FirstLayer.C.First().Length);

            for (int sigmaIndexI = 0; sigmaIndexI < nn.M; sigmaIndexI++)
            {
                for (int sigmaIndexJ = 0; sigmaIndexJ < nn.N; sigmaIndexJ++)
                {
                    newSigma[sigmaIndexI][sigmaIndexJ] = nn.FirstLayer.Sigma[sigmaIndexI][sigmaIndexJ] - currentLearningCoef * dEdSigma(currentLearningIteration, x, y, d, sigmaIndexI, sigmaIndexJ);
                }
            }
            return newSigma;
        }

        private double dEdSigma(int currentLearningIteration, double[] x, double y, double d, int sigmaIndexI, int sigmaIndexJ)
        {
            var v = (y - d);
            v = v * SumForR1035(x, sigmaIndexI, sigmaIndexJ);
            v = v * C1036(x, sigmaIndexI, sigmaIndexJ);
            return v;
        }

        private double C1036(double[] x, int sigmaIndexI, int sigmaIndexJ)
        {
            var v = 1.0;

            for (int j = 0; j < nn.N; j++)
            {
                for (int l = 0; l < nn.N; l++)
                {
                    v *= Nu(sigmaIndexI, l, x) * Math.Pow(Nu(sigmaIndexJ, j, x), 2);
                }
            }
            var b = nn.FirstLayer.B[sigmaIndexI][sigmaIndexJ];
            var c = nn.FirstLayer.C[sigmaIndexI][sigmaIndexJ];
            var sigma = nn.FirstLayer.Sigma[sigmaIndexI][sigmaIndexJ];
            v *= 2.0 * b * Math.Pow(Math.Abs(x[sigmaIndexJ] - c), 2.0 * b);
            v = v / sigma;
            v = v / Math.Pow(sigma, 2.0 * b + 1.0);
            return v;

        }

        private double dEdC(int currentLearningIteration, double[] x, double y, double d, int cIndexI, int cIndexJ)
        {
            var v = (y - d);
            v = v * SumForR1035(x, cIndexI, cIndexJ);
            v = v * C1035(x, cIndexI, cIndexJ);
            return v;
        }

        private double dEdB(int currentLearningIteration, double[] x, double y, double d, int bIndexI, int bIndexJ)
        {
            var v = (y - d);
            v = v * SumForR1035(x, bIndexI, bIndexJ);
            v = v * C1037(x, bIndexI, bIndexJ);
            return v;
        }

        private double C1037(double[] x, int bIndexI, int bIndexJ)
        {
            var v = 1.0;

            for (int j = 0; j < nn.N; j++)
            {
                for (int l = 0; l < nn.N; l++)
                {
                    v *= Nu(bIndexI, l, x) * (-2) * Math.Pow(Nu(bIndexI, j, x), 2);
                }
            }

            var b = nn.FirstLayer.B[bIndexI][bIndexJ];
            var c = nn.FirstLayer.C[bIndexI][bIndexJ];
            var sigma = nn.FirstLayer.Sigma[bIndexI][bIndexJ];

            v *= Math.Pow(Math.Abs(x[bIndexJ] - c) / sigma, 2.0 * b);
            v *= Math.Log(Math.Abs(x[bIndexJ] - c) / sigma);
            return v;
        }

        private double C1035(double[] x, int cIndexI, int cIndexJ)
        {
            var v = 1.0;

            for (int j = 0; j < nn.N; j++)
            {
                for (int l = 0; l < nn.N; l++)
                {
                    v *= Nu(cIndexI, l, x) * Math.Pow(Nu(cIndexI, j, x), 2);
                }
            }
            var b = nn.FirstLayer.B[cIndexI][cIndexJ];
            var c = nn.FirstLayer.C[cIndexI][cIndexJ];
            var sigma = nn.FirstLayer.Sigma[cIndexI][cIndexJ];
            v *= 2.0 * b * Math.Pow(Math.Abs(x[cIndexJ] - c), 2.0 * b - 1.0);
            v = v / sigma;
            v = v / Math.Pow(sigma, 2.0 * b);
            return v;
        }

        private double SumForR1035(double[] x, int cIndexI, int cIndexJ)
        {
            var v = 0.0;
            for (int r = 0; r < nn.M; r++)
            {
                v += A1035(cIndexI, cIndexJ, r, x) * B1035(cIndexI, cIndexJ, r, x);
            }
            return v;
        }

        private double B1035(int cIndexI, int cIndexJ, int r, double[] x)
        {
            var v = Kroneker(r, cIndexI);
            v = v * BA1035(cIndexI, cIndexJ, r, x);
            v = v / Math.Pow(SumForIMultiplyNuForJ(x), 2);
            return v;
        }


       

        private double BA1035(int cIndexI, int cIndexJ, int r, double[] x)
        {
            var v = 0.0;
            for (int i = 0; i < nn.M; i++)
            {
                v += MultiplyNuForJ(i, x) - MultiplyNuForJ(r, x);
            }
            return v;
        }

        private double A1035(int cIndexI, int cIndexJ, int r, double[] x)
        {
            var v = nn.ThirdLayer.P[r][0];
            for (int j = 0; j < nn.N; j++)
            {
                v += nn.ThirdLayer.P[r][j] * x[j];
            }
            return v;
        }

        private double Kroneker(int a1, int a2)
        {
            return a1 == a2 ? 1.0 : 0.0;
        }
     


        #region dEdP
        private double dEdP(int currentLearningIteration, double[] x, double y, double d, int pIndexI, int pIndexJ)
        {
            var v = (y - d);
            v = v * MultiplyNuForJ(pIndexI, x);
            v = v * (pIndexJ == 0 ? 1.0 : x[pIndexJ - 1]);
            v = v / SumForIMultiplyNuForJ(x);
            return v;
        }

        private double Nu(int i, int j, double[] x)
        {
            var dx = Math.Abs(x[j] - nn.FirstLayer.C[i][j]);
            dx = dx / nn.FirstLayer.Sigma[i][j];
            var pow = 2.0 * nn.FirstLayer.B[i][j];
            var v = 1.0 / (1.0 + Math.Pow(dx, pow));
            return v;
        }

        private double MultiplyNuForJ(int i, double[] x)
        {
            var v = 1.0;
            if (x.Length != nn.FirstLayer.N) throw new Exception("nn.FirstLayer.N != x.Length");
            for (int index = 0; index < nn.FirstLayer.N; index++)
            {
                v *= Nu(i, index, x);
            }
            return v;
        }

        private double SumForIMultiplyNuForJ(double[] x)
        {
            var v = 0.0;
            for (int i = 0; i < nn.FirstLayer.M; i++)
            {
                v = v + MultiplyNuForJ(i, x);
            }
            return v;
        }
        #endregion

        private double Error(double y, double d)
        {
            var e = 0.5 * Math.Pow(y - d, 2);
            return e;
        }

      

        private double[][] init(int iMax, int jMax)
        {
            double[][] array = new double[iMax][];
            for (int i = 0; i < iMax; i++)
            {
                array[i] = new double[jMax];
            }
            return array;
        }
    }
}
