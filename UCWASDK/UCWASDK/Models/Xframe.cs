namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the cross-domain frame used for web-based applications. 
    /// To prevent against cross-domain attacks, AJAX requests are not allowed between two different domains. Instead, the browser-based postMessage feature is used between the application's domain and the server's domain using this special iframe. 
    /// </summary>
    public class Xframe : UCWAModelBaseLink
    {
    }
}
