using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Stationery.Common.Helpers
{
    /// <summary>
    /// JsonContent
    /// </summary>
    /// <seealso cref="System.Net.Http.StringContent" />
    public class JsonContent : StringContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonContent"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public JsonContent(object value)
            : base(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonContent"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="mediaType">Type of the media.</param>
        public JsonContent(object value, string mediaType)
            : base(JsonConvert.SerializeObject(value), Encoding.UTF8, mediaType)
        {
        }
    }

    /// <summary>
    /// PatchContent
    /// </summary>
    /// <seealso cref="System.Net.Http.StringContent" />
    public class PatchContent : StringContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PatchContent"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public PatchContent(object value)
            : base(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json-patch+json")
        {
        }
    }

    /// <summary>
    /// FileContent
    /// </summary>
    /// <seealso cref="System.Net.Http.MultipartFormDataContent" />
    public class FileContent : MultipartFormDataContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileContent"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="apiParamName">Name of the API parameter.</param>
        public FileContent(string filePath, string apiParamName)
        {
            var filestream = File.Open(filePath, FileMode.Open);
            var filename = Path.GetFileName(filePath);

            Add(new StreamContent(filestream), apiParamName, filename);
        }
    }
}
