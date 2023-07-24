using Moq;
using ReservationManager.Domain.Entities;
using ReservationManager.Domain.Models.RequestDtos.Table;
using ReservationManager.Repository.IRepositories;
using ReservationManager.Service.IServices;
using ReservationManager.Service.Services;

namespace ReservationManager.Test
{
    public class TableServiceTest
    {
        private readonly Mock<ITableRepository> _tableRepository;
        private readonly ITableService _tableService;
        public TableServiceTest()
        {
            _tableRepository = new Mock<ITableRepository>();
            _tableService = new TableService(_tableRepository.Object);
        }

        #region SaveTable Method Test Methods
        [Fact]
        public void SaveTable_SaveTableRequestDto_ReturnEqualTrue()
        {
            //Arrange
            var saveTableRequest = new SaveTableRequestDto()
            {
                Capacity = 5,
                Number = 2
            };
            //Act

            _tableRepository.Setup(x => x.SaveTable(saveTableRequest));
            var expected = _tableService.SaveTable(saveTableRequest);

            //Assert
            Assert.True(expected.IsSuccess);
        }
        [Fact]
        public void SaveTable_NullRequestDto_ReturnEqualFalse()
        {
            //Arrange
            SaveTableRequestDto saveTableRequest = null;
            //Act

            _tableRepository.Setup(x => x.SaveTable(saveTableRequest));
            var expected = _tableService.SaveTable(saveTableRequest);

            //Assert
            Assert.False(expected.IsSuccess);
        }
        [Fact]
        public void SaveTable_ZeroCapacityRequestDto_ReturnEqualFalse()
        {
            //Arrange
            SaveTableRequestDto saveTableRequest = new SaveTableRequestDto()
            {
                Capacity = 0,
                Number = 1
            };
            //Act

            _tableRepository.Setup(x => x.SaveTable(saveTableRequest));
            var expected = _tableService.SaveTable(saveTableRequest);

            //Assert
            Assert.False(expected.IsSuccess);
        }
        [Fact]
        public void SaveTable_ZeroNumberRequestDto_ReturnEqualFalse()
        {
            //Arrange
            SaveTableRequestDto saveTableRequest = new SaveTableRequestDto()
            {
                Capacity = 1,
                Number = 0
            };
            //Act

            _tableRepository.Setup(x => x.SaveTable(saveTableRequest));
            var expected = _tableService.SaveTable(saveTableRequest);

            //Assert
            Assert.False(expected.IsSuccess);
        }
        #endregion

        #region GetTableList Method Test Methods
        [Fact]
        public void GetTableList_ReturnEqualTrue()
        {
            //Act
            _tableRepository.Setup(x => x.GetTableList()).Returns(new List<Table>()
            {
                new ()
                {
                    Number = 0,
                    Id = 1,
                    Capacity = 1,
                    Cancel = false
                }
            });
            var expected = _tableService.GetTableList();

            //Assert
            Assert.True(expected.IsSuccess);
        }
        [Fact]
        public void GetTableList_ReturnEqualFalse()
        {
            //Act
            _tableRepository.Setup(x => x.GetTableList());
            var expected = _tableService.GetTableList();

            //Assert
            Assert.False(expected.IsSuccess);
        }
        #endregion

        #region GetTablesWithoutReservation Method Test Methods
        [Fact]
        public void GetTablesWithoutReservation_GetTablesWithoutReservationRequestDto_ReturnEqualTrue()
        {
            //Arrange
            var getTablesWithoutReservationRequest = new GetTablesWithoutReservationRequestDto()
            {
               Date = DateTime.Now,
               Guests = 1
            };
            //Act

            _tableRepository.Setup(x => x.GetTablesWithoutReservation(getTablesWithoutReservationRequest)).Returns(new List<Table>()
            {
                new Table()
                {
                    Cancel=false,
                    Capacity = 1,
                    Id = 2,
                    Number = 3
                }
            });
            var expected = _tableService.GetTablesWithoutReservation(getTablesWithoutReservationRequest);

            //Assert
            Assert.True(expected.IsSuccess);
        }
    
        #endregion

    }
}
