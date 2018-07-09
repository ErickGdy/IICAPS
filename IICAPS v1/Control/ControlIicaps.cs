﻿using MySql.Data.MySqlClient;
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
                cmd.CommandText = "INSERT INTO alumnos (nombre, direccion, telefono1, telefono2, correo, facebook, curp, rfc, sexo, estadoCivil, escuelaProcedencia, carrera, programa, nivel, fecha, estado, tipo) VALUES('" 
                    + alumno.nombre + "','" + alumno.direccion + "','" + alumno.telefono1 + "','" + alumno.telefono2 + "','" + alumno.correo + "','" + alumno.facebook + "','" + alumno.curp + "','"
                    + alumno.rfc + "','" + alumno.sexo + "','" + alumno.estadoCivil + "','" + alumno.escuelaProcedencia + "','" + alumno.carrera + "','" + alumno.programa + "','" + alumno.nivel + "','"
                    + formatearFecha(alumno.fecha) + "','Registrado','" + alumno.tipo + "')";
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
                    throw new Exception("Error...!\n Error al agregar el Alumno a la Base de datos");
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("Error...!\n Error al establecer conexion con el servidor");
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
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter("SELECT A.rfc AS 'RFC', A.nombre AS 'Nombre', A.telefono1 AS 'Telefono 1', A.programa AS 'Programa', G.nombre AS 'Grupo' FROM alumnos A LEFT JOIN grupoalumno GA ON A.rfc = GA.alumno LEFT JOIN grupos G ON G.codigo = GA.grupo", conn);
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
                    string sqlString = "SELECT A.rfc AS 'RFC', A.nombre AS 'Nombre', A.telefono1 AS 'Telefono 1', A.programa AS 'Programa', G.nombre AS 'Grupo' FROM alumnos A LEFT JOIN grupoalumno GA ON A.rfc = GA.alumno LEFT JOIN grupos G ON G.codigo = GA.grupo" +
                        "WHERE " +
                        "(A.nombre LIKE '%" + parameter + "%' or " +
                        " A.telefono1 LIKE '%" + parameter + "%' or " +
                        " A.programa LIKE '%" + parameter + "%' or " +
                        " G.nombre LIKE '%" + parameter + "%')"; 
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
                cmd.CommandText = "SELECT * FROM alumnos WHERE rfc='" + rfc + "'";
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
                cmd.CommandText = "SELECT A.rfc AS 'RFC', A.nombre AS 'Nombre', A.telefono1 AS 'Telefono 1', A.programa AS 'Programa', G.nombre AS 'Grupo' FROM alumnos A LEFT JOIN grupoalumno GA ON A.rfc = GA.alumno LEFT JOIN grupos G ON G.codigo = GA.grupo WHERE A.programa = '" + programa + "'";
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
                cmd.CommandText = "UPDATE alumnos SET estado='Baja' WHERE rfc = '" + rfc + "'";
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
                cmd.CommandText = "UPDATE alumnos SET nombre= '"+alumno.nombre+"', direccion= '"+alumno.direccion+"', telefono1= '"+alumno.telefono1+"', telefono2= '"
                    +alumno.telefono2+"', correo= '"+alumno.correo+"', facebook= '"+alumno.facebook+"', sexo= '"+alumno.sexo+"', estadoCivil= '"+alumno.estadoCivil+
                    "', programa= '"+alumno.programa+"', estado= '"+alumno.estado+"', tipo= '"+alumno.tipo+"'";
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
                cmd.CommandText = "INSERT INTO creditoalumno (alumno, cantidadMensualidad, cantidadMeses, fechaSolicitud, observaciones, estado) VALUES ('"
                    + credito.alumno + "', '" + credito.cantidadMensualidad + "', '" + credito.cantidadMeses + "', '" + formatearFecha(credito.fechaSolicitud) + "', '"
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
                    MySqlDataAdapter mdaDatos = new MySqlDataAdapter("SELECT * FROM creditoalumno", conn);
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
                    string sqlString = "SELECT * FROM creditoalumno WHERE" +
                        "(alumno LIKE '%" + parameter + "%' or " +
                        "cantidadMensualidad LIKE '%" + parameter + "%' or " +
                        "cantidadMeses LIKE '%" + parameter + "%' or " +
                        "observaciones LIKE '%" + parameter + "%')";
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
                cmd.CommandText = "SELECT * FROM creditoalumno WHERE alumno='" + rfc + "'";
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

        //-------------------------------PAGOS--------------------------------------//

        public MySqlDataAdapter obtenerPagosCreditoAlumnoTable(string rfc, string credito)
        {
            try
            {
                conn = new MySqlConnection(builder.ToString());
                conn.Open();
                try
                {
                    string sqlString = "SELECT ID, fechaPago AS 'Fecha de Pago', cantidad AS 'Cantidad', observaciones AS 'Observaciones' FROM pagoscreditoalumno WHERE creditoID='" + credito + "' AND alumnoID='" + rfc + "'";
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
                string agregar = "INSERT INTO programa (Nivel, Nombre, Codigo, Duracion, Horario, Modalidad, RequisitosEspecialidad, RequisitosTitulacion,RequisitosDiplomado, Objetivo, PerfilIngreso,PerfilEgreso,ProcesoSeleccion,CostoInscripcionSemestral,CostoMensual,CostoCursoPropedeutico) VALUES("
                    + " ' " + programa.Nivel + "','" + programa.Nombre + "','" + programa.Codigo + "','" + programa.Duracion
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
                    + " Nivel='" + programa.Nivel + "',Nombre='" + programa.Nombre +"',Duracion='" + programa.Duracion
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
                        p.Nombre = reader.GetString(1);
                        p.Nivel = reader.GetString(2);
                        p.Duracion = reader.GetString(3);
                        p.Horario = reader.GetString(4);
                        p.Modalidad = reader.GetString(5);
                        p.RequisitosEspecialidad = reader.GetString(6);
                        p.RequisitosTitulacion = reader.GetString(7);
                        p.RequisitosDiplomado = reader.GetString(8);
                        p.Objetivo = reader.GetString(9);
                        p.PerfilIngreso = reader.GetString(10);
                        p.PerfilEgreso = reader.GetString(11);
                        p.ProcesoSeleccion = reader.GetString(12);
                        p.CostoInscripcionSemestral = reader.GetDecimal(13);
                        p.CostoMensualidad = reader.GetDecimal(14);
                        p.CostoCursoPropedeutico = reader.GetDecimal(15);
                        p.Activo = reader.GetBoolean(16);
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
                        p.Nombre = reader.GetString(1);
                        p.Nivel = reader.GetString(2);
                        p.Duracion = reader.GetString(3);
                        p.Horario = reader.GetString(4);
                        p.Modalidad = reader.GetString(5);
                        p.RequisitosEspecialidad = reader.GetString(6);
                        p.RequisitosTitulacion = reader.GetString(7);
                        p.RequisitosDiplomado = reader.GetString(8);
                        p.Objetivo = reader.GetString(9);
                        p.PerfilIngreso = reader.GetString(10);
                        p.PerfilEgreso = reader.GetString(11);
                        p.ProcesoSeleccion = reader.GetString(12);
                        p.CostoInscripcionSemestral = reader.GetDecimal(13);
                        p.CostoMensualidad = reader.GetDecimal(14);
                        p.CostoCursoPropedeutico = reader.GetDecimal(15);
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
