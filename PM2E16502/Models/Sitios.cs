using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PM2E16502.Models
{
    public class Sitios
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        public Byte[] foto { get; set; }
        public Double latitud { get; set; }
        public Double longitud { get; set; }
        public string descripcion { get; set; }

    }
}
