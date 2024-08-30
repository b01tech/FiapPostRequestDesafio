# Descrição do desafio

O time deve desenvolver um projeto que adivinhe uma senha que pode conter ao menos um número e uma letra do alfabeto, podendo ser maiúscula ou minúscula. 

Ex.: A chave pode ser a10z, c48i, d98à ou A13c, A38g...etc 

Após criar os números o sistema deve fazer um POST no endpoint abaixo, ele não tem autenticação. 

Exemplo do corpo do JSON: 

curl --location 'https://fiapnet.azurewebsites.net/fiap' \ 

--header 'Content-Type: application/json' \ 

--data '{ 
"Key": "X99h", 
"grupo":"seu_grupo" 
}' 

Esse endpoint deve retornar uma string com duas hashtags. 
