# Ecommerce API

REST API de e-commerce construida con ASP.NET Core 8, Clean Architecture, CQRS con MediatR y autenticación JWT. Usa SQLite como base de datos.

## Requisitos previos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Git

## Clonar el repositorio

```bash
git clone <url-del-repositorio>
cd Ecomercio
```

## Configuración

El archivo `src/Ecomercio.WebApi/appsettings.json` ya viene con valores por defecto listos para desarrollo:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=ecommerce.db"
  },
  "Jwt": {
    "Key": "mi-clave-super-secreta-de-al-menos-32-caracteres-para-jwt",
    "Issuer": "ecommerce-api",
    "Audience": "ecommerce-frontend",
    "ExpirationHours": 1
  },
  "Seed": {
    "AdminEmail": "admin@ecommerce.com",
    "AdminPassword": "Admin123!",
    "AdminName": "Administrador"
  }
}
```

> **Producción:** cambia `Jwt:Key` por una clave secreta propia de al menos 32 caracteres.

## Aplicar migraciones y ejecutar

```bash
cd src/Ecomercio.WebApi
dotnet run
```

La base de datos SQLite (`ecommerce.db`) se crea automáticamente al iniciar la app. Las migraciones se aplican solas y se siembra el usuario administrador.

La API queda disponible en:
- HTTP: `http://localhost:5022`
- HTTPS: `https://localhost:7299`
- Swagger UI: `http://localhost:5022/swagger`

## Roles

| Rol       | Descripción                              |
|-----------|------------------------------------------|
| `Admin`   | Acceso completo. Puede crear productos, categorías y ver órdenes. |
| `Customer`| Usuario registrado. Puede ver productos, categorías y crear órdenes. |

## Autenticación

### Registrar usuario (Customer)

```http
POST /api/auth/register
Content-Type: application/json

{
  "email": "usuario@ejemplo.com",
  "password": "MiPassword123!",
  "name": "Juan Perez"
}
```

Los usuarios creados con `/register` tienen rol `Customer` por defecto.

### Iniciar sesión

```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "usuario@ejemplo.com",
  "password": "MiPassword123!"
}
```

Respuesta:

```json
{
  "token": "eyJhbGci...",
  "user": {
    "id": "...",
    "email": "usuario@ejemplo.com",
    "name": "Juan Perez",
    "role": "Customer",
    "createdAt": "2026-01-01T00:00:00Z"
  }
}
```

El campo `token` es el JWT que debés usar en el header `Authorization: Bearer <token>` de todas las requests protegidas.

### Admin predefinido

Al arrancar la app por primera vez se crea automáticamente:

| Campo    | Valor                   |
|----------|-------------------------|
| Email    | `admin@ecommerce.com`   |
| Password | `Admin123!`             |
| Rol      | `Admin`                 |

### Usar el token en Swagger

1. Abrí `http://localhost:5022/swagger`
2. Hacé click en **Authorize** (candado arriba a la derecha)
3. Pegá el token (sin la palabra "Bearer", solo el token)
4. Hacé click en **Authorize** y cerrá el diálogo

## Endpoints

### Auth (público)

| Método | Ruta               | Descripción             |
|--------|--------------------|-------------------------|
| POST   | `/api/auth/register` | Registrar nuevo usuario |
| POST   | `/api/auth/login`    | Iniciar sesión          |

### Productos (requiere JWT)

| Método | Ruta                | Rol requerido | Descripción           |
|--------|---------------------|---------------|-----------------------|
| GET    | `/api/products`     | Cualquiera    | Listar productos      |
| GET    | `/api/products/{id}`| Cualquiera    | Obtener producto por ID |
| POST   | `/api/products`     | Admin         | Crear producto        |

### Categorías (requiere JWT)

| Método | Ruta                   | Rol requerido | Descripción              |
|--------|------------------------|---------------|--------------------------|
| GET    | `/api/categories`      | Cualquiera    | Listar categorías        |
| GET    | `/api/categories/{id}` | Cualquiera    | Obtener categoría por ID |
| POST   | `/api/categories`      | Admin         | Crear categoría          |

### Órdenes (requiere JWT)

| Método | Ruta              | Rol requerido | Descripción          |
|--------|-------------------|---------------|----------------------|
| GET    | `/api/orders`     | Cualquiera    | Listar órdenes       |
| GET    | `/api/orders/{id}`| Cualquiera    | Obtener orden por ID |
| POST   | `/api/orders`     | Cualquiera    | Crear orden          |

## Ejemplos de uso

### Crear categoría (Admin)

```http
POST /api/categories
Authorization: Bearer <token-admin>
Content-Type: application/json

{
  "name": "Electrónica"
}
```

### Crear producto (Admin)

```http
POST /api/products
Authorization: Bearer <token-admin>
Content-Type: application/json

{
  "name": "Notebook Lenovo",
  "description": "Intel i5, 16GB RAM, 512GB SSD",
  "price": 850.00,
  "stock": 10,
  "categoryId": "<id-de-categoria>"
}
```

### Crear orden (Customer o Admin)

```http
POST /api/orders
Authorization: Bearer <token>
Content-Type: application/json

{
  "userId": "<id-del-usuario>",
  "items": [
    {
      "productId": "<id-del-producto>",
      "quantity": 2
    }
  ]
}
```

## Migraciones

El proyecto ya incluye las migraciones generadas. Si modificás el modelo y necesitás crear una nueva migración:

```bash
cd src/Ecomercio.WebApi
dotnet ef migrations add NombreDeLaMigracion --project ../Ecomercio.Infrastructure
dotnet ef database update --project ../Ecomercio.Infrastructure
```

## Estructura del proyecto

```
src/
├── Ecomercio.Domain/          # Entidades, interfaces, excepciones de dominio
├── Ecomercio.Application/     # Casos de uso (CQRS con MediatR), DTOs, validaciones
├── Ecomercio.Infrastructure/  # DbContext, repositorios, JWT, migraciones
└── Ecomercio.WebApi/          # Controllers, middleware, Program.cs
```
