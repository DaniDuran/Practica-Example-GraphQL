# Proyecto GraphQL con Arquitectura de 3 Capas

Este proyecto es una práctica que demuestra cómo utilizar GraphQL con una arquitectura de 3 capas, que incluye las siguientes capas: dominio (domain), infraestructura (infrastructure) y servicios (services) como capa de exposición GraphQL.

## Resumen del Proyecto

### Objetivo

El objetivo de este proyecto es proporcionar un ejemplo claro y práctico de cómo estructurar una aplicación GraphQL siguiendo una arquitectura de 3 capas. Cada capa tiene un propósito específico en la gestión de datos y lógica de negocio.

### Estructura del Proyecto

- **Domain (Dominio):** Contiene la lógica de negocio y las entidades del dominio. Aquí se definen los modelos y la funcionalidad principal de la aplicación.

- **Infrastructure (Infraestructura):** Encargada de interactuar con la base de datos y otros servicios externos. Contiene implementaciones concretas de la persistencia y servicios externos.

- **Services (Servicios):** Capa de exposición GraphQL. Aquí se definen los resolvers y el esquema GraphQL que expone la funcionalidad a través de la API GraphQL.


