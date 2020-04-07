using HyperRadialBasisNeuralNetwork;
using HyperRadialBasisNeuralNetwork.HRBF.NeuralNetworkStructure;
using HyperRadialBasisNeuralNetwork.NeuralNetworkStructure;
using NeuralNetworkHelperPack.Functions;
using NeuralNetworkHelperPack.Functions.HRBF;
using NeuralNetworkHelperPack.LearningAlgorithms;
using NeuralNetworkHelperPack.NeuralNetworkStructure;
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

        IHRBFNeuralNetwork nn;


        //private async void button1_Click(object sender, EventArgs e)
        //{

        //    var t = BitConverter.ToInt32(new byte[] { 248, 1, 0, 0 }, 0);

        //    button1.Enabled = false;
        //    await new TaskFactory().StartNew(() =>
        //    {
        //        var file = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\airpass.dat")
        //            .Select(o => double.Parse(o))
        //            .ToList();
        //        var max = file.Max();

        //        file = file.Select(o => o / max)
        //            .ToList();


        //        IRBFHiddenLayer hiddenLayer = new RadialBasisHiddenLayer(
        //              23
        //            , 3
        //            , 1
        //            , null
        //            , null
        //            , null
        //            , new RandomHiddenLayerInitializer()
        //            , true
        //            , new GaussActivationFunction()
        //            );
        //        nn = new RadialBasisNeuralNetwork(hiddenLayer);

        //        var backPropLearningAlgorithm = new BackPropLearningAlgorithm(
        //              nn
        //            , file.ToArray()
        //            , new NonePreprocessor()
        //            , new ErrorCalculator()
        //            , new RBFFastDescendParamEditor(nn)
        //            );

        //        backPropLearningAlgorithm.Learning(7000, 0.004, new SimpleLearningCoefProcessor());
        //        (var real, var test) = backPropLearningAlgorithm.Test();
        //        var t1 = real.SelectMany(o => o).ToList();
        //        var r1 = test.SelectMany(o => o).ToList();

        //        chart1.Invoke((MethodInvoker)delegate ()
        //        {

        //            chart1.Series.Add("real");
        //            chart1.Series.Add("test");
        //            chart1.Series["test"].Points.DataBindY(t1);
        //            chart1.Series["real"].Points.DataBindY(r1);
        //            chart1.Series["real"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
        //            chart1.Series["test"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
        //            chart1.Width = t1.Count() * 5;
        //            button1.Enabled = true;
        //        });
        //    }
        //    );
        //}

        private async void button1_Click(object sender, EventArgs e)
        {

            var t = BitConverter.ToInt32(new byte[] { 248, 1, 0, 0 }, 0);

            button1.Enabled = false;
            await new TaskFactory().StartNew(() =>
            {
                var file = File.ReadAllLines(Directory.GetCurrentDirectory() + "\\airpass.dat")
                    .Select(o => double.Parse(o))
                    .ToList();
                var max = file.Max();

                file = file.Select(o => o / max)
                    .ToList();


                IHRBFHiddenLayer hiddenLayer = new HyperRadialBasisHiddenLayer(
                      28
                    , 4
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
                    , new HRBFFastDescendParamEditor(nn)
                    );

                backPropLearningAlgorithm.Learning(800, 0.004, new SimpleLearningCoefProcessor());
                (var real, var test) = backPropLearningAlgorithm.Test();
                var t1 = real.SelectMany(o => o).ToList();
                var r1 = test.SelectMany(o => o).ToList();

                chart1.Invoke((MethodInvoker)delegate ()
                {

                    chart1.Series.Add("real");
                    chart1.Series.Add("test");
                    chart1.Series["test"].Points.DataBindY(t1);
                    chart1.Series["real"].Points.DataBindY(r1);
                    chart1.Series["real"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                    chart1.Series["test"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                    chart1.Width = t1.Count() * 5;
                    button1.Enabled = true;
                });
            }
            );
        }
    }
}
