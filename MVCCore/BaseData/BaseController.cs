using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace MVCCore.BaseData
{
    public class BaseController : Controller
    {
        public BaseController()
        {

        }

        public bool isArLang = CultureInfo.CurrentCulture.Name.StartsWith("ar");
    }
}
