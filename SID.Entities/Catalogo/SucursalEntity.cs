using System;
using System.Collections.Generic;
using System.Text;

namespace SID.Entities.Catalogo
{
    public class SucursalEntity
    {
        public long Id { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
