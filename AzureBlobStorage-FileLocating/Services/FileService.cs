using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using AzureBlobStorage_FileLocating.Configuration;
using AzureBlobStorage_FileLocating.Services.Abstract;
using Microsoft.Extensions.Options;

namespace AzureBlobStorage_FileLocating.Services;

public class FileService : IFileService
{
    public readonly BlobConfig _config;

    public FileService(IOptions<BlobConfig> options)
    {
        _config = options.Value;
    }
        

    public async Task<bool> DeleteFileAsync(string fileName)
    {
        var serviceClient = new BlobServiceClient(_config.ConnectionString);
        var containerClient = serviceClient.GetBlobContainerClient(_config.ContainerName);
        var blobClient = containerClient.GetBlobClient(fileName);

        var response = await blobClient.DeleteIfExistsAsync();
        return response.Value;
    }

    public string GetSignedUrl(string fileName)
    {
        var serviceClient = new BlobServiceClient(_config.ConnectionString);
        var contaionerClient = serviceClient.GetBlobContainerClient(_config.ContainerName);
        var blobClient = contaionerClient.GetBlobClient(fileName);

        var signedUrl = blobClient.GenerateSasUri(BlobSasPermissions.Read, DateTime.Now.AddHours(2)).AbsoluteUri;

        return signedUrl;
    }

    public async Task<bool> UploadFileAsync(Stream stream, string fileName, string contentType)
    {
        var serviceClient = new BlobServiceClient(_config.ConnectionString);
        var contaionerClient = serviceClient.GetBlobContainerClient(_config.ContainerName);
        var blobClient = contaionerClient.GetBlobClient(fileName);

        await blobClient.UploadAsync(stream);
        return true;
    }
}
