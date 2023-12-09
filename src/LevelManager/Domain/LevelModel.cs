using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelManager.Domain
{
    public class LevelModel
    {
        public LevelModel(string name, Elevation elevation, BasePointType basePointType)
        {
            Name = name;
            Elevation = elevation;
            BasePointType = basePointType;
        }

        public string Name { get; }
        public Elevation Elevation { get; }
        public BasePointType BasePointType { get;  }
    }
}
