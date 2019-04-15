<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Web.ColorsPages.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        .divColor {
            min-height: 200px;
            width: 100%;
        }

        div.red {
            background-color: red;
        }

        div.amarelo {
            background-color: yellowgreen;
        }

        div.azul {
            background-color: blue;
        }

        div.verde {
            background-color: lawngreen;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <input type="button" value="chamar form" id="BtnClickBiometriaCall" />
            <input type="button" value="limpar cores" id="BtnLimparCores" />
            <div class="divColor" id="divRed">
                <center><h2>Vermelho</h2></center>
            </div>
            <div class="divColor" id="divAzul">
                <center><h2>Azul</h2></center>
            </div>
            <div class="divColor" id="divAmarelo">
                <center><h2>Amarelo</h2></center>
            </div>
            <div class="divColor" id="divVerde">
                <center><h2>Verde</h2></center>
            </div>
        </div>
    </form>
    <script src="Scripts/jquery-1.10.0.js"></script>
    <script src="Scripts/jquery.signalR-2.4.0.js"></script>
    <script src="signalr/hubs"></script>
    <script src="Scripts/EnviarChamada.js"></script>

</body>
</html>
