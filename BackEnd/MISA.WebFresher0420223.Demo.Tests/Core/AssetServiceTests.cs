using AutoMapper;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Core.MISAException;
using MISA.WebFresher042023.Demo.Core.Service;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
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

        [Test]

        public async Task GetByIdAsync_ValidEntity()
        {
            //Arrange 
            var id = Guid.Parse("844257ba-56d7-4bc5-892d-ceeaf0e616d5");

            var assetRepositoty = new FakeAssetRepository();
            var mapper = new FakeMapper();


            var assetService = new AssetService(assetRepositoty, mapper);

            var actuaResult = await assetService.GetByIdAsync(id);

            /// Assert
            Assert.That(assetRepositoty.ActualId, Is.EqualTo(id));

        }

        //delete test

        [Test]
        public void DeleteAsync_NulLEntity_ThrowNotFound()
        {
            //Arrange 
            var id = Guid.Parse("844257ba-56d7-4bc5-892d-ceeaf0e616d5");

            var assetRepositoty = Substitute.For< IAssetRepository > ();
            assetRepositoty.GetByIdAsync(id).ReturnsNull();
            assetRepositoty.DeleteEntityAsync(id).Returns(1);

            var mapper = Substitute.For<IMapper>();


            var assetService = new AssetService(assetRepositoty, mapper);

            ///Act && Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await assetService.GetByIdAsync(id));

        }

    }

}
