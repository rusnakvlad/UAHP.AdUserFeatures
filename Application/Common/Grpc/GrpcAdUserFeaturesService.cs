using Application.Common.Interfaces;
using AdUserFeauresServer;
using Grpc.Core;
using MongoDB.Driver;

namespace Application.Common.Grpc.GrpcAdUserFeaturesService;

public class GrpcAdUserFeaturesService : GrpcAdUserFeatures.GrpcAdUserFeaturesBase
{
    private readonly IMongoContext mongoContext;

    public GrpcAdUserFeaturesService(IMongoContext context) => mongoContext = context;

    public override Task<GetCommentsCountResponse> GetUserCommentsCount(GetCommentsCountRequest request, ServerCallContext context)
    {
        var localUserId = mongoContext.Users.AsQueryable().FirstOrDefault(x => x.ExternalId == request.UserId).Id;
        var comentsCount = (int)mongoContext.Comments.Count(c => c.UserId == localUserId);

        return Task.FromResult(new GetCommentsCountResponse { Count = comentsCount });
    }
}