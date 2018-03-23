using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// Represents a user's persistent, personal group version two. 
    /// An application can subscribe to updates from members of this group. Updates include presence, location, or note changes for a specific contact. An application must call startOrRefreshSubscriptionToContactsAndGroups before it can receive events when a group is created, modified, or removed.
    /// </summary>
    public class Group2 : Group
    {
        public Task Update()
        {
            return Update(HttpService.GetNewCancellationToken());
        }
        public async Task Update(CancellationToken cancellationToken)
        {
            await HttpService.Put(Self, this, cancellationToken, "2");
        }

        public Task Delete()
        {
            return Delete(HttpService.GetNewCancellationToken());
        }
        public async Task Delete(CancellationToken cancellationToken)
        {
            await HttpService.Delete(Self, cancellationToken, "2");
        }
    }    
}
