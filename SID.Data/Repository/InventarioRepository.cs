using System;
using System.Collections.Generic;
using System.Text;
using SID.Entities.Catalogo;
using SID.Entities.Inventario;
using SID.Data.StoredProcedures;
using System.Data;
using System.Data.SqlClient;

namespace SID.Data.Repository
{
    public class InventarioRepository : IDisposable
    {
        public void Dispose() {
            GC.SuppressFinalize(this);
        }
        public List<RelacionProductoSucursalEntity> GetProductosByIdSucursal(string DBCnn, int IdSucursal) {
            SqlDataReader DatosReader;
            using (ContextoDB DataObj = new ContextoDB(DBCnn)) {

                SqlParameter[] _Parametros = new SqlParameter[] {
                    new SqlParameter("IdSucursal", SqlDbType.Int){ Value = IdSucursal}
                };

                DatosReader = DataObj.EjecutaSP(InventarioSP.GetProductosByIdSucursal, _Parametros);
                List<RelacionProductoSucursalEntity> listaProductos = new List<RelacionProductoSucursalEntity>();

                while (DatosReader.Read()) {
                    RelacionProductoSucursalEntity objProductos = new RelacionProductoSucursalEntity();
                    objProductos.Id = string.IsNullOrEmpty(DatosReader["Id"].ToString()) ? new long() : Convert.ToInt64(DatosReader["Id"].ToString());
                    objProductos.Producto = new ProductosEntity();
                    objProductos.Producto.Descripcion = DatosReader["Descripcion"].ToString();
                    objProductos.Producto.CodigoBarras = Convert.ToInt64(DatosReader["CodigoBarras"].ToString());
                    objProductos.Cantidad = string.IsNullOrEmpty(DatosReader["Cantidad"].ToString()) ? new int() : Convert.ToInt16(DatosReader["Cantidad"].ToString());
                    objProductos.Producto.PrecioUnitario = string.IsNullOrEmpty(DatosReader["PrecioUnitario"].ToString()) ? new decimal() : Convert.ToDecimal(DatosReader["PrecioUnitario"].ToString());
                    listaProductos.Add(objProductos);
                }
                return listaProductos;
            }
        }
        public List<SucursalEntity> GetListaSucursal(string DBCnn)
        {
            SqlDataReader DatosReader;
            using (ContextoDB DataObj = new ContextoDB(DBCnn))
            {
                DatosReader = DataObj.EjecutaSP(InventarioSP.GetListaSucursal);
                List<SucursalEntity> listaSucursal = new List<SucursalEntity>();

                while (DatosReader.Read())
                {
                    SucursalEntity objSucursal = new SucursalEntity();
                    objSucursal.Id = string.IsNullOrEmpty(DatosReader["Id"].ToString()) ? new long() : Convert.ToInt64(DatosReader["Id"].ToString());
                    objSucursal.Descripcion = DatosReader["Descripcion"].ToString();
                    listaSucursal.Add(objSucursal);
                }
                return listaSucursal;
            }
        }
        public void InsertaSucursal(string DBCnn, string NombreSucursal)
        {
            SqlDataReader DatosReader;
            using (ContextoDB DataObj = new ContextoDB(DBCnn))
            {
                SqlParameter[] _Parametros = new SqlParameter[] {
                    new SqlParameter("nombreSucursal", SqlDbType.VarChar){ Value = NombreSucursal}
                };
                DatosReader = DataObj.EjecutaSP(InventarioSP.InsertaSucursal, _Parametros);
            }
        }
        public void InsertaProducto(string DBCnn, int CodigoBarras, string NombreProducto, decimal Precio)
        {
            SqlDataReader DatosReader;
            using (ContextoDB DataObj = new ContextoDB(DBCnn))
            {
                SqlParameter[] _Parametros = new SqlParameter[] {
                    new SqlParameter("NombreProducto", SqlDbType.VarChar){ Value = NombreProducto},
                    new SqlParameter("CodigoBarras", SqlDbType.Decimal){ Value = CodigoBarras},
                    new SqlParameter("Precio", SqlDbType.Decimal){ Value = Precio}
                };
                DatosReader = DataObj.EjecutaSP(InventarioSP.InsertaProducto, _Parametros);
            }
        }
        public List<ProductosEntity> BuscarProductosByParametro(string DBCnn, string Parametro)
        {
            SqlDataReader DatosReader;
            using (ContextoDB DataObj = new ContextoDB(DBCnn))
            {
                SqlParameter[] _Parametros = new SqlParameter[] {
                    new SqlParameter("Parametro", SqlDbType.VarChar){ Value = Parametro}
                };
                DatosReader = DataObj.EjecutaSP(InventarioSP.BuscarProductoByParametro, _Parametros);
                List<ProductosEntity> listaProductos = new List<ProductosEntity>();

                while (DatosReader.Read())
                {
                    ProductosEntity objProductos = new ProductosEntity();
                    objProductos.Id = string.IsNullOrEmpty(DatosReader["Id"].ToString()) ? new long() : Convert.ToInt64(DatosReader["Id"].ToString());
                    objProductos.CodigoBarras = string.IsNullOrEmpty(DatosReader["CodigoBarras"].ToString()) ? new long() : Convert.ToInt64(DatosReader["CodigoBarras"].ToString());
                    objProductos.Descripcion = DatosReader["Descripcion"].ToString();
                    listaProductos.Add(objProductos);
                }
                return listaProductos;
            }
        }
        public void InsertaExistencia(string DBCnn, int IdSucursal, int IdProducto, int Cantidad)
        {
            SqlDataReader DatosReader;
            using (ContextoDB DataObj = new ContextoDB(DBCnn))
            {
                SqlParameter[] _Parametros = new SqlParameter[] {
                    new SqlParameter("IdSucursal", SqlDbType.Int){ Value = IdSucursal},
                    new SqlParameter("IdProducto", SqlDbType.Int){ Value = IdProducto},
                    new SqlParameter("Cantidad", SqlDbType.Int){ Value = Cantidad}
                };
                DatosReader = DataObj.EjecutaSP(InventarioSP.InsertaExistencia, _Parametros);
            }
        }
        public List<RelacionProductoSucursalEntity> GetProductosByIdSucursalCompra(string DBCnn, int IdSucursal)
        {
            SqlDataReader DatosReader;
            using (ContextoDB DataObj = new ContextoDB(DBCnn))
            {

                SqlParameter[] _Parametros = new SqlParameter[] {
                    new SqlParameter("IdSucursal", SqlDbType.Int){ Value = IdSucursal}
                };

                DatosReader = DataObj.EjecutaSP(InventarioSP.GetProductosByIdSucursalCompra, _Parametros);
                List<RelacionProductoSucursalEntity> listaProductos = new List<RelacionProductoSucursalEntity>();

                while (DatosReader.Read())
                {
                    RelacionProductoSucursalEntity objProductos = new RelacionProductoSucursalEntity();
                    objProductos.Id = string.IsNullOrEmpty(DatosReader["Id"].ToString()) ? new long() : Convert.ToInt64(DatosReader["Id"].ToString());
                    objProductos.Producto = new ProductosEntity();
                    objProductos.Producto.Descripcion = DatosReader["Descripcion"].ToString();
                    objProductos.Producto.CodigoBarras = Convert.ToInt64(DatosReader["CodigoBarras"].ToString());
                    objProductos.Cantidad = string.IsNullOrEmpty(DatosReader["Cantidad"].ToString()) ? new int() : Convert.ToInt16(DatosReader["Cantidad"].ToString());
                    objProductos.Producto.PrecioUnitario = string.IsNullOrEmpty(DatosReader["PrecioUnitario"].ToString()) ? new decimal() : Convert.ToDecimal(DatosReader["PrecioUnitario"].ToString());
                    listaProductos.Add(objProductos);
                }
                return listaProductos;
            }
        }
        public void InsertaCompra(string DBCnn, ComprasEntity Model)
        {
            SqlDataReader DatosReader;
            Model.Producto = new RelacionProductoSucursalEntity();
            long IdSucursal = Model.Producto.Fk_IdSucursal;

            using (ContextoDB DataObj = new ContextoDB(DBCnn))
            {
                foreach (RelacionProductoSucursalEntity entidad in Model.ListaProducto)
                {
                    SqlParameter[] _Parametros = new SqlParameter[] {
                        new SqlParameter("IdRelacion", SqlDbType.Int){ Value = entidad.Id},
                        new SqlParameter("Cantidad", SqlDbType.Int){ Value = entidad.Cantidad},
                        new SqlParameter("Monto", SqlDbType.Decimal){ Value = Model.Monto}
                    };
                    if (entidad.Selected == 1)
                    {
                        DatosReader = DataObj.EjecutaSP(InventarioSP.RegistraCompra, _Parametros);
                    }
                }
            }
        }
        public void EliminaProducto(string DBCnn, int IdProducto)
        {
            SqlDataReader DatosReader;
            using (ContextoDB DataObj = new ContextoDB(DBCnn))
            {
                SqlParameter[] _Parametros = new SqlParameter[] {
                    new SqlParameter("IdProducto", SqlDbType.Int){ Value = IdProducto}
                };
                DatosReader = DataObj.EjecutaSP(InventarioSP.EliminaProducto, _Parametros);
            }
        }
    }
}
