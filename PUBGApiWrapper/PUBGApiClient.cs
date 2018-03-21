using Newtonsoft.Json;
using PUBGApiWrapper.Models.PUBGApiNet.Models;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PUBGApiWrapper
{
    public class PUBGApiClient
    {
        private const int MAXLIMIT = 5;
        private const string API_URL = "https://api.playbattlegrounds.com/";
        private const string API_URL_TEST = "";

        private bool _testmode;

        private int _limit;
        //private int _offset;
        private string _apiUrl;
        private string _apiKey;
        private string _shard;
        private string _endpoint;
        private string _sortingAttribute;
        private bool _sortingOrder;
        private bool _isSuccessStatusCode;
        private int _status;
        private string _statusText;
        private object _rootObject;
        private string _nextUrl;
        private string _previousUrl;
        private string _firstUrl;

        public PUBGApiClient(Shards shard, Endpoint endpoint, string apiKey = null, bool testmode = false)
        {
            _shard = Helpers.GetShard(shard) ?? throw new ArgumentNullException(nameof(shard));
            _endpoint = Helpers.GetEndpoint(endpoint) ?? throw new ArgumentNullException(nameof(endpoint));
            _apiUrl = API_URL;
            _apiKey = apiKey;
            _limit = MAXLIMIT;
            _firstUrl = "";
            Testmode = testmode;
            SortingOrder = "ASC";

            var url = _shard;
            url += _endpoint;
        }    

        public async Task<IRootObject> CallEndpointAsync()
        {
            var url = _shard;
            url += _endpoint;

            return await Query(url);                                   
        }

        private async Task<IRootObject> Query(string url)
        {
            if (!string.IsNullOrEmpty(_sortingAttribute) && _endpoint != Endpoint.Status.ToString())
            {
                if (!url.Contains("?"))
                {
                    url += "?";
                }
                url += "sort=";
                if(!_sortingOrder)
                {
                    url += "-";
                }
                url += _sortingAttribute;
            }
                       
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_apiUrl);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Authorization", _apiKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.api+json"));

                HttpResponseMessage response = await client.GetAsync(url);

                _status = (int)response.StatusCode;
                _statusText = response.StatusCode.ToString();

                string jsonString = await response.Content.ReadAsStringAsync();
                ResponseRootObject returnObject = null;

                if (_isSuccessStatusCode = response.IsSuccessStatusCode)
                {
                    ResponseRootObject responseData = (ResponseRootObject)JsonConvert.DeserializeObject(jsonString);

                    returnObject = Helpers.ConvertToEndpointType(responseData, (Endpoint)Enum.Parse(typeof(Endpoint),_endpoint));                     
                    _rootObject = returnObject;
                    _nextUrl = returnObject.Links.Next.Replace(_apiUrl, "");
                    _previousUrl = returnObject.Links.Previous.Replace(_apiUrl, "");
                    _firstUrl = returnObject.Links.First.Replace(_apiUrl, "");
                    return responseData;
                }
                else
                {
                    ErrorsRootObject responseError = (ErrorsRootObject)JsonConvert.DeserializeObject(jsonString);
                    //TODO: Error handling including throttling (errorcode 4XX)
                    return responseError;
                }
            }
        }

        public async Task<IRootObject> NextPageAsync()
        {
            if (string.IsNullOrEmpty(_nextUrl))
            {
                throw new ArgumentNullException("Next page not found");
            }
            return await Query(_nextUrl);
        }

        public async Task<IRootObject> PreviousPageAsync()
        {
            if (string.IsNullOrEmpty(_previousUrl))
            {
                throw new ArgumentNullException("Previous page not found");
            }
            return await Query(_previousUrl);
        }

        public async Task<IRootObject> FirstPageAsync()
        {
            if (string.IsNullOrEmpty(_firstUrl))
            {
                throw new ArgumentNullException("First page not found");
            }
            return await Query(_firstUrl);
        }

        private object GetEndpointType(Endpoint endpoint)
        {
            throw new NotImplementedException();
        }

        ///<summary>
        ///Get or Set the match query limit
        ///</summary>
        public int Limit
        {
            get { return _limit; }
            set
            {
                if (value >= 1 && value <= MAXLIMIT)
                {
                    _limit = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Invalid value. Value must be between 1 and " + MAXLIMIT.ToString());
                }
            }
        }

        public bool Testmode
        {
            get { return _testmode; }
            private set {
                if (value)
                {
                    _apiUrl = API_URL_TEST;
                }
                else
                {
                    _apiUrl = API_URL;
                }
                _testmode = value;
            }
        }

        public string SortingAttribute
        {
            get
            {
                return _sortingAttribute;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Attribute must contain a value");
                }
                _sortingAttribute = value;
            }
        }

        [Description("Use \"ASC\" or \"DESC\" to set sorting order. Default value is \"ASC\"")]
        public string SortingOrder
        {
            get
            {
                if (_sortingOrder) return "ASC";
                else return "DESC";
            }
            set
            {
                if (value == "ASC") _sortingOrder = true;
                else if (value == "DESC") _sortingOrder = false;
                else
                {
                    throw new ArgumentOutOfRangeException("Not a valid sorting order - Use \"ASC\" or \"DESC\"");
                }
            }
        }
    }
}
