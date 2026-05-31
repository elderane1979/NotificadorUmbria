# WinForms Analysis Skill

## Objective

Analizar formularios WinForms para detectar problemas arquitectónicos.

---

## Detect

### SQL in Forms

Buscar:

```csharp
SqlConnection
SqlCommand
SqlDataAdapter
```

---

### Business Rules in Events

Buscar:

```csharp
btnSave_Click

btnDelete_Click

Form_Load
```

---

### DataTable Abuse

Buscar:

```csharp
DataTable

DataSet
```

---

## Classification

### Good

Formulario únicamente coordina acciones.

### Warning

Formulario contiene validaciones complejas.

### Critical

Formulario accede a base de datos.

---

## Output

### Findings

### Risks

### Refactoring Proposal

### Migration Steps
