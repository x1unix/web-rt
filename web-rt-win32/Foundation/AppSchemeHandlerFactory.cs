using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WebRT.Foundation
{
    class AppSchemeHandlerFactory: ISchemeHandlerFactory
    {
        public static string SchemeName = "app";
        public string Root;

        public AppSchemeHandlerFactory(string root)
        {
            Root = root;
        }

        public IResourceHandler Create(IBrowser browser, IFrame frame, string schemeName, IRequest request)
        {
            Uri uri = new Uri(request.Url);
            string location;
            string extension;

            if (uri.AbsolutePath == "/")
            {
                location = uri.Host;
                extension = Path.GetExtension(uri.Host);
            } else
            {
                string[] dirs = { uri.Host, uri.AbsolutePath };
                extension = Path.GetExtension(uri.AbsolutePath.Split('/').Last());
                location = $"{uri.Host}/{uri.AbsolutePath}";
            }

            string mime = ResourceHandler.GetMimeType(extension);
            string path = FSHelper.NormalizeLocation($"{Root}/{location}"); 
            
            return ResourceHandler.FromFilePath(path, mime);
        }
    }
}
