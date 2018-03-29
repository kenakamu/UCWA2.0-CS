using Microsoft.Skype.UCWA.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the version two of MyGroupMembershipResource (a group membership of a single contact) 
    /// The version two supports deletion of single group membership instance 
    /// </summary>
    public class MyGroupMembership2 : MyGroupMembership
    {        
        public Task Delete()
        {
            return Delete(HttpService.GetNewCancellationToken());
        }
        public Task Delete(CancellationToken cancellationToken)
        {
            return HttpService.Delete(Self, cancellationToken, "2");
        }
    }
}
