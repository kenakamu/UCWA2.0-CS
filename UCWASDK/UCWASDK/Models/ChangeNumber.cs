namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Changes or clears the number stored in the corresponding phone resource.
    /// The presence of this resource indicates that the user can change the number stored in this phone resource. The server will normalize the number if necessary and if no number is provided, the existing number is deleted. Please note that the user is responsible for supplying a valid number. Additionally, this resource can be used to change the visibility of the supplied number without needing a second request. 
    /// </summary>
    public class ChangeNumber : UCWAModelBaseLink
    {
    }
}
