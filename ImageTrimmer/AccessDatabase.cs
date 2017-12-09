using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.IO;

namespace ImageTrimmer
{
    class AccessDatabase
    {
        /// <value>String to connect to the Access database.</value>
        private string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0}; Persist Security Info=True";
        
        /// <value>Name of the Access database file.</value>
        public string FileName { get; set; }

        /// <summary>
        /// Fetches ids and bitmap images by the query from database.
        /// </summary>
        /// <param name="query">Query for fetch data.</param>
        /// <returns>
        /// A dictionary of image ids with bitmap images converted from OLE object.
        /// </returns>
        public Dictionary<int, Bitmap> GetOleImages(string query)
        {
            Dictionary<int, Bitmap> oleImages = new Dictionary<int, Bitmap>();
            using (OleDbConnection con = new OleDbConnection(String.Format(connectionString, FileName)))
            {
                OleDbCommand cmd = new OleDbCommand(query, con);

                con.Open();
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                    oleImages.Add((int)reader["idOle"], ConvertImageBytesToBitmap((byte[])reader["object"]));

                reader.Close();
                con.Close();
            }

            return oleImages;
        }

        /// <summary>
        /// Converts an array of bytes of OLE object into bitmap image.
        /// </summary>
        /// <param name="bytes">An array of bytes of OLE object.</param>
        /// <returns></returns>
        private Bitmap ConvertImageBytesToBitmap(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(OleImageUnwrap.GetImageBytesFromOLEField(bytes)))
            {
                return new Bitmap(stream);
            }
        }
    }
}
