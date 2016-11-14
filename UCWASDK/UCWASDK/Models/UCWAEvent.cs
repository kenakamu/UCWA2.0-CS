using Newtonsoft.Json;

namespace Microsoft.Skype.UCWA.Models
{
    public class UCWAEvent : UCWAModelBase
    {
        [JsonProperty("_links")]
        public InternalLinks Links { get; set; }

        [JsonProperty("sender")]
        public EventSender[] Sender { get; set; }

        [JsonIgnore]
        public string Self { get { return Links.self.Href; } }

        public class InternalLinks
        {
            [JsonProperty("self")]
            internal UCWAHref self { get; set; }            

            [JsonProperty("next")]
            private UCWAHref next { get; set; }
            public string Next { get { return next?.Href; } }

            [JsonProperty("resync")]
            private UCWAHref resync { get; set; }
            public string Resync { get { return resync == null ? "" : resync?.Href; } }
        }

        public class EventSender : UCWAModelBaseLink
        {
            [JsonProperty("events")]
            public Event[] Events { get; set; }

            public class Event : UCWAModelBaseLink
            {
                [JsonProperty("type")]
                public string Type { get; set; }

                [JsonProperty("link")]
                public InternalLink Link { get; set; }


                [JsonProperty("in")]
                public EventIn In { get; set; }

                [JsonProperty("_embedded")]
                public InternalEmbedded Embedded { get; set; }
               
                public class InternalLink : UCWAModelBase
                {
                }

                public class EventIn : UCWAModelBase
                {                    
                }

                public class InternalEmbedded
                {
                    // ApplicationSharer is not returned...
                    //[JsonProperty("applicationSharer")]
                    //public ApplicationSharer ApplicationSharer { get; set; }

                    [JsonProperty("applicationSharing")]
                    public ApplicationSharing ApplicationSharing { get; set; }

                    [JsonProperty("attendees")]
                    public Attendees Attendees { get; set; }

                    [JsonProperty("audioVideo")]
                    public AudioVideo AudioVideo { get; set; }

                    [JsonProperty("communication")]
                    public Communication Communication { get; set; }
                    
                    [JsonProperty("conversation")]
                    public Conversation Conversation { get; set; }

                    [JsonProperty("dataCollaboration")]
                    public DataCollaboration DataCollaboration { get; set; }

                    [JsonProperty("distributionGroup")]
                    public DistributionGroup DistributionGroup { get; set; }

                    [JsonProperty("group")]
                    public Group Group { get; set; }

                    [JsonProperty("localParticipant")]
                    public LocalParticipant LocalParticipant { get; set; }

                    [JsonProperty("message")]
                    public Message Message { get; set; }

                    [JsonProperty("messaging")]
                    public Messaging Messaging { get; set; }

                    [JsonProperty("messagingInvitation")]
                    public MessagingInvitation MessagingInvitation { get; set; }

                    [JsonProperty("missedItems")]
                    public MissedItems MissedItems { get; set; }

                    [JsonProperty("myContactsAndGroupsSubscription")]
                    public MyContactsAndGroupsSubscription MyContactsAndGroupsSubscription { get; set; }

                    [JsonProperty("onlineMeeting")]
                    public OnlineMeeting OnlineMeeting { get; set; }

                    [JsonProperty("onlineMeetingInvitation")]
                    public OnlineMeetingInvitation OnlineMeetingInvitation { get; set; }

                    [JsonProperty("participant")]
                    public Participant Participant { get; set; }

                    [JsonProperty("participantApplicationSharing")]
                    public ParticipantApplicationSharing ParticipantApplicationSharing { get; set; }

                    //ParticipantAudio is not returned...
                    //[JsonProperty("participantAudio")]
                    //public ParticipantAudio ParticipantAudio { get; set; }

                    //ParticipantDataCollaboration is not returned...
                    //[JsonProperty("participantDataCollaboration")]
                    //public ParticipantDataCollaboration ParticipantDataCollaboration { get; set; }

                    [JsonProperty("participantInvitation")]
                    public ParticipantInvitation ParticipantInvitation { get; set; }

                    //ParticipantMessaging is not returned...
                    //[JsonProperty("participantMessaging")]
                    //public ParticipantMessaging ParticipantMessaging { get; set; }

                    //ParticipantPanoramicVideo is not returned...
                    //[JsonProperty("participantPanoramicVideo")]
                    //public ParticipantPanoramicVideo ParticipantPanoramicVideo { get; set; }

                    //ParticipantVideo is not returned...
                    //[JsonProperty("participantVideo")]
                    //public ParticipantVideo ParticipantVideo { get; set; }

                    [JsonProperty("phoneAudio")]
                    public PhoneAudio PhoneAudio { get; set; }

                    [JsonProperty("phoneAudioInvitation")]
                    public PhoneAudioInvitation PhoneAudioInvitation { get; set; }

                    [JsonProperty("presenceSubscription")]
                    public PresenceSubscription PresenceSubscription { get; set; }

                    [JsonProperty("videoLockedOnParticipant")]
                    public VideoLockedOnParticipant VideoLockedOnParticipant { get; set; }
                }
            }
        }       
    }    
}
