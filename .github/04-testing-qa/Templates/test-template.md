Plantilla: test-xunit-moq

Encabezado

- using Moq;
- using Xunit;
- using AutoMapper; (si aplica)

Estructura

Arrange: Crear mocks y SUT
Act: Ejecutar el método bajo prueba
Assert: Comprobar resultados y/o Verify en mocks

Ejemplo

```csharp
[Fact]
public async Task Method_WhenCondition_ReturnsExpected()
{
	// Arrange
	var mock = new Moq.Mock<IService>();
	mock.Setup(m => m.Do(It.IsAny<string>())).Returns("ok");
	var sut = new MyService(mock.Object);

	// Act
	var result = await sut.MethodAsync();

	// Assert
	Assert.Equal("ok", result);
}
```
