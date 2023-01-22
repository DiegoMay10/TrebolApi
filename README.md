# TrebolApi
Proyecto api restful para prueba técnica

Para poder ejecutar este proyecto se requiere realizar los siguientes paso:
1. clonar el repositorio del proyecto al VS2022
2. luego de haber clonado el proyecto se requiere abrir el archivo llamado appsettings.json y cambiar la cadena de conexion de sql server local proveniente de la clave "TrebolAcademyContext".
3. luego de haber ingresado la cadena de conexion se debe abrir la consola del administrador de paquetes en la ruta Herramientas> Administrador de paquetes Nuget> Consola de administrador de paquetes.
4. ingresar en la consola el comando "update-database" (sin las comillas) para poder realiazar la migracion de la base de datos y generar automaticamenta la BD y tablas necesarias para poder ejecutar el proyecto.
5. cuando ya se haya realizado los pasos anteriores ya se podra ejecutar el proyecto y probarlo en el swagger.

Cuando la api ya se encuentra en ejecucion, existen los siguientes servicios.
1. Grimory
GET=> api/grimory: devuelve el listado de todos los grimorios registrados en la BD.
POST=> api/grimory: se utiliza para agregar los grimorios mencionados en el documento a la BD.
GET{ID}=> api/grimory/{id}: se utiliza para obtener un grimorio en especifico por medio del ID.
PUT=> api/grimory/{id}: sirve para actualizar el nombre o corregir el nombre de algun grimory registrado buscandolo por el ID.
DELETE=> api/grimory/{id}: sirve para eliminar un grimory en especifico localizandolo por medio del ID.

2. Solicitud
GET=> api/solicitud: devuelve el listado de todos las solicitudes registrados en la BD.
POST=> api/solicitud: se utiliza para agregar o enviar una nueva solicitud de ingreso en la cual se pone el IDSTATUS=1 lo que significa que es un status pendiente.
GET{ID}=> api/solicitud/{id}: se utiliza para obtener una solicitud en especifico por medio del ID.
PUT=> api/solicitud/{id}: sirve para actualizar una solicitud registrada en la BD buscandola por medio del ID.
DELETE=> api/solicitud/{id}: sirve para eliminar una solicitud en especifico localizandolo por medio del ID.

3.Status
PUT=> api/status/{id}: sirve para actualizar el status de una solicitud registrada en la BD buscandola por medio del ID e ingresando el IDSTATUS, se tomo en cuenta 3 status los cuales son ID=1 pendiente, ID=2 aceptado, ID=3 rechazado.
GET=> api/status/{idGrimorio}: sirve para buscar el listado de solicitudes registradas de acuerdo a un grimorio en especifico asignado.

Como primer paso se recomienda crear primero los grimorios en el post o en la base de datos, ya que apartir de los grimorios se realizara el proceso para la actualizacion del status de las solicitudes.
