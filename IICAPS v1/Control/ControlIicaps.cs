
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IICAPS_v1.DataObject;
using System.IO;
//using MySql.Data.MySqlClient;
using System.Data.SqlClient;


namespace IICAPS_v1.Control
{
    class ControlIicaps
    {

        SqlConnection conn;
        SqlConnectionStringBuilder builder;
        SqlCommand cmd;

        //Devs
        string server = @"DESKTOP-0SEOAIM\SQLEXPRESS";
        string userID = "iic2ps1d_db";
        string password = "ConejoVolador11";
        string database = "iicaps_db_devs";
        uint port = 1433;

        ////Production
        //string server = @"WIN-B2Q6B50DPEM";
        //string userID = "iic2ps1d_devs_db";
        //string password = "ConejoVolador11";
        //string database = "iicaps_db_devs";
        //uint port = 1433;

        public static ControlIicaps instance;
        public ParametrosGenerales parametros_Generales;
        /** MYSSQL SERVER
         * 
         * 
         *  //-------HOSTING ALDEAHOST-----------
        //string userID = "iic2ps1d";
        //string pass = "ConejoVolador11";

        string server = "iicaps.edu.mx";
        //string server = "187.137.151.226";
        string userID = "iic2ps1d_devs";
        string password = "ConejoVolador11";
        string database = "iic2ps1d_iicaps_devs";
        uint port = 3306;
        //uint port = 2083;
        ////MySqlConnectionProtocol protocolo =  MySqlConnectionProtocol.Tcp;
        //string database = "iic2ps1d_iicaps_prod";
        public ControlIicaps()
        {
            builder = new MySqlConnectionStringBuilder();
            builder.Server = server;
            builder.UserID = userID;
            builder.Password = password;
            builder.Database = database;
            builder.AllowUserVariables = true;
            builder.SslMode = MySqlSslMode.None;
            //builder.ConnectionProtocol = protocolo;
            builder.Port = port;

            conn = new MySqlConnection(builder.ToString());
            cmd = conn.CreateCommand();

            try
            {
                consultarParametrosGenerales();
            }
            catch (Exception ex) {
                //throw new Exception("Error al obtener parametros generales");
            }
        }**/
        // MSSQL SERVER
        public ControlIicaps()
        {
            builder = new SqlConnectionStringBuilder();
            builder.DataSource = server+","+port;
            builder.UserID = userID;
            builder.Password = password;
            builder.InitialCatalog = database;
            builder.IntegratedSecurity = true;

            conn = new SqlConnection(builder.ConnectionString);
            cmd = conn.CreateCommand();

            try
            {
                consultarParametrosGenerales();
            }
            catch (Exception ex) {
                //throw new Exception("Error al obtener parametros generales");
            }
        }

        public static ControlIicaps getInstance()
        {
            if (instance == null)
            {
                instance = new ControlIicaps();
                return instance;
            }
            else
            {
                return instance;
            }
        }

        private void openConection()
        {
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                throw new Exception("ERROR...! \n\n No es posible establecer conexión con el servidor");
            }
        }

        //-------------------------------Alumnos-------------------------------//
        public bool agregarAlumno(Alumno alumno)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "INSERT INTO alumnos (Nombre, Direccion, Telefono1, Telefono2, Correo, Facebook, CURP, RFC, Sexo, EstadoCivil, EscuelaProcedencia, Carrera, Programa, Nivel, Fecha, Estado, Tipo, Observaciones, Matricula) VALUES('"
                    + alumno.nombre + "','" + alumno.direccion + "','" + alumno.telefono1 + "','" + alumno.telefono2 + "','" + alumno.correo + "','" + alumno.facebook + "','" + alumno.curp + "','"
                    + alumno.rfc + "','" + alumno.sexo + "','" + alumno.estadoCivil + "','" + alumno.escuelaProcedencia + "','" + alumno.carrera + "','" + alumno.programa + "','" + alumno.nivel + "','" + formatearFecha(alumno.fecha) +
                    "','Registrado','" + alumno.tipo + "','" + alumno.observaciones + "','" + alumno.matricula + "')";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n No ha sido posible guardar los datos. Si el error persiste consulte con soporte técnico");
            }
        }
        public SqlDataAdapter obtenerAlumnosTable()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT A.RFC, A.Nombre , A.Telefono1 AS 'Telefono 1', A.Programa AS 'Programa', G.Generacion FROM alumnos A LEFT JOIN grupoAlumno GA ON A.RFC = GA.Alumno LEFT JOIN grupos G ON G.Codigo = GA.Grupo WHERE A.Estado NOT LIKE 'Baja'", conn);
                conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n No ha sido posible guardar los datos. Si el error persiste consulte con soporte técnico");
            }
        }
        public SqlDataAdapter obtenerAlumnosTable(string parameter)
        {

            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT A.RFC, A.Nombre, A.Telefono1 AS 'Telefono 1', A.Programa, G.Generacion FROM alumnos A LEFT JOIN grupoAlumno GA ON A.RFC = GA.Alumno LEFT JOIN grupos G ON G.Codigo = GA.Grupo " +
                    "WHERE " +
                    "(A.Nombre LIKE '%" + parameter + "%' or " +
                    " A.Telefono1 LIKE '%" + parameter + "%' or " +
                    " A.Programa LIKE '%" + parameter + "%' or " +
                    " G.Generacion LIKE '%" + parameter + "%') AND A.Estado NOT LIKE 'Baja'";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.conn);
                conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n No ha sido posible guardar los datos. Si el error persiste consulte con soporte técnico");
            }
        }
        public Alumno consultarAlumno(string rfc)
        {

            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {

                cmd.CommandText = "SELECT * FROM alumnos WHERE RFC='" + rfc + "'";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Alumno a = new Alumno();
                    a.nombre = reader.GetString(0);
                    a.direccion = reader.GetString(1);
                    a.telefono1 = reader.GetString(2);
                    a.telefono2 = reader.GetString(3);
                    a.correo = reader.GetString(4);
                    a.facebook = reader.GetString(5);
                    a.curp = reader.GetString(6);
                    a.rfc = reader.GetString(7);
                    a.sexo = reader.GetString(8);
                    a.estadoCivil = reader.GetString(9);
                    a.escuelaProcedencia = reader.GetString(10);
                    a.carrera = reader.GetString(11);
                    a.programa = reader.GetString(12);
                    a.nivel = reader.GetString(13);
                    a.fecha = reader.GetDateTime(14);
                    a.estado = reader.GetString(15);
                    a.tipo = reader.GetString(16);
                    a.observaciones = reader.GetString(17);
                    a.matricula = reader.GetString(18);
                    conn.Close();
                    return a;
                }
                conn.Close();
                return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n No ha sido posible guardar los datos. Si el error persiste consulte con soporte técnico");
            }
        }
        public List<Alumno> obtenerAlumnos()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {

                cmd.CommandText = "SELECT * FROM alumnos";
                SqlDataReader reader = cmd.ExecuteReader();
                List<Alumno> aux = new List<Alumno>();
                while (reader.Read())
                {
                    Alumno a = new Alumno();
                    a.nombre = reader.GetString(0);
                    a.direccion = reader.GetString(1);
                    a.telefono1 = reader.GetString(2);
                    a.telefono2 = reader.GetString(3);
                    a.correo = reader.GetString(4);
                    a.facebook = reader.GetString(5);
                    a.curp = reader.GetString(6);
                    a.rfc = reader.GetString(7);
                    a.sexo = reader.GetString(8);
                    a.estadoCivil = reader.GetString(9);
                    a.escuelaProcedencia = reader.GetString(10);
                    a.carrera = reader.GetString(11);
                    a.programa = reader.GetString(12);
                    a.nivel = reader.GetString(13);
                    a.fecha = reader.GetDateTime(14);
                    a.estado = reader.GetString(15);
                    a.tipo = reader.GetString(16);
                    a.observaciones = reader.GetString(17);
                    a.matricula = reader.GetString(18);
                    aux.Add(a);
                }
                conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n No ha sido posible guardar los datos. Si el error persiste consulte con soporte técnico");
            }
        }
        public List<Alumno> obtenerAlumnosByPrograma(string programa)
        {

            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {

                cmd.CommandText = "SELECT * FROM alumnos WHERE Programa = '" + programa + "'";
                SqlDataReader reader = cmd.ExecuteReader();
                List<Alumno> aux = new List<Alumno>();
                while (reader.Read())
                {
                    Alumno a = new Alumno();
                    a.nombre = reader.GetString(0);
                    a.direccion = reader.GetString(1);
                    a.telefono1 = reader.GetString(2);
                    a.telefono2 = reader.GetString(3);
                    a.correo = reader.GetString(4);
                    a.facebook = reader.GetString(5);
                    a.curp = reader.GetString(6);
                    a.rfc = reader.GetString(7);
                    a.sexo = reader.GetString(8);
                    a.estadoCivil = reader.GetString(9);
                    a.escuelaProcedencia = reader.GetString(10);
                    a.carrera = reader.GetString(11);
                    a.programa = reader.GetString(12);
                    a.nivel = reader.GetString(13);
                    a.fecha = reader.GetDateTime(14);
                    a.estado = reader.GetString(15);
                    a.tipo = reader.GetString(16);
                    a.observaciones = reader.GetString(17);
                    a.matricula = reader.GetString(18);
                    aux.Add(a);
                }
                conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de los alumnos de la base de datos");
            }

        }
        public bool darDeBajaAlumno(string rfc)
        {

            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {

                cmd.CommandText = "UPDATE alumnos SET Estado='Baja' WHERE RFC = '" + rfc + "'";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error..! Error al dar de baja el alumno de la Base de Datos");
            }

        }
        public string obtenerNombreAlumno(string rfc)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {

                cmd.CommandText = "SELECT Nombre FROM alumnos WHERE RFC = '" + rfc + "'";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string nombre = "";
                    nombre = reader.GetString(0);
                    return nombre;
                }
                conn.Close();
                return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error..! Error al obtener el nombre del alumno de la Base de Datos");
            }
        }
        public bool darDeAltaAlumno(string rfc)
        {

            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {

                cmd.CommandText = "UPDATE alumnos SET Estado='Registrado' WHERE RFC = '" + rfc + "'";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error..! Error al dar de alta el alumno de la Base de Datos");
            }
        }
        public bool actualizarAlumno(Alumno alumno)
        {

            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {

                cmd.CommandText = "UPDATE alumnos SET Nombre= '" + alumno.nombre + "', Direccion= '" + alumno.direccion + "', Telefono1= '" + alumno.telefono1 + "', Telefono2= '"
                    + alumno.telefono2 + "', Correo= '" + alumno.correo + "', Facebook= '" + alumno.facebook + "', Sexo= '" + alumno.sexo + "', EstadoCivil= '" + alumno.estadoCivil +
                    "', Programa= '" + alumno.programa + "', Fecha= '" + formatearFecha(alumno.fecha) + "', Estado= '" + alumno.estado + "', Tipo= '" + alumno.tipo + "',Observaciones= '" + alumno.observaciones + "',Matricula= '" + alumno.matricula + "' WHERE RFC = '" + alumno.rfc + "'";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al actualizar los datos del alumno en la Base de Datos");
            }

        }

        //-------------------------------Credito de alumnos-------------------------------//
        public bool agregarCreditoAlumno(CreditoAlumno credito)
        {

            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string creditoQuery = "INSERT INTO creditoAlumno (AlumnoID, CantidadMensualidad, CantidadMeses, CantidadAbonoCredito, CantidadAbonoMensual, FechaSolicitud, Observaciones, Estado) VALUES ('"
                    + credito.alumno + "', '" + credito.cantidadMensualidad + "', '" + credito.cantidadMeses + "', '"
                    + credito.cantidadAbonoCredito + "', '" + credito.cantidadAbonoMensual + "','"
                    + formatearFecha(DateTime.Now) + "', '" + credito.observaciones + "', '" + credito.estado + "');";
                string registroCobro = "INSERT INTO cobrosAlumno(Alumno, Concepto, Cantidad, Pago, Restante, Fecha, Parent_ID)SELECT '"
                    + credito.alumno + "','Credito', '" + (credito.cantidadMensualidad * credito.cantidadMeses) +
                    "','0.00','" + (credito.cantidadMensualidad * credito.cantidadMeses) + "','" + formatearFecha(DateTime.Now) +
                    "', AUTO_INCREMENT-1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '" + this.database +
                    "' AND TABLE_NAME = 'creditoAlumno';";
                string updateColegiatura = "UPDATE cobrosAlumno SET Restante=0,Cantidad=Pago WHERE Parent_ID = '" + credito.alumno + "' AND Restante > 0;";


                cmd.CommandText = "BEGIN TRANSACTION;" +
                    creditoQuery +
                    registroCobro +
                    updateColegiatura +
                    "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception E)
            {
                conn.Close();
                throw new Exception("Error al agregar el credito del alumnos a la base d datos");

            }
        }
        public SqlDataAdapter obtenerCreditoAlumnosTable()
        {

            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT ID, AlumnoID, CantidadMensualidad AS 'Mensualidad' , CantidadMeses AS 'No. de Meses',  FechaSolicitud AS 'Fecha de Solicitud', Observaciones FROM creditoAlumno WHERE Estado NOT LIKE 'Cancelado'", conn);
                conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de los creditos de los alumnos de la base de datos");
            }

        }
        public SqlDataAdapter obtenerCreditoAlumnosTable(string parameter)
        {

            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT ID, AlumnoID, CantidadMensualidad AS 'Mensualidad' , CantidadMeses AS 'No. de Meses',  FechaSolicitud AS 'Fecha de Solicitud', Observaciones FROM creditoAlumno WHERE Estado NOT LIKE 'Cancelado' AND " +
                    "(AlumnoID LIKE '%" + parameter + "%' or " +
                    "CantidadMensualidad LIKE '%" + parameter + "%' or " +
                    "CantidadMeses LIKE '%" + parameter + "%' or " +
                    "Observaciones LIKE '%" + parameter + "%')";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, conn);
                conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de los creditos de los alumnos de la base de datos");
            }

        }
        public CreditoAlumno consultarCreditoActivoAlumno(string rfc)
        {

            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT C.ID, C.AlumnoID, C.CantidadMensualidad, C.CantidadMeses, C.CantidadAbonoCredito, C.CantidadAbonoMensual, C.FechaSolicitud, C.Observaciones, C.Estado, A.Pago FROM creditoAlumno C, cobrosAlumno A WHERE A.Parent_ID=C.ID AND AlumnoID='" + rfc + "' AND C.Estado='Activo'";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CreditoAlumno credito = new CreditoAlumno();
                    credito.id = reader.GetInt32(0);
                    credito.alumno = reader.GetString(1);
                    credito.cantidadMensualidad = reader.GetDecimal(2);
                    credito.cantidadMeses = reader.GetInt32(3);
                    credito.cantidadAbonoCredito = reader.GetDecimal(4);
                    credito.cantidadAbonoMensual = reader.GetDecimal(5);
                    credito.fechaSolicitud = reader.GetDateTime(6);
                    credito.observaciones = reader.GetString(7);
                    credito.estado = reader.GetString(8);
                    credito.pago = reader.GetDecimal(9);
                    conn.Close();
                    return credito;
                }
                conn.Close();
                return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos del credito del alumno de la base de datos");
            }

        }
        public CreditoAlumno consultarCreditoAlumno(string rfc)
        {

            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT C.ID, C.AlumnoID, C.CantidadMensualidad, C.CantidadMeses, C.CantidadAbonoCredito, C.CantidadAbonoMensual, C.FechaSolicitud, C.Observaciones, C.Estado, A.Pago FROM creditoAlumno C, cobrosAlumno A WHERE A.Parent_ID=C.ID AND AlumnoID='" + rfc + "'";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CreditoAlumno credito = new CreditoAlumno();
                    credito.id = reader.GetInt32(0);
                    credito.alumno = reader.GetString(1);
                    credito.cantidadMensualidad = reader.GetDecimal(2);
                    credito.cantidadMeses = reader.GetInt32(3);
                    credito.cantidadAbonoCredito = reader.GetDecimal(4);
                    credito.cantidadAbonoMensual = reader.GetDecimal(5);
                    credito.fechaSolicitud = reader.GetDateTime(6);
                    credito.observaciones = reader.GetString(7);
                    credito.estado = reader.GetString(8);
                    credito.pago = reader.GetDecimal(9);
                    conn.Close();
                    return credito;
                }
                conn.Close();
                return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos del credito del alumno de la base de datos");
            }

        }
        public bool actualizarEstadoCredito(string credito_ID, string estado)
        {

            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string creditoQuery = "UPDATE creditoAlumno SET Estado ='" + estado + "' WHERE ID = '" + credito_ID + "'";
                cmd.CommandText = "BEGIN TRANSACTION;" +
                    creditoQuery +
                    "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al actualizar los datos del credito del alumno en la Base de Datos");
            }
        }
        public bool actualizarCredito(CreditoAlumno credito)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string creditoQuery = "UPDATE creditoAlumno SET CantidadMensualidad= '" + credito.cantidadMensualidad + "', CantidadMeses= '" + credito.cantidadMeses +
                    "', CantidadAbonoCredito=" + credito.cantidadAbonoCredito + "', CantidadAbonoMensual='" + credito.cantidadAbonoMensual +
                    "', Observaciones= '" + credito.observaciones + "' WHERE AlumnoID = '" + credito.alumno + "'";
                string registroCobro = "UPDATE cobrosAlumno set Alumno='" + credito.alumno + "',Concepto='Credito', Cantidad='" + (credito.cantidadMensualidad * credito.cantidadMeses) +
                    "',Restante=" + (credito.cantidadMensualidad * credito.cantidadMeses) + "- pago,Fecha='" + formatearFecha(DateTime.Now) + "' WHERE Parent_ID = '" + credito.id + "';";

                cmd.CommandText = "BEGIN TRANSACTION;" +
                    creditoQuery +
                    registroCobro +
                    "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al actualizar los datos del credito del alumno en la Base de Datos");
            }

        }
        public bool cancelarCredito(string id)
        {


            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "UPDATE creditoAlumno SET Estado = 'Cancelado' WHERE ID=" + id;
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al cancelar el crédito del alumno en la Base de Datos");
            }
        }

        //-------------------------------PAGOS ALUMNO--------------------------------------//
        public bool agregarPagoAlumno(PagoAlumno pago, List<Cobro> cobros)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string pagoQuery = "INSERT INTO pagosAlumno (AlumnoID, FechaPago, Cantidad, Concepto, Observaciones, Recibio) VALUES ('"
                        + pago.alumnoID + "', '" + formatearFecha(pago.fechaPago) + "'," + pago.cantidad + ", '" + pago.concepto + "', '"
                        + pago.observaciones + "', '" + pago.recibio + "');";
                string actualizarCobros = "";
                foreach (Cobro aux in cobros)
                {
                    actualizarCobros += "UPDATE cobrosAlumno SET Pago = " + aux.pago + ", Restante=" + aux.restante + " WHERE ID = " + aux.id + "; ";
                }


                cmd.CommandText = "BEGIN TRANSACTION;" +
                    pagoQuery +
                    actualizarCobros +
                    "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception E)
            {
                conn.Close();
                throw new Exception("Error al agregar el pago del alumno a la base de datos");
            }
        }
        public bool agregarPagoAlumno(PagoAlumno pago)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "INSERT INTO pagosAlumno (AlumnoID, FechaPago, Cantidad, Concepto, Observaciones, Recibio) VALUES ('"
                        + pago.alumnoID + "', '" + formatearFecha(pago.fechaPago) + "'," + pago.cantidad + ", '" + pago.concepto + "', '"
                        + pago.observaciones + "', '" + pago.recibio + "')";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception E)
            {
                conn.Close();
                throw new Exception("Error al agregar el pago del alumno a la base de datos");
            }

        }
        public SqlDataAdapter obtenerPagosAlumnosTable()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT ID, AlumnoID AS 'ID de Alumno', FechaPago AS 'Fecha de Pago', Cantidad, Concepto, Observaciones, Recibio, Estado FROM pagosAlumno", conn);
                conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de los pagos de la base de datos");
            }

        }
        public SqlDataAdapter obtenerPagosAlumnosTable(string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT ID, AlumnoID AS 'ID de Alumno', FechaPago AS 'Fecha de Pago', Cantidad, Concepto, Observaciones, Recibio, Estado FROM pagosAlumno WHERE" +
                    "(AlumnoID LIKE '%" + parameter + "%' or " +
                    "Cantidad LIKE '%" + parameter + "%' or " +
                    "Concepto LIKE '%" + parameter + "%' or " +
                    "Recibio LIKE '%" + parameter + "%')";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, conn);
                conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de los pagos de la base de datos");
            }

        }
        public PagoAlumno consultarPagoAlumno(int id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT * FROM pagosAlumno WHERE ID='" + id + "'";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    PagoAlumno pago = new PagoAlumno();
                    pago.id = reader.GetInt32(0);
                    pago.alumnoID = reader.GetString(1);
                    pago.fechaPago = reader.GetDateTime(2);
                    pago.cantidad = reader.GetInt32(3);
                    pago.concepto = reader.GetString(4);
                    pago.observaciones = reader.GetString(5);
                    pago.recibio = reader.GetString(6);
                    conn.Close();
                    return pago;
                }
                conn.Close();
                return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos del pago de la base de datos");
            }
        }
        public SqlDataAdapter obtenerPagosDeAlumnoTable(String rfc)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT ID,Cantidad,Concepto,Observaciones,Recibio,FechaPago FROM pagosAlumno WHERE AlumnoID = '" + rfc + "'", conn);
                conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de los pagos del alumno de la base de datos");
            }


        }
        public List<String> obtenerConceptosDePagoAlumno(string area)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT Concepto FROM conceptos WHERE Tipo='Pago' AND Area='" + area + "';";
                SqlDataReader reader = cmd.ExecuteReader();
                List<String> aux = new List<String>();
                while (reader.Read())
                {
                    aux.Add(reader.GetString(0));
                }
                conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de los conceptos de pago de la base de datos");
            }

        }
        public bool cancelarPagoAlumno(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "UPDATE pagosAlumno SET Estado = 'Cancelado' WHERE ID=" + id;
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                throw new Exception("Error al cancelar el pago del alumno en la Base de Datos");

            }
        }
        public Cobro consultarCobrosDeAlumnoPorConcepto(String rfc, string concepto)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT ID,Concepto,Cantidad,Pago,Restante,Alumno,Parent_ID FROM cobrosAlumno WHERE Alumno = '" + rfc + "' AND Concepto='" + concepto + "';";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Cobro cobro = new Cobro();
                    cobro.id = reader.GetInt32(0);
                    cobro.concepto = reader.GetString(1);
                    cobro.cantidad = reader.GetDecimal(2);
                    cobro.pago = reader.GetDecimal(3);
                    cobro.restante = reader.GetDecimal(4);
                    cobro.alumno = reader.GetString(5);
                    cobro.parent_id = reader.GetString(6);
                    conn.Close();
                    return cobro;
                }
                return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de los pagos del alumno de la base de datos");
            }
 
        }
        public List<Cobro> consultarCobrosDeAlumno(String rfc)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT ID,Concepto,Cantidad,Pago,Restante,Alumno,Parent_ID FROM cobrosAlumno WHERE Alumno = '" + rfc + "' AND Restante > 0";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Cobro> aux = new List<Cobro>();
                    while (reader.Read())
                    {
                        Cobro cobro = new Cobro();
                        cobro.id = reader.GetInt32(0);
                        cobro.concepto = reader.GetString(1);
                        cobro.cantidad = reader.GetDecimal(2);
                        cobro.pago = reader.GetDecimal(3);
                        cobro.restante = reader.GetDecimal(4);
                        cobro.alumno = reader.GetString(5);
                        cobro.parent_id = reader.GetString(6);
                        aux.Add(cobro);
                    }
                    conn.Close();
                    if (aux.Count != 0)
                        return aux;
                    else
                        return null;
                }
                catch (Exception e)
                {
                    conn.Close();
                    throw new Exception("Error al obtener los datos de los pagos del alumno de la base de datos");
                }
            
        }
        public SqlDataAdapter obtenerCobrosDeAlumnoTable(String rfc)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                    SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT ID,Concepto,Cantidad,Pago,Restante,Fecha FROM cobrosAlumno WHERE Alumno = '" + rfc + "'", conn);
                    conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    conn.Close();
                    throw new Exception("Error al obtener los datos de los pagos del alumno de la base de datos");
                }
   
        }


        //-------------------------------MATERIAS-------------------------------//
        public bool agregarMateria(Materia materia)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string agregar = "INSERT INTO materia (Nombre, Duracion, Semestre, Costo) VALUES('"
                        + materia.nombre + "','" + materia.duracion + "','" + materia.semestre + "','" + materia.costo + "');";
                string programas = "";
                if (materia.programa != null)
                    programas = "INSERT INTO mapaCurricular (Materia, Programa) VALUES ((select ID from materia ORDER BY id DESC LIMIT 1), '" + materia.programa + "');";


                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + agregar
                                    + programas
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar la materia a la Base de datos");
            }
          
        }
        public int obtenerUltimoIDMateria()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT MAX(ID) FROM materia";
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int x = reader.GetInt32(0);
                        conn.Close();
                        return x;
                    }
                    return 0;
                }
                catch (Exception e)
                {
                    conn.Close();
                    throw new Exception("Error...!\n Error al consultar id de materia a la Base de datos");
                }
          
        }
        public bool actualizarMateria(Materia materia)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "UPDATE materia SET " +
                        "Nombre='" + materia.nombre + "', Duracion='" + materia.duracion + "', Semestre='" + materia.semestre + "', Costo='" + materia.costo +
                        "' WHERE ID=" + materia.id + ";";
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    conn.Close();
                    throw new Exception("Error...!\n Error al actualizar la materia a la Base de datos");
                }
            
        }
        public bool desactivarMateria(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "UPDATE materia SET Activo=0 WHERE ID=" + id + ";";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al eliminar la materia a la Base de datos");
            }
        }
        public SqlDataAdapter obtenerMateriasTable()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT M.ID, M.Nombre, M.Duracion,M.Semestre,M.Costo, P.Nombre AS 'Programa' FROM materia M LEFT JOIN mapaCurricular C ON C.Materia=M.ID LEFT JOIN programa P ON C.Programa=P.Codigo ORDER BY M.ID ASC", conn);
                conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de las materias de la base de datos");
            }
        }
        public SqlDataAdapter obtenerMateriasTable(string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT M.ID, M.Nombre, M.Duracion,M.Semestre,M.Costo, P.Nombre AS 'Programa' FROM materia M LEFT JOIN mapaCurricular C ON C.Materia=M.ID LEFT JOIN programa P ON C.Programa=P.Codigo " +
                    " WHERE " +
                    "(M.ID LIKE '%" + parameter + "%' or " +
                    " M.Nombre LIKE '%" + parameter + "%' or " +
                    " M.Semestre LIKE '%" + parameter + "%' or " +
                    " P.Nombre LIKE '%" + parameter + "%' or " +
                    " C.Programa LIKE '%" + parameter + "%') ORDER BY M.ID ASC";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.conn);
                this.conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de las materias de la base de datos");
            }
        }
        public Materia consultarMateria(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT * FROM materia WHERE ID='" + id + "'";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Materia a = new Materia();
                    a.id = reader.GetInt32(0);
                    a.nombre = reader.GetString(1);
                    a.duracion = reader.GetString(2);
                    a.semestre = reader.GetInt32(3).ToString();
                    a.costo = reader.GetDecimal(4);
                    conn.Close();
                    return a;
                }
                conn.Close();
                return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de la materia de la base de datos");
            }
        }
        public List<Materia> obtenerMaterias()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT * FROM materia";
                SqlDataReader reader = cmd.ExecuteReader();
                List<Materia> aux = new List<Materia>();
                while (reader.Read())
                {

                    Materia a = new Materia();
                    a.id = reader.GetInt32(0);
                    a.nombre = reader.GetString(1);
                    a.duracion = reader.GetString(2);
                    a.semestre = reader.GetInt32(3).ToString();
                    a.costo = reader.GetDecimal(4);
                    aux.Add(a);
                }
                conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de las materias de la base de datos");
            }
        }

        //-------------------------------PROGRAMA-------------------------------//
        public bool agregarPrograma(Programa programa)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string agregar = "INSERT INTO programa (Nivel, Nombre, Codigo, RVOE, CEIFRHS, Duracion, Horario, Modalidad, RequisitosEspecialidad, RequisitosTitulacion,RequisitosDiplomado, Objetivo, PerfilIngreso,PerfilEgreso,ProcesoSeleccion,CostoInscripcionSemestral,CostoMensual,CostoCursoPropedeutico) VALUES("
                        + " ' " + programa.Nivel + "','" + programa.Nombre + "','" + programa.Codigo + "','" + programa.RVOE + "','" + programa.CEIFRHS + "','" + programa.Duracion
                        + "','" + programa.Horario + "','" + programa.Modalidad + "','" + programa.RequisitosEspecialidad
                        + "','" + programa.RequisitosTitulacion + "','" + programa.RequisitosDiplomado + "','" + programa.Objetivo
                        + "','" + programa.PerfilIngreso + "','" + programa.PerfilEgreso
                        + "','" + programa.ProcesoSeleccion + "','" + programa.CostoInscripcionSemestral + "','" + programa.CostoMensualidad
                        + "','" + programa.CostoCursoPropedeutico + "');";
                string materias = "";
                string mapa = "";
                if (programa.MapaCurricular != null)
                    foreach (Materia m in programa.MapaCurricular)
                    {
                        materias += "INSERT INTO materia (Nombre,Duracion,Semestre,Costo,Activo) SELECT '" + m.nombre + "', '" + m.duracion + "', '" + m.semestre + "', '" + m.costo + "',1  WHERE NOT EXISTS (SELECT * FROM materia WHERE Nombre='" + m.nombre + "' AND Duracion='" + m.duracion + "' AND Semestre= '" + m.semestre + "' AND Costo= '" + m.costo + "' and Activo=1); ";
                        mapa += "INSERT INTO mapaCurricular (Materia, Programa) VALUES('" + m.id + "','" + programa.Codigo + "');";
                    }


                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + agregar
                                    + "DELETE FROM mapaCurricular WHERE Programa='" + programa.Codigo + "';"
                                    + materias
                                    + mapa
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar Prorgrama a la Base de datos");
            }
        }
        public bool actualizarPrograma(Programa programa)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string update = "UPDATE programa SET "
                         + " RVOE='" + programa.RVOE + "', CEIFRHS='" + programa.CEIFRHS
                         + "',Nivel='" + programa.Nivel + "',Nombre='" + programa.Nombre + "',Duracion='" + programa.Duracion
                         + "',Horario='" + programa.Horario + "',Modalidad='" + programa.Modalidad + "',RequisitosEspecialidad='" + programa.RequisitosEspecialidad
                         + "',RequisitosTitulacion='" + programa.RequisitosTitulacion + "',RequisitosDiplomado='" + programa.RequisitosDiplomado + "',Objetivo='" + programa.Objetivo
                         + "',PerfilIngreso='" + programa.PerfilIngreso + "',PerfilEgreso='" + programa.PerfilEgreso
                         + "',ProcesoSeleccion='" + programa.ProcesoSeleccion + "',CostoInscripcionSemestral='" + programa.CostoInscripcionSemestral + "',CostoMensual='" + programa.CostoMensualidad
                         + "',CostoCursoPropedeutico='" + programa.CostoCursoPropedeutico + "'" +
                         "WHERE Codigo='" + programa.Codigo + "';";
                string materias = "";
                string mapa = "";
                if (programa.MapaCurricular != null)
                    foreach (Materia m in programa.MapaCurricular)
                    {
                        materias += "INSERT INTO materia (Nombre,Duracion,Semestre,Costo,Activo) SELECT '" + m.nombre + "', '" + m.duracion + "', '" + m.semestre + "', '" + m.costo + "',1  WHERE NOT EXISTS (SELECT * FROM materia WHERE Nombre='" + m.nombre + "' AND Duracion='" + m.duracion + "' AND Semestre= '" + m.semestre + "' AND Costo= '" + m.costo + "' and Activo=1); ";
                        mapa += "INSERT INTO mapaCurricular (Materia, Programa) VALUES('" + m.id + "','" + programa.Codigo + "');";
                    }


                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + update
                                    + "DELETE FROM mapaCurricular WHERE Programa='" + programa.Codigo + "';"
                                    + materias
                                    + mapa
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al actualizar la materia a la Base de datos");
            }
        }
        public bool desactivarPrograma(string codigo)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "UPDATE programa SET Activo=0 WHERE Codigo='" + codigo + "';";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al eliminar programa a la Base de datos");
            }
        }
        public SqlDataAdapter obtenerProgramaTable()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT P.Codigo,P.Nivel, P.Nombre, P.Duracion, P.Horario, P.Modalidad FROM programa P WHERE P.Activo=1", conn); conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de programas de la base de datos");
            }
        }
        public SqlDataAdapter obtenerProgramaTable(string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT P.Codigo,P.Nivel, P.Nombre, P.Duracion, P.Horario, P.Modalidad FROM programa P " +
                    " WHERE " +
                    "(P.Codigo LIKE '%" + parameter + "%' or " +
                    " P.Nivel LIKE '%" + parameter + "%' or " +
                    " P.Nombre LIKE '%" + parameter + "%' or " +
                    " P.Duracion LIKE '%" + parameter + "%' or " +
                    " P.Horario LIKE '%" + parameter + "%' or " +
                    " P.Modalidad LIKE '%" + parameter + "%') AND P.Activo=1";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.conn);
                this.conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de las materias de la base de datos");
            }
        }
        public Programa consultarPrograma(string codigo)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT * FROM programa WHERE Codigo='" + codigo + "'";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Programa p = new Programa();
                    p.Codigo = reader.GetString(0);
                    p.RVOE = reader.GetString(1);
                    p.CEIFRHS = reader.GetString(2);
                    p.Nombre = reader.GetString(3);
                    p.Nivel = reader.GetString(4);
                    p.Duracion = reader.GetString(5);
                    p.Horario = reader.GetString(6);
                    p.Modalidad = reader.GetString(7);
                    p.RequisitosEspecialidad = reader.GetString(8);
                    p.RequisitosTitulacion = reader.GetString(9);
                    p.RequisitosDiplomado = reader.GetString(10);
                    p.Objetivo = reader.GetString(11);
                    p.PerfilIngreso = reader.GetString(12);
                    p.PerfilEgreso = reader.GetString(13);
                    p.ProcesoSeleccion = reader.GetString(14);
                    p.CostoInscripcionSemestral = reader.GetDecimal(15);
                    p.CostoMensualidad = reader.GetDecimal(16);
                    p.CostoCursoPropedeutico = reader.GetDecimal(17);
                    p.Activo = reader.GetBoolean(18);
                    conn.Close();
                    return p;
                }
                conn.Close();
                return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos del programa de la base de datos");
            }
        }
        public List<Materia> consultarMapaCurricularPrograma(string codigo)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT C.Materia ,M.Nombre,M.Duracion,M.Semestre,M.Costo FROM mapaCurricular C, materia M WHERE C.Materia= M.ID AND C.Programa='" + codigo + "'";

                SqlDataReader reader = cmd.ExecuteReader();
                List<Materia> aux = new List<Materia>();
                while (reader.Read())
                {
                    Materia m = new Materia();
                    m.id = reader.GetInt32(0);
                    m.nombre = reader.GetString(1);
                    m.duracion = reader.GetString(2);
                    m.semestre = reader.GetInt32(3).ToString();
                    m.costo = reader.GetDecimal(4);
                    aux.Add(m);
                }
                conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos del programa de la base de datos");
            }
        }
        public List<Programa> obtenerProgramas()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT * FROM programa";
                SqlDataReader reader = cmd.ExecuteReader();
                List<Programa> aux = new List<Programa>();
                while (reader.Read())
                {

                    Programa p = new Programa();
                    p.Codigo = reader.GetString(0);
                    p.RVOE = reader.GetString(1);
                    p.CEIFRHS = reader.GetString(2);
                    p.Nombre = reader.GetString(3);
                    p.Nivel = reader.GetString(4);
                    p.Duracion = reader.GetString(5);
                    p.Horario = reader.GetString(6);
                    p.Modalidad = reader.GetString(7);
                    p.RequisitosEspecialidad = reader.GetString(8);
                    p.RequisitosTitulacion = reader.GetString(9);
                    p.RequisitosDiplomado = reader.GetString(10);
                    p.Objetivo = reader.GetString(11);
                    p.PerfilIngreso = reader.GetString(12);
                    p.PerfilEgreso = reader.GetString(13);
                    p.ProcesoSeleccion = reader.GetString(14);
                    p.CostoInscripcionSemestral = reader.GetDecimal(15);
                    p.CostoMensualidad = reader.GetDecimal(16);
                    p.CostoCursoPropedeutico = reader.GetDecimal(17);
                    p.Activo = reader.GetBoolean(18);
                    aux.Add(p);
                }
                conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de las materias de la base de datos");
            }
        }
        public string obtenerNombrePrograma(string codigo)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT Nombre FROM programa WHERE Codigo = '" + codigo + "'";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string nombre = "";
                    nombre = reader.GetString(0);
                    return nombre;
                }
                conn.Close();
                return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error..! Error al obtener el nombre del programa de la Base de Datos");
            }
        }
        public decimal consultarCostoPrograma(string programa)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT M.Costo FROM mapaCurricular C, materia M WHERE C.Materia= M.ID AND C.Programa='" + programa + "'";
                SqlDataReader reader = cmd.ExecuteReader();
                decimal costoTotal = 0;
                while (reader.Read())
                {
                    costoTotal += reader.GetDecimal(0);
                }
                conn.Close();
                return costoTotal;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos del programa de la base de datos");
            }
        }
        public string obtenerProgramaAlumno(String rfc)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT Programa FROM alumnos WHERE RFC='" + rfc + "'";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string programa = "";
                    programa = reader.GetString(0);
                    return programa;
                }
                conn.Close();
                return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos del programa del alumno de la base de datos");
            }
        }
        public List<string> obtenerProgramasAlumno(String rfc)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT Programa FROM alumnos WHERE RFC='" + rfc + "'";
                SqlDataReader reader = cmd.ExecuteReader();
                List<string> programas = new List<string>();
                while (reader.Read())
                {
                    string programa = "";
                    programa = reader.GetString(0);
                    programas.Add(programa);
                }
                conn.Close();
                if (programas.Count != 0)
                    return programas;
                else
                    return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos del programa del alumno de la base de datos");
            }
        }
        //-------------------------------GRUPOS-------------------------------//
        public bool agregarGrupo(Grupo grupo)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string agregar = "INSERT INTO grupos (Generacion, Codigo, Programa) VALUES("
                        + " ' " + grupo.generacion + "','" + grupo.codigo + "','" + grupo.programa + "');";


                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + agregar
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar Grupo a la Base de datos");
            }
        }
        public bool actualizarGrupo(Grupo grupo)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string update = "UPDATE grupos SET Generacion='" + grupo.generacion +
                          "', Programa='" + grupo.programa + "' WHERE Codigo='" + grupo.codigo + "';";



                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + update
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al actualizar la grupo a la Base de datos");
            }
        }
        public bool desactivarGrupo(string codigo)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "UPDATE grupos SET Activo=0 WHERE Codigo='" + codigo + "';";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al eliminar programa a la Base de datos");
            }
        }
        public SqlDataAdapter obtenerGruposTable()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT G.Codigo,G.Generacion, P.Nombre FROM grupos G, programa P WHERE G.Activo=1 AND P.Codigo=G.Programa", conn); conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de grupos de la base de datos");
            }
        }
        public SqlDataAdapter obtenerGruposTable(string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT G.Codigo,G.Generacion, P.Nombre FROM grupos G, programa P WHERE " +
                    "(G.Codigo LIKE '%" + parameter + "%' or " +
                    " P.Nombre LIKE '%" + parameter + "%' or " +
                    " G.Programa LIKE '%" + parameter + "%' or " +
                    " G.Generacion LIKE '%" + parameter + "%') AND G.Activo=1 AND P.Codigo=G.Programa";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.conn);
                this.conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de las materias de la base de datos");
            }
        }
        public Grupo consultarGrupo(string codigo)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT * FROM grupos WHERE Codigo='" + codigo + "'";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Grupo g = new Grupo();
                    g.codigo = reader.GetString(1);
                    g.generacion = reader.GetString(0);
                    g.programa = reader.GetString(2);
                    conn.Close();
                    return g;
                }
                conn.Close();
                return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos del grupo de la base de datos");
            }
        }
        public List<Alumno> obtenerAlumnosGrupos(string codigo)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT A.Nombre, A.Direccion, A.Telefono1, A.Telefono2, A.Correo, A.Facebook, A.CURP, A.RFC, A.Sexo, A.EstadoCivil, A.EscuelaProcedencia, A.Carrera, A.Programa, A.Nivel, A.Fecha, A.Estado, A.Tipo FROM alumnos A, grupoAlumno G WHERE G.Alumno=A.RFC AND G.Grupo ='" + codigo + "';";
                SqlDataReader reader = cmd.ExecuteReader();
                List<Alumno> aux = new List<Alumno>();
                while (reader.Read())
                {
                    Alumno a = new Alumno();
                    a.nombre = reader.GetString(0);
                    a.direccion = reader.GetString(1);
                    a.telefono1 = reader.GetString(2);
                    a.telefono2 = reader.GetString(3);
                    a.correo = reader.GetString(4);
                    a.facebook = reader.GetString(5);
                    a.curp = reader.GetString(6);
                    a.rfc = reader.GetString(7);
                    a.sexo = reader.GetString(8);
                    a.estadoCivil = reader.GetString(9);
                    a.escuelaProcedencia = reader.GetString(10);
                    a.carrera = reader.GetString(11);
                    a.programa = reader.GetString(12);
                    a.nivel = reader.GetString(13);
                    a.fecha = reader.GetDateTime(14);
                    a.estado = reader.GetString(15);
                    a.tipo = reader.GetString(16);
                    aux.Add(a);
                }
                conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de las grupos de la base de datos");
            }
        }
        public List<Grupo> obtenerGrupos()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT * FROM grupos";
                SqlDataReader reader = cmd.ExecuteReader();
                List<Grupo> aux = new List<Grupo>();
                while (reader.Read())
                {

                    Grupo g = new Grupo();
                    g.codigo = reader.GetString(1);
                    g.generacion = reader.GetString(0);
                    g.programa = reader.GetString(2);
                    aux.Add(g);
                }
                conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de las grupos de la base de datos");
            }
        }
        public List<Grupo> obtenerGrupos(string parameter, string programa)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT G.Codigo,G.Generacion, G.Programa FROM grupos G, programa P WHERE " +
                           "(G.Codigo LIKE '%" + parameter + "%' or " +
                           " P.Nombre LIKE '%" + parameter + "%' or " +
                           " G.Programa LIKE '%" + parameter + "%' or " +
                           " P.Codigo LIKE '%" + parameter + "%' or " +
                           " G.Generacion LIKE '%" + parameter + "%') AND G.Activo=1 AND P.Codigo=G.Programa AND P.Codigo='" + programa + "';";
                SqlDataReader reader = cmd.ExecuteReader();
                List<Grupo> aux = new List<Grupo>();
                while (reader.Read())
                {

                    Grupo g = new Grupo();
                    g.codigo = reader.GetString(0);
                    g.generacion = reader.GetString(1);
                    g.programa = reader.GetString(2);
                    aux.Add(g);
                }
                conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return aux;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de las grupos de la base de datos");
            }
        }

        //-------------------------------GRUPOS DE ALUMNOS-------------------------//
        public SqlDataAdapter obtenerAlumnosGruposTable(string grupo)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT A.Nombre, A.RFC FROM grupoAlumno G, alumnos A WHERE A.RFC=G.Alumno AND G.Grupo='" + grupo + "';";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.conn);
                this.conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos del grupo de la base de datos");
            }
        }
        public SqlDataAdapter obtenerAlumnosGruposTable(string grupo, string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT A.Nombre, A.RFC FROM grupoAlumno G, alumnos A WHERE " +
                    "(A.Nombre LIKE '%" + parameter + "%' or " +
                    " A.RFC LIKE '%" + parameter + "%') " +
                    //" A.Matricula LIKE '%" + parameter + "%' or " +
                    //" G.Generacion LIKE '%" + parameter + "%') "+
                    "AND A.RFC=G.Alumno AND G.Grupo='" + grupo + "';";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.conn);
                this.conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos del grupo de la base de datos");
            }
        }
        public string consultarGrupoAlumno(string RFC)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT Grupo FROM grupoAlumno WHERE Alumno='" + RFC + "'";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string grupo = reader.GetString(0);
                    conn.Close();
                    return grupo;
                }
                conn.Close();
                return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos del alumno de la base de datos");
            }
        }
        public bool quitarAlumnoDeGrupo(string grupo, string alumno)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string delete = "DELETE FROM grupoAlumno WHERE Alumno='" + alumno + "' AND Grupo='" + grupo + "';";


                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + delete
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar Grupo a la Base de datos");
            }
        }

        //-------------------------------TALLERES-------------------------------//
        public bool agregarTaller(Taller taller)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string agregar = "INSERT INTO taller (Nombre, Fecha, CostoClientes,CostoPublico, Capacidad, Requisitos) VALUES("
                        + " ' " + taller.nombre + "','" + taller.fecha + "','" + taller.costoClientes + "','" + taller.costoPublico + "','" + taller.capacidad + "','" + taller.requisitos + "');";


                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + agregar
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar el taller a la Base de datos");
            }
        }
        public bool actualizarTaller(Taller taller)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string update = "UPDATE taller SET Nombre='" + taller.nombre +
                        "', Fecha='" + formatearFecha(taller.fecha) + "', CostoClientes='" + taller.costoClientes +
                        "', CostoPublico='" + taller.costoPublico + "', Capacidad='" + taller.capacidad +
                        "', Requisitos='" + taller.requisitos
                        + "', Estado='1' WHERE ID='" + taller.id + "';";



                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + update
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al actualizar el taller de la Base de datos");
            }
        }
        public bool cancelarTaller(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "UPDATE taller SET Estado=0 WHERE ID='" + id + "';";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al eliminar taller a la Base de datos");
            }
        }
        public SqlDataAdapter obtenerTalleresTable()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT ID, Nombre, Fecha, CostoClientes,CostoPublico, Capacidad, Requisitos FROM taller WHERE Estado=1", conn); conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de talleres de la base de datos");
            }
        }
        public SqlDataAdapter obtenerTalleresTable(string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT ID, Nombre, Fecha, CostoClientes, CostoPublico, Capacidad, Requisitos FROM taller WHERE  " +
                    "(ID LIKE '%" + parameter + "%' or " +
                    " Nombre LIKE '%" + parameter + "%' or " +
                    " Fecha LIKE '%" + parameter + "%' or " +
                    " Capacidad LIKE '%" + parameter + "%' or " +
                    " Requisitos LIKE '%" + parameter + "%' or " +
                    " CostoClientes LIKE '%" + parameter + "%' or " +
                    " CostoPublico LIKE '%" + parameter + "%') AND Estado=1";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.conn);
                this.conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de las materias de la base de datos");
            }
        }
        public Taller consultarTaller(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT ID, Nombre, Fecha, CostoClientes, CostoPublico, Capacidad, Requisitos FROM taller WHERE ID='" + id + "'";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Taller g = new Taller();
                    g.id = reader.GetInt32(0);
                    g.nombre = reader.GetString(1);
                    g.fecha = reader.GetDateTime(2);
                    g.costoClientes = reader.GetDecimal(3);
                    g.costoPublico = reader.GetDecimal(4);
                    g.capacidad = reader.GetInt32(5);
                    g.requisitos = reader.GetString(6);
                    conn.Close();
                    return g;
                }
                conn.Close();
                return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos del grupo de la base de datos");
            }
        }
        public TallerAsistente obtenerAsistenteTaller(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT A.Nombre,A.Telefono,A.Correo,A.CURP,A.RFC,A.Costo,A.Pago, A.Taller FROM tallerAsistentes A, taller T WHERE T.ID = A.Taller AND A.ID='" + id + "';";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TallerAsistente a = new TallerAsistente();
                    a.ID = Convert.ToInt32(id);
                    a.nombre = reader.GetString(0);
                    a.telefono = reader.GetString(1);
                    a.correo = reader.GetString(2);
                    a.curp = reader.GetString(3);
                    a.rfc = reader.GetString(4);
                    a.costo = reader.GetDecimal(5);
                    a.pago = reader.GetDecimal(6);
                    a.restante = a.costo - a.pago;
                    a.taller = reader.GetInt32(7);
                    conn.Close();
                    return a;
                }
                return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de los asistentes del taller de la base de datos");
            }
        }
        public List<TallerAsistente> obtenerAsistentesTalleres(string taller)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT A.Nombre,A.Telefono,A.Correo,A.CURP,A.RFC,A.Costo,A.Pago FROM tallerAsistentes A, taller T WHERE T.ID = A.Taller AND T.ID='" + taller + "';";
                SqlDataReader reader = cmd.ExecuteReader();
                List<TallerAsistente> aux = new List<TallerAsistente>();
                while (reader.Read())
                {
                    TallerAsistente a = new TallerAsistente();
                    a.nombre = reader.GetString(0);
                    a.telefono = reader.GetString(1);
                    a.correo = reader.GetString(2);
                    a.curp = reader.GetString(3);
                    a.rfc = reader.GetString(4);
                    a.costo = reader.GetDecimal(5);
                    a.pago = reader.GetDecimal(6);
                    a.restante = a.costo - a.pago;
                    aux.Add(a);
                }
                conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de los asistentes del taller de la base de datos");
            }
        }
        public SqlDataAdapter obtenerAsistentesTalleresTable(string taller)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT A.ID, A.Nombre,A.Telefono,A.Correo,A.CURP,A.RFC,A.Costo,A.Pago, A.Costo-A.Pago AS 'Restante' FROM tallerAsistentes A, taller T WHERE T.ID = A.Taller AND T.ID='" + taller + "'", conn);
                conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de talleres de la base de datos");
            }
        }
        public SqlDataAdapter obtenerAsistentesTalleresTable(string taller, string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT A.ID, A.Nombre,A.Telefono,A.Correo,A.CURP,A.RFC,A.Costo,A.Pago,A.Costo-A.Pago AS 'Restante' FROM tallerAsistentes A, taller T WHERE " +
                    "(A.Nombre LIKE '%" + parameter + "%' or " +
                    " A.Telefono LIKE '%" + parameter + "%' or " +
                    " A.CURP LIKE '%" + parameter + "%' or " +
                    " A.RFC LIKE '%" + parameter + "%' or " +
                    " A.Correo LIKE '%" + parameter + "%') AND  T.ID = A.Taller AND T.ID = '" + taller + "';";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.conn);
                this.conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de las materias de la base de datos");
            }
        }
        public List<Taller> obtenerTalleres()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT ID, Nombre, Fecha, CostoClientes, CostoPublico, Capacidad, Requisitos FROM taller WHERE Estado=1";
                SqlDataReader reader = cmd.ExecuteReader();
                List<Taller> aux = new List<Taller>();
                while (reader.Read())
                {
                    Taller g = new Taller();
                    g.id = reader.GetInt32(0);
                    g.nombre = reader.GetString(1);
                    g.fecha = reader.GetDateTime(2);
                    g.costoClientes = reader.GetDecimal(3);
                    g.costoPublico = reader.GetDecimal(4);
                    g.capacidad = reader.GetInt32(5);
                    g.requisitos = reader.GetString(6);
                    aux.Add(g);
                }
                conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de los talleres de la base de datos");
            }
        }
        public bool registrarAsistenteTaller(TallerAsistente asistente)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string inscribir = "INSERT INTO tallerAsistentes (Taller, Nombre, Telefono, Correo, CURP, RFC, Costo, Observaciones) VALUE ( '"
                     + asistente.taller + "','" + asistente.nombre + "','" + asistente.telefono + "','" + asistente.correo
                     + "','" + asistente.curp + "','" + asistente.rfc + "','" + asistente.costo
                     + "','" + asistente.observaciones + "');";
                if (asistente.ID > 0)
                {
                    inscribir = "UPDATE tallerAsistentes SET Taller='" + asistente.taller + "',Nombre='" + asistente.nombre +
                    "',Telefono='" + asistente.telefono + "',Correo='" + asistente.correo + "',CURP='" + asistente.curp +
                    "',RFC='" + asistente.rfc + "',Costo='" + asistente.costo +
                    "',Observaciones='" + asistente.observaciones + "' WHERE ID = " + asistente.ID + ";";
                }


                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + inscribir
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar asistencia a la Base de datos");
            }
        }
        public bool borrarAsistenteTaller(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string inscribir = "DELETE FROM tallerAsistentes WHERE ID=" + id + "; ";


                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + inscribir
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n Error al borrar asistencia a la Base de datos");
            }
        }
        public bool registrarPagoAsistenciaTaller(Pago pago, string idAsistente)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string updateAsistente = "UPDATE tallerAsistentes SET Pago = Pago + " + pago.cantidad +
                                            " WHERE ID = " + idAsistente + ";";
                string agregarPago = "INSERT INTO pagos (Emisor, FechaPago, Cantidad, Concepto, Area, Observaciones, Recibio, Parent_ID) VALUES ('"
                    + pago.emisor + "', '" + formatearFecha(pago.fechaPago) + "'," + pago.cantidad + ", 'Pago de Taller', 'Escuela', '"
                    + pago.observaciones + "', '" + pago.recibio + "', '" + idAsistente + "');";


                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + updateAsistente
                                    + agregarPago
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar Pago de taller a la Base de datos");
            }
        }

        //-------------------------------ENTREGA DOCUMENTOS-------------------------------//
        public bool agregarEntregaDocumentos(DocumentosInscripcion doc)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "INSERT INTO documentosInscripcion (Alumno, ActaNacimientoOrg, ActaNacimientoCop, TituloCedulaOrg, TituloLicCop, "
                     + "CedProfCop, SolicitudOpcionTitulacion, CertificadoLicCop, ConstanciaLibSSOrg, Curp, Fotografias, RecibioEmpleado, TipoInscripcion) VALUES ('"
                     + doc.alumno + "', " + doc.actaNacimientoOrg + ", " + doc.actaNacimientoCop + ", " + doc.tituloCedulaOrg + ", " + doc.tituloLicCop + ", "
                     + doc.cedProfCop + ", " + doc.solicitudOpcTitulacion + ", " + doc.certificadoLicCop + ", " + doc.constanciaLibSSOrg + ", " + doc.curp + ", "
                     + doc.fotografias + ", '" + doc.recibioEmpleado + "', " + doc.tipoInscripcion + ")";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception E)
            {
                conn.Close();
                throw new Exception("Error al agregar la documentacion del alumnos a la base d datos");
            }
        }
        public SqlDataAdapter obtenerEntregaDocumentos()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT Alumno, ActaNacimientoOrg, ActaNacimientoCop, TituloCedulaOrg, TituloLicCop, CedProfCop, SolicitudOpcionTitulacion, CertificadoLicCop, ConstanciaLibSSOrg, Curp, Fotografias, RecibioEmpleado FROM documentosInscripcion", conn);
                conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de la documentacion entregada del alumno de la base de datos");
            }
        }
        public SqlDataAdapter obtenerEntregaDocumentosTable(string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT Alumno, ActaNacimientoOrg, ActaNacimientoCop, TituloCedulaOrg, TituloLicCop, CedProfCop, SolicitudOpcionTitulacion, CertificadoLicCop, ConstanciaLibSSOrg, Curp, Fotografias, RecibioEmpleado FROM documentosInscripcion WHERE" +
                    "(Alumno LIKE '%" + parameter + "%' or " +
                    "RecibioEmpleado LIKE '%" + parameter + "%')";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, conn);
                conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de la documentacion entregada del alumno de la base de datos");
            }
        }
        public DocumentosInscripcion consultarEntregaDocumentos(string rfc)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT * FROM documentosInscripcion WHERE Alumno='" + rfc + "'";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DocumentosInscripcion doc = new DocumentosInscripcion();
                    doc.alumno = reader.GetString(0);
                    doc.actaNacimientoOrg = reader.GetBoolean(1);
                    doc.actaNacimientoCop = reader.GetBoolean(2);
                    doc.tituloCedulaOrg = reader.GetBoolean(3);
                    doc.tituloLicCop = reader.GetBoolean(4);
                    doc.cedProfCop = reader.GetBoolean(5);
                    doc.solicitudOpcTitulacion = reader.GetBoolean(6);
                    doc.certificadoLicCop = reader.GetBoolean(7);
                    doc.constanciaLibSSOrg = reader.GetBoolean(8);
                    doc.curp = reader.GetBoolean(9);
                    doc.fotografias = reader.GetBoolean(10);
                    doc.recibioEmpleado = reader.GetString(11);
                    doc.tipoInscripcion = reader.GetInt32(12);
                    conn.Close();
                    return doc;
                }
                conn.Close();
                return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de la documentacion entregada del alumno de la base de datos");
            }
        }
        public bool actualizarEntregaDocumentos(DocumentosInscripcion doc)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "UPDATE documentosInscripcion SET ActaNacimientoOrg= " + doc.actaNacimientoOrg + ", ActaNacimientoCop= " + doc.actaNacimientoCop + ", TituloCedulaOrg= " + doc.tituloCedulaOrg
                        + ", TituloLicCop= " + doc.tituloLicCop + ", CedProfCop= " + doc.cedProfCop + ", SolicitudOpcionTitulacion =" + doc.solicitudOpcTitulacion + ", CertificadoLicCop= " + doc.certificadoLicCop
                        + ", ConstanciaLibSSOrg =" + doc.constanciaLibSSOrg + ", Curp =" + doc.curp + ", Fotografias =" + doc.fotografias + ", RecibioEmpleado ='" + doc.recibioEmpleado + "' WHERE Alumno = '" + doc.alumno + "'";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al actualizar los datos de la documentacion entregada del alumno en la Base de Datos");
            }
        }

        //-------------------------------EMPLEADOS-------------------------------//
        public bool agregarEmpleado(Empleado empleado, Usuario usuario)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string empleados = " INSERT INTO empleados (Matricula, Correo, Nombre, Telefono, Puesto) VALUES('"
                    + empleado.Matricula + "','" + empleado.Correo + "','" + empleado.Nombre + "','" + empleado.Telefono + "','" + empleado.Puesto + "');";
                string usuarios = "";
                if (usuario != null)
                {
                    usuarios = " INSERT INTO usuarios (Estado, Matricula, Usuario, Contrasena, Nivel_Acceso) VALUES(1,'" + empleado.Matricula + "','" + usuario.Nombre_De_Usuario + "','" + usuario.Contrasena + "','" + usuario.Nivel_Acceso + "');";
                }


                cmd.CommandText = "BEGIN TRANSACTION; " + empleados + usuarios + " COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar el empleado de la Base de datos");
            }
        }
        public bool actualizarEmpleado(Empleado empleado, Usuario usuario)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string emp = "UPDATE empleados SET Nombre='" + empleado.Nombre + "',Telefono='" + empleado.Telefono +
                        "',Puesto='" + empleado.Puesto + "',Correo='" + empleado.Correo +
                        "' WHERE ID=" + empleado.ID + " OR Matricula='" + empleado.Matricula + "';";
                string user = "";
                if (usuario != null)
                {
                    user = "INSERT INTO usuarios(Estado, Matricula, Usuario, Contrasena, Nivel_Acceso) SELECT 1,'" + empleado.Matricula + "','" + usuario.Nombre_De_Usuario + "','" + usuario.Contrasena + "','" + usuario.Nivel_Acceso + "'  WHERE NOT EXISTS(SELECT * FROM usuarios WHERE Matricula = '" + empleado.Matricula + "';";
                    user += " UPDATE usuarios SET " + "Usuario='" + usuario.Nombre_De_Usuario + "',Contrasena='" + usuario.Contrasena + "', Estado='"
                    + usuario.Estado + "' WHERE Matricula='" + empleado.Matricula + "';";
                }
                cmd.CommandText = "BEGIN TRANSACTION; " +
                    emp + user + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al actualizar el empleado de la Base de datos");
            }
        }
        public bool actualizarEmpleado(Empleado empleado)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "UPDATE empleados SET " +
                   "Nombre='" + empleado.Nombre + "',Telefono='" + empleado.Telefono + "',Puesto='" + empleado.Puesto + "',Correo='" + empleado.Correo +
                   "' WHERE ID=" + empleado.ID + " OR Matricula='" + empleado.Matricula + "';";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al actualizar el empleado de la Base de datos");
            }
        }
        public bool desactivarEmpleado(int id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "UPDATE empleados SET Estado=0 WHERE ID='" + id + "';";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al eliminar el empleado de la Base de datos");
            }
        }
        public bool desactivarEmpleado(string matricula)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string empleados = "UPDATE empleados SET Estado=0 WHERE Matricula='" + matricula + "';";
                string usuariosQuery = "UPDATE usuarios SET Estado=0 WHERE Matricula='" + matricula + "';";

                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + empleados
                                    + usuariosQuery
                                    + "COMMIT;"; int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al desactivar el usuario de la Base de datos");
            }
        }
        public SqlDataAdapter obtenerEmpleadosTable()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT E.ID, E.Matricula, E.Nombre,E.Telefono,E.Puesto, E.Correo FROM empleados E WHERE E.Estado = 1", conn);
                conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de los empleados de la base de datos");
            }
        }
        public SqlDataAdapter obtenerEmpleadosTable(string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT E.ID, E.Matricula, E.Nombre,E.Telefono,E.Puesto, E.Correo FROM empleados E WHERE " +
                    "(E.Correo LIKE '%" + parameter + "%' or " +
                    " E.Nombre LIKE '%" + parameter + "%' or " +
                    " E.Telefono LIKE '%" + parameter + "%' or " +
                    " E.Puesto LIKE '%" + parameter + "%' or " +
                    " E.Matricula LIKE '%" + parameter + "%') and " +
                    " E.Estado = 1 ORDER BY E.Nombre ASC";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.conn);
                this.conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de los empleados de la base de datos");
            }
        }
        public Empleado consultarEmpleado(string matricula)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT E.ID, E.Matricula, E.Nombre, E.Telefono, E.Puesto, E.Correo FROM empleados E WHERE E.Matricula='" + matricula + "'";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Empleado a = new Empleado();
                    a.ID = reader.GetInt32(0);
                    a.Matricula = reader.GetString(1);
                    a.Nombre = reader.GetString(2);
                    a.Telefono = reader.GetString(3);
                    a.Puesto = reader.GetString(4);
                    a.Correo = reader.GetString(5);
                    conn.Close();
                    return a;
                }
                conn.Close();
                return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos del empleado de la base de datos");
            }
        }
        public List<Empleado> obtenerEmpleados()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT E.ID, E.Matricula, E.Nombre, E.Telefono, E.Puesto, E.Correo FROM empleados E WHERE E.Estado = 1";
                SqlDataReader reader = cmd.ExecuteReader();
                List<Empleado> aux = new List<Empleado>();
                while (reader.Read())
                {
                    Empleado a = new Empleado();
                    a.ID = reader.GetInt32(0);
                    a.Matricula = reader.GetString(1);
                    a.Nombre = reader.GetString(2);
                    a.Telefono = reader.GetString(3);
                    a.Puesto = reader.GetString(4);
                    a.Correo = reader.GetString(5);
                    aux.Add(a);
                }
                conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de los empleados de la base de datos");
            }
        }
        public string obtenerNombreEmpleado(string matricula)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT Nombre FROM empleados WHERE Matricula = '" + matricula + "'";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string nombre = "";
                    nombre = reader.GetString(0);
                    return nombre;
                }
                conn.Close();
                return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error..! Error al obtener el nombre del empleado de la Base de Datos");
            }
        }
        public string validarMatricula(string matricula)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT Matricula FROM empleados WHERE Matricula = '" + matricula + "'; SELECT Matricula FROM psicoterapeutas WHERE Matricula = '" + matricula + "';";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string nombre = "";
                    try
                    {
                        nombre += reader.GetString(0);
                    }
                    catch (Exception ex) { }
                    try
                    {
                        nombre += reader.GetString(1);
                    }
                    catch (Exception ex) { }
                    return nombre;
                }
                conn.Close();
                return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error..! Error al obtener el nombre del empleado de la Base de Datos");
            }
        }
        public string obtenerOrigenMatricula(string matricula)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT 'Empleado' FROM empleados E WHERE E.Matricula = '" + matricula + "' UNION SELECT 'Psicoterapeuta' FROM psicoterapeutas P WHERE P.Matricula = '" + matricula + "'";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string nombre = "";
                    nombre = reader.GetString(0);
                    return nombre;
                }
                conn.Close();
                return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error..! Error al obtener el tipo de la Base de Datos");
            }
        }


        //-----------------------------INSCRIPCIONES---------------------------------//
        public bool inscribirAlumnoPrograma(string RFC, string programa)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string agregar = "INSERT INTO programaAlumno (Alumno, Programa, Estado) VALUES("
                        + "','" + RFC + "','" + programa + "','Inscrito');";
                string updateDatosAlumno = "UPDATE alumno SET Programa = '" + programa + "' WHERE RFC='" + RFC + "';";


                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + agregar
                                    + updateDatosAlumno
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar Prorgrama a la Base de datos");
            }
        }
        public bool inscribirAlumnoGrupo(string RFC, string grupo, string programa)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string inscribirGrupo = "INSERT INTO grupoAlumno (Alumno, Grupo) VALUES('"
                     + RFC + "','" + grupo + "'); ";
                string inscribirPrograma = "INSERT INTO programaAlumno (Alumno, Programa, Estado) VALUES('"
                    + RFC + "','" + programa + "','Inscrito'); ";
                decimal cantidad = 0;
                try
                {
                    cantidad = consultarCostoPrograma(programa);
                }
                catch (Exception ex) { }
                string registroCobroInscripcion = "INSERT INTO cobrosAlumno (Alumno,Concepto,Cantidad,Pago,Restante,Fecha,Parent_ID) VALUES('"
                    + RFC + "','Colegiatura','" + cantidad + "','0.00','" + cantidad + "','" + formatearFecha(DateTime.Now) + "','" + RFC + "');";
                string updateDatosAlumno = "UPDATE alumnos SET Programa = '" + programa + "', Estado='Registrado' WHERE RFC='" + RFC + "';";


                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + inscribirGrupo
                                    + inscribirPrograma
                                    + updateDatosAlumno
                                    + registroCobroInscripcion; int rowsAfected = cmd.ExecuteNonQuery();
                cmd.CommandText = "COMMIT;";
                cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                cmd.CommandText = "ROLLBACK;";
                cmd.ExecuteNonQuery();
                conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar Prorgrama a la Base de datos");
            }
        }

        //-------------------------------ASISTENCIAS-------------------------------//
        public List<PaseDeListaAlumno> obtenerAsistenciaAlumnosMateriaTable(string grupo, int materia, List<Alumno> alumnos)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT G.Alumno, A.Nombre, P.Estado, P.Fecha,P.isTarde FROM grupoAlumno G INNER JOIN alumnos A on A.RFC=G.Alumno inner JOIN pasesDeListaAlumnos P ON A.RFC=P.Alumno WHERE P.Grupo='" + grupo + "' AND P.Materia=' " + materia.ToString() + "' ORDER BY P.Fecha ASC;";

                cmd.CommandText = sqlString;

                SqlDataReader reader = cmd.ExecuteReader();
                List<PaseDeListaAlumno> aux = new List<PaseDeListaAlumno>();
                foreach (Alumno alu in alumnos)
                {
                    PaseDeListaAlumno pls = new PaseDeListaAlumno();
                    pls.RFC = alu.rfc;
                    pls.alumno = alu.nombre;
                    pls.asistencias = null;
                    aux.Add(pls);
                }
                string rfc;
                string nombre;
                Asistencias asistencia;
                while (reader.Read())
                {
                    rfc = reader.GetString(0);
                    nombre = reader.GetString(1);
                    asistencia = new Asistencias();
                    asistencia.Estado = reader.GetString(2);
                    asistencia.Fecha = reader.GetDateTime(3);
                    asistencia.isTarde = reader.GetBoolean(4);
                    int index = 0;
                    foreach (PaseDeListaAlumno pl in aux)
                    {
                        if (pl.RFC == rfc)
                        {
                            if (aux.ElementAt(index).asistencias == null)
                                aux.ElementAt(index).asistencias = new List<Asistencias>();
                            aux.ElementAt(index).asistencias.Add(asistencia);
                        }
                        index++;
                    }
                }
                conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de los listas de la base de datos");
            }
        }
        public bool registrarAsistencias(List<PaseDeListaAlumno> lista, string maestro, string grupo, string materia, string fecha)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string paseDeLista = "INSERT INTO pasesDeLista (Grupo, Materia, Fecha, Encargado) VALUES('"
                    + grupo + "','" + materia + "','" + fecha + "','" + maestro + "'); ";
                string paseDeListaAlumnos = "";
                foreach (PaseDeListaAlumno aux in lista)
                {
                    paseDeListaAlumnos += " INSERT INTO pasesDeListaAlumnos (ID,Alumno, Estado, Fecha, Grupo, Materia, isTarde) "
                        + " SELECT AUTO_INCREMENT , '" + aux.RFC + "','" + aux.asistencias.First().Estado + "','" + fecha + "','" + grupo + "','" + materia + "'," + aux.asistencias.First().isTarde + " FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '" + this.database + "' AND TABLE_NAME = 'pasesDeLista'; ";
                }


                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + paseDeListaAlumnos
                                    + paseDeLista
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n Error al registrar asistencias a la Base de datos");
            }
        }
       
        //-------------------------------CALIFICACIONES-------------------------------//
        public List<CalificacionesAlumno> obtenerCalificacionesAlumnosMateriaTable(string grupo, int materia, List<Alumno> alumnos)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT G.Alumno, A.Nombre, C.CalificacionTareas, C.CalificacionFinal FROM grupoAlumno G INNER JOIN alumnos A on A.RFC=G.Alumno inner JOIN calificacionAlumno C ON A.RFC=C.Alumno WHERE C.Grupo='" + grupo + "' AND C.Materia='" + materia.ToString() + "' ;";

                cmd.CommandText = sqlString; SqlDataReader reader = cmd.ExecuteReader();
                List<CalificacionesAlumno> aux = new List<CalificacionesAlumno>();
                foreach (Alumno alu in alumnos)
                {
                    CalificacionesAlumno pls = new CalificacionesAlumno();
                    pls.RFC = alu.rfc;
                    pls.alumno = alu.nombre;
                    pls.calificaciones = null;
                    aux.Add(pls);
                }
                string rfc;
                string nombre;
                Calificacion calificacion;
                while (reader.Read())
                {
                    rfc = reader.GetString(0);
                    nombre = reader.GetString(1);
                    calificacion = new Calificacion();
                    calificacion.calificacionTareas = reader.GetFloat(2);
                    calificacion.calificacionFinal = reader.GetFloat(3);
                    calificacion.materia = materia;
                    int index = 0;
                    foreach (CalificacionesAlumno pl in aux)
                    {
                        if (pl.RFC == rfc)
                        {
                            if (aux.ElementAt(index).calificaciones == null)
                                aux.ElementAt(index).calificaciones = new List<Calificacion>();
                            aux.ElementAt(index).calificaciones.Add(calificacion);
                        }
                        index++;
                    }
                }
                conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de las calificaciones de la base de datos");
            }
        }
        public bool registrarCalificaciones(List<CalificacionesAlumno> lista, string grupo, string materia)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string calificaciones = "";
                foreach (CalificacionesAlumno aux in lista)
                {
                    calificaciones += "INSERT INTO calificacionAlumno (Grupo, Materia, Alumno, CalificacionTareas,CalificacionFinal) SELECT '" + grupo + "','" + materia + "','" + aux.RFC + "','" + aux.calificaciones.ElementAt(0).calificacionTareas + "','"
                    + aux.calificaciones.ElementAt(0).calificacionFinal + "'  WHERE NOT EXISTS (SELECT * FROM calificacionAlumno WHERE Grupo='" + grupo + "' AND Materia ='" + materia + "' AND Alumno ='" + aux.RFC + "'); ";

                }


                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + calificaciones
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar calificaciones a la Base de datos");
            }
        }
        public List<Calificacion> obtenerCalificacionesAlumno(string alumno, string grupo)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT M.Nombre , C.Materia, C.CalificacionTareas, C.CalificacionFinal FROM materia M, grupoAlumno G INNER JOIN alumnos A on A.RFC=G.Alumno inner JOIN calificacionAlumno C ON A.RFC=C.Alumno WHERE C.Grupo='" + grupo + "' AND M.ID=C.Materia AND C.Alumno='" + alumno + "' ;";
                SqlDataReader reader = cmd.ExecuteReader();
                List<Calificacion> aux = new List<Calificacion>();
                while (reader.Read())
                {
                    Calificacion cal = new Calificacion();
                    cal.materiaNombre = reader.GetString(0);
                    cal.materia = reader.GetInt32(1);
                    cal.calificacionTareas = reader.GetFloat(2);
                    cal.calificacionFinal = reader.GetFloat(3);
                    aux.Add(cal);
                }
                conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener calificaciones del alumno de la base de datos");
            }
        }
        public SqlDataAdapter consultarCalificacionesAlumno(string alumno, string grupo)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT M.Nombre AS 'Materia', C.CalificacionTareas, C.CalificacionFinal FROM materia M, grupoAlumno G INNER JOIN alumnos A on A.RFC=G.Alumno inner JOIN calificacionAlumno C ON A.RFC=C.Alumno WHERE C.Grupo='" + grupo + "' AND M.ID=C.Materia AND C.Alumno='" + alumno + "' ;", conn);
                conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener calificaciones del alumno de la base de datos");
            }
        }

        //-------------------------------PSICOTERAPEUTAS-------------------------------//
        public bool agregarPsicoterapeuta(Psicoterapeuta psicoterapeuta, Usuario usuario)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string psicoterapeutaQuery = "INSERT INTO psicoterapeutas (Matricula, Nombre, Telefono, Carrera, Especialidad, Horario, Observaciones, Estado) VALUES('"
                        + psicoterapeuta.Matricula + "','" + psicoterapeuta.Nombre + "','" + psicoterapeuta.Telefono + "','" + psicoterapeuta.Carrera +
                        "','" + psicoterapeuta.Especialidad + "','" + psicoterapeuta.Horario + "','" + psicoterapeuta.Observaciones + "',1);";
                string usuariosQuery = "";
                if (usuario != null)
                {
                    usuariosQuery = "INSERT INTO usuarios (Estado, Matricula, Usuario, Contrasena, Nivel_Acceso) VALUES("
                        + usuario.Estado + ",'" + psicoterapeuta.Matricula + "','" + usuario.Nombre_De_Usuario + "','" + usuario.Contrasena + "','" + usuario.Nivel_Acceso + "');";
                }


                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + psicoterapeutaQuery
                                    + usuariosQuery
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar al Psicoterapeuta de la Base de datos");
            }
        }
        public bool actualizarPsicoterapeuta(Psicoterapeuta psicoterapeuta, Usuario usuario)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string psicoterapeutaQuery = "UPDATE psicoterapeutas SET Matricula='" + psicoterapeuta.Matricula + "', Nombre='" + psicoterapeuta.Nombre +
                        "', Telefono='" + psicoterapeuta.Telefono + "', Carrera='" + psicoterapeuta.Carrera + "', Especialidad='" + psicoterapeuta.Especialidad +
                        "', Horario='" + psicoterapeuta.Horario + "', Observaciones='" + psicoterapeuta.Observaciones +
                        "', Estado=1 WHERE ID = " + psicoterapeuta.ID + " OR Matricula = '" + psicoterapeuta.Matricula + "';";
                string usuariosQuery = "";
                if (usuario != null)
                {
                    usuariosQuery = "INSERT INTO usuarios(Estado, Matricula, Usuario, Contrasena, Nivel_Acceso) SELECT 1,'" + psicoterapeuta.Matricula + "','" + usuario.Nombre_De_Usuario + "','" + usuario.Contrasena + "','" + usuario.Nivel_Acceso + "'  WHERE NOT EXISTS(SELECT * FROM usuarios WHERE Matricula = '" + psicoterapeuta.Matricula + "';";
                    usuariosQuery += " UPDATE usuarios SET " + "Usuario='" + usuario.Nombre_De_Usuario + "',Contrasena='" + usuario.Contrasena + "', Estado=1 WHERE Matricula='" + psicoterapeuta.Matricula + "';";
                }
                cmd.CommandText = "BEGIN TRANSACTION; " +
                                    psicoterapeutaQuery +
                                    usuariosQuery +
                                    "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al actualizar al psicoterapeuta de la Base de datos");
            }
        }
        public bool actualizarPsicoterapeuta(Psicoterapeuta psicoterapeuta)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "UPDATE psicoterapeutas SET Matricula='" + psicoterapeuta.Matricula + "', Nombre='" + psicoterapeuta.Nombre +
                    "', Telefono='" + psicoterapeuta.Telefono + "', Carrera='" + psicoterapeuta.Carrera + "', Especialidad='" + psicoterapeuta.Especialidad +
                    "', Horario='" + psicoterapeuta.Horario + "', Observaciones='" + psicoterapeuta.Observaciones +
                    "', Estado=1 WHERE ID = " + psicoterapeuta.ID + " OR Matricula = '" + psicoterapeuta.Matricula + "';";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al actualizar al psicoterapeuta de la Base de datos");
            }
        }
        public bool desactivarPsicoterapeuta(int id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "UPDATE psicoterapeutas SET Estado=0 WHERE ID=" + id + ";";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al desactivar al psicoterapeuta de la Base de datos");
            }
        }
        public bool desactivarPsicoterapeuta(string matricula)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string psicoterapeutasQuery = "UPDATE psicoterapeutas SET Estado=0 WHERE Matricula='" + matricula + "';";
                string usuariosQuery = "UPDATE usuarios SET Estado=0 WHERE Matricula='" + matricula + "';";


                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + psicoterapeutasQuery
                                    + usuariosQuery
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al desactivar al psicoterapeuta de la Base de datos");
            }
        }
        public SqlDataAdapter obtenerPsicoterapeutasTable()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT ID, Matricula, Nombre, Telefono, Carrera, Especialidad, Horario FROM psicoterapeutas WHERE Estado = 1", conn);
                conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de los psicoterapeutas de la base de datos");
            }
        }
        public SqlDataAdapter obtenerPsicoterapeutasTable(string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT ID, Matricula, Nombre, Telefono, Carrera, Especialidad, Horario FROM psicoterapeutas WHERE " +
                    "(Matricula LIKE '%" + parameter + "%' or " +
                    " Nombre LIKE '%" + parameter + "%' or " +
                    " Telefono LIKE '%" + parameter + "%' or " +
                    " Carrera LIKE '%" + parameter + "%' or " +
                    " Horario LIKE '%" + parameter + "%' or " +
                    " Especialidad  LIKE '%" + parameter + "%') and " +
                    " Estado = 1";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.conn);
                this.conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de los psicoterapeutas de la base de datos");
            }
        }
        public Psicoterapeuta consultarPsicoterapeuta(string matricula)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT ID, Matricula, Nombre, Telefono, Carrera, Especialidad, Horario, Observaciones, Estado FROM psicoterapeutas WHERE Matricula='" + matricula + "'";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Psicoterapeuta a = new Psicoterapeuta();
                    a.ID = reader.GetInt32(0);
                    a.Matricula = reader.GetString(1);
                    a.Nombre = reader.GetString(2);
                    a.Telefono = reader.GetString(3);
                    a.Carrera = reader.GetString(4);
                    a.Especialidad = reader.GetString(5);
                    a.Horario = reader.GetString(6);
                    a.Observaciones = reader.GetString(7);
                    a.Estado = reader.GetString(8);
                    conn.Close();
                    return a;
                }
                conn.Close();
                return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos del Psicoterapeuta de la base de datos");
            }
        }
        public List<Psicoterapeuta> obtenerPsicoterapeutas()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT ID, Matricula, Nombre, Telefono, Carrera, Especialidad, Horario, Observaciones, Estado FROM psicoterapeutas WHERE Estado = 1";
                SqlDataReader reader = cmd.ExecuteReader();
                List<Psicoterapeuta> aux = new List<Psicoterapeuta>();
                while (reader.Read())
                {
                    Psicoterapeuta a = new Psicoterapeuta();
                    a.ID = reader.GetInt32(0);
                    a.Matricula = reader.GetString(1);
                    a.Nombre = reader.GetString(2);
                    a.Telefono = reader.GetString(3);
                    a.Carrera = reader.GetString(4);
                    a.Especialidad = reader.GetString(5);
                    a.Horario = reader.GetString(6);
                    a.Observaciones = reader.GetString(7);
                    a.Estado = reader.GetString(8);
                    aux.Add(a);
                }
                conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de los Psicoterapeuta de la base de datos");
            }
        }
        public SqlDataAdapter obtenerConsultasPsicoterapeutaTable(string matricula)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string query = "";
                query += "SELECT S.Fecha, concat_ws(' ',P.Nombre, P.Apellidos) AS 'Paciente', 'Sesión' AS 'Tipo' FROM sesiones S INNER JOIN pacientes P on S.Paciente_ID= P.ID WHERE S.Psicoterapeuta_ID = '" + matricula + "' AND S.Estado = 'Activa' ";
                query += "UNION SELECT E.Fecha, concat_ws(' ',P.Nombre, P.Apellidos) AS 'Paciente', 'Evaluación' AS 'Tipo' FROM evaluaciones E INNER JOIN pacientes P on E.Paciente_ID= P.ID WHERE E.Psicoterapeuta_ID = '" + matricula + "' AND E.Estado = 'Activa' ";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(query, conn);
                conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de los psicoterapeutas de la base de datos");
            }
        }
        public SqlDataAdapter obtenerPacientesPsicoterapeutaTable(string matricula)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT P.ID, concat_ws(' ',P.Nombre, P.Apellidos) AS 'Nombre', P.EscuelaEmpresa AS 'Institucion',P.Telefono FROM pacientes P  WHERE P.Psicoterapeuta='" + matricula + "' AND P.Estado = 1", conn);
                conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de pacientes de la base de datos");
            }
        }
        public SqlDataAdapter obtenerPacientesPsicoterapeutaTable(string matricula, string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string query = "SELECT P.ID, concat_ws(' ',P.Nombre, P.Apellidos) AS 'Nombre', P.EscuelaEmpresa AS 'Institucion',P.Telefono FROM pacientes P WHERE P.Psicoterapeuta='" + matricula + "' AND P.Estado = 1 AND (" +
                    " P.ID LIKE '%" + parameter + "%' or " +
                    " P.EscuelaEmpresa LIKE '%" + parameter + "%' or " +
                    " P.Telefono LIKE '%" + parameter + "%' or " +
                    " concat_ws(' ',P.Nombre, P.Apellidos) LIKE '%" + parameter + "%');";
                ;
                SqlDataAdapter mdaDatos = new SqlDataAdapter(query, conn);
                conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de pacientes de la base de datos");
            }
        }

        //--------------------------------NOMINA--------------------------------------//
        public List<string> obtenerConsultasPsicoterapeutaPendientes(string matricula,DateTime inicio ,DateTime fin)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string query = "";
                query += "SELECT 's' AS Tipo, S.Costo FROM sesiones S WHERE S.Psicoterapeuta_ID = '" + matricula + "' AND S.Estado = 'Activa' AND S.Fecha BETWEEN '" + formatearFecha(inicio) + "' AND '" + formatearFecha(fin) + "' ";
                query += "UNION SELECT 'e' AS Tipo E.Costo FROM evaluaciones E WHERE E.Psicoterapeuta_ID = '" + matricula + "' AND E.Estado = 'Activa' AND E.Fecha BETWEEN '" + formatearFecha(inicio) + "' AND '" + formatearFecha(fin) + "'";


                cmd.CommandText = query;
                SqlDataReader reader = cmd.ExecuteReader();
                List<string> aux = new List<string>();
                while (reader.Read())
                {
                    aux.Add(reader.GetString(0));
                    aux.Add(reader.GetDecimal(1).ToString());
                }
                conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de la nomina de la base de datos");
            }
        }
        public bool agregarNomina(Nomina nomina)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string nominaQuery = "INSERT INTO nominas (Psicoterapeuta,	DiaEntrega,	FechaInicio, FechaFin, Total, Estado) VALUES("
                        + nomina.Psicoterapeutas + ",'" + formatearFecha(nomina.DiaEntrega) + "','" + formatearFecha(nomina.FechaInicio) + "','" + formatearFecha(nomina.FechaFin) + "'," + nomina.Total + ",'Pagada');";

                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + nominaQuery
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar pago de nomina a la Base de datos");
            }
        }
        public bool actualizarNomina(Nomina nomina)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string nominaQuery = "UPDATE nominas SET=Psicoterapeuta='" + nomina.Psicoterapeutas + "', DiaEntrega='" + formatearFecha(nomina.DiaEntrega) +
                   "',	FechaInicio='" + formatearFecha(nomina.FechaInicio) + "', FechaFin='" + formatearFecha(nomina.FechaFin) + "', Total=" + nomina.Total + ", Estado='Pagada';";

                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + nominaQuery
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar pago de nomina a la Base de datos");
            }
        }
        public DateTime consultarUltimaFechaNomina(string matricula)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT DiaEntrega FROM nominas WHERE Psicoterapeuta='" + matricula + "' ORDER BY DiaEntrega DESC LIMIT 1";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DateTime date = reader.GetDateTime(0);
                    conn.Close();
                    return date;
                }
                conn.Close();
                return new DateTime(2000, 01, 01);
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de nomina de la base de datos");
            }
        }
        public SqlDataAdapter obtenerNominaPacienteTable(string matricula,string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string query = "SELECT ID,DiaEntrega,FechaInicio,FechaFin,Total FROM nominas WHERE Psicoterapeuta='" + matricula + "' AND " +
                    " (ID LIKE '%" + parameter + "%' or " +
                    " DiaEntrega LIKE '%" + parameter + "%' or " +
                    " Total LIKE '%" + parameter + "%' );";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(query, conn);
                conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de nomina de la base de datos");
            }
        }
        public SqlDataAdapter obtenerNominaPacienteTable(string matricula)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT ID,DiaEntrega,FechaInicio,FechaFin,Total FROM nominas WHERE Psicoterapeuta='" + matricula + "'", conn);
                conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de nomina de la base de datos");
            }
        }


        //------------------------------PACIENTES------------------------------------//
        public bool agregarPaciente(Paciente paciente)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string pacienteQuery = "INSERT INTO pacientes (Nombre, Apellidos, FechaNacimiento, EscuelaEmpresa, CostoPaciente, Telefono, TutorNombre, TutorTelefono, Psicoterapeuta) VALUES ('"
                    + paciente.nombre + "','" + paciente.apellidos + "','" + formatearFecha(paciente.fechaNacimiento) + "','" + paciente.institucion + "','" + paciente.costoEspecial + "','" + paciente.telefono + "', '" + paciente.nombre_tutor + "','" + paciente.telefono_tutor + "'," + paciente.psicoterapeuta + "); ";
                if (paciente.psicoterapeuta == null)
                    pacienteQuery = "INSERT INTO pacientes (Nombre, Apellidos, FechaNacimiento, EscuelaEmpresa, CostoPaciente, Telefono, TutorNombre, TutorTelefono, Psicoterapeuta) VALUES ('"
                        + paciente.nombre + "','" + paciente.apellidos + "','" + formatearFecha(paciente.fechaNacimiento) + "','" + paciente.institucion + "','" + paciente.costoEspecial + "','" + paciente.telefono + "', '" + paciente.nombre_tutor + "','" + paciente.telefono_tutor + "',NULL); ";
                string facturacionQuery = "";
                if (paciente.datos_facturacion != null)
                {
                    facturacionQuery = " INSERT INTO datosFacturacion (PacienteID, RFC, Nombre, RazonSocial, Direccion)"
                    + " SELECT AUTO_INCREMENT-1, '" + paciente.datos_facturacion[0] + "','" + paciente.datos_facturacion[1] + "','" + paciente.datos_facturacion[2] + "','" + paciente.datos_facturacion[3] + "' FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '" + this.database + "' AND TABLE_NAME = 'pacientes'; "; ;
                }


                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + pacienteQuery
                                    + facturacionQuery
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar datos de praciente de la Base de datos");
            }
        }
        public bool actualizarPaciente(Paciente paciente)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string facturacionQuery = "";
                string pacienteQuery = "UPDATE pacientes SET Nombre='" + paciente.nombre + "' ,Apellidos='" + paciente.apellidos + "',FechaNacimiento=" + formatearFecha(paciente.fechaNacimiento) + "',EscuelaEmpresa='" + paciente.institucion
                    + "',CostoPaciente='" + paciente.costoEspecial + "',Telefono='" + paciente.telefono + "',Estado=1, TutorNombre='" + paciente.nombre_tutor +
                    "',TutorTelefono='" + paciente.telefono_tutor + "',Psicoterapeuta='" + paciente.psicoterapeuta + "' WHERE ID=" + paciente.id + ";";
                if (paciente.psicoterapeuta == null)
                    pacienteQuery = "UPDATE pacientes SET Nombre='" + paciente.nombre + "' ,Apellidos='" + paciente.apellidos + "',EscuelaEmpresa='" + paciente.institucion
                    + "',CostoPaciente='" + paciente.costoEspecial + "',Telefono='" + paciente.telefono + "',Estado=1, TutorNombre='" + paciente.nombre_tutor +
                    "',TutorTelefono='" + paciente.telefono_tutor + "',Psicoterapeuta=NULL WHERE ID=" + paciente.id + ";";
                if (paciente.datos_facturacion != null)
                {


                    facturacionQuery = " INSERT INTO datosFacturacion (PacienteID, RFC, Nombre, RazonSocial, Direccion) SELECT " + paciente.id + ",'"
                        + paciente.datos_facturacion[0] + "', '" + paciente.datos_facturacion[1] + "', '" + paciente.datos_facturacion[2] + "', '" + paciente.datos_facturacion[3] +
                        "'  WHERE NOT EXISTS (SELECT * FROM datosFacturacion WHERE PacienteID =  " + paciente.id + ");";
                }
                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + pacienteQuery
                                    + facturacionQuery
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al actualizar el paciente de la Base de datos");
            }
        }
        public bool eliminarPaciente(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "UPDATE pacientes SET Estado=0 WHERE ID =" + id + ";";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al desactivar el usuario de la Base de datos");
            }
        }
        public SqlDataAdapter obtenerPacientesTable()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT P.ID, concat_ws(' ',P.Nombre, P.Apellidos) AS 'Nombre', P.EscuelaEmpresa AS 'Institucion',P.Telefono, T.Nombre AS 'Psicoterapeuta' FROM pacientes P LEFT JOIN psicoterapeutas T on P.Psicoterapeuta=T.ID  WHERE P.Estado = 1", conn);
                conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de pacientes de la base de datos");
            }
        }
        public SqlDataAdapter obtenerPacientesTable(string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT P.ID, concat_ws(' ',P.Nombre, P.Apellidos) AS 'Nombre', P.EscuelaEmpresa AS 'Institucion',P.Telefono, T.Nombre AS 'Psicoterapeuta' FROM pacientes P LEFT JOIN psicoterapeutas T on P.Psicoterapeuta=T.ID  WHERE " +
                    "(P.Nombre LIKE '%" + parameter + "%' or " +
                    " P.Apellidos LIKE '%" + parameter + "%' or " +
                    " P.EscuelaEmpresa LIKE '%" + parameter + "%' or " +
                    " P.Telefono LIKE '%" + parameter + "%' or " +
                    " T.Nombre LIKE '%" + parameter + "%') AND " +
                    " (P.Estado = 1)";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.conn);
                this.conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de los empleados de la base de datos");
            }
        }
        public Paciente consultarPaciente(string ID)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT P.ID, P.Nombre, P.Apellidos, P.FechaNacimiento, P.EscuelaEmpresa, P.CostoPaciente, P.Telefono, P.Estado, P.TutorNombre, P.TutorTelefono, P.Psicoterapeuta, F.RFC,F.Nombre,F.RazonSocial,F.Direccion FROM pacientes P LEFT JOIN datosFacturacion F ON P.ID = F.PacienteID WHERE P.ID=" + ID + ";";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Paciente p = new Paciente();
                    p.id = reader.GetInt32(0);
                    p.nombre = reader.GetString(1);
                    p.apellidos = reader.GetString(2);
                    p.fechaNacimiento = reader.GetDateTime(3);
                    p.institucion = reader.GetString(4);
                    p.costoEspecial = reader.GetDecimal(5);
                    p.telefono = reader.GetString(6);
                    p.estado = reader.GetString(7);
                    p.nombre_tutor = reader.GetString(8);
                    p.telefono_tutor = reader.GetString(9);
                    try
                    {
                        reader.GetString(11);
                        p.datos_facturacion = new string[4];
                        p.datos_facturacion[0] = reader.GetString(11);
                        p.datos_facturacion[1] = reader.GetString(12);
                        p.datos_facturacion[2] = reader.GetString(13);
                        p.datos_facturacion[3] = reader.GetString(14);
                    }
                    catch (Exception e)
                    { }
                    try
                    {
                        p.psicoterapeuta = reader.GetString(10);
                    }
                    catch (Exception e)
                    { }
                    conn.Close();
                    return p;
                }
                conn.Close();
                return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos del paciente de la base de datos");
            }
        }
        public List<Paciente> obtenerPacientes()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT P.ID, P.Nombre, P.Apellidos,P.FechaNacimiento, P.EscuelaEmpresa, P.CostoPaciente, P.Telefono, P.Estado, P.TutorNombre, P.TutorTelefono, P.Psicoterapeuta, F.RFC,F.Nombre,F.RazonSocial,F.Direccion FROM pacientes P LEFT JOIN datosFacturacion F ON P.ID = F.PacienteID WHERE P.Estado = 1";
                SqlDataReader reader = cmd.ExecuteReader();
                List<Paciente> aux = new List<Paciente>();
                while (reader.Read())
                {
                    Paciente p = new Paciente();
                    p.id = reader.GetInt32(0);
                    p.nombre = reader.GetString(1);
                    p.apellidos = reader.GetString(2);
                    p.fechaNacimiento = reader.GetDateTime(3);
                    p.institucion = reader.GetString(4);
                    p.costoEspecial = reader.GetDecimal(5);
                    p.telefono = reader.GetString(6);
                    p.estado = reader.GetString(7);
                    p.nombre_tutor = reader.GetString(8);
                    p.telefono_tutor = reader.GetString(9);
                    try
                    {
                        reader.GetString(11);
                        p.datos_facturacion = new string[4];
                        p.datos_facturacion[0] = reader.GetString(11);
                        p.datos_facturacion[1] = reader.GetString(12);
                        p.datos_facturacion[2] = reader.GetString(13);
                        p.datos_facturacion[3] = reader.GetString(14);
                    }
                    catch (Exception e)
                    { }
                    try
                    {
                        p.psicoterapeuta = reader.GetString(10);
                    }
                    catch (Exception e)
                    { }
                    aux.Add(p);
                }
                conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de los pacientes de la base de datos");
            }
        }

        //---------------------------SESIONES------------------------//
        public bool agregarSesion(Sesion sesion)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string reservacionQuery = "";
                string sesionQuery = "";
                if (sesion.reservacion != null)
                {
                    reservacionQuery = "UPDATE reservaciones SET ID_Parent = (SELECT AUTO_INCREMENT FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '" + this.database + "' AND TABLE_NAME = 'sesiones') WHERE ID=" + sesion.reservacion.id + ";";
                    sesionQuery = " INSERT INTO sesiones ( Reservacion_ID, Costo, Pago, Pendiente ,Fecha, Hora, Tipo, Observaciones, Paciente_ID, Psicoterapeuta_ID, Estado)"
                        + " VALUES ('" + sesion.reservacion.id + "','" + sesion.Costo + "','" + sesion.Pago + "', Costo-Pago,'" + formatearFecha(sesion.fecha) + "','" + sesion.hora + "','" + sesion.tipo + "','" + sesion.observaciones + "','" + sesion.paciente + "','" + sesion.psicoterapeuta + "','Activa');";
                }
                else
                {
                    sesionQuery = " INSERT INTO sesiones ( Costo, Pago, Pendiente, Fecha, Hora, Tipo, Observaciones, Paciente_ID, Psicoterapeuta_ID, Estado)"
                        + " VALUES ('" + sesion.Costo + "','" + sesion.Pago + "', Costo-Pago,'" + formatearFecha(sesion.fecha) + "','" + sesion.hora + "','" + sesion.tipo + "','" + sesion.observaciones + "','" + sesion.paciente + "','" + sesion.psicoterapeuta + "','Activa');";
                }


                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + reservacionQuery
                                    + sesionQuery
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar datos de sesión de la Base de datos");
            }
        }
        public bool actualizarSesion(Sesion sesion)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string reservacionQuery = "";
                if (sesion.reservacion != null)
                    reservacionQuery = "UPDATE reservaciones SET Reservante='" + sesion.reservacion.reservante + "', Fecha='" + formatearFecha(sesion.reservacion.fecha) +
                         "Codigo_Reservacion='" + sesion.reservacion.codigo_Reservacion + "', Hora_Inicio='" + sesion.reservacion.hora_Inicio + "', Duracion='" + sesion.reservacion.duracion +
                        "', Hora_Fin='" + sesion.reservacion.hora_Fin + "', Concepto='" + sesion.reservacion.concepto + "', ID_Parent='" + sesion.id +
                        "', Ubicacion='" + sesion.reservacion.ubicacion + "', Observaciones='" + sesion.reservacion.observaciones +
                        "' WHERE ID=" + sesion.reservacion.id + "; ";
                string sesionQuery = " UPDATE sesiones SET Costo='" + sesion.Costo + "', Tipo='" + sesion.tipo + "',Fecha='" + formatearFecha(sesion.fecha) + "',Hora='" + sesion.hora + "', Observaciones='" + sesion.observaciones +
                    "',Paciente='" + sesion.paciente + "',Psicoterapeuta='" + sesion.psicoterapeuta + "',Estado='" + sesion.estado + "' WHERE ID=" + sesion.id + "; ";


                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + reservacionQuery
                                    + sesionQuery
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n Error al actualizar datos de sesion de la Base de datos");
            }
        }
        public bool cancelarSesion(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sesionQuery = " UPDATE sesiones SET Estado='Cancelada' WHERE ID=" + id + "; ";


                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + sesionQuery
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n Error al actualizar datos de sesion de la Base de datos");
            }
        }
        public SqlDataAdapter obtenerSesionesTable()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT S.ID,  S.Tipo, E.Fecha, concat_ws(' ',P.Nombre, P.Apellidos) AS 'Paciente', S.Costo, S.Observaciones, Ps.Nombre AS 'Psicoterapeuta' FROM sesiones S INNER JOIN pacientes P ON S.Paciente_ID=P.ID INNER JOIN psicoterapeutas Ps on S.Psicoterapeuta_ID=Ps.ID WHERE S.Estado = 'Activa'", conn);
                conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de sesiones de la base de datos");
            }
        }
        public SqlDataAdapter obtenerSesionesTable(string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT S.ID,  S.Tipo, E.Fecha, concat_ws(' ',P.Nombre, P.Apellidos) AS 'Paciente', S.Costo, S.Observaciones, Ps.Nombre AS 'Psicoterapeuta' FROM sesiones S INNER JOIN pacientes P ON S.Paciente_ID=P.ID INNER JOIN psicoterapeutas Ps on S.Psicoterapeuta_ID=Ps.ID WHERE " +
                    "(S.Tipo LIKE '%" + parameter + "%' or " +
                    " E.Fecha LIKE '%" + parameter + "%' or " +
                    " concat_ws(' ',P.Nombre, P.Apellidos) LIKE '%" + parameter + "%' or " +
                    " Ps.Nombre LIKE '%" + parameter + "%') AND " +
                    " (S.Estado = 'Activa')";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.conn);
                this.conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de los sesiones de la base de datos");
            }
        }
        public Sesion consultarSesion(string ID)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT S.ID, S.Costo,S.Tipo, S.Observaciones, S.Paciente_ID, S.Psicoterapeuta_ID, S.Estado, R.ID, R.Reservante, R.Fecha,R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones, S.Fecha, S.Hora, S.Pago, S.Pendiente FROM sesiones S LEFT JOIN reservaciones R ON R.ID=S.Reservacion_ID WHERE S.ID=" + ID + ";";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Sesion s = new Sesion();
                    s.id = reader.GetInt32(0);
                    s.Costo = reader.GetDecimal(1);
                    s.tipo = reader.GetString(2);
                    s.observaciones = reader.GetString(3);
                    s.paciente = reader.GetInt32(4);
                    s.estado = reader.GetString(6);
                    s.fecha = reader.GetDateTime(18);
                    s.hora = reader.GetTimeSpan(19);
                    s.Pago = reader.GetDecimal(20);
                    s.Pendiente = reader.GetDecimal(21);
                    try
                    {
                        reader.GetInt32(7);
                        Reservacion r = new Reservacion();
                        r.id = reader.GetInt32(7);
                        r.reservante = reader.GetString(8);
                        r.fecha = reader.GetDateTime(9);
                        r.codigo_Reservacion = reader.GetString(10);
                        r.hora_Inicio = reader.GetTimeSpan(11);
                        r.duracion = reader.GetTimeSpan(12);
                        r.hora_Fin = reader.GetTimeSpan(13);
                        r.concepto = reader.GetString(14);
                        r.id_parent = reader.GetString(15);
                        r.ubicacion = reader.GetString(16);
                        r.observaciones = reader.GetString(17);
                        s.reservacion = r;
                    }
                    catch (Exception e)
                    { }
                    try
                    {
                        s.psicoterapeuta = reader.GetString(5);
                    }
                    catch (Exception e)
                    { }
                    conn.Close();
                    return s;
                }
                conn.Close();
                return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de la sesion de la base de datos");
            }
        }
        public SqlDataAdapter obtenerSesionesPacienteTable(string paciente)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT S.ID, S.Tipo, S.Psicoterapeuta_ID,S.Fecha, S.Hora, S.Observaciones FROM sesiones S WHERE S.Paciente_ID=" + paciente + " AND S.Estado='Activa'  ORDER BY S.Fecha ASC;";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, conn);
                this.conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de las sesiones de la base de datos");
            }
        }
        public List<Sesion> obtenerSesionesPaciente(string paciente)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT S.ID, S.Costo,S.Tipo, S.Observaciones, S.Paciente_ID, S.Psicoterapeuta_ID, S.Estado, R.ID, R.Reservante, R.Fecha, R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones, S.Fecha, S.Hora, S.Pago, S.Pendiente FROM sesiones S LEFT JOIN reservaciones R ON R.ID=S.Reservacion_ID WHERE S.Paciente_ID=" + paciente + ";";
                SqlDataReader reader = cmd.ExecuteReader();
                List<Sesion> aux = new List<Sesion>();
                while (reader.Read())
                {
                    Sesion s = new Sesion();
                    s.id = reader.GetInt32(0);
                    s.Costo = reader.GetDecimal(1);
                    s.tipo = reader.GetString(2);
                    s.observaciones = reader.GetString(3);
                    s.paciente = reader.GetInt32(4);
                    s.estado = reader.GetString(6);
                    s.fecha = reader.GetDateTime(18);
                    s.hora = reader.GetTimeSpan(19);
                    s.Pago = reader.GetDecimal(20);
                    s.Pendiente = reader.GetDecimal(21);
                    try
                    {
                        reader.GetInt32(7);
                        Reservacion r = new Reservacion();
                        r.id = reader.GetInt32(7);
                        r.reservante = reader.GetString(8);
                        r.fecha = reader.GetDateTime(9);
                        r.codigo_Reservacion = reader.GetString(10);
                        r.hora_Inicio = reader.GetTimeSpan(11);
                        r.duracion = reader.GetTimeSpan(12);
                        r.hora_Fin = reader.GetTimeSpan(13);
                        r.concepto = reader.GetString(14);
                        r.id_parent = reader.GetString(15);
                        r.ubicacion = reader.GetString(16);
                        r.observaciones = reader.GetString(17);
                        s.reservacion = r;
                    }
                    catch (Exception e)
                    { }
                    try
                    {
                        s.psicoterapeuta = reader.GetString(5);
                    }
                    catch (Exception e)
                    { }
                    aux.Add(s);
                }
                conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de las sesiones de la base de datos");
            }
        }
        public List<Sesion> obtenerSesionesPendietesDePagoPaciente(string paciente)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT S.ID, S.Costo,S.Tipo, S.Observaciones, S.Paciente_ID, S.Psicoterapeuta_ID, S.Estado, R.ID, R.Reservante, R.Fecha, R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones, S.Fecha, S.Hora, S.Pago, S.Pendiente FROM sesiones S LEFT JOIN reservaciones R ON R.ID=S.Reservacion_ID WHERE S.Paciente_ID=" + paciente + " AND S.Pendiente > 0.0 ORDER BY S.Fecha DESC;";
                SqlDataReader reader = cmd.ExecuteReader();
                List<Sesion> aux = new List<Sesion>();
                while (reader.Read())
                {
                    Sesion s = new Sesion();
                    s.id = reader.GetInt32(0);
                    s.Costo = reader.GetDecimal(1);
                    s.tipo = reader.GetString(2);
                    s.observaciones = reader.GetString(3);
                    s.paciente = reader.GetInt32(4);
                    s.estado = reader.GetString(6);
                    s.fecha = reader.GetDateTime(18);
                    s.hora = reader.GetTimeSpan(19);
                    s.Pago = reader.GetDecimal(20);
                    s.Pendiente = reader.GetDecimal(21);
                    try
                    {
                        reader.GetInt32(7);
                        Reservacion r = new Reservacion();
                        r.id = reader.GetInt32(7);
                        r.reservante = reader.GetString(8);
                        r.fecha = reader.GetDateTime(9);
                        r.codigo_Reservacion = reader.GetString(10);
                        r.hora_Inicio = reader.GetTimeSpan(11);
                        r.duracion = reader.GetTimeSpan(12);
                        r.hora_Fin = reader.GetTimeSpan(13);
                        r.concepto = reader.GetString(14);
                        r.id_parent = reader.GetString(15);
                        r.ubicacion = reader.GetString(16);
                        r.observaciones = reader.GetString(17);
                        s.reservacion = r;
                    }
                    catch (Exception e)
                    { }
                    try
                    {
                        s.psicoterapeuta = reader.GetString(5);
                    }
                    catch (Exception e)
                    { }
                    aux.Add(s);
                }
                conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de las sesiones de la base de datos");
            }
        }
        public bool registrarPagoDeSesion(Pago pago, List<Sesion> sesionesPagadas)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sesionesPagadasQuery = "";
                foreach (Sesion aux in sesionesPagadas)
                {
                    sesionesPagadasQuery += "UPDATE sesiones SET Pago = " + aux.Pago + ", Pendiente=" + aux.Pendiente + " WHERE ID = " + aux.id + "; ";
                }
                string agregarPago = "INSERT INTO pagos (Emisor, FechaPago, Cantidad, Concepto, Area, Observaciones, Recibio, Parent_ID) VALUES ('"
                    + pago.emisor + "', '" + formatearFecha(pago.fechaPago) + "'," + pago.cantidad + ", 'Pago de Sesión', 'Psicoterapia', '"
                    + pago.observaciones + "', '" + pago.recibio + "', '" + sesionesPagadas.ElementAt(0).paciente + "');";


                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + sesionesPagadasQuery
                                    + agregarPago
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar Pago de sesiones a la Base de datos");
            }
        }
        public SqlDataAdapter obtenerPagosPacienteTable(string paciente)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT ID, Emisor, FechaPago AS 'Fecha De Pago', Cantidad, Observaciones, Recibio FROM pagos WHERE Concepto='Pago de sesion' AND Area='Psicoterapia' AND Parent_ID='" + paciente + "';", conn);
                conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de los pagos de sesiones de la base de datos");
            }
        }
        public SqlDataAdapter obtenerPagosPacienteTable(string paciente, string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT ID, Emisor, FechaPago AS 'Fecha De Pago', Cantidad, Observaciones, Recibio FROM pagos WHERE Concepto='Pago de sesion' AND Area='Psicoterapia' AND Parent_ID='" + paciente + "' AND " +
                    "(Emisor LIKE '%" + parameter + "%' or " +
                    "Cantidad LIKE '%" + parameter + "%' or " +
                    "FechaPago LIKE '%" + parameter + "%' or " +
                    "Recibio LIKE '%" + parameter + "%')";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, conn);
                conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de los pagos de sesiones de la base de datos");
            }
        }
        public SqlDataAdapter obtenerPagosSesionesPacienteTable(string paciente)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT S.ID, S.Fecha, S.Hora, S.Costo, S.Pago, S.Pendiente FROM sesiones S WHERE S.Paciente_ID=" + paciente + " AND S.Estado='Activa'  ORDER BY S.Fecha ASC;", conn);
                conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de sesiones de la base de datos");
            }
        }
        public SqlDataAdapter obtenerPagosSesionesPacienteTable(string paciente, string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT S.ID, S.Fecha, S.Hora, S.Costo, S.Pago, S.Pendiente FROM sesiones S WHERE S.Paciente_ID=" + paciente + " AND S.Estado='Activa' AND " +
                    "(S.ID LIKE '%" + parameter + "%' or " +
                    "S.Fecha LIKE '%" + parameter + "%' or " +
                    "S.Psicoterapeuta_ID LIKE '%" + parameter + "%')  ORDER BY S.Fecha ASC;";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, conn);
                conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de sesiones de la base de datos");
            }
        }
        //---------------------------EVALUACIONES------------------------//
        public bool agregarEvaluacion(Evaluacion evaluacion)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string reservacionQuery = "";
                string evaluacionQuery = "";
                if (evaluacion.reservacion != null)
                {
                    reservacionQuery = "UPDATE reservaciones SET ID_Parent = (SELECT AUTO_INCREMENT FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '" + this.database + "' AND TABLE_NAME = 'sesiones') WHERE ID=" + evaluacion.reservacion.id + ";";
                    evaluacionQuery = " INSERT INTO evaluaciones (Reservacion_ID, Costo, pruebas, Observaciones, Paciente_ID, Psicoterapeuta_ID, Estado) VALUES ('"
                        + evaluacion.reservacion.id + "','" + evaluacion.costo + "','" + evaluacion.pruebas + "','" + evaluacion.observaciones +
                        "','" + evaluacion.paciente + "','" + evaluacion.psicoterapeuta + "','Activa');";
                }
                else
                {
                    evaluacionQuery = " INSERT INTO evaluaciones ( Paciente_ID, Psicoterapeuta_ID, Fecha, Hora, Observaciones, Pruebas, Costo, Estado)"
                        + " VALUES ('" + evaluacion.paciente + "','" + evaluacion.psicoterapeuta + "','" + formatearFecha(evaluacion.fecha) + "','" + evaluacion.hora + "','" + evaluacion.observaciones + "','" + evaluacion.pruebas + "','" + evaluacion.costo + "','Activa');";
                }


                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + reservacionQuery
                                    + evaluacionQuery
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar datos de sesión de la Base de datos");
            }
        }
        public bool actualizarEvaluacion(Evaluacion evaluacion)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string reservacionQuery = "";
                if (evaluacion.reservacion != null)
                    reservacionQuery = "UPDATE reservaciones SET Reservante='" + evaluacion.reservacion.reservante + "', Fecha='" + formatearFecha(evaluacion.reservacion.fecha) +
                        "Codigo_Reservacion='" + evaluacion.reservacion.codigo_Reservacion + "', Hora_Inicio='" + evaluacion.reservacion.hora_Inicio + "', Duracion='" + evaluacion.reservacion.duracion +
                       "', Hora_Fin='" + evaluacion.reservacion.hora_Fin + "', Concepto='" + evaluacion.reservacion.concepto + "', ID_Parent='" + evaluacion.id +
                       "', Ubicacion='" + evaluacion.reservacion.ubicacion + "', Observaciones='" + evaluacion.reservacion.observaciones +
                       "' WHERE ID=" + evaluacion.reservacion.id + "; ";
                string evaluacionQuery = " UPDATE evaluaciones SET Costo='" + evaluacion.costo + "', Pruebas='" + evaluacion.pruebas + "', Observaciones='" + evaluacion.observaciones +
                   "', Paciente_ID'" + evaluacion.paciente + "', Psicoterapeuta_ID'" + evaluacion.psicoterapeuta + "',Fecha='" + formatearFecha(evaluacion.fecha) + "',Hora='" + evaluacion.hora + "', Estado='Activa' WHERE ID=" + evaluacion.id + "; ";


                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + reservacionQuery
                                    + evaluacionQuery
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n Error al actualizar datos de sesion de la Base de datos");
            }
        }
        public bool cancelarEvaluacion(Evaluacion evaluacion)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string reservacionQuery = "DELETE FROM reservaciones WHERE ID=" + evaluacion.reservacion.id + "; ";
                string evaluacionQuery = " UPDATE evaluacion SET Estado='Cancelada' WHERE ID=" + evaluacion.id + "; ";


                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + reservacionQuery
                                    + evaluacionQuery
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n Error al actualizar datos de sesion de la Base de datos");
            }
        }
        public SqlDataAdapter obtenerEvaluacionTable()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT E.ID, E.Pruebas, E.Fecha, concat_ws(' ',P.Nombre, P.Apellidos) AS 'Paciente', E.Costo, E.Observaciones, Ps.Nombre AS 'Psicoterapeuta' FROM evaluaciones E INNER JOIN pacientes P ON E.Paciente_ID=P.ID INNER JOIN psicoterapeutas Ps on E.Psicoterapeuta_ID=Ps.ID WHERE E.Estado = 'Activa';", conn);
                conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de evaluaciones de la base de datos");
            }
        }
        public SqlDataAdapter obtenerEvaluacionTable(string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT E.ID, E.Pruebas, E.Fecha, concat_ws(' ',P.Nombre, P.Apellidos) AS 'Paciente', E.Costo, E.Observaciones, Ps.Nombre AS 'Psicoterapeuta' FROM evaluaciones E INNER JOIN pacientes P ON E.Paciente_ID=P.ID INNER JOIN psicoterapeutas Ps on E.Psicoterapeuta_ID=Ps.ID WHERE " +
                    "(E.Pruebas LIKE '%" + parameter + "%' or " +
                    " E.Fecha LIKE '%" + parameter + "%' or " +
                    " concat_ws(' ',P.Nombre, P.Apellidos) LIKE '%" + parameter + "%' or " +
                    " Ps.Nombre LIKE '%" + parameter + "%') AND " +
                    " (E.Estado = 'Activa')";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.conn);
                this.conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de los evaluaciones de la base de datos");
            }
        }
        public Evaluacion consultarEvaluacion(string ID)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT E.ID, E.Costo, E.Pruebas, E.Observaciones, E.Paciente_ID, E.Psicoterapeuta_ID, E.Estado, R.ID, R.Reservante, R.Fecha, R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones, E.Fecha, E.Hora FROM evaluaciones E LEFT JOIN reservaciones R ON R.ID=E.Reservacion_ID WHERE E.ID=" + ID + ";";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Evaluacion s = new Evaluacion();
                    s.id = reader.GetInt32(0);
                    s.costo = reader.GetDecimal(1);
                    s.pruebas = reader.GetString(2);
                    s.observaciones = reader.GetString(3);
                    s.paciente = reader.GetInt32(4);
                    s.estado = reader.GetString(6);
                    s.fecha = reader.GetDateTime(18);
                    s.hora = reader.GetTimeSpan(19);
                    try
                    {
                        reader.GetInt32(7);
                        Reservacion r = new Reservacion();
                        r.id = reader.GetInt32(7);
                        r.reservante = reader.GetString(8);
                        r.fecha = reader.GetDateTime(9);
                        r.codigo_Reservacion = reader.GetString(10);
                        r.hora_Inicio = reader.GetTimeSpan(11);
                        r.duracion = reader.GetTimeSpan(12);
                        r.hora_Fin = reader.GetTimeSpan(13);
                        r.concepto = reader.GetString(14);
                        r.id_parent = reader.GetString(15);
                        r.ubicacion = reader.GetString(16);
                        r.observaciones = reader.GetString(17);
                        s.reservacion = r;
                    }
                    catch (Exception e)
                    { }
                    try
                    {
                        s.psicoterapeuta = reader.GetString(5);
                    }
                    catch (Exception e)
                    { }
                    conn.Close();
                    return s;
                }
                conn.Close();
                return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de la evaluacion de la base de datos");
            }
        }
        public SqlDataAdapter obtenerEvaluacionPacienteTable(string paciente)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT E.ID, E.Pruebas, E.Fecha, E.Observaciones, Ps.Nombre AS 'Psicoterapeuta' FROM evaluaciones E INNER JOIN pacientes P ON E.Paciente_ID=P.ID INNER JOIN psicoterapeutas Ps on E.Psicoterapeuta_ID=Ps.ID WHERE E.Paciente_ID=" + paciente + " AND E.Estado = 'Activa';";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.conn);
                this.conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de los evaluaciones de la base de datos");
            }
        }
        public SqlDataAdapter obtenerEvaluacionPacienteTable(string paciente, string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT E.ID, E.Pruebas, E.Fecha, E.Observaciones, Ps.Nombre AS 'Psicoterapeuta' FROM evaluaciones E INNER JOIN pacientes P ON E.Paciente_ID=P.ID INNER JOIN psicoterapeutas Ps on E.Psicoterapeuta_ID=Ps.Matricula WHERE E.Paciente_ID=" + paciente + " AND E.Estado = 'Activa' AND (" +
                    "E.ID LIKE '%" + parameter + "%' or " +
                    "E.Fecha LIKE '%" + parameter + "%' or " +
                    "E.Pruebas LIKE '%" + parameter + "%' or " +
                    "E.Observaciones LIKE '%" + parameter + "%' or " +
                    "Ps.Nombre LIKE '%" + parameter + "%');";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.conn);
                this.conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de los evaluaciones de la base de datos");
            }
        }
        public List<Evaluacion> obtenerEvaluacionPaciente(string paciente)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT E.ID, E.Costo, E.Pruebas, E.Observaciones, E.Paciente_ID, E.Psicoterapeuta_ID, E.Estado, R.ID, R.Reservante, R.Fecha,R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones, E.Fecha, E.Hora FROM evaluaciones E LEFT JOIN reservaciones R ON R.ID=E.Reservacion_ID WHERE E.Paciente_ID=" + paciente + ";";
                SqlDataReader reader = cmd.ExecuteReader();
                List<Evaluacion> aux = new List<Evaluacion>();
                while (reader.Read())
                {
                    Evaluacion s = new Evaluacion();
                    s.id = reader.GetInt32(0);
                    s.costo = reader.GetDecimal(1);
                    s.pruebas = reader.GetString(2);
                    s.observaciones = reader.GetString(3);
                    s.paciente = reader.GetInt32(4);
                    s.estado = reader.GetString(6);
                    s.fecha = reader.GetDateTime(18);
                    s.hora = reader.GetTimeSpan(19);
                    try
                    {
                        reader.GetInt32(7);
                        Reservacion r = new Reservacion();
                        r.id = reader.GetInt32(7);
                        r.reservante = reader.GetString(8);
                        r.fecha = reader.GetDateTime(9);
                        r.codigo_Reservacion = reader.GetString(10);
                        r.hora_Inicio = reader.GetTimeSpan(11);
                        r.duracion = reader.GetTimeSpan(12);
                        r.hora_Fin = reader.GetTimeSpan(13);
                        r.concepto = reader.GetString(14);
                        r.id_parent = reader.GetString(15);
                        r.ubicacion = reader.GetString(16);
                        r.observaciones = reader.GetString(17);
                        s.reservacion = r;
                    }
                    catch (Exception e)
                    { }
                    try
                    {
                        s.psicoterapeuta = reader.GetString(5);
                    }
                    catch (Exception e)
                    { }
                    aux.Add(s);
                }
                conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de las evaluaciones de la base de datos");
            }
        }

        //-------------------------------CLUB DE TAREAS-------------------------------//
        public bool agregarClubDeTareas(ClubDeTareas club)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string reservacionQuery = "";
                string clubTareasQuery = "";
                if (club.reservacion != null)
                {
                    reservacionQuery = "UPDATE reservaciones SET ID_Parent = (SELECT AUTO_INCREMENT FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '" + this.database + "' AND TABLE_NAME = 'clubDeTareas') WHERE ID=" + club.reservacion.id + ";";
                    clubTareasQuery = "INSERT INTO clubDeTareas (Reservacion_ID, Fecha, Hora, Observaciones, Costo, Psicoterapeuta, Estado) VALUES('"
                     + club.reservacion.id + "','" + formatearFecha(club.Fecha) + "','" + club.Hora + "','" + club.Observaciones + "','" + club.Costo + "','" + club.Encargado + "','Activo');";
                }
                else
                {
                    clubTareasQuery = "INSERT INTO clubDeTareas (Fecha, Hora, Observaciones, Costo, Psicoterapeuta, Estado) VALUES("
                    + " ' " + formatearFecha(club.Fecha) + "','" + club.Hora + "','" + club.Observaciones + "','" + club.Costo + "','" + club.Encargado + "','Activo');";
                }


                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + reservacionQuery
                                    + clubTareasQuery
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar el club a la Base de datos");
            }
        }
        public bool actualizarClubDeTareas(ClubDeTareas club)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string reservacionQuery = "";
                if (club.reservacion != null)
                    reservacionQuery = "UPDATE reservaciones SET Reservante='" + club.reservacion.reservante + "', Fecha='" + formatearFecha(club.reservacion.fecha) +
                        "Codigo_Reservacion='" + club.reservacion.codigo_Reservacion + "', Hora_Inicio='" + club.reservacion.hora_Inicio + "', Duracion='" + club.reservacion.duracion +
                       "', Hora_Fin='" + club.reservacion.hora_Fin + "', Concepto='" + club.reservacion.concepto + "', ID_Parent='" + club.ID +
                       "', Ubicacion='" + club.reservacion.ubicacion + "', Observaciones='" + club.reservacion.observaciones +
                       "' WHERE ID=" + club.reservacion.id + "; ";
                string update = "UPDATE clubDeTareas SET Fecha='" + formatearFecha(club.Fecha) + "', Hora='" + club.Hora +
                    "', Costo='" + club.Costo + "', Psicoterapeuta='" + club.Encargado +
                    "', Observaciones='" + club.Observaciones + "', Estado='Activo' WHERE ID='" + club.ID + "';";



                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + reservacionQuery
                                    + update
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al actualizar el club de la Base de datos");
            }
        }
        public bool cancelarClubDeTareas(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "UPDATE clubDeTareas SET Estado='Cancelado' WHERE ID='" + id + "';";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                throw new Exception("Error...!\n Error al eliminar club De Tareasa la Base de datos");
            }
        }
        public SqlDataAdapter obtenerClubDeTareasTable()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT C.ID, C.Fecha, C.Hora, C.Costo, P.Nombre, C.Observaciones FROM clubDeTareas C INNER JOIN empleados P ON P.ID=C.Psicoterapeuta WHERE C.Estado='Activo'", conn); conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de clubes de la base de datos");
            }
        }
        public SqlDataAdapter obtenerClubDeTareasTable(string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT C.ID, C.Fecha, C.Hora, C.Costo, P.Nombre, C.Observaciones FROM clubDeTareas C INNER JOIN empleados P ON P.ID=C.Psicoterapeuta WHERE " +
                    "(C.ID LIKE '%" + parameter + "%' or " +
                    " P.Nombre LIKE '%" + parameter + "%' or " +
                    " C.Fecha LIKE '%" + parameter + "%') AND C.Estado='Activo'";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.conn);
                this.conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de clubes de tarea de la base de datos");
            }
        }
        public ClubDeTareas consultarClubDeTareas(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT C.ID, C.Fecha, C.Hora, C.Costo, C.Psicoterapeuta, C.Observaciones, C.Estado ,R.ID, R.Reservante, R.Fecha, R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones FROM clubDeTareas C LEFT JOIN reservaciones R ON R.ID=C.Reservacion_ID WHERE C.ID='" + id + "'";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ClubDeTareas c = new ClubDeTareas();
                    c.ID = reader.GetInt32(0);
                    c.Fecha = reader.GetDateTime(1);
                    c.Hora = reader.GetTimeSpan(2);
                    c.Costo = reader.GetDecimal(3);
                    c.Observaciones = reader.GetString(5);
                    c.Estado = reader.GetString(6);
                    try
                    {
                        reader.GetInt32(7);
                        Reservacion r = new Reservacion();
                        r.id = reader.GetInt32(7);
                        r.reservante = reader.GetString(8);
                        r.fecha = reader.GetDateTime(9);
                        r.codigo_Reservacion = reader.GetString(10);
                        r.hora_Inicio = reader.GetTimeSpan(11);
                        r.duracion = reader.GetTimeSpan(12);
                        r.hora_Fin = reader.GetTimeSpan(13);
                        r.concepto = reader.GetString(14);
                        r.id_parent = reader.GetString(15);
                        r.ubicacion = reader.GetString(16);
                        r.observaciones = reader.GetString(17);
                        c.reservacion = r;
                    }
                    catch (Exception e)
                    { }
                    try
                    {
                        c.Encargado = reader.GetString(4);
                    }
                    catch (Exception e)
                    { }
                    conn.Close();
                    return c;
                }
                conn.Close();
                return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos del grupo de la base de datos");
            }
        }
        public ClubDeTareasAsistente obtenerAsistenteClubDeTareas(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT Club_Tareas_ID, Nombre, Apellidos, Nombre_Tutor, Telefono_Tutor, Costo, Pago, Observaciones FROM ClubDeTareasAsistente WHERE ID='" + id + "';";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ClubDeTareasAsistente a = new ClubDeTareasAsistente();
                    a.ID = Convert.ToInt32(id);
                    a.Club_Tareas_ID = reader.GetInt32(0);
                    a.Nombres = reader.GetString(1);
                    a.Apellidos = reader.GetString(2);
                    a.NombreTutor = reader.GetString(3);
                    a.TelefonoTutor = reader.GetString(4);
                    a.Costo = reader.GetDecimal(5);
                    a.Pago = reader.GetDecimal(6);
                    a.Restante = a.Costo - a.Pago;
                    a.Observaciones = reader.GetString(7);
                    conn.Close();
                    return a;
                }
                return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de los asistentes del club de la base de datos");
            }
        }
        public List<ClubDeTareasAsistente> obtenerAsistentesClubDeTareas(string club)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT ID, Club_Tareas_ID, Nombre, Apellidos, Nombre_Tutor, Telefono_Tutor, Costo, Pago, Observaciones FROM ClubDeTareasAsistente WHERE Club_Tareas_ID='" + club + "';";
                SqlDataReader reader = cmd.ExecuteReader();
                List<ClubDeTareasAsistente> aux = new List<ClubDeTareasAsistente>();
                while (reader.Read())
                {
                    ClubDeTareasAsistente a = new ClubDeTareasAsistente();
                    a.ID = reader.GetInt32(0);
                    a.Club_Tareas_ID = reader.GetInt32(1);
                    a.Nombres = reader.GetString(2);
                    a.Apellidos = reader.GetString(3);
                    a.NombreTutor = reader.GetString(4);
                    a.TelefonoTutor = reader.GetString(5);
                    a.Costo = reader.GetDecimal(6);
                    a.Pago = reader.GetDecimal(7);
                    a.Restante = a.Costo - a.Pago;
                    a.Observaciones = reader.GetString(8);
                    aux.Add(a);
                }
                conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de los asistentes del club de la base de datos");
            }
        }
        public SqlDataAdapter obtenerAsistentesClubDeTareasTable(string club)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT ID, Club_Tareas_ID, Nombre, Apellidos, Nombre_Tutor, Telefono_Tutor, Costo, Pago, Observaciones FROM ClubDeTareasAsistente WHERE Club_Tareas_ID='" + club + "'", conn); conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de clubes de la base de datos");
            }
        }
        public SqlDataAdapter obtenerAsistentesClubDeTareasTable(string club, string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT ID, Club_Tareas_ID, Nombre, Apellidos, Nombre_Tutor, Telefono_Tutor, Costo, Pago, Observaciones FROM ClubDeTareasAsistente WHERE Club_Tareas_ID='" + club + "' AND " +
                    "(Nombre LIKE '%" + parameter + "%' or " +
                    " Apellidos LIKE '%" + parameter + "%' or " +
                    " Nombre_Tutor LIKE '%" + parameter + "%' or " +
                    " Telefono_Tutor LIKE '%" + parameter + "%');";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.conn);
                this.conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos del club de tareas de la base de datos");
            }
        }
        public List<ClubDeTareas> obtenerClubDeTareas()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT C.ID, C.Fecha, C.Hora, C.Costo, C.Psicoterapeuta, C.Observaciones, C.Estado ,R.ID, R.Reservante, R.Fecha, R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones FROM clubDeTareas C LEFT JOIN reservaciones R ON R.ID=C.Reservacion_ID WHERE C.Estado='Activo'";
                SqlDataReader reader = cmd.ExecuteReader();
                List<ClubDeTareas> aux = new List<ClubDeTareas>();
                while (reader.Read())
                {
                    ClubDeTareas c = new ClubDeTareas();
                    c.ID = reader.GetInt32(0);
                    c.Fecha = reader.GetDateTime(1);
                    c.Hora = reader.GetTimeSpan(2);
                    c.Costo = reader.GetDecimal(3);
                    c.Observaciones = reader.GetString(5);
                    c.Estado = reader.GetString(6);
                    try
                    {
                        reader.GetInt32(7);
                        Reservacion r = new Reservacion();
                        r.id = reader.GetInt32(7);
                        r.reservante = reader.GetString(8);
                        r.fecha = reader.GetDateTime(9);
                        r.codigo_Reservacion = reader.GetString(10);
                        r.hora_Inicio = reader.GetTimeSpan(11);
                        r.duracion = reader.GetTimeSpan(12);
                        r.hora_Fin = reader.GetTimeSpan(13);
                        r.concepto = reader.GetString(14);
                        r.id_parent = reader.GetString(15);
                        r.ubicacion = reader.GetString(16);
                        r.observaciones = reader.GetString(17);
                        c.reservacion = r;
                    }
                    catch (Exception e)
                    { }
                    try
                    {
                        c.Encargado = reader.GetString(4);
                    }
                    catch (Exception e)
                    { }
                    aux.Add(c);
                }
                conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de los clubes de la base de datos");
            }
        }
        public bool registrarAsistenteClubDeTareas(ClubDeTareasAsistente asistente)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string inscribir = "INSERT INTO clubDeTareasAsistentes (Club_Tareas_ID, Nombres, Apellidos, Nombre_Tutor, Telefono_Tutor, Costo, Observaciones) VALUE ( '"
                    + asistente.Club_Tareas_ID + "','" + asistente.Nombres + "','" + asistente.Apellidos + "','" + asistente.NombreTutor
                    + "','" + asistente.TelefonoTutor + "','" + asistente.Costo + "','" + asistente.Observaciones + "');";
                if (asistente.ID > 0)
                {
                    inscribir = "UPDATE clubDeTareasAsistentes SET Club_Tareas_ID='" + asistente.Club_Tareas_ID + "',Nombres='" + asistente.Nombres +
                    "',Apellidos='" + asistente.Apellidos + "', Nomre_Tutor='" + asistente.NombreTutor + "', Telefono_Tutor='" + asistente.TelefonoTutor +
                    "', Costo='" + asistente.Costo + "',Observaciones='" + asistente.Observaciones + "' WHERE ID = " + asistente.ID + ";";
                }


                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + inscribir
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar asistencia a la Base de datos");
            }
        }
        public bool borrarAsistenteClubDeTareas(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string borrarAsistente = "DELETE FROM clubDeTareasAsistentes WHERE ID=" + id + "; ";


                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + borrarAsistente
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n Error al borrar asistencia a la Base de datos");
            }
        }
        public bool registrarPagoAsistenciaClubDeTareas(Pago pago, string idAsistente)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string updateAsistente = "UPDATE cluDeTareasAsistentes SET Pago = Pago + " + pago.cantidad +
                                            " WHERE ID = " + idAsistente + ";";
                string agregarPago = "INSERT INTO pagos (Emisor, FechaPago, Cantidad, Concepto, Area, Observaciones, Recibio, Parent_ID) VALUES ('"
                    + pago.emisor + "', '" + formatearFecha(pago.fechaPago) + "'," + pago.cantidad + ", 'Pago de Club De Tareas', 'Psicoterapia','"
                    + pago.observaciones + "', '" + pago.recibio + "', '" + idAsistente + "');";


                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + updateAsistente
                                    + agregarPago
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar Pago de club a la Base de datos");
            }
        }

        //-------------------------------PAGOS--------------------------------------//
        public bool agregarPago(Pago pago)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "INSERT INTO pagos (Emisor, FechaPago, Cantidad, Concepto, Area, Observaciones, Recibio, Parent_ID) VALUES ('"
                    + pago.emisor + "', '" + formatearFecha(pago.fechaPago) + "'," + pago.cantidad + ", '" + pago.concepto + "', '" + pago.area + "', '"
                    + pago.observaciones + "', '" + pago.recibio + "', '" + pago.parent_id + "')";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception E)
            {
                conn.Close();
                throw new Exception("Error al agregar el pago a la base de datos");
            }
        }
        public SqlDataAdapter obtenerPagosTable(string area)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT ID, Emisor, FechaPago AS 'Fecha De Pago', Cantidad, Concepto, Observaciones, Recibio  FROM pagos WHERE Area='" + area + "'", conn);
                conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de los pagos de la base de datos");
            }
        }
        public SqlDataAdapter obtenerPagosTable(string parameter, string area)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT ID, Emisor, FechaPago AS 'Fecha De Pago', Cantidad, Concepto, Observaciones, Recibio FROM pagos WHERE" +
                    "(Emisor LIKE '%" + parameter + "%' or " +
                    "Cantidad LIKE '%" + parameter + "%' or " +
                    "Concepto LIKE '%" + parameter + "%' or " +
                    "FechaPago LIKE '%" + parameter + "%' or " +
                    "Recibio LIKE '%" + parameter + "%') AND Area='" + area + "'";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, conn);
                conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener los datos de los pagos de la base de datos");
            }
        }
        public Pago consultarPago(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT ID, Emisor, FechaPago, Cantidad, Concepto, Area, Parent_ID, Observaciones, Recibio, Estado FROM pagos WHERE ID='" + id + "'";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Pago pago = new Pago();
                    pago.id = reader.GetInt32(0);
                    pago.emisor = reader.GetString(1);
                    pago.fechaPago = reader.GetDateTime(2);
                    pago.cantidad = reader.GetInt32(3);
                    pago.concepto = reader.GetString(4);
                    pago.area = reader.GetString(5);
                    pago.parent_id = reader.GetInt32(6);
                    pago.observaciones = reader.GetString(7);
                    pago.recibio = reader.GetString(8);
                    pago.estado = reader.GetString(9);
                    conn.Close();
                    return pago;
                }
                conn.Close();
                return null;
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener los datos del pago de la base de datos");
            }
        }
        public int obtenerUltimoIDPagos()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT AUTO_INCREMENT FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '" + this.database + "' AND TABLE_NAME = 'pagos'; ";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    conn.Close();
                    return id;
                }
                conn.Close();
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener id de pagos de la base de datos");
            }
        }
        public bool cancelarPago(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "UPDATE pagos SET Estado = 'Cancelado' WHERE ID=" + id;
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                throw new Exception("Error al cancelar el pago en la Base de Datos");
            }
        }

        //-------------------------------RESERVACIONES--------------------------------------//
        public bool agregarReservacion(Reservacion reservacion)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string reservacionQuery = "INSERT INTO reservaciones (Reservante, Fecha, Codigo_Reservacion, Hora_Inicio, Duracion, Hora_Fin, Concepto, ID_Parent, Ubicacion, Observaciones) VALUES ('"
                    + reservacion.reservante + "','" + formatearFecha(reservacion.fecha) + "','" + reservacion.codigo_Reservacion + "','" + reservacion.hora_Inicio + "','" + reservacion.duracion + "','" + reservacion.hora_Fin + "','" + reservacion.concepto + "','" + reservacion.id_parent + "','" + reservacion.ubicacion + "','" + reservacion.observaciones + "'); ";


                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + reservacionQuery
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                throw new Exception("ERROR...! \n\n Error al agregar datos de reservación de la Base de datos");
            }
        }
        public bool actualizarReservacion(Reservacion reservacion)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string reservacionQuery = "UPDATE reservaciones SET Reservante='" + reservacion.reservante + "', Fecha='" + formatearFecha(reservacion.fecha) +
                    "Codigo_Reservacion='" + reservacion.codigo_Reservacion + "', Hora_Inicio='" + reservacion.hora_Inicio + "', Duracion='" + reservacion.duracion +
                   "', Hora_Fin='" + reservacion.hora_Fin + "', Concepto='" + reservacion.concepto + "', ID_Parent='" + reservacion.id_parent +
                   "', Ubicacion='" + reservacion.ubicacion + "', Observaciones='" + reservacion.observaciones +
                   "' WHERE ID=" + reservacion.id + "; ";


                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + reservacionQuery
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                throw new Exception("ERROR...! \n\n Error al actualizar datos de reservación de la Base de datos");
            }
        }
        public bool cancelarReservacion(int id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string reservacionQuery = "DELETE FROM reservaciones WHERE ID=" + id + "; ";


                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + reservacionQuery
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n Error al actualizar datos de reservación de la Base de datos");
            }
        }
        public bool cancelarReservacion(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string reservacionQuery = "DELETE FROM reservaciones WHERE Codigo_Reservacion=" + id + "; ";


                cmd.CommandText = "BEGIN TRANSACTION; "
                                    + reservacionQuery
                                    + "COMMIT;";
                int rowsAfected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("ERROR...! \n\n Error al actualizar datos de reservación de la Base de datos");
            }
        }
        public Reservacion consultarReservacion(int ID)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT R.ID, R.Reservante, R.Fecha, R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones FROM reservaciones R WHERE R.ID=" + ID + ";";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    Reservacion r = new Reservacion();
                    r.id = reader.GetInt32(0);
                    r.reservante = reader.GetString(1);
                    r.fecha = reader.GetDateTime(2);
                    r.codigo_Reservacion = reader.GetString(3);
                    r.hora_Inicio = reader.GetTimeSpan(4);
                    r.duracion = reader.GetTimeSpan(5);
                    r.hora_Fin = reader.GetTimeSpan(6);
                    r.concepto = reader.GetString(7);
                    r.id_parent = reader.GetString(8);
                    r.ubicacion = reader.GetString(9);
                    r.observaciones = reader.GetString(10);
                    conn.Close();
                    return r;
                }
                conn.Close();
                return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de la reservación de la base de datos");
            }
        }
        public Reservacion consultarReservacion(TimeSpan hora, DateTime fecha, string psicoterapeuta)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT R.ID, R.Reservante, R.Fecha, R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones FROM reservaciones R WHERE R.Reservante='" + psicoterapeuta + "' AND R.Fecha='" + formatearFecha(fecha) + "' AND R.Hora_Inicio='" + hora + "';";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    Reservacion r = new Reservacion();
                    r.id = reader.GetInt32(0);
                    r.reservante = reader.GetString(1);
                    r.fecha = reader.GetDateTime(2);
                    r.codigo_Reservacion = reader.GetString(3);
                    r.hora_Inicio = reader.GetTimeSpan(4);
                    r.duracion = reader.GetTimeSpan(5);
                    r.hora_Fin = reader.GetTimeSpan(6);
                    r.concepto = reader.GetString(7);
                    r.id_parent = reader.GetString(8);
                    r.ubicacion = reader.GetString(9);
                    r.observaciones = reader.GetString(10);
                    conn.Close();
                    return r;
                }
                conn.Close();
                return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de la reservación de la base de datos");
            }
        }
        public Reservacion consultarReservacion(string ID)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT R.ID, R.Reservante, R.Fecha, R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones FROM reservaciones R WHERE R.Codigo_Reservacion=" + ID + ";";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    Reservacion r = new Reservacion();
                    r.id = reader.GetInt32(0);
                    r.reservante = reader.GetString(1);
                    r.fecha = reader.GetDateTime(2);
                    r.codigo_Reservacion = reader.GetString(3);
                    r.hora_Inicio = reader.GetTimeSpan(4);
                    r.duracion = reader.GetTimeSpan(5);
                    r.hora_Fin = reader.GetTimeSpan(6);
                    r.concepto = reader.GetString(7);
                    r.id_parent = reader.GetString(8);
                    r.ubicacion = reader.GetString(9);
                    r.observaciones = reader.GetString(10);
                    conn.Close();
                    return r;
                }
                conn.Close();
                return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de la reservación de la base de datos");
            }
        }
        public List<Reservacion> obtenerReservaciones(DateTime fecha)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT R.ID, R.Reservante, R.Fecha, R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones FROM reservaciones R WHERE R.Fecha ='" + formatearFecha(fecha).Substring(0, 10) + "';";
                SqlDataReader reader = cmd.ExecuteReader();
                List<Reservacion> aux = new List<Reservacion>();
                while (reader.Read())
                {
                    Reservacion r = new Reservacion();
                    r.id = reader.GetInt32(0);
                    r.reservante = reader.GetString(1);
                    r.fecha = reader.GetDateTime(2);
                    r.codigo_Reservacion = reader.GetString(3);
                    r.hora_Inicio = reader.GetTimeSpan(4);
                    r.duracion = reader.GetTimeSpan(5);
                    r.hora_Fin = reader.GetTimeSpan(6);
                    r.concepto = reader.GetString(7);
                    r.id_parent = reader.GetString(8);
                    r.ubicacion = reader.GetString(9);
                    r.observaciones = reader.GetString(10);
                    aux.Add(r);
                }
                conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de las reservaciones de la base de datos");
            }
        }
        public int obtenerUltimoIDReservaciones()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT AUTO_INCREMENT FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '" + this.database + "' AND TABLE_NAME = 'reservaciones'; ";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    conn.Close();
                    return id;
                }
                conn.Close();
                return 0;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener id de pagos de la base de datos");
            }
        }

        //----------------------------------USUARIO--------------------------------------------//
        public Usuario consultarUsuario(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT Matricula, Usuario, Contrasena, Nivel_Acceso FROM usuarios WHERE Matricula='" + id + "' or Usuario ='" + id + "'";
                //int rowsAfected = cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Usuario pv = new Usuario();
                    pv.Matricula = reader.GetString(0);
                    pv.Nombre_De_Usuario = reader.GetString(1);
                    pv.Contrasena = reader.GetString(2);
                    pv.Nivel_Acceso = reader.GetInt32(3);
                    conn.Close();
                    return pv;
                }
                conn.Close();
                return null;

            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de Usuarios de la Base de Datos");
            }

        }
        public bool actualizarUsuario(Usuario usuario)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "UPDATE usuarios SET Usuario= '" + usuario.Nombre_De_Usuario +
               "',Contrasena='" + usuario.Contrasena +
               "',Nivel_Acceso='" + usuario.Nivel_Acceso +
               "',Estado = 1 WHERE Matricula='" + usuario.Matricula + "'";
                //cmd.CommandText = "SELECT * FROM Servicios";
                int rowsAfected = cmd.ExecuteNonQuery();
                //SqlDataReader reader = cmd.ExecuteReader();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error..! Error al actualizar usuario de la Base de Datos");
            }
        }
        public bool eliminarUsuario(string matricula)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "UPDATE usuarios SET Estado = 0 WHERE Matricula='" + matricula + "'";
                //cmd.CommandText = "SELECT * FROM Servicios";
                int rowsAfected = cmd.ExecuteNonQuery();
                //SqlDataReader reader = cmd.ExecuteReader();
                conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error..! Error al desactivar usuario de la Base de Datos");
            }
        }

        //--------------------------------PARAMETROS GENERALES ----------------------------------//
        private void consultarParametrosGenerales()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT * FROM parametros_Generales;";
                this.parametros_Generales = new ParametrosGenerales();
                SqlDataReader reader = cmd.ExecuteReader();
                List<String> aux = new List<String>();
                reader.Read();
                parametros_Generales.Costo_Credito_Especialidad_Diplomado = reader.GetDecimal(0);
                parametros_Generales.Costo_Credito_Maestria = reader.GetDecimal(1);
                parametros_Generales.Porcentaje_Pago_Sesion = reader.GetDecimal(2);
                parametros_Generales.Porcentaje_Pago_Taller = reader.GetDecimal(3);
                parametros_Generales.Porcentaje_Pago_Clase = reader.GetDecimal(4);
                parametros_Generales.Porcentaje_Pago_Evaluacion = reader.GetDecimal(5);
                parametros_Generales.Director = reader.GetString(6);
                parametros_Generales.Sede = reader.GetString(7);
                reader.Close();
                cmd.CommandText = "SELECT Nombre FROM ubicaciones;";
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    aux.Add(reader.GetString(0));
                }
                parametros_Generales.ubicaciones = aux;
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener parametros generales de la Base de Datos");
            }
        }
        public bool actualizarParametrosGenerales(ParametrosGenerales parametros)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {

                string updateParametros = "DELETE FROM parametros_Generales; INSERT INTO parametros_Generales VALUES ('" +
                    parametros.Costo_Credito_Especialidad_Diplomado + "','" + parametros.Costo_Credito_Maestria + "','" +
                    parametros.Porcentaje_Pago_Sesion + "','" + parametros.Porcentaje_Pago_Taller + "','" +
                    parametros.Porcentaje_Pago_Clase + "','" + parametros.Porcentaje_Pago_Evaluacion + "','" + parametros.Director + "','" + parametros.Sede + "'); ";
                string updateUbicaciones = "";
                if (parametros.ubicaciones != null)
                {
                    foreach (string aux in parametros.ubicaciones)
                    {
                        if (updateUbicaciones != "")
                            updateUbicaciones += ",";
                        updateUbicaciones += "('" + aux + "')";
                    }
                    updateUbicaciones = "DELETE FROM ubicaciones; INSERT INTO ubicaciones (Nombre) VALUES " + updateUbicaciones + "; ";

                }


                cmd.CommandText = "BEGIN TRANSACTION; " +
                    updateParametros +
                    updateUbicaciones +
                    "COMMIT; ";

                //cmd.CommandText = "SELECT * FROM Servicios";
                int rowsAfected = cmd.ExecuteNonQuery();
                //SqlDataReader reader = cmd.ExecuteReader();
                conn.Close();
                if (rowsAfected > 0)
                {
                    this.parametros_Generales = parametros;
                    return true;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error..! Error al actualizar usuario de la Base de Datos");
            }
        }
        public List<String> consultarUbicaciones()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT Nombre FROM ubicaciones";
                SqlDataReader reader = cmd.ExecuteReader();
                List<String> aux = new List<String>();
                while (reader.Read())
                {
                    aux.Add(reader.GetString(0));
                }
                conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de Ubicaciones de la Base de Datos");
            }
        }
        public List<String> obtenerConceptos(string tipo, string area)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                cmd.CommandText = "SELECT Concepto FROM conceptos WHERE Tipo='" + tipo + "' AND Area='" + area + "';";
                SqlDataReader reader = cmd.ExecuteReader();
                List<String> aux = new List<String>();
                while (reader.Read())
                {
                    aux.Add(reader.GetString(0));
                }
                conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al obtener datos de los conceptos de pago de la base de datos");
            }
        }

        //------------------------------------CONFIGURACIÓN--------------------------------------//
        public string formatearFecha(DateTime fecha)
        {
            DateTime aux;
            if (fecha == null)
                aux = DateTime.Now;
            else
                aux = fecha;

            string day;
            if (aux.Day.ToString().Length == 1)
                day = "0" + aux.Day.ToString();
            else
                day = aux.Day.ToString();
            string month;
            if (aux.Month.ToString().Length == 1)
                month = "0" + aux.Month.ToString();
            else
                month = aux.Month.ToString();
            string hour;
            if (aux.Hour.ToString().Length == 1)
                hour = "0" + aux.Hour.ToString();
            else
                hour = aux.Hour.ToString();
            string second;
            if (aux.Second.ToString().Length == 1)
                second = "0" + aux.Second.ToString();
            else
                second = aux.Second.ToString();
            string minute;
            if (aux.Minute.ToString().Length == 1)
                minute = "0" + aux.Minute.ToString();
            else
                minute = aux.Minute.ToString();

            return aux.Year + "-" + month + "-" + day + " " + hour + ":" + minute + ":" + second;
        }
        public string leerUserDoc()
        {
            String line;
            StreamReader sr = null;
            string user = "";
            try
            {
                string archivo = "thumbs.txt";
                // comprobar si el fichero ya existe
                if (File.Exists(archivo))
                {
                    //Pass the file path and file name to the StreamReader constructor
                    sr = new StreamReader(archivo);
                    //read de first line
                    line = sr.ReadLine();
                    //Continue to read until you reach end of file
                    while (line != null)
                    {
                        if (line == "us")
                        {
                            user = sr.ReadLine();
                            break;
                        }
                            line = sr.ReadLine();
                    }
                    //close the file
                    sr.Close();
                    sr.Dispose();
                }
                return user;
            }
            catch (Exception e)
            {
                sr.Dispose();
                return "";
            }
        }
        public string leerPVDoc()
        {
            String line;
            StreamReader sr = null;
            string pv = "";
            try
            {
                string archivo = "thumbs.txt";
                // comprobar si el fichero ya existe
                if (File.Exists(archivo))
                {
                    //Pass the file path and file name to the StreamReader constructor
                    sr = new StreamReader(archivo);
                    //read de first line
                    line = sr.ReadLine();
                    //Continue to read until you reach end of file
                    while (line != null)
                    {
                        if (line == "pv")
                        {
                            pv = sr.ReadLine();
                            break;
                        }
                        line = sr.ReadLine();
                    }
                    //close the file
                    sr.Close();
                    sr.Dispose();
                }
                return pv;
            }
            catch (Exception e)
            {
                sr.Dispose();
                return "";
            }
        }
        public bool recordarUsuario(string usuario)
        {
            StreamWriter sw = null;
            try
            {
                string archivo = "thumbs.txt";
                // comprobar si el fichero ya existe
                if (!File.Exists(archivo))
                {
                    File.Create(archivo).Close();
                }
                //Pass the file path and file name to the StreamReader constructor
                sw = new StreamWriter(archivo);
                //Write a line of text
                if (usuario != null)
                {
                    sw.WriteLine("us");
                    sw.WriteLine(usuario);
                }else
                {
                    sw.WriteLine();
                }
                //Close the file
                sw.Close();
                sw.Dispose();
                return true;
            }
            catch (Exception e)
            {
                sw.Dispose();
                return false;
            }
        }

    }
}
