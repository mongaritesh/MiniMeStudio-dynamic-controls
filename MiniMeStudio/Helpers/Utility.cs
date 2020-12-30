using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using MiniMeStudio.Models;
using Newtonsoft.Json;
using RestSharp;

namespace MiniMeStudio.Services
{
    public static class EnumUtils
    {
        public static List<string> GetSortedEnumNames(this Type t)
        {
            if (t.IsEnum)
            {
                string[] strOut = Enum.GetNames(t);
                Array.Sort(strOut);
                List<string> sorted_enum_names = new List<string>(strOut);
                return sorted_enum_names;

            }
            throw new ArgumentException("Must be an enum", "t");
        }
    }

    public static class Utility
    {
        public static string ExecuteSP(string SP_Name, string Parameters)
        {
            try
            {
                var client = new RestClient(Globals.MiniMeAPI + SP_Name);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", Parameters, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                var result = JsonConvert.DeserializeObject(response.Content);
                return result != null ? result.ToString() : String.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<string> ExecuteSPAsync(string SP_Name, string Parameters)
        {
            try
            {
                var client = new RestClient(Globals.MiniMeAPI + SP_Name);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json", Parameters, ParameterType.RequestBody);
                IRestResponse response = await client.ExecuteAsync(request);
                Console.WriteLine();
                var result = JsonConvert.DeserializeObject(response.Content);
                return result.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async static Task<string> GetBlobSharedAccessTokenAsync()
        {
            // some constants
            var storageAccountName = "minimedevstorage";
            var accessKey = "RRs/+7UyNtRdQY9yoZcfD6ccqK4CC4r48C7dz/S1gJwjTF3pjJGlYsslyD0uWE8JQMmvn8sm+1o2vZZQcdfGcA==";// your storage account access key here
            var policyName = "defaultaccountpolicy";
            var containerName = "account-1234";

            // connect to our storage account and create a blob client
            var connectionString = String.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}",
                storageAccountName,
                accessKey);
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();

            // get a reference to the container
            var blobContainer = blobClient.GetContainerReference(containerName);
            await blobContainer.CreateIfNotExistsAsync();

            // create the stored policy we will use, with the relevant permissions and expiry time
            var storedPolicy = new SharedAccessBlobPolicy()
            {
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(10),
                Permissions = SharedAccessBlobPermissions.Read |
                              SharedAccessBlobPermissions.Write |
                              SharedAccessBlobPermissions.List
            };

            // get the existing permissions (alternatively create new BlobContainerPermissions())
            var permissions = await blobContainer.GetPermissionsAsync();

            // optionally clear out any existing policies on this container
            permissions.SharedAccessPolicies.Clear();
            // add in the new one
            permissions.SharedAccessPolicies.Add(policyName, storedPolicy);
            // save back to the container
            await blobContainer.SetPermissionsAsync(permissions);

            // Now we are ready to create a shared access signature based on the stored access policy
            var containerSignature = blobContainer.GetSharedAccessSignature(null, policyName);
            // create the URI a client can use to get access to just this container
            var uri = blobContainer.Uri + containerSignature;
            return uri;
        }
    }

}
