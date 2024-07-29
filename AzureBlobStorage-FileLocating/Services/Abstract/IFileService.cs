namespace AzureBlobStorage_FileLocating.Services.Abstract;

public interface IFileService
{
    string GetSignedUrl(string fileName);
    Task<string> GetSignedUrlAsync(string fileName);
    bool UploadFile(Stream stream, string fileName, string contentType);
    Task<bool> UploadFileAsync(Stream stream, string fileName, string contentType);
    bool DeleteFile(string fileName);
    Task<bool> DeleteFileAsync(string fileName);
}