﻿using System.Web;
using System.Web.Mvc;

namespace SE2_IndividueleOpdracht
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}