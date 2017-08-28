using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;

using TodoItemPush.MobileAppService.DataObjects;
using TodoItemPush.MobileAppService.Models;

using System.Collections.Generic;
using Microsoft.Azure.NotificationHubs;
using Microsoft.Azure.Mobile.Server.Config;

namespace TodoItemPush.MobileAppService.Controllers
{
    // TODO: Uncomment [Authorize] attribute to require user be authenticated to access Item(s).
    // [Authorize]
    public class ItemController : TableController<Item>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MasterDetailContext context = new MasterDetailContext();
            DomainManager = new EntityDomainManager<Item>(context, Request);
        }

        // GET tables/Item
        public IQueryable<Item> GetAllItems()
        {
            return Query();
        }

        // GET tables/Item/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Item> GetItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Item/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Item> PatchItem(string id, Delta<Item> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/Item
        public async Task<IHttpActionResult> PostItem(Item item)
        {
            Item current = await InsertAsync(item);
            // Get the settings for the server project.
            HttpConfiguration config = this.Configuration;
            MobileAppSettingsDictionary settings =
                this.Configuration.GetMobileAppSettingsProvider().GetMobileAppSettings();

            // Get the Notification Hubs credentials for the Mobile App.
            string notificationHubName = settings.NotificationHubName;
            string notificationHubConnection = settings
                .Connections[MobileAppSettingsKeys.NotificationHubConnectionString].ConnectionString;

            // Create a new Notification Hub client.
            NotificationHubClient hub = NotificationHubClient
            .CreateClientFromConnectionString(notificationHubConnection, notificationHubName);

            // Sending the message so that all template registrations that contain "messageParam"
            // will receive the notifications. This includes APNS, GCM, WNS, and MPNS template registrations.
            Dictionary<string, string> templateParams = new Dictionary<string, string>();
            templateParams["messageParam"] = item.Text + " was added to the list.";

            try
            {
                // Send the push notification and log the results.
                var result = await hub.SendTemplateNotificationAsync(templateParams);

                // Write the success result to the logs.
                config.Services.GetTraceWriter().Info(result.State.ToString());
            }
            catch (System.Exception ex)
            {
                // Write the failure result to the logs.
                config.Services.GetTraceWriter()
                    .Error(ex.Message, null, "Push.SendAsync Error");
            }

            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Item/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteItem(string id)
        {
            return DeleteAsync(id);
        }
    }
}