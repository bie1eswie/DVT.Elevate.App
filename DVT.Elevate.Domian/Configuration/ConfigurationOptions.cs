using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVT.Elevate.Domian.Configuration
{
    public class ConfigurationOptions
    {
        public int NumberOfFloors {  get; set; }
        public int CheckUpdateTime { get; set; }
        public int PassengerLimit {  get; set; }
    }
}
