using System;
using System.Collections.Generic;
using System.Text;
using SID.Entities.Catalogo;

namespace SID.Entities.Inventario
{
    public class ComprasEntity
    {
        public long Id { get; set; }
        public long Fk_IdRelSucursalProd { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Monto { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public List<RelacionProductoSucursalEntity> ListaProducto { get; set; }
        public RelacionProductoSucursalEntity Producto { get; set; }
    }
}
