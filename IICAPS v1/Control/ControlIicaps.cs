using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IICAPS_v1.DataObject;
using System.IO;

namespace IICAPS_v1.Control
{
    class ControlIicaps
    {
        MySqlConnection conn;
        MySqlConnectionStringBuilder builder;
        MySqlCommand cmd;
        string server = "logacell.com";
        string userID = "logacell_logamel";
        string password = "Logamel82";
        //string database = "logacell_iicaps";
        string database = "logacell_iicaps_copiaPruebas";
        public static ControlIicaps instance;

        public ControlIicaps()
        {
            builder = new MySqlConnectionStringBuilder();
            builder.Server = server;
            builder.UserID = userID;
            builder.Password = password;
            builder.Database = database;
            builder.AllowUserVariables = true;
            builder.SslMode = MySqlSslMode.None;
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
                throw new Exception("Error...! Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                        credito.cantidadMensualidad = reader.GetDouble(2);
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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

        //-------------------------------PAGOS--------------------------------------//

        public bool agregarPago(PagoAlumno pago)
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
                throw new Exception("Error al establecer conexion con el servidor");
            }
        }
        public MySqlDataAdapter obtenerPagosTable()
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
                throw new Exception("Error al establecer conexion con el servidor");
            }
        }
        public MySqlDataAdapter obtenerPagosTable(string parameter)
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
                throw new Exception("Error al establecer conexion con el servidor");
            }
        }
        public PagoAlumno consultarPago(string id)
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                        mdaDatos = new MySqlDataAdapter("SELECT * FROM pagosAlumno WHERE AlumnoID = '"+rfc+"' AND Concepto = 'Pago de Colegiatura' OR Concepto = 'Abono de Colegiatura'", conn);
                    else if (concepto.Contains("Credito"))
                        mdaDatos = new MySqlDataAdapter("SELECT * FROM pagosAlumno WHERE AlumnoID = '" + rfc + "' AND Concepto = 'Pago de Credito' OR Concepto = 'Abono de Credito'", conn);
                    else if (concepto.Contains("Taller"))
                        mdaDatos = new MySqlDataAdapter("SELECT * FROM pagosAlumno WHERE AlumnoID = '" + rfc + "' AND Concepto = 'Pago de Taller' OR Concepto = 'Abono de Taller'", conn);
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
                throw new Exception("Error al establecer conexion con el servidor");
            }
        }

        public List<String> obtenerConceptosDePago(string area)
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
                throw new Exception("Error al establecer conexion con el servidor");
            }
        }

        public bool cancelarPago(string id)
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
                throw new Exception("Error...!\n Error al establecer conexion con el servidor");
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
                throw new Exception("Error...!\n Error al establecer conexion con el servidor");
            }
        }
        public bool actualizarMateria(Materia materia)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE materia SET "+
                    "Nombre=" + materia.nombre + "',Duracion='" + materia.duracion + "',Semestre='" + materia.semestre + "',Costo='" + materia.costo + 
                    "WHERE ID="+materia.id+";";
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
                throw new Exception("Error...!\n Error al establecer conexion con el servidor");
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
                throw new Exception("Error...!\n Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                throw new Exception("Error...! Error al establecer conexion con el servidor");
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
                throw new Exception("Error...!\n Error al establecer conexion con el servidor");
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
                throw new Exception("Error...!\n Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                throw new Exception("Error...! Error al establecer conexion con el servidor");
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
                throw new Exception("Error...!\n Error al establecer conexion con el servidor");
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
                throw new Exception("Error...!\n Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                throw new Exception("Error...! Error al establecer conexion con el servidor");
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
                throw new Exception("Error...! Error al establecer conexion con el servidor");
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
                throw new Exception("Error...!\n Error al establecer conexion con el servidor");
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
                throw new Exception("Error...!\n Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
            }
        }
        public Taller consultarTaller(string id)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT ID, Nombre, Fecha, Costo, Capacidad, Requisitos FROM taller WHERE ID='" + id + "'";
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
                throw new Exception("Error al establecer conexion con el servidor");
            }
        }
        public TallerAsistente obtenerAsistenteTaller(string id)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT A.Nombre,A.Telefono,A.Correo,A.CURP,A.RFC,A.Costo,A.Anticipo,A.Pago, A.Taller FROM tallerAsistentes A, taller T WHERE T.ID = A.Taller AND A.ID='" + id + "';";
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
                        a.anticipo = reader.GetDecimal(6);
                        a.pago = reader.GetDecimal(7);
                        a.restante = a.costo - a.anticipo - a.pago;
                        a.taller = reader.GetInt32(8);
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
                throw new Exception("Error al establecer conexion con el servidor");
            }
        }
        public List<TallerAsistente> obtenerAsistentesTalleres(string taller)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT A.Nombre,A.Telefono,A.Correo,A.CURP,A.RFC,A.Costo,A.Anticipo,A.Pago FROM tallerAsistentes A, taller T WHERE T.ID = A.Taller AND T.ID='" + taller+"';";
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
                        a.anticipo = reader.GetDecimal(6);
                        a.pago = reader.GetDecimal(7);
                        a.restante = a.costo - a.anticipo - a.pago;
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter("SELECT A.ID, A.Nombre,A.Telefono,A.Correo,A.CURP,A.RFC,A.Costo,A.Anticipo,A.Pago, A.Costo-A.Anticipo-A.Pago AS 'Restante' FROM tallerAsistentes A, taller T WHERE T.ID = A.Taller AND T.ID='" + taller + "'", conn); conn.Close();
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                    string sqlString = "SELECT A.ID, A.Nombre,A.Telefono,A.Correo,A.CURP,A.RFC,A.Costo,A.Anticipo,A.Pago,A.Costo-A.Anticipo-A.Pago AS 'Restante' FROM tallerAsistentes A, taller T WHERE " +
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
                throw new Exception("Error al establecer conexion con el servidor");
            }
        }
        public List<Taller> obtenerTalleres()
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM taller WHERE Estado=1";
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
                throw new Exception("Error al establecer conexion con el servidor");
            }
        }
        public bool registrarAsistenteTaller(TallerAsistente asistente)
        {
            try
            {
                string inscribir = "INSERT INTO tallerAsistentes (Taller, Nombre, Telefono, Correo, CURP, RFC, Costo, Anticipo, Observaciones) VALUE ( '" 
                    + asistente.taller + "','" + asistente.nombre + "','" + asistente.telefono + "','" + asistente.correo
                    + "','" + asistente.curp + "','" + asistente.rfc + "','" + asistente.costo
                    + "','" + asistente.anticipo + "','" + asistente.observaciones + "');";
                if (asistente.ID > 0)
                {
                    inscribir = "UPDATE tallerAsistentes SET Taller='" + asistente.taller + "',Nombre='" + asistente.nombre +
                    "',Telefono='" + asistente.telefono + "',Correo='" + asistente.correo + "',CURP='" + asistente.curp +
                    "',RFC='" + asistente.rfc + "',Costo='" + asistente.costo + "',Anticipo='" + asistente.anticipo +
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
                throw new Exception("Error...! Error al establecer conexion con el servidor");
            }
        }
        public bool registrarPagoAsistenteTaller(int id, decimal pago)
        {
            try
            {
                string update = "UPDATE tallerAsistentes SET Pago='" + pago + "' WHERE ID = " + id + ";";
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
                    conn.Close();
                    throw new Exception("Error...! Error al agregar pago de asistencia a la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...! Error al establecer conexion con el servidor");
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
                throw new Exception("Error...! Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
        public bool agregarEmpleado(Empleados empleado, Usuarios usuario)
        {
            try
            {
                string empleados = "INSERT INTO empleados (Correo, Nombre, Telefono, Puesto, NivelDeAcceso) VALUES('"
                    + empleado.correo + "','" + empleado.nombre + "','" + empleado.telefono + "','" + empleado.puesto + "'," + empleado.niveldeacceso+");";
                string usuarios = "";
                if (usuario != null) {
                    usuarios = "INSERT INTO usuarios (Estado, Empleado, Usuario, Contrasena) VALUES("
                        + usuario.estado + ",'" + usuario.empleado + "','" + usuario.usuario + "','" + usuario.contrasena + "');";
                }
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = empleados + usuarios;
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
                throw new Exception("Error...!\n Error al establecer conexion con el servidor");
            }
        }
        public bool actualizarEmpleado(Empleados empleado, Usuarios usuario)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                string emp = "UPDATE empleados SET " +
                    "Nombre=" + empleado.nombre + "',Telefono='" + empleado.telefono + "Puesto='" + empleado.puesto + "',NivelDeAcceso='" + empleado.niveldeacceso +
                    "WHERE Correo=" + empleado.correo + ";";
                string user = "UPDATE usuarios SET " +
                    "Usuario=" + usuario.usuario + "',Contrasena='" + usuario.contrasena + "Estado='" + usuario.estado + 
                    "WHERE Empleado=" + usuario.empleado + ";";
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
                throw new Exception("Error...!\n Error al establecer conexion con el servidor");
            }
        }
        public bool actualizarEmpleado(Empleados empleado)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE empleados SET " +
                    "Nombre=" + empleado.nombre + "',Telefono='" + empleado.telefono + "Puesto='" + empleado.puesto + "',NivelDeAcceso='" + empleado.niveldeacceso +
                    "WHERE Correo=" + empleado.correo + ";";
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
                throw new Exception("Error...!\n Error al establecer conexion con el servidor");
            }
        }
        public bool desactivarEmpleado(string correo)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE usuarios SET Estado=0 WHERE Empleado=" + correo + ";";
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
                throw new Exception("Error...!\n Error al establecer conexion con el servidor");
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
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter("SELECT E.Correo,E.Nombre,E.Telefono,E.Puesto,E.NivelDeAcceso FROM empleados E, usuarios U WHERE E.Correo = U.Empleado AND U.Estado = 1", conn);
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                    string sqlString = "SELECT E.Correo,E.Nombre,E.Telefono,E.Puesto,E.NivelDeAcceso FROM empleados E LEFT JOIN usuarios U ON E.Correo = U.Empleado" +
                        " WHERE " +
                        "(Correo LIKE '%" + parameter + "%' or " +
                        " Nombre LIKE '%" + parameter + "%' or " +
                        " Telefono LIKE '%" + parameter + "%' or " +
                        " Puesto LIKE '%" + parameter + "%' or " +
                        " NivelDeAcceso = " + parameter + ") and " +
                        " Estado = 1 ORDER BY Nombre ASC";
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
                throw new Exception("Error al establecer conexion con el servidor");
            }
        }
        public Empleados consultarEmpleado(string correo)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT E.Correo,E.Nombre,E.Telefono,E.Puesto,E.NivelDeAcceso FROM empleados E LEFT JOIN usuarios U ON E.Correo = U.Empleado WHERE U.Estado = 1 AND E.Correo='" + correo + "'";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Empleados a = new Empleados();
                        a.correo = reader.GetString(0);
                        a.nombre = reader.GetString(1);
                        a.telefono = reader.GetString(2);
                        a.puesto = reader.GetString(3);
                        a.niveldeacceso = reader.GetInt32(4);
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
                throw new Exception("Error al establecer conexion con el servidor");
            }
        }
        public List<Empleados> obtenerEmpleados()
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT E.Correo,E.Nombre,E.Telefono,E.Puesto,E.NivelDeAcceso FROM empleados E LEFT JOIN usuarios U ON E.Correo = U.Empleado WHERE U.Estado = 1";
                conn.Open();
                try
                {
                    MySqlDataReader reader = cmd.ExecuteReader();
                    List<Empleados> aux = new List<Empleados>();
                    while (reader.Read())
                    {

                        Empleados a = new Empleados();
                        a.correo = reader.GetString(0);
                        a.nombre = reader.GetString(1);
                        a.telefono = reader.GetString(2);
                        a.puesto = reader.GetString(3);
                        a.niveldeacceso = reader.GetInt32(4);
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
                throw new Exception("Error al establecer conexion con el servidor");
            }
        }

        public string obtenerNombreEmpleado(string correo)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Nombre FROM empleados WHERE Correo = '" + correo + "'";
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
                throw new Exception("Error...! Error al establecer conexion con el servidor");
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
                throw new Exception("Error...! Error al establecer conexion con el servidor");
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
                throw new Exception("Error al establecer conexion con el servidor");
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
                    throw new Exception("Error...! Error al agregar Prorgrama a la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...! Error al establecer conexion con el servidor");
            }
        }


        //-----------------------------------USUARIO------------------------------//
        public Usuarios consultarUsuario(string id)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Empleado, Usuario, Contrasena FROM usuarios WHERE Empleado='" + id + "' or Usuario ='" + id + "'";
                conn.Open();
                try
                {

                    //int rowsAfected = cmd.ExecuteNonQuery();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Usuarios pv = new Usuarios();
                        pv.empleado = reader.GetString(0);
                        pv.usuario = reader.GetString(1);
                        pv.contrasena = reader.GetString(2);
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
        public bool actualizarUsuario(Usuarios usuario)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE usuarios SET Usuario= '" + usuario.usuario +
                "',Contrasena='" + usuario.contrasena +
                "',Estado = 1 WHERE Empleado='" + usuario.empleado + "'";
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

        //-------------------------------Configuracion-------------------------------//
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
