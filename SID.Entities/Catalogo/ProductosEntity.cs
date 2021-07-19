using System;
using System.Collections.Generic;
using System.Text;

namespace SID.Entities.Catalogo
{
    public class ProductosEntity
    {
        public long Id { get; set; }
        public string Descripcion { get; set; }
        public long CodigoBarras { get; set; }
        public decimal PrecioUnitario { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
