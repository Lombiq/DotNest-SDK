using Orchard;
using Orchard.FileSystems.VirtualPath;
using Orchard.Mvc.Filters;
using Orchard.UI.Admin;
using Orchard.UI.Resources;
using System;
using System.Web.Mvc;

namespace DotNest.Sdk.Filters
{
    /// <summary>
    /// Filter for automatically including common static resources from a theme to be deployed as a Media Theme.
    /// Better than doing this in the Layout template since that can be overridden by users, breaking the functionality.
    /// </summary>
    public class AutomaticStaticResourceIncludingFilter : FilterProvider, IResultFilter
    {
        private readonly Lazy<IResourceManager> _resourceManager;
        private readonly Lazy<IWorkContextAccessor> _workContextAccessor;
        private readonly Lazy<IVirtualPathProvider> _virtualPathProvider;


        public AutomaticStaticResourceIncludingFilter(
            Lazy<IResourceManager> resourceManager,
            Lazy<IWorkContextAccessor> workContextAccessor,
            Lazy<IVirtualPathProvider> virtualPathProvider)
        {
            _resourceManager = resourceManager;
            _workContextAccessor = workContextAccessor;
            _virtualPathProvider = virtualPathProvider;
        }


        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            // Should only run on a full view rendering result and on the frontend only.
            if (!(filterContext.Result is ViewResult) || AdminFilter.IsApplied(filterContext.RequestContext))
                return;


            var currentTheme = _workContextAccessor.Value.GetContext().CurrentTheme;
            var currentThemePath = _virtualPathProvider.Value.Combine(currentTheme.Location, currentTheme.Name);

            // Favicon.
            var path = _virtualPathProvider.Value.Combine(currentThemePath, "Images", "favicon.ico");
            if (_virtualPathProvider.Value.FileExists(path))
                _resourceManager.Value.RegisterLink(new LinkEntry { Type = "image/x-icon", Rel = "shortcut icon", Href = path });

            // Stylesheet.
            path = _virtualPathProvider.Value.Combine(currentThemePath, "Styles", "site.css");
            if (_virtualPathProvider.Value.FileExists(path))
                _resourceManager.Value.Include("stylesheet", path, path);

            // Head script.
            path = _virtualPathProvider.Value.Combine(currentThemePath, "Scripts", "site-head.js");
            if (_virtualPathProvider.Value.FileExists(path))
                _resourceManager.Value.Include("script", path, path).AtHead();

            // Foot script.
            path = _virtualPathProvider.Value.Combine(currentThemePath, "Scripts", "site-foot.js");
            if (_virtualPathProvider.Value.FileExists(path))
                _resourceManager.Value.Include("script", path, path).AtFoot();
        }

        public void OnResultExecuted(ResultExecutedContext filterContext) { }
    }
}