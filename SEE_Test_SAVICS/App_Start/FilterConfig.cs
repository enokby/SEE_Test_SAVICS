using System.Web;
using System.Web.Mvc;

namespace SEE_Test_SAVICS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
