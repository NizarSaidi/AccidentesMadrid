using NUnit.Framework;
using Moq;
using AccidentesMadrid.Mappers;
using AccidentesMadrid.Models;

namespace AccidentesMadrid.Repository.Tests
{
    [TestFixture]
    public class AccidentesRepositoryMoqTests
    {
        private List<Accidentes> _fakeAccidentes;

        [SetUp]
        public void Setup()
        {
            _fakeAccidentes = new List<Accidentes>
            {
                new Accidentes(
                    NumExpediente: "2025S000056",
                    Fecha: new DateTime(2025, 1, 1),
                    Hora: new TimeOnly(0, 49),
                    Localizacion: "CALLE A",
                    Numero: 1,
                    CodDistrito: 3,
                    Distrito: "Centro",
                    TipoAccidente: TipoAccidente.ColisionDoble,
                    EstadoMeteorológico: null,
                    TipoVehiculo: "Coche",
                    TipoPersona: "Pasajero",
                    RangoEdad: "30-39",
                    Sexo: TipoSexo.Hombre,
                    CodLesividad: 1,
                    Lesividad: "Lesión leve",
                    CoordenadaXUtm: 123.45,
                    CoordenadaYUtm: 678.90,
                    PositivaAlcohol: 1,
                    PositivaDroga: false
                )
            };
        }

        private async IAsyncEnumerable<Accidentes> FakeAsyncEnumerable()
        {
            foreach (var a in _fakeAccidentes)
            {
                yield return a;
                await Task.Yield();
            }
        }

        [Test]
        public async Task Create_LoadsDataFromAllCsvFiles()
        {
            // Arrange
            var mapperMock = new Mock<IMapper<Accidentes>>();

            mapperMock
                .Setup(m => m.CsvToModel(It.IsAny<string>()))
                .Returns(FakeAsyncEnumerable());

            var repo = new AccidentesRepository(mapperMock.Object);

            // Act
            await repo.Create();
            var result = await repo.GetAll();

            // Assert
            Assert.AreEqual(3, result.Count); 
            // 1 accidente * 3 ficheros mockeados

            mapperMock.Verify(m => m.CsvToModel("300228-1-accidentes-trafico-detalle-csv.csv"), Times.Once);
            mapperMock.Verify(m => m.CsvToModel("300228-2-accidentes-trafico-detalle-csv.csv"), Times.Once);
            mapperMock.Verify(m => m.CsvToModel("300228-34-accidentes-trafico-detalle.csv"), Times.Once);
        }

        [Test]
        public async Task GetAll_ReturnsAccidents()
        {
            var mapperMock = new Mock<IMapper<Accidentes>>();
            mapperMock.Setup(m => m.CsvToModel(It.IsAny<string>()))
                      .Returns(FakeAsyncEnumerable());

            var repo = new AccidentesRepository(mapperMock.Object);

            await repo.Create();
            var result = await repo.GetAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [Test]
        public async Task Create_ParsesAccidentCorrectly()
        {
            var mapperMock = new Mock<IMapper<Accidentes>>();
            mapperMock.Setup(m => m.CsvToModel(It.IsAny<string>()))
                      .Returns(FakeAsyncEnumerable());

            var repo = new AccidentesRepository(mapperMock.Object);

            await repo.Create();
            var result = await repo.GetAll();

            var acc = result[0];

            Assert.AreEqual("2025S000056", acc.NumExpediente);
            Assert.AreEqual("Centro", acc.Distrito);
            Assert.AreEqual(TipoAccidente.ColisionDoble, acc.TipoAccidente);
            Assert.AreEqual(1, acc.PositivaAlcohol);
            Assert.AreEqual(false, acc.PositivaDroga);
        }
    }
}
