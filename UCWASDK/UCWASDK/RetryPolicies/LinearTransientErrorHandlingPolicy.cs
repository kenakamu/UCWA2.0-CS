using System;

namespace Microsoft.Skype.UCWA.RetryPolicies
{
    public class LinearTransientErrorHandlingPolicy : ITransientErrorHandlingPolicy
    {
        private readonly uint _baseBackOffIntervalInMs;
        private readonly uint _maxRetryAttempts;
        /// <summary>
        /// Defines a linear retry policy, which means each attempt is going to be tried periodicly with a fixed period
        /// </summary>
        /// <param name="baseBackOffIntervalInMs">Time to wait before next retry</param>
        /// <param name="maxRetryAttempts">Max number of attempts before failing</param>
        public LinearTransientErrorHandlingPolicy(uint baseBackOffIntervalInMs, uint maxRetryAttempts)
        {
            if (baseBackOffIntervalInMs == default(uint)) throw new ArgumentOutOfRangeException(nameof(baseBackOffIntervalInMs));
            if (maxRetryAttempts == default(uint)) throw new ArgumentOutOfRangeException(nameof(maxRetryAttempts));

            _baseBackOffIntervalInMs = baseBackOffIntervalInMs;
            _maxRetryAttempts = maxRetryAttempts;
        }
        public int GetNextErrorWaitTimeInMs(uint currentRetryCount)
        {
            return Convert.ToInt32((currentRetryCount + 1) * _baseBackOffIntervalInMs);
        }
        public bool ShouldRetry(uint currentRetryCount)
        {
            return currentRetryCount <= _maxRetryAttempts;
        }
    }
}
