namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Initiates an operation that groups multiple, independent HTTP operations into a single HTTP request payload. 
    /// Batch requests are submitted as a single HTTP POST request to the batch resource. The batch request must contain a Content-Type header specifying a content type of "multipart/batching" and a boundary specification. The body of a batch request is made up of an ordered series of HTTP operations. In the batch request body, each HTTP operation is represented as a distinct MIME part and is separated by the boundary marker defined in the Content-Type header of the request. Each MIME part representing an HTTP operation within the Batch includes both Content-Type and Content-Transfer-Encoding MIME headers. The batch request boundary is the name specified in the Content-Type Header for the batch. 
    /// </summary>
    public class Batch : UCWAModelBaseLink
    {
    }
}
