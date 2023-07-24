using ReservationManager.Domain.Entities;
using ReservationManager.Domain.Models.EmailSender;
using ReservationManager.Domain.Models.GeneralResponses;
using ReservationManager.Domain.Models.RequestDtos.Reservation;
using ReservationManager.Domain.Models.RequestDtos.Table;
using ReservationManager.Domain.Models.ResponseModels.Reservation;
using ReservationManager.Infrastructure.Email.Interfaces;
using ReservationManager.Repository.IRepositories;
using ReservationManager.Service.IServices;

namespace ReservationManager.Service.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ITableService _tableService;
        private readonly IEmailSender _emailSender;

        public ReservationService(IReservationRepository reservationRepository, ITableService tableService,IEmailSender emailSender) 
        {
            _reservationRepository = reservationRepository;
            _tableService = tableService;
            _emailSender = emailSender;
        }

        public GeneralDataResponse<SaveReservationResponse> SaveReservation(SaveReservationRequestDto requestSaveReservation)
        {
            if (requestSaveReservation == null)
            {
                return new GeneralDataResponse<SaveReservationResponse>()
                {
                    IsSuccess = false,
                    Message = "Gönderdiğiniz verileri kontrol ediniz."
                };
            }
            if (string.IsNullOrEmpty(requestSaveReservation.CustomerName))
            {
                return new GeneralDataResponse<SaveReservationResponse>()
                {
                    IsSuccess = false,
                    Message = "Lütfen Müşteri Adını Giriniz."
                };
            } 
            if (!requestSaveReservation.ReservationDate.HasValue)
            {
                return new GeneralDataResponse<SaveReservationResponse>()
                {
                    IsSuccess = false,
                    Message = "Lütfen Tarih Giriniz."
                };
            }
            if (requestSaveReservation.NumberOfGuests == 0)
            {
                return new GeneralDataResponse<SaveReservationResponse>()
                {
                    IsSuccess = false,
                    Message = "Lütfen Kişi Sayısını Giriniz."
                };
            }
            var availableTableListForReservationResponse = _tableService.GetTablesWithoutReservation(new GetTablesWithoutReservationRequestDto()
            {
                Date = requestSaveReservation.ReservationDate,
                Guests = requestSaveReservation.NumberOfGuests
            });
            if (!availableTableListForReservationResponse.IsSuccess)
            {
                return new GeneralDataResponse<SaveReservationResponse>()
                {
                    IsSuccess = availableTableListForReservationResponse.IsSuccess,
                    Data = null,
                    Message = availableTableListForReservationResponse.Message
                };
            }
            var availableTable = availableTableListForReservationResponse.Items.FirstOrDefault();
            if(availableTable == null)
            {
                return new GeneralDataResponse<SaveReservationResponse>()
                {
                    Message = "Üzgünüz, uygun masa bulunamadı.",
                    IsSuccess = false,
                    Data = null
                };
            }
            requestSaveReservation.TableNumber = availableTable.TableId;
            var saveReservationReferenceId = _reservationRepository.SaveReservation(new Reservation()
            {
                CustomerName = requestSaveReservation.CustomerName,
                NumberOfGuests = requestSaveReservation.NumberOfGuests,
                TableNumber = requestSaveReservation.TableNumber,
                ReservationDate = requestSaveReservation.ReservationDate.Value
            });
            _emailSender.Send(new EmailSenderDto()
            {
                Body = "ExampleBody",
                Subject = "ExampleSubject",
                EmailAddress = "ExampleEmailAddres"
            });
            return new GeneralDataResponse<SaveReservationResponse>()
            {
                IsSuccess = true,
                Data = new SaveReservationResponse()
                {
                    ReferenceId = saveReservationReferenceId
                },
                Message = "İşlem Başarı İle Tamamlanmıştır."
            };

        }
        public GeneralResponse DeleteReservation(DeleteReservationRequestDto requestDeleteReservation)
        {
            if(requestDeleteReservation == null)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Message = "Gönderdiğiniz verileri kontrol ediniz."
                };
            }
            if (requestDeleteReservation.Id == 0)
            {
                return new GeneralResponse()
                {
                    IsSuccess = false,
                    Message = "Id verisi sıfırdan farklı olmalıdır."
                };
            }
            var entityReservation = _reservationRepository.GetById(requestDeleteReservation.Id);
            if(entityReservation == null)
            {
                return new GeneralResponse()
                {
                    Message = "Silme işlemi yapılacak olan kayıt bulunamadı.",
                    IsSuccess = false
                };
            }
            _reservationRepository.DeleteReservation(entityReservation);
            return new GeneralResponse()
            {
                IsSuccess = true
            };
           
        }
    }
}
