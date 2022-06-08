using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class EspecieNegocio
    {
        public List <Especie> listar()
        {
            List<Especie> lista = new List<Especie>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.settearConsulta("select idEspecie,nombreEspecie from especies order by 2");
                datos.ejecutarLectura();
                while(datos.Lector.Read())
                {
                    Especie especie = new Especie();
                    if (!(datos.Lector["idEspecie"] is DBNull))
                        especie.IdEspecie = (int)datos.Lector["idEspecie"];

                    if (!(datos.Lector["nombreEspecie"] is DBNull))
                        especie.NombreEspecie = (string)datos.Lector["nombreEspecie"];

                    lista.Add(especie); 

                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
       
    }
}
