---
name: test-generator
description: Generador de Test Unitarios para clases específicas, siguiendo las mejores prácticas de testing y mocking.
---

# test-generator

1. Analizar la clase objetivo y sus dependencias (interfaces, DTOs, etc.).
2. Generar fakes o mocks en tests/.../Mocks cuando falten, siguiendo "skills/mocking.md".
3. Crear tests claros con Arrange/Act/Assert; usar perfiles de AutoMapper si aplica.
4. Ejecutar msbuild para validar compilación y reportar errores.
5. Añadir verifications cuando sea relevante (Verify en Moq).

# Reglas adicionales para este repositorio

- Los tests generados deben colocarse en el proyecto Notificador.Tests dentro de una carpeta con el nombre del proyecto bajo prueba y el sufijo ".Test". Ejemplo: para Notificador.Core crear tests/Notificador.Tests/Notificador.Core.Test/...
- Usar MockFactory (tests/.../Mocks/MockFactory.cs) para recuperar dependencias por defecto: MockFactory.GetMock<T>() o MockFactory.GetMockInstance<T>().
- Preparar datos en los tests mediante la asignación de propiedades en la instancia del mock (mock.Object.Prop = valor). Evitar usar Setup/Get/SetupAllProperties directamente en los tests salvo en los casos donde se necesite control fino. Los defaults deben provenir de las clases Mock concretas en tests/.../Mocks.
- Implementar en los tests un helper GetService o CreateService que acepte parámetros opcionales para inyectar mocks/implementaciones específicas (ISettingsProvider, ICryptoService, etc.). Por defecto dicho helper deberá usar MockFactory.GetMock<T>().
- Si un test necesita sobrescribir setups o llamar a Verify con control fino, crear una nueva Moq.Mock<T>() local en el Arrange y pasarla explícitamente al helper CreateService (o usar MockFactory.GetMockInstance<T>() cuando la fábrica devuelva una Moq.Mock<T> que se pueda manipular).

# Notas y reglas relevantes (actualizadas)

- Las clases Mock deben contener un setup por defecto cuando sea apropiado (valores de retorno predecibles). Esto mantiene los Arranges de los tests simples.
- El repositorio contiene una MockFactory con mocks/fakes por defecto. No asumas que MockFactory devuelve una nueva instancia de Moq.Mock<T> por llamada: puede devolver objetos fijos o fakes concretos.
  - Si un test necesita modificar setups o llamar a Verify, crea una nueva instancia de Moq.Mock<T>() en el Arrange y pásala explícitamente al helper CreateService (o reemplaza la entrada en MockFactory si el proyecto lo soporta).
  - Alternativamente, si MockFactory almacena un Moq.Mock<T>, el test puede acceder a esa instancia concreta desde la fábrica según la API existente, pero no todas las entradas están garantizadas como Moq.Mock<T> (algunas son fakes concretos).
- No es obligatorio modificar MockFactory para cada test; crear y pasar mocks locales es la opción más clara y segura cuando se requiere control fino.

# Notas sobre SettingsProvider y tests

- Evitar depender de ConfigurationManager en tests unitarios. Preferir ISettingsProvider mockeado.
- Para probar manejo de claves (por ejemplo cuando ICryptoService.Decrypt lanza), usar un ICryptoService mock que lance excepción o ajustar ISettingsProvider.ClaveEncriptada en el Arrange.

# Recomendación de estructura CreateService en tests

- Crear un helper CreateService que acepte parámetros opcionales para inyectar implementaciones o mocks específicas (ISettingsProvider, ICryptoService, etc.).
- Por defecto, CreateService debe usar MockFactory.GetMock<T>() o fakes concretos que existan en la fábrica.
- Si el test necesita sobrescribir setups o realizar Verify, crear un Moq.Mock<T> local y pasarlo a CreateService (o configurar explícitamente la fábrica si el equipo lo prefiere).

# Formato de salida

- Archivos generados en tests/... con comentarios mínimos y nombres claros.
- Añadir un breve resumen de cambios en README.md de la carpeta .github/04-testing-qa.

