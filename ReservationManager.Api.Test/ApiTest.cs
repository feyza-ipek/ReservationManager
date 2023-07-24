using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using ReservationManager.Domain.Models.GeneralResponses;
using ReservationManager.Domain.Models.RequestDtos.Reservation;
using ReservationManager.Domain.Models.RequestDtos.Table;
using ReservationManager.Domain.Models.ResponseModels.Table;

namespace ReservationManager.Api.Test
{
    [TestClass]
    public class ApiTest
    {
        private readonly HttpClient _httpClient;
        public ApiTest()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            _httpClient = webAppFactory.CreateClient();
        }


        #region SaveTable Action Tests
        [TestMethod]
        public async Task Table_SaveTable_SaveTableRequestDto_ReturnEqualTrue()
        {
            //Arrange
            var serializePostData = JsonSerializer.Serialize(new SaveTableRequestDto()
            {
                Capacity = 1,
                Number = 1
            });

            //Act
            var clientResponse = await _httpClient.PostAsync("/Table/SaveTable", new StringContent(serializePostData, Encoding.UTF8, "application/json"));
            var contentReadStringValue = await clientResponse.Content.ReadAsStringAsync();
            var responseService = JsonSerializer.Deserialize<GeneralDataResponse<SaveTableResponse>>(contentReadStringValue);

            //Assert
            Assert.IsTrue(responseService?.IsSuccess);
        }
        [TestMethod]
        public async Task Table_SaveTable_DefaultRequestDto_ReturnEqualFalse()
        {
            //Arrange
            var serializePostData = JsonSerializer.Serialize(default(SaveTableRequestDto));

            //Act
            var clientResponse = await _httpClient.PostAsync("/Table/SaveTable", new StringContent(serializePostData, Encoding.UTF8, "application/json"));
            var contentReadStringValue = await clientResponse.Content.ReadAsStringAsync();
            var responseService = JsonSerializer.Deserialize<GeneralDataResponse<SaveTableResponse>>(contentReadStringValue);

            //Assert
            Assert.IsFalse(responseService?.IsSuccess);
        }
        [TestMethod]
        public async Task Table_SaveTable_SaveTableRequestDtoZeroCapacity_ReturnEqualFalse()
        {
            //Arrange
            var serializePostData = JsonSerializer.Serialize(new SaveTableRequestDto()
            {
                Capacity = 0,
                Number = 1
            });

            //Act
            var clientResponse = await _httpClient.PostAsync("/Table/SaveTable", new StringContent(serializePostData, Encoding.UTF8, "application/json"));
            var contentReadStringValue = await clientResponse.Content.ReadAsStringAsync();
            var responseService = JsonSerializer.Deserialize<GeneralDataResponse<SaveTableResponse>>(contentReadStringValue);

            //Assert
            Assert.IsFalse(responseService?.IsSuccess);
        }
        [TestMethod]
        public async Task Table_SaveTable_SaveTableRequestDtoZeroNumber_ReturnEqualFalse()
        {
            //Arrange
            var serializePostData = JsonSerializer.Serialize(new SaveTableRequestDto()
            {
                Capacity = 1,
                Number = 0
            });

            //Act
            var clientResponse = await _httpClient.PostAsync("/Table/SaveTable", new StringContent(serializePostData, Encoding.UTF8, "application/json"));
            var contentReadStringValue = await clientResponse.Content.ReadAsStringAsync();
            var responseService = JsonSerializer.Deserialize<GeneralDataResponse<SaveTableResponse>>(contentReadStringValue);

            //Assert
            Assert.IsFalse(responseService?.IsSuccess);
        }


        #endregion

        #region GetTableList Action Tests

        [TestMethod]
        public async Task Table_GetTableList_ReturnEqualTrue()
        {

            //Act
            var newUri = _httpClient.BaseAddress?.OriginalString + "/Table/GetTableList";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(newUri),
                Content = new StringContent("", Encoding.UTF8, MediaTypeNames.Application.Json ),
            };
            var clientResponse = await _httpClient.SendAsync(request);
            var contentReadStringValue = await clientResponse.Content.ReadAsStringAsync();
            var responseService = JsonSerializer.Deserialize<GeneralListResponse<GetTableListResponse>>(contentReadStringValue);

            //Assert
            Assert.IsTrue(responseService?.IsSuccess);
        }

        #endregion
        #region GetTablesWithoutReservation Action Tests
        [TestMethod]
        public async Task Table_GetTablesWithoutReservation_GetTablesWithoutReservationRequestDto_ReturnEqualTrue()
        {
            //Arrange
            var serializeSendDto = JsonSerializer.Serialize(new GetTablesWithoutReservationRequestDto()
            {
                Date = DateTime.Now,
                Guests = 1
            });
            var serializePostDataSaveTableDto = JsonSerializer.Serialize(new SaveTableRequestDto()
            {
                Capacity = 2,
                Number = 1
            });
            //Act
             await _httpClient.PostAsync("/Table/SaveTable", new StringContent(serializePostDataSaveTableDto, Encoding.UTF8, "application/json"));
            var newUri = _httpClient.BaseAddress?.OriginalString + "/Table/GetTablesWithoutReservation";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(newUri),
                Content = new StringContent(serializeSendDto, Encoding.UTF8, MediaTypeNames.Application.Json),
            };
            var clientResponse = await _httpClient.SendAsync(request);
            var contentReadStringValue = await clientResponse.Content.ReadAsStringAsync();
            var responseService = JsonSerializer.Deserialize<GeneralListResponse<GetTableListResponse>>(contentReadStringValue);

            //Assert
            Assert.IsTrue(responseService?.IsSuccess);
        }
        [TestMethod]
        public async Task Table_GetTablesWithoutReservation_GetTablesWithoutReservationRequestDtoNullDate_ReturnEqualFalse()
        {
            //Arrange
            var serializeSendDto = JsonSerializer.Serialize(new GetTablesWithoutReservationRequestDto()
            {
                Date = null,
                Guests = 1
            });
            var serializePostDataSaveTableDto = JsonSerializer.Serialize(new SaveTableRequestDto()
            {
                Capacity = 2,
                Number = 1
            });
            //Act
             await _httpClient.PostAsync("/Table/SaveTable", new StringContent(serializePostDataSaveTableDto, Encoding.UTF8, "application/json"));
            var newUri = _httpClient.BaseAddress?.OriginalString + "/Table/GetTablesWithoutReservation";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(newUri),
                Content = new StringContent(serializeSendDto, Encoding.UTF8, MediaTypeNames.Application.Json),
            };
            var clientResponse = await _httpClient.SendAsync(request);
            var contentReadStringValue = await clientResponse.Content.ReadAsStringAsync();
            var responseService = JsonSerializer.Deserialize<GeneralListResponse<GetTableListResponse>>(contentReadStringValue);

            //Assert
            Assert.IsFalse(responseService?.IsSuccess);
        }
        [TestMethod]
        public async Task Table_GetTablesWithoutReservation_GetTablesWithoutReservationRequestDtoZeroGuests_ReturnEqualFalse()
        {
            //Arrange
            var serializeSendDto = JsonSerializer.Serialize(new GetTablesWithoutReservationRequestDto()
            {
                Date = DateTime.Now,
                Guests = 0
            });
            var serializePostDataSaveTableDto = JsonSerializer.Serialize(new SaveTableRequestDto()
            {
                Capacity = 2,
                Number = 1
            });
            //Act
             await _httpClient.PostAsync("/Table/SaveTable", new StringContent(serializePostDataSaveTableDto, Encoding.UTF8, "application/json"));
            var newUri = _httpClient.BaseAddress?.OriginalString + "/Table/GetTablesWithoutReservation";
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(newUri),
                Content = new StringContent(serializeSendDto, Encoding.UTF8, MediaTypeNames.Application.Json),
            };
            var clientResponse = await _httpClient.SendAsync(request);
            var contentReadStringValue = await clientResponse.Content.ReadAsStringAsync();
            var responseService = JsonSerializer.Deserialize<GeneralListResponse<GetTableListResponse>>(contentReadStringValue);

            //Assert
            Assert.IsFalse(responseService?.IsSuccess);
        }

        #endregion

        #region SaveReservation Action Tests

        [TestMethod]
        public async Task Reservation_SaveReservation_SaveReservationRequestDto_ReturnEqualTrue()
        {
            //Arrange
            var serializeSendDto = JsonSerializer.Serialize(new SaveReservationRequestDto()
            {
                CustomerName = "Example",
                NumberOfGuests = 1,
                ReservationDate = DateTime.Now,
            });
            var serializePostDataSaveTableDto = JsonSerializer.Serialize(new SaveTableRequestDto()
            {
                Capacity = 2,
                Number = 1
            });
            //Act
            await _httpClient.PostAsync("/Table/SaveTable", new StringContent(serializePostDataSaveTableDto, Encoding.UTF8, "application/json"));
            var clientResponse = await _httpClient.PostAsync("/Reservation/SaveReservation", new StringContent(serializeSendDto, Encoding.UTF8, "application/json"));
            var contentReadStringValue = await clientResponse.Content.ReadAsStringAsync();
            var responseService = JsonSerializer.Deserialize<GeneralListResponse<GetTableListResponse>>(contentReadStringValue);

            //Assert
            Assert.IsTrue(responseService?.IsSuccess);
        }

        [TestMethod]
        public async Task Reservation_SaveReservation_SaveReservationRequestDtoNotAvailableTable_ReturnEqualFalse()
        {
            //Arrange
            var serializeSendDto = JsonSerializer.Serialize(new SaveReservationRequestDto()
            {
                CustomerName = "Example",
                NumberOfGuests = 1,
                ReservationDate = DateTime.Now,
            });
           
            //Act
            var clientResponse = await _httpClient.PostAsync("/Reservation/SaveReservation", new StringContent(serializeSendDto, Encoding.UTF8, "application/json"));
            var contentReadStringValue = await clientResponse.Content.ReadAsStringAsync();
            var responseService = JsonSerializer.Deserialize<GeneralListResponse<GetTableListResponse>>(contentReadStringValue);

            //Assert
            Assert.IsFalse(responseService?.IsSuccess);
        }

        [TestMethod]
        public async Task Reservation_SaveReservation_SaveReservationRequestDtoNullCustomerName_ReturnEqualFalse()
        {
            //Arrange
            var serializeSendDto = JsonSerializer.Serialize(new SaveReservationRequestDto()
            {
                CustomerName = null,
                NumberOfGuests = 1,
                ReservationDate = DateTime.Now,
            });
            var serializePostDataSaveTableDto = JsonSerializer.Serialize(new SaveTableRequestDto()
            {
                Capacity = 2,
                Number = 1
            });
            //Act
            await _httpClient.PostAsync("/Table/SaveTable", new StringContent(serializePostDataSaveTableDto, Encoding.UTF8, "application/json"));
            var clientResponse = await _httpClient.PostAsync("/Reservation/SaveReservation", new StringContent(serializeSendDto, Encoding.UTF8, "application/json"));
            var contentReadStringValue = await clientResponse.Content.ReadAsStringAsync();
            var responseService = JsonSerializer.Deserialize<GeneralListResponse<GetTableListResponse>>(contentReadStringValue);

            //Assert
            Assert.IsFalse(responseService?.IsSuccess);
        }
        [TestMethod]
        public async Task Reservation_SaveReservation_SaveReservationRequestDtoZeroNumberOfGuests_ReturnEqualFalse()
        {
            //Arrange
            var serializeSendDto = JsonSerializer.Serialize(new SaveReservationRequestDto()
            {
                CustomerName = "Example",
                NumberOfGuests = 0,
                ReservationDate = DateTime.Now,
            });
            var serializePostDataSaveTableDto = JsonSerializer.Serialize(new SaveTableRequestDto()
            {
                Capacity = 2,
                Number = 1
            });
            //Act
            await _httpClient.PostAsync("/Table/SaveTable", new StringContent(serializePostDataSaveTableDto, Encoding.UTF8, "application/json"));
            var clientResponse = await _httpClient.PostAsync("/Reservation/SaveReservation", new StringContent(serializeSendDto, Encoding.UTF8, "application/json"));
            var contentReadStringValue = await clientResponse.Content.ReadAsStringAsync();
            var responseService = JsonSerializer.Deserialize<GeneralListResponse<GetTableListResponse>>(contentReadStringValue);

            //Assert
            Assert.IsFalse(responseService?.IsSuccess);
        }
        [TestMethod]
        public async Task Reservation_SaveReservation_SaveReservationRequestDtoNullReservationDate_ReturnEqualFalse()
        {
            //Arrange
            var serializeSendDto = JsonSerializer.Serialize(new SaveReservationRequestDto()
            {
                CustomerName = "Example",
                NumberOfGuests = 0,
            });
            var serializePostDataSaveTableDto = JsonSerializer.Serialize(new SaveTableRequestDto()
            {
                Capacity = 2,
                Number = 1
            });
            //Act
            await _httpClient.PostAsync("/Table/SaveTable", new StringContent(serializePostDataSaveTableDto, Encoding.UTF8, "application/json"));
            var clientResponse = await _httpClient.PostAsync("/Reservation/SaveReservation", new StringContent(serializeSendDto, Encoding.UTF8, "application/json"));
            var contentReadStringValue = await clientResponse.Content.ReadAsStringAsync();
            var responseService = JsonSerializer.Deserialize<GeneralListResponse<GetTableListResponse>>(contentReadStringValue);

            //Assert
            Assert.IsFalse(responseService?.IsSuccess);
        }
        #endregion
    }
}
