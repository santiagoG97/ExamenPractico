using System;
using System.Collections.Generic;
using System.Text;
using SID.Entities.Catalogo;

namespace SID.Entities.Inventario
{
    public class RelacionProductoSucursalEntity
    {
        public long Id { get; set; }
        public long Fk_IdSucursal { get; set; }
        public long Fk_IdProducto { get; set; }
        public int Cantidad { get; set; }
        public bool Activo { get; set; }
        public byte Selected { get; set; }
        public DateTime FechaRegistro { get; set; }
        public ProductosEntity Producto { get; set; }
        
    }
}
