using System;
using System.Collections.Generic;
using System.Text;

namespace WebServerHttp
{
    public class WebApp
    {
        public string _domainName;
        IFileSystem fileSystem = new StaticFileSystem();

        public string GetRepository(string domainName)
        {
            _domainName = domainName;
            return fileSystem.GetFile(_domainName);
        }
        public string GetDomainName()
        {
            return _domainName;
        }
    }
}
