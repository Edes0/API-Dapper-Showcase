namespace Mimbly.Infrastructure.AAD;

using Microsoft.Graph;

public interface IGraphService
{
    GraphServiceClient GetClient();
}
