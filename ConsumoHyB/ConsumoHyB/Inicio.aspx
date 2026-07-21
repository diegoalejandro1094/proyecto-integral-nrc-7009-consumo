<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="ConsumoHyB.Inicio" MasterPageFile="~/Site1.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Inicio - HyB Textiles</title>
</asp:Content>

<asp:Content ID="Content2"
    ContentPlaceHolderID="ContentPlaceHolder1"
    runat="server">

    <div class="container mt-4">
        <div class="p-5 mb-4 bg-light border rounded-3">
            <div class="container-fluid py-3">
                <h1 class="display-5 fw-bold">HyB Textiles</h1>
                <p class="col-md-8 fs-5">
                    Bienvenido al sistema de gestión de HyB Textiles.
                    Seleccione una opción del menú para administrar la información.               
                </p>
            </div>
        </div>

        <div class="row">

            <div class="col-md-4 mb-4">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <h4 class="card-title">Clientes</h4>
                        <p class="card-text">
                            Registrar, actualizar, eliminar y consultar clientes.
                       
                        </p>

                        <a href="frmCliente.aspx"
                            class="btn btn-primary">Ir a Clientes
                        </a>

                    </div>
                </div>
            </div>

            <div class="col-md-4 mb-4">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <h4 class="card-title">Productos</h4>
                        <p class="card-text">
                            Administración de productos del sistema.
                       
                        </p>

                        <button class="btn btn-secondary" disabled>
                            Próximamente
                       
                        </button>

                    </div>
                </div>
            </div>

            <div class="col-md-4 mb-4">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <h4 class="card-title">Empleados</h4>
                        <p class="card-text">
                            Gestión de empleados de la empresa.                       
                        </p>
                        <button class="btn btn-secondary" disabled>
                            Próximamente                       
                        </button>
                    </div>
                </div>
            </div>
        </div>
        <div class="p-5 mb-4 bg-light border rounded-3">
            <div class="container-fluid py-3">
                <h1 class="display-5 fw-bold">¿Quienes somos?</h1>
                <p class="col-md-8 fs-5">
                    HyB Textiles es una empresa dedicada a la fabricación y comercialización de tejidos industriales, comprometida con ofrecer productos de calidad, innovación y un servicio confiable. 
                    Nuestro objetivo es satisfacer las necesidades de nuestros clientes mediante procesos eficientes y un equipo orientado a la excelencia.
               
                </p>
            </div>
        </div>
        <div class="p-5 mb-4 bg-light border rounded-3">
            <div class="container-fluid py-3">
                <h1 class="display-5 fw-bold">Servicio de tejido</h1>
                <p class="col-md-8 fs-5">
                    HyB Textiles se especializa en la fabricación de tejidos industriales de alta calidad. 
                    Este sistema permite gestionar de forma eficiente clientes, productos y procesos operativos de la empresa.
               
                </p>
            </div>
        </div>
    </div>
</asp:Content>
