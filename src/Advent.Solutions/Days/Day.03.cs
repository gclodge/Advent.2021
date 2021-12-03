using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Advent.Solutions.Days
{
    internal class Counter
    {
        public int On { get; set; } = 0;
        public int Off { get; set; } = 0;

        public bool AreEqual => On == Off;

        public string GetGammaValue()
        {
            return (On > Off) ? "1" : "0";
        }

        public string GetEpsilonValue()
        {
            return (On > Off) ? "0" : "1";
        }
    }

    public class SubmarineDiagnostic
    {
        public int NumberLength { get; }
        public List<string> BinaryNumbers { get; }

        Dictionary<int, Counter> _Counters;

        public int Power => Gamma * Epsilon;
        public int LifeSupport => Oxygen * CarbonDioxide;

        public int Gamma => CalculateValue(GammaString);
        public int Epsilon => CalculateValue(EpsilonString);
        public int Oxygen => CalculateValue(OxygenString);
        public int CarbonDioxide => CalculateValue(CarbonDioxideString);

        public string? GammaString { get; private set; }
        public string? EpsilonString { get; private set; }
        public string? OxygenString { get; private set; }
        public string? CarbonDioxideString { get; private set; }

        public SubmarineDiagnostic(IEnumerable<string> vals)
        {
            this.BinaryNumbers = vals.ToList();
            this.NumberLength = BinaryNumbers.First().Length;

            _Counters = Enumerable.Range(0, NumberLength)
                                  .ToDictionary(i => i, i => GetCounter(BinaryNumbers, i));
        }

        public SubmarineDiagnostic GetGammaAndEpsilon()
        {
            this.GammaString = "";
            this.EpsilonString = "";
            //< Compose the strings, m'lord
            foreach (int i in Enumerable.Range(0, NumberLength))
            {
                GammaString += _Counters[i].GetGammaValue();
                EpsilonString += _Counters[i].GetEpsilonValue();
            }
            return this;
        }

        public SubmarineDiagnostic GetOxygenAndCarbonDioxide()
        {
            this.OxygenString = "";
            this.CarbonDioxideString = "";

            var oVals = this.BinaryNumbers.ToList();
            var cVals = this.BinaryNumbers.ToList();

            foreach (int i in Enumerable.Range(0, NumberLength))
            {
                if (oVals.Count > 1)
                {
                    var counter = GetCounter(oVals, i);
                    char gVal = (counter.AreEqual) ? '1' : counter.GetGammaValue()[0];
                    oVals = oVals.Where(x => x[i] == gVal).ToList();
                }
                
                if (cVals.Count > 1)
                {
                    var counter = GetCounter(cVals, i);
                    char eVal = (counter.AreEqual) ? '0' : counter.GetEpsilonValue()[0];
                    cVals = cVals.Where(x => x[i] == eVal).ToList();
                }
            }

            this.OxygenString = oVals.Single();
            this.CarbonDioxideString = cVals.Single();

            return this;
        }

        static Counter GetCounter(IEnumerable<string> numbers, int idx)
        {
            var vals = numbers.Select(x => x[idx]);
            return new Counter()
            {
                On = vals.Count(c => c == '1'),
                Off = vals.Count(c => c == '0')
            };
        }

        static int CalculateValue(string str)
        {
            int res = 0;
            int binaryIndex;
            foreach (int i in Enumerable.Range(0, str.Length))
            {
                binaryIndex = (str.Length - 1) - i;
                if (str[i] == '1')
                {
                    res += (int)Math.Pow(2, binaryIndex);
                }
            }
            return res;
        }
    }
}
