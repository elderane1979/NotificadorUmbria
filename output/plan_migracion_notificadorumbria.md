Resumen y propuesta de modernización — NotificadorUmbria

Contexto
- Fichero analizado: NotificadorMensajesUmbria\NotificadorUmbria.cs
- Plataforma objetivo: .NET Framework 4.8, C# 7.3
- Observaciones principales: mezcla de responsabilidades (HTTP, parsing, negocio), uso de Properties.Settings y EasyCrypto directo, uso de .Result (bloqueos), clases y campos sin encapsular.

Responsabilidades identificadas
- Lógica de presentación (parsing / interpretación HTML)
  - Uso de HtmlAgilityPack para obtener nodos por id, recorrer UL/LI y extraer valores.
  - Métodos: GetMensajes, GetMensajesPrivados.

- Lógica de negocio
  - Agregación y reglas: conteo de hilos (n_hilos), suma de mensajes (n_mensajes), filtros por flags (showMensajes*).
  - Orquestación de qué mensajes incluir.

- Acceso a datos / externas
  - Llamada HTTP POST con HttpClient y MultipartFormDataContent para autenticación y obtención del HTML.
  - Uso de Properties.Settings.Default para URL, usuario, clave y flags.
  - Uso de EasyCrypto para desencriptar clave.

Problemas y riesgos
- Alta acoplamiento que dificulta pruebas unitarias y mantenimiento.
- Uso de .Result en I/O asíncrono (posible deadlock y bloqueos).
- Campos públicos y nombres que no siguen PascalCase; ausencia de interfaces.
- Manejo de excepciones insuficiente y silencioso en llamadas externas.

Arquitectura objetivo (tres capas)
1) Capa de Presentación / Aplicación (Notificador.App)
   - Responsable: composición, orquestación de ejecución y UI o servicio host.
   - Consume INotificadorService.

2) Capa de Dominio / Servicio (Notificador.Core)
   - Responsable: reglas de negocio y modelos.
   - Contiene: INotificadorService, NotificadorService, modelos POCO (Mensaje con propiedades PascalCase).

3) Capa de Infraestructura (Notificador.Infrastructure)
   - Responsable: llamadas HTTP, parsing HTML, acceso a settings y crypto.
   - Componentes: IUmbriaClient (HTTP), IHtmlParser (HtmlAgilityPack wrapper), ISettingsProvider, ICryptoService.

Estructura de proyectos sugerida (mantener net48)
- src/Notificador.App (exe/host)
- src/Notificador.Core (lógica y contratos)
- src/Notificador.Infrastructure (implementaciones externas)
- tests/Notificador.Tests (unitarios)

Plan de migración incremental (acciones y validación)
1) Preparación y baseline
   - Compilar solución (msbuild .\NotificadorUmbria.sln /p:Configuration=Debug).
   - Registrar errores/warnings.
   - Validación: build reproducible.

2) Crear Notificador.Core (modelos e interfaces)
   - Mensaje.cs con propiedades públicas en PascalCase.
   - INotificadorService con GetNovedadesAsync().
   - Validación: compilar sin cambiar comportamiento.

3) Introducir ISettingsProvider e ICryptoService
   - Implementación que envuelve Properties.Settings y EasyCrypto.
   - Validación: compilar y mocks en tests.

4) Extraer IUmbriaClient (HTTP)
   - Implementación async que usa HttpClient, evita .Result y maneja timeouts/excepciones.
   - Validación: compilar y probar flujo de obtención HTML.

5) Extraer IHtmlParser (parsing)
   - Implementación UmbriaHtmlParser con HtmlAgilityPack; devolver DTOs específicos.
   - Añadir pruebas unitarias con fixtures HTML.

6) Implementar NotificadorService (core)
   - Orquestación: usar ISettingsProvider, ICryptoService, IUmbriaClient, IHtmlParser.
   - Aplicar filtros y agregaciones; devolver IEnumerable<Mensaje>.
   - Validación: tests unitarios que verifiquen agregación y reglas.

7) Reemplazar la clase monolítica por fachada/adapter
   - Mantener compatibilidad binaria si otros componentes la usan; marcar Obsolete y planificar eliminación.
   - Validación: consumidores existentes no fallan.

8) Pruebas de integración y CI local
   - Tests de integración opcionales con HTTP mock o entorno de prueba.
   - Añadir script de build (msbuild) en README.

9) Limpieza y cumplimiento de coding standards
   - Renombrar campos a _prefijo para privados; propiedades en PascalCase.
   - Añadir logging consistente según patrón existente; no introducir librerías sin actualizar .csproj.
   - Validación final: build limpio, tests unitarios pasando, warnings resueltos.

Artefactos a entregar por paso
- Notificador.Core: Models/Mensaje.cs, Interfaces/INotificadorService.cs
- Notificador.Infrastructure: UmbriaHttpClient, UmbriaHtmlParser, SettingsProvider, CryptoService
- Notificador.App: Program/host que consume INotificadorService
- Notificador.Tests: unitarios para Core y Parser

Reglas a respetar
- Mantener TargetFramework net48.
- No introducir dependencias runtime sin actualizar csproj y validar compatibilidad.
- PascalCase para tipos y propiedades; prefijo _ para campos privados.
- Evitar .Result; usar async/await y manejar excepciones.
- Tratar warnings como errores al finalizar cada paso.

Siguiente paso propuesto
- Opción A: ejecuto Step 1 (compilar y devolver salida msbuild). 
- Opción B: creo el esqueleto de Notificador.Core con Mensaje e INotificadorService.

Indicar la opción a ejecutar.