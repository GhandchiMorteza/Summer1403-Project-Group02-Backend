using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using mohaymen_codestar_Team02.Data;
using mohaymen_codestar_Team02.Models.VertexEAV;
using mohaymen_codestar_Team02.Services;

namespace mohaymen_codestar_Team02_XUnitTest.Servies;

public class VertexServiceTest2
{
    private IServiceProvider _serviceProvider;
    private VertexService _sut;

    public VertexServiceTest2()
    {
        var serviceCollection = new ServiceCollection();

        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        serviceCollection.AddScoped(_ => new DataContext(options));

        _serviceProvider = serviceCollection.BuildServiceProvider();
        _sut = new VertexService(_serviceProvider);
    }

    [Fact]
    public void GetVertexDetails_ReturnsCorrectDetails()
    {
        //Arange
        var objectId1 = "object1";
        var objectId2 = "object2";
        using var scope = _serviceProvider.CreateScope();
        var mockContext = scope.ServiceProvider.GetRequiredService<DataContext>();

        var att1 = new VertexAttribute("att1", 1);
        var att2 = new VertexAttribute("att2", 2);
        att1.Id = 1;
        att2.Id = 2;

        List<VertexAttribute> att = new List<VertexAttribute>() { att1, att2 };

        foreach (var attribute in att)
        {
            mockContext.Add(attribute);
        }

        var val1 = new VertexValue("val1", 1, objectId2);
        var val2 = new VertexValue("val2", 1, objectId1);
        var val3 = new VertexValue("val3", 2, objectId1);
        var val4 = new VertexValue("val4", 2, objectId2);
        List<VertexValue> vertexValues = new List<VertexValue>() { val1, val2, val3, val4 };
        foreach (var value in vertexValues)
        {
            mockContext.Add(value);
        }

        mockContext.SaveChanges();
        var expected = new Dictionary<string, string>();
        expected[att1.Name] = val2.StringValue;
        expected[att2.Name] = val3.StringValue;
        //Action
        var result = _sut.GetVertexDetails(objectId1);
        //assert
        Assert.Equal(result.AttributeValue, expected);
    }
}