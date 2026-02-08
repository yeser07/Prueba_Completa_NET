# Prueba_Completa_NET

API RESTful desarrollada en .NET 8 para la gestión de clientes, productos y órdenes.
El proyecto implementa validaciones, manejo de transacciones, control de inventario y cálculo automático de subtotales, impuestos y totales.

Proyecto desarrollado exclusivamente para evaluación técnica.
No debe utilizarse en producción sin revisiones adicionales de seguridad, rendimiento y escalabilidad.


---
## Tecnologías utilizadas:

--.NET 8<br>
--C#<br>
--ASP.NET Core Web API <br>
--Entity Framework Core <br>
--SQL Server <br>
--Swagger <br>
--Postman (En la carpeta ColeccionPostman se encuentra el archivo json, para importar  la coleccion y probar los enpoints desde esa aplicacion) <br>
--Git <br>
--NuGet <br>

## Requisitos:

Editor de código compatible con .NET
(Visual Studio, VS Code, Rider, etc.)

.NET 8 SDK
https://dotnet.microsoft.com/download/dotnet/8.0

SQL Server instalado y en ejecución

(Opcional) SQL Server Management Studio (SSMS)

El proyecto utiliza un connection string local como ejemplo: "DefaultConnection": "Server=.;Database=OrderManagementDB;Trusted_Connection=True;TrustServerCertificate=True"

## Instalación y ejecución:
1. Clona el repositorio:
   git clone https://github.com/yeser07/Prueba_Completa_NET.git
2. Restaura los paquetes NuGet:
   dotnet restore

3.Ejecutar el script SQL ubicado en la carpeta Scripts, esto creará la base de datos y las tablas necesarias para la aplicación.
4. Configura el connection string en appsettings.json si es necesario.
5. Ejecuta la aplicación:
   dotnet run
6. Accede a Swagger UI para probar los endpoints:
	http://localhost:7000/swagger/index.html
    http://localhost:5000/swagger/index.html

## Endpoints disponibles:
- Clientes:
  - GET /api/clientes
  - GET /api/clientes/{id}
  - POST /api/clientes
  - PUT /api/clientes/{id}
- Productos:
  - GET /api/productos
  - GET /api/productos/{id}
  - POST /api/productos
  - PUT /api/productos/{id}
- Ordenes:
  - POST /api/ordenes
        

### Ejemplo de request para crear una orden

```json
{
  "ordenId": 0,
  "clienteId": 1,
  "detalle": [
    {
      "productoId": 1,
      "cantidad": 2
    },
    {
      "productoId": 3,
      "cantidad": 1
    }
  ]
} 
```

## Decisiones Técnicas
1. Si ocurre un error, la transaccion se revierte
2. Se utiliza DTOs para evitar exponer directamente las entidades.
3. La lógica de negocio se mantiene fuera del controlador.

## Autor

<strong>Yeser Sabillón</strong> <br>
Proyecto desarrollado como parte de una prueba técnica.
