Hola
Soy Bruno Dante Huarca Bustinza

Se debe crear una db en MongoDB llamada "wayni" con un documento "Users" con estos campos:
json de ejemplo para importar en la db:

{
  "FirstName": "Bruno",
  "LastName": "Huarca",
  "Username": "Bruno_HB",
  "Password": "bruno123A",
  "Email": "bruno.huarca@gmail.com",
  "PhoneNumber": "902311395"
}

luego el id del usuario se tiene que cambiar en 
/frontend/src/components/EditFile.js (linea 29)
/frontend/src/components/UserForm.js (linea 23)

