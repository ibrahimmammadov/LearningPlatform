using Microsoft.Extensions.Options;
using Platform.Web.Models;

namespace Platform.Web.Helpers
{
    public class PhotoHelper
    {
        public ServiceApiSettings  _serviceApiSettings { get; set; }

        public PhotoHelper(IOptions<ServiceApiSettings> serviceApiSettings)
        {
            _serviceApiSettings = serviceApiSettings.Value;
        }

        public string GetPhotoUrl(string photoUrl)
        {
            return $"{_serviceApiSettings.PhotoStockUri}/photos/{photoUrl}";
        }
    }
}
