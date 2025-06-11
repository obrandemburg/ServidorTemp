using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteHTTPMonitor
{
    public class Temperatura
    {
        public string unidade { get; set; }
        public double valor { get; set; }

        public override string ToString()
        {
            return $"Unidade: {unidade} || Valor: {valor}";
        }
    }

}
