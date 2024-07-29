namespace AzureBlobStorage_FileLocating.Services.Abstract;

public interface IFileService
{
    string GetSignedUrl(string fileName);
    Task<bool> UploadFileAsync(Stream stream, string fileName, string contentType);
    Task<bool> DeleteFileAsync(string fileName);
}