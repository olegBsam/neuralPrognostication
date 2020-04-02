using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Environment.CurrentDirectory + "\\" + "data";

            var fileArr = File.ReadAllLines(path).ToList();
            fileArr.RemoveAt(0);

            var list = new List<(string, string, string, DateTime)>();

            foreach (var item in fileArr)
            {
                var strData = item.Split(',');
                var name = strData[0];
                var max = strData[1];
                var count = strData[2];
                var dateStr = strData[3];
                var date = DateTime.Parse(dateStr);
                list.Add((name, max, count, date));
            }

            var list2 = list.GroupBy<(string, string, string, DateTime), DateTime>(o => o.Item4.Date).ToDictionary(o => o.Key, o1 => o1);


        }


    }
}
