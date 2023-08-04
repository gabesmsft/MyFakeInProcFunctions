using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace FunctionApp1
{
    public class BlobTrigger1
    {
        [FunctionName("BlobTrigger1")]
        public static void Run([BlobTrigger("test-samples-trigger/{name}", Connection = "blobconn")] string myTriggerItem,
        [Blob("test-samples-output/{name}-output.txt", FileAccess.Write, Connection = "blobconn")] TextWriter myBlobOut,
        [Blob("test-samples-input/sample1.txt", FileAccess.Read, Connection = "blobconn")] string myBlob,
        ILogger logger
        )
        {
            logger.LogInformation($"Triggered Item = {myTriggerItem}");
            logger.LogInformation($"Input Item = {myBlob}");

            myBlobOut.WriteLine(myBlob);
            myBlobOut.Flush();
            myBlobOut.Close();
            myBlobOut = null;
        }
    }
}
