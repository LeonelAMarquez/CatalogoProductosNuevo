<%@ Page Title="Formulario" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Formulario.aspx.cs" Inherits="CatalogoProductosNuevo.Formulario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .form-control.is-invalid, input.is-invalid {
            border-color: #dc3545 !important;
            box-shadow: 0 0 0 .2rem rgba(220,53,69,.1);
        }

        label.form-label.text-danger {
            color: #dc3545 !important;
        }

            label.form-label.text-danger::after {
                content: "\2716";
                display: inline-block;
                margin-left: .5rem;
                color: #dc3545;
                font-weight: 700;
                font-size: .9rem;
                line-height: 1;
                vertical-align: middle;
            }

        .field-error {
            color: #dc3545;
            font-size: .875rem;
            display: block;
        }
    </style>

    <div class="container py-4">
        <h1 class="display-6 fw-semibold mb-4">Ingresá tus datos</h1>

        <asp:UpdatePanel ID="upForm" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                <asp:Literal ID="litError" runat="server" EnableViewState="false" />

                <div class="row g-3">
                    <div class="col-lg-6 col-md-8">
                        <label for="txtDni" class="form-label">DNI</label>
                        <asp:TextBox ID="txtDni" runat="server" CssClass="form-control" required="required" MaxLength="12" AutoPostBack="true" pattern="^\d{7,12}$" OnTextChanged="txtDni_TextChanged" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDni" CssClass="field-error" ErrorMessage="Ingresá tu DNI" Display="Dynamic" />
                        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtDni" CssClass="field-error" Display="Dynamic" ErrorMessage="Sólo números (7–12 dígitos)" ValidationExpression="^\d{7,12}$" />
                    </div>
                </div>

                <div class="row g-3 mt-1">
                    <div class="col-md-4">
                        <label for="txtNombre" class="form-label">Nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"
                            required="required"
                            pattern="^[A-Za-zÁÉÍÓÚÜÑáéíóúüñ' -]{2,40}$" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNombre"
                            CssClass="field-error" ErrorMessage="Ingresá tu nombre" Display="Dynamic" />
                        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtNombre"
                            CssClass="field-error" Display="Dynamic"
                            ErrorMessage="Sólo letras y espacios (2–40)"
                            ValidationExpression="^[A-Za-zÁÉÍÓÚÜÑáéíóúüñ' -]{2,40}$" />
                    </div>

                    <div class="col-md-4">
                        <label for="txtApellido" class="form-label">Apellido</label>
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"
                            required="required"
                            pattern="^[A-Za-zÁÉÍÓÚÜÑáéíóúüñ' -]{2,40}$" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtApellido"
                            CssClass="field-error" ErrorMessage="Ingresá tu apellido" Display="Dynamic" />
                        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtApellido"
                            CssClass="field-error" Display="Dynamic"
                            ErrorMessage="Sólo letras y espacios (2–40)"
                            ValidationExpression="^[A-Za-zÁÉÍÓÚÜÑáéíóúüñ' -]{2,40}$" />
                    </div>

                    <div class="col-md-4">
                        <label for="txtEmail" class="form-label">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"
                            required="required"
                            pattern="^(?=.{1,254}$)(?=.{1,64}@)(?!.*\.\.)[A-Za-z0-9!#$%&'*+/=?^_{}|~\-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_{}|~\-]+)*@(?:(?=[A-Za-z0-9\-]{1,63}\.)[A-Za-z0-9](?:[A-Za-z0-9\-]{0,61}[A-Za-z0-9])?\.)+[A-Za-z]{2,24}$" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail"
                            CssClass="field-error" ErrorMessage="Ingresá tu email" Display="Dynamic" />
                        <asp:RegularExpressionValidator ID="revEmail" runat="server"
                            ControlToValidate="txtEmail"
                            CssClass="field-error" Display="Dynamic"
                            ErrorMessage="Formato de email inválido"
                            ValidationExpression="^(?=.{1,254}$)(?=.{1,64}@)(?!.*\.\.)[A-Za-z0-9!#$%&'*+/=?^_{}|~\-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_{}|~\-]+)*@(?:(?=[A-Za-z0-9\-]{1,63}\.)[A-Za-z0-9](?:[A-Za-z0-9\-]{0,61}[A-Za-z0-9])?\.)+[A-Za-z]{2,24}$" />
                    </div>
                </div>

                <div class="row g-3 mt-1">
                    <div class="col-md-6">
                        <label for="txtDireccion" class="form-label">Dirección</label>
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"
                            required="required"
                            pattern="^[A-Za-zÁÉÍÓÚÜÑáéíóúüñ0-9°º#.,' -]{4,60}$" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDireccion"
                            CssClass="field-error" ErrorMessage="Ingresá la dirección" Display="Dynamic" />
                        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtDireccion"
                            CssClass="field-error" Display="Dynamic"
                            ErrorMessage="Dirección inválida (4–60 caracteres)"
                            ValidationExpression="^[A-Za-zÁÉÍÓÚÜÑáéíóúüñ0-9°º#.,' -]{4,60}$" />
                    </div>

                    <div class="col-md-4">
                        <label for="txtCiudad" class="form-label">Ciudad</label>
                        <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control"
                            required="required"
                            pattern="^[A-Za-zÁÉÍÓÚÜÑáéíóúüñ' .-]{2,50}$" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCiudad"
                            CssClass="field-error" ErrorMessage="Ingresá la ciudad" Display="Dynamic" />
                        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtCiudad"
                            CssClass="field-error" Display="Dynamic"
                            ErrorMessage="Sólo letras/espacios (2–50)"
                            ValidationExpression="^[A-Za-zÁÉÍÓÚÜÑáéíóúüñ' .-]{2,50}$" />
                    </div>

                    <div class="col-md-2">
                        <label for="txtCp" class="form-label">CP</label>
                        <asp:TextBox ID="txtCp" runat="server" CssClass="form-control"
                            MaxLength="10" required="required"
                            pattern="^\d{4,10}$" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCp"
                            CssClass="field-error" ErrorMessage="Ingresá el CP" Display="Dynamic" />
                        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtCp"
                            CssClass="field-error" Display="Dynamic"
                            ErrorMessage="Sólo números (4–10)"
                            ValidationExpression="^\d{4,10}$" />
                    </div>
                </div>

                <div class="mt-3">
                    <asp:Button ID="btnParticipar"
                        runat="server"
                        CssClass="btn btn-primary"
                        Text="¡Participar!"
                        OnClick="btnParticipar_Click"
                        OnClientClick="return markInvalidAndValidate();" />
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script>
        function getInputForValidator(v) {
            var id = v.controltovalidate;
            var el = document.getElementById(id);
            if (!el) el = document.querySelector("[name$='$" + id + "']");
            return el;
        }

        function validatorHasError(v) {
            if (v.isvalid === false) return true;
            if (v.style && v.style.display !== "none") return true;
            try {
                var cs = getComputedStyle(v);
                if (cs && cs.display !== "none" && cs.visibility !== "hidden" && v.offsetWidth > 0) return true;
            } catch (_) { }
            return false;
        }

        function applyValidationStyles() {
            if (typeof Page_Validators === "undefined") return;
            var byControl = {};
            for (var i = 0; i < Page_Validators.length; i++) {
                var v = Page_Validators[i];
                var id = v.controltovalidate;
                if (!byControl[id]) {
                    var el = getInputForValidator(v);
                    var lbl = document.querySelector("label[for='" + id + "']");
                    byControl[id] = { hasError: false, el: el, lbl: lbl };
                }
                if (validatorHasError(v)) {
                    byControl[id].hasError = true;
                }
            }
            Object.keys(byControl).forEach(function (id) {
                var info = byControl[id];
                if (!info.el) return;
                info.el.classList.toggle("is-invalid", info.hasError);
                info.el.classList.toggle("is-valid", !info.hasError);
                if (info.lbl) info.lbl.classList.toggle("text-danger", info.hasError);
            });
        }

        function markInvalidAndValidate() {
            if (typeof Page_ClientValidate === "function") {
                Page_ClientValidate();
            }
            applyValidationStyles();
            return window.Page_IsValid !== false;
        }

        (function () {
            var form = document.querySelector("form.needs-validation");
            if (!form) return;
            form.addEventListener("submit", function (ev) {
                if (typeof Page_ClientValidate === "function") Page_ClientValidate();
                form.classList.add("was-validated");
                applyValidationStyles();
                if (!window.Page_IsValid) {
                    ev.preventDefault();
                    ev.stopPropagation();
                }
            });
            if (window.Sys && Sys.WebForms) {
                Sys.WebForms.PageRequestManager.getInstance()
                    .add_endRequest(function () {
                        form.classList.add("was-validated");
                        applyValidationStyles();
                    });
            }
            document.addEventListener("DOMContentLoaded", function () {
                applyValidationStyles();
            });
        })();
    </script>


</asp:Content>
