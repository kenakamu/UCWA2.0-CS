using Microsoft.Skype.UCWA;
using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Models;
using System;
using System.Linq;
using System.Net.Http.Headers;

namespace TestClient
{
    class UCWASample
    {
        UCWAClient client;
        bool autoAcceptSet = true;

        public UCWASample()
        {
            client = new UCWAClient();
            client.SendingRequest += (client, resource) => { client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenService.AquireAADToken(resource)); };

            client.ContactPresenceUpdated += Client_ContactPresenceUpdated;
            client.ContactAdded += Client_ContactAdded;
            client.ContactAddedToGroup += Client_ContactAddedToGroup;
            client.ContactDeleted += Client_ContactDeleted;
            client.ContactDeletedFromGroup += Client_ContactDeletedFromGroup;

            client.MessageReceived += Client_MessageReceived;
            client.MessagingInvitationReceived += Client_MessagingInvitationReceived;
            client.OnlineMeetingInvitationReceived += Client_OnlineMeetingInvitationReceived;
            client.GroupAddedToMyGroups += Client_GroupAddedToMyGroups;
            client.GroupDeletedFromMyGroups += Client_GroupDeletedFromMyGroups;

            Signin(TokenService.username, TokenService.password);
        }

        public void Run()
        {
            while (true)
            {               
                ConsoleWrite_Green("Enter command (signin | signout | me | contact | group | chat | meeting) >");
                var result = Console.ReadLine();
                switch (result.ToLower().Replace(" ",""))
                {
                    case "signin":
                        Signin();
                        break;
                    case "signout":
                        Signout();
                        break;
                    case "me":
                        Me();
                        break;
                    case "contact":
                        Contact();
                        break;
                    case "group":
                        Group();
                        break;
                    case "chat":
                        Chat();
                        break;
                    case "meeting":
                        Meeting();
                        break;
                }
            }
        }

        #region Login/Logoff

        private void Signin(string username = "", string password = "")
        {
            if (string.IsNullOrEmpty(username))
            {
                ConsoleWrite_White("username:");
                username = Console.ReadLine();
                TokenService.username = username;
            }
            
            if (string.IsNullOrEmpty(password))
            {
                ConsoleWrite_White("password:");
                password = Console.ReadLine();
                TokenService.password = password;
            }

            client.Initialize(username.Split('@')[1]).Wait();
            client.SignIn(availability:Availability.Online, supportMessage:true, supportAudio:false, supportPlainText:true, supportHtmlFormat:false, phoneNumber:"", keepAlive:true).Wait();

            #region Initial message
            
            ConsoleWrite_White("**************************************************");
            ConsoleWrite_White("****  UCWA C# SDK Sample console application. ****");
            ConsoleWrite_White("**************************************************");
            ConsoleWrite_White("");
            ConsoleWrite_White("**************************************************");
            ConsoleWrite_White("Application Id: {0}", client.Host + client.Application.Self);
            ConsoleWrite_White("Token: {0}", TokenService.AquireAADToken(client.Host));
            ConsoleWrite_White("**************************************************");

            #endregion
        }

        private void Signout()
        {
            if (client.Application == null)
                ConsoleWrite_White("No app");
            else
                client.Application.Delete().Wait();

            client.Application = null;
            TokenService.SignOut();

            ConsoleWrite_White("logged off");
        }

        #endregion

        #region Me

        private void Me()
        {
            if (client.Application == null)
            {
                ConsoleWrite_White("Login first");
                return;
            }

            while (true)
            {
                ConsoleWrite_Green("Enter command (info | presence | location | note | back) >");
                var result = Console.ReadLine();
                switch (result.ToLower().Replace(" ",""))
                {
                    case "info":
                        DisplayMe();
                        break;
                    case "presence":
                        Presence();
                        break;
                    case "location":
                        Location();
                        break;
                    case "note":
                        Note();
                        break;
                    case "back":
                        return;
                    default:
                        ConsoleWrite_Red("Invalid Command");
                        break;
                }
            }
        }

        private void DisplayMe()
        {
            ConsoleWrite_White("department: {0}", client.Me.Department);
            foreach (var email in client.Me.EmailAddresses) { ConsoleWrite_White("email: {0}", email); }
            ConsoleWrite_White("name: {0}", client.Me.Name);
            ConsoleWrite_White("title: {0}", client.Me.Title);
            ConsoleWrite_White("sip: {0}", client.Me.Uri);
            var location = client.Me.GetLocation().Result;
            ConsoleWrite_White("location: {0}", location.Address);
            var note = client.Me.GetNote().Result;
            ConsoleWrite_White("note: {0}", note.Message);
            foreach (var phone in client.Me.GetPhones().Result) { ConsoleWrite_White("phone: {0}", phone); }
            ConsoleWrite_White("photo: {0}", client.Me.GetPhoto().Result);
            var presence = client.Me.GetPresencs().Result;
            ConsoleWrite_White("presence: {0}", presence.Availability);
        }

        private void Presence()
        {
            var presence = client.Me.GetPresencs().Result;
            ConsoleWrite_White("Select new status");
            ConsoleWrite_White("Online | Offline | Away | BeRightBack | Busy | DoNotDisturb");
            var result = Console.ReadLine();
            Availability availability;
            Enum.TryParse<Availability>(result, out availability);

            if (availability == Availability.NotSet)
            {
                ConsoleWrite_Red("invalid state");
                return;
            }
            presence.Availability = availability;
            presence.Update().Wait();
        }

        private void Location()
        {
            var location = client.Me.GetLocation().Result;
            ConsoleWrite_White("Enter new location");
            var result = Console.ReadLine();
            location.Address = result;
            location.Update().Wait();
        }

        private void Note()
        {
            var note = client.Me.GetNote().Result;
            ConsoleWrite_White("Enter new note");
            var result = Console.ReadLine();
            note.Message = result;
            note.Update().Wait();
        }

        #endregion
        
        #region Contact

        private void Contact()
        {
            if (client.Application == null)
            {
                ConsoleWrite_White("Login first");
                return;
            }

            while (true)
            {
                ConsoleWrite_Green("Enter command (all | search | detail | subscribe | subscribe all | unsubscribe all | add | remove | back) >");
                var result = Console.ReadLine();
                switch (result.ToLower().Replace(" ",""))
                {
                    case "all":
                        AllContact();
                        break;
                    case "search":
                        SearchContact();
                        break;
                    case "detail":
                        ContactDetail();
                        break;
                    case "subscribe":
                        Subscribe();
                        break;
                    case "subscribeall":
                        SubscribeAllContact();
                        break;
                    case "unsubscribeall":
                        UnsubscribeAllContact();
                        break;
                    case "add":
                        AddContact();
                        break;
                    case "remove":
                        RemoveContact();
                        break;
                    case "back":
                        return;
                    default:
                        ConsoleWrite_Red("Invalid Command");
                        break;
                }                
            }
        }

        private void AllContact()
        {
            ConsoleWrite_Green("all contacts");
            foreach(var contact in client.People.GetMyContacts().Result.Contacts)
            {
                ConsoleWrite_White("name: {0}, {1}", contact.Name, contact.Uri);
            }

            ConsoleWrite_Green("subscribed contacts");
            if (client.People.GetSubscribedContacts().Result?.GetContacts().Result == null)
                return;

            foreach (var contact in client.People.GetSubscribedContacts().Result.GetContacts().Result)
            {
                ConsoleWrite_White("name: {0}", contact.Name);
            }
        }

        private void SearchContact()
        {
            ConsoleWrite_White("Enter serach query");
            var result = Console.ReadLine();
            var searchResult = client.People.Search(result).Result;
            foreach (var contact in searchResult.Contacts)
            {
                ConsoleWrite_White("name: {0}, sip: {1}", contact.Name, contact.Uri);
            }
        }

        private void ContactDetail()
        {
            ConsoleWrite_White("Enter sip");
            var result = Console.ReadLine();
            var searchResult = client.People.Search(result).Result;

            var contact = searchResult.Contacts.FirstOrDefault();

            if (contact != null)
            {
                ConsoleWrite_White("name: {0}", contact.Name);
                ConsoleWrite_White("title: {0}", contact.Title);
                ConsoleWrite_White("office: {0}", contact.Office);
                ConsoleWrite_White("department: {0}", contact.Department);
                if (contact.EmailAddresses != null)
                    foreach (var email in contact.EmailAddresses) { ConsoleWrite_White("email: {0}", email); }
                ConsoleWrite_White("sip: {0}", contact.Uri);
                ConsoleWrite_White("location: {0}", contact.GetContactLocation().Result.Location);
                ConsoleWrite_White("note: {0}", contact.GetContactNote().Result.Message);
                ConsoleWrite_White("home phone: {0}", contact.HomePhoneNumber);
                ConsoleWrite_White("mobile phone: {0}", contact.MobilePhoneNumber);
                ConsoleWrite_White("other phone: {0}", contact.OtherPhoneNumber);

                ConsoleWrite_White("photo: {0}", contact.GetContactPhoto().Result);
                ConsoleWrite_White("presence: {0}", contact.GetContactPresence().Result.Availability);
            }            
        }

        private void Subscribe()
        {
            ConsoleWrite_White("Enter sip");
            var result = Console.ReadLine();
            client.SubscribeContactsChange(result).Wait();
        }

        private void SubscribeAllContact()
        {
            foreach(Contact contact in client.People.GetMyContacts().Result.Contacts)
            {
                client.SubscribeContactsChange(contact.Uri).Wait();
            }
        }

        private void UnsubscribeAllContact()
        {
            foreach (Contact contact in client.People.GetMyContacts().Result.Contacts)
            {
                client.UnSubscribeContactsChange(contact.Uri).Wait();
            }
        }

        private void AddContact()
        {
            ConsoleWrite_White("Enter Group Name");
            var result = Console.ReadLine();
            var group = client.People.GetMyGroups().Result.Groups.Where(x => x.Name.ToLower() == result.ToLower()).FirstOrDefault();

            if (group == null)
                ConsoleWrite_White("No group found");
            else
            {
                var membership = client.People.GetMyGroupMemberships().Result;

                ConsoleWrite_White("Enter sip to add");
                var sip = Console.ReadLine();

                membership.AddContact(sip, group.Id).Wait();
            }
        }

        private void RemoveContact()
        {
            ConsoleWrite_White("Enter sip");
            var result = Console.ReadLine();
            var myGroupMemberships = client.People.GetMyGroupMemberships().Result;
            myGroupMemberships.RemoveContactFromAllGroups(result).Wait();
        }

        #endregion

        #region Group

        private void Group()
        {
            if (client.Application == null)
            {
                ConsoleWrite_White("Login first");
                return;
            }

            while (true)
            {
                ConsoleWrite_Green("Enter command (all | contact | subscribe contacts in group | subscribe all | unsubscribe all | new | update | delete | back) >");
                var result = Console.ReadLine();
                switch (result.ToLower().Replace(" ",""))
                {
                    case "all":
                        AllGroup();
                        break;
                    case "contact":
                        GroupContacts();
                        break;
                    case "subscribecontactsingroup":
                        SubscribeContactsInGroup();
                        break;
                    case "subscribeall":
                        SubscribeAll();
                        break;
                    case "unsubscribeall":
                        UnsubscribeAll();
                        break;
                    case "new":
                        NewGroup();
                        break;
                    case "update":
                        UpdateGroup();
                        break;
                    case "delete":
                        DeleteGroup();
                        break;
                    case "back":
                        return;
                    default:
                        ConsoleWrite_Red("Invalid Command");
                        break;
                }
            }
        }

        private void AllGroup()
        {
            foreach (var group in client.People.GetMyGroups().Result.Groups)
            {
                ConsoleWrite_White("name: {0}", group.Name);
            }
        }             

        private void GroupContacts()
        {
            ConsoleWrite_White("Enter Group Name");
            var result = Console.ReadLine();
            var group = client.People.GetMyGroups().Result.Groups.Where(x => x.Name.ToLower() == result.ToLower()).FirstOrDefault();
            
            if (group != null)
                ConsoleWrite_White("No group found");
            else
            {
                foreach(var contact in group.GetGroupContacts().Result.Contacts)
                {
                    ConsoleWrite_White("name: {0}", contact.Name);
                }
            }
            
        }

        private void SubscribeContactsInGroup()
        {
            ConsoleWrite_White("Enter Group Name");
            var result = Console.ReadLine();
            var group = client.People.GetMyGroups().Result.Groups.Where(x => x.Name.ToLower() == result.ToLower()).FirstOrDefault();

            if (group == null)
                ConsoleWrite_White("No group found");
            else
                group.SubscribeToGroupPresence().Wait();
        }

        private void SubscribeAll()
        {
            MyContactsAndGroupsSubscription myContactsAndGroupsSubscription = client.People.GetMyContactsAndGroupsSubscription().Result;
            myContactsAndGroupsSubscription.StartOrRefreshSubscriptionToContactsAndGroups(30).Wait();
        }

        private void UnsubscribeAll()
        {
            MyContactsAndGroupsSubscription myContactsAndGroupsSubscription = client.People.GetMyContactsAndGroupsSubscription().Result;
            myContactsAndGroupsSubscription.StopSubscriptionToContactsAndGroups().Wait();
        }

        private void NewGroup()
        {
            ConsoleWrite_White("Enter New Group Name");
            var result = Console.ReadLine();
            client.CreateGroup(result).Wait();
        }

        private void UpdateGroup()
        {
            ConsoleWrite_White("Enter Group Name");
            var result = Console.ReadLine();
            var group = client.People.GetMyGroups().Result.Groups.Where(x => x.Name.ToLower() == result.ToLower()).FirstOrDefault();
            if (group == null)
                ConsoleWrite_White("No group found");
            else
            {
                ConsoleWrite_White("Enter New Group Name");
                result = Console.ReadLine();
                group.Name = result;
                group.Update().Wait();
            }
        }

        private void DeleteGroup()
        {
            ConsoleWrite_White("Enter Group Name");
            var result = Console.ReadLine();
            var group = client.People.GetMyGroups().Result.Groups.Where(x => x.Name.ToLower() == result.ToLower()).FirstOrDefault();
            if (group == null)
                ConsoleWrite_White("No group found");
            else
                group.Delete().Wait();
        }

        #endregion

        #region Chat

        private void Chat()
        {
            if (client.Application == null)
            {
                ConsoleWrite_White("Login first");
                return;
            }

            while (true)
            {
                ConsoleWrite_Green("Enter command (info | enable history | all | start | reply | auto accept | invite | delete all | history | back) >");
                var result = Console.ReadLine();
                switch (result.ToLower().Replace(" ",""))
                {
                    case "info":
                        ChatInfo();
                        break;
                    case "enablehistory":
                        EnableHistory();
                        break;
                    case "all":
                        AllChat();
                        break;
                    case "start":
                        StartChat();
                        break;
                    case "reply":
                        ReplyChat();
                        break;
                    case "autoaccept":
                        AutoAccept();
                        break;
                    case "invite":
                        InviteChat();
                        break;
                    case "deleteall":
                        DeleteAllChat();
                        break;
                    case "history":
                        History();
                        break;
                    case "back":
                        return;
                    default:
                        ConsoleWrite_Red("Invalid Command");
                        break;
                }
            }
        }

        private void ChatInfo()
        {
            var communication = client.Communication;
            ConsoleWrite_White("Conversation History: {0}", communication.ConversationHistory);
            ConsoleWrite_White("Supported Message Formats: {0}", string.Join(",",communication.SupportedMessageFormats));
            ConsoleWrite_White("Supported Modalities: {0}", string.Join(",",communication.SupportedModalities));
        }

        private void EnableHistory()
        {
            var communication = client.Communication;
            communication.ConversationHistory = GenericPolicy.Enabled;
            communication.Update().Wait();
            client.Refresh().Wait();
        }


        private void AllChat()
        {
            if (client.Communication.GetConversations().Result == null || client.Communication.GetConversations().Result.GetConversations().Result ==null)
                return;
            foreach(var conversation in client.Communication.GetConversations().Result.GetConversations().Result)
            {
                ConsoleWrite_White("name: {0}", conversation.Subject);
            }
        }
        
        private void StartChat()
        {
            ConsoleWrite_White("Enter sip");
            var sip = Console.ReadLine();
            ConsoleWrite_White("Enter subject");
            var subject = Console.ReadLine();
            ConsoleWrite_White("Enter message");
            var message = Console.ReadLine();
            client.Communication.StartMessaging(sip, subject, Importance.Normal, message).Wait();
        }

        private void ReplyChat()
        {
            var conversation = SelectConversation();
            if (conversation == null)
                return;

            ConsoleWrite_White("Enter message");
            var message = Console.ReadLine();
            client.ReplyMessage(message, conversation).Wait();
        }

        private void InviteChat()
        {
            var conversation = SelectConversation();
            if (conversation == null)
                return;

            ConsoleWrite_White("Enter sip name");
            var sip = Console.ReadLine();

            client.AddParticipant(sip, conversation).Wait();
        }

        private void DeleteAllChat()
        {
            if (client.Communication.GetConversations().Result?.GetConversations().Result == null)
                return;

            foreach (var conversation in client.Communication.GetConversations().Result.GetConversations().Result)
            {
                conversation.Delete().Wait();
            }
        }

        private void History()
        {
            if (client.Communication.ConversationHistory == GenericPolicy.Disabled)
            {
                ConsoleWrite_White("history is disabled");
                return;
            }

            ConversationLogs historyLogs = client.Communication.GetConversationLogs().Result;
            if (historyLogs == null)
            {
                ConsoleWrite_White("no history yet");
                return;
            }

            foreach(var history in historyLogs.GetConversationLogs().Result)
            {
                ConsoleWrite_White("history: {0}, participants: {1}", history.PreviewMessage, string.Join(",", history.ConversationLogRecipients.Select(x => x.DisplayName)));
            }
        }

        private void AutoAccept()
        {
            ConsoleWrite_White("Select Auto Accept setting");
            ConsoleWrite_White("accept | reject");
            var result = Console.ReadLine();
            autoAcceptSet = result.ToLower() == "accept" ? true : false;
        }


        #endregion

        #region Meeting

        private void Meeting()
        {
            if (client.Application == null)
            {
                ConsoleWrite_White("Login first");
                return;
            }

            while (true)
            {
                ConsoleWrite_Green("Enter command (all | detail | adhoc | schedule | invite | promote | demote | add messaging | auto accept | join | leave | delete all | back) >");
                var result = Console.ReadLine();
                switch (result.ToLower().Replace(" ",""))
                {
                    case "all":
                        AllMeeting();
                        break;
                    case "detail":
                        MeetingDetail();
                        break;
                    case "adhoc":
                        Adhoc();
                        break;
                    case "schedule":
                        Schedule();
                        break;
                    case "invite":
                        InviteMeeting();
                        break;
                    case "promote":
                        Promote();
                        break;
                    case "demote":
                        Demote();
                        break;
                    case "addmessaging":
                        MeetingAddMessaging();
                        break;
                    case "autoaccept":
                        AutoAccept();
                        break;
                    case "join":
                        JoinMeeting();
                        break;
                    case "leave":
                        LeaveMeeting();
                        break;
                    case "deleteall":
                        DeleteAllMeeting();
                        break;
                    case "back":
                        return;
                    default:
                        ConsoleWrite_Red("Invalid Command");
                        break;
                }
            }
        }

        private void AllMeeting()
        {
            if (client.OnlineMeetings.GetMyOnlineMeetings().Result == null || client.OnlineMeetings.GetMyOnlineMeetings().Result.OnlineMeetings == null)
                return;
            foreach (var onlineMeeting in client.OnlineMeetings.GetMyOnlineMeetings().Result.OnlineMeetings)
            {
                ConsoleWrite_White("name: {0}", onlineMeeting.Subject);
            }
        }

        private void MeetingDetail()
        {
            var conversation = SelectConversation();
            if (conversation == null)
                return;

            var participants = conversation.GetAttendees().Result?.GetParticipants().Result;
            var leaders = conversation.GetLeaders().Result?.GetParticipants().Result;

            ConsoleWrite_White("participant count: {0}", conversation.ParticipantCount);
            if (leaders != null)
                ConsoleWrite_White("Leaders: {0}", string.Join(",", leaders.Select(x => x.Name)));
            if (participants != null)
                ConsoleWrite_White("Perticipants: {0}", string.Join(",", participants.Select(x => x.Name)));
        }

        private void Adhoc()
        {
            ConsoleWrite_White("Enter Meeting Subject.");
            var subject = Console.ReadLine();
            client.StartOnlineMeeting(subject, Importance.Normal).Wait();
        }
        
        private void Schedule()
        {
            ConsoleWrite_White("Enter Subject.");
            var subject = Console.ReadLine();
            ConsoleWrite_White("Enter Description.");
            var description = Console.ReadLine();
            ConsoleWrite_White("Enter attendee sip.");
            var attendee = Console.ReadLine();
            MyOnlineMeeting meeting = new MyOnlineMeeting()
            {
                AccessLevel = AccessLevel.Everyone,
                Attendees = new string[] { attendee },
                Subject = subject,
                Description = description,
                ExpirationTime = DateTime.Now.AddDays(10)
            };

            client.CreateScheduledOnlineMeeting(meeting).Wait();
        }

        private void InviteMeeting()
        {
            var conversation = SelectConversation();
            if (conversation == null)
                return;

            ConsoleWrite_White("Enter sip");
            var sip = Console.ReadLine();

            client.AddParticipant(sip, conversation).Wait();
        }

        private void Promote()
        {
            var conversation = SelectConversation();
            if (conversation == null)
                return;

            ConsoleWrite_White("Enter sip");
            var sip = Console.ReadLine();

            var participant = conversation.GetAttendees().Result.GetParticipants().Result.Where(x => x.Uri == sip).FirstOrDefault();
            if (participant == null)
                ConsoleWrite_White("No participant found");
            else
                participant.Promote().Wait();

        }

        private void Demote()
        {
            var conversation = SelectConversation();
            if (conversation == null)
                return;

            ConsoleWrite_White("Enter sip");
            var sip = Console.ReadLine();

            var participant = conversation.GetLeaders().Result.GetParticipants().Result.Where(x => x.Uri == sip).FirstOrDefault();
            if (participant == null)
                ConsoleWrite_White("No leader found");
            else
                participant.Demote().Wait();
        }

        private void MeetingAddMessaging()
        {
            var conversation = SelectConversation();
            if (conversation == null)
                return;

            ConsoleWrite_White("Enter message ");
            var message = Console.ReadLine();

            client.AddMessaging(conversation, message).Wait();

        }

        private void JoinMeeting()
        {
            ConsoleWrite_White("Enter sip who host the meeting");
            var sip = Console.ReadLine();
            ConsoleWrite_White("Enter meeting id");
            var meetingId = Console.ReadLine();
            var onlineMeetingUri = $"{sip};gruu;opaque=app:conf:focus:id:{meetingId}";
            ConsoleWrite_White("Enter Subject");
            var subject = Console.ReadLine();
            client.Application.Communication.JoinOnlineMeeting(onlineMeetingUri, subject, Importance.Normal).Wait();
        }

        private void LeaveMeeting()
        {
            var conversation = SelectConversation();
            if (conversation == null)
                return;

            conversation.Delete().Wait();
        }        

        private void DeleteAllMeeting()
        {
            if (client.OnlineMeetings.GetMyOnlineMeetings().Result == null)
                return;
            foreach (var onlineMeeting in client.OnlineMeetings.GetMyOnlineMeetings().Result.OnlineMeetings)
            {
                onlineMeeting.Delete().Wait();
            }
        }

        #endregion        

        private void Client_ContactPresenceUpdated(ContactPresence contactPresence, Contact contact)
        {
            ConsoleWrite_Green("{0} presence has been changed to {1}", contact.Name, contactPresence.Availability);
        }

        private void Client_ContactAdded(Contact contact)
        {
            ConsoleWrite_Green($"Contact {contact.Name} added to MyGroup");
        }

        private void Client_ContactAddedToGroup(Contact contact, Group group)
        {
            ConsoleWrite_Green($"Contact {contact.Name} added to {group.Name}");
        }

        private void Client_ContactDeleted(Contact contact)
        {
            ConsoleWrite_Green($"Contact {contact.Name} removed from MyGroup");
        }

        private void Client_ContactDeletedFromGroup(Contact contact, Group group)
        {
            ConsoleWrite_Green($"Contact {contact.Name} removed from {group.Name}");
        }

        private void Client_MessageReceived(Message message)
        {
            ConsoleWrite_Green("Messaged Received from {0}: {1}", message.GetParticipant().Result.Uri, message.Text);
            client.ReplyMessage(message.Text, message).Wait();
        }

        private void Client_MessagingInvitationReceived(MessagingInvitation messagingInvitation)
        {
            if (autoAcceptSet)
            {
                messagingInvitation.Accept().Wait();
                client.ReplyMessage("Thanks for having me!", messagingInvitation).Wait();
            }
            else
                messagingInvitation.Decline(CallDeclineReason.Local).Wait();
        }

        private void Client_OnlineMeetingInvitationReceived(OnlineMeetingInvitation onlineMeetingInvitation)
        {
            if (autoAcceptSet)
            {
                onlineMeetingInvitation.Accept().Wait();
                client.ReplyMessage("Thanks for having me!", onlineMeetingInvitation).Wait();
            }
            else
                onlineMeetingInvitation.Decline(CallDeclineReason.Local).Wait();
        }

        private void Client_GroupDeletedFromMyGroups(string uri)
        {
            ConsoleWrite_Green($"Group removed: {uri}");
        }

        private void Client_GroupAddedToMyGroups(Group group)
        {
            ConsoleWrite_Green($"Group added: {group.Name}");
        }    
           
        private Conversation SelectConversation()
        {
            if (client.Communication.GetConversations().Result == null || client.Communication.GetConversations().Result.GetConversations().Result == null)
            {
                ConsoleWrite_Red("no conversation");
                return null;
            }

            ConsoleWrite_White("select conversation");

            int i = 0;
            var conversations = client.Communication.GetConversations().Result.GetConversations().Result;
            foreach (var conv in conversations)
            {
                ConsoleWrite_White("[{0}]: {1}", i, conv.Subject);
                i++;
            }

            int result = int.Parse(Console.ReadLine());
            if (result > conversations.Count())
                return null;
            return conversations[result];
        }

        #region ConsoleWrite Helper

        private void ConsoleWrite_White(string value, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(value, args);
            Console.ResetColor();
        }

        private void ConsoleWrite_Green(string value, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(value, args);
            Console.ResetColor();
        }

        private void ConsoleWrite_Red(string value, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(value, args);
            Console.ResetColor();
        }

        #endregion
    }
}
