using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Mascota
    {
        private int codigo;
        private string nombre;
        private Especie especie;
        private int sexo;
        private DateTime fechaNacimiento;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; } 
        }
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; } 
        }
        public Especie Especie
        {
            get { return especie; }
            set { especie = value; }    
        }
        public int Sexo
        {
            get { return sexo; }
            set { sexo = value; }
        }
        public DateTime FechaNacimiento
        {
            get { return fechaNacimiento; }
            set
            { fechaNacimiento = value; }

        }
     
      

    }
}
