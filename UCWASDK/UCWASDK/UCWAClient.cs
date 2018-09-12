using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Models;
using Microsoft.Skype.UCWA.RetryPolicies;
using Microsoft.Skype.UCWA.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using static Microsoft.Skype.UCWA.Models.UCWAEvent;
using static Microsoft.Skype.UCWA.Models.UCWAEvent.EventSender;

namespace Microsoft.Skype.UCWA
{
    public class UCWAClient : IDisposable
    {
        readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        #region Events

        /// <summary>
        /// You must subscribe this event. This is called before every HTTP call and 
        /// you need to put OAuth 2.0 and set Authorization header. You can also tweak
        /// HttpClient options.
        /// </summary>
        public event SendingRequestHandler SendingRequest;

        #region Communication Events

        /// <summary>
        /// Subscribe if you want to know when conversation resource is added to Communication. This passes added conversation.
        /// </summary>
        public event ConversationAddedHandler ConversationAdded;
        /// <summary>
        /// Subscribe if you want to know when conversation resource is deleted from communication. This passes conversation uri.
        /// </summary>
        public event ConversationDeletedHandler ConversationDeleted;
        /// <summary>
        /// Subscribe if you want to know when conversation resource is updated in communication. This passes updated conversation.
        /// </summary>
        public event ConversationUpdatedHandler ConversationUpdated;
        /// <summary>
        /// Subscribe if you want to know when communication is updated such as when new modalities added. This passes updated communication.
        /// </summary>
        public event CommunicationUpdatedHandler CommunicationUpdated;
        /// <summary>
        /// Subscribe if you want to know when invitation to messaging is completed. This passes completed MessagingInvitation which you can check invitation state.
        /// </summary>
        public event MessagingInvitationCompletedHandler MessagingInvitationCompleted;
        /// <summary>
        /// Subscribe if you want to know someone invite you to messaging. This passes MessageInvitation which you can execute Accept or Decline method.
        /// </summary>
        public event MessagingInvitationReceivedHandler MessagingInvitationReceived;
        /// <summary>
        /// Subscribe if you want to know when invitation to messaging is updated. This passes updated MessagingInvitation which you can check invitation state.
        /// </summary>
        public event MessagingInvitationUpdatedHandler MessagingInvitationUpdated;
        /// <summary>
        /// Subscribe if you want to check MessagingInvitation you sent. This passes send MessagingInvitation.
        /// </summary>
        public event MessagingInvitationSentHandler MessagingInvitationSent;
        /// <summary>
        /// Subscribe if you want to know when missed items are changed. This passes updated MissedItem so that you can check which items has been missed.
        /// </summary>
        public event MissedItemsUpdatedHandler MissedItemsUpdated;
        /// <summary>
        /// Subscribe if you want to know when invitation to online meeting is completed. This passes completed OnlineMeetingInvitation which you can check invitation state.
        /// </summary>
        public event OnlineMeetingInvitationCompletedHandler OnlineMeetingInvitationCompleted;
        /// <summary>
        /// Subscribe if you want to know someone invite you to online meeting. This passes OnlineMeetingInvitation, which you can execute Accept or Decline method.
        /// </summary>
        public event OnlineMeetingInvitationReceivedHandler OnlineMeetingInvitationReceived;
        /// <summary>
        /// Subscribe if you want to check OnlineMeetingInvitation you sent. This passes send OnlineMeetingInvitation.
        /// </summary>
        public event OnlineMeetingInvitationSentHandler OnlineMeetingInvitationSent;
        /// <summary>
        /// Subscribe if you want to know when invitation to online meeting is updated. This passes updated OnlineMeetingInvitation which you can check invitation state.
        /// </summary>
        public event OnlineMeetingInvitationUpdatedHandler OnlineMeetingInvitationUpdated;
        /// <summary>
        /// Subscribe if you want to know when invitation to join existing conversation is completed. This passes completed ParticipantInvitation which you can check invitation state.
        /// </summary>
        public event ParticipantInvitationCompletedHandler ParticipantInvitationCompleted;
        /// <summary>
        /// Subscribe if you want to know someone invite you to existing conversation. This passes ParticipantInvitation, which you can execute Accept or Decline method.
        /// </summary>
        public event ParticipantInvitationReceivedHandler ParticipantInvitationReceived;
        /// <summary>
        /// Subscribe if you want to check ParticipantInvitation you sent. This passes send ParticipantInvitation.
        /// </summary>
        public event ParticipantInvitationSentHandler ParticipantInvitationSent;
        /// <summary>
        /// Subscribe if you want to know when invitation to join existing conversation is updated. This passes updated ParticipantInvitation which you can check invitation state.
        /// </summary>
        public event ParticipantInvitationUpdatedHandler ParticipantInvitationUpdated;
        /// <summary>
        /// Subscribe if you want to know when invitation to phone call is completed. This passes completed PhoneAudioInvitation which you can check invitation state.
        /// </summary>
        public event PhoneAudioInvitationCompletedHandler PhoneAudioInvitationCompleted;
        /// <summary>
        /// Subscribe if you want to know someone invite you to phone call. This passes PhoneAudioInvitation, which you can execute Accept or Decline method.
        /// </summary>
        public event PhoneAudioInvitationReceivedHandler PhoneAudioInvitationReceived;
        /// <summary>
        /// Subscribe if you want to check PhoneAudioInvitation you sent. This passes send PhoneAudioInvitation.
        /// </summary>
        public event PhoneAudioInvitationSentHandler PhoneAudioInvitationSent;
        /// <summary>
        /// Subscribe if you want to know when invitation to phone call is updated. This passes updated PhoneAudioInvitation which you can check invitation state.
        /// </summary>
        public event PhoneAudioInvitationUpdatedHandler PhoneAudioInvitationUpdated;

        #endregion

        #region Conversation Events

        /// <summary>
        /// Subscribe if you want to know which contact shared a program or screen in a conversation at first time in a conversation. This passes ApplicationSharer.
        /// </summary>
        public event ApplicationSharerAddedHandler ApplicationSharerAdded;
        /// <summary>
        /// Subscribe if you want to know when another contact shared a program or screen in conversation. This passes ApplicationSharer.
        /// </summary>
        public event ApplicationSharerDeletedHandler ApplicationSharerDeleted;
        /// <summary>
        /// Subscribe if you want to know when a program or screen sharing is stopped. This passes ApplicationSharer.
        /// </summary>
        public event ApplicationSharerUpdatedHandler ApplicationSharerUpdated;
        /// <summary>
        /// Subscribe if you want to know when application sharing modality is updated. This returns updated ApplicationSharing.
        /// </summary>
        public event ApplicationSharingUpdatedHandler ApplicationSharingUpdated;
        /// <summary>
        /// Subscribe if you want to know conversaion's audio video modality is updated. This returns AudioVideo for the conversation.
        /// </summary>
        public event AudioVideoUpdatedHandler AudioVideoUpdated;
        /// <summary>
        /// Subscribe if you want to know conversaion's DataCollaboration modality is updated. This returns DataCollaboration for the conversation.
        /// </summary>
        public event DataCollaborationUpdatedHandler DataCollaborationUpdated;
        /// <summary>
        /// Subscribe if you want to know when you joined to a conversation. This returns LocalParticipant.
        /// </summary>
        public event LocalParticipantAddedHandler LocalParticipantAdded;
        /// <summary>
        /// Subscribe if you want to know when you left from a conversation. This returns LocalParticipant.
        /// </summary>
        public event LocalParticipantDeletedHandler LocalParticipantDeleted;
        /// <summary>
        /// Subscribe if you want to know when you are updated in a conversation. This returns updated LocalParticipant.
        /// </summary>
        public event LocalParticipantUpdatedHandler LocalParticipantUpdated;
        /// <summary>
        /// Subscribe if you want to know when message is received. This returns received message.
        /// </summary>
        public event MessageReceivedHandler MessageReceived;
        /// <summary>
        /// Subscribe if you want to know when you send message. This returns sent message.
        /// </summary>
        public event MessageSentHandler MessageSent;
        /// <summary>
        /// Subscribe if you want to know when first message is send. You can also subscribe MessagingInvitation. This returns sent message.
        /// </summary>
        public event MessageStartedHandler MessageStarted;
        /// <summary>
        /// Subscribe if you want to know when messaging is updated. This returns updated Messaging.
        /// </summary>
        public event MessagingUpdatedHandler MessagingUpdated;
        /// <summary>
        /// Subscribe if you want to know when online meeting is added to a conversation. This returns added OnlineMeeting.
        /// </summary>
        public event OnlineMeetingAddedHandler OnlineMeetingAdded;
        /// <summary>
        /// Subscribe if you want to know when online meeting is update of a conversation. This returns updated OnlineMeeting.
        /// </summary>
        public event OnlineMeetingUpdatedHandler OnlineMeetingUpdated;
        /// <summary>
        /// Subscribe if you want to know when participant shared a program or screen. You can also subscribe ApplicationSharerAdded. This returns ParticipantApplicationSharing. 
        /// </summary>
        public event ParticipantApplicationSharingAddedHandler ParticipantApplicationSharingAdded;
        /// <summary>
        /// Subscribe if you want to know when participant stop sharing a program or screen. You can also subscribe ApplicationSharerDeleted. This returns ParticipantApplicationSharing. 
        /// </summary>
        public event ParticipantApplicationSharingDeletedHandler ParticipantApplicationSharingDeleted;
        /// <summary>
        /// Subscribe if you want to know when participant sharing is updated You can also subscribe ApplicationSharerUpdated. This returns ParticipantApplicationSharing. 
        /// </summary>
        public event ParticipantApplicationSharingUpdatedHandler ParticipantApplicationSharingUpdated;
        /// <summary>
        /// Subscribe if you want to know when participant's audio is added to a conversation. It returns added ParticipantAudio.
        /// </summary>
        public event ParticipantAudioAddedHandler ParticipantAudioAdded;
        /// <summary>
        /// Subscribe if you want to know when participant's audio is delete from a conversation. It returns deleted ParticipantAudio Uri.
        /// </summary>
        public event ParticipantAudioDeletedHandler ParticipantAudioDeleted;
        /// <summary>
        /// Subscribe if you want to know when participant's audio is added to a conversation. It returns updated ParticipantAudio.
        /// </summary>
        public event ParticipantAudioUpdatedHandler ParticipantAudioUpdated;
        /// <summary>
        /// Subscribe if you want to know when participant started co-authoring. It returns added ParticipantDataCollaboration.
        /// </summary>
        public event ParticipantDataCollaborationAddedHandler ParticipantDataCollaborationAdded;

        public event ParticipantDataCollaborationDeletedHandler ParticipantDataCollaborationDeleted;
        public event ParticipantDataCollaborationUpdatedHandler ParticipantDataCollaborationUpdated;

        /// <summary>
        /// Subscribe if you want to know when participant's messaging is added to a conversation. This may happen when user join the conversation with IM capability. It returns added ParticipantMessaging.
        /// </summary>
        public event ParticipantMessagingAddedHandler ParticipantMessagingAdded;
        /// <summary>
        /// Subscribe if you want to know when participant's messaging is deleted from a conversation. This may happen when user lose IM capability. It returns removed ParticipantMessaging uri.
        /// </summary>
        public event ParticipantMessagingDeletedHandler ParticipantMessagingDeleted;
        /// <summary>
        /// Subscribe if you want to know when participant's messaging is updated from a conversation. It returns updated ParticipantMessaging.
        /// </summary>
        public event ParticipantMessagingUpdatedHandler ParticipantMessagingUpdated;
        public event ParticipantPanoramicVideoAddedHandler ParticipantPanoramicVideoAdded;
        public event ParticipantPanoramicVideoDeletedHandler ParticipantPanoramicVideoDeleted;
        public event ParticipantPanoramicVideoUpdatedHandler ParticipantPanoramicVideoUpdated;
        /// <summary>
        /// Subscribe if you want to know when participant's video is added to a conversation. It returns added ParticipantVideo.
        /// </summary>
        public event ParticipantVideoAddedHandler ParticipantVideoAdded;
        /// <summary>
        /// Subscribe if you want to know when participant's video is deleted from a conversation. It returns deleted ParticipantVideo uri.
        /// </summary>
        public event ParticipantVideoDeletedHandler ParticipantVideoDeleted;
        /// <summary>
        /// Subscribe if you want to know when participant's video is updated. It returns updated ParticipantVideo.
        /// </summary>
        public event ParticipantVideoUpdatedHandler ParticipantVideoUpdated;
        /// <summary>
        /// Subscribe if you want to know when participant's PhoneAudio is updated. It returns updated PhoneAudio.
        /// </summary>
        public event PhoneAudioUpdatedHandler PhoneAudioUpdated;

        public event VideoLockedOnParticipantAddedHandler VideoLockedOnParticipantAdded;
        public event VideoLockedOnParticipantDeletedHandler VideoLockedOnParticipantDeleted;
        public event VideoLockedOnParticipantUpdatedHandler VideoLockedOnParticipantUpdated;

        #endregion

        #region Me Events

        /// <summary>
        /// Subscribe if you want to know when you start monitoring Location. This may happen when you signin by calling MakeMeAvailable.
        /// </summary>
        public event MyLocationAddedHandler MyLocationAdded;
        /// <summary>
        /// Subscribe if you want to know when you stop monitoring Location. This may happen when you sign out.
        /// </summary>
        public event MyLocationDeletedHandler MyLocationDeleted;
        /// <summary>
        /// Subscribe if you want to know when your Location is updated. This returns updated Location
        /// </summary>
        public event MyLocationUpdatedHandler MyLocationUpdated;
        /// <summary>
        /// Subscribe if you want to know when you start monitoring Note. This may happen when you signin by calling MakeMeAvailable.
        /// </summary>
        public event MyNoteAddedHandler MyNoteAdded;
        /// <summary>
        /// Subscribe if you want to know when your stop monitoring Note. This may happen when you sign out.
        /// </summary>
        public event MyNoteDeletedHandler MyNoteDeleted;
        /// <summary>
        /// Subscribe if you want to know when your Note is updated. This returns updated Note
        /// </summary>
        public event MyNoteUpdatedHandler MyNoteUpdated;
        /// <summary>
        /// Subscribe if you want to know when you start monitoring Presence. This may happen when you signin by calling MakeMeAvailable.
        /// </summary>
        public event MyPresenceAddedHandler MyPresenceAdded;
        /// <summary>
        /// Subscribe if you want to know when your stop monitoring Presence. This may happen when you sign out.
        /// </summary>
        public event MyPresenceDeletedHandler MyPresenceDeleted;
        /// <summary>
        /// Subscribe if you want to know when your Presence is updated. This returns updated Presence
        /// </summary>
        public event MyPresenceUpdatedHandler MyPresenceUpdated;
        /// <summary>
        /// Subscribe if you want to know when you signin.
        /// </summary>
        public event MeStartedHandler MeStarted;

        #endregion

        #region People Events

        /// <summary>
        /// Subscribe if you want to know when contact's Location is updated. It returns updated Location and Contact.
        /// </summary>
        public event ContactLocationUpdatedHandler ContactLocationUpdated;
        /// <summary>
        /// Subscribe if you want to know when contact's Note is updated. It returns updated Note and Contact.
        /// </summary>
        public event ContactNoteUpdatedHandler ContactNoteUpdated;
        /// <summary>
        /// Subscribe if you want to know when contact's Presence is updated. It returns updated Presence and Contact.
        /// </summary>
        public event ContactPresenceUpdatedHandler ContactPresenceUpdated;
        /// <summary>
        /// Subscribe if you want to know when contact's PrivacyRelationship is updated. It returns updated PrivacyRelationship and Contact.
        /// </summary>
        public event ContactPrivacyRelationshipUpdatedHandler ContactPrivacyRelationshipUpdated;
        /// <summary>
        /// Subscribe if you want to know when contact's SupportedModalities is updated. It returns updated SupportedModalities and Contact.
        /// </summary>
        public event ContactSupportedModalitiesUpdatedHandler ContactSupportedModalitiesUpdated;
        /// <summary>
        /// Subscribe if you want to know when a DistributionGroup is updated. It returns updated DistributionGroup.
        /// </summary>
        public event DistributionGroupUpdatedHandler DistributionGroupUpdated;
        /// <summary>
        /// Subscribe if you want to know when a group is updated. It returns updated Group.
        /// </summary>
        public event GroupUpdatedHandler GroupUpdated;
        /// <summary>
        /// Subscribe if you want to know your subscription will be expired soon. It returns MyContactsAndGroupsSubscription and you can call Refresh method to extend subscription.
        /// </summary>
        public event MyContactsAndGroupsSubscriptionExpiringHandler MyContactsAndGroupsSubscriptionExpiring;
        /// <summary>
        /// Subscribe if you want to know when new PresenceSubscription is added. It returns added PresenceSubscription.
        /// </summary>
        public event PresenceSubscriptionAddedHandler PresenceSubscriptionAdded;
        /// <summary>
        /// Subscribe if you want to know when new PresenceSubscription is deleted. It returns deleted PresenceSubscription uri.
        /// </summary>
        public event PresenceSubscriptionDeletedHandler PresenceSubscriptionDeleted;
        /// <summary>
        /// Subscribe if you want to know when new PresenceSubscription is updated. It returns updated PresenceSubscription.
        /// </summary>
        public event PresenceSubscriptionUpdatedHandler PresenceSubscriptionUpdated;

        #endregion

        #region Participant Events

        /// <summary>
        /// Subscribe if you want to know when participant added to attendees of your online meeting. This may happen when a participant added to meeting as attendee, or demote from leader. This returns current attendees.
        /// </summary>
        public event AttendeesAddedHandler AttendeesAdded;
        /// <summary>
        /// Subscribe if you want to know when participant removed from attendees of your online meeting. This may happen when participant promoted to be a leader so that it is removed from attendees. Subscribe to ParticipantDeleted event if you want to monitor when participant left the meeting. This returns current attendees.
        /// </summary>
        public event AttendeesDeletedHandler AttendeesDeleted;
        /// <summary>
        /// Subscribe if you want to know when participant added to Leaders of your online meeting. This may happen when a participant added to meeting as leader, or promote to leader. This returns current Leaders.
        /// </summary>
        public event LeadersAddedHandler LeadersAdded;
        /// <summary>
        /// Subscribe if you want to know when participant removed from Leaders of your online meeting. This may happen when participant demoted to be an attendee so that it is removed from Leaders. Subscribe to ParticipantDeleted event if you want to monitor when participant left the meeting. This returns current Leaders.
        /// </summary>
        public event LeadersDeletedHandler LeadersDeleted;
        /// <summary>
        /// Subscribe if you want to know when participant added to Lobby of your online meeting. This may happen when a participant added to meeting but waiting at lobby. This returns current Lobby.
        /// </summary>
        public event LobbyAddedHandler LobbyAdded;
        /// <summary>
        /// Subscribe if you want to know when participant removed from Lobby of your online meeting. This may happen when a participant joined to meeting. Subscribe to ParticipantDeleted event if you want to monitor when participant left the meeting. This returns current Lobby.
        /// </summary>
        public event LobbyDeletedHandler LobbyDeleted;
        /// <summary>
        /// Subscribe if you want to know when participant joined to a conversation. This returns joined Participant.
        /// </summary>
        public event ParticipantAddedHandler ParticipantAdded;
        /// <summary>
        /// Subscribe if you want to know when participant leaves a conversation. This returns left Participant uri.
        /// </summary>
        public event ParticipantDeletedHandler ParticipantDeleted;
        /// <summary>
        /// Subscribe if you want to know when participant in a conversation is updated. This returns updated Participant.
        /// </summary>
        public event ParticipantUpdatedHandler ParticipantUpdated;
        /// <summary>
        /// Subscribe if you want to know when participant added to TypingParticipants of your online meeting. This returns current TypingParticipants.
        /// </summary>
        public event TypingParticipantsAddedHandler TypingParticipantsAdded;
        /// <summary>
        /// Subscribe if you want to know when participant removed from TypingParticipants of your online meeting. This returns current TypingParticipants.
        /// </summary>
        public event TypingParticipantsDeletedHandler TypingParticipantsDeleted;

        #endregion

        #region Group Events

        /// <summary>
        /// Subscribe if you want to know when a Distribution Group is added to MyGroups. It returns added Distribution Group.
        /// </summary>
        public event DistributionGroupAddedToMyGroupsHandler DistributionGroupAddedToMyGroups;
        /// <summary>
        /// Subscribe if you want to know when a Distribution Group is deleted to MyGroups. It returns deleted Distribution Group uri.
        /// </summary>
        public event DistributionGroupDeletedFromMyGroupsHandler DistributionGroupDeletedFromMyGroups;
        /// <summary>
        /// Subscribe if you want to know when a group is added to MyGroups. It returns added Group.
        /// </summary>
        public event GroupAddedToMyGroupsHandler GroupAddedToMyGroups;
        /// <summary>
        /// Subscribe if you want to know when a group is deleted to MyGroups. It returns deleted Group uri.
        /// </summary>
        public event GroupDeletedFromMyGroupsHandler GroupDeletedFromMyGroups;

        #endregion

        #region Contact Events

        /// <summary>
        /// Subscribe if you want to know when a contact is added to MyCotacts. It returns added Contact.
        /// </summary>
        public event ContactAddedHandler ContactAdded;
        /// <summary>
        /// Subscribe if you want to know when a contact is added to a group. It returns added Contact and a Group.
        /// </summary>
        public event ContactAddedToGroupHandler ContactAddedToGroup;
        /// <summary>
        /// Subscribe if you want to know when a contact is delete from a group. It returns removed Contact and a Group.
        /// </summary>
        public event ContactDeletedFromGroupHandler ContactDeletedFromGroup;
        /// <summary>
        /// Subscribe if you want to know when a contact is deleted from MyCotacts. It returns removed Contact.
        /// </summary>
        public event ContactDeletedHandler ContactDeleted;
        /// <summary>
        /// Subscribe if you want to know when a contact is added to ContactSubscription. It returns added Contact.
        /// </summary>
        public event ContactSubscriptionAddedHandler ContactSubscriptionAdded;
        /// <summary>
        /// Subscribe if you want to know when a contact is deleted from ContactSubscription. It returns removed Contact.
        /// </summary>
        public event ContactSubscriptionDeletedHandler ContactSubscriptionDeleted;

        #endregion

        #endregion

        #region Delegates

        public delegate void SendingRequestHandler(HttpClient client, string resource);

        #region Communication Delegates

        public delegate void CommunicationUpdatedHandler(Communication communication);
        public delegate void ConversationAddedHandler(Conversation conversation);
        public delegate void ConversationDeletedHandler(string uri);
        public delegate void ConversationUpdatedHandler(Conversation conversation);
        public delegate void MessagingInvitationCompletedHandler(MessagingInvitation messagingInvitation);
        public delegate void MessagingInvitationReceivedHandler(MessagingInvitation messagingInvitation);
        public delegate void MessagingInvitationSentHandler(MessagingInvitation messagingInvitation);
        public delegate void MessagingInvitationUpdatedHandler(MessagingInvitation messagingInvitation);
        public delegate void MissedItemsUpdatedHandler(MissedItems missedItems);
        public delegate void OnlineMeetingInvitationCompletedHandler(OnlineMeetingInvitation onlineMeetingInvitation);
        public delegate void OnlineMeetingInvitationReceivedHandler(OnlineMeetingInvitation onlineMeetingInvitation);
        public delegate void OnlineMeetingInvitationSentHandler(OnlineMeetingInvitation onlineMeetingInvitation);
        public delegate void OnlineMeetingInvitationUpdatedHandler(OnlineMeetingInvitation onlineMeetingInvitation);
        public delegate void ParticipantInvitationCompletedHandler(ParticipantInvitation participantInvitation);
        public delegate void ParticipantInvitationReceivedHandler(ParticipantInvitation participantInvitation);
        public delegate void ParticipantInvitationSentHandler(ParticipantInvitation participantInvitation);
        public delegate void ParticipantInvitationUpdatedHandler(ParticipantInvitation participantInvitation);
        public delegate void PhoneAudioInvitationCompletedHandler(PhoneAudioInvitation phoneAudioInvitation);
        public delegate void PhoneAudioInvitationReceivedHandler(PhoneAudioInvitation phoneAudioInvitation);
        public delegate void PhoneAudioInvitationSentHandler(PhoneAudioInvitation phoneAudioInvitation);
        public delegate void PhoneAudioInvitationUpdatedHandler(PhoneAudioInvitation phoneAudioInvitation);

        #endregion

        #region Conversation Delegates

        public delegate void ApplicationSharerAddedHandler(ApplicationSharer applicationSharer);
        public delegate void ApplicationSharerDeletedHandler(ApplicationSharer applicationSharer);
        public delegate void ApplicationSharerUpdatedHandler(ApplicationSharer applicationSharer);
        public delegate void ApplicationSharingUpdatedHandler(ApplicationSharing applicationSharing);
        public delegate void AudioVideoUpdatedHandler(AudioVideo audioVideo);
        public delegate void DataCollaborationUpdatedHandler(DataCollaboration dataCollaboration);
        public delegate void LocalParticipantAddedHandler(LocalParticipant localParticipant);
        public delegate void LocalParticipantDeletedHandler(LocalParticipant localParticipant);
        public delegate void LocalParticipantUpdatedHandler(LocalParticipant localParticipant);
        public delegate void MessageReceivedHandler(Message message);
        public delegate void MessageSentHandler(Message message);
        public delegate void MessageStartedHandler(Message message);
        public delegate void MessagingUpdatedHandler(Messaging messaging);
        public delegate void OnlineMeetingAddedHandler(OnlineMeeting onlineMeeting);
        public delegate void OnlineMeetingUpdatedHandler(OnlineMeeting onlineMeeting);
        public delegate void ParticipantApplicationSharingAddedHandler(ParticipantApplicationSharing participantApplicationSharing);
        public delegate void ParticipantApplicationSharingDeletedHandler(ParticipantApplicationSharing participantApplicationSharing);
        public delegate void ParticipantApplicationSharingUpdatedHandler(ParticipantApplicationSharing participantApplicationSharing);
        public delegate void ParticipantAudioAddedHandler(ParticipantAudio participantAudio);
        public delegate void ParticipantAudioDeletedHandler(string uri);
        public delegate void ParticipantAudioUpdatedHandler(ParticipantAudio participantAudio);
        public delegate void ParticipantDataCollaborationAddedHandler(ParticipantDataCollaboration participantDataCollaboration);
        public delegate void ParticipantDataCollaborationDeletedHandler(string uri);
        public delegate void ParticipantDataCollaborationUpdatedHandler(ParticipantDataCollaboration participantDataCollaboration);
        public delegate void ParticipantMessagingAddedHandler(ParticipantMessaging participantMessaging);
        public delegate void ParticipantMessagingDeletedHandler(string uri);
        public delegate void ParticipantMessagingUpdatedHandler(ParticipantMessaging participantMessaging);
        public delegate void ParticipantPanoramicVideoAddedHandler(ParticipantPanoramicVideo participantPanoramicVideo);
        public delegate void ParticipantPanoramicVideoDeletedHandler(string uri);
        public delegate void ParticipantPanoramicVideoUpdatedHandler(ParticipantPanoramicVideo participantPanoramicVideo);
        public delegate void ParticipantVideoAddedHandler(ParticipantVideo participantVideo);
        public delegate void ParticipantVideoDeletedHandler(string uri);
        public delegate void ParticipantVideoUpdatedHandler(ParticipantVideo participantVideo);
        public delegate void PhoneAudioUpdatedHandler(PhoneAudio phoneAudio);
        public delegate void VideoLockedOnParticipantAddedHandler(VideoLockedOnParticipant videoLockedOnParticipant);
        public delegate void VideoLockedOnParticipantDeletedHandler(VideoLockedOnParticipant videoLockedOnParticipant);
        public delegate void VideoLockedOnParticipantUpdatedHandler(VideoLockedOnParticipant videoLockedOnParticipant);


        #endregion

        #region Me Delegates

        public delegate void MyLocationAddedHandler();
        public delegate void MyLocationDeletedHandler();
        public delegate void MyLocationUpdatedHandler(Location location);
        public delegate void MyNoteAddedHandler();
        public delegate void MyNoteDeletedHandler();
        public delegate void MyNoteUpdatedHandler(Note note);
        public delegate void MyPresenceAddedHandler();
        public delegate void MyPresenceDeletedHandler();
        public delegate void MyPresenceUpdatedHandler(Presence presence);
        public delegate void MeStartedHandler();

        #endregion

        #region People Delegates

        public delegate void ContactLocationUpdatedHandler(ContactLocation contactLocation, Contact contact);
        public delegate void ContactNoteUpdatedHandler(ContactNote contactNote, Contact contact);
        public delegate void ContactPresenceUpdatedHandler(ContactPresence contactPresence, Contact contact);
        public delegate void ContactPrivacyRelationshipUpdatedHandler(ContactPrivacyRelationship2 contactPrivacyRelationship, Contact contact);
        public delegate void ContactSupportedModalitiesUpdatedHandler(ContactSupportedModalities contactSupportedModalities, Contact contact);
        public delegate void DistributionGroupUpdatedHandler(DistributionGroup distributionGroup);
        public delegate void GroupUpdatedHandler(Group group);
        public delegate void MyContactsAndGroupsSubscriptionExpiringHandler(MyContactsAndGroupsSubscription myContactsAndGroupsSubscription);
        public delegate void PresenceSubscriptionAddedHandler(PresenceSubscription presenceSubscription);
        public delegate void PresenceSubscriptionDeletedHandler(string uri);
        public delegate void PresenceSubscriptionUpdatedHandler(PresenceSubscription presenceSubscription);

        #endregion

        #region Participant Delegates

        public delegate void AttendeesAddedHandler(Attendees attendees);
        public delegate void AttendeesDeletedHandler(Attendees attendees);
        public delegate void LeadersAddedHandler(Leaders leaders);
        public delegate void LeadersDeletedHandler(Leaders leaders);
        public delegate void LobbyAddedHandler(Lobby lobby);
        public delegate void LobbyDeletedHandler(Lobby lobby);
        public delegate void ParticipantAddedHandler(Participant participant);
        public delegate void ParticipantDeletedHandler(string uri);
        public delegate void ParticipantUpdatedHandler(Participant participant);
        public delegate void TypingParticipantsAddedHandler(TypingParticipants typingParticipants);
        public delegate void TypingParticipantsDeletedHandler(TypingParticipants typingParticipants);

        #endregion

        #region Group Delegates

        public delegate void DistributionGroupAddedToMyGroupsHandler(DistributionGroup distributionGroup);
        public delegate void DistributionGroupDeletedFromMyGroupsHandler(string uri);
        public delegate void GroupAddedToMyGroupsHandler(Group group);
        public delegate void GroupDeletedFromMyGroupsHandler(string uri);

        #endregion

        #region Contact Deletes

        public delegate void ContactAddedHandler(Contact contact);
        public delegate void ContactAddedToGroupHandler(Contact contact, Group group);
        public delegate void ContactDeletedFromGroupHandler(Contact contact, Group group);
        public delegate void ContactDeletedHandler(Contact contact);
        public delegate void ContactSubscriptionAddedHandler(Contact contact);
        public delegate void ContactSubscriptionDeletedHandler(Contact contact);

        #endregion

        #endregion

        #region Properties
        private ITransientErrorHandlingPolicy transientErrorHandlingPolicy;
        /// <summary>
        /// Policiy to leverage when http calls fail because of transient errors
        /// </summary>
        public ITransientErrorHandlingPolicy TransientErrorHandlingPolicy
        {
            get
            {
                if (transientErrorHandlingPolicy == null)
                    transientErrorHandlingPolicy = new LinearTransientErrorHandlingPolicy(1000U, 3U);
                return transientErrorHandlingPolicy;
            }
            set
            {
                transientErrorHandlingPolicy = value;
            }
        }
        /// <summary>
        /// UCWA Application object if you need to access all properties.
        /// </summary>
        private Application application;
        public Application Application
        {
            get { return application; }
            set
            {
                if (value == null)
                    eventUri = null;
                application = value;
            }
        }

        /// <summary>
        /// UCWA host address.
        /// </summary>
        public string Host { get { return Settings.Host; } }

        // Store next EventUri
        private string eventUri;

        #region Properties Shortcut

        public Me Me { get { return application?.Me; } }
        public People People { get { return application?.People; } }
        public OnlineMeetings OnlineMeetings { get { return application?.OnlineMeetings; } }
        public Communication Communication { get { return application?.Communication; } }

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Instantiate UCWAClient by specifying ErrorHandling Policy.
        /// See https://github.com/kenakamu/UCWA2.0-CS/wiki/Performance-Considerations for performance considerations.
        /// </summary>
        /// <param name="errorHandlingPolicy"></param>
        public UCWAClient(ITransientErrorHandlingPolicy errorHandlingPolicy = null)
        {
            if (errorHandlingPolicy != null)
                transientErrorHandlingPolicy = errorHandlingPolicy;
            Settings.UCWAClient = this;
        }

        /// <summary>
        /// Dispose underline HttpClient objects.
        /// </summary>
        public void Dispose()
        {
            HttpService.DisposeHttpClients();
            _cancellationTokenSource.Dispose();
        }

        /// <summary>
        /// Create UCWA proxy. Subscribe events beforehand then call Start method
        /// </summary>
        /// <param name="tenant">Office 365 tenant name</param>
        /// <returns></returns>
        public Task<bool> Initialize(string tenant, bool isOffice365PublicTenant = true)
        {
            return Initialize(tenant, _cancellationTokenSource.Token, isOffice365PublicTenant);
        }
        /// <summary>
        /// Create UCWA proxy. Subscribe events beforehand then call Start method
        /// </summary>
        /// <param name="tenant">Office 365 tenant name</param>
        /// <returns></returns>
        public Task<bool> Initialize(string tenant, CancellationToken cancellationToken, bool isOffice365PublicTenant = true)
        {
            Settings.Tenant = tenant;
            Settings.IsOffice365PublicTenant = isOffice365PublicTenant;
            return CreateApplication(cancellationToken);
        }

        /// <summary>
        /// Run the application and start monitoring events
        /// </summary>
        /// <param name="availability">Presence at login</param>
        /// <param name="supportMessage">Indicate if this application supports Message.</param>
        /// <param name="supportAudio">Indicate if this application supports Audio. UCWA 2.0 doesn't support Audio.</param>
        /// <param name="supportPlainText">Indicate if this application supports Plain text.</param>
        /// <param name="supportHtmlFormat">Indicate if this application supports HTML text.</param>
        /// <param name="phoneNumber">Specify PhoneNumber.</param>
        /// <param name="keepAlive">By specify keepAlive, it keeps sending ReportActivity every minute.</param>
        /// <returns></returns>
        public Task SignIn(Availability availability, bool supportMessage, bool supportAudio,
            bool supportPlainText, bool supportHtmlFormat, string phoneNumber, bool keepAlive)
        {
            return SignIn(availability, supportMessage, supportAudio, supportPlainText, supportHtmlFormat, phoneNumber, keepAlive, _cancellationTokenSource.Token);
        }

        /// <summary>
        /// Run the application and start monitoring events
        /// </summary>
        /// <param name="availability">Presence at login</param>
        /// <param name="supportMessage">Indicate if this application supports Message.</param>
        /// <param name="supportAudio">Indicate if this application supports Audio. UCWA 2.0 doesn't support Audio.</param>
        /// <param name="supportPlainText">Indicate if this application supports Plain text.</param>
        /// <param name="supportHtmlFormat">Indicate if this application supports HTML text.</param>
        /// <param name="phoneNumber">Specify PhoneNumber.</param>
        /// <param name="keepAlive">By specify keepAlive, it keeps sending ReportActivity every minute.</param>
        /// <returns></returns>
        public async Task SignIn(Availability availability, bool supportMessage, bool supportAudio,
            bool supportPlainText, bool supportHtmlFormat, string phoneNumber, bool keepAlive, CancellationToken cancellationToken)
        {
            if (application == null)
                throw new InvalidOperationException("You need to initialize and subscribe the event before starting.");

            await application.Me.MakeMeAvailable(availability, supportMessage, supportAudio, supportPlainText, supportHtmlFormat, phoneNumber, cancellationToken);
            application = await application.Get(cancellationToken);

            if (keepAlive)
                Parallel.Invoke(async () => await KeepAlive(cancellationToken));

            Parallel.Invoke(async () => await MonitorEvent(cancellationToken));
        }
        /// <summary>
        /// Keep the status by sending ReportActivity.
        /// </summary>
        /// <param name="durationInSeconds">Interval to send ReportActivity in seconds. Default value is 60.</param>
        private async Task KeepAlive(CancellationToken cancellationToken, int durationInSeconds = 60)
        {
            while (Me != null)
            {
                await Me.ReportMyActivity(cancellationToken);
                await Task.Delay(durationInSeconds * 1000, cancellationToken);
            }
        }

        #region People Methods
        /// <summary>
        /// Search Contact and Distribution Group
        /// </summary>
        /// <param name="query">Search Query</param>
        /// <returns>Search result as Search2 model</returns>
        public Task<Search2> Search(string query)
        {
            return Search(query, _cancellationTokenSource.Token);
        }
        /// <summary>
        /// Search Contact and Distribution Group
        /// </summary>
        /// <param name="query">Search Query</param>
        /// <returns>Search result as Search2 model</returns>
        public Task<Search2> Search(string query, CancellationToken cancellationToken)
        {
            return application.People.Search(query, cancellationToken);
        }

        /// <summary>
        /// Subscribe to Contact Change. Handle appropriate events such as ContactLocationUpdated.
        /// </summary>
        /// <param name="sips">sip names to subscribe.</param>
        /// <returns>PresenceSubscription</returns>
        public Task<PresenceSubscription> SubscribeContactsChange(params string[] sips)
        {
            return SubscribeContactsChange(_cancellationTokenSource.Token, sips);
        }
        /// <summary>
        /// Subscribe to Contact Change. Handle appropriate events such as ContactLocationUpdated.
        /// </summary>
        /// <param name="sips">sip names to subscribe.</param>
        /// <returns>PresenceSubscription</returns>
        public async Task<PresenceSubscription> SubscribeContactsChange(CancellationToken cancellationToken, params string[] sips)
        {
            PresenceSubscriptions presenceSubscriptions = await application.People.GetPresenceSubscriptions(cancellationToken);
            return await presenceSubscriptions.SubscribeToContactsPresence(cancellationToken, sips, 30);
        }
        /// <summary>
        /// Unsubscribe to Contact Change.
        /// </summary>
        /// <param name="sips">sip names to unsubscribe.</param>
        public Task UnSubscribeContactsChange(params string[] sips)
        {
            return UnSubscribeContactsChange(_cancellationTokenSource.Token, sips);
        }
        /// <summary>
        /// Unsubscribe to Contact Change.
        /// </summary>
        /// <param name="sips">sip names to unsubscribe.</param>
        public async Task UnSubscribeContactsChange(CancellationToken cancellationToken, params string[] sips)
        {
            PresenceSubscriptions presenceSubscriptions = await application.People.GetPresenceSubscriptions(cancellationToken);
            foreach (var sip in sips)
            {
                PresenceSubscription presenceSubscription = presenceSubscriptions.Subscriptions.FirstOrDefault(x => x.Id == sip);
                if (presenceSubscription != null)
                    await presenceSubscription.Delete(cancellationToken);
            }
        }

        /// <summary>
        /// Add a contact to specified group.
        /// </summary>
        /// <param name="sip">Contact's sip. Use Uri property for Contact object.</param>
        /// <param name="groupId">Id of group.</param>
        public Task AddContactToGroup(string sip, string groupId)
        {
            return AddContactToGroup(sip, groupId, _cancellationTokenSource.Token);
        }
        /// <summary>
        /// Add a contact to specified group.
        /// </summary>
        /// <param name="sip">Contact's sip. Use Uri property for Contact object.</param>
        /// <param name="groupId">Id of group.</param>
        public async Task AddContactToGroup(string sip, string groupId, CancellationToken cancellationToken)
        {
            MyGroupMemberships2 myGroupMemberships = await application.People.GetMyGroupMemberships(cancellationToken);
            await myGroupMemberships.AddContact(sip, groupId, cancellationToken);
        }

        /// <summary>
        /// Removed a contact from all groups.
        /// </summary>
        /// <param name="sip">Contact's sip. Use Uri property for Contact object.</param>
        public Task RemoveContactFromAllGroup(string sip)
        {
            return RemoveContactFromAllGroup(sip, _cancellationTokenSource.Token);
        }
        /// <summary>
        /// Removed a contact from all groups.
        /// </summary>
        /// <param name="sip">Contact's sip. Use Uri property for Contact object.</param>
        public async Task RemoveContactFromAllGroup(string sip, CancellationToken cancellationToken)
        {
            MyGroupMemberships2 myGroupMemberships = await application.People.GetMyGroupMemberships(cancellationToken);
            await myGroupMemberships.RemoveContactFromAllGroups(sip, cancellationToken);
        }
        /// <summary>
        /// Create new group.
        /// </summary>
        /// <param name="groupName">Group name</param>
        public Task CreateGroup(string groupName)
        {
            return CreateGroup(groupName, _cancellationTokenSource.Token);
        }
        /// <summary>
        /// Create new group.
        /// </summary>
        /// <param name="groupName">Group name</param>
        public async Task CreateGroup(string groupName, CancellationToken cancellationToken)
        {
            var myGroups = await application.People.GetMyGroups(cancellationToken);
            await myGroups.CreateGroup(groupName, cancellationToken);
        }

        #endregion

        #region Communication/OnlineMeetings Methods
        /// <summary>
        /// Get Conversation by using subject or title.
        /// </summary>
        /// <param name="subject">Conversation Subject.</param>
        /// <param name="title">Conversation Title.</param>
        /// <returns>Conversation</returns>
        public Task<Conversation> GetConversation(string subject = "", string title = "")
        {
            return GetConversation(_cancellationTokenSource.Token, subject, title);
        }
        /// <summary>
        /// Get Conversation by using subject or title.
        /// </summary>
        /// <param name="subject">Conversation Subject.</param>
        /// <param name="title">Conversation Title.</param>
        /// <returns>Conversation</returns>
        public async Task<Conversation> GetConversation(CancellationToken cancellationToken, string subject = "", string title = "")
        {
            if (string.IsNullOrEmpty(subject) && string.IsNullOrEmpty(title))
                return null;

            Conversations conversations = await Communication.GetConversations(cancellationToken);
            if (conversations == null)
                return null;

            List<Conversation> convs = await conversations.GetConversations(cancellationToken);
            if (convs == null)
                return null;

            Conversation conversation = string.IsNullOrEmpty(subject) ?
                convs.FirstOrDefault(x => x.Title.ToLower() == title.ToLower()) :
                convs.FirstOrDefault(x => x.Subject.ToLower() == subject.ToLower());

            return conversation;
        }

        /// <summary>
        /// Start new instant message.
        /// </summary>
        /// <param name="sip">Contact's sip. Use Uri property for Contact object.</param>
        /// <param name="subject">Instant message subject. You can omit this.</param>
        /// <param name="importance">Instant message importance. Default: Normal</param>
        /// <param name="message">Initial message. You can omit this.</param>
        public Task StartMessaging(string sip, string subject = "", Importance importance = Importance.Normal, string message = "")
        {
            return StartMessaging(sip, _cancellationTokenSource.Token, subject, importance, message);
        }

        /// <summary>
        /// Start new instant message.
        /// </summary>
        /// <param name="sip">Contact's sip. Use Uri property for Contact object.</param>
        /// <param name="subject">Instant message subject. You can omit this.</param>
        /// <param name="importance">Instant message importance. Default: Normal</param>
        /// <param name="message">Initial message. You can omit this.</param>
        public async Task StartMessaging(string sip, CancellationToken cancellationToken, string subject = "", Importance importance = Importance.Normal, string message = "")
        {
            await application.Communication.StartMessaging(sip, subject, importance, message, cancellationToken);
        }
        /// <summary>
        /// Start adhoc Online Meeting.
        /// </summary>
        /// <param name="subject">Online Meeting Subject.</param>
        /// <param name="importance">Instant message importance. Default: Normal</param>
        /// <returns></returns>
        public Task StartOnlineMeeting(string subject, Importance importance = Importance.Normal)
        {
            return StartOnlineMeeting(subject, _cancellationTokenSource.Token, importance);
        }

        /// <summary>
        /// Start adhoc Online Meeting.
        /// </summary>
        /// <param name="subject">Online Meeting Subject.</param>
        /// <param name="importance">Instant message importance. Default: Normal</param>
        /// <returns></returns>
        public async Task StartOnlineMeeting(string subject, CancellationToken cancellationToken, Importance importance = Importance.Normal)
        {
            await application.Communication.StartOnlineMeeting(subject, importance, cancellationToken);
        }
        /// <summary>
        /// Create Scheduled Online Meeting
        /// </summary>
        /// <param name="myOnlineMeeting">MyOnlineMeeting</param>
        /// <returns>Created MyOnlineMeeting</returns>
        public Task<MyOnlineMeeting> CreateScheduledOnlineMeeting(MyOnlineMeeting myOnlineMeeting)
        {
            return CreateScheduledOnlineMeeting(myOnlineMeeting, _cancellationTokenSource.Token);
        }
        /// <summary>
        /// Create Scheduled Online Meeting
        /// </summary>
        /// <param name="myOnlineMeeting">MyOnlineMeeting</param>
        /// <returns>Created MyOnlineMeeting</returns>
        public async Task<MyOnlineMeeting> CreateScheduledOnlineMeeting(MyOnlineMeeting myOnlineMeeting, CancellationToken cancellationToken)
        {
            MyOnlineMeetings myOnlineMeetings = await application.OnlineMeetings.GetMyOnlineMeetings(cancellationToken);
            return await myOnlineMeetings.Create(myOnlineMeeting, cancellationToken);
        }
        /// <summary>
        /// Add instant message capability to existing conversation.
        /// </summary>
        /// <param name="conversation">Conversation to add messaging.</param>
        /// <param name="message">Initial message.</param>
        public Task AddMessaging(Conversation conversation, string message = "")
        {
            return AddMessaging(conversation, _cancellationTokenSource.Token, message);
        }
        /// <summary>
        /// Add instant message capability to existing conversation.
        /// </summary>
        /// <param name="conversation">Conversation to add messaging.</param>
        /// <param name="message">Initial message.</param>
        public async Task AddMessaging(Conversation conversation, CancellationToken cancellationToken, string message = "")
        {
            Messaging messaging = await conversation.GetMessaging(cancellationToken);
            await messaging?.AddMessaging(MessageFormat.Plain, message, cancellationToken);
        }
        /// <summary>
        /// Add instant message capability to existing conversation by using OnlineMeetingInvitation.
        /// </summary>
        /// <param name="onlineMeetingInvitation">OnlineMeetingInvitation</param>
        /// <param name="message">Initial message.</param>
        public Task AddMessaging(OnlineMeetingInvitation onlineMeetingInvitation, string message = "")
        {
            return AddMessaging(onlineMeetingInvitation, _cancellationTokenSource.Token, message);
        }
        /// <summary>
        /// Add instant message capability to existing conversation by using OnlineMeetingInvitation.
        /// </summary>
        /// <param name="onlineMeetingInvitation">OnlineMeetingInvitation</param>
        /// <param name="message">Initial message.</param>
        public async Task AddMessaging(OnlineMeetingInvitation onlineMeetingInvitation, CancellationToken cancellationToken, string message = "")
        {
            Conversation conversation = await onlineMeetingInvitation.GetConversation(cancellationToken);
            Messaging messaging = await conversation?.GetMessaging(cancellationToken);
            await messaging?.AddMessaging(MessageFormat.Plain, message, cancellationToken);
        }
        /// <summary>
        /// Add a participant to existing conversation.
        /// </summary>
        /// <param name="sip">Contact's sip. Use Uri property for Contact object.</param>
        /// <param name="conversation">Conversation to add participant.</param>
        public Task AddParticipant(string sip, Conversation conversation)
        {
            return AddParticipant(sip, conversation, _cancellationTokenSource.Token);
        }
        /// <summary>
        /// Add a participant to existing conversation.
        /// </summary>
        /// <param name="sip">Contact's sip. Use Uri property for Contact object.</param>
        /// <param name="conversation">Conversation to add participant.</param>
        public async Task AddParticipant(string sip, Conversation conversation, CancellationToken cancellationToken)
        {
            await conversation.AddParticipant(sip, cancellationToken);
        }
        /// <summary>
        /// Add a participant to existing conversation by using message.
        /// </summary>
        /// <param name="sip">Contact's sip. Use Uri property for Contact object.</param>
        /// <param name="message">Message to add participant.</param>
        public Task AddParticipant(string sip, Message message)
        {
            return AddParticipant(sip, message, _cancellationTokenSource.Token);
        }
        /// <summary>
        /// Add a participant to existing conversation by using message.
        /// </summary>
        /// <param name="sip">Contact's sip. Use Uri property for Contact object.</param>
        /// <param name="message">Message to add participant.</param>
        public async Task AddParticipant(string sip, Message message, CancellationToken cancellationToken)
        {
            Messaging messaging = await message.GetMessaging(cancellationToken);
            Conversation conversation = await messaging?.GetConversation(cancellationToken);
            await conversation?.AddParticipant(sip, cancellationToken);
        }
        /// <summary>
        /// Add a participant to existing conversation by using onlineMeetingInvitation.
        /// </summary>
        /// <param name="sip">Contact's sip. Use Uri property for Contact object.</param>
        /// <param name="onlineMeetingInvitation">OnlineMeetingInvitation to add participant.</param>
        public Task AddParticipant(string sip, OnlineMeetingInvitation onlineMeetingInvitation)
        {
            return AddParticipant(sip, onlineMeetingInvitation, _cancellationTokenSource.Token);
        }
        /// <summary>
        /// Add a participant to existing conversation by using onlineMeetingInvitation.
        /// </summary>
        /// <param name="sip">Contact's sip. Use Uri property for Contact object.</param>
        /// <param name="onlineMeetingInvitation">OnlineMeetingInvitation to add participant.</param>
        public async Task AddParticipant(string sip, OnlineMeetingInvitation onlineMeetingInvitation, CancellationToken cancellationToken)
        {
            Conversation conversation = await onlineMeetingInvitation.GetConversation(cancellationToken);
            await conversation?.AddParticipant(sip, cancellationToken);
        }
        /// <summary>
        /// Add a participant to existing conversation by using MessagingInvitation.
        /// </summary>
        /// <param name="sip">Contact's sip. Use Uri property for Contact object.</param>
        /// <param name="messagingInvitation">MessagingInvitation to add participant.</param>
        public Task AddParticipant(string sip, MessagingInvitation messagingInvitation)
        {
            return AddParticipant(sip, messagingInvitation, _cancellationTokenSource.Token);
        }
        /// <summary>
        /// Add a participant to existing conversation by using MessagingInvitation.
        /// </summary>
        /// <param name="sip">Contact's sip. Use Uri property for Contact object.</param>
        /// <param name="messagingInvitation">MessagingInvitation to add participant.</param>
        public async Task AddParticipant(string sip, MessagingInvitation messagingInvitation, CancellationToken cancellationToken)
        {
            Conversation conversation = await messagingInvitation.GetConversation(cancellationToken);
            await conversation.AddParticipant(sip, cancellationToken);
        }
        /// <summary>
        /// Add Phone Call to existing conversation by using OnlineMeetingInvitation.
        /// </summary>
        /// <param name="sip">Contact's sip. Use Uri property for Contact object.</param>
        /// <param name="phoneNumber">Phone number.</param>
        /// <param name="onlineMeetingInvitation">OnlineMeetingInvitation</param>
        public Task AddPhoneCall(string sip, string phoneNumber, OnlineMeetingInvitation onlineMeetingInvitation)
        {
            return AddPhoneCall(sip, phoneNumber, onlineMeetingInvitation, _cancellationTokenSource.Token);
        }

        /// <summary>
        /// Add Phone Call to existing conversation by using OnlineMeetingInvitation.
        /// </summary>
        /// <param name="sip">Contact's sip. Use Uri property for Contact object.</param>
        /// <param name="phoneNumber">Phone number.</param>
        /// <param name="onlineMeetingInvitation">OnlineMeetingInvitation</param>
        public async Task AddPhoneCall(string sip, string phoneNumber, OnlineMeetingInvitation onlineMeetingInvitation, CancellationToken cancellationToken)
        {
            Conversation conversation = await onlineMeetingInvitation.GetConversation(cancellationToken);
            PhoneAudio phoneAudio = await conversation?.GetPhoneAudio(cancellationToken);
            await phoneAudio?.AddPhoneAudio(sip, phoneNumber, _cancellationTokenSource.Token);
        }
        /// <summary>
        /// Reply message to existing Messaging by using Message.
        /// </summary>
        /// <param name="text">Reply message text.</param>
        /// <param name="message">Message</param>
        public Task ReplyMessage(string text, Message message)
        {
            return ReplyMessage(text, message, _cancellationTokenSource.Token);
        }
        /// <summary>
        /// Reply message to existing Messaging by using Message.
        /// </summary>
        /// <param name="text">Reply message text.</param>
        /// <param name="message">Message</param>
        public async Task ReplyMessage(string text, Message message, CancellationToken cancellationToken)
        {
            Messaging messaging = await message.GetMessaging(cancellationToken);
            await messaging?.SendMessage(text, cancellationToken);
        }

        /// <summary>
        /// Reply message to existing Messaging by using Conversation.
        /// </summary>
        /// <param name="text">Reply message text.</param>
        /// <param name="conversation">Conversation</param>
        public Task ReplyMessage(string text, Conversation conversation)
        {
            return ReplyMessage(text, conversation, _cancellationTokenSource.Token);
        }
        /// <summary>
        /// Reply message to existing Messaging by using Conversation.
        /// </summary>
        /// <param name="text">Reply message text.</param>
        /// <param name="conversation">Conversation</param>
        public async Task ReplyMessage(string text, Conversation conversation, CancellationToken cancellationToken)
        {
            Messaging messaging = await conversation.GetMessaging(cancellationToken);
            await messaging?.SendMessage(text, cancellationToken);
        }
        /// <summary>
        /// Reply message to existing Messaging by using MessagingInvitation.
        /// </summary>
        /// <param name="text">Reply message text.</param>
        /// <param name="messagingInvitation">MessagingInvitation</param>
        public Task ReplyMessage(string text, MessagingInvitation messagingInvitation)
        {
            return ReplyMessage(text, messagingInvitation, _cancellationTokenSource.Token);
        }
        /// <summary>
        /// Reply message to existing Messaging by using MessagingInvitation.
        /// </summary>
        /// <param name="text">Reply message text.</param>
        /// <param name="messagingInvitation">MessagingInvitation</param>
        public async Task ReplyMessage(string text, MessagingInvitation messagingInvitation, CancellationToken cancellationToken)
        {
            Messaging messaging = await messagingInvitation.GetMessaging(cancellationToken);
            await messaging?.SendMessage(text, cancellationToken);
        }
        // <summary>
        /// Reply message to existing Messaging by using OnlineMeetingInvitation.
        /// </summary>
        /// <param name="text">Reply message text.</param>
        /// <param name="onlineMeetingInvitation">OnlineMeetingInvitation</param>
        public Task ReplyMessage(string text, OnlineMeetingInvitation onlineMeetingInvitation)
        {
            return ReplyMessage(text, onlineMeetingInvitation, _cancellationTokenSource.Token);
        }
        /// <summary>
        /// Reply message to existing Messaging by using OnlineMeetingInvitation.
        /// </summary>
        /// <param name="text">Reply message text.</param>
        /// <param name="onlineMeetingInvitation">OnlineMeetingInvitation</param>
        public async Task ReplyMessage(string text, OnlineMeetingInvitation onlineMeetingInvitation, CancellationToken cancellationToken)
        {
            Conversation conversation = await onlineMeetingInvitation.GetConversation(cancellationToken);
            Messaging messaging = await conversation?.GetMessaging(cancellationToken);
            await messaging?.SendMessage(text, cancellationToken);
        }

        #endregion

        #region Create App

        private async Task<bool> CreateApplication(CancellationToken cancellationToken, string agentName = "myAgent", string language = "en-US")
        {
            User user = await GetUserDiscoverUri();
            if (user == null)
                return false;

            application = await user.CreateApplication(agentName, Guid.NewGuid().ToString(), language, cancellationToken);

            // Get host address
            Settings.Host = new Uri(user.Self).Scheme + "://" + new Uri(user.Self).Host;

            eventUri = application.Links.Events;
            return true;
        }

        private async Task<User> GetUserDiscoverUri()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
                HttpResponseMessage response = null;
                try
                {
                    try
                    {
                        response = await client.GetAsync(Settings.IsOffice365PublicTenant ? $"https://webdir.online.lync.com/Autodiscover/AutodiscoverService.svc/root?originalDomain={Settings.Tenant}" : $"https://lyncdiscoverinternal.{Settings.Tenant}");
                    }
                    catch
                    {
                        // request externally if internal discovery fails
                        response = await client.GetAsync($"https://lyncdiscover.{Settings.Tenant}");
                    }
                }
                catch (HttpRequestException hex)
                {
                    Exception ex = hex;
                    while (ex != null)
                    {
                        if (ex.GetType().Name == "AuthenticationException")
                        {
                            response = await client.GetAsync($"http://lyncdiscover.{Settings.Tenant}");
                            break;
                        }
                        ex = ex.InnerException;
                    }
                    if (response == null)
                    {
                        throw;
                    }
                }
                if (response.IsSuccessStatusCode)
                {
                    var root = JsonConvert.DeserializeObject<Root>(await response.Content.ReadAsStringAsync(), new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Include });
                    var redirect = await root.GetRedirect(_cancellationTokenSource.Token);
                    return await (redirect?.GetUser(_cancellationTokenSource.Token) ?? root.GetUser(_cancellationTokenSource.Token));
                }
                else
                    return null;
            }
        }

        /// <summary>
        /// Refresh Application data.
        /// </summary>
        public Task Refresh()
        {
            return Refresh(_cancellationTokenSource.Token);
        }
        /// <summary>
        /// Refresh Application data.
        /// </summary>
        public async Task Refresh(CancellationToken cancellationToken)
        {
            application = await application.Get(cancellationToken);
        }

        #endregion
        private async Task<UCWAEvent> GetEvent(CancellationToken cancellationToken)
        {
            while (!string.IsNullOrEmpty(eventUri))
                using (var client = new HttpClient())
                    try
                    {
                        var ucwaEvent = await HttpService.Get<UCWAEvent>(eventUri, cancellationToken);

                        if (ucwaEvent == null)
                            await Task.Delay(1000);

                        if (!string.IsNullOrEmpty(ucwaEvent?.Links?.Resync))
                        {
                            eventUri = ucwaEvent.Links.Resync;
                            ucwaEvent = await GetEvent(cancellationToken);
                        }

                        eventUri = ucwaEvent?.Links?.Next;
                        return ucwaEvent;
                    }
                    catch (TaskCanceledException)
                    {
                        await Task.Delay(1000);
                    }
            return null;
        }
        private async Task MonitorEvent(CancellationToken cancellationToken)
        {
            while (true)
            {
                UCWAEvent eventdata = await GetEvent(cancellationToken);

                if (eventdata == null)
                    return;

                foreach (var sender in eventdata.Sender)
                {
                    switch (sender.Rel)
                    {
                        case "communication":
                            HandleCommunicationEvent(sender);
                            break;
                        case "conversation":
                            await HandleConversationEvent(sender, cancellationToken);
                            break;
                        case "me":
                            await HandleMeEvent(sender, cancellationToken);
                            break;
                        case "people":
                            await HandlePeopleEvent(sender, cancellationToken);
                            break;
                    }
                }
            }
        }

        internal async Task GetToken(HttpClient client, string uri, CancellationToken cancellationToken)
        {
            SendingRequest(client, uri);
            while (client.DefaultRequestHeaders.Authorization == null)
            {
                await Task.Delay(100, cancellationToken);
            }
        }

        #endregion

        #region Event Handlers

        #region Sender Handlers

        private void HandleCommunicationEvent(EventSender sender)
        {
            if (sender.Events == null)
                return;

            foreach (var ucwaEvent in sender.Events)
            {
                switch (ucwaEvent.Type)
                {
                    case "added":
                        HandleCommunicationAddedEvent(ucwaEvent);
                        break;
                    case "completed":
                        HandleCommunicationCompletedEvent(ucwaEvent);
                        break;
                    case "deleted":
                        HandleCommunicationDeletedEvent(ucwaEvent);
                        break;
                    case "started":
                        HandleCommunicationStartedEvent(ucwaEvent);
                        break;
                    case "updated":
                        HandleCommunicationUpdatedEvent(ucwaEvent);
                        break;
                }
            }
        }

        private async Task HandleConversationEvent(EventSender sender, CancellationToken cancellationToken)
        {
            if (sender.Events == null)
                return;

            foreach (var ucwaEvent in sender.Events)
            {
                switch (ucwaEvent.Type)
                {
                    case "added":
                        await HandleConversationAddedEvent(ucwaEvent, cancellationToken);
                        break;
                    case "completed":
                        HandleConversationCompletedEvent(ucwaEvent);
                        break;
                    case "deleted":
                        await HandleConversationDeletedEvent(ucwaEvent, cancellationToken);
                        break;
                    case "started":
                        HandleConversationStartedEvent(ucwaEvent);
                        break;
                    case "updated":
                        await HandleConversationUpdatedEvent(ucwaEvent, cancellationToken);
                        break;
                }
            }
        }

        private async Task HandleMeEvent(EventSender sender, CancellationToken cancellationToken)
        {
            if (sender.Events == null)
                return;

            foreach (var ucwaEvent in sender.Events)
            {
                switch (ucwaEvent.Type)
                {
                    case "added":
                        HandleMeAddedEvent(ucwaEvent);
                        break;
                    case "deleted":
                        HandleMeDeletedEvent(ucwaEvent);
                        break;
                    case "updated":
                        await HandleMeUpdatedEvent(ucwaEvent, cancellationToken);
                        break;
                }
            }
        }

        private async Task HandlePeopleEvent(EventSender sender, CancellationToken cancellationToken)
        {
            if (sender.Events == null)
                return;

            foreach (var ucwaEvent in sender.Events)
            {
                switch (ucwaEvent.Type)
                {
                    case "added":
                        await HandlePeopleAddedEvent(ucwaEvent, cancellationToken);
                        break;
                    case "deleted":
                        await HandlePeopleDeletedEvent(ucwaEvent, cancellationToken);
                        break;
                    case "updated":
                        await HandlePeopleUpdatedEvent(ucwaEvent, cancellationToken);
                        break;
                }
            }
        }

        #endregion

        #region Communication Handers

        private void HandleCommunicationAddedEvent(Event communication)
        {
            switch (communication.Link.Rel)
            {
                case "conversation":
                    ConversationAdded?.Invoke(communication.Embedded.Conversation);
                    break;
            }
        }

        private void HandleCommunicationCompletedEvent(Event communication)
        {
            switch (communication.Link.Rel)
            {
                case "messagingInvitation":
                    MessagingInvitationCompleted?.Invoke(communication.Embedded.MessagingInvitation);
                    break;
                case "onlineMeetingInvitation":
                    OnlineMeetingInvitationCompleted?.Invoke(communication.Embedded.OnlineMeetingInvitation);
                    break;
                case "participantInvitation":
                    ParticipantInvitationCompleted?.Invoke(communication.Embedded.ParticipantInvitation);
                    break;
                case "phoneAudioInvitation":
                    PhoneAudioInvitationCompleted?.Invoke(communication.Embedded.PhoneAudioInvitation);
                    break;
            }
        }

        private void HandleCommunicationDeletedEvent(Event communication)
        {
            switch (communication.Link.Rel)
            {
                case "conversation":
                    ConversationDeleted?.Invoke(communication.Link.Href);
                    break;
            }
        }

        private void HandleCommunicationUpdatedEvent(Event communication)
        {
            switch (communication.Link.Rel)
            {
                case "communication":
                    CommunicationUpdated?.Invoke(communication.Embedded.Communication);
                    break;
                case "conversation":
                    ConversationUpdated?.Invoke(communication.Embedded.Conversation);
                    break;
                case "messagingInvitation":
                    MessagingInvitationUpdated?.Invoke(communication.Embedded.MessagingInvitation);
                    break;
                case "missedItems":
                    MissedItemsUpdated?.Invoke(communication.Embedded.MissedItems);
                    break;
                case "onlineMeetingInvitation":
                    OnlineMeetingInvitationUpdated?.Invoke(communication.Embedded.OnlineMeetingInvitation);
                    break;
                case "participantInvitation":
                    ParticipantInvitationUpdated?.Invoke(communication.Embedded.ParticipantInvitation);
                    break;
                case "phoneAudioInvitation":
                    PhoneAudioInvitationUpdated?.Invoke(communication.Embedded.PhoneAudioInvitation);
                    break;
            }
        }

        private void HandleCommunicationStartedEvent(Event communication)
        {
            switch (communication.Link.Rel)
            {
                case "messagingInvitation":
                    if (communication.Embedded.MessagingInvitation.Direction == Direction.Incoming)
                        MessagingInvitationReceived?.Invoke(communication.Embedded.MessagingInvitation);
                    else
                        MessagingInvitationSent?.Invoke(communication.Embedded.MessagingInvitation);
                    break;
                case "onlineMeetingInvitation":
                    if (communication.Embedded.OnlineMeetingInvitation.Direction == Direction.Incoming)
                        OnlineMeetingInvitationReceived?.Invoke(communication.Embedded.OnlineMeetingInvitation);
                    else
                        OnlineMeetingInvitationSent?.Invoke(communication.Embedded.OnlineMeetingInvitation);
                    break;
                case "participantInvitation":
                    if (communication.Embedded.ParticipantInvitation.Direction == Direction.Incoming)
                        ParticipantInvitationReceived?.Invoke(communication.Embedded.ParticipantInvitation);
                    else
                        ParticipantInvitationSent?.Invoke(communication.Embedded.ParticipantInvitation);
                    break;
                case "phoneAudioInvitation":
                    if (communication.Embedded.PhoneAudioInvitation.Direction == Direction.Incoming)
                        PhoneAudioInvitationReceived?.Invoke(communication.Embedded.PhoneAudioInvitation);
                    else
                        PhoneAudioInvitationSent?.Invoke(communication.Embedded.PhoneAudioInvitation);
                    break;
            }
        }

        #endregion

        #region Conversation Handlers

        private async Task HandleConversationAddedEvent(Event conversation, CancellationToken cancellationToken)
        {
            switch (conversation.Link.Rel)
            {
                case "applicationSharer":
                    if (ApplicationSharerAdded != null)
                    {
                        ApplicationSharer applicationSharer = await HttpService.Get<ApplicationSharer>(conversation.Link, cancellationToken);
                        ApplicationSharerAdded.Invoke(applicationSharer);
                    }
                    break;
                case "localParticipant":
                    if (LocalParticipantAdded != null)
                    {
                        LocalParticipant localParticipant = await HttpService.Get<LocalParticipant>(conversation.Link, cancellationToken);
                        LocalParticipantAdded?.Invoke(localParticipant);
                    }
                    break;
                case "onlineMeeting":
                    OnlineMeetingAdded?.Invoke(conversation.Embedded.OnlineMeeting);
                    break;
                case "participant":
                    await HandleParticipantAddedEvent(conversation, cancellationToken);
                    break;
                case "participantApplicationSharing":
                    ParticipantApplicationSharingAdded?.Invoke(conversation.Embedded.ParticipantApplicationSharing);
                    break;
                case "participantAudio":
                    if (ParticipantAudioAdded != null)
                    {
                        ParticipantAudio participantAudio = await HttpService.Get<ParticipantAudio>(conversation.Link, cancellationToken);
                        ParticipantAudioAdded?.Invoke(participantAudio);
                    }
                    break;
                case "participantDataCollaboration":
                    if (ParticipantDataCollaborationAdded != null)
                    {
                        ParticipantDataCollaboration participantDataCollaboration = await HttpService.Get<ParticipantDataCollaboration>(conversation.Link, cancellationToken);
                        ParticipantDataCollaborationAdded?.Invoke(participantDataCollaboration);
                    }
                    break;
                case "participantMessaging":
                    if (ParticipantMessagingAdded != null)
                    {
                        ParticipantMessaging participantMessaging = await HttpService.Get<ParticipantMessaging>(conversation.Link, cancellationToken);
                        ParticipantMessagingAdded?.Invoke(participantMessaging);
                    }
                    break;
                case "participantPanoramicVideo":
                    if (ParticipantPanoramicVideoAdded != null)
                    {
                        ParticipantPanoramicVideo participantPanoramicVideo = await HttpService.Get<ParticipantPanoramicVideo>(conversation.Link, cancellationToken);
                        ParticipantPanoramicVideoAdded?.Invoke(participantPanoramicVideo);
                    }
                    break;
                case "participantVideo":
                    if (ParticipantVideoAdded != null)
                    {
                        ParticipantVideo participantVideo = await HttpService.Get<ParticipantVideo>(conversation.Link, cancellationToken);
                        ParticipantVideoAdded?.Invoke(participantVideo);
                    }
                    break;
                case "videoLockedOnParticipant":
                    VideoLockedOnParticipantAdded?.Invoke(conversation.Embedded.VideoLockedOnParticipant);
                    break;
            }
        }

        private void HandleConversationCompletedEvent(Event conversation)
        {
            switch (conversation.Link.Rel)
            {
                case "message":
                    if (conversation.Embedded.Message.Direction == Direction.Incoming)
                        MessageReceived?.Invoke(conversation.Embedded.Message);
                    else
                        MessageSent?.Invoke(conversation.Embedded.Message);
                    break;
            }
        }

        private async Task HandleConversationDeletedEvent(Event conversation, CancellationToken cancellationToken)
        {
            switch (conversation.Link.Rel)
            {
                case "applicationSharer":
                    if (ApplicationSharerDeleted != null)
                    {
                        ApplicationSharer applicationSharer = await HttpService.Get<ApplicationSharer>(conversation.Link, cancellationToken);
                        ApplicationSharerDeleted.Invoke(applicationSharer);
                    }
                    break;
                case "localParticipant":
                    if (LocalParticipantDeleted != null)
                    {
                        LocalParticipant localParticipant = await HttpService.Get<LocalParticipant>(conversation.Link, cancellationToken);
                        LocalParticipantDeleted.Invoke(localParticipant);
                    }
                    break;
                case "participant":
                    await HandleParticipantDeletedEvent(conversation, cancellationToken);
                    break;
                case "participantApplicationSharing":
                    ParticipantApplicationSharingDeleted?.Invoke(conversation.Embedded.ParticipantApplicationSharing);
                    break;
                case "participantAudio":
                    ParticipantAudioDeleted?.Invoke(conversation.Link.Href);
                    break;
                case "participantDataCollaboration":
                    ParticipantDataCollaborationDeleted?.Invoke(conversation.Link.Href);
                    break;
                case "participantMessaging":
                    ParticipantMessagingDeleted?.Invoke(conversation.Link.Href);
                    break;
                case "participantPanoramicVideo":
                    ParticipantPanoramicVideoDeleted?.Invoke(conversation.Link.Href);
                    break;
                case "participantVideo":
                    ParticipantVideoDeleted?.Invoke(conversation.Link.Href);
                    break;
                case "videoLockedOnParticipant":
                    VideoLockedOnParticipantDeleted?.Invoke(conversation.Embedded.VideoLockedOnParticipant);
                    break;
            }
        }

        private void HandleConversationStartedEvent(Event conversation)
        {
            switch (conversation.Link.Rel)
            {
                case "message":
                    MessageStarted?.Invoke(conversation.Embedded.Message);
                    break;
            }
        }

        private async Task HandleConversationUpdatedEvent(Event conversation, CancellationToken cancellationToken)
        {
            switch (conversation.Link.Rel)
            {
                case "applicationSharer":
                    if (ApplicationSharerUpdated != null)
                    {
                        ApplicationSharer applicationSharer = await HttpService.Get<ApplicationSharer>(conversation.Link, cancellationToken);
                        ApplicationSharerUpdated.Invoke(applicationSharer);
                    }
                    break;
                case "applicationSharing":
                    ApplicationSharingUpdated?.Invoke(conversation.Embedded.ApplicationSharing);
                    break;
                case "audioVideo":
                    AudioVideoUpdated?.Invoke(conversation.Embedded.AudioVideo);
                    break;
                case "dataCollaboration":
                    DataCollaborationUpdated?.Invoke(conversation.Embedded.DataCollaboration);
                    break;
                case "localParticipant":
                    if (LocalParticipantUpdated != null)
                    {
                        LocalParticipant localParticipant = await HttpService.Get<LocalParticipant>(conversation.Link, cancellationToken);
                        LocalParticipantUpdated.Invoke(localParticipant);
                    }
                    break;
                case "messaging":
                    MessagingUpdated?.Invoke(conversation.Embedded.Messaging);
                    break;
                case "onlineMeeting":
                    OnlineMeetingUpdated?.Invoke(conversation.Embedded.OnlineMeeting);
                    break;
                case "participant":
                    if (ParticipantUpdated != null)
                    {
                        Participant participant = await HttpService.Get<Participant>(conversation.Link, cancellationToken);
                        ParticipantUpdated.Invoke(participant);
                    }
                    break;
                case "participantApplicationSharing":
                    if (ParticipantApplicationSharingUpdated != null)
                    {
                        ParticipantApplicationSharing participantApplicationSharing = await HttpService.Get<ParticipantApplicationSharing>(conversation.Link, cancellationToken);
                        ParticipantApplicationSharingUpdated?.Invoke(participantApplicationSharing);
                    }
                    break;
                case "participantAudio":
                    if (ParticipantAudioUpdated != null)
                    {
                        ParticipantAudio participantAudio = await HttpService.Get<ParticipantAudio>(conversation.Link, cancellationToken);
                        ParticipantAudioUpdated?.Invoke(participantAudio);
                    }
                    break;
                case "participantDataCollaboration":
                    if (ParticipantDataCollaborationUpdated != null)
                    {
                        ParticipantDataCollaboration participantDataCollaboration = await HttpService.Get<ParticipantDataCollaboration>(conversation.Link, cancellationToken);
                        ParticipantDataCollaborationUpdated?.Invoke(participantDataCollaboration);
                    }
                    break;
                case "participantMessaging":
                    if (ParticipantMessagingUpdated != null)
                    {
                        ParticipantMessaging participantMessaging = await HttpService.Get<ParticipantMessaging>(conversation.Link, cancellationToken);
                        ParticipantMessagingUpdated?.Invoke(participantMessaging);
                    }
                    break;
                case "participantPanoramicVideo":
                    if (ParticipantPanoramicVideoUpdated != null)
                    {
                        ParticipantPanoramicVideo participantPanoramicVideo = await HttpService.Get<ParticipantPanoramicVideo>(conversation.Link, cancellationToken);
                        ParticipantPanoramicVideoUpdated?.Invoke(participantPanoramicVideo);
                    }
                    break;
                case "participantVideo":
                    if (ParticipantVideoUpdated != null)
                    {
                        ParticipantVideo participantVideo = await HttpService.Get<ParticipantVideo>(conversation.Link, cancellationToken);
                        ParticipantVideoUpdated?.Invoke(participantVideo);
                    }
                    break;
                case "phoneAudio":
                    PhoneAudioUpdated?.Invoke(conversation.Embedded.PhoneAudio);
                    break;
                case "videoLockedOnParticipant":
                    VideoLockedOnParticipantUpdated?.Invoke(conversation.Embedded.VideoLockedOnParticipant);
                    break;
            }
        }

        #endregion

        #region Me Handlers

        private void HandleMeAddedEvent(Event me)
        {
            switch (me.Link.Rel)
            {
                case "location":
                    MyLocationAdded?.Invoke();
                    break;
                case "note":
                    MyNoteAdded?.Invoke();
                    break;
                case "presence":
                    MyPresenceAdded?.Invoke();
                    break;
            }
        }

        private void HandleMeDeletedEvent(Event me)
        {
            switch (me.Link.Rel)
            {
                case "location":
                    MyLocationDeleted?.Invoke();
                    break;
                case "note":
                    MyNoteDeleted?.Invoke();
                    break;
                case "presence":
                    MyPresenceDeleted?.Invoke();
                    break;
            }
        }

        private async Task HandleMeUpdatedEvent(Event me, CancellationToken cancellationToken)
        {
            switch (me.Link.Rel)
            {
                case "me":
                    MeStarted?.Invoke();
                    break;
                case "location":
                    if (MyLocationUpdated != null)
                    {
                        Location location = await Me.GetLocation(cancellationToken);
                        MyLocationUpdated.Invoke(location);
                    }
                    break;
                case "note":
                    if (MyNoteUpdated != null)
                    {
                        Note note = await Me.GetNote(cancellationToken);
                        MyNoteUpdated.Invoke(note);
                    }
                    break;
                case "presence":
                    if (MyPresenceUpdated != null)
                    {
                        Presence presence = await Me.GetPresence(cancellationToken);
                        MyPresenceUpdated.Invoke(presence);
                    }
                    break;
            }
        }

        #endregion

        #region People Handlers

        private async Task HandlePeopleAddedEvent(Event people, CancellationToken cancellationToken)
        {
            switch (people.Link.Rel)
            {
                case "contact":
                    await HandleContactAddedEvent(people, cancellationToken);
                    break;
                case "group":
                    await HandleGroupAddedEvent(people, cancellationToken);
                    break;
                case "distributionGroup":
                    await HandleDistributionGroupAddedEvent(people, cancellationToken);
                    break;
                case "presenceSubscription":
                    if (PresenceSubscriptionAdded != null)
                    {
                       var presenceSubscription = people.Embedded?.PresenceSubscription ?? await httpService.Get<PresenceSubscription>(people.Link.Href, cancellationToken);
                       PresenceSubscriptionAdded.Invoke(presenceSubscription);
                    }
                    break;
                case "defaultGroup":
                case "pinnedGroup":
                    break;
            }
        }

        private async Task HandlePeopleDeletedEvent(Event people, CancellationToken cancellationToken)
        {
            switch (people.Link.Rel)
            {
                case "contact":
                    await HandleContactDeletedEvent(people, cancellationToken);
                    break;
                case "group":
                    HandleGroupDeletedEvent(people);
                    break;
                case "distributionGroup":
                    HandleDistributionGroupDeletedEvent(people);
                    break;
                case "presenceSubscription":
                    PresenceSubscriptionDeleted?.Invoke(people.Link.Href);
                    break;
                case "defaultGroup":
                case "pinnedGroup":
                    break;
            }
        }

        private async Task HandlePeopleUpdatedEvent(Event people, CancellationToken cancellationToken)
        {
            switch (people.Link.Rel)
            {
                case "contactLocation":
                    if (ContactLocationUpdated != null)
                    {
                        Contact contact = people.In == null ? null : await HttpService.Get<Contact>(people.In.Href, cancellationToken);
                        ContactLocation contactLocation = await HttpService.Get<ContactLocation>(people.Link.Href, cancellationToken);
                        ContactLocationUpdated.Invoke(contactLocation, contact);
                    }
                    break;
                case "contactNote":
                    if (ContactNoteUpdated != null)
                    {
                        Contact contact = people.In == null ? null : await HttpService.Get<Contact>(people.In.Href, cancellationToken);
                        ContactNote contactNote = await HttpService.Get<ContactNote>(people.Link.Href, cancellationToken);
                        ContactNoteUpdated.Invoke(contactNote, contact);
                    }
                    break;
                case "contactPresence":
                    if (ContactPresenceUpdated != null)
                    {
                        Contact contact = people.In == null ? null : await HttpService.Get<Contact>(people.In.Href, cancellationToken);
                        ContactPresence contactPresence = await HttpService.Get<ContactPresence>(people.Link.Href, cancellationToken);
                        ContactPresenceUpdated.Invoke(contactPresence, contact);
                    }
                    break;
                case "contactPrivacyRelationship":
                    if (ContactPrivacyRelationshipUpdated != null)
                    {
                        Contact contact = people.In == null ? null : await HttpService.Get<Contact>(people.In.Href, cancellationToken);
                        ContactPrivacyRelationship2 contactPrivacyRelationship2 = await HttpService.Get<ContactPrivacyRelationship2>(people.Link.Href, cancellationToken);
                        ContactPrivacyRelationshipUpdated.Invoke(contactPrivacyRelationship2, contact);
                    }
                    break;
                case "contactSupportedModalities":
                    if (ContactSupportedModalitiesUpdated != null)
                    {
                        Contact contact = people.In == null ? null : await HttpService.Get<Contact>(people.In.Href, cancellationToken);
                        ContactSupportedModalities contactSupportedModalities = await HttpService.Get<ContactSupportedModalities>(people.Link.Href, cancellationToken);
                        ContactSupportedModalitiesUpdated.Invoke(contactSupportedModalities, contact);
                    }
                    break;
                case "distributionGroup":
                    DistributionGroupUpdated?.Invoke(people.Embedded.DistributionGroup);
                    break;
                case "group":
                    GroupUpdated?.Invoke(people.Embedded.Group);
                    break;
                case "myContactsAndGroupsSubscription":
                    MyContactsAndGroupsSubscriptionExpiring?.Invoke(people.Embedded.MyContactsAndGroupsSubscription);
                    break;
                case "presenceSubscription":
                    PresenceSubscriptionUpdated?.Invoke(people.Embedded.PresenceSubscription);
                    break;
            }
        }

        #endregion

        #region Contact Handlers

        private async Task HandleContactAddedEvent(Event people, CancellationToken cancellationToken)
        {
            switch (people.In.Rel)
            {
                case "group":
                    if (ContactAddedToGroup != null)
                    {
                        Contact contact = await HttpService.Get<Contact>(people.Link.Href, cancellationToken);
                        Group group = await HttpService.Get<Group>(people.In.Href, cancellationToken);
                        ContactAddedToGroup.Invoke(contact, group);
                    }
                    break;
                case "myContacts":
                    if (ContactAdded != null)
                    {
                        Contact contact = await HttpService.Get<Contact>(people.Link.Href, cancellationToken);
                        ContactAdded.Invoke(contact);
                    }
                    break;
                case "subscribedContacts":
                    if (ContactSubscriptionAdded != null)
                    {
                        Contact contact = await HttpService.Get<Contact>(people.Link.Href, cancellationToken);
                        ContactSubscriptionAdded.Invoke(contact);
                    }
                    break;
            }
        }

        private async Task HandleContactDeletedEvent(Event people, CancellationToken cancellationToken)
        {
            switch (people.In.Rel)
            {
                case "group":
                    if (ContactDeletedFromGroup != null)
                    {
                        Contact contact = await HttpService.Get<Contact>(people.Link.Href, cancellationToken);
                        Group group = await HttpService.Get<Group>(people.In.Href, cancellationToken);
                        ContactDeletedFromGroup.Invoke(contact, group);
                    }
                    break;
                case "myContacts":
                    if (ContactDeleted != null)
                    {
                        Contact contact = await HttpService.Get<Contact>(people.Link.Href, cancellationToken);
                        ContactDeleted.Invoke(contact);
                    }
                    break;
                case "subscribedContacts":
                    if (ContactSubscriptionDeleted != null)
                    {
                        Contact contact = await HttpService.Get<Contact>(people.Link.Href, cancellationToken);
                        ContactSubscriptionDeleted.Invoke(contact);
                    }
                    break;
            }
        }

        #endregion

        #region Participant Handlers

        private async Task HandleParticipantAddedEvent(Event conversation, CancellationToken cancellationToken)
        {
            if (conversation.In == null)
            {
                if (ParticipantAdded != null)
                {
                    Participant participant = await HttpService.Get<Participant>(conversation.Link, cancellationToken);
                    if (participant == null)
                        participant = new Participant() { Name = conversation.Link.Title, Uri = conversation.Link.Href };
                    ParticipantAdded.Invoke(participant);
                }
            }
            else
            {
                switch (conversation.In.Rel)
                {
                    case "attendees":
                        if (AttendeesAdded != null)
                        {
                            Attendees attendees = await HttpService.Get<Attendees>(conversation.In, cancellationToken);
                            AttendeesAdded.Invoke(attendees);
                        }
                        break;
                    case "leaders":
                        if (LeadersAdded != null)
                        {
                            Leaders leaders = await HttpService.Get<Leaders>(conversation.In, cancellationToken);
                            LeadersAdded.Invoke(leaders);
                        }
                        break;
                    case "lobby":
                        if (LobbyAdded != null)
                        {
                            Lobby lobby = await HttpService.Get<Lobby>(conversation.In, cancellationToken);
                            LobbyAdded.Invoke(lobby);
                        }
                        break;
                    case "typingParticipants":
                        if (TypingParticipantsAdded != null)
                        {
                            TypingParticipants typingParticipants = await HttpService.Get<TypingParticipants>(conversation.In.ETag, cancellationToken);
                            TypingParticipantsAdded.Invoke(typingParticipants);
                        }
                        break;
                }
            }
        }

        private async Task HandleParticipantDeletedEvent(Event conversation, CancellationToken cancellationToken)
        {
            if (conversation.In == null)
            {
                ParticipantDeleted?.Invoke(conversation.Link.Href);
            }
            else
            {
                switch (conversation.In.Rel)
                {
                    case "attendees":
                        if (AttendeesDeleted != null)
                        {
                            Attendees attendees = await HttpService.Get<Attendees>(conversation.In, cancellationToken);
                            AttendeesDeleted.Invoke(attendees);
                        }
                        break;
                    case "leaders":
                        if (LeadersDeleted != null)
                        {
                            Leaders leaders = await HttpService.Get<Leaders>(conversation.In, cancellationToken);
                            LeadersDeleted.Invoke(leaders);
                        }
                        break;
                    case "lobby":
                        if (LobbyDeleted != null)
                        {
                            Lobby lobby = await HttpService.Get<Lobby>(conversation.In, cancellationToken);
                            LobbyDeleted.Invoke(lobby);
                        }
                        break;
                    case "typingParticipants":
                        if (TypingParticipantsDeleted != null)
                        {
                            TypingParticipants typingParticipants = await HttpService.Get<TypingParticipants>(conversation.In, cancellationToken);
                            TypingParticipantsDeleted.Invoke(typingParticipants);
                        }
                        break;
                }
            }
        }

        #endregion

        #region Group Handlers

        private async Task HandleGroupAddedEvent(Event people, CancellationToken cancellationToken)
        {
            switch (people.In.Rel)
            {
                case "myGroups":
                    if (GroupAddedToMyGroups != null)
                    {
                        Group group = await HttpService.Get<Group>(people.Link.Href, cancellationToken);
                        GroupAddedToMyGroups.Invoke(group);
                    }
                    break;
            }
        }

        private void HandleGroupDeletedEvent(Event people)
        {
            switch (people.In.Rel)
            {
                case "myGroups":
                    GroupDeletedFromMyGroups?.Invoke(people.Link.Href);
                    break;
            }
        }

        private async Task HandleDistributionGroupAddedEvent(Event people, CancellationToken cancellationToken)
        {
            switch (people.In.Rel)
            {
                case "myGroup":
                    if (DistributionGroupAddedToMyGroups != null)
                    {
                        DistributionGroup distributionGroup = await HttpService.Get<DistributionGroup>(people.Link.Href, cancellationToken);
                        DistributionGroupAddedToMyGroups.Invoke(distributionGroup);
                    }
                    break;
            }
        }

        private void HandleDistributionGroupDeletedEvent(Event people)
        {
            switch (people.In.Rel)
            {
                case "myGroup":
                    DistributionGroupDeletedFromMyGroups?.Invoke(people.Link.Href);
                    break;
            }
        }

        #endregion

        #endregion
    }
}
