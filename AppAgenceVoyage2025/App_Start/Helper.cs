using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppAgenceVoyage2025.App_Start
{
    public class Helper
    {

    }

    public enum Flashlevel
    {
        Info = 1,
        Success = 2,
        Warning = 3,
        Danger = 4
        
    }
    public static class FlashHelper
    {
        public static void Flash(this Controller controller, string message, Flashlevel level)
        {
            IList<string> messages = null;
            string key = String.Format("flash-{0}", level.ToString().ToLower());
            messages = controller.TempData.ContainsKey(key) ? (IList<string>)controller.TempData[key] : new List<string>();
            messages.Add(message);
            controller.TempData[key] = messages;
        }
            
    }
}