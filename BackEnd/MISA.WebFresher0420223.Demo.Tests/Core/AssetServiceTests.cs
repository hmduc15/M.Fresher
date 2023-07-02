using AutoMapper;
using MISA.WebFresher042023.Demo.Core.Dto.Dto.Asset;
using MISA.WebFresher042023.Demo.Core.Entity;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Core.MISAException;
using MISA.WebFresher042023.Demo.Core.Service;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher0420223.Demo.Tests.Core
{
    [TestFixture]
    public class AssetServiceTests
    {

        /// <summary>
        /// Test function Get All
        /// </summary>
        /// <returns>null</returns>
        /// Author: HMDUC (23/06/2023)

        [Test]
        public async Task GetAllAync_NullEnity_ReturnNull()
        {
            //Arange
            var assetRepository = Substitute.For<IAssetRepository>();
            var mapper = Substitute.For<IMapper>();

            assetRepository.GetAllAsync().ReturnsNull();
            var assetService = new AssetService(assetRepository, mapper);

            var actualResult = await assetService.GetAllAsync();

            //Act & Assert
            Assert.That(actualResult, Is.Null);

        }

        /// <summary>
        /// Test function Get All
        /// </summary>
        /// <returns>
        /// List Asset Dto
        /// </returns>
        /// Author: HMDUC(23/06/2023)
        [Test]
        public async Task GetAllAync_ValidEntity_ListAssetDto()
        {
            //Arange
            var assetRepository = Substitute.For<IAssetRepository>();
            var mapper = Substitute.For<IMapper>();

            var listAsset = new List<Asset>()
            {
               new Asset()
               {
                   AssetId = Guid.Parse("844257ba-56d7-4bc5-892d-ceeaf0e616d5"),
                   AssetCode= "TS0001",
                   AssetName="Dell E7490",
                   Quantity =20,

               },
                new Asset()
               {
                   AssetId = Guid.Parse("097cd722-02f1-4a9f-a117-051904860f9f"),
                   AssetCode= "TS0002",
                   AssetName="Dell E7480",
                   Quantity =22,

               }
            };

            var listAssetDto = new List<AssetDto>()
            {
                 new AssetDto()
                 {
                    AssetId = Guid.Parse("844257ba-56d7-4bc5-892d-ceeaf0e616d5"),
                    AssetCode= "TS0001",
                    AssetName="Dell E7490",
                    Quantity =20,
                 },
                 new AssetDto()
                 {
                   AssetId = Guid.Parse("097cd722-02f1-4a9f-a117-051904860f9f"),
                   AssetCode= "TS0002",
                   AssetName="Dell E7480",
                   Quantity =22,

                 }
            };

            assetRepository.GetAllAsync().Returns(listAsset);
            mapper.Map<List<AssetDto>>(Arg.Any<List<Asset>>()).Returns(listAssetDto);

            var assetService = new AssetService(assetRepository, mapper);


            var actualResult = await assetService.GetAllAsync();


            //Act & Assert
            Assert.That(actualResult, Is.EqualTo(listAssetDto));

            mapper.Received(1).Map<List<AssetDto>>(listAsset);

        }



        /// <summary>
        /// Test function get Asset By Code
        ///  Case: return Null
        /// </summary>
        ///  Author: HMDUC (23/06/2023)
        [Test]
        public async Task GetByCode_NullEntity_ReturnNull()
        {
            //Arange
            var code = "TS0001";
            var assetRepository = Substitute.For<IAssetRepository>();
            var mapper = Substitute.For<IMapper>();

            assetRepository.GetByCodeAsync(code).ReturnsNull();

            var assetService = new AssetService(assetRepository, mapper);

            var actualResult = await assetRepository.GetByCodeAsync(code);

            //Act & Assert
            Assert.That(actualResult,Is.Null);
        }

        /// <summary>
        /// Test function Get Asset By Code
        /// </summary>
        /// <returns>AssetDto</returns>
        ///  Author: HMDUC (23/06/2023)
        [Test]
        public async Task GetByCode_ValidEntity_AssetDto()
        {
            //Arange
            var code = "TS0001";
            var assetRepository = Substitute.For<IAssetRepository>();
            var mapper = Substitute.For<IMapper>();

            var asset = new Asset()
            {
                AssetCode = code,
            };


            var assetDto = new AssetDto()
            {
                AssetCode = code,
            };

            assetRepository.GetByCodeAsync(code).Returns(asset);

            mapper.Map<AssetDto>(asset).Returns(assetDto);

            var assetService = new AssetService(assetRepository,mapper);

            var actualResult = await assetService.GetByCodeAsync(code);

            //Act & Assert
            Assert.That(actualResult,Is.EqualTo(assetDto));

        }


        /// <summary>
        /// Test function get Asset By Id
        ///  Case: return Null
        /// </summary>
        ///  Author: HMDUC (23/06/2023)
        [Test]
        public void GetByIdAsync_NulLEntity_ThrowNotFound()
        {
            //Arrange 
            var id = Guid.Parse("844257ba-56d7-4bc5-892d-ceeaf0e616d5");

            var assetRepositoty = new FakeAssetRepositoryNull();
            var mapper = new FakeMapper();


            var assetService = new AssetService(assetRepositoty, mapper);

            ///Act && Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await assetService.GetByIdAsync(id));

        }

        /// <summary>
        /// Test function get Asset By Id
        /// Case: return Asset
        /// </summary>
        /// Author: HMDUC (23/06/2023)
        [Test]
        public async Task GetByIdAsync_ValidEntity_AssetDto()
        {
            //Arrange 
            var id = Guid.Parse("844257ba-56d7-4bc5-892d-ceeaf0e616d5");

            var assetRepositoty = new FakeAssetRepository();
            var mapper = new FakeMapper();


            var assetService = new AssetService(assetRepositoty, mapper);


            /// Assert
            Assert.That(assetRepositoty.ActualId, Is.EqualTo(id));

        }


        /// <summary>
        /// Test function delete Asset By Id
        /// Case: Not found Asset 
        /// </summary>
        /// <return>NotFoundException</return>
        /// Author: HMDUC (23/06/2023)
        [Test]
        public void DeleteAsync_NulLEntity_ThrowNotFound()
        {
            //Arrange 
            var id = Guid.Parse("844257ba-56d7-4bc5-892d-ceeaf0e616d5");

            var assetRepositoty = Substitute.For<IAssetRepository>();
            assetRepositoty.GetByIdAsync(id).ReturnsNull();
            assetRepositoty.DeleteEntityAsync(id).Returns(1);

            var mapper = Substitute.For<IMapper>();


            var assetService = new AssetService(assetRepositoty, mapper);

            ///Act && Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await assetService.GetByIdAsync(id));

        }

        /// <summary>
        /// Test function delete Asset By Id
        /// Case: Success
        ///  </summary>
        /// <return>1</return>
        /// Author: HMDUC (23/06/2023)
        [Test]
        public async Task DeleteAsync_ValidEntity_Sucess()
        {
            //Arrange
            var id = Guid.Parse("844257ba-56d7-4bc5-892d-ceeaf0e616d5");

            var asset = new Asset()
            {
                AssetId = id
            };

            var assetRepository = Substitute.For<IAssetRepository>();
            assetRepository.GetByIdAsync(id).Returns(asset);
            assetRepository.DeleteEntityAsync(id).Returns(1);

            var mapper = Substitute.For<IMapper>();

            var assetService = new AssetService(assetRepository, mapper);

            var actualResult = await assetService.DeleteAsync(id);

            //Act & Assert
            Assert.That(actualResult, Is.EqualTo(1));
        }

        /// <summary>
        ///  Test function Get pagging,Case:return Object null
        /// </summary>
        /// <returns>
        ///  response null
        /// </returns>
        /// Author: HMDUC (23/06/2023)
        [Test]
        public async Task GetPagging_NullEntity_ReturnObjNull()
        {
            var assetRepository = Substitute.For<IAssetRepository>();
            var mapper = Substitute.For<IMapper>();

            int pageSize = 20;
            int pageNumber = 1;
            string searchInput = "TS0001";
            string m_DepartmentName = "Phòng CNTT";
            string m_CategoryName = "Máy tính xách tay";

            //response case null
            var response = new
            {
                data = (Asset)null,
                totalRecord = 0,
                totalRow = 0,
                summaryData = new
                {
                    total_quantity = 0,
                    total_cost = 0,
                    total_depreciation = 0,

                }
            };

            assetRepository.GetPagging(pageSize,pageNumber,searchInput,m_DepartmentName,m_CategoryName).Returns(response);

            var assetService = new AssetService(assetRepository,mapper);
            
            var actualResult = await assetService.GetPagging(pageSize,pageNumber,searchInput,m_DepartmentName,m_CategoryName);

            //Act & Assert
            Assert.That(actualResult, Is.EqualTo(response));

        }

        /// <summary>
        ///  Test function Get pagging,Case: return Object success
        /// </summary>
        /// <returns></returns>
        /// Author: HMDUC (23/06/2023)
        [Test]
        public async Task GetPagging_ValidEntity_ReturnObjSuccess()
        {

            //Arange
            var assetRepository = Substitute.For<IAssetRepository>();
            var mapper = Substitute.For<IMapper>();

            int pageSize = 20;
            int pageNumber = 1;
            string searchInput = "TS0001";
            string m_DepartmentName = "Phòng CNTT";
            string m_CategoryName = "Máy tính xách tay";


            //response case success
            var response = new
            {
                data = new Asset()
                {
                    AssetCode = "TS0001",
                    DepartmentName= "Phòng CNTT",
                    CategoryName= "Máy tính xách tay",
                    Quantity=20,
                    Cost=(decimal)1999.0000,
                    DepreciationRate=(float)1.1
                },
                totalRecord = 1,
                totalRow = 1,
                summaryData = new
                {
                    total_quantity = 20,
                    total_cost = 1999.0000,
                    total_depreciation = 1.1,

                }
            };


            assetRepository.GetPagging(pageSize, pageNumber, searchInput, m_DepartmentName, m_CategoryName).Returns(response);

            var assetService = new AssetService(assetRepository, mapper);

            var actualResult = await assetService.GetPagging(pageSize, pageNumber, searchInput, m_DepartmentName, m_CategoryName);

            //Act & Assert
            Assert.That(actualResult, Is.EqualTo(response));

        }

        /// <summary>
        /// Test function Insert Async
        /// </summary>
        /// <returns>Valid Exception</returns>
        /// Author: HMDUC (23/06/2023)
        [Test]
        public async Task InsertAsync_InValidEntity_ThrowValidException()
        {
            //Arange
            var assetRepository = Substitute.For<IAssetRepository>();
            var mapper = Substitute.For<IMapper>();

            var assetInsertDto = new AssetInsertDto()
            {
                AssetCode ="TS151202"
            };

            var asset = new Asset()
            {
                AssetCode = "TS151202"
            };

            var assetGetByCode = new Asset()
            {
                AssetCode = "TS151202"
            };

            mapper.Map<Asset>(assetInsertDto).Returns(asset);

            assetRepository.GetByCodeAsync(asset.AssetCode).Returns(assetGetByCode);

            var assetService = new AssetService(assetRepository, mapper);

            //Act & Assert
            Assert.ThrowsAsync<ValidateException>(async () =>
            {
                await assetService.InsertAsync(assetInsertDto);
            });
        }


        /// <summary>
        /// Test function Insert success
        /// </summary>
        /// <returns>1</returns>
        /// Author: HMDUC (23/06/2023)
        [Test]
        public async Task InsertAsync_ValidEntity_Success()
        {
            //Arange
            var assetRepository = Substitute.For<IAssetRepository>();
            var mapper = Substitute.For<IMapper>();

            var assetInsertDto = new AssetInsertDto()
            {
                AssetCode = "TS151202"
            };

            var asset = new Asset()
            {
                AssetCode = "TS151202"
            };

            var assetGetByCode = new Asset()
            {
                AssetCode = "TS151202"
            };

            mapper.Map<Asset>(assetInsertDto).Returns(asset);

            assetRepository.GetByCodeAsync(asset.AssetCode).ReturnsNull();
            assetRepository.InsertAsync(asset).Returns(1);

            var assetService = new AssetService(assetRepository, mapper);

            var actualResult = await assetService.InsertAsync(assetInsertDto);


            //Act & Assert
            Assert.That(actualResult, Is.EqualTo(1));

        }


        /// <summary>
        /// Test funcion update InValidEntity
        /// </summary>
        /// <returns> Valid Exception</returns>
        ///  Author: HMDUC (23/06/2023)
        [Test]
        public async Task UpdateAsync_InValid_ThrowException()
        {
            //Arange
            var assetRepository = Substitute.For<IAssetRepository>();
            var mapper = Substitute.For<IMapper>();

            var assetUpdateDto = new AssetUpdateDto()
            {
                AssetCode = "TS151202"
            };

            var asset = new Asset()
            {
                AssetCode = "TS151202"
            };

            var assetGetByCode = new Asset()
            {
                AssetCode = "TS151202"
            };

            mapper.Map<Asset>(assetUpdateDto).Returns(asset);

            assetRepository.GetByCodeAsync(asset.AssetCode).Returns(assetGetByCode);

            var assetService = new AssetService(assetRepository, mapper);

            //Act & Assert
            Assert.ThrowsAsync<ValidateException>(async () =>
            {
                await assetService.UpdateAsync(assetUpdateDto);
            });
        }


        /// <summary>
        /// Test function Update Async ValidEntity
        /// </summary>
        /// <returns>1</returns>
        /// Author: HMDUC (23/06/2023)
        [Test]
        public async Task UpdateAsync_ValidEntity_Success()
        {
            //Arange
            var assetRepository = Substitute.For<IAssetRepository>();
            var mapper = Substitute.For<IMapper>();

            var assetUpdateDto = new AssetUpdateDto()
            {
                AssetCode = "TS151202",
                Quantity = 20,
            };

            var asset = new Asset()
            {
                AssetCode = "TS151202",
                 Quantity = 20,
            };

            var assetGetByCode = new Asset()
            {
                AssetCode = "TS151202",
            };

            mapper.Map<Asset>(assetUpdateDto).Returns(asset);

            assetRepository.GetByCodeAsync(asset.AssetCode).ReturnsNull();
            assetRepository.InsertAsync(asset).Returns(1);

            var assetService = new AssetService(assetRepository, mapper);

            var actualResult = await assetService.UpdateAsync(assetUpdateDto);


            //Act & Assert
            Assert.That(actualResult, Is.EqualTo(1));
        }
    }

}
