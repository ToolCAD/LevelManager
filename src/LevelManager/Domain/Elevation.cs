using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelManager.Domain
{
    public class Elevation
    {
        public double Value { get; }

        public double SimpleValue { get { return Math.Round(Value, 2); } }

        public double RoundedBy(int digits)
        {
            return Math.Round(Value, digits);
        }

        public Elevation(double value)
        {
            Value = value;
        }
    }
}
