Workflow: generate-and-run

Paso 1: Analizar objetivo
- Identificar la clase o método a testear.

Paso 2: Preparar entorno
- Generar/actualizar mocks en tests/.../Mocks.
- Añadir plantillas si faltan.

Paso 3: Generar tests
- Crear pruebas xUnit que cubran escenarios felices y errores.

Paso 4: Build y ejecución
- Ejecutar msbuild .\NotificadorUmbria.sln /p:Configuration=Debug
- Ejecutar tests desde VS Test Explorer o vstest.console.exe

Paso 5: Verificación
- Corregir fallos y repetir.
