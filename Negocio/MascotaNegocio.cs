using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class MascotaNegocio
    {
        public List<Mascota>listar()
        {
            List<Mascota> lista = new List<Mascota>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.settearConsulta("select codigo,nombre,m.especie idEspecie,e.nombreEspecie nombreEspecie,sexo,fechaNacimiento from Mascotas M , Especies E where m.especie=e.idEspecie");
                datos.ejecutarLectura();
                while(datos.Lector.Read())
                {
                    Mascota mascota = new Mascota();

                    if (!(datos.Lector["codigo"] is DBNull))
                        mascota.Codigo = (int)datos.Lector["codigo"];

                    if (!(datos.Lector["nombre"] is DBNull))
                        mascota.Nombre = (string)datos.Lector["nombre"];

                    mascota.Especie = new Especie();

                    if (!(datos.Lector["idEspecie"] is DBNull))
                        mascota.Especie.IdEspecie = (int)datos.Lector["idEspecie"];

                    if (!(datos.Lector["nombreEspecie"] is DBNull))
                        mascota.Especie.NombreEspecie = (string)datos.Lector["nombreEspecie"];

                    if (!(datos.Lector["sexo"] is DBNull))
                        mascota.Sexo = (int)datos.Lector["sexo"];

                    if (!(datos.Lector["fechaNacimiento"] is DBNull))
                        mascota.FechaNacimiento = (DateTime)datos.Lector["fechaNacimiento"];

                    lista.Add(mascota);
                }
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void agregar(Mascota nueva)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.settearConsulta("insert into Mascotas(codigo,nombre,especie,sexo,fechaNacimiento) values (@codigo,@nombre,@idEspecie,@sexo,@fechaNacimiento)");
                datos.settearParametros("@codigo", nueva.Codigo);
                datos.settearParametros("@nombre", nueva.Nombre);
                datos.settearParametros("@idEspecie", nueva.Especie.IdEspecie);
                datos.settearParametros("@sexo", nueva.Sexo);
                datos.settearParametros("@fechaNacimiento", nueva.FechaNacimiento);
                datos.ejecutarAccion();


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
        public void modificar(Mascota modificada)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.settearConsulta("update Mascotas set nombre=@nombre, especie=@idEspecie, sexo=@sexo, fechaNacimiento=@fechaNacimiento where codigo=@codigo");
                datos.settearParametros("@nombre", modificada.Nombre);
                datos.settearParametros("@idEspecie", modificada.Especie.IdEspecie);
                datos.settearParametros("@sexo", modificada.Sexo);
                datos.settearParametros("@fechaNacimiento", modificada.FechaNacimiento);
                datos.settearParametros("@codigo", modificada.Codigo);
                datos.ejecutarAccion();

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
        
        public void eliminar (int codigo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.settearConsulta("delete from Mascotas where codigo=@codigo ");
                datos.settearParametros("@codigo",codigo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
