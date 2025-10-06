/// <summary>
/// Ellie Wacker
/// Created: 2025-02-28
/// 
/// Interface that holds method declarations for managing Document data.
/// </summary>
using DataDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface IDocumentManager
    {
        int InsertDocument(string documentTypeID, string referenceID, string fileName, string fileType, byte[] artifact, int uploader, string description);
        List<Document> GetDocumentsByUploader(int uploader, string fileType);
    }
}
