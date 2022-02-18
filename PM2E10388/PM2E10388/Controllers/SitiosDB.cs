using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.IO;
using System.Threading.Tasks;
using PM2E10388.Models;

namespace PM2E10388.Controllers
{
    public class SitiosDB
    {
        readonly SQLiteAsyncConnection db;

        // Constructor vacio
        public SitiosDB()
        {
        }
        // Constructor con parametros
        public SitiosDB(String pathbasedatos)
        {
            db = new SQLiteAsyncConnection(pathbasedatos);
            // Creamos las tablas de la base de datos
            db.CreateTableAsync<Sitios>();
        }

        // Procedimientos y funciones CRUD
        public Task<List<Sitios>> listaSitios()
        {
            return db.Table<Sitios>().ToListAsync();
        }

        // Buscar persona por ID
        public Task<Sitios> ObtenerSitio(int pid)
        {
            return db.Table<Sitios>()
                .Where(i => i.id == pid)
                .FirstOrDefaultAsync();
        }

        // Guardar o actualizar persona
        public Task<Int32> SitioGuardar(Sitios sitio)
        {
            if (sitio.id != 0)
            {
                return db.UpdateAsync(sitio);
            }
            else
            {
                return db.InsertAsync(sitio);
            }
        }

        // Eliminar persona
        public Task<Int32> SitioEliminar(Sitios sitio)
        {
            return db.DeleteAsync(sitio);
        }

        public Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        // Obtener Latitud de UbicacionesDB
        public Task<Sitios> ObtenerLongitud(float uLongitud)
        {
            return db.Table<Sitios>().Where(i => i.longitud == uLongitud).FirstOrDefaultAsync();
        }

        // Obtener Latitud de UbicacionesDB
        public Task<Sitios> ObtenerLatitud(float uLatitud)
        {
            return db.Table<Sitios>().Where(i => i.latitud == uLatitud).FirstOrDefaultAsync();
        }

        // Obtener Descripcion de UbicacionesDB
        public Task<Sitios> ObtenerDescripcion(String uDescripcion)
        {
            return db.Table<Sitios>().Where(i => i.descripcion == uDescripcion).FirstOrDefaultAsync();
        }

    }
}
