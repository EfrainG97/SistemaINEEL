# SistemaINEEL

SistemaINEEL es una solución de software empresarial desarrollada en .NET 8 que implementa una arquitectura limpia de múltiples capas (N-Tier). El sistema está compuesto por una API RESTful para el manejo de datos y una aplicación cliente web basada en ASP.NET Core MVC, permitiendo la gestión segura de usuarios, roles, auditorías y reportes.

## Arquitectura del Proyecto

La solución está dividida en cuatro proyectos principales para garantizar la separación de responsabilidades y la mantenibilidad del código:

1. **SistemaAPI**: Backend desarrollado en ASP.NET Core Web API. Expone los endpoints REST para la interacción de datos y utiliza Entity Framework Core para la persistencia.
2. **SistemaINEEL (MVC)**: Frontend desarrollado en ASP.NET Core MVC. Actúa como el cliente de la API, maneja la interfaz de usuario, el enrutamiento y la autenticación por sesiones.
3. **ServiciosAPI**: Biblioteca de clases que actúa como capa intermedia. Define las interfaces e implementa los servicios responsables de consumir los endpoints expuestos por la API mediante `IHttpClientFactory`.
4. **LibreriaModelos**: Biblioteca de clases compartida que contiene las entidades de dominio centrales del sistema (Usuario, Rol, Sistema, Auditoria, Reporte, Consecutivo).

## Tecnologías Utilizadas

* **Framework:** .NET 8.0
* **Backend:** ASP.NET Core Web API, Entity Framework Core (Code-First)
* **Frontend:** ASP.NET Core MVC
* **Base de Datos:** SQLite
* **Documentación de API:** Swagger / OpenAPI (`Swashbuckle.AspNetCore`)
* **Procesamiento de Datos:** ExcelMapper (para lectura/escritura de archivos Excel)
* **Patrones de Diseño:** MVC (Model-View-Controller), Inyección de Dependencias (DI), Repositorio (vía EF Core).

## Características Principales

* **Consumo de API Seguro:** Integración centralizada de la API utilizando tipado fuerte y manejo de ciclo de vida óptimo a través de dependencias en `Program.cs`.
* **Gestión de Identidad y Acceso:** Autenticación de usuarios mediante estado de sesión (Session State) y separación de acceso por roles.
* **Mapeo de Datos:** Exportación e importación de registros mediante hojas de cálculo Excel.
* **Trazabilidad:** Módulo integrado para el registro de auditorías y generación de reportes operativos.

## Requisitos Previos e Instalación

Para ejecutar este proyecto de manera local, es necesario contar con lo siguiente:

1. Instalar [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).
2. Clonar el repositorio:
   ```bash
   git clone https://github.com/EfrainG97/SistemaINEEL.git
