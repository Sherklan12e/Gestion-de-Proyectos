* Comenta en json 
```json
    file:settings.json vscode
    "files.associations": {
        "*.json": "jsonc"
    },
```


# Sistema de Gestión de Proyectos y Tareas

## 🚀 Descripción
Sistema web desarrollado en .NET 8 que permite la gestión de proyectos, tickets y equipos de trabajo. Facilita el seguimiento de tareas, la colaboración entre miembros del equipo y la administración de proyectos.

## ✨ Características Principales

- **Gestión de Usuarios**
  - Registro y autenticación de usuarios
  - Perfiles de usuario personalizados
  - Control de acceso basado en roles

- **Gestión de Proyectos**
  - Creación y administración de proyectos
  - Asignación de miembros al equipo
  - Seguimiento del estado del proyecto

- **Gestión de Tickets**
  - Creación y asignación de tickets
  - Estados personalizables (Abierto, En Progreso, Completado)
  - Sistema de comentarios y actividad
  - Seguimiento de tiempo (fechas de inicio y fin)

## 🛠️ Tecnologías Utilizadas

- **Backend**
  - .NET 8
  - Entity Framework Core
  - MySQL
  - Minimal APIs

- **Seguridad**
  - BCrypt para hash de contraseñas
  - Validación de datos
  - CORS configurado

## 📋 Requisitos Previos

- .NET 8 SDK
- MySQL 8.0.39 o superior
- Visual Studio 2022 o VS Code

## 🚀 Instalación

1. Clonar el repositorio
```bash
git clone https://github.com/tu-usuario/tu-repositorio.git
```

2. Configurar la cadena de conexión en `appsettings.json`
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=tu_base_de_datos;Uid=tu_usuario;Pwd=tu_contraseña;"
}
````

3. Ejecutar las migraciones
```bash
dotnet ef migrations add MigracionInicial --context GestionTareasDbContext --output-dir Persistencia/Migraciones --project Api --startup-project Api
```
4. Iniciar el proyecto
```bash
dotnet run --project Api
```

## 📝 Uso de la API

### Endpoints Principales

- **Usuarios**
  - GET `/api/usuarios` - Obtener todos los usuarios
  - POST `/api/usuario` - Crear usuario
  - PUT `/api/usuario/{id}` - Actualizar usuario
  - DELETE `/api/usuario/{id}` - Eliminar usuario

- **Proyectos**
  - GET `/api/proyectos` - Listar proyectos
  - POST `/api/proyecto` - Crear proyecto
  - POST `/api/proyectos/{idProyecto}/usuario/{idUsuario}` - Asignar usuario

- **Tickets**
  - GET `/api/tickets` - Obtener tickets
  - POST `/api/ticket` - Crear ticket
  - PUT `/api/ticket/{id}/estado` - Actualizar estado

## 👥 Contribución

1. Fork el proyecto
2. Crea tu rama de características (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT - ver el archivo [LICENSE.md](LICENSE.md) para más detalles.

## ✉️ Contacto

Sherklan - [@](https://x.com/PatrikPE1) -click

Link del Proyecto: [https://github.com/tu-usuario/Gestion-de-Proyectos](https://github.com/tu-usuario/Gestion-de-Proyectos)



## Cosas que faltann aaa
 
Que envie una salicitud al otro usuario para unirser al proyecto .

