﻿namespace NeuralNetworkHelperPack.Initializers
{
    public interface IHiddenLayerInitializer
    {
        void Initialize(int inputVectorDimension, int outputVectorDimension, int hiddenNeuronCount, bool isOffsetNeuron, ICenterInitializer centerInitializer, IRadiusInitializer radiusInitializer, IWeightInitializer weightInitializer, ref double[][] centers, ref double[][] radiuses, ref double[][] weights, out double[] offsetNeuronWeight);
        void Initialize(int inputVectorDimension, int hiddenNeuronCount, bool isOffsetNeuron, ICenterInitializer centerInitializer, IQInitializer radiusInitializer, IWeightInitializer weightInitializer, ref double[][] centers, ref double[][,] q, ref double[] weights, out double offsetNeuronWeight);
    }
}