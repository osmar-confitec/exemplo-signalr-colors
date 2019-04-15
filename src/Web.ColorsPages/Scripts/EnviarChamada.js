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

    documento: null,
    hash: ` {"Id":"${this.Id}","IdSignalR":null,"Registrar":false,"Caminho":null} `,
    Id:'',
    setevents: function () {
        this.documento.on('click', '#BtnClickBiometriaCall', this.ProcessBiometria);
        this.documento.on('click', '#BtnLimparCores', this.ProcessLimpar);
        
    },
    InicializarHub() {
        //Set the hubs URL for the connection
        $.connection.hub.url = "http://localhost:9022";
        var connection = $.hubConnection('http://localhost:9022', { useDefaultPath: true });
        //console.log($.connection);
        var contosoChatHubProxy = connection.createHubProxy('myHub');
        // Declare a proxy to reference the hub.
        contosoChatHubProxy.on('recebeCor', function (message) {

            if (message === 'Vermelho') {
                $('#divRed').addClass('red');
                return;
            }

            if (message === 'Amarelo') {
                $('#divAmarelo').addClass('amarelo');
                return;
            }

            if (message === 'Verde') {
                $('#divVerde').addClass('verde');
                return;
            }

            if (message === 'Azul') {
                $('#divAzul').addClass('azul');
                return;
            }
            
                
        });

        // Create a function that the hub can call to broadcast messages.
        var chamada = this;
        connection.start()
            .done(function () {
                chamada.Id = connection.id;
                console.log('Now connected, connection ID=' + connection.id);
                $('#BtnLimparCores').click(function () {
                    contosoChatHubProxy.invoke('limparCores');
                    $('#divRed').removeClass('red');
                    $('#divAmarelo').removeClass('amarelo');
                    $('#divVerde').removeClass('verde');
                    $('#divAzul').removeClass('azul');
                });

            })
            .fail(function () { console.log('Could not connect'); });
    
    },
    ProcessLimpar: function () {
       
    },
    ProcessBiometria: function () {
        var id = ChamadasController.Id;
        var url = `NetDocs:{"Id":"${id}","IdSignalR":"${id}","Registrar":false,"Caminho":null}`;
       
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