/*
-- =================================================================================
-- Controller: RequisicoesController.js
-- =================================================================================
-- Developer: Osmar Gonçalves Vieira.
-- 
--
-- Objective: RequisicoesController
--
-- =================================================================================
*/

var ChamadasController = {
  
    documento:null,
    hash: '{"Id":"82891064-b09b-4f6c-aa5d-0a394f5d5fe8","IdSignalR":null,"Registrar":false,"Caminho":null}',
    setevents: function () {
        this.documento.on('click', '#BtnClickBiometriaCall', this.ProcessBiometria);
       
    },
    InicializarHub() {
        //Set the hubs URL for the connection
        $.connection.hub.url = "http://localhost:9022";

        // Declare a proxy to reference the hub.
        var chat = $.connection.colorsHub;

        console.log(chat);

        // Create a function that the hub can call to broadcast messages.
        chat.client.recebeCorSelecionada = function (name) {
            alert(name);
        };
       

        // Start the connection.
        $.connection.hub.start().done(function () {

          

        });
    },
    ProcessBiometria: function () {
        var url = 'NetDocs:' + ChamadasController.hash;
        document.getElementById("iframeBiometria").src = (url);
    },
    init: function () {
        
        this.documento = $(document);
        this.setevents();
        if (!$("#iframeBiometria").length) {
            $('body').prepend('<iframe style="display:none" name="iframeBiometria" id="iframeBiometria"></iframe>');
        }
    }
};

$(document).ready(function () {
    ChamadasController.init();
    ChamadasController.InicializarHub();
});