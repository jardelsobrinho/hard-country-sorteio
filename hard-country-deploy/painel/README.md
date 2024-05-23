# SERVIDOR WEB CLIENTE ATIVO
Esse projeto é responsável por configurar o servidor que vai manter o aplicativo do web-cliente-ativo online.

## REQUISITOS PARA QUE O SERVIDOR FUNCIONE
Esse servidor foi escrito em node, sendo assim é necessário instalar o node para que ele funcione corretamente. Acesse o site ``` https://nodejs.org/pt-br/download/ ``` e faça o download da versão do windows caso ele não esteja instalado. Para ver se o node está instalado, abra o prompt de comando e digite ``` node --version ```, se ele estiver instalado vai exibir o número da versão que está instalada.

## CONFIGURAÇÕES DO SERVIDOR
Toda a configuração do servidor é feita no arquivo index.js.
Ele se parece com o exemplo abaixo:

```
const express = require("express");
const path = require("path");
const app = express();
const porta = 8185; //<--Configura o número da porta

app.listen(porta, () => {
  console.log(
    "|-------------------------------------------------------------------------|"
  );
  console.log(
    "|  WEB CLIENTE ATIVO                                                      |"
  );
  console.log(
    "|-------------------------------------------------------------------------|"
  );
  console.log(
    "|  STATUS: Ativo                                                          |"
  );
  console.log(
    "|  PORTA: " +
      porta +
      "                                                            |"
  );
  console.log(
    "|  URL: http://localhost:8185                                             |"
  );
  console.log(
    "|-------------------------------------------------------------------------|"
  );
  console.log(
    "|  ATENÇÃO !!!                                                            |"
  );
  console.log(
    "|  Mantenha essa janela aberta para que o servidor continue a funcionar!  |"
  );
  console.log(
    "|-------------------------------------------------------------------------|"
  );
  console.log(
    "|  Pressione Ctrl + C para finalizar o servidor                           |"
  );
  console.log(
    "|-------------------------------------------------------------------------|"
  );
});

app.use(express.static(path.join(__dirname, "/public")));

app.get("/", function (req, res) {
  res.sendFile(path.join(__dirname, "index.html"));
});

```

### CONFIGURANDO A PORTA
Localize no arquivo index.js o seguinte trecho  ``` const porta = 8185; //<--Configura o número da porta ``` e altere o número da porta.

### INICIANDO O SERVIDOR
Digite no prompt de comando ```run.bat``` e pressione Enter. O Servidor será iniciado. Enquanto a tela estiver aberta ele vai funcionar.

### ONDE COLOCAR OS ARQUIVO DO APP WEB CLIENTE ATIVO
Dentro da pasta do servidor crie uma pasta chama **public**, caso não exista, e adicione os arquivo do app web-cliente-ativo dentro dessa pasta.

