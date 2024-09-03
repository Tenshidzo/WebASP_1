<%@ Page Language="C#" Debug="true" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebASP_1.Views.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Игра с матрицей кнопок</title>
    
</head>
<body>
    <form id="form1" runat="server">
        <div>
       <asp:Label ID="ScoreLabel" runat="server" Text="Счёт: 0" Font-Bold="True"></asp:Label>
        <br /><br />
        <asp:Button ID="Size3Button" runat="server" Text="3x3" OnClick="GenerateMatrix" />
        <asp:Button ID="Size4Button" runat="server" Text="4x4" OnClick="GenerateMatrix" />
        <asp:Button ID="Size5Button" runat="server" Text="5x5" OnClick="GenerateMatrix" />
        <br /><br />
        <asp:Panel ID="MatrixPanel" runat="server">
        </asp:Panel>
        <br />
        <asp:Button ID="ResetButton" runat="server" Text="Reset" OnClick="ResetGame" />

        <!-- Скрытые поля для хранения состояния -->
        <asp:HiddenField ID="MatrixSizeHidden" runat="server" />
        <asp:HiddenField ID="MatrixDataHidden" runat="server" />
        <asp:HiddenField ID="FirstButtonValueHidden" runat="server" />
        <asp:HiddenField ID="FirstButtonIDHidden" runat="server" />
    </div>
    </form>
</body>
</html>
