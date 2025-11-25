<%@ Page Title="Elegir Premio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EleccionPremio.aspx.cs" Inherits="CatalogoProductosNuevo.EleccionPremio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Elige te premio!</h1>
    <br />
    <asp:Repeater ID="RepeaterArticulos" runat="server">
    <HeaderTemplate>
        <div class="row">
    </HeaderTemplate>

    <ItemTemplate>
        <div class="col-md-4 mb-3">
            <div class="card h-100">
                <div id='carousel_<%# Eval("Id") %>' class="carousel slide">
                    <div class="carousel-inner">
                        <asp:Repeater ID="RepeaterImagenes" runat="server">
                            <ItemTemplate>
                                <div class="carousel-item <%# If(Container.ItemIndex = 0, "active", "") %>">
                                    <img src="<%# Container.DataItem %>" class="d-block w-100" alt="Imagen del artículo">
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <button class="carousel-control-prev" type="button" data-bs-target="#carousel_<%# Eval("Id") %>" data-bs-slide="prev">
                        <span class="carousel-control-prev-icon"></span>
                    </button>
                    <button class="carousel-control-next" type="button" data-bs-target="#carousel_<%# Eval("Id") %>" data-bs-slide="next">
                        <span class="carousel-control-next-icon"></span>
                    </button>
                </div>
                <div class="card-body">
                    <h5 class="card-title"><%# Eval("Nombre") %></h5>
                    <p class="card-text"><%# Eval("Descripcion") %></p>
                    <a href='Formulario.aspx?id=<%# Eval("Id") %>' class="btn btn-primary">Quiero este!</a>
                </div>
            </div>
        </div>
    </ItemTemplate>

    <FooterTemplate>
        </div>
    </FooterTemplate>
</asp:Repeater>

</asp:Content>
