using IICAPS_v1;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Updater_IICAPS.Control
{
    class Control_Updater
    {
        public string version_app;
        public string version_updater;
        MySqlConnection conn;
        MySqlConnectionStringBuilder builder;
        MySqlCommand cmd;

        /*-------HOSTING ALDEAHOST-----------*/
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


        public static Control_Updater instance;

        public Control_Updater()
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
                consultarActualizaciones();
            }
            catch (Exception ex)
            {
                //throw new Exception("Error al obtener parametros generales");
            }
        }

        public static Control_Updater getInstance()
        {
            if (instance == null)
            {
                instance = new Control_Updater();
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
                throw new Exception("No es posible establecer conexión con el servidor");
            }
        }

        public void consultarActualizaciones()
        {

            //INTENTANDO GENERAR Y ABRIR CONEXION CON EL SERVIDOR
            openConection();
            //CREAR COMANDO Y QUERY PARA SER EJECUTADO
            try
            {

                cmd.CommandText = "SELECT version_app, version_updater FROM configuracion";
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    version_app = reader.GetString(0);
                    version_updater = reader.GetString(1);
                }
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                throw new Exception("No ha sido posible obtener los datos. Si el error persiste consulte con soporte técnico");
            }
        }
        public bool controlDeVersion()
        {
            StreamWriter sw = null;
            try
            {
                string archivo = "version";
                // comprobar si el fichero ya existe
                if (!File.Exists(archivo))
                {
                    File.Create(archivo).Close();
                }
                //Pass the file path and file name to the StreamReader constructor
                sw = new StreamWriter(archivo);
                //Write a line of text
                sw.WriteLine(Program.version);
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
        public string leerVersion(string path)
        {
            StreamReader sr = null;
            string version = "";
            try
            {
                string archivo = path+@"\version";
                // comprobar si el fichero ya existe
                if (File.Exists(archivo))
                {
                    //Pass the file path and file name to the StreamReader constructor
                    sr = new StreamReader(archivo);
                    //read de first line
                    version = sr.ReadLine();
                    //close the file
                    sr.Close();
                    sr.Dispose();
                }
                return version;
            }
            catch (Exception e)
            {
                sr.Dispose();
                return "";
            }
        }

    }
}
