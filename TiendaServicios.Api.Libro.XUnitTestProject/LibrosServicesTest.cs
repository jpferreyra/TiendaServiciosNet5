using AutoMapper;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using TiendaServicios.Api.Libro.Aplication;
using TiendaServicios.Api.Libro.Models;
using TiendaServicios.Api.Libro.Persistence;
using Xunit;

namespace TiendaServicios.Api.Libro.XUnitTestProject
{
    public class LibrosServicesTest
    {
        private IEnumerable<LibreriaMaterial> GetTestData()
        {
            A.Configure<LibreriaMaterial>()
                .Fill(x => x.Titulo).AsArticleTitle()
                .Fill(x => x.LibreriaMaterialId, () => { return Guid.NewGuid(); });

            var lista = A.ListOf<LibreriaMaterial>(50);

            lista[0].LibreriaMaterialId = Guid.Empty; //para poder usar getById 0
            return lista;
        }

        private Mock<LibroContext> CreateContext()
        {
            var data = GetTestData().AsQueryable();

            var dbSet = new Mock<DbSet<LibreriaMaterial>>();

            //configuración Entidad EF
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(data.Provider);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Expression).Returns(data.Expression);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.ElementType).Returns(data.ElementType);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            dbSet.As<IAsyncEnumerable<LibreriaMaterial>>()
                .Setup(x => x.GetAsyncEnumerator(new System.Threading.CancellationToken()))
                .Returns(new AsyncEnumerator<LibreriaMaterial>(data.GetEnumerator()));

            //esto es para poder filtrar por librId
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(new AsyncQueryProvider<LibreriaMaterial>(data.Provider));

            var context = new Mock<LibroContext>();

            context.Setup(x => x.LibreriaMaterial).Returns(dbSet.Object);

            return context;
        }

        [Fact]
        public async  void GetLibros()
        {


            //1. Emular Context (Moq) //new Mock<LibroContext>();
            var mockContext = CreateContext(); 

            //2. Emular Imapper  //new Mock<IMapper>();
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });
               
            var mockMapper = mapConfig.CreateMapper(); 

            //3. Instanciar clase GetAllLibroQuery y pasarles los mocks creados
            GetAllLibroQuery.Handler handler = new GetAllLibroQuery.Handler(mockContext.Object, mockMapper);

            //4. Instalar Genfu
            //5. Mapper

            GetAllLibroQuery.Execute execute = new GetAllLibroQuery.Execute();

            var lista = await handler.Handle(execute,new System.Threading.CancellationToken());

            Assert.NotNull(lista);

        }


        [Fact]
        public async void GetLibroById()
        {
            var mockContext = CreateContext();

            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });

            var mockMapper = mapConfig.CreateMapper();
            
            var request = new GetLibroByIdQuery.Execute();
            request.LibroId = Guid.Empty;


            GetLibroByIdQuery.Handler handler = new GetLibroByIdQuery.Handler(mockMapper, mockContext.Object);

            var libro = await handler.Handle(request, new System.Threading.CancellationToken());

            Assert.NotNull(libro);
            Assert.True(libro.LibreriaMaterialId == Guid.Empty);
        }

        [Fact]
        public async void CreateLibro()
        {
            //System.Diagnostics.Debugger.Launch();

            //conf bdd en memoria
            var options = new DbContextOptionsBuilder<LibroContext>()
                .UseInMemoryDatabase(databaseName: "bddlibro")
                .Options;

            
            //context
            var context = new LibroContext(options);

            var request = new CreateLibroCommand.Execute();

            request.Titulo = "Test in Microservices";
            request.AutorLibro = Guid.Empty;
            request.FechaPublicacion = DateTime.Now;

            var handler = new CreateLibroCommand.Handler(context);

            var libro = await handler.Handle(request, new System.Threading.CancellationToken());

            Assert.True(libro != null) ;

        }
    }
}
