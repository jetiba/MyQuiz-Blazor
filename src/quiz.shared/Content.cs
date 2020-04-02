
using System.IO;
using System.Net.Http;

namespace quiz.shared
{
    public class Content
    {
        public HttpContent File1 { get; set; }
        public string FileType { get; set; }
    }
}