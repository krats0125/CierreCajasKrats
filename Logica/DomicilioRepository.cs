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

namespace CierreDeCajas.Logica
{
    public class DomicilioRepository
    {
        //public List<Domicilio> LeerExcel(string rutaArchivo)
        //{
        //    List<Domicilio> domicilios = new List<Domicilio>();

        //    // Configuración para permitir la lectura de archivos Excel
        //    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        //    // Leer el archivo Excel
        //    using (var stream = File.Open(rutaArchivo, FileMode.Open, FileAccess.Read))
        //    {
        //        using (var reader = ExcelReaderFactory.CreateReader(stream))
        //        {
        //            var dataSet = reader.AsDataSet();
        //            var hoja = dataSet.Tables[0]; // Asumiendo que la primera hoja contiene los datos

        //            // Recorrer cada fila del archivo Excel
        //            for (int i = 0; i < hoja.Rows.Count; i++)
        //            {
        //                if (i == 5) // Fila 6 (Dirección)
        //                {
        //                    DataRow filaDirecciones = hoja.Rows[i];
        //                    DataRow filaValores = hoja.Rows[i + 1]; // Fila 7 (Valores)
        //                    DataRow filaMediosDePago = hoja.Rows[i + 2]; // Fila 8 (Medio de pago)

        //                    for (int j = 0; j < hoja.Columns.Count; j++)
        //                    {
        //                        string medioDePago = filaMediosDePago[j].ToString();

        //                        if (medioDePago == "Transferencia" || medioDePago == "Qr")
        //                        {
        //                            Domicilio domicilio = new Domicilio
        //                            {
        //                                Direccion = filaDirecciones[j].ToString(),
        //                                Valor = Convert.ToDecimal(filaValores[j]),
        //                                MedioDePago = medioDePago
        //                            };

        //                            domicilios.Add(domicilio);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return domicilios;
        //}

        public void InsertarDomiciliosEnBaseDeDatos(List<Domicilio> domicilios)
        {
            string connectionString = ""; // Reemplaza por tu cadena de conexión
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var domicilio in domicilios)
                {
                    string query = "INSERT INTO Domicilios (Direccion, Valor, MedioDePago) VALUES (@Direccion, @Valor, @MedioDePago)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Direccion", domicilio.Direccion);
                        cmd.Parameters.AddWithValue("@Valor", domicilio.Valor);
                        cmd.Parameters.AddWithValue("@MedioDePago", domicilio.MedioDePago);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }


    }
}
