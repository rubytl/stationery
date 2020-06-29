using Microsoft.AspNetCore.Http;

namespace Stationery.UI.ViewModels
{
    public class FileInputModel
    {
        public IFormFile FileToUpload { get; set; }
    }
}
