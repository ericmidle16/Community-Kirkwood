/// <summary>
/// Ellie Wacker
/// Created: 2025-03-04
/// 
/// Class for the creation of Document objects 
/// </summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDomain
{
    public class Document
    {
        public int DocumentID {  get; set; }
        public string DocumentTypeID { get; set; }
        public int Uploader {  get; set; }
        public string ReferenceID { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[]? Artifact {  get; set; }
        public string Description { get; set; }
    }
}
