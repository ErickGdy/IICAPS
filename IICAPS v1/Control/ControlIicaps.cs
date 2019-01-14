
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IICAPS_v1.DataObject;
using System.IO;
using MySql.Data.MySqlClient;

namespace IICAPS_v1.Control
{
    class ControlIicaps
    {
        MySqlConnection conn;
        MySqlConnectionStringBuilder builder;
        MySqlCommand cmd;

        /*-------HOSTING NEUBOX-----------*/
        //string server = "logacell.com";
        //string userID = "logacell_logamel";
        //string password = "Logamel82";
        ////string database = "logacell_iicaps";
        //string database = "logacell_iicaps_devs";

        /*-------HOSTING ALDEAHOST-----------*/
        //string userID = "iic2ps1d";
        //string pass = "ConejoVolador11";

        //string server = "iicaps.edu.mx";
        string server = "138.128.160.26";
        string userID = "iic2ps1d_devs";
        string password = "ConejoVolador11";
        string database = "iic2ps1d_iicaps_devs";
        //uint port = 3306;
        //uint port = 2083;
        ////MySqlConnectionProtocol protocolo =  MySqlConnectionProtocol.Tcp;
        ////string database = "iic2ps1d_iicaps_prod";


        public static ControlIicaps instance;
        public ParametrosGenerales parametros_Generales;

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
            //builder.Port = port;
            try
            {
                parametros_Generales = consultarParametrosGenerales();
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

        //-------------------------------Alumnos-------------------------------//
        public bool agregarAlumno(Alumno alumno)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO alumnos (Nombre, Direccion, Telefono1, Telefono2, Correo, Facebook, CURP, RFC, Sexo, EstadoCivil, EscuelaProcedencia, Carrera, Programa, Nivel, Fecha, Estado, Tipo, Observaciones, Matricula) VALUES('" 
                    + alumno.nombre + "','" + alumno.direccion + "','" + alumno.telefono1 + "','" + alumno.telefono2 + "','" + alumno.correo + "','" + alumno.facebook + "','" + alumno.curp + "','"
                    + alumno.rfc + "','" + alumno.sexo + "','" + alumno.estadoCivil + "','" + alumno.escuelaProcedencia + "','" + alumno.carrera + "','" + alumno.programa + "','" + alumno.nivel + "','" +formatearFecha(alumno.fecha)+ 
                    "','Registrado','" + alumno.tipo + "','" + alumno.observaciones + "','" + alumno.matricula + "')";
                conn.Open();
                try
                {
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...! Error al agregar el Alumno a la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...! Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerAlumnosTable()
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter("SELECT A.RFC, A.Nombre , A.Telefono1 AS 'Telefono 1', A.Programa AS 'Programa', G.Generacion FROM alumnos A LEFT JOIN grupoAlumno GA ON A.RFC = GA.Alumno LEFT JOIN grupos G ON G.Codigo = GA.Grupo WHERE A.Estado NOT LIKE 'Baja'", conn);
                    conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de los alumnos de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerAlumnosTable(string parameter)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    string sqlString = "SELECT A.RFC, A.Nombre, A.Telefono1 AS 'Telefono 1', A.Programa, G.Generacion FROM alumnos A LEFT JOIN grupoAlumno GA ON A.RFC = GA.Alumno LEFT JOIN grupos G ON G.Codigo = GA.Grupo " +
                        "WHERE " +
                        "(A.Nombre LIKE '%" + parameter + "%' or " +
                        " A.Telefono1 LIKE '%" + parameter + "%' or " +
                        " A.Programa LIKE '%" + parameter + "%' or " +
                        " G.Generacion LIKE '%" + parameter + "%')"; 
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(sqlString, this.conn);
                    this.conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de los alumnos de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public Alumno consultarAlumno (string rfc)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM alumnos WHERE RFC='" + rfc + "'";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos del alumno de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public List<Alumno> obtenerAlumnos()
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM alumnos";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos de los alumnos de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public List<Alumno> obtenerAlumnosByPrograma(string programa)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM alumnos WHERE Programa = '" + programa + "'";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos de los alumnos de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public bool darDeBajaAlumno(string rfc)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE alumnos SET Estado='Baja' WHERE RFC = '" + rfc + "'";
                conn.Open();
                try
                {
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error..! Error al dar de baja el alumno de la Base de Datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public string obtenerNombreAlumno(string rfc)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Nombre FROM alumnos WHERE RFC = '" + rfc + "'";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error..! Error al obtener el nombre del alumno de la Base de Datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public bool darDeAltaAlumno(string rfc)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE alumnos SET Estado='Registrado' WHERE RFC = '" + rfc + "'";
                conn.Open();
                try
                {
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error..! Error al dar de alta el alumno de la Base de Datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public bool actualizarAlumno(Alumno alumno)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE alumnos SET Nombre= '"+alumno.nombre+"', Direccion= '"+alumno.direccion+"', Telefono1= '"+alumno.telefono1+"', Telefono2= '"
                    +alumno.telefono2+"', Correo= '"+alumno.correo+"', Facebook= '"+alumno.facebook+"', Sexo= '"+alumno.sexo+"', EstadoCivil= '"+alumno.estadoCivil+
                    "', Programa= '"+alumno.programa+"', Fecha= '"+formatearFecha(alumno.fecha)+"', Estado= '"+alumno.estado+"', Tipo= '"+alumno.tipo+ "',Observaciones= '"+alumno.observaciones+ "',Matricula= '" + alumno.matricula + "' WHERE RFC = '" + alumno.rfc + "'";
                conn.Open();
                try
                {
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al actualizar los datos del alumno en la Base de Datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }

        //-------------------------------Credito de alumnos-------------------------------//
        public bool agregarCreditoAlumno(CreditoAlumno credito)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO creditoAlumno (AlumnoID, CantidadMensualidad, CantidadMeses, FechaSolicitud, Observaciones, Estado) VALUES ('"
                    + credito.alumno + "', '" + credito.cantidadMensualidad + "', '" + credito.cantidadMeses + "', '"
                    + formatearFecha(DateTime.Now) + "', '" + credito.observaciones + "', '" + credito.estado + "')";
                conn.Open();
                try
                {
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception E)
                {
                    throw new Exception("Error al agregar el credito del alumnos a la base d datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerCreditoAlumnosTable()
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter("SELECT * FROM creditoAlumno", conn);
                    conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de los creditos de los alumnos de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerCreditoAlumnosTable(string parameter)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    string sqlString = "SELECT * FROM creditoAlumno WHERE" +
                        "(AlumnoID LIKE '%" + parameter + "%' or " +
                        "CantidadMensualidad LIKE '%" + parameter + "%' or " +
                        "CantidadMeses LIKE '%" + parameter + "%' or " +
                        "Observaciones LIKE '%" + parameter + "%')";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(sqlString, conn);
                    conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de los creditos de los alumnos de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public CreditoAlumno consultarCreditoAlumno(string rfc)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM creditoAlumno WHERE AlumnoID='" + rfc + "'";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CreditoAlumno credito = new CreditoAlumno();
                        credito.id = reader.GetInt32(0);
                        credito.alumno = reader.GetString(1);
                        credito.cantidadMensualidad = reader.GetDecimal(2);
                        credito.cantidadMeses = reader.GetInt32(3);
                        credito.fechaSolicitud = reader.GetDateTime(4);
                        credito.observaciones = reader.GetString(5);
                        credito.estado = reader.GetString(6);
                        conn.Close();
                        return credito;
                    }
                    conn.Close();
                    return null;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos del credito del alumno de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public bool actualizarCredito(CreditoAlumno credito)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE creditoAlumno SET CantidadMensualidad= '" + credito.cantidadMensualidad + "', CantidadMeses= '" + credito.cantidadMeses + 
                    "', Observaciones= '" + credito.observaciones +  "' WHERE AlumnoID = '" + credito.alumno + "'";
                conn.Open();
                try
                {
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al actualizar los datos del credito del alumno en la Base de Datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public string obtenerProgramaAlumno(String rfc)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Programa FROM alumnos WHERE RFC='" + rfc + "'";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener los datos del programa del alumno de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public List<string> obtenerProgramasAlumno(String rfc)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Programa FROM alumnos WHERE RFC='" + rfc + "'";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener los datos del programa del alumno de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public bool cancelarCredito(string id)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE creditoAlumno SET Estado = 'Cancelado' WHERE ID=" + id;
                conn.Open();
                try
                {
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al cancelar el crédito del alumno en la Base de Datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }

        //-------------------------------PAGOS ALUMNO--------------------------------------//
        public bool agregarPagoAlumno(PagoAlumno pago)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO pagosAlumno (AlumnoID, FechaPago, Cantidad, Concepto, Observaciones, Recibio) VALUES ('"
                    + pago.alumnoID + "', '" + formatearFecha(pago.fechaPago) + "'," + pago.cantidad + ", '" + pago.concepto + "', '"
                    + pago.observaciones + "', '" + pago.recibio + "')";
                conn.Open();
                try
                {
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception E)
                {
                    throw new Exception("Error al agregar el pago del alumno a la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerPagosAlumnoTable()
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter("SELECT * FROM pagosAlumno", conn);
                    conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de los pagos de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerPagosAlumnoTable(string parameter)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    string sqlString = "SELECT * FROM pagosAlumno WHERE" +
                        "(AlumnoID LIKE '%" + parameter + "%' or " +
                        "Cantidad LIKE '%" + parameter + "%' or " +
                        "Concepto LIKE '%" + parameter + "%' or " +
                        "Recibio LIKE '%" + parameter + "%')";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(sqlString, conn);
                    conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de los pagos de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public PagoAlumno consultarPagoAlumno(string id)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM pagosAlumno WHERE ID='" + id + "'";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener los datos del pago de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerPagosAlumnoTable(String rfc, string concepto)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                MySqlDataAdapter mdaDatos = new MySqlDataAdapter();
                try
                {
                    if(concepto.Contains("Colegiatura")) 
                        mdaDatos = new MySqlDataAdapter("SELECT * FROM pagosAlumno WHERE AlumnoID = '"+rfc+ "' AND Estado NOT LIKE 'Cancelado' AND Concepto = 'Pago de Colegiatura' OR 'Abono de Colegiatura'", conn);
                    else if (concepto.Contains("Credito"))
                        mdaDatos = new MySqlDataAdapter("SELECT * FROM pagosAlumno WHERE AlumnoID = '" + rfc + "' AND Estado NOT LIKE 'Cancelado' AND Concepto = 'Pago de Credito' OR 'Abono de Credito'", conn);
                    else if (concepto.Contains("ClubDeTareas"))
                        mdaDatos = new MySqlDataAdapter("SELECT * FROM pagosAlumno WHERE AlumnoID = '" + rfc + "' AND Estado NOT LIKE 'Cancelado' AND Concepto = 'Pago de ClubDeTareas' OR 'Abono de ClubDeTareas'", conn);
                    else if (concepto.Contains("Inscripcion"))
                        mdaDatos = new MySqlDataAdapter("SELECT * FROM pagosAlumno WHERE AlumnoID = '" + rfc + "' AND Concepto = 'Pago de Inscripcion'", conn);
                    conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de los pagos del alumno de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public List<String> obtenerConceptosDePagoAlumno(string area)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Concepto FROM conceptosDePago WHERE Area='"+area+"';";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos de los conceptos de pago de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public bool cancelarPagoAlumno(string id)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE pagosAlumno SET Estado = 'Cancelado' WHERE ID="+id;
                conn.Open();
                try
                {
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
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }

        //-------------------------------MATERIAS-------------------------------//
        public bool agregarMateria(Materia materia)
        {
            try
            {
                string agregar = "INSERT INTO materia (Nombre, Duracion, Semestre, Costo) VALUES('"
                    + materia.nombre + "','"+ materia.duracion + "','" + materia.semestre + "','" + materia.costo + "');";
                string programas = "";
                if (materia.programa != null)
                    programas = "INSERT INTO mapaCurricular (Materia, Programa) VALUES ((select ID from materia ORDER BY id DESC LIMIT 1), '"+materia.programa+"');";
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + agregar
                                    + programas
                                    + "COMMIT;";
                try
                {
                    conn.Open();
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...! Error al agregar la materia a la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public int obtenerUltimoIDMateria()
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT MAX(ID) FROM materia";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error...!\n Error al consultar id de materia a la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public bool actualizarMateria(Materia materia)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE materia SET "+
                    "Nombre='" + materia.nombre + "', Duracion='" + materia.duracion + "', Semestre='" + materia.semestre + "', Costo='" + materia.costo + 
                    "' WHERE ID="+materia.id+";";
                conn.Open();
                try
                {
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...!\n Error al actualizar la materia a la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public bool desactivarMateria(string id)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE materia SET Activo=0 WHERE ID=" + id + ";";
                conn.Open();
                try
                {
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...!\n Error al eliminar la materia a la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerMateriasTable()
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter("SELECT M.ID, M.Nombre, M.Duracion,M.Semestre,M.Costo, P.Nombre AS 'Programa' FROM materia M LEFT JOIN mapaCurricular C ON C.Materia=M.ID LEFT JOIN programa P ON C.Programa=P.Codigo ORDER BY M.ID ASC", conn);
                    conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de las materias de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerMateriasTable(string parameter)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    string sqlString = "SELECT M.ID, M.Nombre, M.Duracion,M.Semestre,M.Costo, P.Nombre AS 'Programa' FROM materia M LEFT JOIN mapaCurricular C ON C.Materia=M.ID LEFT JOIN programa P ON C.Programa=P.Codigo " + 
                        " WHERE " +
                        "(M.ID LIKE '%" + parameter + "%' or " +
                        " M.nombre LIKE '%" + parameter + "%' or " +
                        " M.Semestre LIKE '%" + parameter + "%' or " +
                        " P.Nombre LIKE '%" + parameter + "%' or " +
                        " C.Programa LIKE '%" + parameter + "%') ORDER BY M.ID ASC";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(sqlString, this.conn);
                    this.conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de las materias de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public Materia consultarMateria(string id)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM materia WHERE ID='" + id + "'";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Materia a = new Materia();
                        a.id = reader.GetInt32(0);
                        a.nombre = reader.GetString(1);
                        a.duracion = reader.GetString(2);
                        a.semestre = reader.GetString(3);
                        a.costo = reader.GetDecimal(4);
                        conn.Close();
                        return a;
                    }
                    conn.Close();
                    return null;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener datos de la materia de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public List<Materia> obtenerMaterias()
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM materia";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    List<Materia> aux = new List<Materia>();
                    while (reader.Read())
                    {

                        Materia a = new Materia();
                        a.id = reader.GetInt32(0);
                        a.nombre = reader.GetString(1);
                        a.duracion = reader.GetString(2);
                        a.semestre = reader.GetString(3);
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
                    throw new Exception("Error al obtener datos de las materias de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }

        //-------------------------------PROGRAMA-------------------------------//
        public bool agregarPrograma(Programa programa)
        {
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
                if(programa.MapaCurricular!=null)
                foreach (Materia m in programa.MapaCurricular)
                {
                    materias += "INSERT INTO materia (Nombre,Duracion,Semestre,Costo,Activo) SELECT '" + m.nombre + "', '" + m.duracion+ "', '" + m.semestre+ "', '" + m.costo + "',1 FROM DUAL WHERE NOT EXISTS (SELECT * FROM materia WHERE Nombre='" + m.nombre + "' AND Duracion='" + m.duracion + "' AND Semestre= '" + m.semestre + "' AND Costo= '" + m.costo +"' and Activo=1); ";
                    mapa += "INSERT INTO mapaCurricular (Materia, Programa) VALUES('"+m.id+"','"+programa.Codigo+"');";
                }
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + agregar
                                    + "DELETE FROM mapaCurricular WHERE Programa='"+programa.Codigo+"';"
                                    + materias
                                    + mapa
                                    +"COMMIT;";
                conn.Open();
                try
                {
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
                    throw new Exception("Error...! Error al agregar Prorgrama a la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...! Error al establecer conexión con el servidor");
            }
        }
        public bool actualizarPrograma(Programa programa)
        {
            try
            {
               string update = "UPDATE programa SET "
                    + " RVOE='" + programa.RVOE+ "', CEIFRHS='" + programa.CEIFRHS 
                    + "',Nivel='" + programa.Nivel + "',Nombre='" + programa.Nombre +"',Duracion='" + programa.Duracion
                    + "',Horario='" + programa.Horario + "',Modalidad='" + programa.Modalidad + "',RequisitosEspecialidad='" + programa.RequisitosEspecialidad
                    + "',RequisitosTitulacion='" + programa.RequisitosTitulacion + "',RequisitosDiplomado='" + programa.RequisitosDiplomado + "',Objetivo='" + programa.Objetivo
                    + "',PerfilIngreso='" + programa.PerfilIngreso + "',PerfilEgreso='" + programa.PerfilEgreso
                    + "',ProcesoSeleccion='" + programa.ProcesoSeleccion + "',CostoInscripcionSemestral='" + programa.CostoInscripcionSemestral + "',CostoMensual='" + programa.CostoMensualidad
                    + "',CostoCursoPropedeutico='" + programa.CostoCursoPropedeutico + "'"+
                    "WHERE Codigo='" + programa.Codigo+ "';";
                string materias = "";
                string mapa = "";
                if (programa.MapaCurricular != null)
                foreach (Materia m in programa.MapaCurricular)
                {
                        materias += "INSERT INTO materia (Nombre,Duracion,Semestre,Costo,Activo) SELECT '" + m.nombre + "', '" + m.duracion + "', '" + m.semestre + "', '" + m.costo + "',1 FROM DUAL WHERE NOT EXISTS (SELECT * FROM materia WHERE Nombre='" + m.nombre + "' AND Duracion='" + m.duracion + "' AND Semestre= '" + m.semestre + "' AND Costo= '" + m.costo + "' and Activo=1); ";
                        mapa += "INSERT INTO mapaCurricular (Materia, Programa) VALUES('" + m.id + "','" + programa.Codigo + "');";
                }
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + update
                                    + "DELETE FROM mapaCurricular WHERE Programa='" + programa.Codigo + "';"
                                    + materias
                                    + mapa
                                    + "COMMIT;";
                conn.Open();
                try
                {
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...!\n Error al actualizar la materia a la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public bool desactivarPrograma(string codigo)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE programa SET Activo=0 WHERE Codigo='" + codigo + "';";
                conn.Open();
                try
                {
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...!\n Error al eliminar programa a la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerProgramaTable()
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter("SELECT P.Codigo,P.Nivel, P.Nombre, P.Duracion, P.Horario, P.Modalidad FROM programa P WHERE P.Activo=1", conn);                    conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de programas de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerProgramaTable(string parameter)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    string sqlString = "SELECT P.Codigo,P.Nivel, P.Nombre, P.Duracion, P.Horario, P.Modalidad FROM programa P "+
                        " WHERE " +
                        "(P.Codigo LIKE '%" + parameter + "%' or " +
                        " P.Nivel LIKE '%" + parameter + "%' or " +
                        " P.Nombre LIKE '%" + parameter + "%' or " +
                        " P.Duracion LIKE '%" + parameter + "%' or " +
                        " P.Horario LIKE '%" + parameter + "%' or " +
                        " P.Modalidad LIKE '%" + parameter + "%') AND P.Activo=1";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(sqlString, this.conn);
                    this.conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de las materias de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public Programa consultarPrograma(string codigo)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM programa WHERE Codigo='" + codigo + "'";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos del programa de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public List<Materia> consultarMapaCurricularPrograma(string codigo)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT C.Materia ,M.Nombre,M.Duracion,M.Semestre,M.Costo FROM mapaCurricular C, materia M WHERE C.Materia= M.ID AND C.Programa='" + codigo + "'";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    List<Materia> aux = new List<Materia>();
                    while (reader.Read())
                    {
                        Materia m = new Materia();
                        m.id = reader.GetInt32(0);
                        m.nombre = reader.GetString(1);
                        m.duracion = reader.GetString(2);
                        m.semestre = reader.GetString(3);
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
                    throw new Exception("Error al obtener datos del programa de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public List<Programa> obtenerProgramas()
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM programa";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos de las materias de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public string obtenerNombrePrograma(string codigo)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Nombre FROM programa WHERE Codigo = '" + codigo + "'";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error..! Error al obtener el nombre del programa de la Base de Datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }

        //-------------------------------GRUPOS-------------------------------//
        public bool agregarGrupo(Grupo grupo)
        {
            try
            {
                string agregar = "INSERT INTO grupos (Generacion, Codigo, Programa) VALUES("
                    + " ' " + grupo.generacion + "','" + grupo.codigo + "','" + grupo.programa + "');";
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + agregar
                                    + "COMMIT;";
                conn.Open();
                try
                {
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
                    throw new Exception("Error...! Error al agregar Grupo a la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...! Error al establecer conexión con el servidor");
            }
        }
        public bool actualizarGrupo(Grupo grupo)
        {
            try
            {
                string update = "UPDATE grupos SET Generacion='" + grupo.generacion +
                    "', Programa='" + grupo.programa+ "' WHERE Codigo='" + grupo.codigo + "';";

                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + update
                                    + "COMMIT;";
                conn.Open();
                try
                {
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...!\n Error al actualizar la grupo a la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public bool desactivarGrupo(string codigo)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE grupos SET Activo=0 WHERE Codigo='" + codigo + "';";
                conn.Open();
                try
                {
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...!\n Error al eliminar programa a la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerGruposTable()
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter("SELECT G.Codigo,G.Generacion, P.Nombre FROM grupos G, programa P WHERE G.Activo=1 AND P.Codigo=G.Programa", conn); conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de grupos de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerGruposTable(string parameter)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    string sqlString = "SELECT G.Codigo,G.Generacion, P.Nombre FROM grupos G, programa P WHERE " +
                        "(G.Codigo LIKE '%" + parameter + "%' or " +
                        " P.Nombre LIKE '%" + parameter + "%' or " +
                        " G.Programa LIKE '%" + parameter + "%' or " +
                        " G.Generacion LIKE '%" + parameter + "%') AND G.Activo=1 AND P.Codigo=G.Programa";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(sqlString, this.conn);
                    this.conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de las materias de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public Grupo consultarGrupo(string codigo)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM grupos WHERE Codigo='" + codigo + "'";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos del grupo de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public List<Alumno> obtenerAlumnosGrupos(string codigo)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT A.Nombre, A.Direccion, A.Telefono1, A.Telefono2, A.Correo, A.Facebook, A.CURP, A.RFC, A.Sexo, A.EstadoCivil, A.EscuelaProcedencia, A.Carrera, A.Programa, A.Nivel, A.Fecha, A.Estado, A.Tipo FROM alumnos A, grupoAlumno G WHERE G.Alumno=A.RFC AND G.Grupo ='" + codigo + "';";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos de las grupos de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public List<Grupo> obtenerGrupos()
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM grupos";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos de las grupos de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public List<Grupo> obtenerGrupos(string parameter, string programa)
        {
            try
            {
                string sqlString = "SELECT G.Codigo,G.Generacion, G.Programa FROM grupos G, programa P WHERE " +
                       "(G.Codigo LIKE '%" + parameter + "%' or " +
                       " P.Nombre LIKE '%" + parameter + "%' or " +
                       " G.Programa LIKE '%" + parameter + "%' or " +
                       " P.Codigo LIKE '%" + parameter + "%' or " +
                       " G.Generacion LIKE '%" + parameter + "%') AND G.Activo=1 AND P.Codigo=G.Programa AND P.Codigo='"+programa+"';";
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = sqlString;
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos de las grupos de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }

        //-------------------------------GRUPOS DE ALUMNOS-------------------------//
        public MySqlDataAdapter obtenerAlumnosGruposTable(string grupo)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    string sqlString = "SELECT A.Nombre, A.RFC FROM grupoAlumno G, alumnos A WHERE A.RFC=G.Alumno AND G.Grupo='"+grupo+"';";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(sqlString, this.conn);
                    this.conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos del grupo de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerAlumnosGruposTable(string grupo, string parameter)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    string sqlString = "SELECT A.Nombre, A.RFC FROM grupoAlumno G, alumnos A WHERE " +
                        "(A.Nombre LIKE '%" + parameter + "%' or " +
                        " A.RFC LIKE '%" + parameter + "%') " +
                        //" A.Matricula LIKE '%" + parameter + "%' or " +
                        //" G.Generacion LIKE '%" + parameter + "%') "+
                        "AND A.RFC=G.Alumno AND G.Grupo='"+grupo+"';";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(sqlString, this.conn);
                    this.conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos del grupo de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public string consultarGrupoAlumno(string RFC)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Grupo FROM grupoAlumno WHERE Alumno='" + RFC + "'";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string grupo = reader.GetString(0);
                        conn.Close();
                        return grupo;
                    }
                    conn.Close();
                    return "";
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener datos del alumno de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public bool quitarAlumnoDeGrupo(string grupo, string alumno)
        {
            try
            {
                string delete = "DELETE FROM grupoAlumno WHERE Alumno='" + alumno + "' AND Grupo='" + grupo + "';";
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + delete
                                    + "COMMIT;";
                conn.Open();
                try
                {
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
                    throw new Exception("Error...! Error al agregar Grupo a la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...! Error al establecer conexión con el servidor");
            }
        }

        //-------------------------------TALLERES-------------------------------//
        public bool agregarTaller(Taller taller)
        {
            try
            {
                string agregar = "INSERT INTO taller (Nombre, Fecha, CostoClientes,CostoPublico, Capacidad, Requisitos) VALUES("
                    + " ' " + taller.nombre + "','" + taller.fecha + "','" + taller.costoClientes+ "','" + taller.costoPublico + "','" + taller.capacidad + "','" + taller.requisitos + "');";
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + agregar
                                    + "COMMIT;";
                conn.Open();
                try
                {
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
                    throw new Exception("Error...! Error al agregar el taller a la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...! Error al establecer conexión con el servidor");
            }
        }
        public bool actualizarTaller(Taller taller)
        {
            try
            {
                string update = "UPDATE taller SET Nombre='" + taller.nombre +
                    "', Fecha='" + formatearFecha(taller.fecha) + "', CostoClientes='" + taller.costoClientes +
                    "', CostoPublico='" + taller.costoPublico + "', Capacidad='" + taller.capacidad + 
                    "', Requisitos='" + taller.requisitos
                    + "', Estado='1' WHERE ID='" + taller.id + "';";

                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + update
                                    + "COMMIT;";
                conn.Open();
                try
                {
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...!\n Error al actualizar el taller de la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public bool cancelarTaller(string id)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE taller SET Estado=0 WHERE ID='" + id + "';";
                conn.Open();
                try
                {
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...!\n Error al eliminar taller a la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerTalleresTable()
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter("SELECT ID, Nombre, Fecha, CostoClientes,CostoPublico, Capacidad, Requisitos FROM taller WHERE Estado=1", conn); conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de talleres de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerTalleresTable(string parameter)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
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
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(sqlString, this.conn);
                    this.conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de las materias de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public Taller consultarTaller(string id)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT ID, Nombre, Fecha, CostoClientes, CostoPublico, Capacidad, Requisitos FROM taller WHERE ID='" + id + "'";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos del grupo de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public TallerAsistente obtenerAsistenteTaller(string id)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT A.Nombre,A.Telefono,A.Correo,A.CURP,A.RFC,A.Costo,A.Pago, A.Taller FROM tallerAsistentes A, taller T WHERE T.ID = A.Taller AND A.ID='" + id + "';";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos de los asistentes del taller de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public List<TallerAsistente> obtenerAsistentesTalleres(string taller)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT A.Nombre,A.Telefono,A.Correo,A.CURP,A.RFC,A.Costo,A.Pago FROM tallerAsistentes A, taller T WHERE T.ID = A.Taller AND T.ID='" + taller+"';";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos de los asistentes del taller de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerAsistentesTalleresTable(string taller)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter("SELECT A.ID, A.Nombre,A.Telefono,A.Correo,A.CURP,A.RFC,A.Costo,A.Pago, A.Costo-A.Pago AS 'Restante' FROM tallerAsistentes A, taller T WHERE T.ID = A.Taller AND T.ID='" + taller + "'", conn);
                    conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de talleres de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerAsistentesTalleresTable(string taller, string parameter)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    string sqlString = "SELECT A.ID, A.Nombre,A.Telefono,A.Correo,A.CURP,A.RFC,A.Costo,A.Pago,A.Costo-A.Pago AS 'Restante' FROM tallerAsistentes A, taller T WHERE " +
                        "(A.Nombre LIKE '%" + parameter + "%' or " +
                        " A.Telefono LIKE '%" + parameter + "%' or " +
                        " A.CURP LIKE '%" + parameter + "%' or " +
                        " A.RFC LIKE '%" + parameter + "%' or " +
                        " A.Correo LIKE '%" + parameter + "%') AND  T.ID = A.Taller AND T.ID = '" + taller + "';";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(sqlString, this.conn);
                    this.conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de las materias de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public List<Taller> obtenerTalleres()
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT ID, Nombre, Fecha, CostoClientes, CostoPublico, Capacidad, Requisitos FROM taller WHERE Estado=1";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos de los talleres de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public bool registrarAsistenteTaller(TallerAsistente asistente)
        {
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
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + inscribir
                                    + "COMMIT;";
                conn.Open();
                try
                {
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
                    throw new Exception("Error...! Error al agregar asistencia a la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...! Error al establecer conexión con el servidor");
            }
        }
        public bool borrarAsistenteTaller(string id)
        {
            try
            {
                string inscribir = "DELETE FROM tallerAsistentes WHERE ID="+id+"; ";
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + inscribir
                                    + "COMMIT;";
                conn.Open();
                try
                {
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
                    throw new Exception("Error...! Error al borrar asistencia a la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...! Error al establecer conexión con el servidor");
            }
        }
        public bool registrarPagoAsistenciaTaller(Pago pago, string idAsistente)
        {
            try
            {
                string updateAsistente = "UPDATE tallerAsistentes SET Pago = Pago + " + pago.cantidad +
                                            " WHERE ID = " + idAsistente + ";";
                string agregarPago = "INSERT INTO pagos (Emisor, FechaPago, Cantidad, Concepto, Area, Observaciones, Recibio, Parent_ID) VALUES ('"
                    + pago.emisor + "', '" + formatearFecha(pago.fechaPago) + "'," + pago.cantidad + ", 'Pago de Taller', 'Escuela', '"
                    + pago.observaciones + "', '" + pago.recibio + "', '" + idAsistente + "');";
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + updateAsistente
                                    + agregarPago
                                    + "COMMIT;";
                conn.Open();
                try
                {
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
                    throw new Exception("Error...! Error al agregar Pago de taller a la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...! Error al establecer conexión con el servidor");
            }
        }

        //-------------------------------ENTREGA DOCUMENTOS-------------------------------//
        public bool agregarEntregaDocumentos(DocumentosInscripcion doc)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO documentosInscripcion (Alumno, ActaNacimientoOrg, ActaNacimientoCop, TituloCedulaOrg, TituloLicCop, "
                    + "CedProfCop, SolicitudOpcionTitulacion, CertificadoLicCop, ConstanciaLibSSOrg, Curp, Fotografias, RecibioEmpleado, TipoInscripcion) VALUES ('"
                    + doc.alumno + "', " + doc.actaNacimientoOrg + ", " + doc.actaNacimientoCop + ", " + doc.tituloCedulaOrg + ", " + doc.tituloLicCop + ", "
                    + doc.cedProfCop + ", " + doc.solicitudOpcTitulacion + ", " + doc.certificadoLicCop + ", " + doc.constanciaLibSSOrg + ", " + doc.curp + ", "
                    + doc.fotografias + ", '" + doc.recibioEmpleado + "', "+ doc.tipoInscripcion +")";
                conn.Open();
                try
                {
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception E)
                {
                    throw new Exception("Error al agregar la documentacion del alumnos a la base d datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerEntregaDocumentos()
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter("SELECT Alumno, ActaNacimientoOrg, ActaNacimientoCop, TituloCedulaOrg, TituloLicCop, CedProfCop, SolicitudOpcionTitulacion, CertificadoLicCop, ConstanciaLibSSOrg, Curp, Fotografias, RecibioEmpleado FROM documentosInscripcion", conn);
                    conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de la documentacion entregada del alumno de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerEntregaDocumentosTable(string parameter)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    string sqlString = "SELECT Alumno, ActaNacimientoOrg, ActaNacimientoCop, TituloCedulaOrg, TituloLicCop, CedProfCop, SolicitudOpcionTitulacion, CertificadoLicCop, ConstanciaLibSSOrg, Curp, Fotografias, RecibioEmpleado FROM documentosInscripcion WHERE" +
                        "(Alumno LIKE '%" + parameter + "%' or " +
                        "RecibioEmpleado LIKE '%" + parameter + "%')";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(sqlString, conn);
                    conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de la documentacion entregada del alumno de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public DocumentosInscripcion consultarEntregaDocumentos(string rfc)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM documentosInscripcion WHERE Alumno='" + rfc + "'";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener los datos de la documentacion entregada del alumno de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public bool actualizarEntregaDocumentos(DocumentosInscripcion doc)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE documentosInscripcion SET ActaNacimientoOrg= " + doc.actaNacimientoOrg + ", ActaNacimientoCop= " + doc.actaNacimientoCop + ", TituloCedulaOrg= " + doc.tituloCedulaOrg 
                    + ", TituloLicCop= " + doc.tituloLicCop + ", CedProfCop= " + doc.cedProfCop + ", SolicitudOpcionTitulacion =" + doc.solicitudOpcTitulacion + ", CertificadoLicCop= "+ doc.certificadoLicCop 
                    + ", ConstanciaLibSSOrg =" + doc.constanciaLibSSOrg + ", Curp =" + doc.curp + ", Fotografias =" + doc.fotografias + ", RecibioEmpleado ='" + doc.recibioEmpleado + "' WHERE Alumno = '" +doc.alumno +"'";                    
                try
                {
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al actualizar los datos de la documentacion entregada del alumno en la Base de Datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }

        //-------------------------------EMPLEADOS-------------------------------//
        public bool agregarEmpleado(Empleado empleado, Usuario usuario)
        {
            try
            {
                string empleados = " INSERT INTO empleados (Matricula, Correo, Nombre, Telefono, Puesto) VALUES('"
                    + empleado.Matricula + "','" + empleado.Correo + "','" + empleado.Nombre + "','" + empleado.Telefono + "','" + empleado.Puesto + "');";
                string usuarios = "";
                if (usuario != null) {
                    usuarios = " INSERT INTO usuarios (Estado, Matricula, Usuario, Contrasena, Nivel_Acceso) VALUES(1,'" + empleado.Matricula + "','" + usuario.Nombre_De_Usuario + "','" + usuario.Contrasena + "','" + usuario.Nivel_Acceso + "');";
                }
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; " + empleados + usuarios + " COMMIT;";
                try
                {
                    conn.Open();
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected >= 1)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...! Error al agregar el empleado de la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public bool actualizarEmpleado(Empleado empleado, Usuario usuario)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                string emp = "UPDATE empleados SET Nombre='" + empleado.Nombre + "',Telefono='" + empleado.Telefono + 
                    "',Puesto='" + empleado.Puesto + "',Correo='" + empleado.Correo +
                    "' WHERE ID=" + empleado.ID + " OR Matricula='"+empleado.Matricula+"';";
                string user = "";
                if (usuario != null)
                {
                    user = "INSERT INTO usuarios(Estado, Matricula, Usuario, Contrasena, Nivel_Acceso) SELECT 1,'" + empleado.Matricula + "','" + usuario.Nombre_De_Usuario + "','" + usuario.Contrasena + "','" + usuario.Nivel_Acceso + "' FROM DUAL WHERE NOT EXISTS(SELECT * FROM usuarios WHERE Matricula = '" + empleado.Matricula + "';";
                    user += " UPDATE usuarios SET " + "Usuario='" + usuario.Nombre_De_Usuario + "',Contrasena='" + usuario.Contrasena + "', Estado='"
                    + usuario.Estado + "' WHERE Matricula='" + empleado.Matricula + "';";
                }
                cmd.CommandText = "START TRANSACTION; " +
                    emp + user + "COMMIT;";
                conn.Open();
                try
                {
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected >= 1)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...!\n Error al actualizar el empleado de la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public bool actualizarEmpleado(Empleado empleado)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE empleados SET " +
                    "Nombre='" + empleado.Nombre + "',Telefono='" + empleado.Telefono + "',Puesto='" + empleado.Puesto + "',Correo='" + empleado.Correo +
                    "' WHERE ID=" + empleado.ID + " OR Matricula='" + empleado.Matricula + "';";
                conn.Open();
                try
                {
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...!\n Error al actualizar el empleado de la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public bool desactivarEmpleado(int id)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE empleados SET Estado=0 WHERE ID='" + id + "';";
                conn.Open();
                try
                {
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...!\n Error al eliminar el empleado de la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public bool desactivarEmpleado(string matricula)
        {
            try
            {
                string empleados = "UPDATE empleados SET Estado=0 WHERE Matricula='" + matricula + "';";
                string usuariosQuery = "UPDATE usuarios SET Estado=0 WHERE Matricula='" + matricula + "';";
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();

                cmd.CommandText = "START TRANSACTION; "
                                    + empleados
                                    + usuariosQuery
                                    + "COMMIT;";
                conn.Open();
                try
                {
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...!\n Error al desactivar el usuario de la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerEmpleadosTable()
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter("SELECT E.ID, E.Matricula, E.Nombre,E.Telefono,E.Puesto, E.Correo FROM empleados E WHERE E.Estado = 1", conn);
                    conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de los empleados de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerEmpleadosTable(string parameter)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    string sqlString = "SELECT E.ID, E.Matricula, E.Nombre,E.Telefono,E.Puesto, E.Correo FROM empleados E WHERE " +
                        "(E.Correo LIKE '%" + parameter + "%' or " +
                        " E.Nombre LIKE '%" + parameter + "%' or " +
                        " E.Telefono LIKE '%" + parameter + "%' or " +
                        " E.Puesto LIKE '%" + parameter + "%' or " +
                        " E.Matricula LIKE '%" + parameter + "%') and " +
                        " E.Estado = 1 ORDER BY E.Nombre ASC";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(sqlString, this.conn);
                    this.conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de los empleados de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public Empleado consultarEmpleado(string matricula)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT E.ID, E.Matricula, E.Nombre, E.Telefono, E.Puesto, E.Correo FROM empleados E WHERE E.Matricula='" + matricula + "'";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos del empleado de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public List<Empleado> obtenerEmpleados()
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT E.ID, E.Matricula, E.Nombre, E.Telefono, E.Puesto, E.Correo FROM empleados E WHERE E.Estado = 1";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos de los empleados de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public string obtenerNombreEmpleado(string matricula)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Nombre FROM empleados WHERE Matricula = '" + matricula + "'";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error..! Error al obtener el nombre del empleado de la Base de Datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public string validarMatricula(string matricula)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Matricula FROM empleados WHERE Matricula = '" + matricula + "'; SELECT Matricula FROM psicoterapeutas WHERE Matricula = '" + matricula + "';";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error..! Error al obtener el nombre del empleado de la Base de Datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public string obtenerOrigenMatricula(string matricula)
        { 
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT 'Empleado' FROM empleados E WHERE E.Matricula = '" + matricula + "' UNION SELECT 'Psicoterapeuta' FROM psicoterapeutas P WHERE P.Matricula = '" + matricula + "'";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error..! Error al obtener el tipo de la Base de Datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }


        //-----------------------------INSCRIPCIONES---------------------------------//
        public bool inscribirAlumnoPrograma(string RFC, string programa)
        {
            try
            {
                string agregar = "INSERT INTO programaAlumno (Alumno, Programa, Estado) VALUES("
                    + "','" + RFC + "','"+ programa + "','Inscrito');";
                string updateDatosAlumno = "UPDATE alumno SET Programa = '" + programa + "' WHERE RFC='"+RFC+"';";
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + agregar
                                    + updateDatosAlumno
                                    + "COMMIT;";
                conn.Open();
                try
                {
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
                    throw new Exception("Error...! Error al agregar Prorgrama a la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...! Error al establecer conexión con el servidor");
            }
        }
        public bool inscribirAlumnoGrupo(string RFC, string grupo, string programa)
        {
            try
            {
                string inscribirGrupo = "INSERT INTO grupoAlumno (Alumno, Grupo) VALUES('"
                    + RFC + "','" + grupo + "'); ";
                string inscribirPrograma = "INSERT INTO programaAlumno (Alumno, Programa, Estado) VALUES('"
                    + RFC + "','" + programa + "','Inscrito'); ";
                string updateDatosAlumno = "UPDATE alumnos SET Programa = '" + programa + "' WHERE RFC='" + RFC + "';";
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + inscribirGrupo
                                    + inscribirPrograma
                                    + updateDatosAlumno
                                    + "COMMIT;";
                conn.Open();
                try
                {
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
                    throw new Exception("Error...! Error al agregar Prorgrama a la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...! Error al establecer conexión con el servidor");
            }
        }

        //-------------------------------ASISTENCIAS-------------------------------//
        public List<PaseDeListaAlumno> obtenerAsistenciaAlumnosMateriaTable(string grupo, int materia, List<Alumno> alumnos)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                string sqlString = "SELECT G.Alumno, A.Nombre, P.Estado, P.Fecha,P.isTarde FROM grupoAlumno G INNER JOIN alumnos A on A.RFC=G.Alumno inner JOIN pasesDeListaAlumnos P ON A.RFC=P.Alumno WHERE P.Grupo='" + grupo + "' AND P.Materia=' "+ materia.ToString() + "' ORDER BY P.Fecha ASC;";
                cmd = conn.CreateCommand();
                cmd.CommandText = sqlString;
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                        foreach (PaseDeListaAlumno pl in aux )
                        {
                            if(pl.RFC == rfc)
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
                    throw new Exception("Error al obtener datos de los listas de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public bool registrarAsistencias(List<PaseDeListaAlumno> lista, string maestro, string grupo, string materia, string fecha)
        {
            try
            {
                string paseDeLista = "INSERT INTO pasesDeLista (Grupo, Materia, Fecha, Encargado) VALUES('"
                    + grupo + "','" + materia +"','" + fecha + "','" + maestro + "'); ";
                string paseDeListaAlumnos = "";
                foreach (PaseDeListaAlumno aux in lista)
                {
                    paseDeListaAlumnos += " INSERT INTO pasesDeListaAlumnos (ID,Alumno, Estado, Fecha, Grupo, Materia, isTarde) "
                        + " SELECT AUTO_INCREMENT , '" + aux.RFC + "','" + aux.asistencias.First().Estado + "','" + fecha + "','" + grupo + "','" + materia + "'," + aux.asistencias.First().isTarde + " FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '" + this.database+"' AND TABLE_NAME = 'pasesDeLista'; ";
                }
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + paseDeListaAlumnos
                                    + paseDeLista
                                    + "COMMIT;";
                conn.Open();
                try
                {
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
                    throw new Exception("Error...! Error al registrar asistencias a la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...! Error al establecer conexión con el servidor");
            }
        }
       
        //-------------------------------CALIFICACIONES-------------------------------//
        public List<CalificacionesAlumno> obtenerCalificacionesAlumnosMateriaTable(string grupo, int materia, List<Alumno> alumnos)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                string sqlString = "SELECT G.Alumno, A.Nombre, C.CalificacionTareas, C.CalificacionFinal FROM grupoAlumno G INNER JOIN alumnos A on A.RFC=G.Alumno inner JOIN calificacionAlumno C ON A.RFC=C.Alumno WHERE C.Grupo='" + grupo + "' AND C.Materia='" + materia.ToString() + "' ;";
                cmd = conn.CreateCommand();
                cmd.CommandText = sqlString;
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos de las calificaciones de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public bool registrarCalificaciones(List<CalificacionesAlumno> lista, string grupo, string materia)
        {
            try
            {
                
                string calificaciones = "";
                foreach (CalificacionesAlumno aux in lista)
                {
                    calificaciones += "INSERT INTO calificacionAlumno (Grupo, Materia, Alumno, CalificacionTareas,CalificacionFinal) SELECT '" + grupo + "','" + materia + "','" + aux.RFC + "','" + aux.calificaciones.ElementAt(0).calificacionTareas + "','"
                    + aux.calificaciones.ElementAt(0).calificacionFinal + "' FROM DUAL WHERE NOT EXISTS (SELECT * FROM calificacionAlumno WHERE Grupo='" + grupo + "' AND Materia ='" + materia + "' AND Alumno ='" + aux.RFC + "'); ";

                }
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + calificaciones
                                    + "COMMIT;";
                conn.Open();
                try
                {
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
                    throw new Exception("Error...! Error al agregar calificaciones a la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...! Error al establecer conexión con el servidor");
            }
        }
        public List<Calificacion> obtenerCalificacionesAlumno(string alumno, string grupo)
        {
            try
            {
                string  query = "SELECT M.Nombre , C.Materia, C.CalificacionTareas, C.CalificacionFinal FROM materia M, grupoAlumno G INNER JOIN alumnos A on A.RFC=G.Alumno inner JOIN calificacionAlumno C ON A.RFC=C.Alumno WHERE C.Grupo='" + grupo + "' AND M.ID=C.Materia AND C.Alumno='" + alumno + "' ;";
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = query;
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener calificaciones del alumno de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter consultarCalificacionesAlumno(string alumno, string grupo)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter("SELECT M.Nombre AS 'Materia', C.CalificacionTareas, C.CalificacionFinal FROM materia M, grupoAlumno G INNER JOIN alumnos A on A.RFC=G.Alumno inner JOIN calificacionAlumno C ON A.RFC=C.Alumno WHERE C.Grupo='" + grupo + "' AND M.ID=C.Materia AND C.Alumno='" + alumno + "' ;", conn);
                    conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener calificaciones del alumno de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }

        //-------------------------------PSICOTERAPEUTAS-------------------------------//
        public bool agregarPsicoterapeuta(Psicoterapeuta psicoterapeuta, Usuario usuario)
        {
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
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + psicoterapeutaQuery
                                    + usuariosQuery
                                    + "COMMIT;";
                try
                {
                    conn.Open();
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected >= 1)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...! Error al agregar al Psicoterapeuta de la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public bool actualizarPsicoterapeuta(Psicoterapeuta psicoterapeuta, Usuario usuario)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                string psicoterapeutaQuery = "UPDATE psicoterapeutas SET Matricula='" + psicoterapeuta.Matricula + "', Nombre='" + psicoterapeuta.Nombre +
                    "', Telefono='" + psicoterapeuta.Telefono + "', Carrera='" + psicoterapeuta.Carrera + "', Especialidad='" + psicoterapeuta.Especialidad +
                    "', Horario='" + psicoterapeuta.Horario + "', Observaciones='" + psicoterapeuta.Observaciones + 
                    "', Estado=1 WHERE ID = " + psicoterapeuta.ID + " OR Matricula = '"+psicoterapeuta.Matricula+"';";
                string usuariosQuery = "";
                if (usuario != null)
                {
                    usuariosQuery = "INSERT INTO usuarios(Estado, Matricula, Usuario, Contrasena, Nivel_Acceso) SELECT 1,'" + psicoterapeuta.Matricula + "','" + usuario.Nombre_De_Usuario + "','" + usuario.Contrasena + "','" + usuario.Nivel_Acceso + "' FROM DUAL WHERE NOT EXISTS(SELECT * FROM usuarios WHERE Matricula = '" + psicoterapeuta.Matricula + "';";
                    usuariosQuery += " UPDATE usuarios SET " + "Usuario='" + usuario.Nombre_De_Usuario + "',Contrasena='" + usuario.Contrasena + "', Estado=1 WHERE Matricula='" + psicoterapeuta.Matricula + "';";
                }
                cmd.CommandText = "START TRANSACTION; " +
                                    psicoterapeutaQuery +
                                    usuariosQuery + 
                                    "COMMIT;";
                conn.Open();
                try
                {
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected >= 1)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...!\n Error al actualizar al psicoterapeuta de la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public bool actualizarPsicoterapeuta(Psicoterapeuta psicoterapeuta)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE psicoterapeutas SET Matricula='" + psicoterapeuta.Matricula + "', Nombre='" + psicoterapeuta.Nombre +
                    "', Telefono='" + psicoterapeuta.Telefono + "', Carrera='" + psicoterapeuta.Carrera + "', Especialidad='" + psicoterapeuta.Especialidad +
                    "', Horario='" + psicoterapeuta.Horario + "', Observaciones='" + psicoterapeuta.Observaciones +
                    "', Estado=1 WHERE ID = " + psicoterapeuta.ID + " OR Matricula = '" + psicoterapeuta.Matricula + "';";
                conn.Open();
                try
                {
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...!\n Error al actualizar al psicoterapeuta de la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public bool desactivarPsicoterapeuta(int id)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE psicoterapeutas SET Estado=0 WHERE ID=" + id + ";";
                conn.Open();
                try
                {
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...!\n Error al desactivar al psicoterapeuta de la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public bool desactivarPsicoterapeuta(string matricula)
        {
            try
            {
                string psicoterapeutasQuery = "UPDATE psicoterapeutas SET Estado=0 WHERE Matricula='" + matricula + "';";
                string usuariosQuery = "UPDATE usuarios SET Estado=0 WHERE Matricula='" + matricula + "';";
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + psicoterapeutasQuery
                                    + usuariosQuery
                                    + "COMMIT;";
                conn.Open();
                try
                {
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...!\n Error al desactivar al psicoterapeuta de la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerPsicoterapeutasTable()
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter("SELECT ID, Matricula, Nombre, Telefono, Carrera, Especialidad, Horario FROM psicoterapeutas WHERE Estado = 1", conn);
                    conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de los psicoterapeutas de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerPsicoterapeutasTable(string parameter)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    string sqlString = "SELECT ID, Matricula, Nombre, Telefono, Carrera, Especialidad, Horario FROM psicoterapeutas WHERE "+
                        "(Matricula LIKE '%" + parameter + "%' or " +
                        " Nombre LIKE '%" + parameter + "%' or " +
                        " Telefono LIKE '%" + parameter + "%' or " +
                        " Carrera LIKE '%" + parameter + "%' or " +
                        " Horario LIKE '%" + parameter + "%' or " +
                        " Especialidad  LIKE '%" + parameter + "%') and " +
                        " Estado = 1";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(sqlString, this.conn);
                    this.conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de los psicoterapeutas de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public Psicoterapeuta consultarPsicoterapeuta(string matricula)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT ID, Matricula, Nombre, Telefono, Carrera, Especialidad, Horario, Observaciones, Estado FROM psicoterapeutas WHERE Matricula='" + matricula + "'";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos del Psicoterapeuta de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public List<Psicoterapeuta> obtenerPsicoterapeutas()
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT ID, Matricula, Nombre, Telefono, Carrera, Especialidad, Horario, Observaciones, Estado FROM psicoterapeutas WHERE Estado = 1";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos de los Psicoterapeuta de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerConsultasPsicoterapeutaTable(string matricula)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    string query = "";
                    query+="SELECT S.Fecha, concat_ws(' ',P.Nombre, P.Apellidos) AS 'Paciente', 'Sesión' AS 'Tipo' FROM sesiones S INNER JOIN pacientes P on S.Paciente_ID= P.ID WHERE S.Psicoterapeuta_ID = '" + matricula + "' AND S.Estado = 'Activa' ";
                    query += "UNION SELECT E.Fecha, concat_ws(' ',P.Nombre, P.Apellidos) AS 'Paciente', 'Evaluación' AS 'Tipo' FROM evaluaciones E INNER JOIN pacientes P on E.Paciente_ID= P.ID WHERE E.Psicoterapeuta_ID = '" + matricula + "' AND E.Estado = 'Activa' ";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(query, conn);
                    conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de los psicoterapeutas de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerPacientesPsicoterapeutaTable(string matricula)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter("SELECT P.ID, concat_ws(' ',P.Nombre, P.Apellidos) AS 'Nombre', P.EscuelaEmpresa AS 'Institucion',P.Telefono FROM pacientes P  WHERE P.Psicoterapeuta='"+matricula+"' AND P.Estado = 1", conn);
                    conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de pacientes de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerPacientesPsicoterapeutaTable(string matricula, string parameter)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    string query = "SELECT P.ID, concat_ws(' ',P.Nombre, P.Apellidos) AS 'Nombre', P.EscuelaEmpresa AS 'Institucion',P.Telefono FROM pacientes P WHERE P.Psicoterapeuta='" + matricula + "' AND P.Estado = 1 AND (" +
                        " P.ID LIKE '%" + parameter + "%' or " +
                        " P.EscuelaEmpresa LIKE '%" + parameter + "%' or " +
                        " P.Telefono LIKE '%" + parameter + "%' or " +
                        " concat_ws(' ',P.Nombre, P.Apellidos) LIKE '%" + parameter + "%');";
                        ;
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(query, conn);
                    conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de pacientes de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }

        //--------------------------------NOMINA--------------------------------------//
        public List<decimal> obtenerConsultasPsicoterapeutaPendientes(string matricula,DateTime inicio ,DateTime fin)
        {
            try
            {
                
                try
                {
                    string query = "";
                    query += "SELECT S.Costo FROM sesiones S WHERE S.Psicoterapeuta_ID = '" + matricula + "' AND S.Estado = 'Activa' AND S.Fecha BETWEEN '"+formatearFecha(inicio)+"' AND '"+formatearFecha(fin)+"' ";
                    query += "UNION SELECT E.Costo FROM evaluaciones E WHERE E.Psicoterapeuta_ID = '" + matricula + "' AND E.Estado = 'Activa' AND E.Fecha BETWEEN '" + formatearFecha(inicio) + "' AND '" + formatearFecha(fin) + "'";
                    conn = new MySqlConnection(builder.ToString());
                    cmd = conn.CreateCommand();
                    cmd.CommandText = query;
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    List<decimal> aux = new List<decimal>();
                    while (reader.Read())
                    {
                        aux.Add(reader.GetDecimal(0));
                    }
                    conn.Close();
                    if (aux.Count != 0)
                        return aux;
                    else
                        return null;
            }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de la nomina de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public bool agregarNomina(Nomina nomina)
        {
            try
            {
                 string nominaQuery = "INSERT INTO nominas (Psicoterapeuta,	DiaEntrega,	FechaInicio, FechaFin, Total, Estado) VALUES("
                        + nomina.Psicoterapeutas + ",'"+ formatearFecha(nomina.DiaEntrega) + "','" + formatearFecha(nomina.FechaInicio) + "','" + formatearFecha(nomina.FechaFin) + "'," +nomina.Total+",'Pagada');";
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + nominaQuery
                                    + "COMMIT;";
                try
                {
                    conn.Open();
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected >= 1)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...! Error al agregar pago de nomina a la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public bool actualizarNomina(Nomina nomina)
        {
            try
            {
                 string nominaQuery = "UPDATE nominas SET=Psicoterapeuta='"+ nomina.Psicoterapeutas + "', DiaEntrega='"+ formatearFecha(nomina.DiaEntrega) +
                    "',	FechaInicio='" + formatearFecha(nomina.FechaInicio) + "', FechaFin='" + formatearFecha(nomina.FechaFin) + "', Total=" + nomina.Total +", Estado='Pagada';";
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + nominaQuery
                                    + "COMMIT;";
                try
                {
                    conn.Open();
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected >= 1)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...! Error al agregar pago de nomina a la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public DateTime consultarUltimaFechaNomina(string matricula)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT DiaEntrega FROM nominas WHERE Psicoterapeuta='" + matricula + "' ORDER BY DiaEntrega DESC LIMIT 1";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DateTime date = reader.GetDateTime(0);
                        conn.Close();
                        return date;
                    }
                    conn.Close();
                    return new DateTime(2000,01,01);
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener datos de nomina de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerNominaPacienteTable(string matricula,string parameter)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    string query = "SELECT ID,DiaEntrega,FechaInicio,FechaFin,Total FROM nominas WHERE Psicoterapeuta='" + matricula + "' AND " +
                        " (ID LIKE '%" + parameter + "%' or " +
                        " DiaEntrega LIKE '%" + parameter + "%' or " +
                        " Total LIKE '%" + parameter + "%' );";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(query, conn);
                    conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de nomina de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerNominaPacienteTable(string matricula)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter("SELECT ID,DiaEntrega,FechaInicio,FechaFin,Total FROM nominas WHERE Psicoterapeuta='" + matricula + "'", conn);
                    conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de nomina de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }


        //------------------------------PACIENTES------------------------------------//
        public bool agregarPaciente(Paciente paciente)
        {
            try
            {
                string pacienteQuery = "INSERT INTO pacientes (Nombre, Apellidos, FechaNacimiento, EscuelaEmpresa, CostoPaciente, Telefono, TutorNombre, TutorTelefono, Psicoterapeuta) VALUES ('"
                    + paciente.nombre + "','" + paciente.apellidos + "','" + formatearFecha(paciente.fechaNacimiento) + "','" + paciente.institucion + "','" + paciente.costoEspecial + "','" + paciente.telefono + "', '" + paciente.nombre_tutor + "','" + paciente.telefono_tutor + "'," + paciente.psicoterapeuta + "); ";
                if(paciente.psicoterapeuta==null)
                    pacienteQuery = "INSERT INTO pacientes (Nombre, Apellidos, FechaNacimiento, EscuelaEmpresa, CostoPaciente, Telefono, TutorNombre, TutorTelefono, Psicoterapeuta) VALUES ('" 
                        + paciente.nombre + "','" + paciente.apellidos + "','" + formatearFecha(paciente.fechaNacimiento) + "','" + paciente.institucion + "','" + paciente.costoEspecial + "','" + paciente.telefono + "', '" + paciente.nombre_tutor + "','" + paciente.telefono_tutor + "',NULL); ";
                string facturacionQuery = "";
                if (paciente.datos_facturacion != null)
                {
                    facturacionQuery = " INSERT INTO datosFacturacion (PacienteID, RFC, Nombre, RazonSocial, Direccion)"
                    + " SELECT AUTO_INCREMENT-1, '" + paciente.datos_facturacion[0] + "','" + paciente.datos_facturacion[1] + "','" + paciente.datos_facturacion[2] + "','" + paciente.datos_facturacion[3] + "' FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '" + this.database + "' AND TABLE_NAME = 'pacientes'; "; ;
                }
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; " 
                                    +pacienteQuery 
                                    + facturacionQuery 
                                    +"COMMIT;";
                try
                {
                    conn.Open();
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected >= 1)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...! Error al agregar datos de praciente de la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public bool actualizarPaciente(Paciente paciente)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                string facturacionQuery = "";
                string pacienteQuery = "UPDATE pacientes SET Nombre='" + paciente.nombre + "' ,Apellidos='" + paciente.apellidos + "',FechaNacimiento="+formatearFecha(paciente.fechaNacimiento)+"',EscuelaEmpresa='" + paciente.institucion
                    + "',CostoPaciente='" + paciente.costoEspecial + "',Telefono='" + paciente.telefono + "',Estado=1, TutorNombre='"+paciente.nombre_tutor+
                    "',TutorTelefono='"+paciente.telefono_tutor+"',Psicoterapeuta='" + paciente.psicoterapeuta + "' WHERE ID=" + paciente.id + ";";
                if(paciente.psicoterapeuta==null)
                    pacienteQuery = "UPDATE pacientes SET Nombre='" + paciente.nombre + "' ,Apellidos='" + paciente.apellidos + "',EscuelaEmpresa='" + paciente.institucion
                    + "',CostoPaciente='" + paciente.costoEspecial + "',Telefono='" + paciente.telefono + "',Estado=1, TutorNombre='" + paciente.nombre_tutor +
                    "',TutorTelefono='" + paciente.telefono_tutor + "',Psicoterapeuta=NULL WHERE ID=" + paciente.id + ";";
                if (paciente.datos_facturacion != null)
                {
                    

                    facturacionQuery = " INSERT INTO datosFacturacion (PacienteID, RFC, Nombre, RazonSocial, Direccion) SELECT " + paciente.id + ",'"
                        + paciente.datos_facturacion[0] +"', '" + paciente.datos_facturacion[1] + "', '" + paciente.datos_facturacion[2] + "', '" + paciente.datos_facturacion[3] +
                        "' FROM DUAL WHERE NOT EXISTS (SELECT * FROM datosFacturacion WHERE PacienteID =  " +paciente.id+ ");";
                }
                cmd.CommandText = "START TRANSACTION; "
                                    +pacienteQuery 
                                    + facturacionQuery
                                    + "COMMIT;";
                conn.Open();
                try
                {
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...!\n Error al actualizar el paciente de la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public bool eliminarPaciente(string id)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE pacientes SET Estado=0 WHERE ID =" + id + ";";
                conn.Open();
                try
                {
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...!\n Error al desactivar el usuario de la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerPacientesTable()
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter("SELECT P.ID, concat_ws(' ',P.Nombre, P.Apellidos) AS 'Nombre', P.EscuelaEmpresa AS 'Institucion',P.Telefono, T.Nombre AS 'Psicoterapeuta' FROM pacientes P LEFT JOIN psicoterapeutas T on P.Psicoterapeuta=T.ID  WHERE P.Estado = 1", conn);
                    conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de pacientes de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerPacientesTable(string parameter)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    string sqlString = "SELECT P.ID, concat_ws(' ',P.Nombre, P.Apellidos) AS 'Nombre', P.EscuelaEmpresa AS 'Institucion',P.Telefono, T.Nombre AS 'Psicoterapeuta' FROM pacientes P LEFT JOIN psicoterapeutas T on P.Psicoterapeuta=T.ID  WHERE " +
                        "(P.Nombre LIKE '%" + parameter + "%' or " +
                        " P.Apellidos LIKE '%" + parameter + "%' or " +
                        " P.EscuelaEmpresa LIKE '%" + parameter + "%' or " +
                        " P.Telefono LIKE '%" + parameter + "%' or " +
                        " T.Nombre LIKE '%"  + parameter + "%') AND " +
                        " (P.Estado = 1)";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(sqlString, this.conn);
                    this.conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de los empleados de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public Paciente consultarPaciente(string ID)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT P.ID, P.Nombre, P.Apellidos, P.FechaNacimiento, P.EscuelaEmpresa, P.CostoPaciente, P.Telefono, P.Estado, P.TutorNombre, P.TutorTelefono, P.Psicoterapeuta, F.RFC,F.Nombre,F.RazonSocial,F.Direccion FROM pacientes P LEFT JOIN datosFacturacion F ON P.ID = F.PacienteID WHERE P.ID=" + ID + ";";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                        try {
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
                   throw new Exception("Error al obtener datos del paciente de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public List<Paciente> obtenerPacientes()
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT P.ID, P.Nombre, P.Apellidos,P.FechaNacimiento, P.EscuelaEmpresa, P.CostoPaciente, P.Telefono, P.Estado, P.TutorNombre, P.TutorTelefono, P.Psicoterapeuta, F.RFC,F.Nombre,F.RazonSocial,F.Direccion FROM pacientes P LEFT JOIN datosFacturacion F ON P.ID = F.PacienteID WHERE P.Estado = 1";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos de los pacientes de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }

        //---------------------------SESIONES------------------------//
        public bool agregarSesion(Sesion sesion)
        {
            try
            {
                string reservacionQuery = "";
                string sesionQuery="";
                if (sesion.reservacion != null)
                {
                    reservacionQuery = "UPDATE reservaciones SET ID_Parent = (SELECT AUTO_INCREMENT FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '" + this.database + "' AND TABLE_NAME = 'sesiones') WHERE ID=" + sesion.reservacion.id + ";";
                    sesionQuery = " INSERT INTO sesiones ( Reservacion_ID, Costo, Pago, Pendiente ,Fecha, Hora, Tipo, Observaciones, Paciente_ID, Psicoterapeuta_ID, Estado)"
                        + " VALUES ('" + sesion.reservacion.id + "','" + sesion.Costo + "','" + sesion.Pago + "', Costo-Pago,'" + formatearFecha(sesion.fecha) + "','" + sesion.hora + "','" + sesion.tipo + "','" + sesion.observaciones + "','" + sesion.paciente + "','" + sesion.psicoterapeuta + "','Activa');";
                }else
                {
                    sesionQuery = " INSERT INTO sesiones ( Costo, Pago, Pendiente, Fecha, Hora, Tipo, Observaciones, Paciente_ID, Psicoterapeuta_ID, Estado)"
                        + " VALUES ('" + sesion.Costo + "','" + sesion.Pago + "', Costo-Pago,'" + formatearFecha(sesion.fecha) + "','" + sesion.hora + "','" + sesion.tipo + "','" + sesion.observaciones + "','" + sesion.paciente + "','" + sesion.psicoterapeuta + "','Activa');";
                }
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + reservacionQuery
                                    + sesionQuery
                                    + "COMMIT;";
                try
                {
                    conn.Open();
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected >= 1)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...! Error al agregar datos de sesión de la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public bool actualizarSesion(Sesion sesion)
        {
            try
            {
                string reservacionQuery = "";
                if (sesion.reservacion != null)
                    reservacionQuery = "UPDATE reservaciones SET Reservante='" + sesion.reservacion.reservante + "', Fecha='" + formatearFecha(sesion.reservacion.fecha) +
                         "Codigo_Reservacion='"+sesion.reservacion.codigo_Reservacion+ "', Hora_Inicio='"+sesion.reservacion.hora_Inicio+"', Duracion='"+sesion.reservacion.duracion+
                        "', Hora_Fin='"+sesion.reservacion.hora_Fin+"', Concepto='"+sesion.reservacion.concepto+"', ID_Parent='" +sesion.id+
                        "', Ubicacion='" + sesion.reservacion.ubicacion + "', Observaciones='" + sesion.reservacion.observaciones +
                        "' WHERE ID="+sesion.reservacion.id+"; ";
                string sesionQuery  = " UPDATE sesiones SET Costo='" + sesion.Costo+ "', Tipo='" + sesion.tipo + "',Fecha='" + formatearFecha(sesion.fecha) + "',Hora='" + sesion.hora + "', Observaciones='" + sesion.observaciones + 
                    "',Paciente='" + sesion.paciente + "',Psicoterapeuta='" + sesion.psicoterapeuta + "',Estado='" + sesion.estado + "' WHERE ID="+sesion.id+"; " ;
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + reservacionQuery
                                    + sesionQuery
                                    + "COMMIT;";
                try
                {
                    conn.Open();
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected >= 1)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...! Error al actualizar datos de sesion de la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public bool cancelarSesion(string id)
        {
            try
            {
                string sesionQuery = " UPDATE sesiones SET Estado='Cancelada' WHERE ID=" + id + "; ";
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + sesionQuery
                                    + "COMMIT;";
                try
                {
                    conn.Open();
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected >= 1)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...! Error al actualizar datos de sesion de la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerSesionesTable()
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter("SELECT S.ID,  S.Tipo, E.Fecha, concat_ws(' ',P.Nombre, P.Apellidos) AS 'Paciente', S.Costo, S.Observaciones, Ps.Nombre AS 'Psicoterapeuta' FROM sesiones S INNER JOIN pacientes P ON S.Paciente_ID=P.ID INNER JOIN psicoterapeutas Ps on S.Psicoterapeuta_ID=Ps.ID WHERE S.Estado = 'Activa'", conn);
                    conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de sesiones de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerSesionesTable(string parameter)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    string sqlString = "SELECT S.ID,  S.Tipo, E.Fecha, concat_ws(' ',P.Nombre, P.Apellidos) AS 'Paciente', S.Costo, S.Observaciones, Ps.Nombre AS 'Psicoterapeuta' FROM sesiones S INNER JOIN pacientes P ON S.Paciente_ID=P.ID INNER JOIN psicoterapeutas Ps on S.Psicoterapeuta_ID=Ps.ID WHERE " +
                        "(S.Tipo LIKE '%" + parameter + "%' or " +
                        " E.Fecha LIKE '%" + parameter + "%' or " +
                        " concat_ws(' ',P.Nombre, P.Apellidos) LIKE '%" + parameter + "%' or " +
                        " Ps.Nombre LIKE '%" + parameter + "%') AND " +
                        " (S.Estado = 'Activa')";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(sqlString, this.conn);
                    this.conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de los sesiones de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public Sesion consultarSesion(string ID)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT S.ID, S.Costo,S.Tipo, S.Observaciones, S.Paciente_ID, S.Psicoterapeuta_ID, S.Estado, R.ID, R.Reservante, R.Fecha,R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones, S.Fecha, S.Hora, S.Pago, S.Pendiente FROM sesiones S LEFT JOIN reservaciones R ON R.ID=S.Reservacion_ID WHERE S.ID=" + ID + ";";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos de la sesion de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerSesionesPacienteTable(string paciente)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                string sqlString = "SELECT S.ID, S.Tipo, S.Psicoterapeuta_ID,S.Fecha, S.Hora, S.Observaciones FROM sesiones S WHERE S.Paciente_ID=" + paciente + " AND S.Estado='Activa'  ORDER BY S.Fecha ASC;";
                conn.Open();
                try
                {
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(sqlString, conn);
                    this.conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener datos de las sesiones de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public List<Sesion> obtenerSesionesPaciente(string paciente)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT S.ID, S.Costo,S.Tipo, S.Observaciones, S.Paciente_ID, S.Psicoterapeuta_ID, S.Estado, R.ID, R.Reservante, R.Fecha, R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones, S.Fecha, S.Hora, S.Pago, S.Pendiente FROM sesiones S LEFT JOIN reservaciones R ON R.ID=S.Reservacion_ID WHERE S.Paciente_ID=" + paciente + ";";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos de las sesiones de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public List<Sesion> obtenerSesionesPendietesDePagoPaciente(string paciente)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT S.ID, S.Costo,S.Tipo, S.Observaciones, S.Paciente_ID, S.Psicoterapeuta_ID, S.Estado, R.ID, R.Reservante, R.Fecha, R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones, S.Fecha, S.Hora, S.Pago, S.Pendiente FROM sesiones S LEFT JOIN reservaciones R ON R.ID=S.Reservacion_ID WHERE S.Paciente_ID=" + paciente + " AND S.Pendiente > 0.0 ORDER BY S.Fecha DESC;";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos de las sesiones de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public bool registrarPagoDeSesion(Pago pago, List<Sesion> sesionesPagadas)
        {
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
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + sesionesPagadasQuery
                                    + agregarPago
                                    + "COMMIT;";
                conn.Open();
                try
                {
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
                    throw new Exception("Error...! Error al agregar Pago de sesiones a la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...! Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerPagosPacienteTable(string paciente)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter("SELECT ID, Emisor, FechaPago AS 'Fecha De Pago', Cantidad, Observaciones, Recibio FROM pagos WHERE Concepto='Pago de sesion' AND Area='Psicoterapia' AND Parent_ID='"+paciente+"';", conn);
                    conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de los pagos de sesiones de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerPagosPacienteTable(string paciente, string parameter)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    string sqlString = "SELECT ID, Emisor, FechaPago AS 'Fecha De Pago', Cantidad, Observaciones, Recibio FROM pagos WHERE Concepto='Pago de sesion' AND Area='Psicoterapia' AND Parent_ID='" + paciente + "' AND "+
                        "(Emisor LIKE '%" + parameter + "%' or " +
                        "Cantidad LIKE '%" + parameter + "%' or " +
                        "FechaPago LIKE '%" + parameter + "%' or " +
                        "Recibio LIKE '%" + parameter + "%')";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(sqlString, conn);
                    conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de los pagos de sesiones de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerPagosSesionesPacienteTable(string paciente)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter("SELECT S.ID, S.Fecha, S.Hora, S.Costo, S.Pago, S.Pendiente FROM sesiones S WHERE S.Paciente_ID=" + paciente + " AND S.Estado='Activa'  ORDER BY S.Fecha ASC;", conn);
                    conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de sesiones de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerPagosSesionesPacienteTable(string paciente, string parameter)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    string sqlString = "SELECT S.ID, S.Fecha, S.Hora, S.Costo, S.Pago, S.Pendiente FROM sesiones S WHERE S.Paciente_ID=" + paciente + " AND S.Estado='Activa' AND " +
                        "(S.ID LIKE '%" + parameter + "%' or " +
                        "S.Fecha LIKE '%" + parameter + "%' or " +
                        "S.Psicoterapeuta_ID LIKE '%" + parameter + "%')  ORDER BY S.Fecha ASC;";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(sqlString, conn);
                    conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de sesiones de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        //---------------------------EVALUACIONES------------------------//
        public bool agregarEvaluacion(Evaluacion evaluacion)
        {
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
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + reservacionQuery
                                    + evaluacionQuery
                                    + "COMMIT;";
                try
                {
                    conn.Open();
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected >= 1)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...! Error al agregar datos de sesión de la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public bool actualizarEvaluacion(Evaluacion evaluacion)
        {
            try
            {
                string reservacionQuery="";
                if (evaluacion.reservacion!=null)
                    reservacionQuery = "UPDATE reservaciones SET Reservante='" + evaluacion.reservacion.reservante + "', Fecha='" + formatearFecha(evaluacion.reservacion.fecha) +
                        "Codigo_Reservacion='" + evaluacion.reservacion.codigo_Reservacion + "', Hora_Inicio='" + evaluacion.reservacion.hora_Inicio + "', Duracion='" + evaluacion.reservacion.duracion +
                       "', Hora_Fin='" + evaluacion.reservacion.hora_Fin + "', Concepto='" + evaluacion.reservacion.concepto + "', ID_Parent='" + evaluacion.id +
                       "', Ubicacion='" + evaluacion.reservacion.ubicacion + "', Observaciones='" + evaluacion.reservacion.observaciones +
                       "' WHERE ID=" + evaluacion.reservacion.id + "; ";
                string evaluacionQuery = " UPDATE evaluaciones SET Costo='" + evaluacion.costo + "', Pruebas='" + evaluacion.pruebas + "', Observaciones='" + evaluacion.observaciones +
                   "', Paciente_ID'" + evaluacion.paciente + "', Psicoterapeuta_ID'" + evaluacion.psicoterapeuta + "',Fecha='" + formatearFecha(evaluacion.fecha) + "',Hora='" + evaluacion.hora + "', Estado='Activa' WHERE ID="+evaluacion.id +"; ";
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + reservacionQuery
                                    + evaluacionQuery
                                    + "COMMIT;";
                try
                {
                    conn.Open();
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected >= 1)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...! Error al actualizar datos de sesion de la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public bool cancelarEvaluacion(Evaluacion evaluacion)
        {
            try
            {
                string reservacionQuery = "DELETE FROM reservaciones WHERE ID=" + evaluacion.reservacion.id + "; ";
                string evaluacionQuery = " UPDATE evaluacion SET Estado='Cancelada' WHERE ID=" + evaluacion.id + "; ";
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + reservacionQuery
                                    + evaluacionQuery
                                    + "COMMIT;";
                try
                {
                    conn.Open();
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected >= 1)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...! Error al actualizar datos de sesion de la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerEvaluacionTable()
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter("SELECT E.ID, E.Pruebas, E.Fecha, concat_ws(' ',P.Nombre, P.Apellidos) AS 'Paciente', E.Costo, E.Observaciones, Ps.Nombre AS 'Psicoterapeuta' FROM evaluaciones E INNER JOIN pacientes P ON E.Paciente_ID=P.ID INNER JOIN psicoterapeutas Ps on E.Psicoterapeuta_ID=Ps.ID WHERE E.Estado = 'Activa';", conn);
                    conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de evaluaciones de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerEvaluacionTable(string parameter)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    string sqlString = "SELECT E.ID, E.Pruebas, E.Fecha, concat_ws(' ',P.Nombre, P.Apellidos) AS 'Paciente', E.Costo, E.Observaciones, Ps.Nombre AS 'Psicoterapeuta' FROM evaluaciones E INNER JOIN pacientes P ON E.Paciente_ID=P.ID INNER JOIN psicoterapeutas Ps on E.Psicoterapeuta_ID=Ps.ID WHERE " +
                        "(E.Pruebas LIKE '%" + parameter + "%' or " +
                        " E.Fecha LIKE '%" + parameter + "%' or " +
                        " concat_ws(' ',P.Nombre, P.Apellidos) LIKE '%" + parameter + "%' or " +
                        " Ps.Nombre LIKE '%" + parameter + "%') AND " +
                        " (E.Estado = 'Activa')";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(sqlString, this.conn);
                    this.conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de los evaluaciones de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public Evaluacion consultarEvaluacion(string ID)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT E.ID, E.Costo, E.Pruebas, E.Observaciones, E.Paciente_ID, E.Psicoterapeuta_ID, E.Estado, R.ID, R.Reservante, R.Fecha, R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones, E.Fecha, E.Hora FROM evaluaciones E LEFT JOIN reservaciones R ON R.ID=E.Reservacion_ID WHERE E.ID=" + ID + ";";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos de la evaluacion de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerEvaluacionPacienteTable(string paciente)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    string sqlString = "SELECT E.ID, E.Pruebas, E.Fecha, E.Observaciones, Ps.Nombre AS 'Psicoterapeuta' FROM evaluaciones E INNER JOIN pacientes P ON E.Paciente_ID=P.ID INNER JOIN psicoterapeutas Ps on E.Psicoterapeuta_ID=Ps.ID WHERE E.Paciente_ID="+paciente+" AND E.Estado = 'Activa';";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(sqlString, this.conn);
                    this.conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de los evaluaciones de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerEvaluacionPacienteTable(string paciente, string parameter)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    string sqlString = "SELECT E.ID, E.Pruebas, E.Fecha, E.Observaciones, Ps.Nombre AS 'Psicoterapeuta' FROM evaluaciones E INNER JOIN pacientes P ON E.Paciente_ID=P.ID INNER JOIN psicoterapeutas Ps on E.Psicoterapeuta_ID=Ps.Matricula WHERE E.Paciente_ID="+paciente+" AND E.Estado = 'Activa' AND ("+
                        "E.ID LIKE '%" + parameter + "%' or " +
                        "E.Fecha LIKE '%" + parameter + "%' or " +
                        "E.Pruebas LIKE '%" + parameter + "%' or " +
                        "E.Observaciones LIKE '%" + parameter + "%' or " +
                        "Ps.Nombre LIKE '%" + parameter + "%');";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(sqlString, this.conn);
                    this.conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de los evaluaciones de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public List<Evaluacion> obtenerEvaluacionPaciente(string paciente)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT E.ID, E.Costo, E.Pruebas, E.Observaciones, E.Paciente_ID, E.Psicoterapeuta_ID, E.Estado, R.ID, R.Reservante, R.Fecha,R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones, E.Fecha, E.Hora FROM evaluaciones E LEFT JOIN reservaciones R ON R.ID=E.Reservacion_ID WHERE E.Paciente_ID=" + paciente + ";";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos de las evaluaciones de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }

        //-------------------------------CLUB DE TAREAS-------------------------------//
        public bool agregarClubDeTareas(ClubDeTareas club)
        {
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
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + reservacionQuery
                                    + clubTareasQuery
                                    + "COMMIT;";
                conn.Open();
                try
                {
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
                    throw new Exception("Error...! Error al agregar el club a la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...! Error al establecer conexión con el servidor");
            }
        }
        public bool actualizarClubDeTareas(ClubDeTareas club)
        {
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
                    "', Observaciones='" + club.Observaciones +"', Estado='Activo' WHERE ID='" + club.ID + "';";

                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + reservacionQuery
                                    + update
                                    + "COMMIT;";
                conn.Open();
                try
                {
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...!\n Error al actualizar el club de la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public bool cancelarClubDeTareas(string id)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE clubDeTareas SET Estado='Cancelado' WHERE ID='" + id + "';";
                conn.Open();
                try
                {
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
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerClubDeTareasTable()
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter("SELECT C.ID, C.Fecha, C.Hora, C.Costo, P.Nombre, C.Observaciones FROM clubDeTareas C INNER JOIN empleados P ON P.ID=C.Psicoterapeuta WHERE C.Estado='Activo'", conn); conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de clubes de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerClubDeTareasTable(string parameter)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    string sqlString = "SELECT C.ID, C.Fecha, C.Hora, C.Costo, P.Nombre, C.Observaciones FROM clubDeTareas C INNER JOIN empleados P ON P.ID=C.Psicoterapeuta WHERE " +
                        "(C.ID LIKE '%" + parameter + "%' or " +
                        " P.Nombre LIKE '%" + parameter + "%' or " +
                        " C.Fecha LIKE '%" + parameter + "%') AND C.Estado='Activo'";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(sqlString, this.conn);
                    this.conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de clubes de tarea de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public ClubDeTareas consultarClubDeTareas(string id)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT C.ID, C.Fecha, C.Hora, C.Costo, C.Psicoterapeuta, C.Observaciones, C.Estado ,R.ID, R.Reservante, R.Fecha, R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones FROM clubDeTareas C LEFT JOIN reservaciones R ON R.ID=C.Reservacion_ID WHERE C.ID='" + id + "'";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos del grupo de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public ClubDeTareasAsistente obtenerAsistenteClubDeTareas(string id)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Club_Tareas_ID, Nombre, Apellidos, Nombre_Tutor, Telefono_Tutor, Costo, Pago, Observaciones FROM ClubDeTareasAsistente WHERE ID='" + id + "';";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos de los asistentes del club de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public List<ClubDeTareasAsistente> obtenerAsistentesClubDeTareas(string club)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT ID, Club_Tareas_ID, Nombre, Apellidos, Nombre_Tutor, Telefono_Tutor, Costo, Pago, Observaciones FROM ClubDeTareasAsistente WHERE Club_Tareas_ID='" + club + "';";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos de los asistentes del club de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerAsistentesClubDeTareasTable(string club)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter("SELECT ID, Club_Tareas_ID, Nombre, Apellidos, Nombre_Tutor, Telefono_Tutor, Costo, Pago, Observaciones FROM ClubDeTareasAsistente WHERE Club_Tareas_ID='" + club + "'", conn); conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de clubes de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerAsistentesClubDeTareasTable(string club, string parameter)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    string sqlString = "SELECT ID, Club_Tareas_ID, Nombre, Apellidos, Nombre_Tutor, Telefono_Tutor, Costo, Pago, Observaciones FROM ClubDeTareasAsistente WHERE Club_Tareas_ID='" + club + "' AND " +
                        "(Nombre LIKE '%" + parameter + "%' or " +
                        " Apellidos LIKE '%" + parameter + "%' or " +
                        " Nombre_Tutor LIKE '%" + parameter + "%' or " +
                        " Telefono_Tutor LIKE '%" + parameter + "%');";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(sqlString, this.conn);
                    this.conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos del club de tareas de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public List<ClubDeTareas> obtenerClubDeTareas()
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT C.ID, C.Fecha, C.Hora, C.Costo, C.Psicoterapeuta, C.Observaciones, C.Estado ,R.ID, R.Reservante, R.Fecha, R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones FROM clubDeTareas C LEFT JOIN reservaciones R ON R.ID=C.Reservacion_ID WHERE C.Estado='Activo'";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos de los clubes de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public bool registrarAsistenteClubDeTareas(ClubDeTareasAsistente asistente)
        {
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
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + inscribir
                                    + "COMMIT;";
                conn.Open();
                try
                {
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
                    throw new Exception("Error...! Error al agregar asistencia a la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...! Error al establecer conexión con el servidor");
            }
        }
        public bool borrarAsistenteClubDeTareas(string id)
        {
            try
            {
                string borrarAsistente = "DELETE FROM clubDeTareasAsistentes WHERE ID=" + id + "; ";
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + borrarAsistente
                                    + "COMMIT;";
                conn.Open();
                try
                {
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
                    throw new Exception("Error...! Error al borrar asistencia a la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...! Error al establecer conexión con el servidor");
            }
        }
        public bool registrarPagoAsistenciaClubDeTareas(Pago pago, string idAsistente)
        {
            try
            {
                string updateAsistente = "UPDATE cluDeTareasAsistentes SET Pago = Pago + " + pago.cantidad +
                                            " WHERE ID = " + idAsistente + ";";
                string agregarPago = "INSERT INTO pagos (Emisor, FechaPago, Cantidad, Concepto, Area, Observaciones, Recibio, Parent_ID) VALUES ('"
                    + pago.emisor + "', '" + formatearFecha(pago.fechaPago) + "'," + pago.cantidad + ", 'Pago de Club De Tareas', 'Psicoterapia','"
                    + pago.observaciones + "', '" + pago.recibio + "', '" + idAsistente + "');";
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + updateAsistente
                                    + agregarPago
                                    + "COMMIT;";
                conn.Open();
                try
                {
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
                    throw new Exception("Error...! Error al agregar Pago de club a la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...! Error al establecer conexión con el servidor");
            }
        }

        //-------------------------------PAGOS--------------------------------------//
        public bool agregarPago(Pago pago)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO pagos (Emisor, FechaPago, Cantidad, Concepto, Area, Observaciones, Recibio, Parent_ID) VALUES ('"
                    + pago.emisor + "', '" + formatearFecha(pago.fechaPago) + "'," + pago.cantidad + ", '" + pago.concepto + "', '" + pago.area + "', '"
                    + pago.observaciones + "', '" + pago.recibio + "', '" + pago.parent_id + "')";
                conn.Open();
                try
                {
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception E)
                {
                    throw new Exception("Error al agregar el pago a la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerPagosTable(string area)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter("SELECT ID, Emisor, FechaPago AS 'Fecha De Pago', Cantidad, Concepto, Observaciones, Recibio  FROM pagos WHERE Area='"+area+"'", conn);
                    conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de los pagos de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public MySqlDataAdapter obtenerPagosTable(string parameter, string area)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    string sqlString = "SELECT ID, Emisor, FechaPago AS 'Fecha De Pago', Cantidad, Concepto, Observaciones, Recibio FROM pagos WHERE" +
                        "(Emisor LIKE '%" + parameter + "%' or " +
                        "Cantidad LIKE '%" + parameter + "%' or " +
                        "Concepto LIKE '%" + parameter + "%' or " +
                        "FechaPago LIKE '%" + parameter + "%' or " +
                        "Recibio LIKE '%" + parameter + "%') AND Area='" + area + "'";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(sqlString, conn);
                    conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de los pagos de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public Pago consultarPago(string id)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT ID, Emisor, FechaPago, Cantidad, Concepto, Area, Parent_ID, Observaciones, Recibio, Estado FROM pagos WHERE ID='" + id + "'";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public int obtenerUltimoIDPagos()
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT AUTO_INCREMENT FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '" + this.database + "' AND TABLE_NAME = 'pagos'; ";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public bool cancelarPago(string id)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE pagos SET Estado = 'Cancelado' WHERE ID=" + id;
                conn.Open();
                try
                {
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
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }

        //-------------------------------RESERVACIONES--------------------------------------//
        public bool agregarReservacion(Reservacion reservacion)
        {
            try
            {
                string reservacionQuery = "INSERT INTO reservaciones (Reservante, Fecha, Codigo_Reservacion, Hora_Inicio, Duracion, Hora_Fin, Concepto, ID_Parent, Ubicacion, Observaciones) VALUES ('"
                    + reservacion.reservante + "','" + formatearFecha(reservacion.fecha) + "','" + reservacion.codigo_Reservacion + "','" + reservacion.hora_Inicio + "','" + reservacion.duracion + "','" + reservacion.hora_Fin + "','" + reservacion.concepto + "','" + reservacion.id_parent + "','" + reservacion.ubicacion + "','" + reservacion.observaciones + "'); ";
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + reservacionQuery
                                    + "COMMIT;";
                try
                {
                    conn.Open();
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected >= 1)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...! Error al agregar datos de reservación de la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...! Error al establecer conexión con el servidor");
            }
        }
        public bool actualizarReservacion(Reservacion reservacion)
        {
            try
            {
                string reservacionQuery = "UPDATE reservaciones SET Reservante='" + reservacion.reservante + "', Fecha='" + formatearFecha(reservacion.fecha) +
                    "Codigo_Reservacion='" + reservacion.codigo_Reservacion + "', Hora_Inicio='" + reservacion.hora_Inicio + "', Duracion='" + reservacion.duracion +
                   "', Hora_Fin='" + reservacion.hora_Fin + "', Concepto='" + reservacion.concepto + "', ID_Parent='" + reservacion.id_parent +
                   "', Ubicacion='" + reservacion.ubicacion + "', Observaciones='" + reservacion.observaciones +
                   "' WHERE ID=" + reservacion.id + "; ";
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + reservacionQuery
                                    + "COMMIT;";
                try
                {
                    conn.Open();
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected >= 1)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...! Error al actualizar datos de reservación de la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public bool cancelarReservacion(int id)
        {
            try
            {
                string reservacionQuery = "DELETE FROM reservaciones WHERE ID=" + id + "; ";
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + reservacionQuery
                                    + "COMMIT;";
                try
                {
                    conn.Open();
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected >= 1)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...! Error al actualizar datos de reservación de la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public bool cancelarReservacion(string id)
        {
            try
            {
                string reservacionQuery = "DELETE FROM reservaciones WHERE Codigo_Reservacion=" + id + "; ";
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; "
                                    + reservacionQuery
                                    + "COMMIT;";
                try
                {
                    conn.Open();
                    int rowsAfected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAfected >= 1)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error...! Error al actualizar datos de reservación de la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexión con el servidor");
            }
        }
        public Reservacion consultarReservacion(int ID)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT R.ID, R.Reservante, R.Fecha, R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones FROM reservaciones R WHERE R.ID=" + ID + ";";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos de la reservación de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public Reservacion consultarReservacion(TimeSpan hora, DateTime fecha, string psicoterapeuta)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT R.ID, R.Reservante, R.Fecha, R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones FROM reservaciones R WHERE R.Reservante='" + psicoterapeuta + "' AND R.Fecha='"+formatearFecha(fecha)+"' AND R.Hora_Inicio='"+hora+"';";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos de la reservación de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public Reservacion consultarReservacion(string ID)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT R.ID, R.Reservante, R.Fecha, R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones FROM reservaciones R WHERE R.Codigo_Reservacion=" + ID + ";";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos de la reservación de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public List<Reservacion> obtenerReservaciones(DateTime fecha)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT R.ID, R.Reservante, R.Fecha, R.Codigo_Reservacion, R.Hora_Inicio, R.Duracion, R.Hora_Fin, R.Concepto, R.ID_Parent, R.Ubicacion, R.Observaciones FROM reservaciones R WHERE R.Fecha ='"+formatearFecha(fecha).Substring(0,10)+"';";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos de las reservaciones de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public int obtenerUltimoIDReservaciones()
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT AUTO_INCREMENT FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '" + this.database + "' AND TABLE_NAME = 'reservaciones'; ";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }

        //----------------------------------USUARIO--------------------------------------------//
        public Usuario consultarUsuario(string id)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Matricula, Usuario, Contrasena, Nivel_Acceso FROM usuarios WHERE Matricula='" + id + "' or Usuario ='" + id + "'";
                conn.Open();
                try
                {

                    //int rowsAfected = cmd.ExecuteNonQuery();
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos de Usuarios de la Base de Datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }

        }
        public bool actualizarUsuario(Usuario usuario)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE usuarios SET Usuario= '" + usuario.Nombre_De_Usuario +
                "',Contrasena='" + usuario.Contrasena +
                "',Nivel_Acceso='" + usuario.Nivel_Acceso +
                "',Estado = 1 WHERE Matricula='" + usuario.Matricula + "'";
                try
                {
                    //cmd.CommandText = "SELECT * FROM Servicios";
                    conn.Open();
                    int rowsAfected = cmd.ExecuteNonQuery();
                    //MySqlDataReader reader = cmd.ExecuteReader();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error..! Error al actualizar usuario de la Base de Datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }

        }
        public bool eliminarUsuario(string matricula)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE usuarios SET Estado = 0 WHERE Matricula='" + matricula + "'";
                try
                {
                    //cmd.CommandText = "SELECT * FROM Servicios";
                    conn.Open();
                    int rowsAfected = cmd.ExecuteNonQuery();
                    //MySqlDataReader reader = cmd.ExecuteReader();
                    conn.Close();
                    if (rowsAfected > 0)
                        return true;
                    else
                        return false;
                }
                catch (Exception e)
                {
                    throw new Exception("Error..! Error al desactivar usuario de la Base de Datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }

        }

        //--------------------------------PARAMETROS GENERALES ----------------------------------//
        private ParametrosGenerales consultarParametrosGenerales()
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM parametros_Generales; SELECT Nombre FROM ubicaciones;";
                conn.Open();
                try
                {
                    ParametrosGenerales parametros = new ParametrosGenerales();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    List<String> aux = new List<String>();
                    reader.Read();
                    parametros.Costo_Credito_Especialidad_Diplomado = reader.GetDecimal(0);
                    parametros.Costo_Credito_Maestria = reader.GetDecimal(1);
                    parametros.Porcentaje_Pago_Sesion = reader.GetDecimal(2);
                    parametros.Porcentaje_Pago_Taller = reader.GetDecimal(3);
                    parametros.Porcentaje_Pago_Clase = reader.GetDecimal(4);
                    while (reader.Read())
                    {
                        aux.Add(reader.GetString(0));
                    }
                    parametros_Generales.ubicaciones = aux;
                    conn.Close();
                    return parametros;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener parametros generales de la Base de Datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }
        }
        public bool actualizarParametrosGenerales(ParametrosGenerales parametros)
        {
            try
            {
                string updateParametros = "DELETE FROM ubiciones; INSERT INTO ubicaciones VALUES ('"+
                    parametros.Costo_Credito_Especialidad_Diplomado+"','"+ parametros.Costo_Credito_Maestria + "','" +
                    parametros.Porcentaje_Pago_Sesion + "','" + parametros.Porcentaje_Pago_Taller + "','" +
                    parametros.Porcentaje_Pago_Clase + "'); ";
                string updateUbicaciones = "";
                if (parametros.ubicaciones != null)
                {
                    foreach(string aux in parametros.ubicaciones)
                    {
                        if (updateUbicaciones != "")
                            updateUbicaciones += ",";
                        updateUbicaciones += "'"+aux+"'";
                    }
                    updateUbicaciones = "DELETE FROM ubiciones; INSERT INTO ubicaciones VALUES ("+ updateUbicaciones+"); ";

                }
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "START TRANSACTION; " + 
                    updateParametros + 
                    updateUbicaciones +
                    "COMMIT; ";
                try
                {
                    //cmd.CommandText = "SELECT * FROM Servicios";
                    conn.Open();
                    int rowsAfected = cmd.ExecuteNonQuery();
                    //MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error..! Error al actualizar usuario de la Base de Datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }

        }
        public List<String> consultarUbicaciones()
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Nombre FROM ubicaciones";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos de Ubicaciones de la Base de Datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
            }

        }
        public List<String> obtenerConceptos(string tipo, string area)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Concepto FROM conceptosDePago WHERE Tipo='" + tipo + "' AND Area='" + area + "';";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
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
                    throw new Exception("Error al obtener datos de los conceptos de pago de la base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexión con el servidor");
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
