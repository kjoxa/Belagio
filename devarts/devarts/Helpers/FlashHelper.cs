using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace devarts.Helpers
{
    public static class FlashHelpers
    {
        public static ActionResult WithInfo(this ActionResult actionResult, ControllerBase controller, string title = "", string message = "")
        {
            AddMessageToTempData(controller, AlertType.Info, title, message);
            return actionResult;
        }

        public static ActionResult WithSuccess(this ActionResult actionResult, ControllerBase controller, string title = "", string message = "")
        {
            AddMessageToTempData(controller, AlertType.Success, title, message);
            return actionResult;
        }

        public static ActionResult WithWarning(this ActionResult actionResult, ControllerBase controller, string title = "", string message = "")
        {
            AddMessageToTempData(controller, AlertType.Warning, title, message);
            return actionResult;
        }

        public static ActionResult WithError(this ActionResult actionResult, ControllerBase controller, string title = "", string message = "")
        {
            AddMessageToTempData(controller, AlertType.Error, title, message);
            return actionResult;
        }

        public static void FlashInfo(this ControllerBase controller, string title = "", string message = "")
        {
            AddMessageToTempData(controller, AlertType.Info, title, message);
        }

        public static void FlashSuccess(this ControllerBase controller, string title = "", string message = "")
        {
            AddMessageToTempData(controller, AlertType.Success, title, message);
        }

        public static void FlashWarning(this ControllerBase controller, string title = "", string message = "")
        {
            AddMessageToTempData(controller, AlertType.Warning, title, message);
        }

        public static void FlashError(this ControllerBase controller, string title = "", string message = "")
        {
            AddMessageToTempData(controller, AlertType.Error, title, message);
        }

        public static MvcHtmlString RenderAlert(this HtmlHelper helper)
        {
            object alertData = helper.ViewContext.TempData["alert"];

            if (alertData == null)
                return MvcHtmlString.Empty;

            var alert = new AlertMessage(alertData.ToString());

            if (String.IsNullOrEmpty(alert.Message) && String.IsNullOrEmpty(alert.Title))
                return MvcHtmlString.Empty;
            return
                new MvcHtmlString(String.Format(
                    "<div class='alert alert-{0} alert-dismissable'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;</button><strong>{1}</strong>{2}</div>",
                    alert.CssClass, HttpUtility.HtmlDecode(alert.Title), HttpUtility.HtmlDecode(alert.Message)));
        }

        private static void AddMessageToTempData(ControllerBase controller, AlertType type, string title, string message)
        {
            var alertMessage = new AlertMessage
            {
                Title = title + " ",
                Message = message,
                CssClass = type.CssClass
            };

            controller.TempData["alert"] = alertMessage.AsJson();
        }

        internal class AlertType
        {
            public string CssClass { get; private set; }

            public AlertType(string cssClass)
            {
                CssClass = cssClass;
            }

            public static AlertType Success = new AlertType("success");
            public static AlertType Info = new AlertType("info");
            public static AlertType Warning = new AlertType("warning");
            public static AlertType Error = new AlertType("danger");
        }

        internal class AlertMessage
        {
            public string Title { get; set; }
            public string Message { get; set; }
            public string CssClass { get; set; }

            public AlertMessage() { }
            public AlertMessage(string json)
            {
                var alertMessage = JsonConvert.DeserializeObject<AlertMessage>(json);

                Title = alertMessage.Title;
                Message = alertMessage.Message;
                CssClass = alertMessage.CssClass;
            }

            public string AsJson()
            {
                return JsonConvert.SerializeObject(this);
            }
        }
    }
}