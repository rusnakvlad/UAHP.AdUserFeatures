using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Application.Common.Interfaces;

public interface IMongoContext
{
    public IMongoCollection<Ad> Advertisments { get; }
    public IMongoCollection<FavoriteAd> FavoriteAds { get; }
    public IMongoCollection<ComparasionAd> ComparasionAds { get; }
    public IMongoCollection<Comment> Comments { get; }
    public IMongoCollection<User> Users { get; }
}
