using System;
using System.Collections.Generic;
using System.Text;
using SID.Entities.Catalogo;
using SID.Entities.Inventario;
using SID.Bussines.Common;
using SID.Data.Repository;

namespace SID.Bussines.Inventario
{
    public class InventarioBusiness: IDisposable 
    {
        public void Dispose() {
            GC.SuppressFinalize(this);
        }
        public List<RelacionProductoSucursalEntity> GetProductosByIDSucursal(int IdSucursal) {
            using (InventarioRepository objDBD = new InventarioRepository()) {
                var resutado = objDBD.GetProductosByIdSucursal(DBSet.DBcnn, IdSucursal);
                objDBD.Dispose();
                return resutado;
            }
        }
        public List<SucursalEntity> GetListaSucursal()
        {
            using (InventarioRepository objDBD = new InventarioRepository())
            {
                var lista = objDBD.GetListaSucursal(DBSet.DBcnn);
                objDBD.Dispose();
                return lista;
            }
        }
        public void InsertaSucursal(string NombreSucursal)
        {
            using (InventarioRepository objDBD = new InventarioRepository())
            {
                objDBD.InsertaSucursal(DBSet.DBcnn, NombreSucursal);
                objDBD.Dispose();
            }
        }
        public void InsertaProducto(int CodigoBarras, string NombreProducto, decimal Precio)
        {
            using (InventarioRepository objDBD = new InventarioRepository())
            {
                objDBD.InsertaProducto(DBSet.DBcnn, CodigoBarras, NombreProducto, Precio);
                objDBD.Dispose();
            }
        }
        public List<ProductosEntity> BuscarProductosByParametro(string Parametro)
        {
            using (InventarioRepository objDBD = new InventarioRepository())
            {
                var lista = objDBD.BuscarProductosByParametro(DBSet.DBcnn, Parametro);
                objDBD.Dispose();
                return lista;
            }
        }
        public void InsertaExistencia(int IdSucursal, int IdProducto, int Cantidad)
        {
            using (InventarioRepository objDBD = new InventarioRepository())
            {
                objDBD.InsertaExistencia(DBSet.DBcnn, IdSucursal, IdProducto, Cantidad);
                objDBD.Dispose();
            }
        }
        public List<RelacionProductoSucursalEntity> GetProductosByIDSucursalCompra(int IdSucursal)
        {
            using (InventarioRepository objDBD = new InventarioRepository())
            {
                var resutado = objDBD.GetProductosByIdSucursalCompra(DBSet.DBcnn, IdSucursal);
                objDBD.Dispose();
                return resutado;
            }
        }
        public void InsertaCompra(ComprasEntity Model)
        {
            using (InventarioRepository objDBD = new InventarioRepository())
            {
                objDBD.InsertaCompra(DBSet.DBcnn, Model);
                objDBD.Dispose();
            }
        }
        public void EliminarProducto(int IdProducto)
        {
            using (InventarioRepository objDBD = new InventarioRepository())
            {
                objDBD.EliminaProducto(DBSet.DBcnn, IdProducto);
                objDBD.Dispose();
            }
        }
    }
}
