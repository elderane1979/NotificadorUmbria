# Dependency Analysis Skill

## Objective

Identificar dependencias innecesarias y acoplamientos entre componentes.

---

## Analyse

Buscar:

* new dentro de servicios.
* referencias cruzadas.
* dependencias estáticas.
* acceso directo a base de datos.

---

## Severity

### High

UI → Database

### Medium

Service → Concrete Repository

### Low

Helper Classes

---

## Recommendations

Utilizar:

* Interfaces
* Dependency Injection
* Repository Pattern

---

## Example

Incorrect:

```csharp
public class CustomerService
{
    CustomerRepository repository =
        new CustomerRepository();
}
```

Correct:

```csharp
public class CustomerService
{
    private readonly ICustomerRepository _repository;
}
```
