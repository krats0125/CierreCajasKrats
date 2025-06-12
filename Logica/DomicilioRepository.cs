using CierreDeCajas.Modelo;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CierreDeCajas.Presentacion;
using System.Windows.Forms;


namespace CierreDeCajas.Logica
{
    public class DomicilioRepository
    {
        CONEXION cn = new CONEXION();
        Principal ppal = null;
        public DomicilioRepository(Principal principal)
        {
            ppal = principal;
        }

        public bool LeerExcel(List<string> rutasArchivos)
        {
            try
            {
                List<Domicilio> domicilios = new List<Domicilio>();

                foreach (string rutaArchivo in rutasArchivos)
                {
                    string nombreArchivo = Path.GetFileName(rutaArchivo).Trim();

                    using (var reader = new StreamReader(rutaArchivo))
                    {
                        string encabezados = reader.ReadLine(); // Leer la primera línea (encabezados)

                        while (!reader.EndOfStream)
                        {
                            var linea = reader.ReadLine();
                            var valores = linea.Split(',');

                            decimal dato1 = 0, dato2 = 0, dato3 = 0, valor = 0;
                            string direccion = string.Empty;
                            string medioDePago = string.Empty;
                            int idMedioPago = 0;

                            if (nombreArchivo.StartsWith("cuadreCajaMensajeros"))
                            {
                                dato1 = Convert.ToDecimal(valores[7].Replace(".", ""));
                                dato2 = Convert.ToDecimal(valores[8].Replace(",",""));
                                dato3 = Convert.ToDecimal(valores[9].Replace(".", ""));
                                direccion = valores[2];
                                medioDePago = valores[5];
                                valor = Convert.ToDecimal(valores[11].Replace(".", ""));
                            }
                            else if (nombreArchivo.StartsWith("pedidosDetallado"))
                            {
                                dato1 = Convert.ToDecimal(valores[6].Replace(".",""));
                                dato2 = Convert.ToDecimal(valores[7].Replace(".", ""));
                                dato3 = Convert.ToDecimal(valores[8].Replace(".",""));
                                direccion = valores[2];
                                medioDePago = valores[4];
                                valor = Convert.ToDecimal(valores[10].Replace(".",""));
                            }

                            idMedioPago = MapearMedioDePago(medioDePago);

                            // Lógica para agregar Domicilio según datos obtenidos
                            if (idMedioPago == 0)
                            {
                                if (dato1 != 0)
                                {
                                    domicilios.Add(new Domicilio { Direccion = direccion, Valor = dato1, MedioDePago = 11 });
                                }
                                if (dato2 != 0)
                                {
                                    domicilios.Add(new Domicilio { Direccion = direccion, Valor = dato2, MedioDePago = 3 });
                                }
                                if (dato3 != 0)
                                {
                                    domicilios.Add(new Domicilio { Direccion = direccion, Valor = dato3, MedioDePago = 5 });
                                }
                            }
                            else
                            {
                                domicilios.Add(new Domicilio { Direccion = direccion, Valor = valor, MedioDePago = idMedioPago });
                            }
                        }
                    }
                }

                // Insertar todos los domicilios en la base de datos en una sola operación
                InsertarDomiciliosEnBaseDeDatos(domicilios);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer archivos CSV: " + ex.Message);
                return false;
            }
        }

        public int MapearMedioDePago(string descripcionExcel)
        {
            switch (descripcionExcel.ToLower())
            {
                case "transferencia":
                    return 5;
                case "qr":
                    return 6;

                case "tarjeta":
                    return 3;

                case "efectivo":
                    return 11;

                default:
                    return 0;
            }
        }
        public bool InsertarDomiciliosEnBaseDeDatos(List<Domicilio> domicilios)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Se eliminaran los archivos anteriores.¿Desea continuar?",
                    "Confirmación de eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                    using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
                    {
                        conexion.Open();
                        string consulta = "Delete from MovimientoCaja Where IdCierre=@IdCierre and IdConcepto=10";
                        using (SqlCommand Eliminarcmd = new SqlCommand(consulta, conexion))
                        {
                            Eliminarcmd.Parameters.AddWithValue("@IdCierre", ppal.idCierre);
                            Eliminarcmd.ExecuteNonQuery();
                        }

                        foreach (var domicilio in domicilios)
                        {
                            string query = "INSERT INTO MovimientoCaja (IdCierre,IdCaja, IdUsuario, IdConcepto, Valor, Descripcion, IdMedioPago) " +
                                           "VALUES (@IdCierre,@IdCaja, @IdUsuario, @IdConcepto, @Valor, @Descripcion, @IdMedioPago)";

                            using (SqlCommand cmd = new SqlCommand(query, conexion))
                            {
                                cmd.Parameters.AddWithValue("@IdCierre", ppal.idCierre);
                                cmd.Parameters.AddWithValue("@IdCaja", ppal.idCaja);
                                cmd.Parameters.AddWithValue("@IdUsuario", ppal.idUsuario);
                                cmd.Parameters.AddWithValue("@IdConcepto", 10);
                                cmd.Parameters.AddWithValue("@Valor", domicilio.Valor);
                                cmd.Parameters.AddWithValue("@Descripcion", domicilio.Direccion);
                                cmd.Parameters.AddWithValue("@IdMedioPago", domicilio.MedioDePago);

                                cmd.ExecuteNonQuery();
                            }
                        }
                    }


                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar domicilios en la base de datos: " + ex.Message);
                return false;
            }

        }

        //PARA ARCHIVO NUEVO 
        //public bool LeerExcel(List<string> rutasArchivos)
        //{
        //    try
        //    {
        //        List<Domicilio> domicilios = new List<Domicilio>();

        //        foreach (string rutaArchivo in rutasArchivos)
        //        {
        //            string nombreArchivo = Path.GetFileName(rutaArchivo).Trim();

        //            using (var reader = new StreamReader(rutaArchivo))
        //            {
        //                string encabezados = reader.ReadLine(); // Leer la primera línea (encabezados)

        //                while (!reader.EndOfStream)
        //                {
        //                    var linea = reader.ReadLine();
        //                    var valores = linea.Split(',');

        //                    decimal dato1 = 0, dato2 = 0, dato3 = 0, valor = 0;
        //                    string direccion = string.Empty;
        //                    string medioDePago = string.Empty;
        //                    string estado = string.Empty;
        //                    int Idestado = 0;
        //                    int idMedioPago = 0;

        //                    if (nombreArchivo.StartsWith("pedidosFacturadosCajero"))
        //                    {

        //                        direccion = valores[4];
        //                        estado=valores[7];
        //                        medioDePago = valores[8];
        //                        valor = Convert.ToDecimal(valores[9]);
        //                    }
        //                    Idestado = MapearEstado(estado);
        //                    if(Idestado==1)
        //                    {
        //                        valor = 0;
        //                    }
        //                    else
        //                    {
        //                        idMedioPago = MapearMedioDePago(medioDePago);

        //                        // Lógica para agregar Domicilio según datos obtenidos
        //                        if (idMedioPago == 0)
        //                        {
        //                            if (dato1 != 0)
        //                            {
        //                                domicilios.Add(new Domicilio { Direccion = direccion, Valor = valor, MedioDePago = 11 });
        //                            }
        //                            if (dato2 != 0)
        //                            {
        //                                domicilios.Add(new Domicilio { Direccion = direccion, Valor = valor, MedioDePago = 3 });
        //                            }
        //                            if (dato3 != 0)
        //                            {
        //                                domicilios.Add(new Domicilio { Direccion = direccion, Valor = valor, MedioDePago = 5 });
        //                            }
        //                        }
        //                        else
        //                        {
        //                            domicilios.Add(new Domicilio { Direccion = direccion, Valor = valor, MedioDePago = idMedioPago });
        //                        }

        //                    }
        //                }
        //            }
        //        }

        //        // Insertar todos los domicilios en la base de datos en una sola operación
        //        InsertarDomiciliosEnBaseDeDatos(domicilios);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error al leer archivos CSV: " + ex.Message);
        //        return false;
        //    }
        //}




        //public int MapearMedioDePago(string descripcionExcel)
        //{
        //    switch (descripcionExcel.ToLower())
        //    {
        //        case "transferencia":
        //            return 5;
        //        case "qr":
        //            return 6;

        //        case "tarjeta":
        //            return 3;

        //        case "efectivo":
        //            return 11;

        //        default:
        //            return 0;
        //    }
        //}


        //public int MapearEstado(string descripcionExcel)
        //{
        //    switch (descripcionExcel.ToLower())
        //    {
        //        case "Eliminado":
        //            return 1;

        //        default:
        //            return 0;
        //    }
        //}
        //public bool InsertarDomiciliosEnBaseDeDatos(List<Domicilio> domicilios)
        //{
        //    try
        //    {
        //        DialogResult dialogResult = MessageBox.Show("Se eliminaran los archivos anteriores.¿Desea continuar?",
        //            "Confirmación de eliminación",
        //            MessageBoxButtons.YesNo,
        //            MessageBoxIcon.Warning);

        //        if (dialogResult == DialogResult.Yes)
        //        {
        //            using (SqlConnection conexion = new SqlConnection(cn.ConexionCierreCaja()))
        //            {
        //                conexion.Open();
        //                string consulta = "Delete from MovimientoCaja Where IdCierre=@IdCierre and IdConcepto=10";
        //                using (SqlCommand Eliminarcmd = new SqlCommand(consulta, conexion))
        //                {
        //                    Eliminarcmd.Parameters.AddWithValue("@IdCierre", ppal.idCierre);
        //                    Eliminarcmd.ExecuteNonQuery();
        //                }

        //                foreach (var domicilio in domicilios)
        //                {
        //                    string query = "INSERT INTO MovimientoCaja (IdCierre,IdCaja, IdUsuario, IdConcepto, Valor, Descripcion, IdMedioPago) " +
        //                                   "VALUES (@IdCierre,@IdCaja, @IdUsuario, @IdConcepto, @Valor, @Descripcion, @IdMedioPago)";

        //                    using (SqlCommand cmd = new SqlCommand(query, conexion))
        //                    {
        //                        cmd.Parameters.AddWithValue("@IdCierre", ppal.idCierre);
        //                        cmd.Parameters.AddWithValue("@IdCaja", ppal.idCaja);
        //                        cmd.Parameters.AddWithValue("@IdUsuario", ppal.idUsuario);
        //                        cmd.Parameters.AddWithValue("@IdConcepto", 10);
        //                        cmd.Parameters.AddWithValue("@Valor", domicilio.Valor);
        //                        cmd.Parameters.AddWithValue("@Descripcion", domicilio.Direccion);
        //                        cmd.Parameters.AddWithValue("@IdMedioPago", domicilio.MedioDePago);

        //                        cmd.ExecuteNonQuery();
        //                    }
        //                }
        //            }


        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error al insertar domicilios en la base de datos: " + ex.Message);
        //        return false;
        //    }

        //}
    }
}
