
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
        readonly SqlConnection Conn;
        readonly SqlConnectionStringBuilder Builder;
        readonly SqlCommand Cmd;

        //Devs
        readonly string Server = @"DESKTOP-HENEPIV\SQLEXPRESS";
        readonly string UserID = "iic2ps1d_devs_db";
        readonly string Password = "ConejoVolador11";
        readonly string Database = "iicaps_db_devs";
        readonly uint Port = 1433;

        //Production
        //readonly string Server = @"WIN-B2Q6B50DPEM";
        //readonly string UserID = "iic2ps1d_db";
        //readonly string Password = "ConejoVolador11";
        //readonly string Database = "iicaps_db_prod";
        //readonly uint Port = 1433;
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
            Builder = new MySqlConnectionStringBuilder();
            Builder.Server = server;
            Builder.UserID = userID;
            Builder.Password = password;
            Builder.Database = database;
            Builder.AllowUserVariables = true;
            Builder.SslMode = MySqlSslMode.None;
            //Builder.ConnectionProtocol = protocolo;
            Builder.Port = port;

            Conn = new MySqlConnection(Builder.ToString());
            Cmd = Conn.CreateCommand();

            try
            {
                ConsultarParametrosGenerales();
            }
            catch (Exception ex) {
                //throw new Exception("Error al obtener parametros generales");
            }
        }**/
        // MSSQL SERVER
        public ControlIicaps()
        {
            Builder = new SqlConnectionStringBuilder
            {
                DataSource = Server + "," + Port,
                UserID = UserID,
                Password = Password,
                InitialCatalog = Database,
                IntegratedSecurity = true
            };

            Conn = new SqlConnection(Builder.ConnectionString);
            Cmd = Conn.CreateCommand();
            

            try
            {
                ConsultarParametrosGenerales();
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

        private void OpenConection()
        {
            try
            {
                Conn.Open();
            }
            catch (Exception e)
            {
                throw new Exception("ERROR...! \n\n No es posible establecer conexión con el servidor");
            }
        }

        //-------------------------------Alumnos-------------------------------//
        public bool AgregarAlumno(Alumno alumno)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "INSERT INTO alumnos (Nombre, Direccion, Telefono1, Telefono2, Correo, Facebook, CURP, RFC, Sexo, EstadoCivil, EscuelaProcedencia, Carrera, Programa, Nivel, Fecha, Estado, Tipo, Observaciones, Matricula) VALUES('"
                    + alumno.Nombre + "','" + alumno.Direccion + "','" + alumno.Telefono1 + "','" + alumno.Telefono2 + "','" + alumno.Correo + "','" + alumno.Facebook + "','" + alumno.Curp + "','"
                    + alumno.Rfc + "','" + alumno.Sexo + "','" + alumno.EstadoCivil + "','" + alumno.EscuelaProcedencia + "','" + alumno.Carrera + "','" + alumno.Programa + "','" + alumno.Nivel + "','" + FormatearFecha(alumno.Fecha) +
                    "','Registrado','" + alumno.Tipo + "','" + alumno.Observaciones + "','" + alumno.Matricula + "')";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n No ha sido posible guardar los datos. Si el error persiste consulte con soporte técnico");
            }
        }
        public SqlDataAdapter ObtenerAlumnosTable()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT A.RFC, A.Nombre , A.Telefono1 AS 'Telefono 1', A.Programa AS 'Programa', G.Generacion FROM alumnos A LEFT JOIN grupoAlumno GA ON A.RFC = GA.Alumno LEFT JOIN grupos G ON G.Codigo = GA.Grupo WHERE A.Estado NOT LIKE 'Baja'", Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n No ha sido posible guardar los datos. Si el error persiste consulte con soporte técnico");
            }
        }
        public SqlDataAdapter ObtenerAlumnosTable(string parameter)
        {

            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT A.RFC, A.Nombre, A.Telefono1 AS 'Telefono 1', A.Programa, G.Generacion FROM alumnos A LEFT JOIN grupoAlumno GA ON A.RFC = GA.Alumno LEFT JOIN grupos G ON G.Codigo = GA.Grupo " +
                    "WHERE " +
                    "(A.Nombre LIKE '%" + parameter + "%' or " +
                    " A.Telefono1 LIKE '%" + parameter + "%' or " +
                    " A.Programa LIKE '%" + parameter + "%' or " +
                    " G.Generacion LIKE '%" + parameter + "%') AND A.Estado NOT LIKE 'Baja'";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n No ha sido posible guardar los datos. Si el error persiste consulte con soporte técnico");
            }
        }
        public Alumno ConsultarAlumno(string rfc)
        {

            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {

                Cmd.CommandText = "SELECT * FROM alumnos WHERE RFC='" + rfc + "'";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    Alumno a = new Alumno
                    {
                        Nombre = reader.GetString(0),
                        Direccion = reader.GetString(1),
                        Telefono1 = reader.GetString(2),
                        Telefono2 = reader.GetString(3),
                        Correo = reader.GetString(4),
                        Facebook = reader.GetString(5),
                        Curp = reader.GetString(6),
                        Rfc = reader.GetString(7),
                        Sexo = reader.GetString(8),
                        EstadoCivil = reader.GetString(9),
                        EscuelaProcedencia = reader.GetString(10),
                        Carrera = reader.GetString(11),
                        Programa = reader.GetString(12),
                        Nivel = reader.GetString(13),
                        Fecha = reader.GetDateTime(14),
                        Estado = reader.GetString(15),
                        Tipo = reader.GetString(16),
                        Observaciones = reader.GetString(17),
                        Matricula = reader.GetString(18)
                    };
                    Conn.Close();
                    return a;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n No ha sido posible guardar los datos. Si el error persiste consulte con soporte técnico");
            }
        }

        public List<Alumno> ObtenerAlumnos()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {

                Cmd.CommandText = "SELECT * FROM alumnos";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<Alumno> aux = new List<Alumno>();
                while (reader.Read())
                {
                    Alumno a = new Alumno
                    {
                        Nombre = reader.GetString(0),
                        Direccion = reader.GetString(1),
                        Telefono1 = reader.GetString(2),
                        Telefono2 = reader.GetString(3),
                        Correo = reader.GetString(4),
                        Facebook = reader.GetString(5),
                        Curp = reader.GetString(6),
                        Rfc = reader.GetString(7),
                        Sexo = reader.GetString(8),
                        EstadoCivil = reader.GetString(9),
                        EscuelaProcedencia = reader.GetString(10),
                        Carrera = reader.GetString(11),
                        Programa = reader.GetString(12),
                        Nivel = reader.GetString(13),
                        Fecha = reader.GetDateTime(14),
                        Estado = reader.GetString(15),
                        Tipo = reader.GetString(16),
                        Observaciones = reader.GetString(17),
                        Matricula = reader.GetString(18)
                    };
                    aux.Add(a);
                }
                Conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n No ha sido posible guardar los datos. Si el error persiste consulte con soporte técnico");
            }
        }
        public List<Alumno> ObtenerAlumnosByPrograma(string programa)
        {

            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {

                Cmd.CommandText = "SELECT * FROM alumnos WHERE Programa = '" + programa + "'";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<Alumno> aux = new List<Alumno>();
                while (reader.Read())
                {
                    Alumno a = new Alumno
                    {
                        Nombre = reader.GetString(0),
                        Direccion = reader.GetString(1),
                        Telefono1 = reader.GetString(2),
                        Telefono2 = reader.GetString(3),
                        Correo = reader.GetString(4),
                        Facebook = reader.GetString(5),
                        Curp = reader.GetString(6),
                        Rfc = reader.GetString(7),
                        Sexo = reader.GetString(8),
                        EstadoCivil = reader.GetString(9),
                        EscuelaProcedencia = reader.GetString(10),
                        Carrera = reader.GetString(11),
                        Programa = reader.GetString(12),
                        Nivel = reader.GetString(13),
                        Fecha = reader.GetDateTime(14),
                        Estado = reader.GetString(15),
                        Tipo = reader.GetString(16),
                        Observaciones = reader.GetString(17),
                        Matricula = reader.GetString(18)
                    };
                    aux.Add(a);
                }
                Conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de los alumnos de la base de datos");
            }

        }
        public bool DarDeBajaAlumno(string rfc)
        {

            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {

                Cmd.CommandText = "UPDATE alumnos SET Estado='Baja' WHERE RFC = '" + rfc + "'";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error..! Error al dar de baja el alumno de la Base de Datos");
            }

        }
        public string ObtenerNombreAlumno(string rfc)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {

                Cmd.CommandText = "SELECT Nombre FROM alumnos WHERE RFC = '" + rfc + "'";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    string nombre = "";
                    nombre = reader.GetString(0);
                    return nombre;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error..! Error al obtener el nombre del alumno de la Base de Datos");
            }
        }
        public bool DarDeAltaAlumno(string rfc)
        {

            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {

                Cmd.CommandText = "UPDATE alumnos SET Estado='Registrado' WHERE RFC = '" + rfc + "'";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error..! Error al dar de alta el alumno de la Base de Datos");
            }
        }
        public bool ActualizarAlumno(Alumno alumno)
        {

            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {

                Cmd.CommandText = "UPDATE alumnos SET Nombre= '" + alumno.Nombre + "', Direccion= '" + alumno.Direccion + "', Telefono1= '" + alumno.Telefono1 + "', Telefono2= '"
                    + alumno.Telefono2 + "', Correo= '" + alumno.Correo + "', Facebook= '" + alumno.Facebook + "', Sexo= '" + alumno.Sexo + "', EstadoCivil= '" + alumno.EstadoCivil +
                    "', Programa= '" + alumno.Programa + "', Fecha= '" + FormatearFecha(alumno.Fecha) + "', Estado= '" + alumno.Estado + "', Tipo= '" + alumno.Tipo + "',Observaciones= '" + alumno.Observaciones + "',Matricula= '" + alumno.Matricula + "' WHERE RFC = '" + alumno.Rfc + "'";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al actualizar los datos del alumno en la Base de Datos");
            }

        }

        //-------------------------------Credito de alumnos-------------------------------//
        public bool AgregarCreditoAlumno(CreditoAlumno credito)
        {

            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string creditoQuery = "INSERT INTO creditoAlumno (AlumnoID, CantidadMensualidad, CantidadMeses, CantidadAbonoCredito, CantidadAbonoMensual, FechaSolicitud, Observaciones, Estado) VALUES ('"
                    + credito.Alumno + "', '" + credito.CantidadMensualidad + "', '" + credito.CantidadMeses + "', '"
                    + credito.CantidadAbonoCredito + "', '" + credito.CantidadAbonoMensual + "','"
                    + FormatearFecha(DateTime.Now) + "', '" + credito.Observaciones + "', '" + credito.Estado + "');";
                string registroCobro = "INSERT INTO cobrosAlumno(Alumno, Concepto, Cantidad, Pago, Restante, Fecha, Parent_ID)SELECT '"
                    + credito.Alumno + "','Credito Escolar', '" + (credito.CantidadMensualidad * credito.CantidadMeses) +
                    "','0.00','" + (credito.CantidadMensualidad * credito.CantidadMeses) + "','" + FormatearFecha(DateTime.Now) +
                    "', AUTO_INCREMENT-1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '" + this.Database +
                    "' AND TABLE_NAME = 'creditoAlumno');";
                Cmd.CommandText = "BEGIN TRANSACTION;" +
                    creditoQuery +
                    registroCobro +
                    "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception E)
            {
                Conn.Close();
                throw new Exception("Error al agregar el credito del alumnos a la base d datos");

            }
        }
        public SqlDataAdapter ObtenerCreditoAlumnosTable()
        {

            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT ID, AlumnoID, CantidadMensualidad AS 'Mensualidad' , CantidadMeses AS 'No. de Meses',  FechaSolicitud AS 'Fecha de Solicitud', Observaciones FROM creditoAlumno WHERE Estado NOT LIKE 'Cancelado'", Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de los creditos de los alumnos de la base de datos");
            }

        }
        public SqlDataAdapter ObtenerCreditoAlumnosTable(string parameter)
        {

            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT ID, AlumnoID, CantidadMensualidad AS 'Mensualidad' , CantidadMeses AS 'No. de Meses',  FechaSolicitud AS 'Fecha de Solicitud', Observaciones FROM creditoAlumno WHERE Estado NOT LIKE 'Cancelado' AND " +
                    "(AlumnoID LIKE '%" + parameter + "%' or " +
                    "CantidadMensualidad LIKE '%" + parameter + "%' or " +
                    "CantidadMeses LIKE '%" + parameter + "%' or " +
                    "Observaciones LIKE '%" + parameter + "%')";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de los creditos de los alumnos de la base de datos");
            }

        }
        public CreditoAlumno ConsultarCreditoActivoAlumno(string rfc)
        {

            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT C.ID, C.AlumnoID, C.CantidadMensualidad, C.CantidadMeses, C.CantidadAbonoCredito, C.CantidadAbonoMensual, C.FechaSolicitud, C.Observaciones, C.Estado, A.Pago FROM creditoAlumno C, cobrosAlumno A WHERE A.Parent_ID=C.ID AND C.AlumnoID='" + rfc + "' AND C.Estado='Activo'";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    CreditoAlumno credito = new CreditoAlumno
                    {
                        Id = reader.GetInt32(0),
                        Alumno = reader.GetString(1),
                        CantidadMensualidad = reader.GetDecimal(2),
                        CantidadMeses = reader.GetInt32(3),
                        CantidadAbonoCredito = reader.GetDecimal(4),
                        CantidadAbonoMensual = reader.GetDecimal(5),
                        FechaSolicitud = reader.GetDateTime(6),
                        Observaciones = reader.GetString(7),
                        Estado = reader.GetString(8),
                        Pago = reader.GetDecimal(9)
                    };
                    Conn.Close();
                    return credito;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos del credito del alumno de la base de datos");
            }

        }
        public CreditoAlumno ConsultarCreditoAlumno(string rfc)
        {

            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT C.ID, C.AlumnoID, C.CantidadMensualidad, C.CantidadMeses, C.CantidadAbonoCredito, C.CantidadAbonoMensual, C.FechaSolicitud, C.Observaciones, C.Estado, A.Pago FROM creditoAlumno C, cobrosAlumno A WHERE A.Parent_ID=C.ID AND C.AlumnoID='" + rfc + "' AND A.Concepto='Credito Escolar'";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    CreditoAlumno credito = new CreditoAlumno
                    {
                        Id = reader.GetInt32(0),
                        Alumno = reader.GetString(1),
                        CantidadMensualidad = reader.GetDecimal(2),
                        CantidadMeses = reader.GetInt32(3),
                        CantidadAbonoCredito = reader.GetDecimal(4),
                        CantidadAbonoMensual = reader.GetDecimal(5),
                        FechaSolicitud = reader.GetDateTime(6),
                        Observaciones = reader.GetString(7),
                        Estado = reader.GetString(8),
                        Pago = reader.GetDecimal(9)
                    };
                    Conn.Close();
                    return credito;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos del credito del alumno de la base de datos");
            }

        }
        public bool ActualizarEstadoCredito(string credito_ID, string estado)
        {

            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string creditoQuery = "UPDATE creditoAlumno SET Estado ='" + estado + "' WHERE ID = '" + credito_ID + "'";
                Cmd.CommandText = "BEGIN TRANSACTION;" +
                    creditoQuery +
                    "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al actualizar los datos del credito del alumno en la Base de Datos");
            }
        }
        public bool ActualizarCredito(CreditoAlumno credito)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string creditoQuery = "UPDATE creditoAlumno SET CantidadMensualidad= '" + credito.CantidadMensualidad + "', CantidadMeses= '" + credito.CantidadMeses +
                    "', CantidadAbonoCredito=" + credito.CantidadAbonoCredito + "', CantidadAbonoMensual='" + credito.CantidadAbonoMensual +
                    "', Observaciones= '" + credito.Observaciones + "' WHERE AlumnoID = '" + credito.Alumno + "'";
                string registroCobro = "UPDATE cobrosAlumno set Alumno='" + credito.Alumno + "',Concepto='Credito Escolar', Cantidad='" + (credito.CantidadMensualidad * credito.CantidadMeses) +
                    "',Restante=" + (credito.CantidadMensualidad * credito.CantidadMeses) + "- pago,Fecha='" + FormatearFecha(DateTime.Now) + "' WHERE Parent_ID = '" + credito.Id + "' AND Concepto='Credito Escolar';";

                Cmd.CommandText = "BEGIN TRANSACTION;" +
                    creditoQuery +
                    registroCobro +
                    "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al actualizar los datos del credito del alumno en la Base de Datos");
            }

        }
        public bool CancelarCredito(string id)
        {


            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string query = "UPDATE creditoAlumno SET Estado = 'Cancelado' WHERE ID=" + id;
                query+= "UPDATE cobrosAlumno SET Estado = 'Cancelado' WHERE Parent_ID="+ id +"AND Concepto='Credito Alumno'";
                Cmd.CommandText = query;
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al cancelar el crédito del alumno en la Base de Datos");
            }
        }

        //-------------------------------PAGOS ALUMNO--------------------------------------//
        public bool AgregarPagoAlumno(PagoAlumno pago, List<Cobro> cobros)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string pagoQuery = "INSERT INTO pagosAlumno (AlumnoID, FechaPago, Cantidad, Concepto, Observaciones, Recibio) VALUES ('"
                        + pago.AlumnoID + "', '" + FormatearFecha(pago.FechaPago) + "'," + pago.Cantidad + ", '" + pago.Concepto + "', '"
                        + pago.Observaciones + "', '" + pago.Recibio + "');";
                string actualizarCobros = "";
                foreach (Cobro aux in cobros)
                {
                    actualizarCobros += "UPDATE cobrosAlumno SET Pago = " + aux.Pago + ", Restante=" + aux.Restante + " WHERE ID = " + aux.Id + "; ";
                }


                Cmd.CommandText = "BEGIN TRANSACTION;" +
                    pagoQuery +
                    actualizarCobros +
                    "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception E)
            {
                Conn.Close();
                throw new Exception("Error al agregar el pago del alumno a la base de datos");
            }
        }
        public bool AgregarPagoAlumno(PagoAlumno pago)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "INSERT INTO pagosAlumno (AlumnoID, FechaPago, Cantidad, Concepto, Observaciones, Recibio) VALUES ('"
                        + pago.AlumnoID + "', '" + FormatearFecha(pago.FechaPago) + "'," + pago.Cantidad + ", '" + pago.Concepto + "', '"
                        + pago.Observaciones + "', '" + pago.Recibio + "')";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception E)
            {
                Conn.Close();
                throw new Exception("Error al agregar el pago del alumno a la base de datos");
            }

        }
        public SqlDataAdapter ObtenerPagosAlumnosTable()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT ID, AlumnoID AS 'ID de Alumno', FechaPago AS 'Fecha de Pago', Cantidad, Concepto, Observaciones, Recibio, Estado FROM pagosAlumno", Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de los pagos de la base de datos");
            }

        }
        public SqlDataAdapter ObtenerPagosAlumnosTable(string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT ID, AlumnoID AS 'ID de Alumno', FechaPago AS 'Fecha de Pago', Cantidad, Concepto, Observaciones, Recibio, Estado FROM pagosAlumno WHERE" +
                    "(AlumnoID LIKE '%" + parameter + "%' or " +
                    "Cantidad LIKE '%" + parameter + "%' or " +
                    "Concepto LIKE '%" + parameter + "%' or " +
                    "Recibio LIKE '%" + parameter + "%')";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de los pagos de la base de datos");
            }

        }
        public PagoAlumno ConsultarPagoAlumno(int id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT * FROM pagosAlumno WHERE ID='" + id + "'";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    PagoAlumno pago = new PagoAlumno
                    {
                        Id = reader.GetInt32(0),
                        AlumnoID = reader.GetString(1),
                        FechaPago = reader.GetDateTime(2),
                        Cantidad = reader.GetInt32(3),
                        Concepto = reader.GetString(4),
                        Observaciones = reader.GetString(5),
                        Recibio = reader.GetString(6)
                    };
                    Conn.Close();
                    return pago;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos del pago de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerPagosDeAlumnoTable(String rfc)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT ID,Cantidad,Concepto,Observaciones,Recibio,FechaPago FROM pagosAlumno WHERE AlumnoID = '" + rfc + "'", Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de los pagos del alumno de la base de datos");
            }


        }
        public List<String> ObtenerConceptosDePagoAlumno(string area)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT Concepto FROM conceptos WHERE Tipo='Pago' AND Area='" + area + "';";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<String> aux = new List<String>();
                while (reader.Read())
                {
                    aux.Add(reader.GetString(0));
                }
                Conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de los conceptos de pago de la base de datos");
            }

        }
        public bool CancelarPagoAlumno(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "UPDATE pagosAlumno SET Estado = 'Cancelado' WHERE ID=" + id;
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
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
        public Cobro ConsultarCobroColegiatura(String rfc)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT ID,Concepto,Cantidad,Pago,Restante,Alumno,Parent_ID FROM cobrosAlumno WHERE Alumno = '" + rfc + "' AND Concepto='Colegiatura';";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    Cobro cobro = new Cobro
                    {
                        Id = reader.GetInt32(0),
                        Concepto = reader.GetString(1),
                        Cantidad = reader.GetDecimal(2),
                        Pago = reader.GetDecimal(3),
                        Restante = reader.GetDecimal(4),
                        Remitente = reader.GetString(5),
                        Parent_id = reader.GetString(6)
                    };
                    Conn.Close();
                    return cobro;
                }
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de los pagos del alumno de la base de datos");
            }
 
        }
        public List<Cobro> ConsultarCobrosDeAlumno(String rfc)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT ID,Concepto,Cantidad,Pago,Restante,Alumno,Parent_ID FROM cobrosAlumno WHERE Alumno = '" + rfc + "' AND Restante > 0";
                    SqlDataReader reader = Cmd.ExecuteReader();
                    List<Cobro> aux = new List<Cobro>();
                    while (reader.Read())
                    {
                    Cobro cobro = new Cobro
                    {
                        Id = reader.GetInt32(0),
                        Concepto = reader.GetString(1),
                        Cantidad = reader.GetDecimal(2),
                        Pago = reader.GetDecimal(3),
                        Restante = reader.GetDecimal(4),
                        Remitente = reader.GetString(5),
                        Parent_id = reader.GetString(6)
                    };
                    aux.Add(cobro);
                    }
                    Conn.Close();
                    if (aux.Count != 0)
                        return aux;
                    else
                        return null;
                }
                catch (Exception e)
                {
                    Conn.Close();
                    throw new Exception("Error al obtener los datos de los pagos del alumno de la base de datos");
                }
            
        }
        public SqlDataAdapter ObtenerCobrosDeAlumnoTable(String rfc)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                    SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT ID,Concepto,Cantidad,Pago,Restante,Fecha FROM cobrosAlumno WHERE Alumno = '" + rfc + "'", Conn);
                    Conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    Conn.Close();
                    throw new Exception("Error al obtener los datos de los pagos del alumno de la base de datos");
                }
   
        }


        //-------------------------------MATERIAS-------------------------------//
        public bool AgregarMateria(Materia materia)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string agregar = "INSERT INTO materia (Nombre, Duracion, Semestre, Costo) VALUES('"
                        + materia.Nombre + "','" + materia.Duracion + "','" + materia.Semestre + "','" + materia.Costo + "');";
                string programas = "";
                if (materia.Programa != null)
                    programas = "INSERT INTO mapaCurricular (Materia, Programa) VALUES ((select TOP 1 ID from materia ORDER BY ID DESC), '" + materia.Programa + "');";


                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + agregar
                                    + programas
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar la materia a la Base de datos");
            }
          
        }
        public int ObtenerUltimoIDMateria()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT MAX(ID) FROM materia";
                    SqlDataReader reader = Cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int x = reader.GetInt32(0);
                        Conn.Close();
                        return x;
                    }
                    return 0;
                }
                catch (Exception e)
                {
                    Conn.Close();
                    throw new Exception("Error...!\n Error al Consultar id de materia a la Base de datos");
                }
          
        }
        public bool ActualizarMateria(Materia materia)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "UPDATE materia SET " +
                        "Nombre='" + materia.Nombre + "', Duracion='" + materia.Duracion + "', Semestre='" + materia.Semestre + "', Costo='" + materia.Costo +
                        "' WHERE ID=" + materia.Id + ";";
                    int rowsAfected = Cmd.ExecuteNonQuery();
                    Conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    Conn.Close();
                    throw new Exception("Error...!\n Error al actualizar la materia a la Base de datos");
                }
            
        }
        public bool DesactivarMateria(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "UPDATE materia SET Activo=0 WHERE ID=" + id + ";";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error...!\n Error al eliminar la materia a la Base de datos");
            }
        }
        public SqlDataAdapter ObtenerMateriasTable()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT M.ID, M.Nombre, M.Duracion,M.Semestre,M.Costo, P.Nombre AS 'Programa' FROM materia M LEFT JOIN mapaCurricular C ON C.Materia=M.ID INNER JOIN programa P ON C.Programa=P.Codigo WHERE M.Activo=1 ORDER BY M.ID ASC", Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de las materias de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerMateriasTable(string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT M.ID, M.Nombre, M.Duracion,M.Semestre,M.Costo, P.Nombre AS 'Programa' FROM materia M LEFT JOIN mapaCurricular C ON C.Materia=M.ID INNER JOIN programa P ON C.Programa=P.Codigo " +
                    " WHERE " +
                    "(M.ID LIKE '%" + parameter + "%' or " +
                    " M.Nombre LIKE '%" + parameter + "%' or " +
                    " M.Semestre LIKE '%" + parameter + "%' or " +
                    " P.Nombre LIKE '%" + parameter + "%' or " +
                    " C.Programa LIKE '%" + parameter + "%') WHERE M.Activo=1 ORDER BY M.ID ASC";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.Conn);
                this.Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de las materias de la base de datos");
            }
        }
        public Materia ConsultarMateria(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT * FROM materia WHERE ID='" + id + "'";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    Materia a = new Materia
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Duracion = reader.GetString(2),
                        Semestre = reader.GetInt32(3).ToString(),
                        Costo = reader.GetDecimal(4)
                    };
                    Conn.Close();
                    return a;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de la materia de la base de datos");
            }
        }
        public List<Materia> ObtenerMaterias()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT * FROM materia";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<Materia> aux = new List<Materia>();
                while (reader.Read())
                {

                    Materia a = new Materia
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Duracion = reader.GetString(2),
                        Semestre = reader.GetInt32(3).ToString(),
                        Costo = reader.GetDecimal(4)
                    };
                    aux.Add(a);
                }
                Conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de las materias de la base de datos");
            }
        }

        //-------------------------------PROGRAMA-------------------------------//
        public bool AgregarPrograma(Programa programa)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
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
                        materias += "INSERT INTO materia (Nombre,Duracion,Semestre,Costo,Activo) SELECT '" + m.Nombre + "', '" + m.Duracion + "', '" + m.Semestre + "', '" + m.Costo + "',1  WHERE NOT EXISTS (SELECT * FROM materia WHERE Nombre='" + m.Nombre + "' AND Duracion='" + m.Duracion + "' AND Semestre= '" + m.Semestre + "' AND Costo= '" + m.Costo + "' and Activo=1); ";
                        mapa += "INSERT INTO mapaCurricular (Materia, Programa) VALUES('" + m.Id + "','" + programa.Codigo + "');";
                    }


                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + agregar
                                    + "DELETE FROM mapaCurricular WHERE Programa='" + programa.Codigo + "';"
                                    + materias
                                    + mapa
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar Prorgrama a la Base de datos");
            }
        }
        public bool ActualizarPrograma(Programa programa)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
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
                        materias += "INSERT INTO materia (Nombre,Duracion,Semestre,Costo,Activo) SELECT '" + m.Nombre + "', '" + m.Duracion + "', '" + m.Semestre + "', '" + m.Costo + "',1  WHERE NOT EXISTS (SELECT * FROM materia WHERE Nombre='" + m.Nombre + "' AND Duracion='" + m.Duracion + "' AND Semestre= '" + m.Semestre + "' AND Costo= '" + m.Costo + "' and Activo=1); ";
                        mapa += "INSERT INTO mapaCurricular (Materia, Programa) VALUES('" + m.Id + "','" + programa.Codigo + "');";
                    }


                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + update
                                    + "DELETE FROM mapaCurricular WHERE Programa='" + programa.Codigo + "';"
                                    + materias
                                    + mapa
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error...!\n Error al actualizar la materia a la Base de datos");
            }
        }
        public bool DesactivarPrograma(string codigo)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "UPDATE programa SET Activo=0 WHERE Codigo='" + codigo + "';";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error...!\n Error al eliminar programa a la Base de datos");
            }
        }
        public SqlDataAdapter ObtenerProgramaTable()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT P.Codigo,P.Nivel, P.Nombre, P.Duracion, P.Horario, P.Modalidad FROM programa P WHERE P.Activo=1", Conn); Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de programas de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerProgramaTable(string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
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
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.Conn);
                this.Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de las materias de la base de datos");
            }
        }
        public Programa ConsultarPrograma(string codigo)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT * FROM programa WHERE Codigo='" + codigo + "'";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    Programa p = new Programa
                    {
                        Codigo = reader.GetString(0),
                        RVOE = reader.GetString(1),
                        CEIFRHS = reader.GetString(2),
                        Nombre = reader.GetString(3),
                        Nivel = reader.GetString(4),
                        Duracion = reader.GetString(5),
                        Horario = reader.GetString(6),
                        Modalidad = reader.GetString(7),
                        RequisitosEspecialidad = reader.GetString(8),
                        RequisitosTitulacion = reader.GetString(9),
                        RequisitosDiplomado = reader.GetString(10),
                        Objetivo = reader.GetString(11),
                        PerfilIngreso = reader.GetString(12),
                        PerfilEgreso = reader.GetString(13),
                        ProcesoSeleccion = reader.GetString(14),
                        CostoInscripcionSemestral = reader.GetDecimal(15),
                        CostoMensualidad = reader.GetDecimal(16),
                        CostoCursoPropedeutico = reader.GetDecimal(17),
                        Activo = reader.GetBoolean(18)
                    };
                    Conn.Close();
                    return p;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos del programa de la base de datos");
            }
        }
        public List<Materia> ConsultarMapaCurricularPrograma(string codigo)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT C.Materia ,M.Nombre,M.Duracion,M.Semestre,M.Costo FROM mapaCurricular C, materia M WHERE C.Materia= M.ID AND C.Programa='" + codigo + "'";

                SqlDataReader reader = Cmd.ExecuteReader();
                List<Materia> aux = new List<Materia>();
                while (reader.Read())
                {
                    Materia m = new Materia
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Duracion = reader.GetString(2),
                        Semestre = reader.GetInt32(3).ToString(),
                        Costo = reader.GetDecimal(4)
                    };
                    aux.Add(m);
                }
                Conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos del programa de la base de datos");
            }
        }
        public List<Programa> ObtenerProgramas()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT * FROM programa";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<Programa> aux = new List<Programa>();
                while (reader.Read())
                {

                    Programa p = new Programa
                    {
                        Codigo = reader.GetString(0),
                        RVOE = reader.GetString(1),
                        CEIFRHS = reader.GetString(2),
                        Nombre = reader.GetString(3),
                        Nivel = reader.GetString(4),
                        Duracion = reader.GetString(5),
                        Horario = reader.GetString(6),
                        Modalidad = reader.GetString(7),
                        RequisitosEspecialidad = reader.GetString(8),
                        RequisitosTitulacion = reader.GetString(9),
                        RequisitosDiplomado = reader.GetString(10),
                        Objetivo = reader.GetString(11),
                        PerfilIngreso = reader.GetString(12),
                        PerfilEgreso = reader.GetString(13),
                        ProcesoSeleccion = reader.GetString(14),
                        CostoInscripcionSemestral = reader.GetDecimal(15),
                        CostoMensualidad = reader.GetDecimal(16),
                        CostoCursoPropedeutico = reader.GetDecimal(17),
                        Activo = reader.GetBoolean(18)
                    };
                    aux.Add(p);
                }
                Conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de las materias de la base de datos");
            }
        }
        public string ObtenerNombrePrograma(string codigo)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT Nombre FROM programa WHERE Codigo = '" + codigo + "'";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    string nombre = "";
                    nombre = reader.GetString(0);
                    return nombre;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error..! Error al obtener el nombre del programa de la Base de Datos");
            }
        }
        public decimal ConsultarCostoPrograma(string programa)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT M.Costo FROM mapaCurricular C, materia M WHERE C.Materia= M.ID AND C.Programa='" + programa + "'";
                SqlDataReader reader = Cmd.ExecuteReader();
                decimal costoTotal = 0;
                while (reader.Read())
                {
                    costoTotal += reader.GetDecimal(0);
                }
                Conn.Close();
                return costoTotal;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos del programa de la base de datos");
            }
        }
        public string ObtenerProgramaAlumno(String rfc)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT Programa FROM alumnos WHERE RFC='" + rfc + "'";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    string programa = "";
                    programa = reader.GetString(0);
                    return programa;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos del programa del alumno de la base de datos");
            }
        }
        public List<string> ObtenerProgramasAlumno(String rfc)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT Programa FROM alumnos WHERE RFC='" + rfc + "'";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<string> programas = new List<string>();
                while (reader.Read())
                {
                    string programa = "";
                    programa = reader.GetString(0);
                    programas.Add(programa);
                }
                Conn.Close();
                if (programas.Count != 0)
                    return programas;
                else
                    return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos del programa del alumno de la base de datos");
            }
        }
        //-------------------------------GRUPOS-------------------------------//
        public bool AgregarGrupo(Grupo grupo)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string agregar = "INSERT INTO grupos (Generacion, Codigo, Programa) VALUES("
                        + " ' " + grupo.Generacion + "','" + grupo.Codigo + "','" + grupo.Programa + "');";


                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + agregar
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar Grupo a la Base de datos");
            }
        }
        public bool ActualizarGrupo(Grupo grupo)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string update = "UPDATE grupos SET Generacion='" + grupo.Generacion +
                          "', Programa='" + grupo.Programa + "' WHERE Codigo='" + grupo.Codigo + "';";



                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + update
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error...!\n Error al actualizar la grupo a la Base de datos");
            }
        }
        public bool DesactivarGrupo(string codigo)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "UPDATE grupos SET Activo=0 WHERE Codigo='" + codigo + "';";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error...!\n Error al eliminar programa a la Base de datos");
            }
        }
        public SqlDataAdapter ObtenerGruposTable()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT G.Codigo,G.Generacion, P.Nombre FROM grupos G, programa P WHERE G.Activo=1 AND P.Codigo=G.Programa", Conn); Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de grupos de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerGruposTable(string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT G.Codigo,G.Generacion, P.Nombre FROM grupos G, programa P WHERE " +
                    "(G.Codigo LIKE '%" + parameter + "%' or " +
                    " P.Nombre LIKE '%" + parameter + "%' or " +
                    " G.Programa LIKE '%" + parameter + "%' or " +
                    " G.Generacion LIKE '%" + parameter + "%') AND G.Activo=1 AND P.Codigo=G.Programa";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.Conn);
                this.Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de las materias de la base de datos");
            }
        }
        public Grupo ConsultarGrupo(string codigo)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT * FROM grupos WHERE Codigo='" + codigo + "'";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    Grupo g = new Grupo
                    {
                        Codigo = reader.GetString(1),
                        Generacion = reader.GetString(0),
                        Programa = reader.GetString(2)
                    };
                    Conn.Close();
                    return g;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos del grupo de la base de datos");
            }
        }
        public List<Alumno> ObtenerAlumnosGrupos(string codigo)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT A.Nombre, A.Direccion, A.Telefono1, A.Telefono2, A.Correo, A.Facebook, A.CURP, A.RFC, A.Sexo, A.EstadoCivil, A.EscuelaProcedencia, A.Carrera, A.Programa, A.Nivel, A.Fecha, A.Estado, A.Tipo FROM alumnos A, grupoAlumno G WHERE G.Alumno=A.RFC AND G.Grupo ='" + codigo + "';";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<Alumno> aux = new List<Alumno>();
                while (reader.Read())
                {
                    Alumno a = new Alumno
                    {
                        Nombre = reader.GetString(0),
                        Direccion = reader.GetString(1),
                        Telefono1 = reader.GetString(2),
                        Telefono2 = reader.GetString(3),
                        Correo = reader.GetString(4),
                        Facebook = reader.GetString(5),
                        Curp = reader.GetString(6),
                        Rfc = reader.GetString(7),
                        Sexo = reader.GetString(8),
                        EstadoCivil = reader.GetString(9),
                        EscuelaProcedencia = reader.GetString(10),
                        Carrera = reader.GetString(11),
                        Programa = reader.GetString(12),
                        Nivel = reader.GetString(13),
                        Fecha = reader.GetDateTime(14),
                        Estado = reader.GetString(15),
                        Tipo = reader.GetString(16)
                    };
                    aux.Add(a);
                }
                Conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de las grupos de la base de datos");
            }
        }
        public List<Grupo> ObtenerGrupos()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT * FROM grupos";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<Grupo> aux = new List<Grupo>();
                while (reader.Read())
                {

                    Grupo g = new Grupo
                    {
                        Codigo = reader.GetString(1),
                        Generacion = reader.GetString(0),
                        Programa = reader.GetString(2)
                    };
                    aux.Add(g);
                }
                Conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de las grupos de la base de datos");
            }
        }
        public List<Grupo> ObtenerGrupos(string parameter, string programa)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT G.Codigo,G.Generacion, G.Programa FROM grupos G, programa P WHERE " +
                           "(G.Codigo LIKE '%" + parameter + "%' or " +
                           " P.Nombre LIKE '%" + parameter + "%' or " +
                           " G.Programa LIKE '%" + parameter + "%' or " +
                           " P.Codigo LIKE '%" + parameter + "%' or " +
                           " G.Generacion LIKE '%" + parameter + "%') AND G.Activo=1 AND P.Codigo=G.Programa AND P.Codigo='" + programa + "';";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<Grupo> aux = new List<Grupo>();
                while (reader.Read())
                {

                    Grupo g = new Grupo
                    {
                        Codigo = reader.GetString(0),
                        Generacion = reader.GetString(1),
                        Programa = reader.GetString(2)
                    };
                    aux.Add(g);
                }
                Conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return aux;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de las grupos de la base de datos");
            }
        }

        //-------------------------------GRUPOS DE ALUMNOS-------------------------//
        public SqlDataAdapter ObtenerAlumnosGruposTable(string grupo)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT A.Nombre, A.RFC FROM grupoAlumno G, alumnos A WHERE A.RFC=G.Alumno AND G.Grupo='" + grupo + "';";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.Conn);
                this.Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos del grupo de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerAlumnosGruposTable(string grupo, string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT A.Nombre, A.RFC FROM grupoAlumno G, alumnos A WHERE " +
                    "(A.Nombre LIKE '%" + parameter + "%' or " +
                    " A.RFC LIKE '%" + parameter + "%') " +
                    //" A.Matricula LIKE '%" + parameter + "%' or " +
                    //" G.Generacion LIKE '%" + parameter + "%') "+
                    "AND A.RFC=G.Alumno AND G.Grupo='" + grupo + "';";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.Conn);
                this.Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos del grupo de la base de datos");
            }
        }
        public string ConsultarGrupoAlumno(string RFC)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT Grupo FROM grupoAlumno WHERE Alumno='" + RFC + "'";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    string grupo = reader.GetString(0);
                    Conn.Close();
                    return grupo;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos del alumno de la base de datos");
            }
        }
        public bool QuitarAlumnoDeGrupo(string grupo, string alumno)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string delete = "DELETE FROM grupoAlumno WHERE Alumno='" + alumno + "' AND Grupo='" + grupo + "';";


                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + delete
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar Grupo a la Base de datos");
            }
        }

        //-------------------------------TALLERES-------------------------------//
        public bool AgregarTaller(Taller taller)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string agregar = "INSERT INTO taller (Nombre, Fecha, CostoClientes,CostoPublico, Capacidad, Requisitos) VALUES("
                        + " ' " + taller.Nombre + "','" + taller.Fecha + "','" + taller.CostoClientes + "','" + taller.CostoPublico + "','" + taller.Capacidad + "','" + taller.Requisitos + "');";


                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + agregar
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar el taller a la Base de datos");
            }
        }
        public bool ActualizarTaller(Taller taller)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string update = "UPDATE taller SET Nombre='" + taller.Nombre +
                        "', Fecha='" + FormatearFecha(taller.Fecha) + "', CostoClientes='" + taller.CostoClientes +
                        "', CostoPublico='" + taller.CostoPublico + "', Capacidad='" + taller.Capacidad +
                        "', Requisitos='" + taller.Requisitos
                        + "', Estado='1' WHERE ID='" + taller.Id + "';";



                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + update
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error...!\n Error al actualizar el taller de la Base de datos");
            }
        }
        public bool CancelarTaller(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "UPDATE taller SET Estado=0 WHERE ID='" + id + "';";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error...!\n Error al eliminar taller a la Base de datos");
            }
        }
        public SqlDataAdapter ObtenerTalleresTable()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT ID, Nombre, Fecha, CostoClientes,CostoPublico, Capacidad, Requisitos FROM taller WHERE Estado=1", Conn); Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de talleres de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerTalleresTable(string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
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
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.Conn);
                this.Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de las materias de la base de datos");
            }
        }
        public Taller ConsultarTaller(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT ID, Nombre, Fecha, CostoClientes, CostoPublico, Capacidad, Requisitos FROM taller WHERE ID='" + id + "'";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    Taller g = new Taller
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Fecha = reader.GetDateTime(2),
                        CostoClientes = reader.GetDecimal(3),
                        CostoPublico = reader.GetDecimal(4),
                        Capacidad = reader.GetInt32(5),
                        Requisitos = reader.GetString(6)
                    };
                    Conn.Close();
                    return g;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos del grupo de la base de datos");
            }
        }
        public TallerAsistente ObtenerAsistenteTaller(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT A.Nombre,A.Telefono,A.Correo,A.CURP,A.RFC,A.Costo,A.Pago, A.Taller FROM tallerAsistentes A, taller T WHERE T.ID = A.Taller AND A.ID='" + id + "';";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    TallerAsistente a = new TallerAsistente
                    {
                        ID = Convert.ToInt32(id),
                        Nombre = reader.GetString(0),
                        Telefono = reader.GetString(1),
                        Correo = reader.GetString(2),
                        Curp = reader.GetString(3),
                        Rfc = reader.GetString(4),
                        Costo = reader.GetDecimal(5),
                        Pago = reader.GetDecimal(6)
                    };
                    a.Restante = a.Costo - a.Pago;
                    a.Taller = reader.GetInt32(7);
                    Conn.Close();
                    return a;
                }
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de los asistentes del taller de la base de datos");
            }
        }
        public List<TallerAsistente> ObtenerAsistentesTalleres(string taller)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT A.Nombre,A.Telefono,A.Correo,A.CURP,A.RFC,A.Costo,A.Pago FROM tallerAsistentes A, taller T WHERE T.ID = A.Taller AND T.ID='" + taller + "';";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<TallerAsistente> aux = new List<TallerAsistente>();
                while (reader.Read())
                {
                    TallerAsistente a = new TallerAsistente
                    {
                        Nombre = reader.GetString(0),
                        Telefono = reader.GetString(1),
                        Correo = reader.GetString(2),
                        Curp = reader.GetString(3),
                        Rfc = reader.GetString(4),
                        Costo = reader.GetDecimal(5),
                        Pago = reader.GetDecimal(6)
                    };
                    a.Restante = a.Costo - a.Pago;
                    aux.Add(a);
                }
                Conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de los asistentes del taller de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerAsistentesTalleresTable(string taller)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT A.ID, A.Nombre,A.Telefono,A.Correo,A.CURP,A.RFC,A.Costo,A.Pago, A.Costo-A.Pago AS 'Restante' FROM tallerAsistentes A, taller T WHERE T.ID = A.Taller AND T.ID='" + taller + "'", Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de talleres de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerAsistentesTalleresTable(string taller, string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT A.ID, A.Nombre,A.Telefono,A.Correo,A.CURP,A.RFC,A.Costo,A.Pago,A.Costo-A.Pago AS 'Restante' FROM tallerAsistentes A, taller T WHERE " +
                    "(A.Nombre LIKE '%" + parameter + "%' or " +
                    " A.Telefono LIKE '%" + parameter + "%' or " +
                    " A.CURP LIKE '%" + parameter + "%' or " +
                    " A.RFC LIKE '%" + parameter + "%' or " +
                    " A.Correo LIKE '%" + parameter + "%') AND  T.ID = A.Taller AND T.ID = '" + taller + "';";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.Conn);
                this.Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de las materias de la base de datos");
            }
        }
        public List<Taller> ObtenerTalleres()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT ID, Nombre, Fecha, CostoClientes, CostoPublico, Capacidad, Requisitos FROM taller WHERE Estado=1";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<Taller> aux = new List<Taller>();
                while (reader.Read())
                {
                    Taller g = new Taller
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Fecha = reader.GetDateTime(2),
                        CostoClientes = reader.GetDecimal(3),
                        CostoPublico = reader.GetDecimal(4),
                        Capacidad = reader.GetInt32(5),
                        Requisitos = reader.GetString(6)
                    };
                    aux.Add(g);
                }
                Conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de los talleres de la base de datos");
            }
        }
        public bool RegistrarAsistenteTaller(TallerAsistente asistente)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string inscribir = "INSERT INTO tallerAsistentes (Taller, Nombre, Telefono, Correo, CURP, RFC, Costo, Observaciones) VALUE ( '"
                     + asistente.Taller + "','" + asistente.Nombre + "','" + asistente.Telefono + "','" + asistente.Correo
                     + "','" + asistente.Curp + "','" + asistente.Rfc + "','" + asistente.Costo
                     + "','" + asistente.Observaciones + "');";
                if (asistente.ID > 0)
                {
                    inscribir = "UPDATE tallerAsistentes SET Taller='" + asistente.Taller + "',Nombre='" + asistente.Nombre +
                    "',Telefono='" + asistente.Telefono + "',Correo='" + asistente.Correo + "',CURP='" + asistente.Curp +
                    "',RFC='" + asistente.Rfc + "',Costo='" + asistente.Costo +
                    "',Observaciones='" + asistente.Observaciones + "' WHERE ID = " + asistente.ID + ";";
                }


                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + inscribir
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar asistencia a la Base de datos");
            }
        }
        public bool BorrarAsistenteTaller(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string inscribir = "DELETE FROM tallerAsistentes WHERE ID=" + id + "; ";


                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + inscribir
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n Error al borrar asistencia a la Base de datos");
            }
        }
        public bool RegistrarPagoAsistenciaTaller(Pago pago, string idAsistente)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string updateAsistente = "UPDATE tallerAsistentes SET Pago = Pago + " + pago.Cantidad +
                                            " WHERE ID = " + idAsistente + ";";
                string agregarPago = "INSERT INTO pagos (Emisor, FechaPago, Cantidad, Concepto, Area, Observaciones, Recibio, Parent_ID) VALUES ('"
                    + pago.Emisor + "', '" + FormatearFecha(pago.FechaPago) + "'," + pago.Cantidad + ", 'Pago de Taller', 'Escuela', '"
                    + pago.Observaciones + "', '" + pago.Recibio + "', '" + idAsistente + "');";


                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + updateAsistente
                                    + agregarPago
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar Pago de taller a la Base de datos");
            }
        }

        //-------------------------------ENTREGA DOCUMENTOS-------------------------------//
        public bool AgregarEntregaDocumentos(DocumentosInscripcion doc)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "INSERT INTO documentosInscripcion (Alumno, ActaNacimientoOrg, ActaNacimientoCop, TituloCedulaOrg, TituloLicCop, "
                     + "CedProfCop, SolicitudOpcionTitulacion, CertificadoLicCop, ConstanciaLibSSOrg, Curp, Fotografias, RecibioEmpleado, TipoInscripcion) VALUES ('"
                     + doc.Alumno + "', " + doc.ActaNacimientoOrg + ", " + doc.ActaNacimientoCop + ", " + doc.TituloCedulaOrg + ", " + doc.TituloLicCop + ", "
                     + doc.CedProfCop + ", " + doc.SolicitudOpcTitulacion + ", " + doc.CertificadoLicCop + ", " + doc.ConstanciaLibSSOrg + ", " + doc.Curp + ", "
                     + doc.Fotografias + ", '" + doc.RecibioEmpleado + "', " + doc.TipoInscripcion + ")";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception E)
            {
                Conn.Close();
                throw new Exception("Error al agregar la documentacion del alumnos a la base d datos");
            }
        }
        public SqlDataAdapter ObtenerEntregaDocumentos()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT Alumno, ActaNacimientoOrg, ActaNacimientoCop, TituloCedulaOrg, TituloLicCop, CedProfCop, SolicitudOpcionTitulacion, CertificadoLicCop, ConstanciaLibSSOrg, Curp, Fotografias, RecibioEmpleado FROM documentosInscripcion", Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de la documentacion entregada del alumno de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerEntregaDocumentosTable(string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT Alumno, ActaNacimientoOrg, ActaNacimientoCop, TituloCedulaOrg, TituloLicCop, CedProfCop, SolicitudOpcionTitulacion, CertificadoLicCop, ConstanciaLibSSOrg, Curp, Fotografias, RecibioEmpleado FROM documentosInscripcion WHERE" +
                    "(Alumno LIKE '%" + parameter + "%' or " +
                    "RecibioEmpleado LIKE '%" + parameter + "%')";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de la documentacion entregada del alumno de la base de datos");
            }
        }
        public DocumentosInscripcion ConsultarEntregaDocumentos(string rfc)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT * FROM documentosInscripcion WHERE Alumno='" + rfc + "'";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    DocumentosInscripcion doc = new DocumentosInscripcion
                    {
                        Alumno = reader.GetString(0),
                        ActaNacimientoOrg = reader.GetBoolean(1),
                        ActaNacimientoCop = reader.GetBoolean(2),
                        TituloCedulaOrg = reader.GetBoolean(3),
                        TituloLicCop = reader.GetBoolean(4),
                        CedProfCop = reader.GetBoolean(5),
                        SolicitudOpcTitulacion = reader.GetBoolean(6),
                        CertificadoLicCop = reader.GetBoolean(7),
                        ConstanciaLibSSOrg = reader.GetBoolean(8),
                        Curp = reader.GetBoolean(9),
                        Fotografias = reader.GetBoolean(10),
                        RecibioEmpleado = reader.GetString(11),
                        TipoInscripcion = reader.GetInt32(12)
                    };
                    Conn.Close();
                    return doc;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de la documentacion entregada del alumno de la base de datos");
            }
        }
        public bool ActualizarEntregaDocumentos(DocumentosInscripcion doc)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "UPDATE documentosInscripcion SET ActaNacimientoOrg= " + doc.ActaNacimientoOrg + ", ActaNacimientoCop= " + doc.ActaNacimientoCop + ", TituloCedulaOrg= " + doc.TituloCedulaOrg
                        + ", TituloLicCop= " + doc.TituloLicCop + ", CedProfCop= " + doc.CedProfCop + ", SolicitudOpcionTitulacion =" + doc.SolicitudOpcTitulacion + ", CertificadoLicCop= " + doc.CertificadoLicCop
                        + ", ConstanciaLibSSOrg =" + doc.ConstanciaLibSSOrg + ", Curp =" + doc.Curp + ", Fotografias =" + doc.Fotografias + ", RecibioEmpleado ='" + doc.RecibioEmpleado + "' WHERE Alumno = '" + doc.Alumno + "'";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al actualizar los datos de la documentacion entregada del alumno en la Base de Datos");
            }
        }

        //-------------------------------EMPLEADOS-------------------------------//
        public bool AgregarEmpleado(Empleado empleado, Usuario usuario)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
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


                Cmd.CommandText = "BEGIN TRANSACTION; " + empleados + usuarios + " COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar el empleado de la Base de datos");
            }
        }
        public bool ActualizarEmpleado(Empleado empleado, Usuario usuario)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
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
                Cmd.CommandText = "BEGIN TRANSACTION; " +
                    emp + user + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error...!\n Error al actualizar el empleado de la Base de datos");
            }
        }
        public bool ActualizarEmpleado(Empleado empleado)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "UPDATE empleados SET " +
                   "Nombre='" + empleado.Nombre + "',Telefono='" + empleado.Telefono + "',Puesto='" + empleado.Puesto + "',Correo='" + empleado.Correo +
                   "' WHERE ID=" + empleado.ID + " OR Matricula='" + empleado.Matricula + "';";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error...!\n Error al actualizar el empleado de la Base de datos");
            }
        }
        public bool DesactivarEmpleado(int id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "UPDATE empleados SET Estado=0 WHERE ID='" + id + "';";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error...!\n Error al eliminar el empleado de la Base de datos");
            }
        }
        public bool DesactivarEmpleado(string matricula)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string empleados = "UPDATE empleados SET Estado=0 WHERE Matricula='" + matricula + "';";
                string usuariosQuery = "UPDATE usuarios SET Estado=0 WHERE Matricula='" + matricula + "';";

                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + empleados
                                    + usuariosQuery
                                    + "COMMIT;"; int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error...!\n Error al desactivar el usuario de la Base de datos");
            }
        }
        public SqlDataAdapter ObtenerEmpleadosTable()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT E.ID, E.Matricula, E.Nombre,E.Telefono,E.Puesto, E.Correo FROM empleados E WHERE E.Estado = 1", Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de los empleados de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerEmpleadosTable(string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
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
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.Conn);
                this.Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de los empleados de la base de datos");
            }
        }
        public Empleado ConsultarEmpleado(string matricula)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT E.ID, E.Matricula, E.Nombre, E.Telefono, E.Puesto, E.Correo FROM empleados E WHERE E.Matricula='" + matricula + "'";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    Empleado a = new Empleado
                    {
                        ID = reader.GetInt32(0),
                        Matricula = reader.GetString(1),
                        Nombre = reader.GetString(2),
                        Telefono = reader.GetString(3),
                        Puesto = reader.GetString(4),
                        Correo = reader.GetString(5)
                    };
                    Conn.Close();
                    return a;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos del empleado de la base de datos");
            }
        }
        public List<Empleado> ObtenerEmpleados()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT E.ID, E.Matricula, E.Nombre, E.Telefono, E.Puesto, E.Correo FROM empleados E WHERE E.Estado = 1";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<Empleado> aux = new List<Empleado>();
                while (reader.Read())
                {
                    Empleado a = new Empleado
                    {
                        ID = reader.GetInt32(0),
                        Matricula = reader.GetString(1),
                        Nombre = reader.GetString(2),
                        Telefono = reader.GetString(3),
                        Puesto = reader.GetString(4),
                        Correo = reader.GetString(5)
                    };
                    aux.Add(a);
                }
                Conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de los empleados de la base de datos");
            }
        }
        public List<Empleado> ObtenerEmpleadosAll()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT E.ID, E.Matricula, E.Nombre, E.Telefono, E.Puesto, E.Correo FROM empleados E";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<Empleado> aux = new List<Empleado>();
                while (reader.Read())
                {
                    Empleado a = new Empleado
                    {
                        ID = reader.GetInt32(0),
                        Matricula = reader.GetString(1),
                        Nombre = reader.GetString(2),
                        Telefono = reader.GetString(3),
                        Puesto = reader.GetString(4),
                        Correo = reader.GetString(5)
                    };
                    aux.Add(a);
                }
                Conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de los empleados de la base de datos");
            }
        }
        public string ObtenerNombreEmpleado(string matricula)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT Nombre FROM empleados WHERE Matricula = '" + matricula + "'";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    string nombre = "";
                    nombre = reader.GetString(0);
                    return nombre;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error..! Error al obtener el nombre del empleado de la Base de Datos");
            }
        }
        public string ValidarMatricula(string matricula)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT Matricula FROM empleados WHERE Matricula = '" + matricula + "'; SELECT Matricula FROM psicoterapeutas WHERE Matricula = '" + matricula + "';";
                SqlDataReader reader = Cmd.ExecuteReader();
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
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error..! Error al obtener el nombre del empleado de la Base de Datos");
            }
        }
        public string ObtenerOrigenMatricula(string matricula)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT 'Empleado' FROM empleados E WHERE E.Matricula = '" + matricula + "' UNION SELECT 'Psicoterapeuta' FROM psicoterapeutas P WHERE P.Matricula = '" + matricula + "'";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    string nombre = "";
                    nombre = reader.GetString(0);
                    return nombre;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error..! Error al obtener el tipo de la Base de Datos");
            }
        }


        //-----------------------------INSCRIPCIONES---------------------------------//
        public bool InscribirAlumnoPrograma(string RFC, string programa)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string agregar = "INSERT INTO programaAlumno (Alumno, Programa, Estado) VALUES("
                        + "','" + RFC + "','" + programa + "','Inscrito');";
                string updateDatosAlumno = "UPDATE alumno SET Programa = '" + programa + "' WHERE RFC='" + RFC + "';";


                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + agregar
                                    + updateDatosAlumno
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar Prorgrama a la Base de datos");
            }
        }
        public bool InscribirAlumnoGrupo(string RFC, string grupo, string programa)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
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
                    cantidad = ConsultarCostoPrograma(programa);
                }
                catch (Exception ex) { }
                string registroCobroInscripcion = "INSERT INTO cobrosAlumno (Alumno,Concepto,Cantidad,Pago,Restante,Fecha,Parent_ID) VALUES('"
                    + RFC + "','Colegiatura','" + cantidad + "','0.00','" + cantidad + "','" + FormatearFecha(DateTime.Now) + "','" + RFC + "');";
                string updateDatosAlumno = "UPDATE alumnos SET Programa = '" + programa + "', Estado='Registrado' WHERE RFC='" + RFC + "';";


                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + inscribirGrupo
                                    + inscribirPrograma
                                    + updateDatosAlumno
                                    + registroCobroInscripcion; int rowsAfected = Cmd.ExecuteNonQuery();
                Cmd.CommandText = "COMMIT;";
                Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Cmd.CommandText = "ROLLBACK;";
                Cmd.ExecuteNonQuery();
                Conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar Prorgrama a la Base de datos");
            }
        }

        //-------------------------------ASISTENCIAS-------------------------------//
        public List<PaseDeListaAlumno> ObtenerAsistenciaAlumnosMateriaTable(string grupo, int materia, List<Alumno> alumnos)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT G.Alumno, A.Nombre, P.Estado, P.Fecha,P.isTarde FROM grupoAlumno G INNER JOIN alumnos A on A.RFC=G.Alumno inner JOIN pasesDeListaAlumnos P ON A.RFC=P.Alumno WHERE P.Grupo='" + grupo + "' AND P.Materia=' " + materia.ToString() + "' ORDER BY P.Fecha ASC;";

                Cmd.CommandText = sqlString;

                SqlDataReader reader = Cmd.ExecuteReader();
                List<PaseDeListaAlumno> aux = new List<PaseDeListaAlumno>();
                foreach (Alumno alu in alumnos)
                {
                    PaseDeListaAlumno pls = new PaseDeListaAlumno();
                    pls.RFC = alu.Rfc;
                    pls.Alumno = alu.Nombre;
                    pls.Asistencias = null;
                    aux.Add(pls);
                }
                string rfc;
                string nombre;
                Asistencias asistencia;
                while (reader.Read())
                {
                    rfc = reader.GetString(0);
                    nombre = reader.GetString(1);
                    asistencia = new Asistencias
                    {
                        Estado = reader.GetString(2),
                        Fecha = reader.GetDateTime(3),
                        IsTarde = reader.GetBoolean(4)
                    };
                    int index = 0;
                    foreach (PaseDeListaAlumno pl in aux)
                    {
                        if (pl.RFC == rfc)
                        {
                            if (aux.ElementAt(index).Asistencias == null)
                                aux.ElementAt(index).Asistencias = new List<Asistencias>();
                            aux.ElementAt(index).Asistencias.Add(asistencia);
                        }
                        index++;
                    }
                }
                Conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de los listas de la base de datos");
            }
        }
        public bool RegistrarAsistencias(List<PaseDeListaAlumno> lista, string maestro, string grupo, string materia, string fecha)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string paseDeLista = "INSERT INTO pasesDeLista (Grupo, Materia, Fecha, Encargado) VALUES('"
                    + grupo + "','" + materia + "','" + fecha + "','" + maestro + "'); ";
                string paseDeListaAlumnos = "";
                foreach (PaseDeListaAlumno aux in lista)
                {
                    paseDeListaAlumnos += " INSERT INTO pasesDeListaAlumnos (ID,Alumno, Estado, Fecha, Grupo, Materia, isTarde) "
                        + " SELECT AUTO_INCREMENT , '" + aux.RFC + "','" + aux.Asistencias.First().Estado + "','" + fecha + "','" + grupo + "','" + materia + "'," + aux.Asistencias.First().IsTarde + " FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '" + this.Database + "' AND TABLE_NAME = 'pasesDeLista'; ";
                }


                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + paseDeListaAlumnos
                                    + paseDeLista
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n Error al registrar asistencias a la Base de datos");
            }
        }
       
        //-------------------------------CALIFICACIONES-------------------------------//
        public List<CalificacionesAlumno> ObtenerCalificacionesAlumnosMateriaTable(string grupo, int materia, List<Alumno> alumnos)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT G.Alumno, A.Nombre, C.CalificacionTareas, C.CalificacionFinal FROM grupoAlumno G INNER JOIN alumnos A on A.RFC=G.Alumno inner JOIN calificacionAlumno C ON A.RFC=C.Alumno WHERE C.Grupo='" + grupo + "' AND C.Materia='" + materia.ToString() + "' ;";

                Cmd.CommandText = sqlString; SqlDataReader reader = Cmd.ExecuteReader();
                List<CalificacionesAlumno> aux = new List<CalificacionesAlumno>();
                foreach (Alumno alu in alumnos)
                {
                    CalificacionesAlumno pls = new CalificacionesAlumno
                    {
                        RFC = alu.Rfc,
                        Alumno = alu.Nombre,
                        Calificaciones = null
                    };
                    aux.Add(pls);
                }
                string rfc;
                string nombre;
                Calificacion calificacion;
                while (reader.Read())
                {
                    rfc = reader.GetString(0);
                    nombre = reader.GetString(1);
                    calificacion = new Calificacion
                    {
                        CalificacionTareas = reader.GetFloat(2),
                        CalificacionFinal = reader.GetFloat(3),
                        Materia = materia
                    };
                    int index = 0;
                    foreach (CalificacionesAlumno pl in aux)
                    {
                        if (pl.RFC == rfc)
                        {
                            if (aux.ElementAt(index).Calificaciones == null)
                                aux.ElementAt(index).Calificaciones = new List<Calificacion>();
                            aux.ElementAt(index).Calificaciones.Add(calificacion);
                        }
                        index++;
                    }
                }
                Conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de las calificaciones de la base de datos");
            }
        }
        public bool RegistrarCalificaciones(List<CalificacionesAlumno> lista, string grupo, string materia)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string calificaciones = "";
                foreach (CalificacionesAlumno aux in lista)
                {
                    calificaciones += "INSERT INTO calificacionAlumno (Grupo, Materia, Alumno, CalificacionTareas,CalificacionFinal) SELECT '" + grupo + "','" + materia + "','" + aux.RFC + "','" + aux.Calificaciones.ElementAt(0).CalificacionTareas + "','"
                    + aux.Calificaciones.ElementAt(0).CalificacionFinal + "'  WHERE NOT EXISTS (SELECT * FROM calificacionAlumno WHERE Grupo='" + grupo + "' AND Materia ='" + materia + "' AND Alumno ='" + aux.RFC + "'); ";

                }


                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + calificaciones
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar calificaciones a la Base de datos");
            }
        }
        public List<Calificacion> ObtenerCalificacionesAlumno(string alumno, string grupo)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT M.Nombre , C.Materia, C.CalificacionTareas, C.CalificacionFinal FROM materia M, grupoAlumno G INNER JOIN alumnos A on A.RFC=G.Alumno inner JOIN calificacionAlumno C ON A.RFC=C.Alumno WHERE C.Grupo='" + grupo + "' AND M.ID=C.Materia AND C.Alumno='" + alumno + "' ;";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<Calificacion> aux = new List<Calificacion>();
                while (reader.Read())
                {
                    Calificacion cal = new Calificacion
                    {
                        MateriaNombre = reader.GetString(0),
                        Materia = reader.GetInt32(1),
                        CalificacionTareas = reader.GetFloat(2),
                        CalificacionFinal = reader.GetFloat(3)
                    };
                    aux.Add(cal);
                }
                Conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener calificaciones del alumno de la base de datos");
            }
        }
        public SqlDataAdapter ConsultarCalificacionesAlumno(string alumno, string grupo)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT M.Nombre AS 'Materia', C.CalificacionTareas, C.CalificacionFinal FROM materia M, grupoAlumno G INNER JOIN alumnos A on A.RFC=G.Alumno inner JOIN calificacionAlumno C ON A.RFC=C.Alumno WHERE C.Grupo='" + grupo + "' AND M.ID=C.Materia AND C.Alumno='" + alumno + "' ;", Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener calificaciones del alumno de la base de datos");
            }
        }

        //-------------------------------PSICOTERAPEUTAS-------------------------------//
        public bool AgregarPsicoterapeuta(Psicoterapeuta psicoterapeuta, Usuario usuario)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
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


                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + psicoterapeutaQuery
                                    + usuariosQuery
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar al Psicoterapeuta de la Base de datos");
            }
        }
        public bool ActualizarPsicoterapeuta(Psicoterapeuta psicoterapeuta, Usuario usuario)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
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
                Cmd.CommandText = "BEGIN TRANSACTION; " +
                                    psicoterapeutaQuery +
                                    usuariosQuery +
                                    "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error...!\n Error al actualizar al psicoterapeuta de la Base de datos");
            }
        }
        public bool ActualizarPsicoterapeuta(Psicoterapeuta psicoterapeuta)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "UPDATE psicoterapeutas SET Matricula='" + psicoterapeuta.Matricula + "', Nombre='" + psicoterapeuta.Nombre +
                    "', Telefono='" + psicoterapeuta.Telefono + "', Carrera='" + psicoterapeuta.Carrera + "', Especialidad='" + psicoterapeuta.Especialidad +
                    "', Horario='" + psicoterapeuta.Horario + "', Observaciones='" + psicoterapeuta.Observaciones +
                    "', Estado=1 WHERE ID = " + psicoterapeuta.ID + " OR Matricula = '" + psicoterapeuta.Matricula + "';";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error...!\n Error al actualizar al psicoterapeuta de la Base de datos");
            }
        }
        public bool DesactivarPsicoterapeuta(int id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "UPDATE psicoterapeutas SET Estado=0 WHERE ID=" + id + ";";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error...!\n Error al desactivar al psicoterapeuta de la Base de datos");
            }
        }
        public bool DesactivarPsicoterapeuta(string matricula)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string psicoterapeutasQuery = "UPDATE psicoterapeutas SET Estado=0 WHERE Matricula='" + matricula + "';";
                string usuariosQuery = "UPDATE usuarios SET Estado=0 WHERE Matricula='" + matricula + "';";


                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + psicoterapeutasQuery
                                    + usuariosQuery
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error...!\n Error al desactivar al psicoterapeuta de la Base de datos");
            }
        }
        public SqlDataAdapter ObtenerPsicoterapeutasTable()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT ID, Matricula, Nombre, Telefono, Carrera, Especialidad, Horario FROM psicoterapeutas WHERE Estado = 1", Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de los psicoterapeutas de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerPsicoterapeutasTable(string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
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
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.Conn);
                this.Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de los psicoterapeutas de la base de datos");
            }
        }
        public Psicoterapeuta ConsultarPsicoterapeuta(string matricula)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT ID, Matricula, Nombre, Telefono, Carrera, Especialidad, Horario, Observaciones, Estado FROM psicoterapeutas WHERE Matricula='" + matricula + "'";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    Psicoterapeuta a = new Psicoterapeuta
                    {
                        ID = reader.GetInt32(0),
                        Matricula = reader.GetString(1),
                        Nombre = reader.GetString(2),
                        Telefono = reader.GetString(3),
                        Carrera = reader.GetString(4),
                        Especialidad = reader.GetString(5),
                        Horario = reader.GetString(6),
                        Observaciones = reader.GetString(7),
                        Estado = reader.GetString(8)
                    };
                    Conn.Close();
                    return a;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos del Psicoterapeuta de la base de datos");
            }
        }
        public List<Psicoterapeuta> ObtenerPsicoterapeutas()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT ID, Matricula, Nombre, Telefono, Carrera, Especialidad, Horario, Observaciones, Estado FROM psicoterapeutas WHERE Estado = 1";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<Psicoterapeuta> aux = new List<Psicoterapeuta>();
                while (reader.Read())
                {
                    Psicoterapeuta a = new Psicoterapeuta
                    {
                        ID = reader.GetInt32(0),
                        Matricula = reader.GetString(1),
                        Nombre = reader.GetString(2),
                        Telefono = reader.GetString(3),
                        Carrera = reader.GetString(4),
                        Especialidad = reader.GetString(5),
                        Horario = reader.GetString(6),
                        Observaciones = reader.GetString(7),
                        Estado = reader.GetString(8)
                    };
                    aux.Add(a);
                }
                Conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de los Psicoterapeuta de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerConsultasPsicoterapeutaTable(string matricula)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string query = "";
                query += "SELECT S.Fecha, concat_ws(' ',P.Nombre, P.Apellidos) AS 'Paciente', 'Sesión' AS 'Tipo' FROM sesiones S INNER JOIN pacientes P on S.Paciente_ID= P.ID WHERE S.Psicoterapeuta_ID = '" + matricula + "' AND S.Estado = 'Activa' ";
                query += "UNION SELECT E.Fecha, concat_ws(' ',P.Nombre, P.Apellidos) AS 'Paciente', 'Evaluación' AS 'Tipo' FROM evaluaciones E INNER JOIN pacientes P on E.Paciente_ID= P.ID WHERE E.Psicoterapeuta_ID = '" + matricula + "' AND E.Estado = 'Activa' ";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(query, Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de los psicoterapeutas de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerPacientesPsicoterapeutaTable(string matricula)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT P.ID, concat_ws(' ',P.Nombre, P.Apellidos) AS 'Nombre', P.EscuelaEmpresa AS 'Institucion',P.Telefono FROM pacientes P  WHERE P.Psicoterapeuta='" + matricula + "' AND P.Estado = 1", Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de pacientes de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerPacientesPsicoterapeutaTable(string matricula, string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string query = "SELECT P.ID, concat_ws(' ',P.Nombre, P.Apellidos) AS 'Nombre', P.EscuelaEmpresa AS 'Institucion',P.Telefono FROM pacientes P WHERE P.Psicoterapeuta='" + matricula + "' AND P.Estado = 1 AND (" +
                    " P.ID LIKE '%" + parameter + "%' or " +
                    " P.EscuelaEmpresa LIKE '%" + parameter + "%' or " +
                    " P.Telefono LIKE '%" + parameter + "%' or " +
                    " concat_ws(' ',P.Nombre, P.Apellidos) LIKE '%" + parameter + "%');";
                ;
                SqlDataAdapter mdaDatos = new SqlDataAdapter(query, Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de pacientes de la base de datos");
            }
        }

        //--------------------------------NOMINA--------------------------------------//
        public List<string> ObtenerConsultasPsicoterapeutaPendientes(string matricula,DateTime inicio ,DateTime fin)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string query = "";
                query += "SELECT 's' AS Tipo, S.Costo FROM sesiones S WHERE S.Psicoterapeuta_ID = '" + matricula + "' AND S.Estado = 'Activa' AND S.Fecha BETWEEN '" + FormatearFecha(inicio) + "' AND '" + FormatearFecha(fin) + "' ";
                query += "UNION SELECT 'e' AS Tipo E.Costo FROM evaluaciones E WHERE E.Psicoterapeuta_ID = '" + matricula + "' AND E.Estado = 'Activa' AND E.Fecha BETWEEN '" + FormatearFecha(inicio) + "' AND '" + FormatearFecha(fin) + "'";


                Cmd.CommandText = query;
                SqlDataReader reader = Cmd.ExecuteReader();
                List<string> aux = new List<string>();
                while (reader.Read())
                {
                    aux.Add(reader.GetString(0));
                    aux.Add(reader.GetDecimal(1).ToString());
                }
                Conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de la nomina de la base de datos");
            }
        }
        public bool AgregarNomina(Nomina nomina)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string nominaQuery = "INSERT INTO nominas (Psicoterapeuta,	DiaEntrega,	FechaInicio, FechaFin, Total, Estado) VALUES("
                        + nomina.Psicoterapeutas + ",'" + FormatearFecha(nomina.DiaEntrega) + "','" + FormatearFecha(nomina.FechaInicio) + "','" + FormatearFecha(nomina.FechaFin) + "'," + nomina.Total + ",'Pagada');";

                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + nominaQuery
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar pago de nomina a la Base de datos");
            }
        }
        public bool ActualizarNomina(Nomina nomina)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string nominaQuery = "UPDATE nominas SET=Psicoterapeuta='" + nomina.Psicoterapeutas + "', DiaEntrega='" + FormatearFecha(nomina.DiaEntrega) +
                   "',	FechaInicio='" + FormatearFecha(nomina.FechaInicio) + "', FechaFin='" + FormatearFecha(nomina.FechaFin) + "', Total=" + nomina.Total + ", Estado='Pagada';";

                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + nominaQuery
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar pago de nomina a la Base de datos");
            }
        }
        public DateTime ConsultarUltimaFechaNomina(string matricula)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT TOP 1 DiaEntrega FROM nominas WHERE Psicoterapeuta='" + matricula + "' ORDER BY DiaEntrega DESC ";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    DateTime date = reader.GetDateTime(0);
                    Conn.Close();
                    return date;
                }
                Conn.Close();
                return new DateTime(2000, 01, 01);
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de nomina de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerNominaPacienteTable(string matricula,string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string query = "SELECT ID,DiaEntrega,FechaInicio,FechaFin,Total FROM nominas WHERE Psicoterapeuta='" + matricula + "' AND " +
                    " (ID LIKE '%" + parameter + "%' or " +
                    " DiaEntrega LIKE '%" + parameter + "%' or " +
                    " Total LIKE '%" + parameter + "%' );";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(query, Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de nomina de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerNominaPacienteTable(string matricula)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT ID,DiaEntrega,FechaInicio,FechaFin,Total FROM nominas WHERE Psicoterapeuta='" + matricula + "'", Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de nomina de la base de datos");
            }
        }


        //------------------------------PACIENTES------------------------------------//
        public bool AgregarPaciente(Paciente paciente)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string pacienteQuery = "INSERT INTO pacientes (Nombre, Apellidos, FechaNacimiento, EscuelaEmpresa, CostoPaciente, Telefono, TutorNombre, TutorTelefono, Psicoterapeuta) VALUES ('"
                    + paciente.Nombre + "','" + paciente.Apellidos + "','" + FormatearFecha(paciente.FechaNacimiento) + "','" + paciente.Institucion + "','" + paciente.CostoEspecial + "','" + paciente.Telefono + "', '" + paciente.Nombre_tutor + "','" + paciente.Telefono_tutor + "'," + paciente.Psicoterapeuta + "); ";
                if (paciente.Psicoterapeuta == null)
                    pacienteQuery = "INSERT INTO pacientes (Nombre, Apellidos, FechaNacimiento, EscuelaEmpresa, CostoPaciente, Telefono, TutorNombre, TutorTelefono, Psicoterapeuta) VALUES ('"
                        + paciente.Nombre + "','" + paciente.Apellidos + "','" + FormatearFecha(paciente.FechaNacimiento) + "','" + paciente.Institucion + "','" + paciente.CostoEspecial + "','" + paciente.Telefono + "', '" + paciente.Nombre_tutor + "','" + paciente.Telefono_tutor + "',NULL); ";
                string facturacionQuery = "";
                if (paciente.Datos_facturacion != null)
                {
                    facturacionQuery = " INSERT INTO datosFacturacion (PacienteID, RFC, Nombre, RazonSocial, Direccion)"
                    + " SELECT AUTO_INCREMENT-1, '" + paciente.Datos_facturacion[0] + "','" + paciente.Datos_facturacion[1] + "','" + paciente.Datos_facturacion[2] + "','" + paciente.Datos_facturacion[3] + "' FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '" + this.Database + "' AND TABLE_NAME = 'pacientes'; "; ;
                }


                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + pacienteQuery
                                    + facturacionQuery
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar datos de praciente de la Base de datos");
            }
        }
        public bool ActualizarPaciente(Paciente paciente)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string facturacionQuery = "";
                string pacienteQuery = "UPDATE pacientes SET Nombre='" + paciente.Nombre + "' ,Apellidos='" + paciente.Apellidos + "',FechaNacimiento=" + FormatearFecha(paciente.FechaNacimiento) + "',EscuelaEmpresa='" + paciente.Institucion
                    + "',CostoPaciente='" + paciente.CostoEspecial + "',Telefono='" + paciente.Telefono + "',Estado=1, TutorNombre='" + paciente.Nombre_tutor +
                    "',TutorTelefono='" + paciente.Telefono_tutor + "',Psicoterapeuta='" + paciente.Psicoterapeuta + "' WHERE ID=" + paciente.Id + ";";
                if (paciente.Psicoterapeuta == null)
                    pacienteQuery = "UPDATE pacientes SET Nombre='" + paciente.Nombre + "' ,Apellidos='" + paciente.Apellidos + "',EscuelaEmpresa='" + paciente.Institucion
                    + "',CostoPaciente='" + paciente.CostoEspecial + "',Telefono='" + paciente.Telefono + "',Estado=1, TutorNombre='" + paciente.Nombre_tutor +
                    "',TutorTelefono='" + paciente.Telefono_tutor + "',Psicoterapeuta=NULL WHERE ID=" + paciente.Id + ";";
                if (paciente.Datos_facturacion != null)
                {


                    facturacionQuery = " INSERT INTO datosFacturacion (PacienteID, RFC, Nombre, RazonSocial, Direccion) SELECT " + paciente.Id + ",'"
                        + paciente.Datos_facturacion[0] + "', '" + paciente.Datos_facturacion[1] + "', '" + paciente.Datos_facturacion[2] + "', '" + paciente.Datos_facturacion[3] +
                        "'  WHERE NOT EXISTS (SELECT * FROM datosFacturacion WHERE PacienteID =  " + paciente.Id + ");";
                }
                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + pacienteQuery
                                    + facturacionQuery
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error...!\n Error al actualizar el paciente de la Base de datos");
            }
        }
        public bool EliminarPaciente(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "UPDATE pacientes SET Estado=0 WHERE ID =" + id + ";";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error...!\n Error al desactivar el usuario de la Base de datos");
            }
        }
        public SqlDataAdapter ObtenerPacientesTable()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT P.ID, concat_ws(' ',P.Nombre, P.Apellidos) AS 'Nombre', P.EscuelaEmpresa AS 'Institucion',P.Telefono, T.Nombre AS 'Psicoterapeuta' FROM pacientes P LEFT JOIN psicoterapeutas T on P.Psicoterapeuta=T.ID  WHERE P.Estado = 1", Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de pacientes de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerPacientesTable(string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
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
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.Conn);
                this.Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de los empleados de la base de datos");
            }
        }
        public Paciente ConsultarPaciente(string ID)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT P.ID, P.Nombre, P.Apellidos, P.FechaNacimiento, P.EscuelaEmpresa, P.CostoPaciente, P.Telefono, P.Estado, P.TutorNombre, P.TutorTelefono, P.Psicoterapeuta, F.RFC,F.Nombre,F.RazonSocial,F.Direccion FROM pacientes P LEFT JOIN datosFacturacion F ON P.ID = F.PacienteID WHERE P.ID=" + ID + ";";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    Paciente p = new Paciente
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Apellidos = reader.GetString(2),
                        FechaNacimiento = reader.GetDateTime(3),
                        Institucion = reader.GetString(4),
                        CostoEspecial = reader.GetDecimal(5),
                        Telefono = reader.GetString(6),
                        Estado = reader.GetString(7),
                        Nombre_tutor = reader.GetString(8),
                        Telefono_tutor = reader.GetString(9)
                    };
                    try
                    {
                        reader.GetString(11);
                        p.Datos_facturacion = new string[4];
                        p.Datos_facturacion[0] = reader.GetString(11);
                        p.Datos_facturacion[1] = reader.GetString(12);
                        p.Datos_facturacion[2] = reader.GetString(13);
                        p.Datos_facturacion[3] = reader.GetString(14);
                    }
                    catch (Exception e)
                    { }
                    try
                    {
                        p.Psicoterapeuta = reader.GetString(10);
                    }
                    catch (Exception e)
                    { }
                    Conn.Close();
                    return p;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos del paciente de la base de datos");
            }
        }
        public List<Paciente> ObtenerPacientes()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT P.ID, P.Nombre, P.Apellidos,P.FechaNacimiento, P.EscuelaEmpresa, P.CostoPaciente, P.Telefono, P.Estado, P.TutorNombre, P.TutorTelefono, P.Psicoterapeuta, F.RFC,F.Nombre,F.RazonSocial,F.Direccion FROM pacientes P LEFT JOIN datosFacturacion F ON P.ID = F.PacienteID WHERE P.Estado = 1";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<Paciente> aux = new List<Paciente>();
                while (reader.Read())
                {
                    Paciente p = new Paciente
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Apellidos = reader.GetString(2),
                        FechaNacimiento = reader.GetDateTime(3),
                        Institucion = reader.GetString(4),
                        CostoEspecial = reader.GetDecimal(5),
                        Telefono = reader.GetString(6),
                        Estado = reader.GetString(7),
                        Nombre_tutor = reader.GetString(8),
                        Telefono_tutor = reader.GetString(9)
                    };
                    try
                    {
                        reader.GetString(11);
                        p.Datos_facturacion = new string[4];
                        p.Datos_facturacion[0] = reader.GetString(11);
                        p.Datos_facturacion[1] = reader.GetString(12);
                        p.Datos_facturacion[2] = reader.GetString(13);
                        p.Datos_facturacion[3] = reader.GetString(14);
                    }
                    catch (Exception e)
                    { }
                    try
                    {
                        p.Psicoterapeuta = reader.GetString(10);
                    }
                    catch (Exception e)
                    { }
                    aux.Add(p);
                }
                Conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de los pacientes de la base de datos");
            }
        }

        //---------------------------SESIONES------------------------//
        public bool AgregarSesion(Sesion sesion)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string reservacionQuery = "";
                string sesionQuery = "";
                if (sesion.Reservacion != null)
                {
                    reservacionQuery = "UPDATE reservaciones SET ID_Parent = (SELECT AUTO_INCREMENT FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '" + this.Database + "' AND TABLE_NAME = 'sesiones') WHERE ID=" + sesion.Reservacion.Id + ";";
                    sesionQuery = " INSERT INTO sesiones ( Reservacion_ID, Costo, Pago, Pendiente ,Fecha, Hora, Tipo, Observaciones, Paciente_ID, Psicoterapeuta_ID, Estado)"
                        + " VALUES ('" + sesion.Reservacion.Id + "','" + sesion.Costo + "','" + sesion.Pago + "', Costo-Pago,'" + FormatearFecha(sesion.Fecha) + "','" + sesion.Hora + "','" + sesion.Tipo + "','" + sesion.Observaciones + "','" + sesion.Paciente + "','" + sesion.Psicoterapeuta + "','Activa');";
                }
                else
                {
                    sesionQuery = " INSERT INTO sesiones ( Costo, Pago, Pendiente, Fecha, Hora, Tipo, Observaciones, Paciente_ID, Psicoterapeuta_ID, Estado)"
                        + " VALUES ('" + sesion.Costo + "','" + sesion.Pago + "', Costo-Pago,'" + FormatearFecha(sesion.Fecha) + "','" + sesion.Hora + "','" + sesion.Tipo + "','" + sesion.Observaciones + "','" + sesion.Paciente + "','" + sesion.Psicoterapeuta + "','Activa');";
                }


                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + reservacionQuery
                                    + sesionQuery
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar datos de sesión de la Base de datos");
            }
        }
        public bool ActualizarSesion(Sesion sesion)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string reservacionQuery = "";
                if (sesion.Reservacion != null)
                    reservacionQuery = "UPDATE reservaciones SET Reservante='" + sesion.Reservacion.Reservante + "', Fecha='" + FormatearFecha(sesion.Reservacion.Fecha) +
                         "Codigo_Reservacion='" + sesion.Reservacion.Codigo_Reservacion + "', Hora_Inicio='" + sesion.Reservacion.Hora_Inicio + "', Duracion='" + sesion.Reservacion.Duracion +
                        "', Hora_Fin='" + sesion.Reservacion.Hora_Fin + "', Concepto='" + sesion.Reservacion.Concepto + "', ID_Parent='" + sesion.Id +
                        "', Ubicacion='" + sesion.Reservacion.Ubicacion + "', Observaciones='" + sesion.Reservacion.Observaciones +
                        "' WHERE ID=" + sesion.Reservacion.Id + "; ";
                string sesionQuery = " UPDATE sesiones SET Costo='" + sesion.Costo + "', Tipo='" + sesion.Tipo + "',Fecha='" + FormatearFecha(sesion.Fecha) + "',Hora='" + sesion.Hora + "', Observaciones='" + sesion.Observaciones +
                    "',Paciente='" + sesion.Paciente + "',Psicoterapeuta='" + sesion.Psicoterapeuta + "',Estado='" + sesion.Estado + "' WHERE ID=" + sesion.Id + "; ";


                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + reservacionQuery
                                    + sesionQuery
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n Error al actualizar datos de sesion de la Base de datos");
            }
        }
        public bool CancelarSesion(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sesionQuery = " UPDATE sesiones SET Estado='Cancelada' WHERE ID=" + id + "; ";


                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + sesionQuery
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n Error al actualizar datos de sesion de la Base de datos");
            }
        }
        public SqlDataAdapter ObtenerSesionesTable()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT S.ID,  S.Tipo, E.Fecha, concat_ws(' ',P.Nombre, P.Apellidos) AS 'Paciente', S.Costo, S.Observaciones, Ps.Nombre AS 'Psicoterapeuta' FROM sesiones S INNER JOIN pacientes P ON S.Paciente_ID=P.ID INNER JOIN psicoterapeutas Ps on S.Psicoterapeuta_ID=Ps.ID WHERE S.Estado = 'Activa'", Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de sesiones de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerSesionesTable(string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT S.ID,  S.Tipo, E.Fecha, concat_ws(' ',P.Nombre, P.Apellidos) AS 'Paciente', S.Costo, S.Observaciones, Ps.Nombre AS 'Psicoterapeuta' FROM sesiones S INNER JOIN pacientes P ON S.Paciente_ID=P.ID INNER JOIN psicoterapeutas Ps on S.Psicoterapeuta_ID=Ps.ID WHERE " +
                    "(S.Tipo LIKE '%" + parameter + "%' or " +
                    " E.Fecha LIKE '%" + parameter + "%' or " +
                    " concat_ws(' ',P.Nombre, P.Apellidos) LIKE '%" + parameter + "%' or " +
                    " Ps.Nombre LIKE '%" + parameter + "%') AND " +
                    " (S.Estado = 'Activa')";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.Conn);
                this.Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de los sesiones de la base de datos");
            }
        }
        public Sesion ConsultarSesion(string ID)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT S.ID, S.Costo,S.Tipo, S.Observaciones, S.Paciente_ID, S.Psicoterapeuta_ID, S.Estado, R.ID, R.Reservante, R.Fecha,R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones, S.Fecha, S.Hora, S.Pago, S.Pendiente FROM sesiones S LEFT JOIN reservaciones R ON R.ID=S.Reservacion_ID WHERE S.ID=" + ID + ";";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    Sesion s = new Sesion
                    {
                        Id = reader.GetInt32(0),
                        Costo = reader.GetDecimal(1),
                        Tipo = reader.GetString(2),
                        Observaciones = reader.GetString(3),
                        Paciente = reader.GetInt32(4),
                        Estado = reader.GetString(6),
                        Fecha = reader.GetDateTime(18),
                        Hora = reader.GetTimeSpan(19),
                        Pago = reader.GetDecimal(20),
                        Pendiente = reader.GetDecimal(21)
                    };
                    try
                    {
                        reader.GetInt32(7);
                        Reservacion r = new Reservacion
                        {
                            Id = reader.GetInt32(7),
                            Reservante = reader.GetString(8),
                            Fecha = reader.GetDateTime(9),
                            Codigo_Reservacion = reader.GetString(10),
                            Hora_Inicio = reader.GetTimeSpan(11),
                            Duracion = reader.GetTimeSpan(12),
                            Hora_Fin = reader.GetTimeSpan(13),
                            Concepto = reader.GetString(14),
                            Id_parent = reader.GetString(15),
                            Ubicacion = reader.GetString(16),
                            Observaciones = reader.GetString(17)
                        };
                        s.Reservacion = r;
                    }
                    catch (Exception e)
                    { }
                    try
                    {
                        s.Psicoterapeuta = reader.GetString(5);
                    }
                    catch (Exception e)
                    { }
                    Conn.Close();
                    return s;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de la sesion de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerSesionesPacienteTable(string paciente)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT S.ID, S.Tipo, S.Psicoterapeuta_ID,S.Fecha, S.Hora, S.Observaciones FROM sesiones S WHERE S.Paciente_ID=" + paciente + " AND S.Estado='Activa'  ORDER BY S.Fecha ASC;";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, Conn);
                this.Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de las sesiones de la base de datos");
            }
        }
        public List<Sesion> ObtenerSesionesPaciente(string paciente)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT S.ID, S.Costo,S.Tipo, S.Observaciones, S.Paciente_ID, S.Psicoterapeuta_ID, S.Estado, R.ID, R.Reservante, R.Fecha, R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones, S.Fecha, S.Hora, S.Pago, S.Pendiente FROM sesiones S LEFT JOIN reservaciones R ON R.ID=S.Reservacion_ID WHERE S.Paciente_ID=" + paciente + ";";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<Sesion> aux = new List<Sesion>();
                while (reader.Read())
                {
                    Sesion s = new Sesion
                    {
                        Id = reader.GetInt32(0),
                        Costo = reader.GetDecimal(1),
                        Tipo = reader.GetString(2),
                        Observaciones = reader.GetString(3),
                        Paciente = reader.GetInt32(4),
                        Estado = reader.GetString(6),
                        Fecha = reader.GetDateTime(18),
                        Hora = reader.GetTimeSpan(19),
                        Pago = reader.GetDecimal(20),
                        Pendiente = reader.GetDecimal(21)
                    };
                    try
                    {
                        reader.GetInt32(7);
                        Reservacion r = new Reservacion
                        {
                            Id = reader.GetInt32(7),
                            Reservante = reader.GetString(8),
                            Fecha = reader.GetDateTime(9),
                            Codigo_Reservacion = reader.GetString(10),
                            Hora_Inicio = reader.GetTimeSpan(11),
                            Duracion = reader.GetTimeSpan(12),
                            Hora_Fin = reader.GetTimeSpan(13),
                            Concepto = reader.GetString(14),
                            Id_parent = reader.GetString(15),
                            Ubicacion = reader.GetString(16),
                            Observaciones = reader.GetString(17)
                        };
                        s.Reservacion = r;
                    }
                    catch (Exception e)
                    { }
                    try
                    {
                        s.Psicoterapeuta = reader.GetString(5);
                    }
                    catch (Exception e)
                    { }
                    aux.Add(s);
                }
                Conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de las sesiones de la base de datos");
            }
        }
        public List<Sesion> ObtenerSesionesPendietesDePagoPaciente(string paciente)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT S.ID, S.Costo,S.Tipo, S.Observaciones, S.Paciente_ID, S.Psicoterapeuta_ID, S.Estado, R.ID, R.Reservante, R.Fecha, R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones, S.Fecha, S.Hora, S.Pago, S.Pendiente FROM sesiones S LEFT JOIN reservaciones R ON R.ID=S.Reservacion_ID WHERE S.Paciente_ID=" + paciente + " AND S.Pendiente > 0.0 ORDER BY S.Fecha DESC;";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<Sesion> aux = new List<Sesion>();
                while (reader.Read())
                {
                    Sesion s = new Sesion
                    {
                        Id = reader.GetInt32(0),
                        Costo = reader.GetDecimal(1),
                        Tipo = reader.GetString(2),
                        Observaciones = reader.GetString(3),
                        Paciente = reader.GetInt32(4),
                        Estado = reader.GetString(6),
                        Fecha = reader.GetDateTime(18),
                        Hora = reader.GetTimeSpan(19),
                        Pago = reader.GetDecimal(20),
                        Pendiente = reader.GetDecimal(21)
                    };
                    try
                    {
                        reader.GetInt32(7);
                        Reservacion r = new Reservacion
                        {
                            Id = reader.GetInt32(7),
                            Reservante = reader.GetString(8),
                            Fecha = reader.GetDateTime(9),
                            Codigo_Reservacion = reader.GetString(10),
                            Hora_Inicio = reader.GetTimeSpan(11),
                            Duracion = reader.GetTimeSpan(12),
                            Hora_Fin = reader.GetTimeSpan(13),
                            Concepto = reader.GetString(14),
                            Id_parent = reader.GetString(15),
                            Ubicacion = reader.GetString(16),
                            Observaciones = reader.GetString(17)
                        };
                        s.Reservacion = r;
                    }
                    catch (Exception e)
                    { }
                    try
                    {
                        s.Psicoterapeuta = reader.GetString(5);
                    }
                    catch (Exception e)
                    { }
                    aux.Add(s);
                }
                Conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de las sesiones de la base de datos");
            }
        }
        public bool RegistrarPagoDeSesion(Pago pago, List<Sesion> sesionesPagadas)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sesionesPagadasQuery = "";
                foreach (Sesion aux in sesionesPagadas)
                {
                    sesionesPagadasQuery += "UPDATE sesiones SET Pago = " + aux.Pago + ", Pendiente=" + aux.Pendiente + " WHERE ID = " + aux.Id + "; ";
                }
                string agregarPago = "INSERT INTO pagos (Emisor, FechaPago, Cantidad, Concepto, Area, Observaciones, Recibio, Parent_ID) VALUES ('"
                    + pago.Emisor + "', '" + FormatearFecha(pago.FechaPago) + "'," + pago.Cantidad + ", 'Pago de Sesión', 'Psicoterapia', '"
                    + pago.Observaciones + "', '" + pago.Recibio + "', '" + sesionesPagadas.ElementAt(0).Paciente + "');";


                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + sesionesPagadasQuery
                                    + agregarPago
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar Pago de sesiones a la Base de datos");
            }
        }
        public SqlDataAdapter ObtenerPagosPacienteTable(string paciente)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT ID, Emisor, FechaPago AS 'Fecha De Pago', Cantidad, Observaciones, Recibio FROM pagos WHERE Concepto='Pago de sesion' AND Area='Psicoterapia' AND Parent_ID='" + paciente + "';", Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de los pagos de sesiones de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerPagosPacienteTable(string paciente, string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT ID, Emisor, FechaPago AS 'Fecha De Pago', Cantidad, Observaciones, Recibio FROM pagos WHERE Concepto='Pago de sesion' AND Area='Psicoterapia' AND Parent_ID='" + paciente + "' AND " +
                    "(Emisor LIKE '%" + parameter + "%' or " +
                    "Cantidad LIKE '%" + parameter + "%' or " +
                    "FechaPago LIKE '%" + parameter + "%' or " +
                    "Recibio LIKE '%" + parameter + "%')";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de los pagos de sesiones de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerPagosSesionesPacienteTable(string paciente)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT S.ID, S.Fecha, S.Hora, S.Costo, S.Pago, S.Pendiente FROM sesiones S WHERE S.Paciente_ID=" + paciente + " AND S.Estado='Activa'  ORDER BY S.Fecha ASC;", Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de sesiones de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerPagosSesionesPacienteTable(string paciente, string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT S.ID, S.Fecha, S.Hora, S.Costo, S.Pago, S.Pendiente FROM sesiones S WHERE S.Paciente_ID=" + paciente + " AND S.Estado='Activa' AND " +
                    "(S.ID LIKE '%" + parameter + "%' or " +
                    "S.Fecha LIKE '%" + parameter + "%' or " +
                    "S.Psicoterapeuta_ID LIKE '%" + parameter + "%')  ORDER BY S.Fecha ASC;";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de sesiones de la base de datos");
            }
        }
        
        //---------------------------EVALUACIONES------------------------//
        public bool AgregarEvaluacion(Evaluacion evaluacion)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string reservacionQuery = "";
                string evaluacionQuery = "";
                if (evaluacion.Reservacion != null)
                {
                    reservacionQuery = "UPDATE reservaciones SET ID_Parent = (SELECT AUTO_INCREMENT FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '" + this.Database + "' AND TABLE_NAME = 'sesiones') WHERE ID=" + evaluacion.Reservacion.Id + ";";
                    evaluacionQuery = " INSERT INTO evaluaciones (Reservacion_ID, Costo, pruebas, Observaciones, Paciente_ID, Psicoterapeuta_ID, Estado) VALUES ('"
                        + evaluacion.Reservacion.Id + "','" + evaluacion.Costo + "','" + evaluacion.Pruebas + "','" + evaluacion.Observaciones +
                        "','" + evaluacion.Paciente + "','" + evaluacion.Psicoterapeuta + "','Activa');";
                }
                else
                {
                    evaluacionQuery = " INSERT INTO evaluaciones ( Paciente_ID, Psicoterapeuta_ID, Fecha, Hora, Observaciones, Pruebas, Costo, Estado)"
                        + " VALUES ('" + evaluacion.Paciente + "','" + evaluacion.Psicoterapeuta + "','" + FormatearFecha(evaluacion.Fecha) + "','" + evaluacion.Hora + "','" + evaluacion.Observaciones + "','" + evaluacion.Pruebas + "','" + evaluacion.Costo + "','Activa');";
                }


                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + reservacionQuery
                                    + evaluacionQuery
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar datos de sesión de la Base de datos");
            }
        }
        public bool ActualizarEvaluacion(Evaluacion evaluacion)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string reservacionQuery = "";
                if (evaluacion.Reservacion != null)
                    reservacionQuery = "UPDATE reservaciones SET Reservante='" + evaluacion.Reservacion.Reservante + "', Fecha='" + FormatearFecha(evaluacion.Reservacion.Fecha) +
                        "Codigo_Reservacion='" + evaluacion.Reservacion.Codigo_Reservacion + "', Hora_Inicio='" + evaluacion.Reservacion.Hora_Inicio + "', Duracion='" + evaluacion.Reservacion.Duracion +
                       "', Hora_Fin='" + evaluacion.Reservacion.Hora_Fin + "', Concepto='" + evaluacion.Reservacion.Concepto + "', ID_Parent='" + evaluacion.Id +
                       "', Ubicacion='" + evaluacion.Reservacion.Ubicacion + "', Observaciones='" + evaluacion.Reservacion.Observaciones +
                       "' WHERE ID=" + evaluacion.Reservacion.Id + "; ";
                string evaluacionQuery = " UPDATE evaluaciones SET Costo='" + evaluacion.Costo + "', Pruebas='" + evaluacion.Pruebas + "', Observaciones='" + evaluacion.Observaciones +
                   "', Paciente_ID'" + evaluacion.Paciente + "', Psicoterapeuta_ID'" + evaluacion.Psicoterapeuta + "',Fecha='" + FormatearFecha(evaluacion.Fecha) + "',Hora='" + evaluacion.Hora + "', Estado='Activa' WHERE ID=" + evaluacion.Id + "; ";


                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + reservacionQuery
                                    + evaluacionQuery
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n Error al actualizar datos de sesion de la Base de datos");
            }
        }
        public bool CancelarEvaluacion(Evaluacion evaluacion)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string reservacionQuery = "DELETE FROM reservaciones WHERE ID=" + evaluacion.Reservacion.Id + "; ";
                string evaluacionQuery = " UPDATE evaluacion SET Estado='Cancelada' WHERE ID=" + evaluacion.Id + "; ";


                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + reservacionQuery
                                    + evaluacionQuery
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n Error al actualizar datos de sesion de la Base de datos");
            }
        }
        public SqlDataAdapter ObtenerEvaluacionTable()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT E.ID, E.Pruebas, E.Fecha, concat_ws(' ',P.Nombre, P.Apellidos) AS 'Paciente', E.Costo, E.Observaciones, Ps.Nombre AS 'Psicoterapeuta' FROM evaluaciones E INNER JOIN pacientes P ON E.Paciente_ID=P.ID INNER JOIN psicoterapeutas Ps on E.Psicoterapeuta_ID=Ps.ID WHERE E.Estado = 'Activa';", Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de evaluaciones de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerEvaluacionTable(string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT E.ID, E.Pruebas, E.Fecha, concat_ws(' ',P.Nombre, P.Apellidos) AS 'Paciente', E.Costo, E.Observaciones, Ps.Nombre AS 'Psicoterapeuta' FROM evaluaciones E INNER JOIN pacientes P ON E.Paciente_ID=P.ID INNER JOIN psicoterapeutas Ps on E.Psicoterapeuta_ID=Ps.ID WHERE " +
                    "(E.Pruebas LIKE '%" + parameter + "%' or " +
                    " E.Fecha LIKE '%" + parameter + "%' or " +
                    " concat_ws(' ',P.Nombre, P.Apellidos) LIKE '%" + parameter + "%' or " +
                    " Ps.Nombre LIKE '%" + parameter + "%') AND " +
                    " (E.Estado = 'Activa')";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.Conn);
                this.Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de los evaluaciones de la base de datos");
            }
        }
        public Evaluacion ConsultarEvaluacion(string ID)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT E.ID, E.Costo, E.Pruebas, E.Observaciones, E.Paciente_ID, E.Psicoterapeuta_ID, E.Estado, R.ID, R.Reservante, R.Fecha, R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones, E.Fecha, E.Hora FROM evaluaciones E LEFT JOIN reservaciones R ON R.ID=E.Reservacion_ID WHERE E.ID=" + ID + ";";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    Evaluacion s = new Evaluacion
                    {
                        Id = reader.GetInt32(0),
                        Costo = reader.GetDecimal(1),
                        Pruebas = reader.GetString(2),
                        Observaciones = reader.GetString(3),
                        Paciente = reader.GetInt32(4),
                        Estado = reader.GetString(6),
                        Fecha = reader.GetDateTime(18),
                        Hora = reader.GetTimeSpan(19)
                    };
                    try
                    {
                        reader.GetInt32(7);
                        Reservacion r = new Reservacion
                        {
                            Id = reader.GetInt32(7),
                            Reservante = reader.GetString(8),
                            Fecha = reader.GetDateTime(9),
                            Codigo_Reservacion = reader.GetString(10),
                            Hora_Inicio = reader.GetTimeSpan(11),
                            Duracion = reader.GetTimeSpan(12),
                            Hora_Fin = reader.GetTimeSpan(13),
                            Concepto = reader.GetString(14),
                            Id_parent = reader.GetString(15),
                            Ubicacion = reader.GetString(16),
                            Observaciones = reader.GetString(17)
                        };
                        s.Reservacion = r;
                    }
                    catch (Exception e)
                    { }
                    try
                    {
                        s.Psicoterapeuta = reader.GetString(5);
                    }
                    catch (Exception e)
                    { }
                    Conn.Close();
                    return s;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de la evaluacion de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerEvaluacionPacienteTable(string paciente)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT E.ID, E.Pruebas, E.Fecha, E.Observaciones, Ps.Nombre AS 'Psicoterapeuta' FROM evaluaciones E INNER JOIN pacientes P ON E.Paciente_ID=P.ID INNER JOIN psicoterapeutas Ps on E.Psicoterapeuta_ID=Ps.ID WHERE E.Paciente_ID=" + paciente + " AND E.Estado = 'Activa';";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.Conn);
                this.Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de los evaluaciones de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerEvaluacionPacienteTable(string paciente, string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT E.ID, E.Pruebas, E.Fecha, E.Observaciones, Ps.Nombre AS 'Psicoterapeuta' FROM evaluaciones E INNER JOIN pacientes P ON E.Paciente_ID=P.ID INNER JOIN psicoterapeutas Ps on E.Psicoterapeuta_ID=Ps.Matricula WHERE E.Paciente_ID=" + paciente + " AND E.Estado = 'Activa' AND (" +
                    "E.ID LIKE '%" + parameter + "%' or " +
                    "E.Fecha LIKE '%" + parameter + "%' or " +
                    "E.Pruebas LIKE '%" + parameter + "%' or " +
                    "E.Observaciones LIKE '%" + parameter + "%' or " +
                    "Ps.Nombre LIKE '%" + parameter + "%');";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.Conn);
                this.Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de los evaluaciones de la base de datos");
            }
        }
        public List<Evaluacion> ObtenerEvaluacionPaciente(string paciente)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT E.ID, E.Costo, E.Pruebas, E.Observaciones, E.Paciente_ID, E.Psicoterapeuta_ID, E.Estado, R.ID, R.Reservante, R.Fecha,R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones, E.Fecha, E.Hora FROM evaluaciones E LEFT JOIN reservaciones R ON R.ID=E.Reservacion_ID WHERE E.Paciente_ID=" + paciente + ";";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<Evaluacion> aux = new List<Evaluacion>();
                while (reader.Read())
                {
                    Evaluacion s = new Evaluacion
                    {
                        Id = reader.GetInt32(0),
                        Costo = reader.GetDecimal(1),
                        Pruebas = reader.GetString(2),
                        Observaciones = reader.GetString(3),
                        Paciente = reader.GetInt32(4),
                        Estado = reader.GetString(6),
                        Fecha = reader.GetDateTime(18),
                        Hora = reader.GetTimeSpan(19)
                    };
                    try
                    {
                        reader.GetInt32(7);
                        Reservacion r = new Reservacion
                        {
                            Id = reader.GetInt32(7),
                            Reservante = reader.GetString(8),
                            Fecha = reader.GetDateTime(9),
                            Codigo_Reservacion = reader.GetString(10),
                            Hora_Inicio = reader.GetTimeSpan(11),
                            Duracion = reader.GetTimeSpan(12),
                            Hora_Fin = reader.GetTimeSpan(13),
                            Concepto = reader.GetString(14),
                            Id_parent = reader.GetString(15),
                            Ubicacion = reader.GetString(16),
                            Observaciones = reader.GetString(17)
                        };
                        s.Reservacion = r;
                    }
                    catch (Exception e)
                    { }
                    try
                    {
                        s.Psicoterapeuta = reader.GetString(5);
                    }
                    catch (Exception e)
                    { }
                    aux.Add(s);
                }
                Conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de las evaluaciones de la base de datos");
            }
        }

        //-------------------------------CLUB DE TAREAS-------------------------------//
        public bool AgregarClubDeTareas(ClubDeTareas club)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string reservacionQuery = "";
                string clubTareasQuery = "";
                if (club.reservacion != null)
                {
                    reservacionQuery = "UPDATE reservaciones SET ID_Parent = (SELECT AUTO_INCREMENT FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '" + this.Database + "' AND TABLE_NAME = 'clubDeTareas') WHERE ID=" + club.reservacion.Id + ";";
                    clubTareasQuery = "INSERT INTO clubDeTareas (Reservacion_ID, Fecha, Hora, Observaciones, Costo, Psicoterapeuta, Estado) VALUES('"
                     + club.reservacion.Id + "','" + FormatearFecha(club.Fecha) + "','" + club.Hora + "','" + club.Observaciones + "','" + club.Costo + "','" + club.Encargado + "','Activo');";
                }
                else
                {
                    clubTareasQuery = "INSERT INTO clubDeTareas (Fecha, Hora, Observaciones, Costo, Psicoterapeuta, Estado) VALUES("
                    + " ' " + FormatearFecha(club.Fecha) + "','" + club.Hora + "','" + club.Observaciones + "','" + club.Costo + "','" + club.Encargado + "','Activo');";
                }


                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + reservacionQuery
                                    + clubTareasQuery
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar el club a la Base de datos");
            }
        }
        public bool ActualizarClubDeTareas(ClubDeTareas club)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string reservacionQuery = "";
                if (club.reservacion != null)
                    reservacionQuery = "UPDATE reservaciones SET Reservante='" + club.reservacion.Reservante + "', Fecha='" + FormatearFecha(club.reservacion.Fecha) +
                        "Codigo_Reservacion='" + club.reservacion.Codigo_Reservacion + "', Hora_Inicio='" + club.reservacion.Hora_Inicio + "', Duracion='" + club.reservacion.Duracion +
                       "', Hora_Fin='" + club.reservacion.Hora_Fin + "', Concepto='" + club.reservacion.Concepto + "', ID_Parent='" + club.ID +
                       "', Ubicacion='" + club.reservacion.Ubicacion + "', Observaciones='" + club.reservacion.Observaciones +
                       "' WHERE ID=" + club.reservacion.Id + "; ";
                string update = "UPDATE clubDeTareas SET Fecha='" + FormatearFecha(club.Fecha) + "', Hora='" + club.Hora +
                    "', Costo='" + club.Costo + "', Psicoterapeuta='" + club.Encargado +
                    "', Observaciones='" + club.Observaciones + "', Estado='Activo' WHERE ID='" + club.ID + "';";



                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + reservacionQuery
                                    + update
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error...!\n Error al actualizar el club de la Base de datos");
            }
        }
        public bool CancelarClubDeTareas(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "UPDATE clubDeTareas SET Estado='Cancelado' WHERE ID='" + id + "';";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
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
        public SqlDataAdapter ObtenerClubDeTareasTable()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT C.ID, C.Fecha, C.Hora, C.Costo, P.Nombre, C.Observaciones FROM clubDeTareas C INNER JOIN empleados P ON P.ID=C.Psicoterapeuta WHERE C.Estado='Activo'", Conn); Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de clubes de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerClubDeTareasTable(string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT C.ID, C.Fecha, C.Hora, C.Costo, P.Nombre, C.Observaciones FROM clubDeTareas C INNER JOIN empleados P ON P.ID=C.Psicoterapeuta WHERE " +
                    "(C.ID LIKE '%" + parameter + "%' or " +
                    " P.Nombre LIKE '%" + parameter + "%' or " +
                    " C.Fecha LIKE '%" + parameter + "%') AND C.Estado='Activo'";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.Conn);
                this.Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de clubes de tarea de la base de datos");
            }
        }
        public ClubDeTareas ConsultarClubDeTareas(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT C.ID, C.Fecha, C.Hora, C.Costo, C.Psicoterapeuta, C.Observaciones, C.Estado ,R.ID, R.Reservante, R.Fecha, R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones FROM clubDeTareas C LEFT JOIN reservaciones R ON R.ID=C.Reservacion_ID WHERE C.ID='" + id + "'";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    ClubDeTareas c = new ClubDeTareas
                    {
                        ID = reader.GetInt32(0),
                        Fecha = reader.GetDateTime(1),
                        Hora = reader.GetTimeSpan(2),
                        Costo = reader.GetDecimal(3),
                        Observaciones = reader.GetString(5),
                        Estado = reader.GetString(6)
                    };
                    try
                    {
                        reader.GetInt32(7);
                        Reservacion r = new Reservacion
                        {
                            Id = reader.GetInt32(7),
                            Reservante = reader.GetString(8),
                            Fecha = reader.GetDateTime(9),
                            Codigo_Reservacion = reader.GetString(10),
                            Hora_Inicio = reader.GetTimeSpan(11),
                            Duracion = reader.GetTimeSpan(12),
                            Hora_Fin = reader.GetTimeSpan(13),
                            Concepto = reader.GetString(14),
                            Id_parent = reader.GetString(15),
                            Ubicacion = reader.GetString(16),
                            Observaciones = reader.GetString(17)
                        };
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
                    Conn.Close();
                    return c;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos del grupo de la base de datos");
            }
        }
        public ClubDeTareasAsistente ObtenerAsistenteClubDeTareas(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT Club_Tareas_ID, Nombre, Apellidos, Nombre_Tutor, Telefono_Tutor, Costo, Pago, Observaciones FROM ClubDeTareasAsistente WHERE ID='" + id + "';";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    ClubDeTareasAsistente a = new ClubDeTareasAsistente
                    {
                        ID = Convert.ToInt32(id),
                        Club_Tareas_ID = reader.GetInt32(0),
                        Nombres = reader.GetString(1),
                        Apellidos = reader.GetString(2),
                        NombreTutor = reader.GetString(3),
                        TelefonoTutor = reader.GetString(4),
                        Costo = reader.GetDecimal(5),
                        Pago = reader.GetDecimal(6)
                    };
                    a.Restante = a.Costo - a.Pago;
                    a.Observaciones = reader.GetString(7);
                    Conn.Close();
                    return a;
                }
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de los asistentes del club de la base de datos");
            }
        }
        public List<ClubDeTareasAsistente> ObtenerAsistentesClubDeTareas(string club)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT ID, Club_Tareas_ID, Nombre, Apellidos, Nombre_Tutor, Telefono_Tutor, Costo, Pago, Observaciones FROM ClubDeTareasAsistente WHERE Club_Tareas_ID='" + club + "';";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<ClubDeTareasAsistente> aux = new List<ClubDeTareasAsistente>();
                while (reader.Read())
                {
                    ClubDeTareasAsistente a = new ClubDeTareasAsistente
                    {
                        ID = reader.GetInt32(0),
                        Club_Tareas_ID = reader.GetInt32(1),
                        Nombres = reader.GetString(2),
                        Apellidos = reader.GetString(3),
                        NombreTutor = reader.GetString(4),
                        TelefonoTutor = reader.GetString(5),
                        Costo = reader.GetDecimal(6),
                        Pago = reader.GetDecimal(7)
                    };
                    a.Restante = a.Costo - a.Pago;
                    a.Observaciones = reader.GetString(8);
                    aux.Add(a);
                }
                Conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de los asistentes del club de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerAsistentesClubDeTareasTable(string club)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT ID, Club_Tareas_ID, Nombre, Apellidos, Nombre_Tutor, Telefono_Tutor, Costo, Pago, Observaciones FROM ClubDeTareasAsistente WHERE Club_Tareas_ID='" + club + "'", Conn); Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de clubes de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerAsistentesClubDeTareasTable(string club, string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT ID, Club_Tareas_ID, Nombre, Apellidos, Nombre_Tutor, Telefono_Tutor, Costo, Pago, Observaciones FROM ClubDeTareasAsistente WHERE Club_Tareas_ID='" + club + "' AND " +
                    "(Nombre LIKE '%" + parameter + "%' or " +
                    " Apellidos LIKE '%" + parameter + "%' or " +
                    " Nombre_Tutor LIKE '%" + parameter + "%' or " +
                    " Telefono_Tutor LIKE '%" + parameter + "%');";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.Conn);
                this.Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos del club de tareas de la base de datos");
            }
        }
        public List<ClubDeTareas> ObtenerClubDeTareas()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT C.ID, C.Fecha, C.Hora, C.Costo, C.Psicoterapeuta, C.Observaciones, C.Estado ,R.ID, R.Reservante, R.Fecha, R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones FROM clubDeTareas C LEFT JOIN reservaciones R ON R.ID=C.Reservacion_ID WHERE C.Estado='Activo'";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<ClubDeTareas> aux = new List<ClubDeTareas>();
                while (reader.Read())
                {
                    ClubDeTareas c = new ClubDeTareas
                    {
                        ID = reader.GetInt32(0),
                        Fecha = reader.GetDateTime(1),
                        Hora = reader.GetTimeSpan(2),
                        Costo = reader.GetDecimal(3),
                        Observaciones = reader.GetString(5),
                        Estado = reader.GetString(6)
                    };
                    try
                    {
                        reader.GetInt32(7);
                        Reservacion r = new Reservacion
                        {
                            Id = reader.GetInt32(7),
                            Reservante = reader.GetString(8),
                            Fecha = reader.GetDateTime(9),
                            Codigo_Reservacion = reader.GetString(10),
                            Hora_Inicio = reader.GetTimeSpan(11),
                            Duracion = reader.GetTimeSpan(12),
                            Hora_Fin = reader.GetTimeSpan(13),
                            Concepto = reader.GetString(14),
                            Id_parent = reader.GetString(15),
                            Ubicacion = reader.GetString(16),
                            Observaciones = reader.GetString(17)
                        };
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
                Conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de los clubes de la base de datos");
            }
        }
        public bool RegistrarAsistenteClubDeTareas(ClubDeTareasAsistente asistente)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
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


                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + inscribir
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar asistencia a la Base de datos");
            }
        }
        public bool BorrarAsistenteClubDeTareas(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string borrarAsistente = "DELETE FROM clubDeTareasAsistentes WHERE ID=" + id + "; ";


                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + borrarAsistente
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n Error al borrar asistencia a la Base de datos");
            }
        }
        public bool RegistrarPagoAsistenciaClubDeTareas(Pago pago, string idAsistente)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string updateAsistente = "UPDATE cluDeTareasAsistentes SET Pago = Pago + " + pago.Cantidad +
                                            " WHERE ID = " + idAsistente + ";";
                string agregarPago = "INSERT INTO pagos (Emisor, FechaPago, Cantidad, Concepto, Area, Observaciones, Recibio, Parent_ID) VALUES ('"
                    + pago.Emisor + "', '" + FormatearFecha(pago.FechaPago) + "'," + pago.Cantidad + ", 'Pago de Club De Tareas', 'Psicoterapia','"
                    + pago.Observaciones + "', '" + pago.Recibio + "', '" + idAsistente + "');";


                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + updateAsistente
                                    + agregarPago
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar Pago de club a la Base de datos");
            }
        }

        //-------------------------------PAGOS--------------------------------------//
        public bool AgregarPago(Pago pago)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "INSERT INTO pagos (Emisor, FechaPago, Cantidad, Concepto, Area, Observaciones, Recibio, Parent_ID) VALUES ('"
                    + pago.Emisor + "', '" + FormatearFecha(pago.FechaPago) + "'," + pago.Cantidad + ", '" + pago.Concepto + "', '" + pago.Area + "', '"
                    + pago.Observaciones + "', '" + pago.Recibio + "', '" + pago.Parent_id + "')";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception E)
            {
                Conn.Close();
                throw new Exception("Error al agregar el pago a la base de datos");
            }
        }
        public SqlDataAdapter ObtenerPagosTable(string area)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT ID, Emisor, FechaPago AS 'Fecha De Pago', Cantidad, Concepto, Observaciones, Recibio  FROM pagos WHERE Area='" + area + "'", Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de los pagos de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerPagosTable(string parameter, string area)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT ID, Emisor, FechaPago AS 'Fecha De Pago', Cantidad, Concepto, Observaciones, Recibio FROM pagos WHERE" +
                    "(Emisor LIKE '%" + parameter + "%' or " +
                    "Cantidad LIKE '%" + parameter + "%' or " +
                    "Concepto LIKE '%" + parameter + "%' or " +
                    "FechaPago LIKE '%" + parameter + "%' or " +
                    "Recibio LIKE '%" + parameter + "%') AND Area='" + area + "'";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de los pagos de la base de datos");
            }
        }
        public Pago ConsultarPago(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT ID, Emisor, FechaPago, Cantidad, Concepto, Area, Parent_ID, Observaciones, Recibio, Estado FROM pagos WHERE ID='" + id + "'";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    Pago pago = new Pago
                    {
                        Id = reader.GetInt32(0),
                        Emisor = reader.GetString(1),
                        FechaPago = reader.GetDateTime(2),
                        Cantidad = reader.GetInt32(3),
                        Concepto = reader.GetString(4),
                        Area = reader.GetString(5),
                        Parent_id = reader.GetInt32(6),
                        Observaciones = reader.GetString(7),
                        Recibio = reader.GetString(8),
                        Estado = reader.GetString(9)
                    };
                    Conn.Close();
                    return pago;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener los datos del pago de la base de datos");
            }
        }
        public int ObtenerUltimoIDPagos()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT AUTO_INCREMENT FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '" + this.Database + "' AND TABLE_NAME = 'pagos'; ";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    Conn.Close();
                    return id;
                }
                Conn.Close();
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener id de pagos de la base de datos");
            }
        }
        public bool CancelarPago(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "UPDATE pagos SET Estado = 'Cancelado' WHERE ID=" + id;
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
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
        public bool AgregarReservacion(Reservacion reservacion)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string reservacionQuery = "INSERT INTO reservaciones (Reservante, Fecha, Codigo_Reservacion, Hora_Inicio, Duracion, Hora_Fin, Concepto, ID_Parent, Ubicacion, Observaciones) VALUES ('"
                    + reservacion.Reservante + "','" + FormatearFecha(reservacion.Fecha) + "','" + reservacion.Codigo_Reservacion + "','" + reservacion.Hora_Inicio + "','" + reservacion.Duracion + "','" + reservacion.Hora_Fin + "','" + reservacion.Concepto + "','" + reservacion.Id_parent + "','" + reservacion.Ubicacion + "','" + reservacion.Observaciones + "'); ";


                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + reservacionQuery
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
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
        public bool ActualizarReservacion(Reservacion reservacion)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string reservacionQuery = "UPDATE reservaciones SET Reservante='" + reservacion.Reservante + "', Fecha='" + FormatearFecha(reservacion.Fecha) +
                    "Codigo_Reservacion='" + reservacion.Codigo_Reservacion + "', Hora_Inicio='" + reservacion.Hora_Inicio + "', Duracion='" + reservacion.Duracion +
                   "', Hora_Fin='" + reservacion.Hora_Fin + "', Concepto='" + reservacion.Concepto + "', ID_Parent='" + reservacion.Id_parent +
                   "', Ubicacion='" + reservacion.Ubicacion + "', Observaciones='" + reservacion.Observaciones +
                   "' WHERE ID=" + reservacion.Id + "; ";


                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + reservacionQuery
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
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
        public bool CancelarReservacion(int id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string reservacionQuery = "DELETE FROM reservaciones WHERE ID=" + id + "; ";


                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + reservacionQuery
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n Error al actualizar datos de reservación de la Base de datos");
            }
        }
        public bool CancelarReservacion(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string reservacionQuery = "DELETE FROM reservaciones WHERE Codigo_Reservacion=" + id + "; ";


                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + reservacionQuery
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n Error al actualizar datos de reservación de la Base de datos");
            }
        }
        public Reservacion ConsultarReservacion(int ID)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT R.ID, R.Reservante, R.Fecha, R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones FROM reservaciones R WHERE R.ID=" + ID + ";";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {

                    Reservacion r = new Reservacion
                    {
                        Id = reader.GetInt32(0),
                        Reservante = reader.GetString(1),
                        Fecha = reader.GetDateTime(2),
                        Codigo_Reservacion = reader.GetString(3),
                        Hora_Inicio = reader.GetTimeSpan(4),
                        Duracion = reader.GetTimeSpan(5),
                        Hora_Fin = reader.GetTimeSpan(6),
                        Concepto = reader.GetString(7),
                        Id_parent = reader.GetString(8),
                        Ubicacion = reader.GetString(9),
                        Observaciones = reader.GetString(10)
                    };
                    Conn.Close();
                    return r;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de la reservación de la base de datos");
            }
        }
        public Reservacion ConsultarReservacion(TimeSpan hora, DateTime fecha, string psicoterapeuta)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT R.ID, R.Reservante, R.Fecha, R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones FROM reservaciones R WHERE R.Reservante='" + psicoterapeuta + "' AND R.Fecha='" + FormatearFecha(fecha) + "' AND R.Hora_Inicio='" + hora + "';";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {

                    Reservacion r = new Reservacion
                    {
                        Id = reader.GetInt32(0),
                        Reservante = reader.GetString(1),
                        Fecha = reader.GetDateTime(2),
                        Codigo_Reservacion = reader.GetString(3),
                        Hora_Inicio = reader.GetTimeSpan(4),
                        Duracion = reader.GetTimeSpan(5),
                        Hora_Fin = reader.GetTimeSpan(6),
                        Concepto = reader.GetString(7),
                        Id_parent = reader.GetString(8),
                        Ubicacion = reader.GetString(9),
                        Observaciones = reader.GetString(10)
                    };
                    Conn.Close();
                    return r;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de la reservación de la base de datos");
            }
        }
        public Reservacion ConsultarReservacion(string ID)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT R.ID, R.Reservante, R.Fecha, R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones FROM reservaciones R WHERE R.Codigo_Reservacion=" + ID + ";";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {

                    Reservacion r = new Reservacion
                    {
                        Id = reader.GetInt32(0),
                        Reservante = reader.GetString(1),
                        Fecha = reader.GetDateTime(2),
                        Codigo_Reservacion = reader.GetString(3),
                        Hora_Inicio = reader.GetTimeSpan(4),
                        Duracion = reader.GetTimeSpan(5),
                        Hora_Fin = reader.GetTimeSpan(6),
                        Concepto = reader.GetString(7),
                        Id_parent = reader.GetString(8),
                        Ubicacion = reader.GetString(9),
                        Observaciones = reader.GetString(10)
                    };
                    Conn.Close();
                    return r;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de la reservación de la base de datos");
            }
        }
        public List<Reservacion> ObtenerReservaciones(DateTime fecha)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT R.ID, R.Reservante, R.Fecha, R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones FROM reservaciones R WHERE R.Fecha ='" + FormatearFecha(fecha).Substring(0, 10) + "';";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<Reservacion> aux = new List<Reservacion>();
                while (reader.Read())
                {
                    Reservacion r = new Reservacion
                    {
                        Id = reader.GetInt32(0),
                        Reservante = reader.GetString(1),
                        Fecha = reader.GetDateTime(2),
                        Codigo_Reservacion = reader.GetString(3),
                        Hora_Inicio = reader.GetTimeSpan(4),
                        Duracion = reader.GetTimeSpan(5),
                        Hora_Fin = reader.GetTimeSpan(6),
                        Concepto = reader.GetString(7),
                        Id_parent = reader.GetString(8),
                        Ubicacion = reader.GetString(9),
                        Observaciones = reader.GetString(10)
                    };
                    aux.Add(r);
                }
                Conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de las reservaciones de la base de datos");
            }
        }
        public int ObtenerUltimoIDReservaciones()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT AUTO_INCREMENT FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '" + this.Database + "' AND TABLE_NAME = 'reservaciones'; ";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    Conn.Close();
                    return id;
                }
                Conn.Close();
                return 0;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener id de pagos de la base de datos");
            }
        }

        //-------------------------------LIBROS-------------------------------//
        public bool AgregarLibro(Libro libro)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string agregar = "INSERT INTO libro (Titulo, Autor, Editorial, Precio_Base) VALUES('"
                        + libro.Titulo + "','" + libro.Autor + "','" + libro.Editorial + "','" + libro.Precio_base + "');";
                string stock = "INSERT INTO stock_Libros (Libro, Vitrina_1, Vitrina_2, Almacen) VALUES (" +
                        "(select TOP 1 ID from libro ORDER BY ID DESC), '" + libro.Stock_vitrina_1 + "', '" + libro.Stock_vitrina_2 + "', '" + libro.Stock_almacen + "'); ";

                Cmd.CommandText = "BEGIN TRANSACTION; "
                                    + agregar
                                    + stock
                                    + "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("ERROR...! \n\n Error al agregar la libro a la Base de datos");
            }

        }
        public bool ActualizarLibro(Libro libro)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string update_libro = "UPDATE libro SET " +
                        "Titulo='" + libro.Titulo + "', Autor='" + libro.Autor + 
                        "', Editorial='" + libro.Editorial + "', Precio_Base='" + libro.Precio_base +
                        "' WHERE ID=" + libro.Id + ";";
                string update_stock = "UPDATE stock_Libros SET " +
                        "Vitrina_1='" + libro.Stock_vitrina_1 + 
                        "', Vitrina_2='" + libro.Stock_vitrina_2 + 
                        "', Almacen='" + libro.Stock_almacen + 
                        "' WHERE Libro=" + libro.Id + ";";
                Cmd.CommandText = update_libro + update_stock;
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error...!\n Error al actualizar la libro a la Base de datos");
            }

        }
        public bool EliminarLibro(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "UPDATE libro SET Activo=0 WHERE ID=" + id + ";";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error...!\n Error al eliminar libro a la Base de datos");
            }
        }
        public SqlDataAdapter ObtenerLibrosTable()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT L.ID, L.Titulo, L.Autor,L.Editorial,L.Precio_Base AS 'Precio', S.Vitrina_1 AS 'Vitrina 1', S.Vitrina_2 AS 'Vitrina 2', S.Almacen,  (S.Vitrina_1 + S.Vitrina_2 + S.Almacen) AS 'Stock Total' FROM libro L LEFT JOIN stock_Libros S ON L.ID = S.Libro WHERE L.Activo=1 ORDER BY L.Titulo ASC", Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de las materias de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerLibrosTable(string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT L.ID, L.Titulo, L.Autor,L.Editorial,L.Precio_Base AS 'Precio', S.Vitrina_1 AS 'Vitrina 1', S.Vitrina_2 AS 'Vitrina 2',(S.Vitrina_1 + S.Vitrina_2 + S.Almacen) AS 'Stock Total'  FROM libro L LEFT JOIN stock_Libros S ON L.ID = S.Libro" +
                    " WHERE " +
                    "(L.Titulo LIKE '%" + parameter + "%' or " +
                    " L.Autor LIKE '%" + parameter + "%' or " +
                    " L.Editorial LIKE '%" + parameter + "%' or " +
                    " L.Precio_Base LIKE '%" + parameter + "%' or " +
                    " S.Vitrina_1 LIKE '%" + parameter + "%' or " +
                    " 'Stock Total' LIKE '%" + parameter + "%' or " +
                    " S.Vitrina_2 LIKE '%" + parameter + "%') AND L.Activo=1 ORDER BY L.Titulo ASC";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.Conn);
                this.Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de las materias de la base de datos");
            }
        }
        public Libro ConsultarLibro(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT L.ID, L.Titulo, L.Autor,L.Editorial,L.Precio_Base AS 'Precio', S.Vitrina_1, S.Vitrina_2, S.Almacen FROM libro L LEFT JOIN stock_Libros S ON L.ID = S.Libro WHERE L.ID='" + id + "'";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    Libro a = new Libro
                    {
                        Id = reader.GetInt32(0),
                        Titulo = reader.GetString(1),
                        Autor = reader.GetString(2),
                        Editorial = reader.GetString(3),
                        Precio_base = reader.GetDecimal(4),
                        Stock_vitrina_1 = reader.GetInt32(5),
                        Stock_vitrina_2 = reader.GetInt32(6),
                        Stock_almacen = reader.GetInt32(7)
                    };
                    Conn.Close();
                    return a;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos del libro de la base de datos");
            }
        }
        public List<Libro> ObtenerLibros()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT L.ID, L.Titulo, L.Autor,L.Editorial,L.Precio_Base AS 'Precio', S.Vitrina_1 , S.Vitrina_2, S.Almacen FROM libro L LEFT JOIN stock_Libros S ON L.ID = S.Libro WHERE L.Activo=1 ";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<Libro> aux = new List<Libro>();
                while (reader.Read())
                {

                    Libro a = new Libro
                    {
                        Id = reader.GetInt32(0),
                        Titulo = reader.GetString(1),
                        Autor = reader.GetString(2),
                        Editorial = reader.GetString(3),
                        Precio_base = reader.GetDecimal(4),
                        Stock_vitrina_1 = reader.GetInt32(5),
                        Stock_vitrina_2 = reader.GetInt32(6),
                        Stock_almacen = reader.GetInt32(7)
                    };
                    aux.Add(a);
                }
                Conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de los Libros de la base de datos");
            }
        }

        public List<Libro> ObtenerLibrosAll()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT L.ID, L.Titulo, L.Autor,L.Editorial,L.Precio_Base AS 'Precio', S.Vitrina_1 , S.Vitrina_2, S.Almacen FROM libro L LEFT JOIN stock_Libros S ON L.ID = S.Libro";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<Libro> aux = new List<Libro>();
                while (reader.Read())
                {

                    Libro a = new Libro
                    {
                        Id = reader.GetInt32(0),
                        Titulo = reader.GetString(1),
                        Autor = reader.GetString(2),
                        Editorial = reader.GetString(3),
                        Precio_base = reader.GetDecimal(4),
                        Stock_vitrina_1 = reader.GetInt32(5),
                        Stock_vitrina_2 = reader.GetInt32(6),
                        Stock_almacen = reader.GetInt32(7)
                    };
                    aux.Add(a);
                }
                Conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de los Libros de la base de datos");
            }
        }

        //-----------------------------------VENTA LIBROS -----------------------//
        public bool AgregarVentaLibreria(VentaLibro venta, PagoLibreria pago, Cobro cobro) 
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string ventaQuery = "INSERT INTO venta (CompradorID, Fecha, Total, TipoVenta, Observaciones, Recibio) VALUES ('"
                        + venta.Comprador_ID + "', '" + FormatearFecha(venta.Fecha) + "'," + venta.Total + ",'" + venta.TipoVenta + "', '"  + pago.Observaciones + "', '" + pago.Recibio + "');";
                string pagoQuery = "INSERT INTO pagosLibreria (CompradorID, FechaPago, Total,Pago, Cambio, Concepto, Observaciones, Recibio, Parent_Id) select '"
                        + pago.CompradorID + "', '" + FormatearFecha(pago.FechaPago) + "'," + pago.Total + "," + pago.Pago + "," + pago.Cambio + ", '" + pago.Concepto + "', '"
                        + pago.Observaciones + "', '" + pago.Recibio + "', MAX(ID) from ventas;";

                string actualizaCobros = "";
                if(cobro != null)
                    actualizaCobros = "INSERT INTO cobrosLibreria (Alumno, Concepto,Cantidad, Pago, Restante, Fecha, Parent_ID) select '" + cobro.Remitente + "','" + cobro.Concepto + "','" + cobro.Cantidad + "','" + cobro.Pago + "','" + cobro.Restante + "', '" + FormatearFecha(cobro.Fecha) + "', MAX(ID) from ventas;";
                string queryLibros = "";
                string query_inventario = "";
                foreach (DetalleVentaLibro aux in venta.DetallesVenta)
                {
                    int vitrina1 = aux.Libro.Stock_vitrina_1, vitrina2 = aux.Libro.Stock_vitrina_2, almacen = aux.Libro.Stock_almacen;
                    if (aux.Cantidad <= aux.Libro.Stock_almacen)
                    {
                        almacen = aux.Libro.Stock_almacen - aux.Cantidad;
                    }
                    else if(aux.Cantidad-aux.Libro.Stock_almacen <= aux.Libro.Stock_vitrina_1)
                    {
                        almacen = 0;
                        vitrina1 = aux.Cantidad - aux.Libro.Stock_almacen;
                    }
                    else
                    {
                        almacen = 0;
                        vitrina1 = 0;
                        vitrina2 = aux.Cantidad - aux.Libro.Stock_almacen -aux.Libro.Stock_vitrina_1;
                    }
                    query_inventario += "update stock_Libros SET Vitrina_1 ="+vitrina1+" Vitrina_2="+ vitrina2 + " Almacen="+almacen+" WHERE Libro="+aux.Libro_Id + "; ";
                    queryLibros += " insert INTO detalleVentaLibros ( Libro, Precio_Unitario,Cantidad, Total, Venta) select '" + aux.Libro_Id + "','" + aux.Precio_Unitario + "','" + aux.Cantidad + "','" + aux.Total + "', MAX(ID) from ventas; "; 
                }
                //FALTA ACTUALIZAR INVERNTARIO LIBROS********************************

                Cmd.CommandText = "BEGIN TRANSACTION;" +
                    ventaQuery +
                    pagoQuery +
                    actualizaCobros +
                    queryLibros +
                    query_inventario +
                    "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception E)
            {
                Conn.Close();
                throw new Exception("Error al agregar el Venta de libros a la base de datos");
            }
        }
        public bool ActualizarVentaLibreria(VentaLibro venta, List<DetalleVentaLibro> old_detalleVentas, List<PagoLibreria> old_pagos, PagoLibreria pago, Cobro cobro) 
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string ventaQuery = "UPDATE venta SET CompradorID='"+ venta.Comprador_ID+ "', Fecha='" + FormatearFecha(venta.Fecha) + "', Total='" + venta.Total + 
                    "', TipoVenta='" + venta.TipoVenta+ "', Observaciones='" + venta.Observaciones + "', Recibio='" + venta.Recibio + "' WHERE ID="+venta.Id+";";
                string pagos_old = "";
                foreach (var item in old_pagos)
                {
                    pagos_old += "DELETE FROM pagosLibreria WHERE ID="+item.Id+";";
                }
                string pagoQuery = "INSERT INTO pagosLibreria (CompradorID, FechaPago, Total,Pago, Cambio, Concepto, Observaciones, Recibio, Parent_Id) select '"
                       + pago.CompradorID + "', '" + FormatearFecha(pago.FechaPago) + "'," + pago.Total + "," + pago.Pago + "," + pago.Cambio + ", '" + pago.Concepto + "', '"
                       + pago.Observaciones + "', '" + pago.Recibio + "', '"+venta.Id+"'";
                string actualizaCobros = "";
                if(cobro != null)
                    actualizaCobros = "UPDATE cobrosLibreria SET Alumno='" + cobro.Remitente + "', Concepto='" + cobro.Concepto + "',Cantidad='" + cobro.Cantidad +
                        "', Pago='" + cobro.Pago + "', Restante='" + cobro.Restante + "', Fecha='" + FormatearFecha(cobro.Fecha) + "', Parent_ID =" + venta.Id + " WHERE ID=" + cobro.Id + ";";
                string queryLibros = "DELETE FROM detalleVentaLibros WHERE venta=" + pago.Id + "; ";
                string query_inventario_old = "";
                foreach (var item in old_detalleVentas)
                {
                    query_inventario_old += "update stock_Libros SET Almacen=Almacen" + item.Cantidad + " WHERE Libro=" + item.Libro_Id + "; ";
                }
                string query_inventario = "";
                foreach (DetalleVentaLibro aux in venta.DetallesVenta)
                {
                    aux.Cantidad += old_detalleVentas.Where(m => m.Libro_Id == aux.Libro_Id).FirstOrDefault().Cantidad;
                    int vitrina1 = aux.Libro.Stock_vitrina_1, vitrina2 = aux.Libro.Stock_vitrina_2, almacen = aux.Libro.Stock_almacen;
                    if (aux.Cantidad <= aux.Libro.Stock_almacen)
                    {
                        almacen = aux.Libro.Stock_almacen - aux.Cantidad;
                    }
                    else if (aux.Cantidad - aux.Libro.Stock_almacen <= aux.Libro.Stock_vitrina_1)
                    {
                        almacen = 0;
                        vitrina1 = aux.Cantidad - aux.Libro.Stock_almacen;
                    }
                    else
                    {
                        almacen = 0;
                        vitrina1 = 0;
                        vitrina2 = aux.Cantidad - aux.Libro.Stock_almacen - aux.Libro.Stock_vitrina_1;
                    }
                    query_inventario += "update stock_Libros SET Vitrina_1 =" + vitrina1 + " Vitrina_2=" + vitrina2 + " Almacen=" + almacen + " WHERE Libro=" + aux.Libro_Id + "; ";
                    queryLibros += " insert INTO detalleVentaLibros ( Libro, Precio_Unitario,Cantidad, Total, Venta) select '" + aux.Libro_Id + "','" + aux.Precio_Unitario + "','" + aux.Cantidad + "','" + aux.Total + "',"+ venta.Id+"; "; 
                }
                //FALTA ACTUALIZAR INVERNTARIO LIBROS********************************
                Cmd.CommandText = "BEGIN TRANSACTION;" +
                    ventaQuery +
                    pagos_old +
                    pagoQuery +
                    actualizaCobros +
                    queryLibros +
                    query_inventario_old +
                    query_inventario +
                    "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception E)
            {
                Conn.Close();
                throw new Exception("Error al agregar el Venta de libros a la base de datos");
            }
        }
        public Cobro ConsultarCobroLibreria(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT ID, Alumno, Concepto,Cantidad, Pago, Restante, Fecha, Parent_ID FROM cobrosLibreria" +
                    " WHERE Parent_ID='" + id + "' AND Concepto='Credito Libreria' ";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    Cobro Cobro = new Cobro
                    {
                        Id = reader.GetInt32(0),
                        Remitente = reader.GetString(1),
                        Concepto = reader.GetString(2),
                        Cantidad = reader.GetDecimal(3),
                        Pago = reader.GetDecimal(4),
                        Restante = reader.GetDecimal(5),
                        Fecha = reader.GetDateTime(6),
                        Parent_id = reader.GetString(7)
                    };
                    Conn.Close();
                    return Cobro;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos del cobro de la base de datos");
            }
        }        
        public VentaLibro ConsultarVentaLibreria(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT ID, Comprador_ID, Fecha, TipoVenta,Recibio, Total, Observaciones FROM ventas" +
                    " WHERE ID='" + id + "'";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    VentaLibro VentaLibro = new VentaLibro
                    {
                        Id = reader.GetInt32(0),
                        Comprador_ID = reader.GetString(1),
                        Fecha = reader.GetDateTime(2),
                        TipoVenta= reader.GetString(3),
                        Recibio = reader.GetString(4),
                        Total = reader.GetDecimal(5),
                        Observaciones = reader.GetString(6),
                    };
                    Conn.Close();
                    return VentaLibro;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos del pago de la base de datos");
            }
        }

        public List<PagoLibreria> ConsultarVentaLibreria_Pagos(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT ID, CompradorID, FechaPago, Total,Pago, Cambio, Concepto, Observaciones, Recibio, Parent_Id FROM pagosLibreria WHERE Concepto='Venta Libros' AND Parent_Id='" + id + "'";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<PagoLibreria> pagos = new List<PagoLibreria>();
                while (reader.Read())
                {
                    PagoLibreria pago = new PagoLibreria
                    {
                        Id = reader.GetInt32(0),
                        CompradorID = reader.GetString(1),
                        FechaPago = reader.GetDateTime(2),
                        Total = reader.GetDecimal(3),
                        Pago = reader.GetDecimal(4),
                        Cambio = reader.GetDecimal(5),
                        Concepto = reader.GetString(6),
                        Observaciones = reader.GetString(7),
                        Recibio = reader.GetString(8)
                    };
                    pagos.Add(pago);
                }
                Conn.Close();
                return pagos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos del pagos de venta de la base de datos");
            }
        }
        public List<DetalleVentaLibro> ConsultarVentaLibreria_DetallesDeVenta(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT Libro, Cantidad, Precio_Unitario, Total FROM detalleVentaLibros WHERE Venta='" + id + "'";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<DetalleVentaLibro> aux = new List<DetalleVentaLibro>();
                while (reader.Read())
                {
                    DetalleVentaLibro detalle = new DetalleVentaLibro
                    {
                        Libro_Id = reader.GetString(0),
                        Cantidad = reader.GetInt32(1),
                        Precio_Unitario = reader.GetInt32(2),
                        Total= reader.GetInt32(3)
                    };
                    aux.Add(detalle);
                }
                Conn.Close();
                return aux;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de venta de la base de datos");
            }
        }

        public SqlDataAdapter ObtenerVentaLibrosTable()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT Id ,IIF(Comprador_ID = '0' , 'Público General', Comprador_Id) as Comprador ,Fecha,Recibio,TipoVenta,Observaciones,Total,Activo FROM ventas"
                    , Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de las materias de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerVentaLibrosTable(string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT Id ,IIF(Comprador_ID = '0' , 'Público General', Comprador_Id) as Comprador ,Fecha,Recibio,TipoVenta,Observaciones,Total,Activo FROM ventas "+
                    " WHERE " +
                    "(Comprador_ID LIKE '%" + parameter + "%' or " +
                    " Id LIKE '%" + parameter + "%' or " +
                    " Fecha LIKE '%" + parameter + "%' or " +
                    " Recibio LIKE '%" + parameter + "%' or " +
                    " TipoVenta LIKE '%" + parameter + "%' or " +
                    " Observaciones LIKE '%" + parameter + "%' or " +
                    " 'Comprador' LIKE '%" + parameter + "%' or " +
                    " Total LIKE '%" + parameter + "%') AND Activo=1 ";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.Conn);
                this.Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de las materias de la base de datos");
            }
        }
        public bool CancelarVentaLibreria(string id, List<DetalleVentaLibro> venta)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string ventaQuery = "UPDATE ventas SET Activo=0 WHERE ID=" + id + ";";
                string queryLibros = "DELETE FROM detalleVentaLibro WHERE Venta=" + id + "; ";
                string query_inventario_old = "";
                foreach (var item in venta)
                {
                    query_inventario_old += "update stock_Libros SET Almacen=Almacen+" + item.Cantidad + " WHERE Libro=" + item.Libro_Id + "; ";
                }
                Cmd.CommandText = "BEGIN TRANSACTION;" +
                    ventaQuery +
                    queryLibros +
                    query_inventario_old +
                    "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception E)
            {
                Conn.Close();
                throw new Exception("Error al cancelar venta de libros a la base de datos");
            }
        }


        //-------------------------------PAGOS LIBRERIA--------------------------------------//
        public bool AgregarPagoLibreria(PagoLibreria pago, List<Cobro> cobros)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string pagoQuery = "INSERT INTO pagosLibreria (CompradorID, FechaPago, Total,Pago,Cambio, Concepto, Observaciones, Recibio) VALUES ('"
                        + pago.CompradorID + "', '" + FormatearFecha(pago.FechaPago) + "'," + pago.Total + "," + pago.Pago + "," + pago.Cambio + ", '" + pago.Concepto + "', '"
                        + pago.Observaciones + "', '" + pago.Recibio + "');";
                string actualizarCobros = "";
                foreach (Cobro aux in cobros)
                {
                    actualizarCobros += "UPDATE cobrosAlumno SET Pago = " + aux.Pago + ", Restante=" + aux.Restante + " WHERE ID = " + aux.Id + "; ";
                }


                Cmd.CommandText = "BEGIN TRANSACTION;" +
                    pagoQuery +
                    actualizarCobros +
                    "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception E)
            {
                Conn.Close();
                throw new Exception("Error al agregar el pago del alumno a la base de datos");
            }
        }
        public bool AgregarPagoLibreria(PagoLibreria pago)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string pagoQuery = "INSERT INTO pagosLibreria (CompradorID, FechaPago, Total,Pago,Cambio, Concepto, Observaciones, Recibio) VALUES ('"
                        + pago.CompradorID + "', '" + FormatearFecha(pago.FechaPago) + "'," + pago.Total + "," + pago.Pago + "," + pago.Cambio + ", '" + pago.Concepto + "', '"
                        + pago.Observaciones + "', '" + pago.Recibio + "');";
                Cmd.CommandText = pagoQuery;
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception E)
            {
                Conn.Close();
                throw new Exception("Error al agregar el pago del alumno a la base de datos");
            }

        }
        public SqlDataAdapter ObtenerPagosLibreriaTable()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT ID, CompradorID AS 'Comprador', FechaPago AS 'Fecha de Pago', Cantidad, Concepto, Observaciones, Recibio, Estado FROM pagosLibreria", Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de los pagos de la base de datos");
            }

        }
        public SqlDataAdapter ObtenerPagosLibreriaTable(string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT ID, CompradorID AS 'Comprador', FechaPago AS 'Fecha de Pago', Cantidad, Concepto, Observaciones, Recibio, Estado FROM pagosLibreria WHERE" +
                    "(CompradorID LIKE '%" + parameter + "%' or " +
                    "Cantidad LIKE '%" + parameter + "%' or " +
                    "Concepto LIKE '%" + parameter + "%' or " +
                    "Recibio LIKE '%" + parameter + "%')";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de los pagos de la base de datos");
            }

        }
        public PagoLibreria ConsultarPagoLibreria(int id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT  ID ,CompradorID,FechaPago,Total,Pago,Cambio,Concepto,Observaciones,Recibio,Estado FROM pagosLibreria WHERE ID='" + id + "'";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    PagoLibreria pago = new PagoLibreria
                    {
                        Id = reader.GetInt32(0),
                        CompradorID = reader.GetString(1),
                        FechaPago = reader.GetDateTime(2),
                        Total = reader.GetDecimal(3),
                        Pago = reader.GetDecimal(4),
                        Cambio = reader.GetDecimal(5),
                        Concepto = reader.GetString(6),
                        Observaciones = reader.GetString(7),
                        Recibio = reader.GetString(8)
                    };
                    Conn.Close();
                    return pago;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos del pago de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerPagosLibreriaByCompradorTable(String rfc)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT ID,Cantidad,Concepto,Observaciones,Recibio,FechaPago FROM pagosLibreria WHERE CompradorID = '" + rfc + "'", Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de los pagos del alumno de la base de datos");
            }


        }
        public List<String> ObtenerConceptosDePagoLibreria(string area)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT Concepto FROM conceptos WHERE Tipo='Pago' AND Area='Libreria';";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<String> aux = new List<String>();
                while (reader.Read())
                {
                    aux.Add(reader.GetString(0));
                }
                Conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de los conceptos de pago de la base de datos");
            }

        }
        public bool CancelarPagoLibreria(int id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string query = "UPDATE pagosLibreria SET Activo=0 WHERE ID=" + id;
                query += "UPDATE cobroAlumno SET Activo = 0 WHERE ID = " + id;
                Cmd.CommandText = query;
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
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
        public Cobro ConsultarCobroDeAlumnoLibreria(String rfc)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT ID,Concepto,Cantidad,Pago,Restante,Alumno,Parent_ID FROM cobrosAlumno WHERE Alumno = '" + rfc + "' AND Concepto='Libreria';";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    Cobro cobro = new Cobro();
                    cobro.Id = reader.GetInt32(0);
                    cobro.Concepto = reader.GetString(1);
                    cobro.Cantidad = reader.GetDecimal(2);
                    cobro.Pago = reader.GetDecimal(3);
                    cobro.Restante = reader.GetDecimal(4);
                    cobro.Remitente = reader.GetString(5);
                    cobro.Parent_id = reader.GetString(6);
                    Conn.Close();
                    return cobro;
                }
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de los pagos del alumno de la base de datos");
            }

        }
        public List<Cobro> ConsultarCobrosDeAlumnoLibreria(String rfc)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT ID,Concepto,Cantidad,Pago,Restante,Alumno,Parent_ID FROM cobrosAlumno WHERE Alumno = '" + rfc + "' AND Restante > 0";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<Cobro> aux = new List<Cobro>();
                while (reader.Read())
                {
                    Cobro cobro = new Cobro();
                    cobro.Id = reader.GetInt32(0);
                    cobro.Concepto = reader.GetString(1);
                    cobro.Cantidad = reader.GetDecimal(2);
                    cobro.Pago = reader.GetDecimal(3);
                    cobro.Restante = reader.GetDecimal(4);
                    cobro.Remitente = reader.GetString(5);
                    cobro.Parent_id = reader.GetString(6);
                    aux.Add(cobro);
                }
                Conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de los pagos del alumno de la base de datos");
            }

        }
        public SqlDataAdapter ObtenerCobrosDeAlumnoLibreriaTable(String rfc)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT ID,Concepto,Cantidad,Pago,Restante,Fecha FROM cobrosAlumno WHERE Alumno = '" + rfc + "'", Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de los pagos del alumno de la base de datos");
            }

        }

        //-------------------------------Credito de Libreria-------------------------------//
        public bool AgregarCreditoLibreriaAlumno(CreditoAlumno credito)
        {

            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string creditoQuery = "INSERT INTO creditoAlumno (AlumnoID, CantidadMensualidad, CantidadMeses, CantidadAbonoCredito, CantidadAbonoMensual, FechaSolicitud, Observaciones, Estado) VALUES ('"
                    + credito.Alumno + "', '" + credito.CantidadMensualidad + "', '" + credito.CantidadMeses + "', '"
                    + credito.CantidadAbonoCredito + "', '" + credito.CantidadAbonoMensual + "','"
                    + FormatearFecha(DateTime.Now) + "', '" + credito.Observaciones + "', '" + credito.Estado + "');";
                string registroCobro = "INSERT INTO cobrosAlumno(Alumno, Concepto, Cantidad, Pago, Restante, Fecha, Parent_ID)SELECT '"
                    + credito.Alumno + "','Credito Escolar', '" + (credito.CantidadMensualidad * credito.CantidadMeses) +
                    "','0.00','" + (credito.CantidadMensualidad * credito.CantidadMeses) + "','" + FormatearFecha(DateTime.Now) +
                    "', AUTO_INCREMENT-1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '" + this.Database +
                    "' AND TABLE_NAME = 'creditoAlumno';";
                string updateColegiatura = "UPDATE cobrosAlumno SET Restante=0,Cantidad=Pago WHERE Parent_ID = '" + credito.Alumno + "' AND Restante > 0;";


                Cmd.CommandText = "BEGIN TRANSACTION;" +
                    creditoQuery +
                    registroCobro +
                    updateColegiatura +
                    "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception E)
            {
                Conn.Close();
                throw new Exception("Error al agregar el credito del alumnos a la base d datos");

            }
        }
        public SqlDataAdapter ObtenerCreditoLibreriaAlumnosTable()
        {

            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT ID, AlumnoID, CantidadMensualidad AS 'Mensualidad' , CantidadMeses AS 'No. de Meses',  FechaSolicitud AS 'Fecha de Solicitud', Observaciones FROM creditoAlumno WHERE Estado NOT LIKE 'Cancelado'", Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de los creditos de los alumnos de la base de datos");
            }

        }
        public SqlDataAdapter ObtenerCreditoLibreriaAlumnosTable(string parameter)
        {

            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT ID, AlumnoID, CantidadMensualidad AS 'Mensualidad' , CantidadMeses AS 'No. de Meses',  FechaSolicitud AS 'Fecha de Solicitud', Observaciones FROM creditoAlumno WHERE Estado NOT LIKE 'Cancelado' AND " +
                    "(AlumnoID LIKE '%" + parameter + "%' or " +
                    "CantidadMensualidad LIKE '%" + parameter + "%' or " +
                    "CantidadMeses LIKE '%" + parameter + "%' or " +
                    "Observaciones LIKE '%" + parameter + "%')";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de los creditos de los alumnos de la base de datos");
            }

        }
        public CreditoAlumno ConsultarCreditoLibreriaActivoAlumno(string rfc)
        {

            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT C.ID, C.AlumnoID, C.CantidadMensualidad, C.CantidadMeses, C.CantidadAbonoCredito, C.CantidadAbonoMensual, C.FechaSolicitud, C.Observaciones, C.Estado, A.Pago FROM creditoAlumno C, cobrosAlumno A WHERE A.Parent_ID=C.ID AND AlumnoID='" + rfc + "' AND C.Estado='Activo'";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    CreditoAlumno credito = new CreditoAlumno
                    {
                        Id = reader.GetInt32(0),
                        Alumno = reader.GetString(1),
                        CantidadMensualidad = reader.GetDecimal(2),
                        CantidadMeses = reader.GetInt32(3),
                        CantidadAbonoCredito = reader.GetDecimal(4),
                        CantidadAbonoMensual = reader.GetDecimal(5),
                        FechaSolicitud = reader.GetDateTime(6),
                        Observaciones = reader.GetString(7),
                        Estado = reader.GetString(8),
                        Pago = reader.GetDecimal(9)
                    };
                    Conn.Close();
                    return credito;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos del credito del alumno de la base de datos");
            }

        }
        public CreditoAlumno ConsultarCreditoLibreriaAlumno(string rfc)
        {

            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT C.ID, C.AlumnoID, C.CantidadMensualidad, C.CantidadMeses, C.CantidadAbonoCredito, C.CantidadAbonoMensual, C.FechaSolicitud, C.Observaciones, C.Estado, A.Pago FROM creditoAlumno C, cobrosAlumno A WHERE A.Parent_ID=C.ID AND AlumnoID='" + rfc + "'";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    CreditoAlumno credito = new CreditoAlumno
                    {
                        Id = reader.GetInt32(0),
                        Alumno = reader.GetString(1),
                        CantidadMensualidad = reader.GetDecimal(2),
                        CantidadMeses = reader.GetInt32(3),
                        CantidadAbonoCredito = reader.GetDecimal(4),
                        CantidadAbonoMensual = reader.GetDecimal(5),
                        FechaSolicitud = reader.GetDateTime(6),
                        Observaciones = reader.GetString(7),
                        Estado = reader.GetString(8),
                        Pago = reader.GetDecimal(9)
                    };
                    Conn.Close();
                    return credito;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos del credito del alumno de la base de datos");
            }

        }
        public bool ActualizarEstadoCreditoLibreria(string credito_ID, string estado)
        {

            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string creditoQuery = "UPDATE creditoAlumno SET Estado ='" + estado + "' WHERE ID = '" + credito_ID + "'";
                Cmd.CommandText = "BEGIN TRANSACTION;" +
                    creditoQuery +
                    "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al actualizar los datos del credito del alumno en la Base de Datos");
            }
        }
        public bool ActualizarCreditoLibreria(CreditoAlumno credito)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string creditoQuery = "UPDATE creditoAlumno SET CantidadMensualidad= '" + credito.CantidadMensualidad + "', CantidadMeses= '" + credito.CantidadMeses +
                    "', CantidadAbonoCredito=" + credito.CantidadAbonoCredito + "', CantidadAbonoMensual='" + credito.CantidadAbonoMensual +
                    "', Observaciones= '" + credito.Observaciones + "' WHERE AlumnoID = '" + credito.Alumno + "'";
                string registroCobro = "UPDATE cobrosAlumno set Alumno='" + credito.Alumno + "', Cantidad='" + (credito.CantidadMensualidad * credito.CantidadMeses) +
                    "',Restante=" + (credito.CantidadMensualidad * credito.CantidadMeses) + "- pago,Fecha='" + FormatearFecha(DateTime.Now) + "' WHERE Parent_ID = '" + credito.Id + "' AND Concepto='Credito Escolar';";

                Cmd.CommandText = "BEGIN TRANSACTION;" +
                    creditoQuery +
                    registroCobro +
                    "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al actualizar los datos del credito del alumno en la Base de Datos");
            }

        }
        public bool CancelarCreditoLibreria(string id)
        {


            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "UPDATE creditoAlumno SET Estado = 'Cancelado' WHERE ID=" + id;
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al cancelar el crédito del alumno en la Base de Datos");
            }
        }


        //-----------------------------------PRÉSTAMOS LIBROS -----------------------//
        public bool AgregarPrestamoLibreria(Prestamo prestamo)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string ventaQuery = "INSERT INTO prestamos (CompradorID, FechaPrestamo, FechaLimite, DiasPrestamo Observaciones, Recibio) VALUES ('"
                        + prestamo.Comprador_ID + "', '" + FormatearFecha(prestamo.FechaPrestamo) + "'," + FormatearFecha(prestamo.FechaLimite) + "'," + prestamo.Dias + ",'" + prestamo.Observaciones + "', '" + prestamo.Recibio + "');";
                string queryLibros = "";
                string query_inventario = "";
                foreach (DetallePrestamoLibro aux in prestamo.DetallesPrestamo)
                {
                    int vitrina1 = aux.Libro.Stock_vitrina_1, vitrina2 = aux.Libro.Stock_vitrina_2, almacen = aux.Libro.Stock_almacen;
                    if (aux.Cantidad <= aux.Libro.Stock_almacen)
                    {
                        almacen = aux.Libro.Stock_almacen - aux.Cantidad;
                    }
                    else if (aux.Cantidad - aux.Libro.Stock_almacen <= aux.Libro.Stock_vitrina_1)
                    {
                        almacen = 0;
                        vitrina1 = aux.Cantidad - aux.Libro.Stock_almacen;
                    }
                    else
                    {
                        almacen = 0;
                        vitrina1 = 0;
                        vitrina2 = aux.Cantidad - aux.Libro.Stock_almacen - aux.Libro.Stock_vitrina_1;
                    }
                    query_inventario += "update stock_Libros SET Vitrina_1 =" + vitrina1 + ", Vitrina_2=" + vitrina2 + ", Almacen=" + almacen + ", Prestados=Prestados+"+aux.Cantidad+" WHERE Libro=" + aux.Libro_Id + "; ";
                    queryLibros += " insert INTO detallePrestamoLibro ( Libro, Cantidad, Prestamo) select '" + aux.Libro_Id + "','" + aux.Cantidad + "', MAX(ID) from prestamos;";
                }

                Cmd.CommandText = "BEGIN TRANSACTION;" +
                    ventaQuery +
                    queryLibros +
                    query_inventario+
                    "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception E)
            {
                Conn.Close();
                throw new Exception("Error al agregar el préstamo de libros a la base de datos");
            }
        }
        public bool ActualizarPrestamoLibreria(Prestamo prestamo, List<DetallePrestamoLibro> old_detallePrestamo)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string ventaQuery = "UPDATE prestamos SET CompradorID='" + prestamo.Comprador_ID + "', FechaPrestamo='" + FormatearFecha(prestamo.FechaPrestamo) +
                    "', FechaLimite='" + FormatearFecha(prestamo.FechaLimite) + "', DiasPrestamo='" + prestamo.Dias +
                    "', Observaciones='" + prestamo.Observaciones + "', Recibio='" + prestamo.Recibio + "' WHERE ID=" + prestamo.Id + ";";
                string queryLibros = "DELETE FROM detallePrestamoLibros WHERE Prestamo=" + prestamo.Id + "; ";
                string query_inventario_old = "";
                foreach (var item in old_detallePrestamo)
                {
                    query_inventario_old += "update stock_Libros SET Almacen=Almacen+" + item.Cantidad + ", Prestados=Prestados-"+item.Cantidad+" WHERE Libro=" + item.Libro_Id + "; ";
                }
                string query_inventario = "";
                foreach (DetallePrestamoLibro aux in prestamo.DetallesPrestamo)
                {
                    aux.Cantidad += old_detallePrestamo.Where(m => m.Libro_Id == aux.Libro_Id).FirstOrDefault().Cantidad;
                    int vitrina1 = aux.Libro.Stock_vitrina_1, vitrina2 = aux.Libro.Stock_vitrina_2, almacen = aux.Libro.Stock_almacen;
                    if (aux.Cantidad <= aux.Libro.Stock_almacen)
                    {
                        almacen = aux.Libro.Stock_almacen - aux.Cantidad;
                    }
                    else if (aux.Cantidad - aux.Libro.Stock_almacen <= aux.Libro.Stock_vitrina_1)
                    {
                        almacen = 0;
                        vitrina1 = aux.Cantidad - aux.Libro.Stock_almacen;
                    }
                    else
                    {
                        almacen = 0;
                        vitrina1 = 0;
                        vitrina2 = aux.Cantidad - aux.Libro.Stock_almacen - aux.Libro.Stock_vitrina_1;
                    }
                    query_inventario += "update stock_Libros SET Vitrina_1 =" + vitrina1 + ", Vitrina_2=" + vitrina2 + ", Almacen=" + almacen + ",Prestados=Prestados+" + aux.Cantidad+" WHERE Libro=" + aux.Libro_Id + "; ";
                    queryLibros += " insert INTO detallePrestamoLibro ( Libro, Cantidad, Prestamo) select '" + aux.Libro_Id + "','" + aux.Cantidad + "'," + prestamo.Id + "; ";
                }
                Cmd.CommandText = "BEGIN TRANSACTION;" +
                    ventaQuery +
                    queryLibros +
                    query_inventario_old+
                    query_inventario+
                    "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception E)
            {
                Conn.Close();
                throw new Exception("Error al agregar el Venta de libros a la base de datos");
            }
        }
        public Prestamo ConsultarPrestamoLibreria(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT ID, Comprador_ID, FechaPrestamo, FechaLimite,Recibio, DiasPrestamo, Observaciones FROM prestamos" +
                    " WHERE ID='" + id + "'";
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    Prestamo Prestamo = new Prestamo
                    {
                        Id = reader.GetInt32(0),
                        Comprador_ID = reader.GetString(1),
                        FechaPrestamo = reader.GetDateTime(2),
                        FechaLimite = reader.GetDateTime(3),
                        Recibio = reader.GetString(4),
                        Dias = reader.GetInt32(5),
                        Observaciones = reader.GetString(6),
                    };
                    Conn.Close();
                    return Prestamo;
                }
                Conn.Close();
                return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos del pago de la base de datos");
            }
        }
        public List<DetallePrestamoLibro> ConsultarPrestamoLibreria_DetallesDePrestamo(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT Libro, Cantidad FROM detallePrestamoLibro WHERE Prestamo='" + id + "'";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<DetallePrestamoLibro> aux = new List<DetallePrestamoLibro>();
                while (reader.Read())
                {
                    DetallePrestamoLibro detalle = new DetallePrestamoLibro
                    {
                        Id = reader.GetInt32(0),
                        Cantidad = reader.GetInt32(1),
                    };
                    aux.Add(detalle);
                }
                Conn.Close();
                return aux;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos del pago de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerPrestamoLibrosTable()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                SqlDataAdapter mdaDatos = new SqlDataAdapter("SELECT Id, Comprador_ID, FechaPrestamo, FechaLimite,Recibio, DiasPrestamo, Observaciones, Activo FROM prestamos "
                    , Conn);
                Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de los prestamos de la base de datos");
            }
        }
        public SqlDataAdapter ObtenerPrestamoLibrosTable(string parameter)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string sqlString = "SELECT Id, Comprador_ID, FechaPrestamo, FechaLimite,Recibio, DiasPrestamo, Observaciones, Activo FROM prestamos " +
                    " WHERE " +
                    "(Comprador_ID LIKE '%" + parameter + "%' or " +
                    " Id LIKE '%" + parameter + "%' or " +
                    " FechaPrestamo LIKE '%" + parameter + "%' or " +
                    " FechaLimite LIKE '%" + parameter + "%' or " +
                    " Recibio LIKE '%" + parameter + "%' or " +
                    " Observaciones LIKE '%" + parameter + "%') AND Activo=1 ";
                SqlDataAdapter mdaDatos = new SqlDataAdapter(sqlString, this.Conn);
                this.Conn.Close();
                return mdaDatos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos de los prestamos de la base de datos");
            }
        }
        public bool CancelarPrestamoLibreria(string id, List<DetallePrestamoLibro> prestamo)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                string ventaQuery = "UPDATE prestamos SET Activo=0 WHERE ID=" + id + ";";
                string queryLibros = "DELETE FROM detallePrestamoLibros WHERE Prestamo=" + id + "; ";
                string query_inventario_old = "";
                foreach (var item in prestamo)
                {
                    query_inventario_old += "update stock_Libros SET Almacen=Almacen+" + item.Cantidad + ", Prestados=Prestados-" + item.Cantidad + " WHERE Libro=" + item.Libro_Id + "; ";
                }
                Cmd.CommandText = "BEGIN TRANSACTION;" +
                    ventaQuery +
                    queryLibros +
                    query_inventario_old +
                    "COMMIT;";
                int rowsAfected = Cmd.ExecuteNonQuery();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception E)
            {
                Conn.Close();
                throw new Exception("Error al cancelar préstamo de libros a la base de datos");
            }
        }

        public List<PagoLibreria> ConsultarPrestamoLibreria_Pagos(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT ID, CompradorID, FechaPago, Total,Pago, Cambio, Concepto, Observaciones, Recibio, Parent_Id FROM pagosLibreria WHERE  Concepto='Prestamo Libreria' AND  Parent_Id='" + id + "'";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<PagoLibreria> pagos = new List<PagoLibreria>();
                while (reader.Read())
                {
                    PagoLibreria pago = new PagoLibreria
                    {
                        Id = reader.GetInt32(0),
                        CompradorID = reader.GetString(1),
                        FechaPago = reader.GetDateTime(2),
                        Total = reader.GetDecimal(3),
                        Pago = reader.GetDecimal(4),
                        Cambio = reader.GetDecimal(5),
                        Concepto = reader.GetString(6),
                        Observaciones = reader.GetString(7),
                        Recibio = reader.GetString(8)
                    };
                    pagos.Add(pago);
                }
                Conn.Close();
                return pagos;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener los datos del pagos de venta de la base de datos");
            }
        }







        //----------------------------------USUARIO--------------------------------------------//
        public Usuario ConsultarUsuario(string id)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT Matricula, Usuario, Contrasena, Nivel_Acceso FROM usuarios WHERE Matricula='" + id + "' or Usuario ='" + id + "'";
                //int rowsAfected = Cmd.ExecuteNonQuery();
                SqlDataReader reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    Usuario pv = new Usuario
                    {
                        Matricula = reader.GetString(0),
                        Nombre_De_Usuario = reader.GetString(1),
                        Contrasena = reader.GetString(2),
                        Nivel_Acceso = reader.GetInt32(3)
                    };
                    Conn.Close();
                    return pv;
                }
                Conn.Close();
                return null;

            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de Usuarios de la Base de Datos");
            }

        }
        public bool ActualizarUsuario(Usuario usuario)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "UPDATE usuarios SET Usuario= '" + usuario.Nombre_De_Usuario +
               "',Contrasena='" + usuario.Contrasena +
               "',Nivel_Acceso='" + usuario.Nivel_Acceso +
               "',Estado = 1 WHERE Matricula='" + usuario.Matricula + "'";
                //Cmd.CommandText = "SELECT * FROM Servicios";
                int rowsAfected = Cmd.ExecuteNonQuery();
                //SqlDataReader reader = Cmd.ExecuteReader();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error..! Error al actualizar usuario de la Base de Datos");
            }
        }
        public bool EliminarUsuario(string matricula)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "UPDATE usuarios SET Estado = 0 WHERE Matricula='" + matricula + "'";
                //Cmd.CommandText = "SELECT * FROM Servicios";
                int rowsAfected = Cmd.ExecuteNonQuery();
                //SqlDataReader reader = Cmd.ExecuteReader();
                Conn.Close();
                if (rowsAfected > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error..! Error al desactivar usuario de la Base de Datos");
            }
        }


        //--------------------------------PARAMETROS GENERALES ----------------------------------//
        private void ConsultarParametrosGenerales()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT * FROM parametros_Generales;";
                this.parametros_Generales = new ParametrosGenerales();
                SqlDataReader reader = Cmd.ExecuteReader();
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
                Cmd.CommandText = "SELECT Nombre FROM ubicaciones;";
                reader = Cmd.ExecuteReader();
                while (reader.Read())
                {
                    aux.Add(reader.GetString(0));
                }
                parametros_Generales.Ubicaciones = aux;
                Conn.Close();
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener parametros generales de la Base de Datos");
            }
        }
        public bool ActualizarParametrosGenerales(ParametrosGenerales parametros)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {

                string updateParametros = "DELETE FROM parametros_Generales; INSERT INTO parametros_Generales VALUES ('" +
                    parametros.Costo_Credito_Especialidad_Diplomado + "','" + parametros.Costo_Credito_Maestria + "','" +
                    parametros.Porcentaje_Pago_Sesion + "','" + parametros.Porcentaje_Pago_Taller + "','" +
                    parametros.Porcentaje_Pago_Clase + "','" + parametros.Porcentaje_Pago_Evaluacion + "','" + parametros.Director + "','" + parametros.Sede + "'); ";
                string updateUbicaciones = "";
                if (parametros.Ubicaciones != null)
                {
                    foreach (string aux in parametros.Ubicaciones)
                    {
                        if (updateUbicaciones != "")
                            updateUbicaciones += ",";
                        updateUbicaciones += "('" + aux + "')";
                    }
                    updateUbicaciones = "DELETE FROM ubicaciones; INSERT INTO ubicaciones (Nombre) VALUES " + updateUbicaciones + "; ";

                }


                Cmd.CommandText = "BEGIN TRANSACTION; " +
                    updateParametros +
                    updateUbicaciones +
                    "COMMIT; ";

                //Cmd.CommandText = "SELECT * FROM Servicios";
                int rowsAfected = Cmd.ExecuteNonQuery();
                //SqlDataReader reader = Cmd.ExecuteReader();
                Conn.Close();
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
                Conn.Close();
                throw new Exception("Error..! Error al actualizar usuario de la Base de Datos");
            }
        }
        public List<String> ConsultarUbicaciones()
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT Nombre FROM ubicaciones";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<String> aux = new List<String>();
                while (reader.Read())
                {
                    aux.Add(reader.GetString(0));
                }
                Conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de Ubicaciones de la Base de Datos");
            }
        }
        public List<String> ObtenerConceptos(string tipo, string area)
        {
            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            OpenConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {
                Cmd.CommandText = "SELECT Concepto FROM conceptos WHERE Tipo='" + tipo + "' AND Area='" + area + "';";
                SqlDataReader reader = Cmd.ExecuteReader();
                List<String> aux = new List<String>();
                while (reader.Read())
                {
                    aux.Add(reader.GetString(0));
                }
                Conn.Close();
                if (aux.Count != 0)
                    return aux;
                else
                    return null;
            }
            catch (Exception e)
            {
                Conn.Close();
                throw new Exception("Error al obtener datos de los conceptos de pago de la base de datos");
            }
        }

        //------------------------------------CONFIGURACIÓN--------------------------------------//
        public string FormatearFecha(DateTime fecha)
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
        public string LeerUserDoc()
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
        public string LeerPVDoc()
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
        public bool RecordarUsuario(string usuario)
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
