using Microsoft.Skype.UCWA.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents the entry point for registering this application with the server. 
    /// </summary>
    public class Applications2 : Applications
    {
        public Task<Application> Get()
        {
            return Get(HttpService.GetNewCancellationToken());
        }
        public async Task<Application> Get(CancellationToken cancellationToken)
        {
            return await HttpService.Get<Application>(Self, cancellationToken);
        }    
    }
}
