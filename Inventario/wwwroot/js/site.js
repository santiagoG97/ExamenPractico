var monto = 0;
function cargarProductosTablaPrincipal(idSucursal) {
    $("#table_Products").dataTable().fnDestroy();
    $("#table_Products tbody").empty();
    monto = 0;
    document.getElementById('lvlMonto').innerHTML = ' ' + monto + 'MN';
    document.getElementById('btnCancelarCompra').style.display = 'none';
    document.getElementById('btnRealizarCompra').style.display = 'none';
    $.ajax({
        type: "GET",
        url: "/Home/GetListaProductosBySucursal",
        data: "IdSucursal=" + idSucursal,
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data) {
            if (!$.isEmptyObject(data.data)) {
                var identChk = 0;
                $.each(data.data, function (key, entidad) {
                    var tr = '<tr>'
                        + '<td>' + entidad.producto.codigoBarras + '</td>'
                        + '<td>' + entidad.producto.descripcion + '</td>'
                        + '<td>' + entidad.cantidad + '</td>'
                        + '<td>' + entidad.producto.precioUnitario + ' MN</td>'
                        + '<td><input type="checkbox" id="cbox' + identChk + '" value="1" disabled> </td>'
                        + '</tr>';
                    $("#table_Products tbody").append(tr);
                    identChk += 1;
                });
                $("#table_Products").dataTable().api();
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error!!' + xhr + ", " + textStatus + ", " + errorThrown);
        }
    });
}
function obtenerListaSucursales() {
    $.ajax({
        type: "GET",
        url: "/Home/GetListaSucursal",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data) {
            if (!$.isEmptyObject(data.data)) {
                $.each(data.data, function (key, entidad) {
                    var miSelect = document.getElementById("dllSucursal");
                    //miSelect.options.length = 0;

                    var miOption = document.createElement("option");
                    miOption.setAttribute("value", entidad.id);
                    miOption.setAttribute("label", entidad.descripcion);

                    miSelect.appendChild(miOption);
                });
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error!!' + xhr + ", " + textStatus + ", " + errorThrown);
        }
    });
}
//Agrega sucursal
function abrirModalNuevaSucursal() {
    $('#dvAgregarNuevaSucursal').modal('show');
}
function agregarSucursal() {
    var nombreSucursal = document.getElementById("txtSucursal").value;
    $.ajax({
        type: "POST",
        url: "/Home/insertaNuevaSucursal",
        data: "NombreSucursal=" + nombreSucursal,
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (info) {
            swal("¡Operación realizada con éxito!", "Se ha agregado una nueva sucursal", "success")
            $('#txtSucursal').val("");
            $('#dvAgregarNuevaSucursal').modal('hide');
            var miSelect = document.getElementById("dllSucursal");
            miSelect.options.length = 0;
            obtenerListaSucursales()
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error!!');
        }
    });
}
//Agrega Nuevo Producto
function abrirModalNuevoProducto() {
    $('#dvAgregarNuevoProducto').modal('show');
}
function agregarProducto() {
    var nombreProducto = document.getElementById("txtProducto").value;
    var codigoBarras = document.getElementById("txtCodigoBarras").value;
    var precioProducto = document.getElementById("txtPrecio").value;
    $.ajax({
        type: "POST",
        url: "/Home/insertaNuevoProducto",
        data: { CodigoBarras: codigoBarras, NombreProducto: nombreProducto, Precio: precioProducto},
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (info) {
            swal("¡Operación realizada con éxito!", "Se ha agregado un nuevo producto", "success")
            $('#txtProducto').val("");
            $('#txtCodigoBarras').val("");
            $('#txtPrecio').val("");
            $('#dvAgregarNuevoProducto').modal('hide');
            cargarProductosTablaPrincipal(1)
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error!!');
        }
    });
}
//Agrega existencias
function abrirModalAgregarExistencias() {
    obtenerListaSucursalesExistencias();
    $('#dvAgregarExistencias').modal('show');
}
function obtenerListaSucursalesExistencias() {
    $.ajax({
        type: "GET",
        url: "/Home/GetListaSucursal",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data) {
            if (!$.isEmptyObject(data.data)) {
                $.each(data.data, function (key, entidad) {
                    var miSelect = document.getElementById("ddlSucursalExistencias");

                    var miOption = document.createElement("option");
                    miOption.setAttribute("value", entidad.id);
                    miOption.setAttribute("label", entidad.descripcion);

                    miSelect.appendChild(miOption);
                });
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error!!' + xhr + ", " + textStatus + ", " + errorThrown);
        }
    });
}
function onKeyUpBusquedaProducto(parametro) {
    if (parametro != "") {
        $.ajax({
            type: "GET",
            url: "/Home/BuscarProductoByParametro",
            data: "parametro=" + parametro,
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (data) {
                $(".searchFilter").remove();
                if (data.data !== null) {
                    var ul = '<ul style="position:relative !important; width: 100% !important;" class="searchFilter">{0}</ul>';
                    var li = "";
                    $.each(data.data, function (key, entidad) {
                        var item = entidad.codigoBarras + ' - ' + entidad.descripcion;
                    li += '<li onclick="onclickOkayProducto(' + entidad.id + ',\'' + entidad.descripcion +'\')">' + item + '</li>';
                    });
                    var value = ul.replace('{0}', li);
                    $("#txtNombreProducto").after(value);
                }
                else {
                    alert('No hay registros para esta busqueda');
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                alert('Error!!' + xhr + ", " + textStatus + ", " + errorThrown);
            }
        });
    } else { $(".searchFilter").remove(); }
}
function onclickOkayProducto(idProducto, nombreProducto) {
    $("#txtIdProducto").val(idProducto);
    $("#txtNombreProducto").val(nombreProducto);
    $(".searchFilter").remove();
}
function agregarExistencia() {
    var idSucursal = document.getElementById("ddlSucursalExistencias").value;
    var idProducto = document.getElementById("txtIdProducto").value;
    var cantidad = document.getElementById("txtCantProd").value;
    $.ajax({
        type: "POST",
        url: "/Home/insertaNuevaExistencia",
        data: { IdSucursal: idSucursal, idProducto: idProducto, Cantidad: cantidad },
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (info) {
            swal("¡Operación realizada con éxito!", "se han agregado existencias a esta sucursal", "success")
            $('#txtIdProducto').val("");
            $('#txtNombreProducto').val("");
            $('#txtCantProd').val("");
            $('#dvAgregarExistencias').modal('hide');
            cargarProductosTablaPrincipal(idSucursal)
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error!!');
        }
    });
}
//Realizar compra
function cancelarCompra() {
    var idSucursal = document.getElementById("dllSucursal").value;
    cargarProductosTablaPrincipal(idSucursal)
    monto = 0;
}
function preparaCompra() {
    var idSucursal = document.getElementById("dllSucursal").value;

    $("#table_Products").dataTable().fnDestroy();
    $("#table_Products tbody").empty();
    $.ajax({
        type: "GET",
        url: "/Home/GetListaProductosBySucursalCompra",
        data: "IdSucursal=" + idSucursal,
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        success: function (data) {
            if (!$.isEmptyObject(data.data)) {
                var identChk = 0;
                $.each(data.data, function (key, entidad) {
                    var disableCheck = entidad.cantidad > 0 ? "" : "disabled";
                    console.log(identChk);
                    var tr = '<tr>'
                        + '<td><input type="hidden"  name="ListaProducto[' + identChk + '].Id" id="txtId' + identChk + '" value="' + entidad.id + '" class="form-control" autocomplete="off">' + entidad.producto.codigoBarras + '</td>'
                        + '<td>' + entidad.producto.descripcion + '</td>'
                        + '<td><input type="text"  name="ListaProducto[' + identChk + '].Cantidad"  onchange="calcularMonto(this.value,' + identChk + ')" id="txtCantidad' + identChk + '" value="' + entidad.cantidad + '" class="form-control" autocomplete="off" title="Primero debe seleccionar el check del producto y después establecer su cantidad">'
                        + '<td><input type="text" name="precioProd" id="txtPrecio' + identChk + '" value="' + entidad.producto.precioUnitario + '" class="form-control" autocomplete="off" disabled>'
                        + '<td><input type="checkbox"  name="ListaProducto[' + identChk + '].Selected" id="cbox' + identChk + '" value="1" ' + disableCheck + '>'
                        + '</tr>';

                    $("#table_Products tbody").append(tr);
                    identChk += 1;
                });
                $("#table_Products").dataTable().api();
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            alert('Error!!' + xhr + ", " + textStatus + ", " + errorThrown);
        }
    });
    document.getElementById('btnCancelarCompra').style.display = 'block';
    document.getElementById('btnRealizarCompra').style.display = 'block';
}
function calcularMonto(cantidad, ident) {
    if (document.getElementById('cbox' + ident).checked) {
        var precioU = document.getElementById("txtPrecio" + ident).value;
        var montoInd = precioU * cantidad;
        monto += montoInd;
    }
    document.getElementById('lvlMonto').innerHTML = ' ' + monto + ' MN';
    $("#txtMonto").val(monto);
}
//Eliminar Productos
function abrirModalEliminarProducto() {
    $('#dvEliminaProd').modal('show');
}
function onKeyUpBusquedaProductoEliminar(parametro) {
    if (parametro != "") {
        $.ajax({
            type: "GET",
            url: "/Home/BuscarProductoByParametro",
            data: "parametro=" + parametro,
            contentType: "application/json; charset=utf-8",
            dataType: 'json',
            success: function (data) {
                $(".searchFilter").remove();
                if (data.data !== null) {
                    var ul = '<ul style="position:relative !important; width: 100% !important;" class="searchFilter">{0}</ul>';
                    var li = "";
                    $.each(data.data, function (key, entidad) {
                        var item = entidad.codigoBarras + ' - ' + entidad.descripcion;
                        li += '<li onclick="onclickProductoEliminar(' + entidad.id + ',\'' + entidad.descripcion + '\')">' + item + '</li>';
                    });
                    var value = ul.replace('{0}', li);
                    $("#txtNombreProductoEliminar").after(value);
                }
                else {
                    alert('No hay registros para esta busqueda');
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                alert('Error!!' + xhr + ", " + textStatus + ", " + errorThrown);
            }
        });
    } else { $(".searchFilter").remove(); }
}
function onclickProductoEliminar(idProducto, nombreProducto) {
    $("#txtIdProductoEliminar").val(idProducto);
    $("#txtNombreProductoEliminar").val(nombreProducto);
    $(".searchFilter").remove();
}
function eliminarProducto() {
    var idProducto = document.getElementById("txtIdProductoEliminar").value;
    var idSucursal = document.getElementById("dllSucursal").value;
    $.ajax({
        type: "POST",
        url: "/Home/EliminarProducto",
        data: { IdProducto: idProducto},
        content: "application/json; charset=utf-8",
        dataType: "json",
        success: function (info) {
            swal("¡Operación realizada con éxito!", "Se ha eliminado un producto", "success")
            $('#txtIdProductoEliminar').val("");
            $('#txtNombreProductoEliminar').val("");
            $('#dvEliminaProd').modal('hide');
            cargarProductosTablaPrincipal(idSucursal)
        },
        error: function (xhr, textStatus, errorThrown) {
            swal("¡Ops!", "Ha ocurrido un error interno", "danger")
        }
    });
}



