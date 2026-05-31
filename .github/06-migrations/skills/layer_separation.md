# Layer Separation Skill

## Objective

Identificar responsabilidades mezcladas dentro de una clase y proponer su separación en capas.

---

## When To Use

Utilizar cuando:

* Exista una clase grande.
* Existan formularios WinForms.
* Existan servicios con demasiadas responsabilidades.
* Existan accesos a datos mezclados con lógica de negocio.

---

## Layers

### Presentation

Responsabilidades:

* Mostrar datos.
* Recoger datos del usuario.
* Navegación.
* Validaciones visuales.

---

### Business

Responsabilidades:

* Reglas de negocio.
* Validaciones funcionales.
* Procesos.
* Casos de uso.

---

### Data Access

Responsabilidades:

* SQL.
* Entity Framework.
* Procedimientos almacenados.
* APIs externas de persistencia.

---

## Analysis Process

Para cada método:

1. Identificar acceso a datos.
2. Identificar reglas de negocio.
3. Identificar código de UI.
4. Clasificar cada bloque.
5. Proponer extracción.

---

## Decision Rules

Si contiene SQL:

→ Data Access

Si contiene cálculos de negocio:

→ Business

Si contiene MessageBox:

→ Presentation

Si contiene controles WinForms:

→ Presentation

---

## Example

Incorrect:

```csharp
private void btnSave_Click(...)
{
    if(txtName.Text == "")
        return;

    SqlCommand cmd = ...

    cmd.ExecuteNonQuery();

    MessageBox.Show("Saved");
}
```

Correct:

Presentation:

```csharp
_customerService.CreateCustomer(customer);
```

Business:

```csharp
ValidateCustomer(customer);
```

DataAccess:

```csharp
_customerRepository.Insert(customer);
```
