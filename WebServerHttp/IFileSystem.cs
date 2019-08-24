using System;
using System.Collections.Generic;
using System.Text;

namespace WebServerHttp
{
    public interface IFileSystem
    {
        string GetFile(string domainName);
    }
}
