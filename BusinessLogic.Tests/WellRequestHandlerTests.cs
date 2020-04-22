using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using DAL;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Models;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace BusinessLogic.Tests
{
    public class WellRequestHandlerTests
    {
        [Fact]
        public async Task Get_NormalConditions_CorrectResultIsReturned()
        {
            // Arrange
            var fixture = new Fixture();

            var wells = fixture
                .Build<WellModel>()
                .Without(x => x.Company)
                .Without(x => x.Workshop)
                .Without(x => x.Field)
                .CreateMany()
                .ToList();
            var wellRepositoryMock = new Mock<IWellRepository>();
            wellRepositoryMock
                .Setup(x => x.Get())
                .ReturnsAsync(wells);

            var companies = wells
                .Select(x => fixture
                    .Build<CompanyModel>()
                    .With(y => y.Id, x.CompanyId)
                    .Create())
                .ToList();
            var companyRepositoryMock = new Mock<ICompanyRepository>();
            companyRepositoryMock
                .Setup(x => x.GetByIds(It.Is<IEnumerable<int>>(
                        y => y.OrderBy(z => z)
                            .SequenceEqual(wells.Select(m => m.CompanyId).OrderBy(z => z)))))
                .ReturnsAsync(companies);
            
            var workshops = wells
                .Select(x => fixture
                    .Build<WorkshopModel>()
                    .With(y => y.Id, x.WorkshopId)
                    .Create())
                .ToList();
            var workshopRepositoryMock = new Mock<IWorkshopRepository>();
            workshopRepositoryMock
                .Setup(x => x.GetByIds(It.Is<IEnumerable<int>>(
                    y => y.OrderBy(z => z)
                        .SequenceEqual(wells.Select(m => m.WorkshopId).OrderBy(z => z)))))
                .ReturnsAsync(workshops);

            var fields = wells
                .Select(x => fixture
                    .Build<FieldModel>()
                    .With(y => y.Id, x.FieldId)
                    .Create())
                .ToList();
            var fieldRepositoryMock = new Mock<IFieldRepository>();
            fieldRepositoryMock
                .Setup(x => x.GetByIds(It.Is<IEnumerable<int>>(
                    y => y.OrderBy(z => z)
                        .SequenceEqual(wells.Select(m => m.FieldId).OrderBy(z => z)))))
                .ReturnsAsync(fields);

            var copyOfWells = JsonConvert.DeserializeObject<IEnumerable<WellModel>>(JsonConvert.SerializeObject(wells));
            var expected = copyOfWells.Select(x =>
            {
                x.Company = companies.First(y => y.Id == x.CompanyId);
                x.Workshop = workshops.First(y => y.Id == x.WorkshopId);
                x.Field = fields.First(y => y.Id == x.FieldId);
                return x;
            }).ToList();
            
            var sut = new WellRequestHandler(
                wellRepositoryMock.Object,
                companyRepositoryMock.Object,
                workshopRepositoryMock.Object,
                fieldRepositoryMock.Object);
            
            // Act
            var actual = (await sut.Get()).ToList();

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}