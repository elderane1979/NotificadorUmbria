# add-sources-to-csproj.ps1
param(
  [string[]] $Projects = @("src\Notificador.Core\Notificador.Core.csproj","src\Notificador.Infrastructure\Notificador.Infrastructure.csproj")
)

foreach ($proj in $Projects) {
  if (-not (Test-Path $proj)) {
    Write-Warning "No existe: $proj"
    continue
  }

  $backup = "$proj.bak"
  Copy-Item $proj $backup -Force
  Write-Host "Backup creado: $backup"

  [xml]$doc = Get-Content $proj
  $ns = $doc.Project.NamespaceURI
  $projNode = $doc.SelectSingleNode("/Project")

  # crear ItemGroup con Compile por cada .cs en el directorio del proyecto (recursivo)
  $itemGroup = $doc.CreateElement("ItemGroup", $ns)

  $projDir = Split-Path $proj -Parent
  Get-ChildItem -Path $projDir -Filter *.cs -Recurse | ForEach-Object {
    # ruta relativa al directorio del proyecto, con separadores '\'
    $rel = $_.FullName.Substring($projDir.Length + 1) -replace "/","\\"
    $compile = $doc.CreateElement("Compile", $ns)
    $compile.SetAttribute("Include", $rel)
    $itemGroup.AppendChild($compile) | Out-Null
    Write-Host "Añadiendo $rel a $proj"
  }

  # si ya había un ItemGroup idéntico que incluya los mismos Compile, esto añadirá duplicados;
  # si prefieres evitar duplicados, revisa manualmente el .csproj tras ejecutar.
  $projNode.AppendChild($itemGroup) | Out-Null

  $doc.Save($proj)
  Write-Host "Actualizado: $proj"
}