using PUBGApiWrapper.Models.PUBGApiNet.Models;
using System;
using System.Linq;

namespace PUBGApiWrapper
{
    class Demo
    {
        static void Main(string[] args)
        {
            PUBGApiClient client = new PUBGApiClient(Shards.PcAsia, Endpoint.Matches);
            var taskObj = client.CallEndpointAsync();
            
            if (taskObj.Result.GetType() == typeof(ErrorsRootObject))
            {
                ErrorsRootObject result = null;
                result = (ErrorsRootObject)taskObj.Result;
                Console.WriteLine(result.Errors.FirstOrDefault().Title);
            }
            else if (taskObj.Result.GetType() == typeof(ResponseRootObject))
            {
                //do something
            }            
            
            Console.ReadKey();
        }
    }
}
