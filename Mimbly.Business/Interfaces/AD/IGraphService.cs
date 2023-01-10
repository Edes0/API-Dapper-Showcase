namespace Mimbly.Business.Interfaces.AD;

using Microsoft.Graph;

public interface IGraphService
{
    GraphServiceClient GetClient();
}
