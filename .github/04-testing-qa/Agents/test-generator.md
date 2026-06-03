
Agent: test-generator

Objetivo

Generar o actualizar tests unitarios xUnit/Moq siguiendo la arquitectura del proyecto y las reglas definidas en /04-testing-qa/skills.

Instrucciones al agente

1. Analizar la clase objetivo y sus dependencias (interfaces, DTOs, etc.).
2. Generar fakes o mocks en tests/.../Mocks cuando falten, siguiendo "skills/mocking.md".
3. Crear tests claros con Arrange/Act/Assert; usar perfiles de AutoMapper si aplica.
4. Ejecutar msbuild para validar compilación y reportar errores.
5. Añadir verifications cuando sea relevante (Verify en Moq).

Notas y reglas relevantes (actualizadas)

- Las clases Mock deben contener un setup por defecto cuando sea apropiado (valores de retorno predecibles). Esto mantiene los Arranges de los tests simples.
- El repositorio contiene una MockFactory con mocks/fakes por defecto. No asumas que MockFactory devuelve una nueva instancia de Moq.Mock<T> por llamada: puede devolver objetos fijos o fakes concretos.
  - Si un test necesita modificar setups o llamar a Verify, crea una nueva instancia de Moq.Mock<T>() en el Arrange y pásala explícitamente al helper CreateService (o reemplaza la entrada en MockFactory si el proyecto lo soporta).
  - Alternativamente, si MockFactory almacena un Moq.Mock<T>, el test puede acceder a esa instancia concreta desde la fábrica según la API existente, pero no todas las entradas están garantizadas como Moq.Mock<T> (algunas son fakes concretos).
- No es obligatorio modificar MockFactory para cada test; crear y pasar mocks locales es la opción más clara y segura cuando se requiere control fino.

Notas sobre SettingsProvider y tests

- Evitar depender de ConfigurationManager en tests unitarios. Preferir ISettingsProvider mockeado.
- Para probar manejo de claves (por ejemplo cuando ICryptoService.Decrypt lanza), usar un ICryptoService mock que lance excepción o ajustar ISettingsProvider.ClaveEncriptada en el Arrange.

Recomendación de estructura CreateService en tests

- Crear un helper CreateService que acepte parámetros opcionales para inyectar implementaciones o mocks específicas (ISettingsProvider, ICryptoService, etc.).
- Por defecto, CreateService debe usar MockFactory.GetMock<T>() o fakes concretos que existan en la fábrica.
- Si el test necesita sobrescribir setups o realizar Verify, crear un Moq.Mock<T> local y pasarlo a CreateService (o configurar explícitamente la fábrica si el equipo lo prefiere).

Formato de salida

- Archivos generados en tests/... con comentarios mínimos y nombres claros.
- Añadir un breve resumen de cambios en README.md de la carpeta .github/04-testing-qa.

