using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using quiz.server.Data;
using Azure.Storage;
using Azure.Storage.Blobs;
using quiz.shared;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Azure.Storage.Blobs.Models;

namespace quiz.server.Controllers
{
    [Route("api/[controller]")]
    public class BlobStorageController : Controller
    {
        IConfiguration _configuration;

        public BlobStorageController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetContentFromBlob(string filename)
        {
            BlobContainerClient cc = new BlobContainerClient(_configuration["StorageConnString"], "content");
            
            var blobs = cc.GetBlobs(BlobTraits.All, BlobStates.All).ToList();
            var check = blobs.Find(x => x.Name.Contains(filename)) != null;
            if(check){
                var blobInfo = await cc.GetBlobClient(filename).DownloadAsync();
                return new FileStreamResult(blobInfo.Value.Content, blobInfo.Value.ContentType);
            }
            else
            {
                return new NotFoundResult();
            }
           
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutContentInBlob(string filename)
        {
            BlobContainerClient cc = new BlobContainerClient(_configuration["StorageConnString"], "content");

            await cc.UploadBlobAsync(filename, Request.Body);

            return new OkResult();
        }

        [HttpDelete("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBlobs()
        {
            BlobContainerClient cc = new BlobContainerClient(_configuration["StorageConnString"], "content");

            var blobs = cc.GetBlobs(BlobTraits.All, BlobStates.All).ToList();
            foreach (var b in blobs)
            {
                await cc.DeleteBlobIfExistsAsync(b.Name);
            }

            return new OkResult();
        }

    }
}