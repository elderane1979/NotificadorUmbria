---
applyTo: "**"
---
# Project general coding standards

Este archivo contiene reglas específicas y contextuales para agentes IA que trabajan en este repositorio (.NET Framework 4.8).

## Naming Conventions
- Usa PascalCase para tipos y miembros públicos.
- Usa el guión bajo (_) como prefijo para miembros privados de una clase (ej. _logger).
- Usa TODO_EN_MAYUSCULAS para constantes.

## Error Handling
- Usa bloques try/catch para operaciones asíncronas y registra o propaga errores según el patrón existente en el proyecto.

## Visión general del repositorio
- Plataforma objetivo: .NET Framework 4.8 (proyectos .csproj en formato clásico). No introducir APIs de .NET Core/5/6+ sin validación.
- Componente central: NotificadorMensajesUmbria (fichero principal: NotificadorMensajesUmbria\NotificadorUmbria.cs). Revisar ese archivo para entender la lógica de creación y envío de notificaciones.

## Arquitectura y patrones observables
- Separación entre construcción del mensaje y transporte: mantener o introducir adaptadores/servicios que aíslen llamadas externas.
- Evitar dependencias runtime nuevas sin actualizar los .csproj y validar compatibilidad con .NET Framework 4.8.

## Archivos y directorios clave
- NotificadorMensajesUmbria\NotificadorUmbria.cs — punto de entrada para la lógica de notificación.
- *.sln y *.csproj — revisar Target Framework y referencias NuGet.

## Build / testing / depuración
- IDE recomendado: Visual Studio 2026 (usar Build Solution y Test Explorer).
- Línea de comandos (PowerShell): msbuild .\<SolutionName>.sln /p:Configuration=Debug. Ejemplo desde la raíz del repo:
  msbuild .\NotificadorUmbria.sln /p:Configuration=Debug
- Si hay tests, ejecutar con vstest.console.exe o desde Test Explorer en la IDE.

## Reglas al modificar código
- Hacer cambios mínimos y mantener compatibilidad con .NET Framework 4.8.
- Respetar las convenciones de nombres y el prefijo _ para campos privados.
- Añadir try/catch alrededor de operaciones asíncronas y registrar errores según el patrón existente.

## Comportamiento esperado de agentes IA
- Antes de editar: leer NotificadorMensajesUmbria\NotificadorUmbria.cs y cualquier .csproj afectado.
- Ejecutar msbuild tras cambios para validar compilación y reportar errores (mostrar salida relevante).
- No reemplazar el sistema de logging ni introducir nuevas librerías sin actualizar los proyectos.

Si falta información útil (por ejemplo .sln, csproj o archivos de configuración), solicita esos archivos antes de proponer cambios significativos.
