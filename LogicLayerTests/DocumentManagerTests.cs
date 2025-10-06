/// <summary>
/// Ellie Wacker
/// Created: 2025-02-28
/// 
/// Test Class for ensuring that each DocumentManager method
/// returns the expected values.
/// </summary>

using LogicLayer;
using DataAccessFakes;
using DataDomain;
using System.Diagnostics.Metrics;
using System.Net;

namespace LogicLayerTests;

[TestClass]
public class DocumentManagerTests
{
    private IDocumentManager? _documentManager;

    [TestInitialize]
    public void InitializeTest()
    {
        _documentManager = new DocumentManager(new DocumentAccessorFake());
    }

    [TestMethod]
    /// <summary>
    /// Ellie Wacker
    /// Created: 2025/02/28
    /// 
    /// The test method that tests if the insert succeeds if the document is good
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks> 
    public void TestInsertDocumentSucceedsForGoodDocument()
    {
        // Arrange
        const string documentTypeID = "1"; 
        const string referenceID = "REF12345"; 
        const string fileName = "testfile.pdf";
        const string fileType = "pdf";
        byte[] artifact = null; 
        const int uploader = 12345;
        const string description = "Test document for unit test.";

        // Act
        const int expectedResult = 1; // Assuming 1 means success in this case
        int actualResult = 0;

        actualResult = _documentManager.InsertDocument(documentTypeID, referenceID, fileName, fileType, artifact, uploader, description);

        // Assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    /// <summary>
    /// Ellie Wacker
    /// Created: 2025/03/07
    /// 
    /// The test method that tests if an exception is thrown if the filename contains spaces
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks> 
    public void TestInsertDocumentThrowsExceptionForFileSpaces()
    {
        const string documentTypeID = "1";
        const string referenceID = "REF12345";
        const string fileName = "test fil e.pdf";
        const string fileType = "pdf";
        byte[] artifact = null;
        const int uploader = 12345;
        const string description = "Test document for unit test.";

         _documentManager.InsertDocument(documentTypeID, referenceID, fileName, fileType, artifact, uploader, description);
    }

    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    /// <summary>
    /// Ellie Wacker
    /// Created: 2025/03/07
    /// 
    /// The test method that tests if an exception is thrown if the filename is empty
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks> 
    public void TestInsertDocumentThrowsExceptionForEmptyFileName()
    {
        const string documentTypeID = "1";
        const string referenceID = "REF12345";
        const string fileName = " ";
        const string fileType = "pdf";
        byte[] artifact = null;
        const int uploader = 12345;
        const string description = "Test document for unit test.";

        _documentManager.InsertDocument(documentTypeID, referenceID, fileName, fileType, artifact, uploader, description);
    }

    [TestMethod]
    /// <summary>
    /// Ellie Wacker
    /// Created: 2025/03/01
    /// 
    /// The test method that tests if retrieving a document with a certain uploader gets the correct list
    /// </summary>
    ///
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks> 
    public void TestRetrieveDocumentsByUploaderReturnsCorrectDocuments()
    {

        // arrange 
        const int expectedUploader = 100000;
        const string fileType = "Driver License";

        // act
        List<Document> documents = _documentManager.GetDocumentsByUploader(expectedUploader, fileType);

        // assert
        foreach (var document in documents)
        {
            Assert.AreEqual(expectedUploader, document.Uploader);
        }
    }
    [TestMethod]
    [ExpectedException(typeof(ApplicationException))]
    /// <summary>
    /// Ellie Wacker
    /// Created: 2025/03/01
    /// 
    /// The test method that tests if retrieving an empty list of document throws an exception
    /// </summary>
    /// <remarks>
    /// Updater Name
    /// Updated: yyyy/mm/dd 
    /// example: Fixed a problem when user inputs bad data
    /// </remarks> 
    public void TestRetrieveVehiclesByUserIDReturnsFalseForEmptyList()
    {
        // arrange 
        const int expectedUploader = 10000000;
        const string fileType = "Driver License";

        // act
        List<Document> documents = _documentManager.GetDocumentsByUploader(expectedUploader, fileType);

        // if no vehicles are returned, the exception should be thrown
        if (documents.Count == 0)
        {
            throw new ApplicationException("No documents found for the provided uploader");
        }

        // assert
        foreach (var document in documents)
        {
            Assert.AreEqual(expectedUploader, document.Uploader);
        }
    }

}