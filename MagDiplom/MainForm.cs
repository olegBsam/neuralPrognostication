using HyperRadialBasisNeuralNetwork;
using HyperRadialBasisNeuralNetwork.HRBF.NeuralNetworkStructure;
using HyperRadialBasisNeuralNetwork.NeuralNetworkStructure;
using HyperRadialBasisNeuralNetwork.TSK;
using HyperRadialBasisNeuralNetwork.TSK.TSKNeuralNetworkStructure;
using NeuralNetworkHelperPack.Functions;
using NeuralNetworkHelperPack.Functions.HRBF;
using NeuralNetworkHelperPack.Initializers;
using NeuralNetworkHelperPack.LearningAlgorithms;
using NeuralNetworkHelperPack.NeuralNetworkStructure;
using NeuralNetworkHelperPack.NeuralNetworkStructure.TSK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagDiplom
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        INeuralNetwork nn;

        int rbfCountGlobal = 23;
        int inputCountGlobal = 4;
        int learningIterCountGlobal = 7000;
        int volumeGlobal = 7;

        double learningCoefGlobal = 0.04;

        private async void button1_Click(object sender, EventArgs e)
        {
            //HRBF(rbfCountGlobal, inputCountGlobal, learningIterCountGlobal, volumeGlobal, learningCoefGlobal);
            HRBF(rbfCountGlobal, inputCountGlobal, learningIterCountGlobal, volumeGlobal, learningCoefGlobal);
           // tsk(rbfCountGlobal, inputCountGlobal, learningIterCountGlobal, volumeGlobal, learningCoefGlobal);

            var t = 12;
        }

        public async void HRBF(int rbfCount, int inputCount, int learningIterCount, int volume, double learningCoef)
        {
            button1.Enabled = false;
            //await new TaskFactory().StartNew(() =>
            {
                var file = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\airpass.dat")
                    .Select(o => double.Parse(o))
                    .ToList();
                var max = file.Max();

                file = file.Select(o => o / max)
                    .ToList();


                IHRBFHiddenLayer hiddenLayer = new HyperRadialBasisHiddenLayer(
                      rbfCount
                    , inputCount
                    , 1
                    , null
                    , null
                    , null
                    , new RandomHiddenLayerInitializer()
                    , true
                    , new HRBFActivationFunction()
                    );
                nn = new HyperRadialBasisNeuralNetwork.HyperRadialBasisNeuralNetwork(hiddenLayer);

                var backPropLearningAlgorithm = new BackPropLearningAlgorithm(
                      nn
                    , file.ToArray()
                    , new NonePreprocessor()
                    , new ErrorCalculator()
                    , new HRBFFastDescendParamEditor((IHRBFNeuralNetwork)nn)
                    );

                var learningResult = backPropLearningAlgorithm.Learning(learningIterCount, learningCoef, new SimpleLearningCoefProcessor());

                (var real, var test) = backPropLearningAlgorithm.Test(volume);

                var y = real.SelectMany(o => o).ToList();
                var d = test.SelectMany(o => o).ToList();
                var tailError = Tail(y.ToArray(), d.ToArray());
                var maeError = Mae(y.ToArray(), d.ToArray());


                var errors = learningResult.Select(o => o + "").ToList();
                errors.Add("");
                errors.Add("Tail");
                errors.Add(tailError + "");
                errors.Add("MAE");
                errors.Add(maeError + "");



                File.WriteAllLines("C:\\Users\\Олег\\Desktop\\МАГИСТРАТУРА ДИПЛОМ\\MagDiplom\\neuralPrognostication\\neuralPrognostication\\MagDiplom\\bin\\Debug\\Hrbf\\Errors.txt", errors);
                File.WriteAllLines("C:\\Users\\Олег\\Desktop\\МАГИСТРАТУРА ДИПЛОМ\\MagDiplom\\neuralPrognostication\\neuralPrognostication\\MagDiplom\\bin\\Debug\\Hrbf\\d.txt", d.Select(o => o + "").ToArray());
                File.WriteAllLines("C:\\Users\\Олег\\Desktop\\МАГИСТРАТУРА ДИПЛОМ\\MagDiplom\\neuralPrognostication\\neuralPrognostication\\MagDiplom\\bin\\Debug\\Hrbf\\y.txt", y.Select(o => o + "").ToArray());


                var t = 12;


                //chart1.Invoke((MethodInvoker)delegate ()
                //{

                //    chart1.Series.Add("error");
                //    chart1.Series["error"].Points.DataBindY(errors);
                //    chart1.Width = learningResult.Count() * 5;
                //});

                //chart1.Invoke((MethodInvoker)delegate ()
                //{

                //    chart1.Series.Add("real");
                //    chart1.Series.Add("test");
                //    chart1.Series["test"].Points.DataBindY(testSet);
                //    chart1.Series["real"].Points.DataBindY(realDataSet);
                //    chart1.Series["real"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                //    chart1.Series["test"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                //    chart1.Width = testSet.Count() * 5;
                //    button1.Enabled = true;
                //});
            }
                //    );
        }


        public async void RBF(int rbfCount, int inputCount, int learningIterCount, int volume, double learningCoef)
        {
            button1.Enabled = false;
            //await new TaskFactory().StartNew(() =>
            {
                var file = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\airpass.dat")
                    .Select(o => double.Parse(o))
                    .ToList();
                var max = file.Max();

                file = file.Select(o => o / max)
                    .ToList();


                IRBFHiddenLayer hiddenLayer = new RadialBasisHiddenLayer(
                      rbfCount
                    , inputCount
                    , 1
                    , null
                    , null
                    , null
                    , new RandomHiddenLayerInitializer()
                    , true
                    , new GaussActivationFunction()
                    );
                nn = new RadialBasisNeuralNetwork(hiddenLayer);

                var backPropLearningAlgorithm = new BackPropLearningAlgorithm(
                      nn
                    , file.ToArray()
                    , new NonePreprocessor()
                    , new ErrorCalculator()
                    , new RBFFastDescendParamEditor((IRBFNeuralNetwork)nn)
                    );

                var learningResult = backPropLearningAlgorithm.Learning(learningIterCount, learningCoef, new SimpleLearningCoefProcessor());

                (var real, var test) = backPropLearningAlgorithm.Test(volume);

                var y = real.SelectMany(o => o).ToList();
                var d = test.SelectMany(o => o).ToList();
                var tailError = Tail(y.ToArray(), d.ToArray());
                var maeError = Mae(y.ToArray(), d.ToArray());


                var errors = learningResult.Select(o => o + "").ToList();
                errors.Add("");
                errors.Add("Tail");
                errors.Add(tailError + "");
                errors.Add("MAE");
                errors.Add(maeError + "");



                File.WriteAllLines("C:\\Users\\Олег\\Desktop\\МАГИСТРАТУРА ДИПЛОМ\\MagDiplom\\neuralPrognostication\\neuralPrognostication\\MagDiplom\\bin\\Debug\\Rbf\\Errors.txt", errors);
                File.WriteAllLines("C:\\Users\\Олег\\Desktop\\МАГИСТРАТУРА ДИПЛОМ\\MagDiplom\\neuralPrognostication\\neuralPrognostication\\MagDiplom\\bin\\Debug\\Rbf\\d.txt", d.Select(o => o + "").ToArray());
                File.WriteAllLines("C:\\Users\\Олег\\Desktop\\МАГИСТРАТУРА ДИПЛОМ\\MagDiplom\\neuralPrognostication\\neuralPrognostication\\MagDiplom\\bin\\Debug\\Rbf\\y.txt", y.Select(o => o + "").ToArray());


                var t = 12;


                //chart1.Invoke((MethodInvoker)delegate ()
                //{

                //    chart1.Series.Add("error");
                //    chart1.Series["error"].Points.DataBindY(errors);
                //    chart1.Width = learningResult.Count() * 5;
                //});

                //chart1.Invoke((MethodInvoker)delegate ()
                //{

                //    chart1.Series.Add("real");
                //    chart1.Series.Add("test");
                //    chart1.Series["test"].Points.DataBindY(testSet);
                //    chart1.Series["real"].Points.DataBindY(realDataSet);
                //    chart1.Series["real"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                //    chart1.Series["test"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                //    chart1.Width = testSet.Count() * 5;
                //    button1.Enabled = true;
                //});
            }
                //    );
        }



        /// <summary>
        /// TSK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void tsk(int rbfCount, int inputCount, int learningIterCount, int volume, double learningCoef)
        {
            button1.Enabled = false;
           // await new TaskFactory().StartNew(() =>
            {
                var file = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\airpass.dat")
                    .Select(o => double.Parse(o))
                    .ToList();
                var max = file.Max();

                file = file.Select(o => o / max)
                    .ToList();

                var tksInit = new TSKLayerInitializer(
                        m: rbfCount
                      , n: inputCount
                      , null
                      , null
                      , null
                      , null);

                ITSKFirstLayer tskFirstLayer = new TSKFirstLayer(tksInit);
                ITSKSecondLayer tSKSecondLayer = new TSKSecondLayer(tksInit);
                ITSKThirdLayer tSKThirdLayer = new TSKThirdLayer(tksInit);
                ITSKFourthLayer tSKFourthLayer = new TSKFourthLayer();
                ITSKFifthLayer tSKFifthLayer = new TSKFifthLayer();

                nn = new TSKNeuralNetwork(
                      tskFirstLayer
                    , tSKSecondLayer
                    , tSKThirdLayer
                    , tSKFourthLayer
                    , tSKFifthLayer
                    );

                var backPropLearningAlgorithm = new BackPropLearningAlgorithm(
                      nn
                    , file.ToArray()
                    , new NonePreprocessor()
                    , new ErrorCalculator()
                    , new TSKFastDescendParamEditor((ITSKNeuralNetwork)nn)
                    );


                var learningResult = backPropLearningAlgorithm.Learning(learningIterCount
                    , learningCoef, new SimpleLearningCoefProcessor());

                (var real, var test) = backPropLearningAlgorithm.Test(volume);

                var y = real.SelectMany(o => o).ToList();
                var d = test.SelectMany(o => o).ToList();
                var tailError = Tail(y.ToArray(), d.ToArray());
                var maeError = Mae(y.ToArray(), d.ToArray());


                var errors = learningResult.Select(o => o + "").ToList();
                errors.Add("");
                errors.Add("Tail");
                errors.Add(tailError + "");
                errors.Add("MAE");
                errors.Add(maeError + "");



                File.WriteAllLines("C:\\Users\\Олег\\Desktop\\МАГИСТРАТУРА ДИПЛОМ\\MagDiplom\\neuralPrognostication\\neuralPrognostication\\MagDiplom\\bin\\Debug\\Tsk\\Errors.txt", errors);
                File.WriteAllLines("C:\\Users\\Олег\\Desktop\\МАГИСТРАТУРА ДИПЛОМ\\MagDiplom\\neuralPrognostication\\neuralPrognostication\\MagDiplom\\bin\\Debug\\Tsk\\d.txt", d.Select(o => o + "").ToArray());
                File.WriteAllLines("C:\\Users\\Олег\\Desktop\\МАГИСТРАТУРА ДИПЛОМ\\MagDiplom\\neuralPrognostication\\neuralPrognostication\\MagDiplom\\bin\\Debug\\Tsk\\y.txt", y.Select(o => o + "").ToArray());


                var t = 12;


                //chart1.Invoke((MethodInvoker)delegate ()
                //{

                //    chart1.Series.Add("error");
                //    chart1.Series["error"].Points.DataBindY(errors);
                //    chart1.Width = learningResult.Count() * 5;
                //});

                //chart1.Invoke((MethodInvoker)delegate ()
                //{

                //    chart1.Series.Add("real");
                //    chart1.Series.Add("test");
                //    chart1.Series["test"].Points.DataBindY(testSet);
                //    chart1.Series["real"].Points.DataBindY(realDataSet);
                //    chart1.Series["real"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                //    chart1.Series["test"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                //    chart1.Width = testSet.Count() * 5;
                //    button1.Enabled = true;
                //});
            }
        //    );
        }

        public double Mae(double[] y, double[] d)
        {
            var v = 0.0;
            for (int i = 0; i < y.Length; i++)
            {
                v += Math.Abs(d[i] - y[i]);
            }
            return v / y.Length;
        }

        public double Tail(double[] y, double[] d)
        {
            var v1 = 0.0;
            for (int i = 0; i < y.Length; i++)
            {
                v1 += Math.Pow(y[i] - d[i], 2);
            }
            var v2 = 0.0;
            for (int i = 0; i < y.Length; i++)
            {
                v2 += Math.Pow(y[i], 2);
            }
            var v3 = 0.0;
            for (int i = 0; i < y.Length; i++)
            {
                v3 += Math.Pow(d[i], 2);
            }

            return v1 / (v2 + v3);
        }
    }
}
