Plantilla: mock-moq

Ejemplo de clase mock reutilizable:

```csharp
internal class ServiceMock : Moq.Mock<IService>
{
	public ServiceMock()
	{
		SetupDefaults();
	}

	private void SetupDefaults()
	{
		this.Setup(m => m.Method(It.IsAny<string>())).Returns("value");
	}
}
```
