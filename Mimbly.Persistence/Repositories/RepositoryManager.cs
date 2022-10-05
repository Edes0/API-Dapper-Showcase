//namespace Mimbly.Persistence.Repositories; ////TODO: REMOVE IF UNUSED

//using Mimbly.Application.Common.Interfaces;

//public sealed class RepositoryManager : IRepositoryManager
//{
//    private readonly ISqlDataAccess _sqlDataAccess;

//    private readonly Lazy<IMimboxRepository> _mimboxRepository;
//    private readonly Lazy<IIdentityRepository> _identityRepository;
//    //Add new items from database here if needed

//    public RepositoryManager(ISqlDataAccess sqlDataAccess)
//    {
//        _sqlDataAccess = sqlDataAccess;

//        _mimboxRepository = new Lazy<IMimboxRepository>(() => new
//        MimboxRepository(_sqlDataAccess));

//        _identityRepository = new Lazy<IIdentityRepository>(() => new
//        IdentityRepository(_sqlDataAccess));
//    }
//    public IMimboxRepository Mimbox => _mimboxRepository.Value;
//}