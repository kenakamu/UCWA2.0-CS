using Microsoft.Skype.UCWA;
using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using UCWAUWP.Models;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace UCWAUWP.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        #region Properties

        UCWAClient client = new UCWAClient();

        #region Me Profile

        private ImageSource imageSource;
        public ImageSource ImageSource
        {
            get { return imageSource; }
            set { imageSource = value; NotifyPropertyChanged(); }
        }

        private Me me;
        public Me Me
        {
            get { return me; }
            set { me = value; NotifyPropertyChanged(); }
        }

        private string memo;
        public string Memo
        {
            get { return memo; }
            set { memo = value; NotifyPropertyChanged(); }
        }

        private string status;
        public string Status
        {
            get { return status; }
            set { if (status == value) return; status = value; NotifyPropertyChanged(); }
        }


        #endregion

        #region Contact Profile

        private Contact selectedContact;
        public Contact SelectedContact
        {
            get { return selectedContact; }
            set { selectedContact = value; NotifyPropertyChanged(); }
        }

        private ImageSource contactImageSource;
        public ImageSource ContactImageSource
        {
            get { return contactImageSource; }
            set { contactImageSource = value; NotifyPropertyChanged(); }
        }
        
        private string contactAddress;
        public string ContactAddress
        {
            get { return contactAddress; }
            set { contactAddress = value; NotifyPropertyChanged(); }
        }

        private string contactMemo;
        public string ContactMemo
        {
            get { return contactMemo; }
            set { contactMemo = value; NotifyPropertyChanged(); }
        }

        private string contactStatus;
        public string ContactStatus
        {
            get { return contactStatus; }
            set { contactStatus = value; NotifyPropertyChanged(); }
        }

        #endregion

        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; NotifyPropertyChanged(); }
        }
        
        private ObservableCollection<MessageDetail> messages = new ObservableCollection<MessageDetail>();
        public ObservableCollection<MessageDetail> Messages
        {
            get { return messages; }
            set { messages = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<LocalContact> myContacts = new ObservableCollection<LocalContact>();
        public ObservableCollection<LocalContact> MyContacts
        {
            get { return myContacts; }
            set { myContacts = value; NotifyPropertyChanged(); }
        }

        private ObservableCollection<Group2> myGroups = new ObservableCollection<Group2>();
        public ObservableCollection<Group2> MyGroups
        {
            get { return myGroups; }
            set { myGroups = value; NotifyPropertyChanged(); }
        }

        public List<string> Availabilities = new List<string>() { "Online", "Away", "Offline", "Busy" };

        private Dictionary<Contact, ObservableCollection<MessageDetail>> contactMessages = new Dictionary<Contact, ObservableCollection<MessageDetail>>();
        private Dictionary<Contact, Conversation> contactConversations = new Dictionary<Contact, Conversation>();

        private Location location;
        private Note note;
        private Presence presence;

        #endregion

        public MainPageViewModel()
        {
            client.SendingRequest += Client_SendingRequest;
            client.MessageReceived += Client_MessageReceived;
            client.MessageSent += Client_MessageSent;
            client.MessagingInvitationCompleted += Client_MessagingInvitationCompleted;
            client.ConversationDeleted += Client_ConversationDeleted;
            client.ContactPresenceUpdated += Client_ContactPresenceUpdated;
            presence = new Presence();
            Status = presence.Availability.ToString();            
        }

        public async Task SignIn()
        {
            var result = await client.Initialize(TokenService.tenant);
            await client.SignIn(Availability.Away, true, true, true, true, "+818033632757", true);

            // Load profile
            BitmapImage image = new BitmapImage();
            var imageData = await client.Me.GetPhoto();
            if (imageData != null)
            {
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    await image.SetSourceAsync(ms.AsRandomAccessStream());
                }
                ImageSource = image;
            }
            Me = client.Me;
            location = await client.Me.GetLocation();
            note = (await client.Me.GetNote());
            Memo = note.Message;
            presence = await client.Me.GetPresence();
            Status = presence.Availability.ToString();

            foreach(var group in (await client.People.GetMyGroups()).Groups)
            {
                MyGroups.Add(group);
            }
        }
     
        public async Task UpdateMemo()
        {
            note.Message = Memo;
            await note.Update();
            note = await client.Me.GetNote();
            Memo = note.Message;
        }

        public async Task UpdateStatus()
        {
            switch(Status)
            {
                case "Online":
                    presence.Availability = Availability.Online;
                    break;
                case "Away":
                    presence.Availability = Availability.Away;
                    break;
                default:
                    presence.Availability = Availability.Busy;
                    break;
            }
            
            await presence.Update();
            presence = await client.Me.GetPresence();
            Status = presence.Availability.ToString();
        }

        public async Task GroupClicked(object sender, ItemClickEventArgs e)
        {
            Group2 group = e.ClickedItem as Group2;
            MyContacts.Clear();
            foreach (var contact in (await group.GetGroupContacts()).Contacts)
            {
                await client.SubscribeContactsChange(contact.Uri);

                LocalContact localContact = new LocalContact();
                localContact.Contact = contact;
                BitmapImage image = new BitmapImage();
                var imageData = await contact.GetContactPhoto();
                if (imageData != null)
                {
                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        await image.SetSourceAsync(ms.AsRandomAccessStream());
                    }
                    localContact.ImageSource = image;
                }
                MyContacts.Add(localContact);
                if (contactMessages.Keys.Where(x => x.Uri == contact.Uri).FirstOrDefault() == null)
                {
                    contactMessages.Add(contact, new ObservableCollection<MessageDetail>());
                    contactConversations.Add(contact, null);
                }
            }
        }

        public async Task ContactClicked(object sender, ItemClickEventArgs e)
        {
            SelectedContact = (e.ClickedItem as LocalContact).Contact;
           
            ContactImageSource = (e.ClickedItem as LocalContact).ImageSource;

            ContactAddress = (await selectedContact.GetContactLocation()).Location;
            ContactMemo = (await selectedContact.GetContactNote()).Message;
            ContactStatus = (await selectedContact.GetContactPresence()).Availability.ToString();
            Messages = contactMessages[selectedContact]; 
        }

        public async Task SendMessage()
        {
            if (string.IsNullOrEmpty(Message))
                return;

            Messages = contactMessages[SelectedContact];
            Messages.Add(new MessageDetail() { Direction = "Outgoing", Text = Message });

            Conversation conversation = contactConversations[SelectedContact];
            Messaging messaging = conversation == null ? null : await conversation.GetMessaging();

            if (messaging != null)
            {
                try
                {
                    await messaging.SendMessage(Message);
                    Message = "";
                    return;
                }
                catch (ResourceNotFoundException ex)
                {
                    // If message does not exists, then start new.
                }
                catch (Exception ex)
                {
                    // Log error
                    return;
                }
            }
            
            try
            {
                await client.StartMessaging(selectedContact.Uri, "Send From UWP", Importance.Normal, Message);
                
                Message = "";
                return;
            }
            catch(Exception ex)
            {
            }           
        }

        public async Task SendMessageKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
                await SendMessage();
        }

        private async void Client_SendingRequest(System.Net.Http.HttpClient client, string resource)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await TokenService.AquireAADToken(resource));
        }

        private async void Client_MessageReceived(Message message)
        {
            var from = (await message.GetParticipant()).Uri;
            Messages = contactMessages.Where(x => x.Key.Uri == from).First().Value;
            Messages.Add(new MessageDetail() { Direction = "Incoming", Text = message.Text });
        }
        
        private async void Client_MessageSent(Message message)
        {
            Conversation conversation = contactConversations[SelectedContact];
            if (conversation == null)
            {
                conversation = await (await message.GetMessaging()).GetConversation();
                contactConversations[selectedContact] = conversation;
            }
        }

        private void Client_ConversationDeleted(string uri)
        {
            var contactConversation = contactConversations.Where(x => x.Value.Self == uri).FirstOrDefault();
            if (contactConversation.Key != null)
            {
                Contact contact = contactConversation.Key;
                contactMessages[contact] = new ObservableCollection<MessageDetail>();
            }
        }

        private void Client_MessagingInvitationCompleted(MessagingInvitation messagingInvitation)
        {            
        }
    
        private void Client_ContactPresenceUpdated(ContactPresence contactPresence, Contact contact)
        {
            if (SelectedContact?.Uri == contact.Uri)
                ContactStatus = contactPresence.Availability.ToString();
        }

    }
}
