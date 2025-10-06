/// <summary>
/// Ellie Wacker
/// Created: 2025-02-28
/// 
/// Class for fake Document objects that are used in testing.
/// </summary>

using DataAccessInterfaces;
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Reflection.Metadata;
using Document = DataDomain.Document;
using System.Reflection;
using System.Globalization;

namespace DataAccessFakes
{
    public class DocumentAccessorFake : IDocumentAccessor
    {
        private List<Vehicle> _vehicles;
        private List<Document> _documents;

        public DocumentAccessorFake()
        {
            _documents = new List<Document>();
        }


        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/02/18
        /// 
        /// This method shows validation for adding a vehicle that can be checked in the vehicleManagerTests
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks> 
        public int AddDocument(string documentTypeID, string referenceID, string fileName, string fileType, byte[] artifact, int uploader, string description)
        {
           
            if (documentTypeID.Length < 0)
            {
                throw new ApplicationException("Invalid documentTypeID.");
            }
            if (fileName.Equals(""))
            {
                throw new ApplicationException("Filename cannot be empty.");
            }
            if (fileName.Contains(' '))
            {
                throw new ApplicationException("Filename cannot contain spaces.");
            }

            var _document = new Document()
            {
                DocumentTypeID = documentTypeID,
                Uploader = uploader,
                ReferenceID = referenceID,
                FileName = fileName,
                FileType = fileType,
                Artifact = artifact,
                Description = description
            };

            _document.DocumentID = _documents.Count + 1;

            _documents.Add(_document);
            return _document.DocumentID;
        }

        /// <summary>
        /// Ellie Wacker
        /// Created: 2025/03/01
        /// 
        /// This method gets a list of test documents with a certain uploader
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd 
        /// example: Fixed a problem when user inputs bad data
        /// </remarks>
        public List<Document> SelectDocumentsByUploader(int uploader , string fileType)
        {
            List<Document> documents = new List<Document>();
            foreach (var document in _documents)
            {
                if (document.Uploader == uploader && document.FileType == fileType)
                {
                    documents.Add(document);
                }
            }
            if (uploader == 0)
            {
                throw new ArgumentException("Document record not found");
            }
            return documents;
        }
    }
}