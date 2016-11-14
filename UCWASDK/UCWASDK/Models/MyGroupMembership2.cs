using Microsoft.Skype.UCWA.Services;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the version two of MyGroupMembershipResource (a group membership of a single contact) 
    /// The version two supports deletion of single group membership instance 
    /// </summary>
    public class MyGroupMembership2 : MyGroupMembership
    {        
        public async Task Delete()
        {
            await HttpService.Delete(Self, "2");
        }
    }
}
