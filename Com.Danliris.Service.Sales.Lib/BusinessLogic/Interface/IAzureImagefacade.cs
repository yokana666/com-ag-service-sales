using System;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Sales.Lib.BusinessLogic.Interface
{
    public interface IAzureImageFacade
    {
        Task<string> DownloadImage(string moduleName, string imagePath);
        Task<string> UploadImage(string moduleName, long id, DateTime _createdUtc, string imageBase64);
    }
}
