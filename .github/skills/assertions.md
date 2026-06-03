Skill: assertions

Descripción

Patrones recomendados para aserciones en xUnit.

Reglas

- Usar Assert.Equal para comparar conteos y valores simples.
- Usar Assert.Contains con predicados para comprobar elementos en colecciones.
- Evitar aserciones demasiado frágiles; centrarse en comportamiento observable.

Ejemplos

```csharp
Assert.Equal(2, result.Count);
Assert.Contains(result, r => r.Tipo == "Director");
```