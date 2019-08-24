using System;
using System.Collections.Generic;
using System.Text;

namespace WebServerHttp
{
    public class StaticFileSystem : IFileSystem         //it returns a repository file
    {
        public string GetFile(string domainName)
        {
            switch (domainName)
            {
                case "wikipedia.com":return "wikipedia";
                case "wikileaks.com": return "wikileaks";
                case "cia.gov": return "cia";
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
