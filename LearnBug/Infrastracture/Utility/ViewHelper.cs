using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

public static class ViewHelper
{
    public static string RenderPartialToString(this ControllerBase controller)
    {
        return RenderViewToString(controller);
    }
    public static string RenderPartialToString(this ControllerBase controller, string partialViewName)
    {
        IView view = ViewEngines.Engines.FindPartialView(controller.ControllerContext, partialViewName).View;
        return RenderViewToString(controller, view);
    }
    public static string RenderPartialToString(this ControllerBase controller, string partialViewName, object model)
    {
        IView view = ViewEngines.Engines.FindPartialView(controller.ControllerContext, partialViewName).View;
        return RenderViewToString(controller, view, model);
    }

    public static string RenderViewToString(this ControllerBase controller)
    {
        string viewName = controller.ControllerContext.RouteData.GetRequiredString("action");
        IView view = ViewEngines.Engines.FindView(controller.ControllerContext, viewName, null).View;
        return RenderViewToString(controller, view);
    }
    public static string RenderViewToString(this ControllerBase controller, string viewName)
    {
        IView view = ViewEngines.Engines.FindView(controller.ControllerContext, viewName, null).View;
        return RenderViewToString(controller, view);
    }
    public static string RenderViewToString(this ControllerBase controller, string viewName, object model)
    {
        IView view = ViewEngines.Engines.FindView(controller.ControllerContext, viewName, null).View;
        return RenderViewToString(controller, view, model);
    }

    public static string RenderViewToString(this ControllerBase controller, IView view)
    {
        using (var writer = new StringWriter())
        {
            var viewContext = new ViewContext(controller.ControllerContext, view, controller.ViewData, controller.TempData, writer);
            view.Render(viewContext, writer);
            return writer.GetStringBuilder().ToString();
        }
    }
    public static string RenderViewToString(this ControllerBase controller, IView view, object model)
    {
        using (var writer = new StringWriter())
        {
            controller.ViewData.Model = model;
            var viewContext = new ViewContext(controller.ControllerContext, view, controller.ViewData, controller.TempData, writer);
            view.Render(viewContext, writer);
            return writer.GetStringBuilder().ToString();
        }
    }
}