using Newtonsoft.Json;

namespace Microsoft.Skype.UCWA.Models
{
    /// <summary>
    /// A collection of unread voicemails and conversations. 
    /// </summary>
    public class MissedItems : UCWAModelBaseLink
    {
        [JsonProperty("conversationLogsCount")]
        public int ConversationLogsCount { get; internal set; }

        [JsonProperty("conversationLogsNotifications")]
        public string ConversationLogsNotifications { get; internal set; }

        [JsonProperty("missedConversationsCount")]
        public int MissedConversationsCount { get; internal set; }

        [JsonProperty("unreadMissedConversationsCount")]
        public int UnreadMissedConversationsCount { get; internal set; }

        [JsonProperty("unreadVoicemailsCount")]
        public int UnreadVoicemailsCount { get; internal set; }

        [JsonProperty("voiceMailsCount")]
        public int VoiceMailsCount { get; internal set; }        
    }
}
