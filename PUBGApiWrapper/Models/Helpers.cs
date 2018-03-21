
using PUBGApiWrapper.Models.PUBGApiNet.Models;
using System;

namespace PUBGApiWrapper
{
    //public static class Pc
    //{
    //    public static string NorthAmerica { get { return "pc-na"; } }
    //    public static string Asia { get { return "pc-as"; } }
    //    public static string KoreaJapan { get { return "pc-krjp"; } }
    //    public static string KAKAO { get { return "pc-kakao"; } }
    //    public static string SouthAndCentralAmerica { get { return "pc-sa"; } }
    //    public static string Europe { get { return "pc-eu"; } }
    //    public static string Oceania { get { return "pc-oc"; } }
    //    public static string SouthEastAsia { get { return "pc-sea"; } }
    //}

    //public static class Xbox
    //{
    //    public static string NorthAmerica { get { return "xbox-na"; } }
    //    public static string Asia { get { return "xbox-as"; } }
    //    public static string Europe { get { return "xbox-eu"; } }
    //    public static string Oceania { get { return "xbox-oc"; } }
    //}

    public static class Helpers
    {
        public static string GetEndpoint(Endpoint endpoint)
        {
            return endpoint.ToString().ToLower() ?? null;
        }

        public static dynamic ConvertToEndpointType(object obj, Endpoint endpoint)
        {
            switch (endpoint)
            {
                case (Endpoint.Matches):
                    return Convert.ChangeType(obj, typeof(ResponseRootObject));
                //case (Endpoint.Status):
                //    return Convert.ChangeType(obj, typeof(Data));
                default:
                    return null;
            }
        }

        public static string GetShard(Shards shard)
        {
            switch(shard)
            {
                case (Shards.PcKAKAO):
                    return "shards/pc-kakao/";
                case (Shards.PcNorthAmerica):
                    return "shards/pc-na/";
                case (Shards.PcAsia):
                    return "shards/pc-as/";
                case (Shards.PcKoreaJapan):
                    return "shards/pc-krjp/";
                case (Shards.PcSouthAndCentralAmerica):
                    return "shards/pc-sa/";
                case (Shards.PcEurope):
                    return "shards/pc-eu/";
                case (Shards.PcOceania):
                    return "shards/pc-oc/";
                case (Shards.PcSouthEastAsia):
                    return "shards/pc-sea/";
                case (Shards.XboxNorthAmerica):
                    return "shards/xbox-na/";
                case (Shards.XboxAsia):
                    return "shards/xbox-as/";
                case (Shards.XboxEurope):
                    return "shards/xbox-eu/";
                case (Shards.XboxOceania):
                    return "shards/xbox-oc/";
                case (Shards.empty):
                    return "";
                default:
                    return null;
            }               
        }
    }

    public enum Shards
    {
        empty,
        PcKAKAO,
        PcNorthAmerica,
        PcAsia,
        PcKoreaJapan,        
        PcSouthAndCentralAmerica,
        PcEurope,
        PcOceania,
        PcSouthEastAsia,
        XboxNorthAmerica,
        XboxAsia,
        XboxEurope,
        XboxOceania,
    }

    public enum Endpoint
    {
        Matches,
        Status,
    }  
}
