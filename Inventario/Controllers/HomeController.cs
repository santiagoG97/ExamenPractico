using Inventario.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SID.Bussines.Inventario;
using SID.Entities.Catalogo;
using SID.Entities.Inventario;

using Microsoft.AspNetCore.Http;


namespace Inventario.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(bool tieneMensaje, string Mensaje)
        {
            ViewBag.TieneMensaje = tieneMensaje;
            ViewBag.Mensaje = Mensaje;
            tieneMensaje = false;
            Mensaje = "";
            return View();
        }
        public ActionResult GetListaProductosBySucursal(int IdSucursal)
        {
            List<RelacionProductoSucursalEntity> listaProductos;
            using (InventarioBusiness getProductos = new InventarioBusiness())
            {
                listaProductos = getProductos.GetProductosByIDSucursal(IdSucursal);
            }
            return Json(new { data = listaProductos });
        }
        public ActionResult GetListaSucursal()
        {
            List<SucursalEntity> listaSucursal;
            using (InventarioBusiness getSucursal = new InventarioBusiness())
            {
                listaSucursal = getSucursal.GetListaSucursal();
            }
            return Json(new { data = listaSucursal });
        }
        public JsonResult insertaNuevaSucursal(string NombreSucursal)
        {
            using (InventarioBusiness setSucursal = new InventarioBusiness())
            {
                setSucursal.InsertaSucursal(NombreSucursal);
            }
            return Json("'Success':'true'");
        }
        public JsonResult insertaNuevoProducto(int CodigoBarras, string NombreProducto, decimal Precio)
        {
            if (NombreProducto != null && Precio != 0m) {
                using (InventarioBusiness setSucursal = new InventarioBusiness())
                {
                    setSucursal.InsertaProducto(CodigoBarras, NombreProducto, Precio);
                }
            }
            return Json("'Success':'true'");
        }
        public ActionResult BuscarProductoByParametro(string Parametro)
        {
            List<ProductosEntity> listaProductos;
            using (InventarioBusiness getProductos = new InventarioBusiness())
            {
                listaProductos = getProductos.BuscarProductosByParametro(Parametro);
            }
            return Json(new { data = listaProductos });
        }
        public JsonResult insertaNuevaExistencia(int IdSucursal, int IdProducto, int Cantidad)
        {
            if (IdSucursal != 0 && IdProducto != 0 && Cantidad != 0)
            {
                using (InventarioBusiness setSucursal = new InventarioBusiness())
                {
                    setSucursal.InsertaExistencia(IdSucursal, IdProducto, Cantidad);
                }
            }
            return Json("'Success':'true'");
        }
        public ActionResult GetListaProductosBySucursalCompra(int IdSucursal)
        {
            List<RelacionProductoSucursalEntity> listaProductos;
            using (InventarioBusiness getProductos = new InventarioBusiness())
            {
                listaProductos = getProductos.GetProductosByIDSucursal(IdSucursal);
            }
            return Json(new { data = listaProductos });
        }
        public IActionResult InsertaCompra(ComprasEntity Model)
        {
            string Mensaje = "";
            using (InventarioBusiness setCompra = new InventarioBusiness())
            {
                setCompra.InsertaCompra(Model);
                Mensaje = "¡Compra exitosa!/¡Operación realizada con éxito!/success";
            }
            return RedirectToAction("Index", "Home", new { tieneMensaje = true, Mensaje = Mensaje});
        }
        public JsonResult EliminarProducto(int IdProducto)
        {
            using (InventarioBusiness setCompra = new InventarioBusiness())
            {
                setCompra.EliminarProducto(IdProducto);
            }
            return Json("'Success':'true'");
        }
    }
}
