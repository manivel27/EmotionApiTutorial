using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EmotionApiTutorial
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();            
        }

        private async void TakePhoto_Clicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                return;
            }

            StoreCameraMediaOptions options = new StoreCameraMediaOptions();

            options.DefaultCamera = CameraDevice.Front;

            var file = await CrossMedia.Current.TakePhotoAsync(options);
            
            if (file == null)
                return;

            var stream = file.GetStream();
            var result = await MakeRequest(stream);
            List<ServiceResult> serviceResult = JsonConvert.DeserializeObject<List<ServiceResult>>(result);
            
        }
        
        static async Task<string> MakeRequest(Stream stream)
        {
            var result = string.Empty;

            using (var httpClient = new HttpClient())
            {
                // Setup HttpClient
                httpClient.BaseAddress = new Uri("https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize");
                httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "{API_KEY_WILL_BE_HERE}");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/octet-stream"));

                // Setup data object
                HttpContent content = new StreamContent(stream);
                content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/octet-stream");

                // Make POST request
                var response = await httpClient.PostAsync("https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize", content);

                // Read response
                result = await response.Content.ReadAsStringAsync();
            }

            return result;
        }
        
        public static byte[] ReadStreamFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
