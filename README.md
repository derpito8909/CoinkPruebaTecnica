<h1 align="center"> Prueba Técnica - Coink Registro de usuarios </h1>

## API REST en **.NET 8** para registrar usuarios y su ubicación (país, departamento, municipio), siguiendo:

- Arquitectura limpia (Domain, Application, Infrastructure, Presentation).
- Principios SOLID.
- Patron de diseño patrón CQRS y Repository
- Acceso a datos con **Dapper** + **PostgreSQL** usando **Stored Procedures / Functions**.
- Validación con **FluentValidation** + pipeline de **MediatR**.
- Manejo centralizado de errores con middleware.
---
<details open>
<summary>
## ✅ Pre-requisitos
</summary>
Para ejecutar la aplicacion necesita tener instalado:

- Instacia de PostgreSQL.
- Crear BD + tablas del programa con este script: 
[DB_Tables.sql](./DB_Tables.sql)
- Ejecutar los Scipts para los procedimientos almacenados con este script:
[DB_Tables.sql](./StoreProcedures.sql)

- al ejecutar la aplicacion se creará por defecto datos de ejemplo para las tablas parametricas
</details>

<details open>
<summary>
## 🚀 Ejecutar la aplicacion
</summary> <br>
para ejecutar la aplicacion:

1. Clone el repositorio:

```shell
git clone https://github.com/derpito8909/CoinkPruebaTecnica.git

```
2. Ingrese a la carpeta /CoinkPruebaTecnica e ingrese estos comandos para inicar la aplicacion

```shell
 cd CoinkPruebaTecnica
 dotnet restore
 dotnet build
 dotnet run --project src/Coink.Presentation/Coink.Presentation.csproj

```
</details>

<details open>
<summary>
Descripción de los Endpoint
</summary> <br />

## Endpoint: `POST /api/Users`

- **Método:** `POST`
- **Descripción:** creacion de un usuario
en memoria

### Parámetros de Solicitud

- `fullName` (requerido): nombre completo del usuario
- `phone` (requerido): telefono del usuario
-  `address` (requerido): direccion del usuario
-  `countryId` (requerido): id del la tabla Country
-  `departmentId` (requerido): id del la tabla Departamet
-  `municipalityId` (requerido): id del la tabla Departamet

```json
{
  "fullName": "Juan Pérez",
  "phone": "3001234567",
  "address": "Calle 123 #45-67",
  "countryId": 1,
  "departmentId": 1,
  "municipalityId": 1
}
```

### Respuesta

```json
{
  "id": 10,
  "fullName": "Juan Pérez",
  "phone": "3001234567",
  "address": "Calle 123 #45-67",
  "countryId": 1,
  "departmentId": 1,
  "municipalityId": 1,
  "createdAt": "2025-11-27T03:40:00Z"
}
```
## Ejemplo de respuest erronea

```json
{
  "status": 400,
  "message": "Validation error",
  "traceId": "0HLQ2...",
  "errors": [
    {
      "field": "FullName",
      "error": "el nombre completo es necesario"
    }
  ]
}

{
  "status": 400,
  "message": "Country not found",
  "traceId": "0HLQ2..."
}
```

## Endpoint GET /api/users/{id}

- **Método:** `GET`
- **Descripción:** Obtener un usuario por Id.

### Respuesta

```json
{
  "id": 10,
  "fullName": "Juan Pérez",
  "phone": "3001234567",
  "address": "Calle 123 #45-67",
  "countryId": 1,
  "departmentId": 1,
  "municipalityId": 1,
  "createdAt": "2025-11-27T03:40:00Z"
}
```
### REspuesta Eronea
```json
{
  "status": 404,
  "message": "Not Found",
  "traceId": "0HLQ2..."
}
```
## Endpoint GET /api/locations/countries

- **Método:** `GET`
- **Descripción:** Devuelve lista de países.

### Respuesta

```json
[
  {
    "id": 1,
    "name": "Colombia",
    "isoCode": "CO"
  },
  {
    "id": 2,
    "name": "México",
    "isoCode": "MX"
  }
]
```
## Endpoint GET /api/locations/departments?countryId={id}

- **Método:** `GET`
- **Descripción:** Devuelve departamentos de un país.

### Respuesta

```json
[
  {
    "id": 1,
    "name": "Antioquia",
    "countryId": 1
  },
  {
    "id": 2,
    "name": "Cundinamarca",
    "countryId": 1
  }
]
```
### Respuesta erronea
```json
{
  "status": 400,
  "message": "el id del departamento debe ser mayor a 0",
  "traceId": "0HLQ2..."
}
```
## Endpoint GET /api/locations/municipalities?departmentId={id}

- **Método:** `GET`
- **Descripción:** Devuelve municipios de un departamento.

### Respuesta

```json
[
  {
    "id": 1,
    "name": "Medellín",
    "departmentId": 1
  },
  {
    "id": 2,
    "name": "Bello",
    "departmentId": 1
  }
]
```
### Respuesta erronea
```json
{
  "status": 400,
  "message": "el id del muncipio debe ser mayor a 0",
  "traceId": "0HLQ2..."
}
```
</details>

## Autor

<p>Desarrollado por David Esteban Rodriguez Pineda 2025&copy;</p>
