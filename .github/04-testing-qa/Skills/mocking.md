Skill: mocking

Descripción

Reglas y snippets para crear mocks reutilizables con Moq en la carpeta tests/.../Mocks.

Directrices

- Implementar la lógica de Setup por defecto dentro de la clase Mock concreta (p. ej. HtmlParserMock, SettingsProviderMock).
- Cada clase Mock debe heredar de Moq.Mock<T> y aplicar sus Setup en el constructor para que los tests puedan obtener objetos ya configurados.
- MockFactory debe devolver una nueva instancia de la clase Mock por llamada (no singletons) para evitar contaminación entre tests y permitir ejecución en paralelo.
- Exponer dos accesos desde MockFactory:
  - GetMockInstance<T>() -> Moq.Mock<T> (para Setup/Verify desde el test)
  - GetMock<T>() -> T (para inyección directa en el SUT, devuelve mock.Object)

Buenas prácticas

- Mantener los setups por defecto centrados en escenarios "felices"; los tests que necesiten comportamiento distinto deben recuperar la Mock<T> y sobrescribir los setups.
- Nombrar las clases mock con sufijo Mock y colocarlas en tests/.../Mocks.

Snippets

Crear mock básico con clase reusable:

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

// Uso desde tests:
var service = MockFactory.GetMock<IService>(); // devuelve service (mock.Object)
var serviceMock = MockFactory.GetMockInstance<IService>(); // devuelve Moq.Mock<IService> para Setup/Verify
```
