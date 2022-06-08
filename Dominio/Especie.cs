using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Especie
    {
        private int idEspecie;
        private string nombreEspecie;

        public int IdEspecie
        {
            get { return idEspecie; }
            set { idEspecie = value; }  
        }
        public string NombreEspecie
        {
            set { nombreEspecie = value; }  
            get { return nombreEspecie; }
        }
        public override string ToString()
        {
            return nombreEspecie.ToString();
        }
    }
}
