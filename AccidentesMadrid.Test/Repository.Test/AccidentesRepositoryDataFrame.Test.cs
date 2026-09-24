using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using AccidentesMadrid.Mappers;
using AccidentesMadrid.Models;
using Microsoft.Data.Analysis;

namespace AccidentesMadrid.Repository.Tests
{
    [TestFixture]
    public class AccidentesRepositoryDataFrameTests
    {
        private DataFrame _df1;
        private DataFrame _df2;
        private DataFrame _df3;

        [SetUp]
        public void Setup()
        {
            _df1 = CreateFakeDataFrame();
            _df2 = CreateFakeDataFrame();
            _df3 = CreateFakeDataFrame();
        }

        private static DataFrame CreateFakeDataFrame()
        {
            var col1 = new StringDataFrameColumn("num_expediente", new[] { "2025S000056" });
            var col2 = new Int32DataFrameColumn("cod_distrito", new[] { 3 });

            return new DataFrame(col1, col2);
        }

        [Test]
        public async Task Create_LoadsAllDataFrames()
        {
            // Arrange
            var mapperMock = new Mock<IMapper<Accidentes>>();

            mapperMock.Setup(m => m.CsvToDT("300228-1-accidentes-trafico-detalle-csv.csv"))
                      .ReturnsAsync(_df1);

            mapperMock.Setup(m => m.CsvToDT("300228-2-accidentes-trafico-detalle-csv.csv"))
                      .ReturnsAsync(_df2);

            mapperMock.Setup(m => m.CsvToDT("300228-34-accidentes-trafico-detalle.csv"))
                      .ReturnsAsync(_df3);

            var repo = new AccidentesRepositoryDataFrame(mapperMock.Object);

            // Act
            await repo.Create();
            var result = await repo.GetAll();

            // Assert
            Assert.AreEqual(3, result.Rows.Count);

            mapperMock.Verify(m => m.CsvToDT(It.IsAny<string>()), Times.Exactly(3));
        }

        [Test]
        public async Task GetAll_ReturnsDataFrame()
        {
            var mapperMock = new Mock<IMapper<Accidentes>>();

            mapperMock.Setup(m => m.CsvToDT(It.IsAny<string>()))
                      .ReturnsAsync(_df1);

            var repo = new AccidentesRepositoryDataFrame(mapperMock.Object);

            await repo.Create();
            var result = await repo.GetAll();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<DataFrame>(result);
        }

        [Test]
        public async Task Create_AppendsRowsCorrectly()
        {
            var mapperMock = new Mock<IMapper<Accidentes>>();

            mapperMock.SetupSequence(m => m.CsvToDT(It.IsAny<string>()))
                      .ReturnsAsync(_df1)
                      .ReturnsAsync(_df2)
                      .ReturnsAsync(_df3);

            var repo = new AccidentesRepositoryDataFrame(mapperMock.Object);

            await repo.Create();
            var result = await repo.GetAll();

            Assert.AreEqual("2025S000056", result.Rows[0]["num_expediente"]);
            Assert.AreEqual("2025S000056", result.Rows[1]["num_expediente"]);
            Assert.AreEqual("2025S000056", result.Rows[2]["num_expediente"]);
        }

        [Test]
        public async Task Create_DoesNotFailWithEmptyDataFrame()
        {
            var emptyDf = new DataFrame();

            var mapperMock = new Mock<IMapper<Accidentes>>();

            mapperMock.Setup(m => m.CsvToDT(It.IsAny<string>()))
                      .ReturnsAsync(emptyDf);

            var repo = new AccidentesRepositoryDataFrame(mapperMock.Object);

            Assert.DoesNotThrowAsync(async () => await repo.Create());
        }
    }
}
