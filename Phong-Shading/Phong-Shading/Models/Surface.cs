using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phong_Shading.Models
{
    public class Surface
    {
        public double KDirectional { get; set; }
        public double KScattered { get; set; }
        public double Smoothness { get; set; }
    }
}
