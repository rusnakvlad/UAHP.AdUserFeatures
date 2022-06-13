using Application.Common.Interfaces;
using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using Persistence.DatabaseSettings;

namespace Persistence;

public class MongoContext : IMongoContext
{
    private readonly IMongoClient client;
    private IMongoDatabase database;
    public MongoContext()
    {
        var pack = new ConventionPack
        {
            new StringObjectIdConvention()
        };
        ConventionRegistry.Register("MongoObjectIdParsingConventions", pack, _ => true);

        client = new MongoClient(DBSettigns.ConnenctionString);
        database = client.GetDatabase(DBSettigns.DatabaseName);
    }
    public IMongoCollection<Ad> Advertisments { get => database.GetCollection<Ad>(Collections.Advertisments); }
    public IMongoCollection<FavoriteAd> FavoriteAds { get => database.GetCollection<FavoriteAd>(Collections.FavoriteAds); }
    public IMongoCollection<ComparasionAd> ComparasionAds { get => database.GetCollection<ComparasionAd>(Collections.ComparisionAds); }
    public IMongoCollection<Comment> Comments { get => database.GetCollection<Comment>(Collections.Comments); }
    public IMongoCollection<User> Users { get => database.GetCollection<User>(Collections.Users); }
    
    private class StringObjectIdConvention : ConventionBase, IPostProcessingConvention
    {
        public void PostProcess(BsonClassMap classMap)
        {
            var idMap = classMap.IdMemberMap;
            if (idMap != null && idMap.MemberName == "Id" && idMap.MemberType == typeof(string))
            {
                idMap.SetIdGenerator(new StringObjectIdGenerator());
            }
        }
    }
}
