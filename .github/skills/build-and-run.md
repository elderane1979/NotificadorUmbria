Skill: build-and-run

Descripción

Pasos y comandos para compilar y ejecutar tests en este repositorio.

Comandos

- Compilar solución en PowerShell:
	msbuild .\NotificadorUmbria.sln /p:Configuration=Debug

- Ejecutar tests en PowerShell (VSTest):
  & "C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" "tests\Notificador.Tests\bin\Debug\Notificador.Tests.dll"

- Ejecutar tests con vstest.console.exe o desde Visual Studio Test Explorer.
