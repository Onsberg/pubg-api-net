using System.Collections.Generic;

namespace PUBGApiWrapper.Models
{
    namespace PUBGApiNet.Models
    {
        public interface IRootObject
        {

        }
        //public class Match
        //{
        //    public string id { get; set; }
        //    public DateTime createdAt { get; set; }
        //    public int duration { get; set; }
        //    public List<Roster> rosters { get; set; }
        //    public List<Round> rounds { get; set; }
        //    public List<Asset> assets { get; set; }
        //    public List<Spectator> spectators { get; set; }
        //    public List<MatchStat> stats { get; set; }
        //    public string gameMode { get; set; }
        //    public string patchVersion { get; set; }
        //    public string titleId { get; set; }
        //    public string shardId { get; set; }
        //    public List<Tag> tags { get; set; }
        //}

        //public class Roster
        //{
        //    public string id { get; set; }
        //    public Team team { get; set; }
        //    public List<Participant> participants { get; set; }
        //    public List<RosterStat> stats { get; set; }
        //    public string won { get; set; }
        //    public string shardId { get; set; }
        //}

        //public class Participant
        //{
        //    public string id { get; set; }
        //    public List<ParticipantStat> stats { get; set; }
        //    public string actor { get; set; }
        //    public string shardId { get; set; }
        //}

        public class Asset
        {
            public List<AssetData> Data { get; set; }
        }

        public class AssetData
        {
            public string Type { get; set; }
            public string Id { get; set; }
        }

        public class Roster
        {
            public List<AssetData> Data { get; set; }
        }

        //public class Team { }

        public class Round { }

        public class Spectator { }

        public class MatchStat { }

        public class Tag { }

        //public class RosterStat { }

        //public class ParticipantStat { }

        public class Relationship
        {
            public Asset Assets { get; set; }
            public Roster Rosters { get; set; }
            public Round Rounds { get; set; }
            public Spectator Spectators { get; set; }
        }

        public class Data
        {            
            public string Type { get; set; }
            public string Id { get; set; }
            public Attribute Attributes { get; set; }
            public Relationship Relationship { get; set; }
            public Link Links { get; set; }
        }

        public class Link
        {
            public string Schema { get; set; }
            public string Self { get; set; }
        }

        public class Attribute
        {
            public string CreatedAt { get; set; }
            public int Duration { get; set; }
            public string GameMode { get; set; }
            public string ShardId { get; set; }
            public List<MatchStat> Stats { get; set; }
            public List<Tag> Tags { get; set; }

            private string titleId;

            public string GetTitleId()
            {
                return titleId;
            }

            public void SetTitleId(string value)
            {
                titleId = value;
            }
        }

        public class Links
        {
            public string First { get; set; }
            public string Next { get; set; }
            public string Self { get; set; }
            public string Previous { get; set; }
        }

        public class Errors
        {
            public string Title { get; set; } 
        }

        public class ErrorsRootObject : IRootObject
        {
            public List<Errors> Errors { get; set; }
        }

        public class Included
        {
            public List<object> Objects { get; set; }
        }

        public class Meta { }

        public class ResponseRootObject : IRootObject
        {
            public List<Data> Data { get; set; }
            public List<Included> Included { get; set; }
            public Links Links { get; set; }
            public Meta Meta { get; set; }
        }

    }
}
