namespace Microsoft.Skype.UCWA.RetryPolicies
{
    /// <summary>
    /// Defines the base structure for retry policies for transient error handling
    /// </summary>
    public interface ITransientErrorHandlingPolicy
    {
        /// <summary>
        /// Gets the next wait delay before retrying the action
        /// </summary>
        /// <param name="currentRetryCount">Number of retries already attempted</param>
        /// <returns>Delay in ms before retrying the action</returns>
        int GetNextErrorWaitTimeInMs(uint currentRetryCount);
        /// <summary>
        /// Gets whether the application should retry or not the action
        /// </summary>
        /// <param name="currentRetryCount">Number of retries already attempted</param>
        /// <returns>Should or not retry the service call</returns>
        bool ShouldRetry(uint currentRetryCount);
    }
}
