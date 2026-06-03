Propósito

Esta carpeta contiene descripciones de agentes, "skills", flujos de trabajo y plantillas en Markdown para automatizar la generación de pruebas unitarias en este repositorio (.NET Framework 4.8).

Estructura

- skills/: capacidades reusables para la generación de tests (mocking, assertions, build/run).
- agents/: instrucciones de alto nivel para agentes que crean o actualizan tests.
- workflow/: flujos de trabajo paso a paso para ejecutar la generación automatizada de pruebas.
- templates/: plantillas de archivos de test y mocks en formato markdown para facilitar la creación programática.

Convenciones

- Las pruebas usan xUnit y Moq.
- Mantener compatibilidad con .NET Framework 4.8.
- Cambios mínimos: preferir añadir nuevos tests y mocks en vez de refactorizar código productivo.
