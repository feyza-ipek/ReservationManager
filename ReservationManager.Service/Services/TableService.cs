using ReservationManager.Domain.Models.GeneralResponses;
using ReservationManager.Domain.Models.RequestDtos.Table;
using ReservationManager.Domain.Models.ResponseModels.Table;
using ReservationManager.Repository.IRepositories;
using ReservationManager.Service.IServices;

namespace ReservationManager.Service.Services
{
    public class TableService : ITableService
    {
        private readonly ITableRepository _tableRepository;

        public TableService(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }

        public GeneralDataResponse<SaveTableResponse> SaveTable(SaveTableRequestDto requestSaveTable)
        {
            if (requestSaveTable == null)
            {
                return new GeneralDataResponse<SaveTableResponse>()
                {
                    IsSuccess = false,
                    Message = "Gönderdiğiniz verileri kontrol ediniz."
                };
            }
            if (requestSaveTable.Number == 0)
            {
                return new GeneralDataResponse<SaveTableResponse>()
                {
                    IsSuccess = false,
                    Message = "Masa numarası zorunludur ve sıfırdan büyük değer girilmelidir."
                };
            }
            if (requestSaveTable.Capacity == 0)
            {
                return new GeneralDataResponse<SaveTableResponse>()
                {
                    IsSuccess = false,
                    Message = "Masa kapasitesi zorunludur ve sıfırdan büyük değer girilmelidir."
                };
            }
            var saveTableReferanceId= _tableRepository.SaveTable(requestSaveTable);
            return new GeneralDataResponse<SaveTableResponse>()
            {
                IsSuccess = true,
                Data = new SaveTableResponse()
                {
                    ReferenceId = saveTableReferanceId
                },
                Message = "İşlem Başarı İle Tamamlanmıştır."
            };

        }

        public GeneralListResponse<GetTableListResponse> GetTableList()
        {
            var getTableListResponse = _tableRepository.GetTableList();
            if(getTableListResponse == null)
            {
                return new GeneralListResponse<GetTableListResponse>
                {
                    IsSuccess = false,
                    Items = null,
                    Message = "Geçerli Bir Masa Listesi Kaydı Bulunamamıştır."

                };
            }
            return new GeneralListResponse<GetTableListResponse>()
            {
                IsSuccess = true,
                Items = getTableListResponse.Select(x=>new GetTableListResponse()
                {
                    Cancel = x.Cancel,
                    Capacity = x.Capacity,
                    Number = x.Number,
                    TableId = x.Id
                    
                }).ToList(),
                Message = "İşlem Başarı İle Tamamlanmıştır."
            };
        }

        public GeneralListResponse<GetTablesWithoutReservationResponse> GetTablesWithoutReservation(GetTablesWithoutReservationRequestDto requestTablesWithoutReservation)
        {
            if (requestTablesWithoutReservation == null)
            {
                return new GeneralListResponse<GetTablesWithoutReservationResponse>()
                {
                    IsSuccess = false,
                    Message = "Gönderdiğiniz verileri kontrol ediniz."

                };
            }
            if (!requestTablesWithoutReservation.Date.HasValue)
            {
                return new GeneralListResponse<GetTablesWithoutReservationResponse>()
                {
                    IsSuccess = false,
                    Message = "Lütfen Tarih Giriniz."
                };
            }
            if (requestTablesWithoutReservation.Guests == 0)
            {
                return new GeneralListResponse<GetTablesWithoutReservationResponse>()
                {
                    IsSuccess = false,
                    Message = "Lütfen Kişi Sayısı Giriniz."
                };
            }
            var getTablesWithoutReservationList = _tableRepository.GetTablesWithoutReservation(requestTablesWithoutReservation);
            if(getTablesWithoutReservationList == null)
            {
                return new GeneralListResponse<GetTablesWithoutReservationResponse>()
                {
                    Message = "Üzgünüz, uygun masa bulunamadı.",
                    IsSuccess = false,
                    Items = new List<GetTablesWithoutReservationResponse>()
                };
            }
            return new GeneralListResponse<GetTablesWithoutReservationResponse>()
            {
                IsSuccess = true,
                Items = getTablesWithoutReservationList.Select(x=> new GetTablesWithoutReservationResponse()
                {
                    Cancel = x.Cancel,
                    Capacity = x.Capacity,
                    Number = x.Number,
                    TableId = x.Id
                }).ToList(),
                Message = "İşlem Başarı İle Tamamlanmıştır."
            };

        }
    }
}
