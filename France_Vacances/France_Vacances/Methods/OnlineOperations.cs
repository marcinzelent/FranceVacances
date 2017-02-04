using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using CoreFtp;
using CoreFtp.Infrastructure;

namespace France_Vacances.Methods
{
    public static class OnlineOperations
    {
        public static string DownloadString(string fileUri)
        {
            var uri = new Uri(fileUri);
            var httpClient = new HttpClient();

            // Always catch network exceptions for async methods
            try
            {
                var response = httpClient.GetAsync(uri).Result;

                if (response.IsSuccessStatusCode)
                {
                    // by calling .Result you are performing a synchronous call
                    var responseContent = response.Content;

                    // by calling .Result you are synchronously reading the result
                    string responseString = responseContent.ReadAsStringAsync().Result;
                    return responseString;
                }
            }
            catch (Exception ex)
            {
                MessageDialog messageDialog = new MessageDialog(ex.Message);
                messageDialog.ShowAsync();
            }


            // Once your app is done using the HttpClient object call dispose to 
            // free up system resources (the underlying socket and memory used for the object)

            httpClient.Dispose();
            return null;
        }

        public static async void DownloadFile(string fileUri, string fileLocation, string fileName)
        {

            HttpClient httpClient = new HttpClient();
            // Always catch network exceptions for async methods
            try
            {
                Uri uri = new Uri(fileUri);
                StorageFolder localFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync(fileLocation,CreationCollisionOption.OpenIfExists);
                StorageFile file = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                byte[] buffer = await httpClient.GetByteArrayAsync(uri); // Download file
                using (Stream stream = await file.OpenStreamForWriteAsync()) stream.Write(buffer, 0, buffer.Length); // Save
            }

            catch (Exception ex)
            {
                MessageDialog messageDialog = new MessageDialog(ex.Message);
                messageDialog.ShowAsync();
            }


            // Once your app is done using the HttpClient object call dispose to 
            // free up system resources (the underlying socket and memory used for the object)

            httpClient.Dispose();
        }

        public static async void UploadToFtp(string fileName, string remoteLocation)
        {
            //Configuration for FTP client
            FtpClientConfiguration ftpClientConfiguration = new FtpClientConfiguration
            {
                Host = "ftp.retroth.ml",
                Port = 21,
                Username = "u157773980.3duser",
                Password = "ftppass67",
                BaseDirectory = remoteLocation

            };
            FtpClient ftpClient = new FtpClient(ftpClientConfiguration);
            try
            {
                StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                byte[] buffer = File.ReadAllBytes(file.Path);
                await ftpClient.LoginAsync();
                using (Stream stream = await ftpClient.OpenFileWriteStreamAsync(fileName)) stream.Write(buffer, 0, buffer.Length);
                await ftpClient.LogOutAsync();
            }
            catch (Exception ex)
            {
                MessageDialog messageDialog = new MessageDialog(ex.Message);
                await messageDialog.ShowAsync();
            }

            ftpClient.Dispose();
        }

        public static async Task<ReadOnlyCollection<FtpNodeInformation>> GetListOfFiles(string remoteLocation)
        {
            //Configuration for FTP client
            FtpClientConfiguration ftpClientConfiguration = new FtpClientConfiguration
            {
                Host = "ftp.retroth.ml",
                Port = 21,
                Username = "u157773980.3duser",
                Password = "ftppass67",
                BaseDirectory = remoteLocation

            };
            FtpClient ftpClient = new FtpClient(ftpClientConfiguration);
            try
            {
                await ftpClient.LoginAsync();
                ReadOnlyCollection<FtpNodeInformation> filesCollection = await ftpClient.ListFilesAsync();
                await ftpClient.LogOutAsync();
                return filesCollection;
            }
            catch (Exception ex)
            {
                MessageDialog messageDialog = new MessageDialog(ex.Message);
                await messageDialog.ShowAsync();
            }

            ftpClient.Dispose();
            return null;
        }
    }
}
