using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phong_Shading.Models
{
    public class Surface
    {
        public double KSpecular { get; set; }
        public double KDiffuse { get; set; }
        public double Shininess { get; set; }
    }
}
