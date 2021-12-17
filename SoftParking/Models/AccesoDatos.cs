using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SoftParking.Models
{
    public class AccesoDatos
    {
        private static string cadenaConexion = @"Data Source=EZEQUIELMAR8E72;Initial Catalog=Parking;Integrated Security=True;User ID=sa; Password=sa";
        public List<Abonado> getValues()
        {
            try
            {
                List<Abonado> lstAbonados = new List<Abonado>();
                using (IDbConnection db = new SqlConnection(cadenaConexion))
                {
                    lstAbonados = db.Query<Abonado>("Select * From tblFriends").ToList();
                }
                return lstAbonados;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool registrarAbonado(Abonado a)
        {
            bool cargado = false;

            string sql = "INSERT INTO Abonados (nombre, apellido, dominio, id_tipoVehiculo, telefono, dni, id_tipoAbono) " +
                "VALUES (@nom, @ape, @dom, @idTipoVe, @tel, @dni, @idTipoAbo)";

            using (var con = new SqlConnection(cadenaConexion))
            {
                if (con.Execute(sql, new
                {
                    nom = a.Nombre,
                    ape = a.Apellido,
                    dom = a.Dominio,
                    idTipoVe = a.IdTipoVehiculo,
                    tel = a.Telefono,
                    dni = a.Dni,
                    idTipoAbo = a.IdTipoAbono
                }) > 0)
                {
                    cargado = true;
                }

            }
            return cargado;
        }

        public List<TipoVehiculos> getTipoVehiculos()
        {
            try
            {
                List<TipoVehiculos> lstTipoVehiculos = new List<TipoVehiculos>();
                using (IDbConnection db = new SqlConnection(cadenaConexion))
                {
                    lstTipoVehiculos = db.Query<TipoVehiculos>("Select id_tipoVehiculo as Id, tipo as Tipo From Tipos_de_Vehiculos").ToList();
                }
                return lstTipoVehiculos;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<TipoAbonos> getTiposAbonos()
        {
            try
            {
                List<TipoAbonos> lstTipoVehiculos = new List<TipoAbonos>();
                using (IDbConnection db = new SqlConnection(cadenaConexion))
                {
                    var query = "select id_tipoAbono as Id, CONCAT(tipoAbono, ' - $', monto) as Tipo, tipoAbono as TipoAbono, monto as Monto from Tipos_abonos";
                    lstTipoVehiculos = db.Query<TipoAbonos>(query).ToList();
                }
                return lstTipoVehiculos;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool registrarIngreso(Cliente c)
        {
            bool cargado = false;

            string sql = "INSERT INTO Clientes (dominio, codigo, fecha, id_tipoVehiculo, estado) " +
                "VALUES (@dom, @cod, @fecha, @idTipoVe, 1)";

            using (var con = new SqlConnection(cadenaConexion))
            {
                if (con.Execute(sql, new
                {
                    dom = c.Dominio,
                    cod = c.Codigo,
                    fecha = c.FechaHora,
                    idTipoVe = c.IdTipoVehiculo,

                }) > 0)
                {
                    cargado = true;
                }

            }
            return cargado;
        }

        public Cliente getFechaIngreso(string dominio)
        {
            try
            {
                var cliente = new Cliente();
                string sql = "SELECT fecha as FechaHora, id_cliente as Id, tv.id_tipoVehiculo as IdTipoVehiculo" +
                        " FROM Clientes c JOIN Tipos_de_Vehiculos tv" +
                        " on c.id_tipoVehiculo=tv.id_tipoVehiculo WHERE dominio=@dom";

                using (var conect = new SqlConnection(cadenaConexion))
                {
                    cliente = conect.Query<Cliente>(sql, new { dom = dominio }).FirstOrDefault();
                }

                return cliente;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int registrarDetalleVenta(Venta v)
        {
            var nuevoId = 0;

            string sql = "INSERT INTO Detalles_Venta (importe) VALUES (@monto) ; SELECT SCOPE_IDENTITY()";

            using (var con = new SqlConnection(cadenaConexion))
            {
                nuevoId = con.Query<int>(sql, new
                {
                    monto = v.Monto

                }).Single();

            }
            return nuevoId;
        }

        public bool registrarVenta(Venta v)
        {
            bool cargado = false;

            string sql = "INSERT INTO Ventas (horaEgreso, id_detalle_venta, id_cliente) VALUES (@fecha, @idDet,@idCl); SELECT SCOPE_IDENTITY()";

            using (var con = new SqlConnection(cadenaConexion))
            {
                if (con.Query<int>(sql, new
                {
                    fecha = v.HoraEgresoDate,
                    idDet = v.IdDetalleVenta,
                    idCl = v.IdCliente

                }).Single() > 0)
                {
                    cargado = true;
                }

            }

            if (cargado)
            {
                cargado = actualizarEstadoClientes(v.Dominio, v.IdCliente);
            }
            return cargado;
        }

        public bool actualizarEstadoClientes(string dominio, long idCliente)
        {
            bool cargado = false;

            string sql = "UPDATE Clientes SET estado=0 WHERE estado=1 AND dominio=@dom AND id_cliente=@idC";

            using (var con = new SqlConnection(cadenaConexion))
            {
                if (con.Execute(sql, new
                {
                    dom = dominio,
                    idC = idCliente

                }) > 0)
                {
                    cargado = true;
                }

            }

            return cargado;
        }

        public List<TipoEstadias> getTipoEstadias()
        {
            try
            {
                List<TipoEstadias> lstTiposEstadias = new List<TipoEstadias>();
                var query = "SELECT id_tipo_estadia as IdTipoEstadia, CONCAT(tipo, ' - $', monto) as TipoMonto, tipo as Tipo, monto as Monto, horas as Horas FROM Tipos_Estadia";

                using (IDbConnection db = new SqlConnection(cadenaConexion))
                {
                    lstTiposEstadias = db.Query<TipoEstadias>(query).ToList();
                }
                return lstTiposEstadias;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool registrarEstadia(VentaEstadias v)
        {
            var cargado = false;

            string sql = "INSERT INTO Ventas_Estadias (dominio, id_tipo_estadia, fecha, fecha_egreso) VALUES (@dom, @idTipoEst, @fechaIng, @fecEgr)";

            using (var con = new SqlConnection(cadenaConexion))
            {
                if (con.Execute(sql, new
                {
                    dom = v.Dominio,
                    idTipoEst = v.Id_tipo_estadia,
                    fechaIng = v.FechaIngreso,
                    fecEgr = v.FechaEgreso

                }) > 0)
                {
                    cargado = true;
                }

            }

            return cargado;
        }

        public Cliente getDatosCliente(long idCliente)
        {
            try
            {
                var cliente = new Cliente();
                string sql = "SELECT dominio as Dominio, fecha as FechaHora, c.id_tipoVehiculo as IdTipoVehiculo, tv.tipo as TipoVehiculo " +
                    "FROM Clientes c join Tipos_de_Vehiculos tv on c.id_tipoVehiculo=tv.id_tipoVehiculo WHERE id_cliente=@idC";

                using (var conect = new SqlConnection(cadenaConexion))
                {
                    cliente = conect.Query<Cliente>(sql, new { idC = idCliente }).FirstOrDefault();
                }

                return cliente;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool actualizarTipoEstadias(TipoEstadias te)
        {
            bool cargado = false;

            string sql = "UPDATE Tipos_Estadia SET monto=@mon WHERE id_tipo_estadia=@id";

            using (var con = new SqlConnection(cadenaConexion))
            {
                if (con.Execute(sql, new
                {
                    mon = te.NuevoMonto,
                    id = te.IdTipoEstadia

                }) > 0)
                {
                    cargado = true;
                }

            }

            return cargado;
        }

        public bool registrarTicket(Ticket t)
        {
            bool cargado = false;

            string sql = "INSERT INTO Tickets (fecha, id_cliente, id_detalle_venta) VALUES (@fec, @idC, @idDet)";

            using (var con = new SqlConnection(cadenaConexion))
            {
                if (con.Execute(sql, new
                {
                    fec = t.Fecha,
                    idC = t.IdCliente,
                    idDet = t.IdDetalleVenta
                }) > 0)
                {
                    cargado = true;
                }

            }

            return cargado;
        }

        /*--------------REPORTES-----------------*/

        public List<ReporteIngresosVehiculos> lstReporteIngresosVehiculos()
        {
            try
            {
                List<ReporteIngresosVehiculos> lstIngresos = new List<ReporteIngresosVehiculos>();
                //var query = @"
                //    select UPPER(dominio) as Dominio, c.fecha as FechaIngreso, ISNULL(getDate(), v.horaEgreso ) as FechaEgreso,
                //    tv.tipo as TipoVehiculo, c.estado as Estado
                //    from Ventas v left join Clientes c on v.id_cliente=c.id_cliente
                //    JOIN Tipos_de_Vehiculos tv on tv.id_tipoVehiculo=c.id_tipoVehiculo
                //    where DAY(FORMAT (horaEgreso, 'dd-MM-yyyy')) = DAY(getDate()) and c.estado = 0
                //    union
                //    select UPPER(dominio) as Dominio, c.fecha as FechaIngreso, getDate() as HoraEgreso,
                //    tv.tipo as TipoVehiculo, c.estado as Estado
                //    from Clientes c JOIN Tipos_de_Vehiculos tv on tv.id_tipoVehiculo=c.id_tipoVehiculo
                //    order by 5";

                var query = @"select UPPER(dominio) as Dominio, c.fecha as FechaIngreso, v.horaEgreso as FechaEgreso, 
                    tv.tipo as TipoVehiculo, c.estado as Estado
                    from Ventas v left
                    join Clientes c on v.id_cliente = c.id_cliente
                    JOIN Tipos_de_Vehiculos tv on tv.id_tipoVehiculo = c.id_tipoVehiculo and c.estado = 0

                    where FORMAT(getdate(), 'dd-MM-yy') = FORMAT(c.fecha, 'dd-MM-yy')
                    union
                    select UPPER(dominio) as Dominio, c.fecha as FechaIngreso, null, 
                    tv.tipo as TipoVehiculo, c.estado as Estado
                    from Clientes c, Tipos_de_Vehiculos tv where tv.id_tipoVehiculo = c.id_tipoVehiculo
                    and estado = 1 and

                    FORMAT(getdate(), 'dd-MM-yy') = FORMAT(c.fecha, 'dd-MM-yy')
                    order by 5";

                using (IDbConnection db = new SqlConnection(cadenaConexion))
                {
                    lstIngresos = db.Query<ReporteIngresosVehiculos>(query).ToList();
                }
                return lstIngresos;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ReporteEstadias> lstReporteEstadias(bool fechaAtual = false)
        {
            try
            {
                List<ReporteEstadias> lstEstadias = new List<ReporteEstadias>();
                var query = @"
                    select UPPER(ve.dominio) as Dominio, te.tipo as TipoEstadia, te.monto as Importe,
                    ve.fecha as Fecha, ve.fecha_egreso as FechaEgreso 
                    from Ventas_Estadias ve JOIN Tipos_Estadia te 
                    on ve.id_tipo_estadia = te.id_tipo_estadia";

                if (fechaAtual)
                {
                    query += " where FORMAT (getdate(), 'dd-MM-yy') = FORMAT(ve.fecha, 'dd-MM-yy')";
                }

                using (IDbConnection db = new SqlConnection(cadenaConexion))
                {
                    lstEstadias = db.Query<ReporteEstadias>(query).ToList();
                }
                return lstEstadias;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Usuario validarUsuario(Usuario u)
        {
            try
            {
                Usuario usrBd = null;
                string sql = "SELECT id_usuario as Id, usuario as Usuario, admin as EsAdmin, usuario as Username FROM Usuarios WHERE usuario=@usu AND password=@pass";

                using (var conect = new SqlConnection(cadenaConexion))
                {
                    usrBd = conect.Query<Usuario>(sql, new { usu = u.Username, pass = u.Password }).FirstOrDefault();
                }

                if (usrBd != null)
                {
                    usrBd.Logueado = true;
                }
                return usrBd;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public bool registrarGasto(Gasto g)
        {
            bool cargado = false;

            string sql = "INSERT INTO Gastos (detalle, monto, fecha) VALUES (@det, @mon, @fec)";

            using (var con = new SqlConnection(cadenaConexion))
            {
                if (con.Execute(sql, new
                {
                    det = g.Detalle,
                    mon = g.Monto,
                    fec = g.Fecha
                }) > 0)
                {
                    cargado = true;
                }

            }

            return cargado;
        }

        public List<Gasto> getGastos(string tipo = "d")
        {
            try
            {
                List<Gasto> lst = new List<Gasto>();
                var query = "";
                switch (tipo)
                {
                    case "d":
                        query = @"select id_gasto as IdGasto,FORMAT(fecha, 'dddd', 'es-ES')  as Periodo, 
                        monto as Monto, fecha as Fecha, detalle as Detalle from Gastos
                        group by id_gasto, DATEPART(DAY, fecha), monto, fecha, detalle 
                        order by FORMAT(fecha, 'dddd') asc";
                        break;
                    case "m":
                        query = @"select id_gasto as IdGasto, FORMAT(fecha, 'MMMM', 'es-ES') as Periodo, 
                        monto as Monto, fecha as Fecha, detalle as Detalle from Gastos
                        group by id_gasto, DATEPART(MONTH, fecha), monto, fecha, detalle
                        order by DATEPART(MONTH, fecha)";
                        break;
                    case "a":
                        query = @"select id_gasto as IdGasto,DATEPART(YEAR, fecha) as Periodo, 
                        monto as Monto, fecha as Fecha, detalle as Detalle from Gastos
                        group by id_gasto, DATEPART(YEAR, fecha), monto, fecha, detalle
                        order by 2";
                        break;
                    default:
                        break;
                }

                using (IDbConnection db = new SqlConnection(cadenaConexion))
                {
                    lst = db.Query<Gasto>(query).ToList();
                }
                return lst;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Tarifa> getTarifas()
        {
            try
            {
                List<Tarifa> lst = new List<Tarifa>();
                var query = @"
                    SELECT id_tarifa as IdTarifa, precio as Precio, tipo as TipoVehiculo, tv.id_tipoVehiculo as IdTipoVehiculo
                    FROM Tarifas t JOIN Tipos_de_Vehiculos tv on t.id_tipo_vehiculo=tv.id_tipoVehiculo";

                using (IDbConnection db = new SqlConnection(cadenaConexion))
                {
                    lst = db.Query<Tarifa>(query).ToList();
                }
                return lst;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool actualizarTarifa(Tarifa t)
        {
            bool cargado = false;

            string sql = "UPDATE Tarifas SET precio=@pre WHERE id_tarifa=@id";

            using (var con = new SqlConnection(cadenaConexion))
            {
                if (con.Execute(sql, new
                {
                    pre = t.NuevoPrecio,
                    id = t.IdTarifa

                }) > 0)
                {
                    cargado = true;
                }

            }

            return cargado;
        }

        public List<Abonado> getAbonados()
        {
            try
            {
                List<Abonado> lst = new List<Abonado>();
                var query = @"
                    select a.id_abonado as IdAbonado, nombre as Nombre, apellido as Apellido, dominio as Dominio, 
                    tv.tipo as TipoVehiculo, telefono as Telefono, dni as Dni, ta.tipoAbono, ta.monto as Importe
                    from Abonados a JOIN Tipos_de_Vehiculos tv ON a.id_tipoVehiculo = tv.id_tipoVehiculo
                    JOIN Tipos_abonos ta on ta.id_tipoAbono = a.id_tipoAbono";

                using (IDbConnection db = new SqlConnection(cadenaConexion))
                {
                    lst = db.Query<Abonado>(query).ToList();
                }
                return lst;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Abonado> getAbonadosDelDia()
        {
            try
            {
                List<Abonado> lst = new List<Abonado>();
                var query = @"
                    SELECT (nombre + ' ' + apellido) as Nombre, dominio as Dominio, ta.tipoAbono as TipoAbono, total as Importe, tv.tipo as TipoVehiculo 
                    from Detalles_Pago dp, Pagos_Abonos pa, Abonados a, Tipos_abonos ta, Tipos_de_Vehiculos tv
                    WHERE dp.id_detallePago = pa.id_detallePago and a.id_abonado = pa.id_abonado and ta.id_tipoAbono = a.id_tipoAbono and a.id_tipoVehiculo = tv.id_tipoVehiculo 
                    AND FORMAT(pa.fecha, 'dd-MM-yyyy') = FORMAT(getDate(), 'dd-MM-yyyy')";

                using (IDbConnection db = new SqlConnection(cadenaConexion))
                {
                    lst = db.Query<Abonado>(query).ToList();
                }
                return lst;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<Abonado> lstReporteAbonado()
        {
            try
            {
                List<Abonado> lst = new List<Abonado>();
                //var query = @"
                //    select a.id_abonado as IdAbonado, nombre as Nombre, apellido as Apellido, UPPER(dominio) as Dominio,
                //    ta.tipoAbono as TipoAbono, ta.id_tipoAbono as IdTipoAbono, tv.tipo as TipoVehiculo, tv.id_tipoVehiculo as IdTipoVehiculo,
                //    dni as Dni, telefono as Telefono
                //    from Abonados a join Tipos_de_Vehiculos tv on a.id_tipoVehiculo=tv.id_tipoVehiculo
                //    JOIN Tipos_abonos ta on ta.id_tipoAbono=a.id_tipoAbono";
                var query = @"select distinct(a.id_abonado),a.id_abonado as IdAbonado, nombre as Nombre, apellido as Apellido, UPPER(dominio) as Dominio,
                    ta.tipoAbono as TipoAbono, ta.id_tipoAbono as IdTipoAbono, tv.tipo as TipoVehiculo, tv.id_tipoVehiculo as IdTipoVehiculo,
                    dni as Dni, telefono as Telefono, id_periodo as IdPeriodo, fecha as Fecha, ta.monto as Importe
                    from Abonados a join Tipos_de_Vehiculos tv on a.id_tipoVehiculo=tv.id_tipoVehiculo
                    JOIN Tipos_abonos ta on ta.id_tipoAbono=a.id_tipoAbono
					full join Pagos_Abonos pa on a.id_abonado = pa.id_abonado order by id_periodo";

                using (IDbConnection db = new SqlConnection(cadenaConexion))
                {
                    lst = db.Query<Abonado>(query).ToList();
                }
                return lst;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Periodo> getPeriodos()
        {
            try
            {
                List<Periodo> lst = new List<Periodo>();
                var query = @"
                    SELECT id_periodo AS Id_periodo, periodo as Mes_periodo FROM Periodos";

                using (IDbConnection db = new SqlConnection(cadenaConexion))
                {
                    lst = db.Query<Periodo>(query).ToList();
                }
                return lst;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int registrarDetallePago(float monto)
        {
            var nuevoId = 0;

            string sql = "INSERT INTO Detalles_Pago (total) VALUES (@monto) ; SELECT SCOPE_IDENTITY()";

            using (var con = new SqlConnection(cadenaConexion))
            {
                nuevoId = con.Query<int>(sql, new
                {
                    monto

                }).Single();

            }
            return nuevoId;
        }

        public bool registrarPagoAbonado(Abonado a)
        {
            bool cargado = false;

            string sql = "INSERT INTO Pagos_Abonos (id_abonado, fecha, id_detallePago, id_periodo) VALUES (@idA, @fec, @idDet, @idPe)";

            using (var con = new SqlConnection(cadenaConexion))
            {
                if (con.Execute(sql, new
                {
                    idA = a.IdAbonado,
                    fec = a.Fecha,
                    idDet = a.IdDetallePago,
                    idPe = a.IdPeriodo

                }) > 0)
                {
                    cargado = true;
                }

            }

            return cargado;
        }

        public bool actualizarAbonado(Abonado a)
        {
            bool cargado = false;

            string sql = "UPDATE Abonados SET nombre=@nom, apellido=@ape, dominio=@dom, id_tipoVehiculo=@idTipoVe, telefono=@tel, dni=@dni, id_tipoAbono=@idTipoAbo " +
                "WHERE id_abonado=@idA";

            using (var con = new SqlConnection(cadenaConexion))
            {
                if (con.Execute(sql, new
                {
                    nom = a.Nombre,
                    ape = a.Apellido,
                    dom = a.Dominio,
                    idTipoVe = a.IdTipoVehiculo,
                    tel = a.Telefono,
                    dni = a.Dni,
                    idTipoAbo = a.IdTipoAbono,
                    idA = a.IdAbonado

                }) > 0)
                {
                    cargado = true;
                }

            }

            return cargado;
        }

        public bool guardarUsuario(Usuario u)
        {
            bool cargado = false;

            string sql = "INSERT INTO Usuarios (usuario, password, admin) VALUES (@usr, @pass, @adm)";

            using (var con = new SqlConnection(cadenaConexion))
            {
                if (con.Execute(sql, new
                {
                    usr = u.Username,
                    pass = u.Password,
                    adm = u.EsAdmin
                }) > 0)
                {
                    cargado = true;
                }

            }

            return cargado;
        }

        public List<Usuario> getUsuarios()
        {
            try
            {
                List<Usuario> lst = new List<Usuario>();
                var query = @"
                    SELECT id_usuario as Id, usuario as Username, password as Password, admin as EsAdmin FROM Usuarios";

                using (IDbConnection db = new SqlConnection(cadenaConexion))
                {
                    lst = db.Query<Usuario>(query).ToList();
                }
                return lst;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool actualizarUsuario(Usuario u)
        {
            bool cargado = false;

            string sql = "UPDATE Usuarios set usuario=@usr, password=@pass WHERE id_usuario=@id";

            using (var con = new SqlConnection(cadenaConexion))
            {
                if (con.Execute(sql, new
                {
                    usr = u.Username,
                    pass = u.Password,
                    id = u.Id
                }) > 0)
                {
                    cargado = true;
                }

            }

            return cargado;
        }

        public decimal getTotalCaja()
        {
            try
            {
                decimal monto = 0;
                string sql = "select ((select sum(total) from Detalles_Pago) +" +
                    "(select sum(importe) from Detalles_Venta) " +
                   ") as totalImporte";

                using (var conect = new SqlConnection(cadenaConexion))
                {
                    monto = conect.Query<decimal>(sql).FirstOrDefault();
                }

                return monto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool nuevoTipoAbono(TipoAbonos tipoAbono)
        {
            bool cargado = false;

            string sql = "INSERT INTO Tipos_abonos (tipoAbono, monto) values (@tipoA, @monto)";

            using (var con = new SqlConnection(cadenaConexion))
            {
                if (con.Execute(sql, new
                {
                    tipoA = tipoAbono.TipoAbono,
                    monto = tipoAbono.NuevoMonto
                }) > 0)
                {
                    cargado = true;
                }

            }

            return cargado;
        }

        public bool actualizarTipoAbono(TipoAbonos tipoAbono)
        {
            bool cargado = false;

            string sql = "UPDATE Tipos_abonos set tipoAbono=@tipoA, monto=@monto WHERE id_tipoAbono=@idTipoAbono";

            using (var con = new SqlConnection(cadenaConexion))
            {
                if (con.Execute(sql, new
                {
                    tipoA = tipoAbono.TipoAbono,
                    monto = tipoAbono.NuevoMonto,
                    idTipoAbono = tipoAbono.Id
                }) > 0)
                {
                    cargado = true;
                }

            }

            return cargado;
        }

        public decimal? getCierreCaja()
        {
            try
            {
                decimal? result = null;
                var query = @"select ISNULL(SUM(total), 0) as Total 
                    from 
                  
                    (SELECT SUM(dv.importe) as total from Detalles_Venta dv, Ventas v WHERE dv.id_detalle_venta = v.id_detalle_venta 
                    AND FORMAT(v.horaEgreso, 'dd-MM-yyyy') = FORMAT(getDate(), 'dd-MM-yyyy')) a";

                using (var conect = new SqlConnection(cadenaConexion))
                {
                    result = conect.Query<decimal>(query).FirstOrDefault();
                }


                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public decimal? getCierreCajaSubtotalEstadias()
        {
            try
            {
                decimal? result = null;
                var query = @"select (ISNULL(SUM(te.monto), 0))
                    from Ventas_Estadias ve JOIN Tipos_Estadia te 
                    on ve.id_tipo_estadia = te.id_tipo_estadia
					where FORMAT(ve.fecha, 'dd-MM-yyyy') = FORMAT(getDate(), 'dd-MM-yyyy')";

                using (var conect = new SqlConnection(cadenaConexion))
                {
                    result = conect.Query<decimal>(query).FirstOrDefault();
                }


                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public decimal? getCierreCajaSubtotalAbonos()
        {
            try
            {
                decimal? result = null;
                var query = @"SELECT ISNULL(SUM(dp.total),0) from Detalles_Pago dp, Pagos_Abonos pa WHERE dp.id_detallePago = pa.id_detallePago 
					and FORMAT(pa.fecha, 'dd-MM-yyyy') = FORMAT(getDate(), 'dd-MM-yyyy') ";

                using (var conect = new SqlConnection(cadenaConexion))
                {
                    result = conect.Query<decimal>(query).FirstOrDefault();
                }


                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public List<Caja> getReporteCaja()
        {
            try
            {
                var result = new List<Caja>();
                var query = "select sum(total) Total, fecha as Fecha from " +
                            "( " +
                            "( " +
                            "select sum(total) total, DATEPART(YEAR, fecha) fecha from Detalles_Pago dp, Pagos_Abonos pa " +
                            "where dp.id_detallePago = pa.id_detallePago " +
                            "group by DATEPART(YEAR, fecha) " +
                            "union all " +
                            "select sum(importe), DATEPART(YEAR, horaEgreso) AS ANIO from Detalles_Venta dv, Ventas v " +
                            "where dv.id_detalle_venta = v.id_detalle_venta " +
                            "group by DATEPART(YEAR, horaEgreso) " +
                            ") " +
                            ") as total " +
                            "group by fecha";

                using (var conect = new SqlConnection(cadenaConexion))
                {
                    result = conect.Query<Caja>(query).ToList().Where(x => x.Total > 0).ToList();
                }


                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public List<ReporteEstadias> lstReporteEstadiasFiltrado(long id)
        {
            try
            {
                string groupBy = "";
                switch (id)
                {
                    case 2:
                        groupBy = "CAST(MONTH(ve.fecha) AS VARCHAR(2))";
                        break;
                    case 3:
                        groupBy = "CAST(YEAR(ve.fecha) AS VARCHAR(4))";
                        break;
                    default:
                        break;
                }
                List<ReporteEstadias> lst = new List<ReporteEstadias>();
                var query = @"select " + groupBy + @", UPPER(ve.dominio) as Dominio, te.tipo as TipoEstadia, te.monto as Importe,
                    ve.fecha as Fecha, ve.fecha_egreso as FechaEgreso 
                    from Ventas_Estadias ve JOIN Tipos_Estadia te 
                    on ve.id_tipo_estadia = te.id_tipo_estadia
					GROUP BY " +
                    groupBy + @", UPPER(ve.dominio), te.tipo, te.monto,
                    ve.fecha, ve.fecha_egreso";

                using (IDbConnection db = new SqlConnection(cadenaConexion))
                {
                    lst = db.Query<ReporteEstadias>(query).ToList();
                }
                return lst;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Abonado> lstReporteAbonosFiltrado(long id)
        {
            try
            {
                string groupBy = "";
                switch (id)
                {
                    case 2:
                        groupBy = "CAST(MONTH(fecha) AS VARCHAR(2))";
                        break;
                    case 3:
                        groupBy = "CAST(YEAR(fecha) AS VARCHAR(4))";
                        break;
                    default:
                        break;
                }
                List<Abonado> lst = new List<Abonado>();

                var query = @"
                    select distinct(a.id_abonado)," + groupBy + @" AS Periodo, a.id_abonado as IdAbonado, nombre as Nombre, apellido as Apellido, UPPER(dominio) as Dominio,
                    ta.tipoAbono as TipoAbono, ta.id_tipoAbono as IdTipoAbono, tv.tipo as TipoVehiculo, tv.id_tipoVehiculo as IdTipoVehiculo,
                    dni as Dni, telefono as Telefono, id_periodo as IdPeriodo, fecha as Fecha, ta.monto as Importe
                    from Abonados a join Tipos_de_Vehiculos tv on a.id_tipoVehiculo=tv.id_tipoVehiculo
                    JOIN Tipos_abonos ta on ta.id_tipoAbono=a.id_tipoAbono
					full join Pagos_Abonos pa on a.id_abonado = pa.id_abonado
					GROUP BY " + groupBy + @", a.id_abonado, nombre, apellido, UPPER(dominio),
                    ta.tipoAbono, ta.id_tipoAbono, tv.tipo, tv.id_tipoVehiculo,
                    dni, telefono, id_periodo, fecha, ta.monto
					order by id_periodo
                ";

                using (IDbConnection db = new SqlConnection(cadenaConexion))
                {
                    lst = db.Query<Abonado>(query).ToList();
                }
                return lst;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Caja> getFiltroCaja(int mes, int year)
        {
            try
            {
                List<Caja> lst = new List<Caja>();
                var query = @"
                            select sum(total) Total, fecha as Fecha, anio as Anio from 
                            ( 
                            ( 
                            select sum(total) total, DATEPART(MONTH, fecha) fecha, DATEPART(YEAR, fecha) anio from Detalles_Pago dp, Pagos_Abonos pa 
                            where dp.id_detallePago = pa.id_detallePago 
							AND DATEPART(MONTH, fecha) = @month AND DATEPART(YEAR, fecha) = @year
                            group by DATEPART(MONTH, fecha), DATEPART(YEAR, fecha)
                            union all 
                            select sum(importe), DATEPART(MONTH, horaEgreso) AS mes, DATEPART(YEAR, horaEgreso) from Detalles_Venta dv, Ventas v 
                            where dv.id_detalle_venta = v.id_detalle_venta 
							AND DATEPART(MONTH, horaEgreso) = @month AND DATEPART(YEAR, horaEgreso) = @year
                            group by DATEPART(MONTH, horaEgreso),  DATEPART(YEAR, horaEgreso)  
                            ) 
                            ) as total 
                            group by fecha, anio";


                using (IDbConnection db = new SqlConnection(cadenaConexion))
                {
                    lst = db.Query<Caja>(query, new { month = mes, year }).ToList();
                }
                return lst;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}