<%@ Page Title="Felicitaciones" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Exito.aspx.cs" Inherits="CatalogoProductosNuevo.Exito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container py-4">
        <div class="alert alert-success shadow-sm" role="alert">
            <h4 class="alert-heading">¡Listo, ya estás participando! 🎉</h4>
            <p>
                <strong><asp:Literal ID="litNombre" runat="server" /></strong>
                Registramos tu participación correctamente!
            </p>
        </div>
    <a href="Home.aspx" class="btn btn-outline-primary">Volver al inicio</a>
    </div>
</asp:Content>
