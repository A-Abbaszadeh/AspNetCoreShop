using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Catalogs.CatalogItems.UriComposer
{
    public interface IUriComposerService
    {
        string ComposeImageUri(string src);
    }

    public class UriComposerService : IUriComposerService
    {
        private readonly string _domain;
        public UriComposerService(string domain)
        {
            _domain = domain;
        }
        public string ComposeImageUri(string src)
        {
            return _domain + src;
        }
    }
}
