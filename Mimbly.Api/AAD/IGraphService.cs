namespace Mimbly.Api.AAD;

using Microsoft.Graph;

public interface IGraphService
{
    GraphServiceClient GetClient();
}
