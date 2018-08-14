using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IICAPS_v1.DataObject;


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
        string database = "logacell_iicaps";
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
                cmd.CommandText = "INSERT INTO alumnos (Nombre, Direccion, Telefono1, Telefono2, Correo, Facebook, CURP, RFC, Sexo, EstadoCivil, EscuelaProcedencia, Carrera, Programa, Nivel, Estado, Tipo) VALUES('" 
                    + alumno.nombre + "','" + alumno.direccion + "','" + alumno.telefono1 + "','" + alumno.telefono2 + "','" + alumno.correo + "','" + alumno.facebook + "','" + alumno.curp + "','"
                    + alumno.rfc + "','" + alumno.sexo + "','" + alumno.estadoCivil + "','" + alumno.escuelaProcedencia + "','" + alumno.carrera + "','" + alumno.programa + "','" + alumno.nivel + "','"
                    + "','Registrado','" + alumno.tipo + "')";
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
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter("SELECT A.RFC, A.Nombre , A.Telefono1 AS 'Telefono 1', A.Programa AS 'Programa', G.Generacion FROM alumnos A LEFT JOIN grupoAlumno GA ON A.RFC = GA.Alumno LEFT JOIN grupos G ON G.Codigo = GA.Grupo", conn);
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
                    string sqlString = "SELECT A.RFC, A.Nombre, A.Telefono1 AS 'Telefono 1', A.Programa, G.Generacion FROM alumnos A LEFT JOIN grupoAlumno GA ON A.RFC = GA.Alumno LEFT JOIN grupos G ON G.Codigo = GA.Grupo" +
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
        public bool actualizarAlumno(Alumno alumno)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE alumnos SET Nombre= '"+alumno.nombre+"', Direccion= '"+alumno.direccion+"', Telefono1= '"+alumno.telefono1+"', Telefono2= '"
                    +alumno.telefono2+"', Correo= '"+alumno.correo+"', Facebook= '"+alumno.facebook+"', Sexo= '"+alumno.sexo+"', EstadoCivil= '"+alumno.estadoCivil+
                    "', Programa= '"+alumno.programa+"', Estado= '"+alumno.estado+"', Tipo= '"+alumno.tipo+ "' WHERE RFC = '" + alumno.rfc + "'";
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
                cmd.CommandText = "INSERT INTO creditoAlumno (AlumnoID, CantidadMensualidad, CantidadMeses, Observaciones, Estado) VALUES ('"
                    + credito.alumno + "', '" + credito.cantidadMensualidad + "', '" + credito.cantidadMeses + "', '"
                    + credito.observaciones + "', '" + credito.estado + "')";
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
        public CreditoAlumno consultarCreditoAlumno (string rfc)
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
                cmd.CommandText = "UPDATE creditoAlumno SET CantidadMensualidad= '" + credito.cantidadMensualidad + "', CantidadMeses= '" + credito.cantidadMeses + "', Observaciones= '" + credito.observaciones + "', Estado= '"
                    + credito.estado + "' WHERE AlumnoID = '" + credito.alumno + "'";
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

        //-------------------------------PAGOS--------------------------------------//

        public MySqlDataAdapter obtenerPagosCreditoAlumnoTable(string rfc, string credito)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    string sqlString = "SELECT ID, FechaPago AS 'Fecha de Pago', Cantidad, Observaciones FROM pagosAlumno WHERE creditoID='" + credito + "' AND alumnoID='" + rfc + "'";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(sqlString, conn);
                    conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de los pagos del credito del alumno");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexion con el servidor");
            }
        }
        public MySqlDataAdapter obtenerPagosColegiaturaAlumnoTable(string rfc)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    string sqlString = "SELECT ID, fechaPago AS 'Fecha de Pago', cantidad AS 'Cantidad', observaciones AS 'Observaciones' FROM pagoscolegiaturaalumno WHERE alumnoID='" + rfc + "'";
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter(sqlString, conn);
                    conn.Close();
                    return mdaDatos;
                }
                catch (Exception e)
                {
                    throw new Exception("Error al obtener los datos de los pagos de la colegiatura del alumno");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error al establecer conexion con el servidor");
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
                    throw new Exception("Error...!\n Error al actualizar la materia a la Base de datos");
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


        //-------------------------------ENTREGA DOCUMENTOS-------------------------------//
        public bool agregarEntregaDocumentos(DocumentosInscripcion doc)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO documentosInscripcion (Alumno, ActaNacimientoOrg, ActaNacimientoCop, TituloCedulaOrg, TituloLicCop, "
                    + "CedProfCop, SolicitudOpcionTitulacion, CertificadoLicCop, ConstanciaLibSSOrg, Curp, Fotografias, RecibioEmpleado, TipoInscripcion  ) VALUES ('"
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
                    string sqlString = "SELECT Alumno, ActaNacimientoOrg, ActaNacimientoCop, TituloCedulaOrg, TituloLicCop, CedProfCop, SolicitudOpcionTitulacion, CertificadoLicCop, Curp, Fotografias, RecibioEmpleado FROM documentosInscripcion WHERE" +
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

    }
}
