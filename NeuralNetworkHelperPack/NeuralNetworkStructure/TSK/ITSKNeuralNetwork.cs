using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkHelperPack.NeuralNetworkStructure.TSK
{
    public interface ITSKNeuralNetwork: INeuralNetwork
    {
        /// <summary>
        /// Количество правил вывода
        /// </summary>
        int M { get; set; } 

        /// <summary>
        /// Размерность входного вектора
        /// </summary>
        int N { get; set; }

        TSK.ITSKFirstLayer FirstLayer { get; set; }
        TSK.ITSKSecondLayer SecondLayer { get; set; }
        TSK.ITSKThirdLayer ThirdLayer { get; set; }
        TSK.ITSKFourthLayer FourthLayer { get; set; }
        TSK.ITSKFifthLayer FifthLayer { get; set; }


    }
}
