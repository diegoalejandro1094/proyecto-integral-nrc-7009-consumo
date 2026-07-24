<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="ConsumoHyB.frmLogin" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Iniciar Sesi&oacute;n - HyB Textiles</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css"
          rel="stylesheet" crossorigin="anonymous" />
    <style>
        html, body {
            height: 100%;
            margin: 0;
            background: linear-gradient(135deg, #1a1a2e 0%, #16213e 50%, #0f3460 100%);
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        .login-wrapper {
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            padding: 20px;
        }

        .login-card {
            background: rgba(255, 255, 255, 0.05);
            backdrop-filter: blur(20px);
            -webkit-backdrop-filter: blur(20px);
            border: 1px solid rgba(255, 255, 255, 0.15);
            border-radius: 20px;
            padding: 48px 40px;
            width: 100%;
            max-width: 420px;
            box-shadow: 0 25px 50px rgba(0, 0, 0, 0.4);
        }

        .brand-logo {
            width: 64px;
            height: 64px;
            background: linear-gradient(135deg, #e94560, #0f3460);
            border-radius: 16px;
            display: flex;
            align-items: center;
            justify-content: center;
            margin: 0 auto 24px;
            font-size: 28px;
            font-weight: 800;
            color: white;
            letter-spacing: -1px;
        }

        .login-title {
            color: #ffffff;
            font-size: 1.75rem;
            font-weight: 700;
            text-align: center;
            margin-bottom: 4px;
        }

        .login-subtitle {
            color: rgba(255, 255, 255, 0.5);
            font-size: 0.9rem;
            text-align: center;
            margin-bottom: 36px;
        }

        .form-label {
            color: rgba(255, 255, 255, 0.75);
            font-size: 0.85rem;
            font-weight: 600;
            letter-spacing: 0.5px;
            margin-bottom: 8px;
        }

        .form-control {
            background: rgba(255, 255, 255, 0.08) !important;
            border: 1px solid rgba(255, 255, 255, 0.15) !important;
            border-radius: 10px;
            color: #ffffff !important;
            padding: 12px 16px;
            font-size: 0.95rem;
            transition: all 0.3s ease;
        }

        .form-control::placeholder {
            color: rgba(255, 255, 255, 0.3);
        }

        .form-control:focus {
            background: rgba(255, 255, 255, 0.12) !important;
            border-color: #e94560 !important;
            box-shadow: 0 0 0 3px rgba(233, 69, 96, 0.2) !important;
            outline: none;
        }

        .btn-login {
            width: 100%;
            padding: 13px;
            border-radius: 10px;
            font-size: 1rem;
            font-weight: 700;
            letter-spacing: 0.5px;
            background: linear-gradient(135deg, #e94560, #c0392b);
            border: none;
            color: white;
            cursor: pointer;
            transition: all 0.3s ease;
            margin-top: 8px;
        }

        .btn-login:hover {
            background: linear-gradient(135deg, #c0392b, #a93226);
            transform: translateY(-2px);
            box-shadow: 0 8px 25px rgba(233, 69, 96, 0.4);
        }

        .btn-login:active {
            transform: translateY(0);
        }

        .alert-error {
            background: rgba(233, 69, 96, 0.15);
            border: 1px solid rgba(233, 69, 96, 0.4);
            border-radius: 10px;
            color: #ff8fa3;
            padding: 12px 16px;
            font-size: 0.875rem;
            margin-bottom: 20px;
        }

        .footer-text {
            color: rgba(255, 255, 255, 0.3);
            font-size: 0.78rem;
            text-align: center;
            margin-top: 28px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-wrapper">
            <div class="login-card">

                <div class="brand-logo">H<span style="color:#e94560;">B</span></div>
                <h1 class="login-title">Bienvenido</h1>
                <p class="login-subtitle">Sistema de Gesti&oacute;n HyB Textiles</p>

                <!-- Mensaje de error -->
                <asp:Panel ID="pnlError" runat="server" Visible="false">
                    <div class="alert-error" id="divError" runat="server">
                        <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                    </div>
                </asp:Panel>

                <!-- Campo Usuario -->
                <div class="mb-3">
                    <label class="form-label" for="txtUsuario">USUARIO</label>
                    <asp:TextBox ID="txtUsuario"
                                 runat="server"
                                 CssClass="form-control"
                                 placeholder="Ingresa tu usuario"
                                 MaxLength="50" />
                </div>

                <!-- Campo Contrase&ntilde;a -->
                <div class="mb-4">
                    <label class="form-label" for="txtClave">CONTRASE&Ntilde;A</label>
                    <asp:TextBox ID="txtClave"
                                 runat="server"
                                 CssClass="form-control"
                                 TextMode="Password"
                                 placeholder="Ingresa tu contrase&ntilde;a"
                                 MaxLength="200" />
                </div>

                <!-- Botón Ingresar -->
                <asp:Button ID="btnIngresar"
                            runat="server"
                            Text="Ingresar"
                            CssClass="btn-login"
                            OnClick="btnIngresar_Click" />

                <p class="footer-text">&copy; <%= DateTime.Now.Year %> HyB Textiles S.A.C. — Todos los derechos reservados</p>

            </div>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
