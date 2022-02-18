using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PM2E10388.Models
{
    public class Sitios
    {
        [PrimaryKey, AutoIncrement]
        public Int32 id { get; set; }

        [MaxLength(100)]
        public String descripcion { get; set; }

        [MaxLength(150)]
        public float latitud { get; set; }

        [MaxLength(150)]
        public float longitud { get; set; }

        public byte[] imagen { get; set; }

    }
}
