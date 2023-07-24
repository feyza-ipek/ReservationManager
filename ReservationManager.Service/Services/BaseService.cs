using ReservationManager.Repository.IRepositories;
using ReservationManager.Service.IServices;
namespace ReservationManager.Service.Services
{
    public class BaseService : IBaseService
    {
        private readonly ICommonRepository _commonRepository;
        public BaseService(ICommonRepository commonRepository)
        {
            _commonRepository = commonRepository;
        }


        public void SaveChanges()
        {
            _commonRepository.SaveChanges();
        }
    }
}
