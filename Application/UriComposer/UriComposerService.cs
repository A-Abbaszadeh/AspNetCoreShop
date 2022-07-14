namespace Application.UriComposer
{
    public class UriComposerService : IUriComposerService
    {
        private readonly string _domain;
        public UriComposerService(string domain)
        {
            _domain = domain;
        }
        public string ComposeImageUri(string src)
        {
            return _domain + src.Replace("\\", "//");
        }
    }
}
