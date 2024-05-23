const express = require("express");
const path = require("path");
const app = express();
const porta = 8185; //<--Configura o número da porta

app.listen(porta, () => {
    console.log(
        "|-------------------------------------------------------------------------|"
    );
    console.log(
        "|  CONTROLE SORTEIO HARD COUNTRY 2024                                     |"
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
