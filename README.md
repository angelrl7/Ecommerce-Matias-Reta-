# Ecomercio API

API REST para un sistema de ecommerce construida con ASP.NET Core 8, Clean Architecture, CQRS y autenticación JWT. Incluye gestión de productos, categorías, órdenes y usuarios con control de acceso basado en roles.

---

## Requisitos previos

- [.NET 8 SDK](https://dotnet.microsoft.com/es-es/download/dotnet/8.0)
- [Git](https://git-scm.com/)
- Editor recomendado: [Visual Studio Code](https://code.visualstudio.com/) o Visual Studio 2022

Verificá que .NET esté instalado:

```bash
dotnet --version
# Debe mostrar 8.0.x
```

---

## Pasos para usar el proyecto

### 1. Clonar el repositorio

```bash
git clone https://github.com/angelrl7/Ecomercio.git
cd Ecomercio
```

### 2. Restaurar dependencias

```bash
dotnet restore
```

### 3. Revisar la configuración

El archivo `src/Ecomercio.WebApi/appsettings.json` ya viene configurado para correr localmente con SQLite. No necesitás cambiar nada para probarlo.

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
  }
}
```

> En producción cambia `Jwt:Key` por una clave larga y aleatoria.

### 4. Correr la aplicación

```bash
dotnet run --project src/Ecomercio.WebApi
```

La primera vez que corre, la aplicación:
- Crea la base de datos SQLite (`ecommerce.db`) automáticamente
- Aplica todas las migraciones
- Crea el usuario administrador por defecto (ver credenciales más abajo)

Una vez que arranca vas a ver:

```
Now listening on: http://localhost:5000
Now listening on: https://localhost:5001
```

### 5. Acceder a Swagger

```
https://localhost:5001/swagger
```

Desde ahí podés explorar y probar todos los endpoints.

---

## Migraciones

Las migraciones están en `src/Ecomercio.Infrastructure/Migrations/` y se aplican automáticamente al iniciar la aplicación.

Si querés aplicarlas manualmente:

```bash
dotnet ef database update --project src/Ecomercio.Infrastructure --startup-project src/Ecomercio.WebApi
```

Para crear una nueva migración:

```bash
dotnet ef migrations add NombreMigracion --project src/Ecomercio.Infrastructure --startup-project src/Ecomercio.WebApi
```

Migraciones existentes:
| Nombre | Descripción |
|--------|-------------|
| `InitialCreate` | Tablas de Products y Categories |
| `AddUsersAndOrders` | Tablas de Users, Orders y OrderItems |
| `SyncUserFullName` | Columna Role con valor por defecto "Customer" |

---

## Roles de usuario

El sistema tiene dos roles:

| Rol | Descripción |
|-----|-------------|
| `Admin` | Puede crear productos, categorías y acceder a todo |
| `Customer` | Puede consultar productos, categorías y crear órdenes |

### Usuario Admin por defecto

Al iniciar la aplicación por primera vez se crea automáticamente un usuario administrador:

| Campo | Valor |
|-------|-------|
| Email | `admin@ecomercio.com` |
| Contraseña | `Admin123!` |
| Rol | `Admin` |

### Registro de nuevos usuarios

Los usuarios que se registran a través de `/api/auth/register` siempre reciben el rol `Customer`.

---

## Autenticación con JWT

### 1. Registrarse (rol Customer)

```http
POST /api/auth/register
Content-Type: application/json

{
  "email": "usuario@ejemplo.com",
  "password": "MiPassword123",
  "name": "Juan Perez"
}
```

### 2. Iniciar sesión

```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "admin@ecomercio.com",
  "password": "Admin123!"
}
```

Respuesta:
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "user": {
    "id": "...",
    "email": "admin@ecomercio.com",
    "fullName": "Administrador",
    "role": "Admin"
  }
}
```

### 3. Usar el token en Swagger

1. Copiá el valor del campo `token` de la respuesta del login
2. En Swagger, hacé click en el botón **Authorize** (candado arriba a la derecha)
3. En el campo escribí: `Bearer <tu_token>`
4. Hacé click en **Authorize**

A partir de ahí todos los endpoints protegidos van a funcionar con ese token.

### 4. Usar el token en cualquier request

Agregá el header:

```
Authorization: Bearer <tu_token>
```

---

## Endpoints y permisos

### Auth (sin autenticación)

| Método | Ruta | Descripción |
|--------|------|-------------|
| POST | `/api/auth/register` | Registrar nuevo usuario (rol Customer) |
| POST | `/api/auth/login` | Iniciar sesión y obtener token JWT |

### Products (requiere autenticación)

| Método | Ruta | Rol requerido | Descripción |
|--------|------|---------------|-------------|
| GET | `/api/products` | Cualquier usuario | Listar todos los productos |
| GET | `/api/products/{id}` | Cualquier usuario | Obtener producto por ID |
| POST | `/api/products` | **Admin** | Crear nuevo producto |

### Categories (requiere autenticación)

| Método | Ruta | Rol requerido | Descripción |
|--------|------|---------------|-------------|
| GET | `/api/categories` | Cualquier usuario | Listar todas las categorías |
| GET | `/api/categories/{id}` | Cualquier usuario | Obtener categoría por ID |
| POST | `/api/categories` | **Admin** | Crear nueva categoría |

### Orders (requiere autenticación)

| Método | Ruta | Rol requerido | Descripción |
|--------|------|---------------|-------------|
| GET | `/api/orders` | Cualquier usuario | Listar todas las órdenes |
| GET | `/api/orders/{id}` | Cualquier usuario | Obtener orden por ID |
| POST | `/api/orders` | Cualquier usuario | Crear nueva orden |

---

## Estructura del proyecto

```
src/
├── Ecomercio.Domain/          # Entidades, interfaces y excepciones de dominio
├── Ecomercio.Application/     # Casos de uso (CQRS con MediatR), DTOs, validaciones
├── Ecomercio.Infrastructure/  # EF Core, repositorios, migraciones, JWT
└── Ecomercio.WebApi/          # Controllers, middlewares, configuración del servidor
```

---

## Posibles problemas

**"No se encuentra dotnet"** → Instalá el SDK de .NET 8 y reiniciá la terminal.

**El puerto ya está en uso** → Cambiá el puerto en `src/Ecomercio.WebApi/Properties/launchSettings.json`.

**Error de certificado HTTPS** → Ejecutá:
```bash
dotnet dev-certs https --trust
```

**401 Unauthorized en Swagger** → Asegurate de haber hecho click en Authorize en Swagger y pegado el token con el prefijo `Bearer `.

**403 Forbidden** → El endpoint requiere rol `Admin`. Hacé login con `admin@ecomercio.com` / `Admin123!`.
