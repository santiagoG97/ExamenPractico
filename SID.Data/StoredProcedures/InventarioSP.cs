using System;
using System.Collections.Generic;
using System.Text;

namespace SID.Data.StoredProcedures
{
    public class InventarioSP
    {
        public static string GetProductosByIdSucursal = "[Inventario].[spObtenerProductosByIdSucursal]";
        public static string GetListaSucursal = "[Inventario].[spObtenerListaSucursal]";
        public static string InsertaSucursal = "[Inventario].[spInsertaSucursal]";
        public static string InsertaProducto = "[Inventario].[spInsertaProducto]";
        public static string BuscarProductoByParametro = "[Inventario].[spBuscarProductoByParametro]";
        public static string InsertaExistencia = "[Inventario].[spInsertaExistencia]";
        public static string RegistraCompra = "[Inventario].[spRegistraCompra]";
        public static string GetProductosByIdSucursalCompra = "[Inventario].[spObtenerProductosByIdSucursalCompra]";
        public static string EliminaProducto = "[Inventario].[spEliminarProducto]";
    }
}
