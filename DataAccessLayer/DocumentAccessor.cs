/// <summary>
/// Ellie Wacker
/// Created: 2025/02/28
/// 
/// The Data Accessor for document
/// </summary>
///
/// <remarks>
/// Updater Name
/// Updated: yyyy/mm/dd 
/// example: Fixed a problem when user inputs bad data
/// </remarks> 

using DataAccessInterfaces;
using DataDomain;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DocumentAccessor : IDocumentAccessor
    {
        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/28
        /// 
        /// The Data Access Method for adding a document into the system
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public int AddDocument(string documentTypeID, string referenceID, string fileName, string fileType, byte[] artifact, int uploader, string description)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand("sp_insert_document", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            // Adding all the parameters
            cmd.Parameters.Add("@DocumentTypeID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@DocumentTypeID"].Value = documentTypeID;

            cmd.Parameters.Add("@ReferenceID", SqlDbType.NVarChar, 50);
            cmd.Parameters["@ReferenceID"].Value = referenceID;

            cmd.Parameters.Add("@FileName", SqlDbType.NVarChar, 200);
            cmd.Parameters["@FileName"].Value = fileName;

            cmd.Parameters.Add("@FileType", SqlDbType.NVarChar, 50); // Adjust length as needed
            cmd.Parameters["@FileType"].Value = fileType;

            cmd.Parameters.Add("@Artifact", SqlDbType.VarBinary, artifact.Length);
            cmd.Parameters["@Artifact"].Value = artifact;

            cmd.Parameters.Add("@Uploader", SqlDbType.Int);
            cmd.Parameters["@Uploader"].Value = uploader;

            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 250); // Adjust length as needed
            cmd.Parameters["@Description"].Value = description;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }


        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/03/01
        /// 
        /// The Data Access Method for selecting documents with a certain uploader
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public List<Document> SelectDocumentsByUploader(int uploader, string fileType)
        {
            List<Document> documents = new List<Document>();

            // connection
            var conn = DBConnection.GetConnection();

            //command
            var cmd = new SqlCommand("sp_select_documents_by_uploader_and_file_type", conn);

            //command type
            cmd.CommandType = CommandType.StoredProcedure;

            //parameters
            cmd.Parameters.Add("@Uploader", SqlDbType.Int).Value = uploader;
            cmd.Parameters.Add("@FileType", SqlDbType.NVarChar, 50).Value = fileType;

            try
            {
                conn.Open();

                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var document = new Document()
                        {
                            DocumentID = reader.GetInt32(0),
                            DocumentTypeID = reader.GetString(1),
                            ReferenceID = reader.GetString(2),
                            FileName = reader.GetString(3),
                            FileType = reader.GetString(4),
                            Artifact = reader.GetSqlBinary(5).Value,
                            Uploader = reader.GetInt32(6),
                            Description = reader.GetString(7)
                        };
                        documents.Add(document);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return documents;
        }
    }

}
