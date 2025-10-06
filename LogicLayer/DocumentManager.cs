/// <summary>
/// Ellie Wacker
/// Created: 2025-02-28
/// 
/// Class that implements the IDocumentManager Interface - used for
/// managing Document data from Document data fake objects &/or the DB.
/// </summary>
using DataAccessInterfaces;
using DataAccessLayer;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class DocumentManager : IDocumentManager
    {
        private IDocumentAccessor _documentAccessor;

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/28
        /// 
        /// Default constructor for DocumentManager
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public DocumentManager()
        {
            _documentAccessor = new DocumentAccessor();

        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/28
        /// 
        /// Parameterized constructor for DocumentManager
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public DocumentManager(IDocumentAccessor documentAccessor)
        {
            _documentAccessor = documentAccessor;

        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/28
        /// 
        /// Method for InsertDocument
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public int InsertDocument(string documentTypeID, string referenceID, string fileName, string fileType, byte[] artifact, int uploader, string description)
        {
            int result = 0;

            try
            {
                result = _documentAccessor.AddDocument(documentTypeID, referenceID, fileName, fileType, artifact, uploader, description);
                if (result == 0)
                {
                    throw new ApplicationException("Insert Document Failed");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Insert Document Failed", ex);
            }
            return result;
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/03/01
        /// 
        /// Method for SelectDocumentsByUploader
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public List<Document> GetDocumentsByUploader(int uploader, string fileType)
        {
            List<Document> documents = new List<Document>(); 
            try
            {
                documents = _documentAccessor.SelectDocumentsByUploader(uploader, fileType);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Document list not found", ex);
            }

            return documents;
        }


    }
}
