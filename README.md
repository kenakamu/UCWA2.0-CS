### C# library for UCWA 2.0

This is a sample library for C# developer to consume UCWA 2.0 (Unified Communication Web API) of Office 365 Skype Service.
For more detail about the Web API, please refer to Skype Developer Site (https://ucwa.skype.com)

The library is PCL so you can include it in any platform such as UWP, Console, Xamarin, etc. All methods are marked as async and all events are exposed as event so that you can subscribe to handle them. See test client for more detail.

## HOW TO TRY
This solution contains two projects. UCWASDK is main project which is C# wrapper. TestClient is console application using it. 
Once download the solution, do the following.

1. Register your application in your Azure AD tenant.
  1. Login to https://manage.windowsazure.com.
  2. Select "Active Directory"
  3. Select your directory, and go to "APPLICATIONS" tab.
  4. Click "ADD" button on the bottom.
  5. Enter any name, and select "NATIVE CLIENT APPLICATION".
  6. Enter any REDIRECT URI. It is not used in this sample but you can specify any valid URI.
  7. Once application created, select CONFIGURE tab.
  8. Copy "CLIENT ID" for step 2.
  9. Scroll down the page and click "Add application".
  10. Select "Skype for Business Online" and click "OK" (check mark)
  11. Select Delegated Permissions as you need. 
  12. Click "SAVE" button to save it.
2. Open TokenService.cs file under TestClient or UCWAUWP project and update the values.
3. Compile all solution.
4. Run TestClient project and try it.

## AUTHENTICATION
The wrapper contains all UCWA related class, enums and methods. However authentication is out of scope as each platform may use slightly different method. To authenticate user, use Active Directory Authentication Library (ADAL). 

## BASICS
1\. Add using to your code.
```csharp
using Microsoft.Skype.UCWA;
using Microsoft.Skype.UCWA.Enums;
using Microsoft.Skype.UCWA.Models;
```
2\. Instantiate UCWAClient.
```csharp
UCWAClient client = new UCWAClient();
```
3\. Subscribe SendingRequest to authenticate. You can also subscribe any other events as you need.
```csharp
// Write some code to obtain accesstoken. You can use ADAL.
client.SendingRequest += (client, resource) => { client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accesstoken); };
// For example, handle message received event.
client.MessageReceived += Client_MessageReceived;
```
4\. Instantiate and SignIn.
```csharp
await client.Initialize("<your tenant id>");
await client.SignIn(availability:Availability.Online, supportMessage:true, supportAudio:false, supportPlainText:true, supportHtmlFormat:false, phoneNumber:"", keepAlive:true);
```
5\. Do anything you need.

##### SPECIAL THANKS TO
Tam Huynh to help me start the project. https://github.com/tamhinsf/ucwa-sfbo-console<br/>
David Newman to help me drive the project. https://www.youtube.com/watch?time_continue=4&v=Epqrsexnn5g<br/>
Adam Hodge and Maxwell Roseman for encourage me.
