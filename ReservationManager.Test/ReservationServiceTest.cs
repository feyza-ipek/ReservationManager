using Moq;
using ReservationManager.Domain.Entities;
using ReservationManager.Domain.Models.EmailSender;
using ReservationManager.Domain.Models.RequestDtos.Reservation;
using ReservationManager.Domain.Models.RequestDtos.Table;
using ReservationManager.Infrastructure.Email.Interfaces;
using ReservationManager.Repository.IRepositories;
using ReservationManager.Service.IServices;
using ReservationManager.Service.Services;

namespace ReservationManager.Test
{
    public class ReservationServiceTest
    {
        private readonly Mock<IReservationRepository> _reservationRepository;
        private readonly ITableService _tableService;
        private readonly IReservationService _reservationService;
        private readonly Mock<ITableRepository> _tableRepository;
        private readonly Mock<IEmailSender> _emailSender;
        public ReservationServiceTest()
        {
            _emailSender = new Mock<IEmailSender>();
            _reservationRepository = new Mock<IReservationRepository>();
            _tableRepository = new Mock<ITableRepository>();
            _tableService = new TableService(_tableRepository.Object);
            _reservationService = new ReservationService(_reservationRepository.Object, _tableService,_emailSender.Object);
        }

        #region SaveReservation Method Test Methods
           
        [Fact]
        public void SaveReservation_SaveReservationRequestDto_ReturnEqualTrue()
        {
            //Arrange
            var requestSaveReservation = new SaveReservationRequestDto()
            {
                CustomerName = "Customer Name",
                ReservationDate = DateTime.Now,
                NumberOfGuests = 1
            };

            //Act
            _reservationRepository.Setup(x => x.SaveReservation(It.IsAny<Reservation>()));
            _emailSender.Setup(x => x.Send(It.IsAny<EmailSenderDto>())).Returns(true);
            _tableRepository.Setup(x => x.GetTablesWithoutReservation(It.IsAny<GetTablesWithoutReservationRequestDto>())).Returns(new List<Table>()
            {
                    new Table()
                    {
                        Cancel = false,
                        Capacity = 1,
                        Number = 1,
                        Id = 1
                    }
            });
            var expected = _reservationService.SaveReservation(requestSaveReservation);

            //Assert
            Assert.True(expected.IsSuccess);
        }

        [Fact]
        public void SaveReservation_NullRequestModel_ReturnEqualFalse()
        {
            //Arrange
            SaveReservationRequestDto requestSaveReservation = null;

            //Act
            _reservationRepository.Setup(x => x.SaveReservation(It.IsAny<Reservation>()));
            _tableRepository.Setup(x => x.GetTablesWithoutReservation(It.IsAny<GetTablesWithoutReservationRequestDto>())).Returns(new List<Table>()
            {
                new()
                {
                    Id = 1,
                    Cancel = false,
                    Capacity = 1,
                    Number = 1
                }
            });
            var expected = _reservationService.SaveReservation(requestSaveReservation);
            //Assert
            Assert.False(expected.IsSuccess);
        }

        [Fact]
        public void SaveReservation_SaveReservationRequestDtoZeroNumberOfGuests_ReturnEqualFalse()
        {
            //Arrange
            var requestSaveReservation = new SaveReservationRequestDto()
            {
                ReservationDate = DateTime.Now,
                CustomerName = "Customer Name",
                NumberOfGuests = 0
            };

            //Act
            _reservationRepository.Setup(x => x.SaveReservation(It.IsAny<Reservation>()));
            _tableRepository.Setup(x => x.GetTablesWithoutReservation(It.IsAny<GetTablesWithoutReservationRequestDto>())).Returns(new List<Table>()
            {
                new()
                {
                    Id = 1,
                    Cancel = false,
                    Capacity = 1,
                    Number = 1
                }
            });
            var expected = _reservationService.SaveReservation(requestSaveReservation);
            
            //Assert
            Assert.False(expected.IsSuccess);
        }


        [Fact]
        public void SaveReservation_SaveReservationRequestDtoNullCustomerName_ReturnEqualFalse()
        {
            //Arrange
            var requestSaveReservation = new SaveReservationRequestDto()
            {
                ReservationDate = DateTime.Now,
                CustomerName = null,
                NumberOfGuests = 2
            };

            //Act
            _reservationRepository.Setup(x => x.SaveReservation(It.IsAny<Reservation>()));
            _tableRepository.Setup(x => x.GetTablesWithoutReservation(It.IsAny<GetTablesWithoutReservationRequestDto>())).Returns(new List<Table>()
            {
                new()
                {
                    Id = 1,
                    Cancel = false,
                    Capacity = 1,
                    Number = 1
                }
            });
            var expected= _reservationService.SaveReservation(requestSaveReservation);
            
            //Assert
            Assert.False(expected.IsSuccess);
        }

        [Fact]
        public void SaveReservation_SaveReservationRequestDtoNullReservationDate_ReturnEqualFalse()
        {
            //Arrange
            var requestSaveReservation = new SaveReservationRequestDto()
            {
                ReservationDate = null,
                CustomerName = "Customer Name",
                NumberOfGuests = 2
            };
            
            //Act
            _reservationRepository.Setup(x => x.SaveReservation(It.IsAny<Reservation>()));
            _tableRepository.Setup(x => x.GetTablesWithoutReservation(It.IsAny<GetTablesWithoutReservationRequestDto>())).Returns(new List<Table>()
            {
                new()
                {
                    Id = 1,
                    Cancel = false,
                    Capacity = 1,
                    Number = 1
                }
            });
            var expected = _reservationService.SaveReservation(requestSaveReservation);
            
            //Assert
            Assert.False(expected.IsSuccess);
        }


        [Fact]
        public void SaveReservation_SaveReservationRequestDtoZeroAvailableTable_ReturnEqualFalse()
        {
            //Arrange
            var requestSaveReservation = new SaveReservationRequestDto()
            {
                ReservationDate = DateTime.Now,
                CustomerName = "Customer Name",
                NumberOfGuests = 2
            };

            //Act
            _reservationRepository.Setup(x => x.SaveReservation(It.IsAny<Reservation>()));
            _tableRepository.Setup(x => x.GetTablesWithoutReservation(It.IsAny<GetTablesWithoutReservationRequestDto>()));
            var expected = _reservationService.SaveReservation(requestSaveReservation);

            //Assert
            Assert.False(expected.IsSuccess);
        }
        #endregion

        #region DeleteReservation Method Test Methods
        [Fact]
        public void DeleteReservation_DeleteReservationRequestDto_ReturnEqualTrue()
        {
            //Arrange
            DeleteReservationRequestDto deleteReservation = new DeleteReservationRequestDto()
            {
                Id = 1
            };

            //Act
            _reservationRepository.Setup(x => x.GetById(1)).Returns(new Reservation()
            {
                Id = 1,
                CustomerName = "",
                NumberOfGuests = 1,
                ReservationDate = DateTime.Now,
                TableNumber = 1
            });
            _reservationRepository.Setup(x => x.DeleteReservation(It.IsAny<Reservation>()));
            var expected = _reservationService.DeleteReservation(deleteReservation);

            //Assert
            Assert.True(expected.IsSuccess);
        }
        [Fact]
        public void DeleteReservation_DeleteReservationRequestDtoNoContains_ReturnEqualFalse()
        {
            //Arrange
            DeleteReservationRequestDto deleteReservation = new DeleteReservationRequestDto()
            {
                Id = 1
            };

            //Act
            _reservationRepository.Setup(x => x.GetById(1));
            _reservationRepository.Setup(x => x.DeleteReservation(It.IsAny<Reservation>()));
            var expected = _reservationService.DeleteReservation(deleteReservation);

            //Assert
            Assert.False(expected.IsSuccess);
        }
        [Fact]
        public void DeleteReservation_DeleteReservationRequestDtoZeroId_ReturnEqualFalse()
        {
            //Arrange
            DeleteReservationRequestDto deleteReservation = new DeleteReservationRequestDto()
            {
                Id = 0
            };

            //Act
            _reservationRepository.Setup(x => x.GetById(0));
            _reservationRepository.Setup(x => x.DeleteReservation(It.IsAny<Reservation>()));
            var expected = _reservationService.DeleteReservation(deleteReservation);

            //Assert
            Assert.False(expected.IsSuccess);
        }

        #endregion

    }

}