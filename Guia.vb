Imports System
Imports System.Text
Imports System.IO
Public Class Guia
    Public reng_num As Integer = 1
#Region "Al iniciar el Form"
    Private Sub Guia_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Delete_Guiatemp()
        Limpiar_Formulario()
        obj._OBNumGuiaAle = Nothing
    End Sub
    Private Sub Guia_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Load_ComboBox()
        Buscar_DatosEmpresa()
        TBGuiaCarga.Enabled = False
        TBComision.Enabled = False
        TBMontoSegu.Enabled = False
        Limpiar.Enabled = False
        Guardar.Enabled = False
        AddButton.Enabled = False
        RemoveButton.Enabled = False
        TC.Enabled = False
        CBTransporte.Enabled = False
        obj._impfisfac = ""
        obj.Mfiscal = My.Settings.fiscal
        obj.Miva = My.Settings.iva
        obj._OBNumGuiaAle = Nothing
        LBSucursal.Text = obj.Sucursal
        CBActCliente.CheckState = CheckState.Unchecked
        GBCliente.Enabled = False
        CBActConsig.CheckState = CheckState.Unchecked
        GBConsignador.Enabled = False
        GBFPago.Enabled = False
    End Sub
#End Region
#Region "Botones"
    Private Sub Nuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Nuevo.Click
        'Activar:
        Limpiar.Enabled = True
        Guardar.Enabled = True
        Guardar.Enabled = True
        AddButton.Enabled = True
        RemoveButton.Enabled = True
        TC.Enabled = True
        CBTransporte.Enabled = True
        'Bloquear:
        Nuevo.Enabled = False
        'Otros:
        Dim randNumber As New Random(DateTime.Now.Millisecond)
        obj._OBNumGuiaAle = randNumber.Next()
        obj.CantPaquetes = Nothing
    End Sub
    Private Sub Limpiar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Limpiar.Click
        Limpiar_Formulario()
    End Sub
    Private Sub Guardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Guardar.Click
        If RBRemitente.Checked Or RBDestinatario.Checked Then
            Try
                Buscar_DatosCliente()
                Buscar_DatosRemitente()
                Buscar_DatosDestinatario()
                Buscar_GuiaNum()
                Buscar_FactNum()
                If DataGridView.Rows.Count > 0 Then
                    Validar_TCB()
                    If Validar_TCB() = True Then
                        Insert_Table()
                        Update_Almacen()
                        MsgBoxInfo(mensaje:="Los Datos de la Factura han sido registrados!", titulo:="AVISO: Validación - Sistema")
                        If obj.Mfiscal = True Then
                            Try
                                If obj.Miva = True Then
                                    Imprimir_Fiscal_iva()
                                Else
                                    Imprimir_Fiscal()
                                End If
                            Catch ex As Exception
                                MsgBoxInfo(mensaje:="No se puede imprimir la Factura Fiscal!", titulo:="AVISO: Validación Impresora Fiscal - Sistema")
                            End Try
                        End If
                        Cancelar_Click(sender, e)
                        My.Forms.VGuiaImp.ShowDialog()
                        Delete_Guiatemp()
                        obj._OBNumGuiaAle = Nothing
                    Else
                        MsgBoxError(mensaje:="Revise los errores indicados antes de guardar los datos de la Guia!", titulo:="ERROR: Validar datos - Sistema")
                        Exit Sub
                    End If
                Else
                    MsgBoxError(mensaje:="No hay Items registrados en la Guia!", titulo:="ERROR: Validar Items - Sistema")
                    Exit Sub
                End If
            Catch ex As Exception
                MsgBoxError(ex.Message, titulo:="ERROR: Validar datos - Sistema")
                Exit Sub
            End Try
        Else
            MsgBoxError(mensaje:="Seleccione primero a quien facturar!", titulo:="ERROR: Validar Facturación - Sistema")
        End If
    End Sub
    Private Sub Cancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancelar.Click
        'Activar:
        Nuevo.Enabled = True
        'Bloquear:
        Delete_Guiatemp()
        Load_ComboBox()
        Limpiar_Formulario()
        TBGuiaCarga.Enabled = False
        TBComision.Enabled = False
        TBMontoSegu.Enabled = False
        Limpiar.Enabled = False
        Guardar.Enabled = False
        AddButton.Enabled = False
        RemoveButton.Enabled = False
        TC.Enabled = False
        CBTransporte.Enabled = False
        CBActCliente.CheckState = CheckState.Unchecked
        CBActConsig.CheckState = CheckState.Unchecked
    End Sub
    Private Sub Cerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cerrar.Click
        Me.Close()
    End Sub
#Region "Otros Botones"
    Private Sub AddButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddButton.Click
        ' Try
        'Validar_AntesAgregarItem()
        'If Validar_AntesAgregarItem() = True Then
        obj._OBIDZona = CBTransporte.SelectedValue
        My.Forms.AgregarItem.ShowDialog()
        ' Else
        'MsgBoxError(mensaje:="Revise los errores indicados antes de ingresar Items a la Guia!", titulo:="ERROR: Validar datos - Sistema")
        ' Exit Sub
        'End If
        'Catch ex As Exception
        '    MsgBoxError(ex.Message, titulo:="ERROR: Validar datos - Sistema")
        'End Try
    End Sub
    Private Sub RemoveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveButton.Click
        Validar_items_DGV()
        If Validar_items_DGV() = True Then
            If DataGridView.CurrentCell.Value <> Nothing Then
                'My.Forms.EliminarArticulo.ShowDialog()
                'If My.Forms.EliminarArticulo.DialogResult = Windows.Forms.DialogResult.OK Then
                '    Delete_ItemGuia()
                '    Select_DGV_Guia()
                'ElseIf My.Forms.EliminarArticulo.DialogResult = Windows.Forms.DialogResult.Cancel Then
                '    Exit Sub
                'End If
                Delete_Guiatemp()
                Select_DGV_Guia()
            Else
                Exit Sub
            End If
        Else
            MsgBoxError(mensaje:="No hay Items a la Guia para eliminar!", titulo:="ERROR: Validar datos - Sistema")
        End If
    End Sub
    Private Sub BAgregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BAgregar.Click
        Validar_ClienteNuevo()
        If Validar_ClienteNuevo() = True Then
            Insert_Cliente()
            L_CBRemitente()
            L_CBDestinatario()
            MsgBoxInfo(mensaje:="Los Datos del Cliente han sido registrados!", titulo:="AVISO: Validación - Sistema")
            GBCliente.Enabled = False
            TBCCI.Text = Nothing
            TBCNombre.Text = Nothing
            TBCDirec1.Text = Nothing
            TBCTelefono.Text = Nothing
            CBActCliente.CheckState = CheckState.Unchecked
        Else
            Exit Sub
        End If
    End Sub
#End Region
#End Region
#Region "Querys"
#Region "Load ComboBox"
    Private Sub Load_ComboBox()
        'Rellena los ComboBox Los provenientes de Profit:
        L_CBRemitente()
        L_CBDestinatario()
        L_CBCPago()
        L_CBContenido()
        'Rellena el CB Choferes y ayudantes
        L_CBRecolector()
        L_CBAyudante1()
        L_CBAyudante2()
        L_CBTrasnporte()
    End Sub
    'Rellena los CB desde Clientes Activos
    Private Sub L_CBRemitente()
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim ds As New DataSet
        Dim sql As String = "select co_cli,(co_cli +' '+cli_des) as nombre from clientes where inactivo='0'"
        cmd = New SqlClient.SqlCommand(sql, cnn1)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(ds)
        CBRemitente.DataSource = ds.Tables(0)
        CBRemitente.DisplayMember = ds.Tables(0).Columns(1).Caption.ToString
        CBRemitente.ValueMember = ds.Tables(0).Columns(0).Caption.ToString
        cnn1.Close()
    End Sub
    Private Sub L_CBDestinatario()
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As SqlClient.SqlDataAdapter
        Dim ds As New DataSet
        Dim sql As String = "select co_cli,(co_cli +' '+cli_des) as nombre from clientes where inactivo='0'"
        cmd = New SqlClient.SqlCommand(sql, cnn1)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(ds)
        CBDestinatario.DataSource = ds.Tables(0)
        CBDestinatario.DisplayMember = ds.Tables(0).Columns(1).Caption.ToString
        CBDestinatario.ValueMember = ds.Tables(0).Columns(0).Caption.ToString
        cnn1.Close()
    End Sub
    'Rellena el CB Cond. de Pago
    Private Sub L_CBCPago()
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As SqlClient.SqlDataAdapter
        Dim ds As New DataSet
        Dim sql As String = "select tip_cli,(tip_cli +' '+des_tipo) as nombre from tipo_cli where co_us_in ='001'"
        cmd = New SqlClient.SqlCommand(sql, cnn1)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(ds)
        CBCPago.DataSource = ds.Tables(0)
        CBCPago.DisplayMember = ds.Tables(0).Columns(1).Caption.ToString
        CBCPago.ValueMember = ds.Tables(0).Columns(0).Caption.ToString
        cnn1.Close()
    End Sub
    'Rellena el CB tipo de contenido
    Private Sub L_CBContenido()
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As SqlClient.SqlDataAdapter
        Dim ds As New DataSet
        Dim sql As String = "select cod_proc,(cod_proc +' '+des_proc) as nombre from proceden order by des_proc asc"
        cmd = New SqlClient.SqlCommand(sql, cnn1)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(ds)
        CBContenido.DataSource = ds.Tables(0)
        CBContenido.DisplayMember = ds.Tables(0).Columns(1).Caption.ToString
        CBContenido.ValueMember = ds.Tables(0).Columns(0).Caption.ToString
        cnn1.Close()
    End Sub
    'Rellena el CB Choferes y ayudantes
    Private Sub L_CBRecolector()
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As SqlClient.SqlDataAdapter
        Dim ds As New DataSet
        Dim sql As String = "select co_ven,(co_ven +' '+ven_des) as nombre from vendedor where co_ven LIKE 'C%' order by co_ven asc"
        cmd = New SqlClient.SqlCommand(sql, cnn1)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(ds)
        CBRecolector.DataSource = ds.Tables(0)
        CBRecolector.DisplayMember = ds.Tables(0).Columns(1).Caption.ToString
        CBRecolector.ValueMember = ds.Tables(0).Columns(0).Caption.ToString
        cnn1.Close()
    End Sub
    Private Sub L_CBAyudante1()
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As SqlClient.SqlDataAdapter
        Dim ds As New DataSet
        Dim sql As String = "select co_tran,(co_tran +' '+des_tran) as nombre from transpor where co_tran LIKE 'A%' order by co_tran asc"
        cmd = New SqlClient.SqlCommand(sql, cnn1)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(ds)
        CBAyudante1.DataSource = ds.Tables(0)
        CBAyudante1.DisplayMember = ds.Tables(0).Columns(1).Caption.ToString
        CBAyudante1.ValueMember = ds.Tables(0).Columns(0).Caption.ToString
        cnn1.Close()
    End Sub
    Private Sub L_CBAyudante2()
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As SqlClient.SqlDataAdapter
        Dim ds As New DataSet
        Dim sql As String = "select co_cond,(co_cond +' '+cond_des) as nombre from condicio where co_cond LIKE 'A%' order by co_cond asc"
        cmd = New SqlClient.SqlCommand(sql, cnn1)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(ds)
        CBAyudante2.DataSource = ds.Tables(0)
        CBAyudante2.DisplayMember = ds.Tables(0).Columns(1).Caption.ToString
        CBAyudante2.ValueMember = ds.Tables(0).Columns(0).Caption.ToString
        cnn1.Close()
    End Sub
    'Rellena el CB no es de Profit
    Private Sub L_CBTrasnporte()
        open_conection2()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select co_zona, zonas_des from ZONAS order by zonas_des asc"
        cmd = New SqlClient.SqlCommand(sql, cnn2)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            CBTransporte.DataSource = dt
            CBTransporte.DisplayMember = dt.Columns(1).Caption.ToString
            CBTransporte.ValueMember = dt.Columns(0).Caption.ToString
        Else
            CBTransporte.DataSource = Nothing
        End If
        cnn2.Close()
    End Sub
    Private Sub L_CBRecolecta()
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim ds As New DataSet
        Dim sql As String = "select fact_num from pedidos where [status]='0'"
        cmd = New SqlClient.SqlCommand(sql, cnn1)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(ds)
        CBoRecolecta.DataSource = ds.Tables(0)
        CBoRecolecta.DisplayMember = ds.Tables(0).Columns(0).Caption.ToString
        CBoRecolecta.ValueMember = ds.Tables(0).Columns(0).Caption.ToString
        cnn1.Close()
    End Sub
#Region "Zonas de envio ****ELIMINADO****"
    Private Sub L_CBCOrigen()
        'Rellena el ComboBox
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As SqlClient.SqlDataAdapter
        Dim ds As New DataSet
        Dim sql As String = "select co_lin,(co_lin +' '+lin_des) as nombre from lin_art order by lin_des asc"
        cmd = New SqlClient.SqlCommand(sql, cnn1)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(ds)
        'CBCOrigen.DataSource = ds.Tables(0)
        'CBCOrigen.DisplayMember = ds.Tables(0).Columns(1).Caption.ToString
        'CBCOrigen.ValueMember = ds.Tables(0).Columns(0).Caption.ToString
        cnn1.Close()
    End Sub
    Private Sub L_CBZEnvio()
        'Rellena el ComboBox
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select co_subl,(co_subl +' '+subl_des) as nombre from sub_lin " ' where co_lin='" & CBCOrigen.SelectedValue & "'"
        cmd = New SqlClient.SqlCommand(sql, cnn1)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            'CBZEnvio.DataSource = dt
            'CBZEnvio.DisplayMember = dt.Columns(1).Caption.ToString
            'CBZEnvio.ValueMember = dt.Columns(0).Caption.ToString
        Else
            'CBZEnvio.DataSource = Nothing
        End If
        cnn1.Close()
    End Sub
    Private Sub L_CBCDestino()
        'Rellena el ComboBox
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As SqlClient.SqlDataAdapter
        Dim ds As New DataSet
        Dim sql As String = "select co_lin,(co_lin +' '+lin_des) as nombre from lin_art order by lin_des asc"
        cmd = New SqlClient.SqlCommand(sql, cnn1)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(ds)
        'CBCDestino.DataSource = ds.Tables(0)
        'CBCDestino.DisplayMember = ds.Tables(0).Columns(1).Caption.ToString
        'CBCDestino.ValueMember = ds.Tables(0).Columns(0).Caption.ToString
        cnn1.Close()
    End Sub
    Private Sub L_CBZEnviar()
        'Rellena el ComboBox
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select co_subl,(co_subl +' '+subl_des) as nombre from sub_lin " ' where co_lin='" & CBCDestino.SelectedValue & "'"
        cmd = New SqlClient.SqlCommand(sql, cnn1)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            'CBZEnviar.DataSource = dt
            'CBZEnviar.DisplayMember = dt.Columns(1).Caption.ToString
            'CBZEnviar.ValueMember = dt.Columns(0).Caption.ToString
        Else
            'CBZEnviar.DataSource = Nothing
        End If
        cnn1.Close()
    End Sub
    'Private Sub CBCOrigen_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles CBCOrigen.SelectionChangeCommitted
    '    open_conection1()
    '    L_CBZEnvio()
    '    cnn1.Close()
    'End Sub
    'Private Sub CBCDestino_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles CBCDestino.SelectionChangeCommitted
    '    open_conection1()
    '    L_CBZEnviar()
    '    cnn1.Close()
    'End Sub
#End Region
#End Region
#Region "Select"
#Region "Buscar"
    Private Sub Buscar_DatosCliente()
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select co_cli,tipo,cli_des,rif,nit,direc1,telefonos,contribu from clientes where [co_cli]='" & CBRemitente.SelectedValue & "'"
        cmd = New SqlClient.SqlCommand(sql, cnn1)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim row As DataRow
            row = dt.Rows(0)
            If Not IsDBNull(row("co_cli")) Then
                obj._co_cli = row("co_cli")
            Else
                obj._co_cli = Nothing
            End If
            If Not IsDBNull(row("tipo")) Then
                obj._tipo = row("tipo")
            Else
                obj._tipo = Nothing
            End If
            If Not IsDBNull(row("cli_des")) Then
                obj._cli_des = row("cli_des")
            Else
                obj._cli_des = Nothing
            End If
            If Not IsDBNull(row("rif")) Then
                obj._rif = row("rif")
            Else
                obj._rif = Nothing
            End If
            If Not IsDBNull(row("nit")) Then
                obj._nit = row("nit")
            Else
                obj._nit = Nothing
            End If
            If Not IsDBNull(row("direc1")) Then
                obj._direc1 = row("direc1")
            Else
                obj._direc1 = Nothing
            End If
            If Not IsDBNull(row("telefonos")) Then
                obj._telefonos = row("telefonos")
            Else
                obj._telefonos = Nothing
            End If
            If Not IsDBNull(row("contribu")) Then
                obj._contribu = row("contribu")
            Else
                obj._contribu = Nothing
            End If
        End If
        cnn1.Close()
    End Sub
    Private Sub Buscar_DirecRemitente()
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select direc1 from clientes where [co_cli]='" & CBRemitente.SelectedValue & "'"
        cmd = New SqlClient.SqlCommand(sql, cnn1)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim row As DataRow
            row = dt.Rows(0)
            If Not IsDBNull(row("direc1")) Then
                LBDirecRemite.Text = row("direc1")
            Else
                LBDirecRemite.Text = Nothing
            End If

        End If
        cnn1.Close()
    End Sub
    Private Sub Buscar_DirecDestinatario()
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select direc1 from clientes where [co_cli]='" & CBDestinatario.SelectedValue & "'"
        cmd = New SqlClient.SqlCommand(sql, cnn1)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim row As DataRow
            row = dt.Rows(0)
            If Not IsDBNull(row("direc1")) Then
                LBDirecDestina.Text = row("direc1")
            Else
                LBDirecDestina.Text = Nothing
            End If

        End If
        cnn1.Close()
    End Sub
    Private Sub Buscar_FactNum()
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select MAX(fact_num) as Mayor from factura"
        cmd = New SqlClient.SqlCommand(sql, cnn1)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim row As DataRow
            row = dt.Rows(0)
            If Not IsDBNull(row("Mayor")) Then
                Dim res As Integer = row("Mayor")
                obj._fact_num = res + 1
            Else
                obj._fact_num = Nothing
            End If
        End If
    End Sub
    Private Sub Buscar_GuiaNum()
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select MAX(guia_num) as Mayor from guia"
        cmd = New SqlClient.SqlCommand(sql, cnn1)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim row As DataRow
            row = dt.Rows(0)
            If Not IsDBNull(row("Mayor")) Then
                Dim res As Integer = row("Mayor")
                obj._guia_num = res + 1
            Else
                obj._guia_num = Nothing
            End If
        End If
    End Sub
    Private Sub Buscar_CobroNum()
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select MAX(cob_num) as Mayor from cobros"
        cmd = New SqlClient.SqlCommand(sql, cnn1)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim row As DataRow
            row = dt.Rows(0)
            If Not IsDBNull(row("Mayor")) Then
                Dim res As Integer = row("Mayor")
                obj._cobro_num = res + 1
            Else
                obj._cobro_num = Nothing
            End If
        End If
    End Sub
    Private Sub Buscar_MovcajNum()
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select MAX(mov_num) as Mayor from mov_caj"
        cmd = New SqlClient.SqlCommand(sql, cnn1)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim row As DataRow
            row = dt.Rows(0)
            If Not IsDBNull(row("Mayor")) Then
                Dim res As Integer = row("Mayor")
                obj._movcaj_num = res + 1
            Else
                obj._movcaj_num = Nothing
            End If
        End If
    End Sub
    Private Sub Buscar_DatosRemitente()
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select co_cli,cli_des,rif,nit,direc1,telefonos from clientes where [co_cli]='" & CBRemitente.SelectedValue & "'"
        cmd = New SqlClient.SqlCommand(sql, cnn1)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim row As DataRow
            row = dt.Rows(0)
            If Not IsDBNull(row("co_cli")) Then
                obj._co_cliR = row("co_cli")
            Else
                obj._co_cliR = Nothing
            End If
            If Not IsDBNull(row("cli_des")) Then
                obj._cli_desR = row("cli_des")
            Else
                obj._cli_desR = Nothing
            End If
            If Not IsDBNull(row("rif")) Then
                obj._rifR = row("rif")
            Else
                obj._rifR = Nothing
            End If
            If Not IsDBNull(row("nit")) Then
                obj._nitR = row("nit")
            Else
                obj._nitR = Nothing
            End If
            If Not IsDBNull(row("direc1")) Then
                obj._direc1R = row("direc1")
            Else
                obj._direc1R = Nothing
            End If
            If Not IsDBNull(row("telefonos")) Then
                obj._telefonosR = row("telefonos")
            Else
                obj._telefonosR = Nothing
            End If
        End If
        cnn1.Close()
    End Sub
    Private Sub Buscar_DatosDestinatario()
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select co_cli,cli_des,rif,nit,direc1,telefonos from clientes where [co_cli]='" & CBDestinatario.SelectedValue & "'"
        cmd = New SqlClient.SqlCommand(sql, cnn1)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim row As DataRow
            row = dt.Rows(0)
            If Not IsDBNull(row("co_cli")) Then
                obj._co_cliD = row("co_cli")
            Else
                obj._co_cliD = Nothing
            End If
            If Not IsDBNull(row("cli_des")) Then
                obj._cli_desD = row("cli_des")
            Else
                obj._cli_desD = Nothing
            End If
            If Not IsDBNull(row("rif")) Then
                obj._rifD = row("rif")
            Else
                obj._rifD = Nothing
            End If
            If Not IsDBNull(row("nit")) Then
                obj._nitD = row("nit")
            Else
                obj._nitD = Nothing
            End If
            If Not IsDBNull(row("direc1")) Then
                obj._direc1D = row("direc1")
            Else
                obj._direc1D = Nothing
            End If
            If Not IsDBNull(row("telefonos")) Then
                obj._telefonosD = row("telefonos")
            Else
                obj._telefonosD = Nothing
            End If
        End If
        cnn1.Close()
    End Sub
    Private Sub Buscar_DatosEmpresa()
        open_conection2()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select NombreEmpresa,Sucursal,RifEmpresa,NitEmpresa,DirecEmpresa,TelefEmpresa from EMPRESA"
        cmd = New SqlClient.SqlCommand(sql, cnn2)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim row As DataRow
            row = dt.Rows(0)
            If Not IsDBNull(row("NombreEmpresa")) Then
                obj._NombreEmpresa = row("NombreEmpresa")
            Else
                obj._NombreEmpresa = Nothing
            End If
            If Not IsDBNull(row("Sucursal")) Then
                obj._Sucursal = row("Sucursal")
            Else
                obj._Sucursal = Nothing
            End If
            If Not IsDBNull(row("RifEmpresa")) Then
                obj._RifEmpresa = row("RifEmpresa")
            Else
                obj._RifEmpresa = Nothing
            End If
            If Not IsDBNull(row("NitEmpresa")) Then
                obj._NitEmpresa = row("NitEmpresa")
            Else
                obj._NitEmpresa = Nothing
            End If
            If Not IsDBNull(row("DirecEmpresa")) Then
                obj._DirecEmpresa = row("DirecEmpresa")
            Else
                obj._DirecEmpresa = Nothing
            End If
            If Not IsDBNull(row("TelefEmpresa")) Then
                obj._TelefEmpresa = row("TelefEmpresa")
            Else
                obj._TelefEmpresa = Nothing
            End If
        End If
        cnn2.Close()
    End Sub
    Private Sub Buscar_DatosRecolecta()
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select co_cli,RTRIM(nombre) as nombre,RTRIM(rif) as rif,co_ven,co_tran,forma_pag,dir_ent from pedidos where [fact_num]='" & CBoRecolecta.SelectedValue & "'"
        cmd = New SqlClient.SqlCommand(sql, cnn1)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim row As DataRow
            row = dt.Rows(0)
            If Not IsDBNull(row("co_cli")) Then
                obj._co_cliRec = row("co_cli")
            Else
                obj._co_cliRec = Nothing
            End If
            If Not IsDBNull(row("nombre")) Then
                obj._cli_desRec = row("nombre")
            Else
                obj._cli_desRec = Nothing
            End If
            If Not IsDBNull(row("rif")) Then
                obj._rifRec = row("rif")
            Else
                obj._rifRec = Nothing
            End If
            If Not IsDBNull(row("dir_ent")) Then
                obj._direc1Rec = row("dir_ent")
            Else
                obj._direc1Rec = Nothing
            End If

            If Not IsDBNull(row("co_ven")) Then
                obj._co_venRec = row("co_ven")
            Else
                obj._co_venRec = Nothing
            End If
            If Not IsDBNull(row("co_tran")) Then
                obj._co_tranRec = row("co_tran")
            Else
                obj._co_tranRec = Nothing
            End If
            If Not IsDBNull(row("forma_pag")) Then
                obj._forma_pagRec = row("forma_pag")
            Else
                obj._forma_pagRec = Nothing
            End If

            'If Not IsDBNull(row("telefonos")) Then
            '    obj._telefonosD = row("telefonos")
            'Else
            '    obj._telefonosD = Nothing
            'End If
            LRecolecta.Text = obj._rifRec & " " & obj._cli_desRec
            LRecolecta2.Text = obj._direc1Rec
            CBRecolector.SelectedValue = obj._co_venRec
            CBAyudante1.SelectedValue = obj._co_tranRec
            CBAyudante2.SelectedValue = obj._forma_pagRec
            CBRemitente.SelectedValue = obj._co_cliRec
        End If
        cnn1.Close()
    End Sub
    Private Function Buscar_MontoDevFactura() As Double
        open_conection2()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select monto from DEVFACT"
        cmd = New SqlClient.SqlCommand(sql, cnn2)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim row As DataRow
            row = dt.Rows(0)
            If Not IsDBNull(row("monto")) Then
                obj.MontoDevFact = row("monto")
            Else
                obj.MontoDevFact = 0
            End If
            Return obj.MontoDevFact
        End If
    End Function
    Private Function Buscar_zonab() As String
        open_conection2()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select co_zona from ZONAS2 where [zonas_des]='" & CBTransporte.Text & "'"
        cmd = New SqlClient.SqlCommand(sql, cnn2)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim row As DataRow
            row = dt.Rows(0)
            obj._zonab = row("co_zona")
        End If
        Return obj._zonab
        cnn2.Close()
    End Function
    Private Function Buscar_co_subl2_Profit1() As String
        open_conection2()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select des from ZONAS where [zonas_des]='" & CBTransporte.Text & "'"
        cmd = New SqlClient.SqlCommand(sql, cnn2)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim row As DataRow
            row = dt.Rows(0)
            obj._ZonaDes = row("des")
        End If
        Return obj._ZonaDes
        cnn2.Close()
    End Function
    Private Function Buscar_co_subl2_Profit2() As String
        cnn1.Close()
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select co_subl from sub_lin where [co_lin]='CCS' and [subl_des] like '%" & obj._ZonaDes & "%'"
        cmd = New SqlClient.SqlCommand(Sql, cnn1)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim row As DataRow
            row = dt.Rows(0)
            obj._co_subl = row("co_subl")
        End If
        cnn1.Close()
        Return obj._co_subl
    End Function
#End Region
    Public Function Validar_items_DGV() As Boolean
        open_conection2()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select * from GUIATEMP where [NumGuia]='" & obj._OBNumGuiaAle & "'"
        cmd = New SqlClient.SqlCommand(sql, cnn2)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Return True
        End If
        Return False
    End Function
#Region "Calcular"
    Private Sub Calcular_SubtotalGuia()
        open_conection2()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select SUM([subtotal]) as tsubtotal FROM GUIATEMP where [NumGuia] ='" & obj._OBNumGuiaAle & "'"
        cmd = New SqlClient.SqlCommand(sql, cnn2)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim row As DataRow
            row = dt.Rows(0)
            If Not IsDBNull(row("tsubtotal")) Then
                obj._OBSubTotal = row("tsubtotal")
            Else
                obj._OBSubTotal = Nothing
            End If
        End If
        cnn2.Close()
    End Sub
    Private Sub Calcular_ComiZona10()
        open_conection2()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select monto from COMISIONZ10"
        cmd = New SqlClient.SqlCommand(sql, cnn2)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim row As DataRow
            row = dt.Rows(0)
            obj._OBComiZ10 = row("monto")
        End If
        cnn2.Close()
    End Sub
    Private Sub Calcular_ComiSeguro()
        open_conection2()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select monto from COMISIONSEGUR"
        cmd = New SqlClient.SqlCommand(sql, cnn2)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim row As DataRow
            row = dt.Rows(0)
            obj._OBComiSeguro = row("monto")
        End If
        cnn2.Close()
    End Sub
    Private Sub Calcular_ComiChoferRecolecta()
        open_conection2()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select monto from COMISIONCHOF"
        cmd = New SqlClient.SqlCommand(sql, cnn2)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim row As DataRow
            row = dt.Rows(0)
            obj._OBComiChofer = row("monto")
        End If
        cnn2.Close()
    End Sub
    Private Sub Calcular_ComiOtrChofer()
        open_conection2()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select porcen from COMISIONCHOFOT where [co_zona]='" & obj._OBIDZona & "'"
        cmd = New SqlClient.SqlCommand(sql, cnn2)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim row As DataRow
            row = dt.Rows(0)
            obj._OBComiOtrChofer = row("porcen")
        End If
        cnn2.Close()
    End Sub
    Private Sub Calcular_ComiChoferFletes()
        open_conection2()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select porcen from COMISIONCHOFFL where [co_zona]='" & obj._OBIDZona & "'"
        cmd = New SqlClient.SqlCommand(sql, cnn2)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim row As DataRow
            row = dt.Rows(0)
            obj._OBComiChoferFletes = row("porcen")
        End If
        cnn2.Close()
    End Sub
#End Region
#End Region
#Region "Insert"
    Private Sub Insert_Table()
        'PROFIT
        If obj.Mfiscal = True Then
            files_exist()
            If files_exist() = True Then
                Buscar_Ultima_Fac()
                If Buscar_Ultima_Fac() = False Then
                    obj._impfisfac = ""
                End If
                obj._impfis = My.Settings.impfis
            End If
        Else
            obj._impfis = ""
            obj._impfisfac = ""
        End If
        Insert_Factura_Profit()
        Insert_Reng_Fac_Sec()
        Insert_Guia_Profit()
        Insert_Documcc_Profit()
        Dim s As String = CBCPago.Text
        If (s.Contains("PAGADO PAGADO")) Then
            Insert_Cobros_Profit()
            Insert_Reng_Cob_Profit()
            Insert_Mov_caj_Profit()
            Insert_Reng_Tip_Profit()
        End If
        'Sistema
        Insert_Guia()
        Insert_Guia_Reng()
    End Sub
    Private Sub Insert_Cliente()
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        cmd = New SqlClient.SqlCommand("GUIA_ins_clientes", cnn1)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@co_cli", SqlDbType.Char).Value = TBCCI.Text
        cmd.Parameters.Add("@tipo", SqlDbType.Char).Value = "DE"
        cmd.Parameters.Add("@cli_des", SqlDbType.VarChar).Value = TBCNombre.Text
        cmd.Parameters.Add("@direc1", SqlDbType.Text).Value = TBCDirec1.Text
        cmd.Parameters.Add("@direc2", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@telefonos", SqlDbType.VarChar).Value = TBCTelefono.Text
        cmd.Parameters.Add("@fax", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@inactivo", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@comentario", SqlDbType.Text).Value = ""
        cmd.Parameters.Add("@respons", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@fecha_reg", SqlDbType.SmallDateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@puntaje", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@saldo", SqlDbType.Float).Value = 0
        cmd.Parameters.Add("@saldo_ini", SqlDbType.Float).Value = 0
        cmd.Parameters.Add("@fac_ult_ve", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@fec_ult_ve", SqlDbType.SmallDateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@net_ult_ve", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@mont_cre", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@plaz_pag", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@desc_ppago", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@co_zon", SqlDbType.Char).Value = "PD"
        cmd.Parameters.Add("@co_seg", SqlDbType.Char).Value = "GN"
        cmd.Parameters.Add("@co_ven", SqlDbType.Char).Value = "PD"
        cmd.Parameters.Add("@desc_glob", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@horar_caja", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@frecu_vist", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@lunes", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@martes", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@miercoles", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@jueves", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@viernes", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@sabado", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@domingo", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@dir_ent2", SqlDbType.Text).Value = ""
        cmd.Parameters.Add("@tipo_iva", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@iva", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@rif", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@contribu", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@dis_cen", SqlDbType.Text).Value = ""
        cmd.Parameters.Add("@nit", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@co_ingr", SqlDbType.Char).Value = "I001"
        cmd.Parameters.Add("@campo1", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@campo2", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@campo3", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@campo4", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@campo5", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@campo6", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@campo7", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@campo8", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@co_us_in", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@fe_us_in", SqlDbType.DateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@co_us_mo", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@fe_us_mo", SqlDbType.DateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@co_us_el", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@fe_us_el", SqlDbType.DateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@revisado", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@trasnfe", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@co_sucu", SqlDbType.Char).Value = "CCS"
        cmd.Parameters.Add("@juridico", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@tipo_adi", SqlDbType.Int).Value = 1
        cmd.Parameters.Add("@matriz", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@co_tab", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@tipo_per", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@serialp", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@valido", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@estado", SqlDbType.Char).Value = "A"
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@co_pais", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@ciudad", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@zip", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@login", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@password", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@website", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@salestax", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@sincredito", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@contribu_e", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@porc_esp", SqlDbType.Float).Value = 0
        cmd.ExecuteNonQuery()
        cnn1.Close()
    End Sub
    Private Sub Insert_Factura_Profit()
        Buscar_co_subl2_Profit1()
        Buscar_co_subl2_Profit2()
        If obj.fact_num <> Nothing Then
            Dim nombre, rif, nit As String
            If RBRemitente.Checked Then
                nombre = obj._rifR & " " & obj._cli_desR
                rif = obj._rifR
                nit = obj._nitR
            ElseIf RBDestinatario.Checked Then
                nombre = obj._rifD & " " & obj._cli_desD
                rif = obj._rifD
                nit = obj._nitD
            Else
                nombre = ""
                rif = ""
                nit = ""
            End If
            Dim prec_vta As Object
            If LBTotal.Text <> Nothing Then
                prec_vta = Sumar_DataGridView("SubTotal", DataGridView)
            Else
                prec_vta = 0
            End If
            Dim prec_iva As Object
            If LBIVA.Text <> Nothing Then
                prec_iva = Sumar_DataGridView("IVA", DataGridView)
            Else
                prec_iva = 0
            End If
            obj._zonab = Buscar_zonab()
            Dim mtotal As Object
            If LBTotal.Text <> "0 Bs." Then
                mtotal = Convert.ToDecimal(LBTotal.Text.Replace("Bs", ""))
            Else
                mtotal = 0
            End If
            Dim adicionales As Object
            If LBTotalAdic.Text <> "0 Bs." Then
                adicionales = Convert.ToDecimal(LBTotalAdic.Text.Replace("Bs", ""))
            Else
                adicionales = 0
            End If
            Dim monto As Object
            Dim dis_cen As String
            If obj.Miva = True Then
                monto = prec_vta + adicionales
                dis_cen = "<IVA><1>" & obj._OBIva & "/" & prec_vta & "/" & prec_iva & "</1></IVA>"
            Else
                monto = mtotal
                dis_cen = "<IVA><E>" & prec_iva & "</E></IVA>"
            End If
            open_conection1()
            Dim cmd As New SqlClient.SqlCommand
            cmd = New SqlClient.SqlCommand("GUIA_ins_factura", cnn1)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@fact_num", SqlDbType.Int).Value = obj.fact_num
            cmd.Parameters.Add("@contrib", SqlDbType.Bit).Value = obj.contribu
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = nombre
            cmd.Parameters.Add("@rif", SqlDbType.Char).Value = rif
            cmd.Parameters.Add("@nit", SqlDbType.Char).Value = nit
            cmd.Parameters.Add("@num_control", SqlDbType.Int).Value = obj._guia_num
            cmd.Parameters.Add("@status", SqlDbType.Char).Value = 0
            cmd.Parameters.Add("@comentario", SqlDbType.Text).Value = "<Forma de Pago:>"
            cmd.Parameters.Add("@descrip", SqlDbType.VarChar).Value = 0
            cmd.Parameters.Add("@saldo", SqlDbType.Decimal).Value = prec_vta
            cmd.Parameters.Add("@fec_emis", SqlDbType.SmallDateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
            cmd.Parameters.Add("@fec_venc", SqlDbType.SmallDateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
            cmd.Parameters.Add("@co_cli", SqlDbType.Char).Value = obj.co_cli
            cmd.Parameters.Add("@co_ven", SqlDbType.Char).Value = CBRecolector.SelectedValue
            cmd.Parameters.Add("@co_tran", SqlDbType.Char).Value = CBAyudante1.SelectedValue
            cmd.Parameters.Add("@dir_ent", SqlDbType.Text).Value = obj.direc1
            cmd.Parameters.Add("@forma_pag", SqlDbType.Char).Value = CBCPago.SelectedValue
            cmd.Parameters.Add("@tot_bruto", SqlDbType.Decimal).Value = monto
            cmd.Parameters.Add("@tot_neto", SqlDbType.Decimal).Value = monto
            cmd.Parameters.Add("@glob_desc", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@tot_reca", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@porc_gdesc", SqlDbType.Char).Value = ""
            cmd.Parameters.Add("@porc_reca", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@total_uc", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@total_cp", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@tot_flete", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@monto_dev", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@totklu", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@anulada", SqlDbType.Bit).Value = 0
            cmd.Parameters.Add("@impresa", SqlDbType.Bit).Value = 1
            cmd.Parameters.Add("@iva", SqlDbType.Decimal).Value = prec_iva
            cmd.Parameters.Add("@iva_dev", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@feccom", SqlDbType.SmallDateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
            cmd.Parameters.Add("@numcom", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@tasa", SqlDbType.Decimal).Value = obj._OBIva
            cmd.Parameters.Add("@moneda", SqlDbType.Char).Value = "Bs"
            cmd.Parameters.Add("@dis_cen", SqlDbType.Text).Value = dis_cen
            cmd.Parameters.Add("@vuelto", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@seriales", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@tasag", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@tasag10", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@tasag20", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@campo1", SqlDbType.VarChar).Value = CBAyudante1.Text
            cmd.Parameters.Add("@campo2", SqlDbType.VarChar).Value = CBAyudante2.Text
            cmd.Parameters.Add("@campo3", SqlDbType.VarChar).Value = "CCS;CARACAS"
            cmd.Parameters.Add("@campo4", SqlDbType.VarChar).Value = obj._zonab
            cmd.Parameters.Add("@campo5", SqlDbType.VarChar).Value = "108;CARACAS"
            cmd.Parameters.Add("@campo6", SqlDbType.VarChar).Value = obj._co_subl
            cmd.Parameters.Add("@campo7", SqlDbType.VarChar).Value = "Aprobado"
            cmd.Parameters.Add("@campo8", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@co_us_in", SqlDbType.Char).Value = "001"
            cmd.Parameters.Add("@fe_us_in", SqlDbType.DateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
            cmd.Parameters.Add("@co_us_mo", SqlDbType.Char).Value = "001"
            cmd.Parameters.Add("@fe_us_mo", SqlDbType.DateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
            cmd.Parameters.Add("@co_us_el", SqlDbType.Char).Value = "001"
            cmd.Parameters.Add("@fe_us_el", SqlDbType.DateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
            cmd.Parameters.Add("@revisado", SqlDbType.Char).Value = ""
            cmd.Parameters.Add("@trasnfe", SqlDbType.Char).Value = ""
            cmd.Parameters.Add("@numcon", SqlDbType.Char).Value = ""
            cmd.Parameters.Add("@co_sucu", SqlDbType.Char).Value = "CCS"
            cmd.Parameters.Add("@mon_ilc", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@otros1", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@otros2", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@otros3", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@num_turno", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@aux01", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@aux02", SqlDbType.VarChar).Value = ""
            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = -1
            cmd.Parameters.Add("@salestax", SqlDbType.Char).Value = ""
            cmd.Parameters.Add("@origen", SqlDbType.Char).Value = ""
            cmd.Parameters.Add("@origen_d", SqlDbType.Char).Value = ""
            cmd.Parameters.Add("@sta_prod", SqlDbType.Char).Value = ""
            cmd.Parameters.Add("@fec_reg", SqlDbType.SmallDateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
            cmd.Parameters.Add("@impfis", SqlDbType.Char).Value = obj._impfis
            cmd.Parameters.Add("@impfisfac", SqlDbType.Char).Value = obj._impfisfac
            cmd.Parameters.Add("@imp_nro_z", SqlDbType.Char).Value = ""
            cmd.Parameters.Add("@ven_ter", SqlDbType.Bit).Value = 0
            cmd.Parameters.Add("@ptovta", SqlDbType.Bit).Value = 0
            cmd.Parameters.Add("@telefono", SqlDbType.VarChar).Value = obj._telefonos
            cmd.ExecuteNonQuery()
            cnn1.Close()
        End If
    End Sub
    Private Sub Insert_Reng_Fac_Sec()
        Try
            If obj.fact_num <> Nothing Then
                If CBRecolecta.CheckState = CheckState.Checked Then
                    Insert_Reng_Fac_Rec()
                    Update_pedidos_Profit()
                    reng_num = reng_num + 1
                End If
                Insert_Reng_Fac_Profit()
                reng_num = reng_num + 1
                If (CBComision.CheckState = CheckState.Checked And LBTotalComi.Text <> "0 Bs.") Or LBZ10.Text <> "0 Bs." Or obj._MontoDevFact <> Nothing Then
                    Insert_Reng_Fac_Com()
                    reng_num = reng_num + 1
                End If
                If CBSeguro.CheckState = CheckState.Checked And LBTotalSeguro.Text <> "0 Bs." Then
                    Insert_Reng_Fac_Seg()
                    reng_num = reng_num + 1
                End If
                Dim fpo As Double = Sumar_DataGridView("FPO", DataGridView)
                If fpo <> 0 Then
                    Insert_Reng_Fac_Fpo()
                End If
            End If
        Catch ex As Exception
            MsgBoxError(mensaje:="No se pueden insertar los renglones de la factura PROFIT!", titulo:="ERROR: Validar Facturación - Sistema")
        End Try
    End Sub
    Private Sub Insert_Reng_Fac_Profit()
        Dim prec_vta As Object
        If LBTotal.Text <> Nothing Then
            prec_vta = Sumar_DataGridView("SubTotal", DataGridView)
        Else
            prec_vta = 0
        End If
        Dim tipo_imp As Integer
        If obj.Miva = True Then
            tipo_imp = 1
        ElseIf obj.Miva = False Then
            tipo_imp = 6
        End If
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        cmd = New SqlClient.SqlCommand("GUIA_ins_reng_fac", cnn1)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@fact_num", SqlDbType.Int).Value = obj.fact_num
        cmd.Parameters.Add("@reng_num", SqlDbType.Int).Value = reng_num
        cmd.Parameters.Add("@dis_cen", SqlDbType.Text).Value = ""
        cmd.Parameters.Add("@tipo_doc", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@reng_doc", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@num_doc", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@co_art", SqlDbType.Char).Value = "FLETE"
        cmd.Parameters.Add("@co_alma", SqlDbType.Char).Value = "SERV"
        cmd.Parameters.Add("@total_art", SqlDbType.Decimal).Value = 1
        cmd.Parameters.Add("@stotal_art", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@pendiente", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@uni_venta", SqlDbType.Char).Value = "UND"
        cmd.Parameters.Add("@prec_vta", SqlDbType.Decimal).Value = prec_vta
        cmd.Parameters.Add("@porc_desc", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@tipo_imp", SqlDbType.Char).Value = tipo_imp
        cmd.Parameters.Add("@isv", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@reng_neto", SqlDbType.Decimal).Value = prec_vta
        cmd.Parameters.Add("@cos_pro_un", SqlDbType.Decimal).Value = 1
        cmd.Parameters.Add("@ult_cos_un", SqlDbType.Decimal).Value = 1
        cmd.Parameters.Add("@ult_cos_om", SqlDbType.Decimal).Value = 1
        cmd.Parameters.Add("@cos_pro_om", SqlDbType.Decimal).Value = 1
        cmd.Parameters.Add("@total_dev", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@monto_dev", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@prec_vta2", SqlDbType.Decimal).Value = 1
        cmd.Parameters.Add("@anulado", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@des_art", SqlDbType.Text).Value = ""
        cmd.Parameters.Add("@seleccion", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@cant_imp", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@comentario", SqlDbType.Text).Value = CBContenido.Text
        cmd.Parameters.Add("@total_uni", SqlDbType.Decimal).Value = 1
        cmd.Parameters.Add("@mon_ilc", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@otros", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@nro_lote", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@fec_lote", SqlDbType.SmallDateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@pendiente2", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@tipo_doc2", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@reng_doc2", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@num_doc2", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@tipo_prec", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@co_alma2", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@aux01", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@aux02", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@cant_prod", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@imp_prod", SqlDbType.Decimal).Value = 0
        cmd.ExecuteNonQuery()
        cnn1.Close()
    End Sub
    Private Sub Insert_Reng_Fac_Rec()
        Dim num_doc As Integer
        num_doc = CBoRecolecta.SelectedValue
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        cmd = New SqlClient.SqlCommand("GUIA_ins_reng_fac", cnn1)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@fact_num", SqlDbType.Int).Value = obj.fact_num
        cmd.Parameters.Add("@reng_num", SqlDbType.Int).Value = 1
        cmd.Parameters.Add("@dis_cen", SqlDbType.Text).Value = ""
        cmd.Parameters.Add("@tipo_doc", SqlDbType.Char).Value = "P"
        cmd.Parameters.Add("@reng_doc", SqlDbType.Int).Value = 1
        cmd.Parameters.Add("@num_doc", SqlDbType.Int).Value = num_doc
        cmd.Parameters.Add("@co_art", SqlDbType.Char).Value = "RECOLECTA"
        cmd.Parameters.Add("@co_alma", SqlDbType.Char).Value = "SERV"
        cmd.Parameters.Add("@total_art", SqlDbType.Decimal).Value = 1
        cmd.Parameters.Add("@stotal_art", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@pendiente", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@uni_venta", SqlDbType.Char).Value = "RPQ/SB"
        cmd.Parameters.Add("@prec_vta", SqlDbType.Decimal).Value = 0.01
        cmd.Parameters.Add("@porc_desc", SqlDbType.Char).Value = "99.00"
        cmd.Parameters.Add("@tipo_imp", SqlDbType.Char).Value = 6
        cmd.Parameters.Add("@isv", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@reng_neto", SqlDbType.Decimal).Value = 0.01
        cmd.Parameters.Add("@cos_pro_un", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@ult_cos_un", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@ult_cos_om", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@cos_pro_om", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@total_dev", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@monto_dev", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@prec_vta2", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@anulado", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@des_art", SqlDbType.Text).Value = ""
        cmd.Parameters.Add("@seleccion", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@cant_imp", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@comentario", SqlDbType.Text).Value = ""
        cmd.Parameters.Add("@total_uni", SqlDbType.Decimal).Value = 1
        cmd.Parameters.Add("@mon_ilc", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@otros", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@nro_lote", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@fec_lote", SqlDbType.SmallDateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@pendiente2", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@tipo_doc2", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@reng_doc2", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@num_doc2", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@tipo_prec", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@co_alma2", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@aux01", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@aux02", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@cant_prod", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@imp_prod", SqlDbType.Decimal).Value = 0
        cmd.ExecuteNonQuery()
        cnn1.Close()
    End Sub
    Private Sub Insert_Reng_Fac_Fpo()
        Dim prec_fpo As Object
        If LBFPO.Text <> Nothing Then
            prec_fpo = Sumar_DataGridView("FPO", DataGridView)
        Else
            prec_fpo = 0
        End If
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        cmd = New SqlClient.SqlCommand("GUIA_ins_reng_fac", cnn1)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@fact_num", SqlDbType.Int).Value = obj.fact_num
        cmd.Parameters.Add("@reng_num", SqlDbType.Int).Value = reng_num
        cmd.Parameters.Add("@dis_cen", SqlDbType.Text).Value = ""
        cmd.Parameters.Add("@tipo_doc", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@reng_doc", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@num_doc", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@co_art", SqlDbType.Char).Value = "FP"
        cmd.Parameters.Add("@co_alma", SqlDbType.Char).Value = "SERV"
        cmd.Parameters.Add("@total_art", SqlDbType.Decimal).Value = 1
        cmd.Parameters.Add("@stotal_art", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@pendiente", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@uni_venta", SqlDbType.Char).Value = "UND"
        cmd.Parameters.Add("@prec_vta", SqlDbType.Decimal).Value = prec_fpo
        cmd.Parameters.Add("@porc_desc", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@tipo_imp", SqlDbType.Char).Value = 6
        cmd.Parameters.Add("@isv", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@reng_neto", SqlDbType.Decimal).Value = prec_fpo
        cmd.Parameters.Add("@cos_pro_un", SqlDbType.Decimal).Value = 1
        cmd.Parameters.Add("@ult_cos_un", SqlDbType.Decimal).Value = 1
        cmd.Parameters.Add("@ult_cos_om", SqlDbType.Decimal).Value = 1
        cmd.Parameters.Add("@cos_pro_om", SqlDbType.Decimal).Value = 1
        cmd.Parameters.Add("@total_dev", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@monto_dev", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@prec_vta2", SqlDbType.Decimal).Value = 1
        cmd.Parameters.Add("@anulado", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@des_art", SqlDbType.Text).Value = ""
        cmd.Parameters.Add("@seleccion", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@cant_imp", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@comentario", SqlDbType.Text).Value = CBContenido.Text
        cmd.Parameters.Add("@total_uni", SqlDbType.Decimal).Value = 1
        cmd.Parameters.Add("@mon_ilc", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@otros", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@nro_lote", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@fec_lote", SqlDbType.SmallDateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@pendiente2", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@tipo_doc2", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@reng_doc2", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@num_doc2", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@tipo_prec", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@co_alma2", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@aux01", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@aux02", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@cant_prod", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@imp_prod", SqlDbType.Decimal).Value = 0
        cmd.ExecuteNonQuery()
        cnn1.Close()
    End Sub
    Private Sub Insert_Reng_Fac_Com()
        Dim totalcomision As Decimal
        Dim comision As Object
        If LBTotalComi.Text <> "0 Bs." Then
            comision = Convert.ToDecimal(LBTotalComi.Text.Replace("Bs", ""))
        Else
            comision = 0
        End If
        Dim porlamar As Object
        If LBZ10.Text <> "0 Bs." Then
            porlamar = Convert.ToDecimal(LBZ10.Text.Replace("Bs", ""))
        Else
            porlamar = 0
        End If
        If obj._MontoDevFact <> Nothing Then
            totalcomision = obj._MontoDevFact + porlamar + comision
        Else
            totalcomision = porlamar + comision
        End If
        Dim tipo_imp As Integer
        If obj.Miva = True Then
            tipo_imp = 1
        ElseIf obj.Miva = False Then
            tipo_imp = 6
        End If
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        cmd = New SqlClient.SqlCommand("GUIA_ins_reng_fac", cnn1)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@fact_num", SqlDbType.Int).Value = obj.fact_num
        cmd.Parameters.Add("@reng_num", SqlDbType.Int).Value = reng_num
        cmd.Parameters.Add("@dis_cen", SqlDbType.Text).Value = ""
        cmd.Parameters.Add("@tipo_doc", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@reng_doc", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@num_doc", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@co_art", SqlDbType.Char).Value = "COMISION"
        cmd.Parameters.Add("@co_alma", SqlDbType.Char).Value = "SERV"
        cmd.Parameters.Add("@total_art", SqlDbType.Decimal).Value = 1
        cmd.Parameters.Add("@stotal_art", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@pendiente", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@uni_venta", SqlDbType.Char).Value = "UND"
        cmd.Parameters.Add("@prec_vta", SqlDbType.Decimal).Value = totalcomision
        cmd.Parameters.Add("@porc_desc", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@tipo_imp", SqlDbType.Char).Value = tipo_imp
        cmd.Parameters.Add("@isv", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@reng_neto", SqlDbType.Decimal).Value = totalcomision
        cmd.Parameters.Add("@cos_pro_un", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@ult_cos_un", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@ult_cos_om", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@cos_pro_om", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@total_dev", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@monto_dev", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@prec_vta2", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@anulado", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@des_art", SqlDbType.Text).Value = ""
        cmd.Parameters.Add("@seleccion", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@cant_imp", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@comentario", SqlDbType.Text).Value = ""
        cmd.Parameters.Add("@total_uni", SqlDbType.Decimal).Value = 1
        cmd.Parameters.Add("@mon_ilc", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@otros", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@nro_lote", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@fec_lote", SqlDbType.SmallDateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@pendiente2", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@tipo_doc2", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@reng_doc2", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@num_doc2", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@tipo_prec", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@co_alma2", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@aux01", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@aux02", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@cant_prod", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@imp_prod", SqlDbType.Decimal).Value = 0
        cmd.ExecuteNonQuery()
        cnn1.Close()
    End Sub
    Private Sub Insert_Reng_Fac_Seg()
        Dim seguro As Object
        If LBTotalSeguro.Text <> "0 Bs." Then
            seguro = Convert.ToDecimal(LBTotalSeguro.Text.Replace("Bs", ""))
        Else
            seguro = 0
        End If
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        cmd = New SqlClient.SqlCommand("GUIA_ins_reng_fac", cnn1)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@fact_num", SqlDbType.Int).Value = obj.fact_num
        cmd.Parameters.Add("@reng_num", SqlDbType.Int).Value = reng_num
        cmd.Parameters.Add("@dis_cen", SqlDbType.Text).Value = ""
        cmd.Parameters.Add("@tipo_doc", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@reng_doc", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@num_doc", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@co_art", SqlDbType.Char).Value = "SEGURO"
        cmd.Parameters.Add("@co_alma", SqlDbType.Char).Value = "SERV"
        cmd.Parameters.Add("@total_art", SqlDbType.Decimal).Value = 1
        cmd.Parameters.Add("@stotal_art", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@pendiente", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@uni_venta", SqlDbType.Char).Value = "UND"
        cmd.Parameters.Add("@prec_vta", SqlDbType.Decimal).Value = seguro
        cmd.Parameters.Add("@porc_desc", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@tipo_imp", SqlDbType.Char).Value = 6
        cmd.Parameters.Add("@isv", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@reng_neto", SqlDbType.Decimal).Value = seguro
        cmd.Parameters.Add("@cos_pro_un", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@ult_cos_un", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@ult_cos_om", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@cos_pro_om", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@total_dev", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@monto_dev", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@prec_vta2", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@anulado", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@des_art", SqlDbType.Text).Value = ""
        cmd.Parameters.Add("@seleccion", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@cant_imp", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@comentario", SqlDbType.Text).Value = ""
        cmd.Parameters.Add("@total_uni", SqlDbType.Decimal).Value = 1
        cmd.Parameters.Add("@mon_ilc", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@otros", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@nro_lote", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@fec_lote", SqlDbType.SmallDateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@pendiente2", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@tipo_doc2", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@reng_doc2", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@num_doc2", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@tipo_prec", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@co_alma2", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@aux01", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@aux02", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@cant_prod", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@imp_prod", SqlDbType.Decimal).Value = 0
        cmd.ExecuteNonQuery()
        cnn1.Close()
    End Sub
    Private Sub Insert_Guia_Profit()
        Buscar_co_subl2_Profit1()
        Buscar_co_subl2_Profit2()
        'Try
        If obj.fact_num <> Nothing Then
            Dim nombre As String
            If RBRemitente.Checked Then
                nombre = obj._rifR & " " & obj._cli_desR
            ElseIf RBDestinatario.Checked Then
                nombre = obj._rifD & " " & obj._cli_desD
            Else
                nombre = ""
            End If
            Dim ped_num As Object
            If CBRecolecta.CheckState = CheckState.Checked Then
                ped_num = CBoRecolecta.SelectedValue
            Else
                ped_num = 0
            End If

            If TBGuiaCarga.Text <> Nothing Then
                'ped_num = CInt(TBRecolecta.Text) '''''''
            Else
                ' ped_num = 0
            End If
            Dim tot_seg As Object
            If TBMontoSegu.Text <> Nothing Then
                Dim cantidad As Double = TBMontoSegu.Text
                Format(cantidad, "##0.00")
                tot_seg = cantidad.ToString("##0.00").Replace(",", ".")
            Else
                tot_seg = 0
            End If

            Dim porlamar As Object
            If LBZ10.Text <> "0 Bs." Then
                porlamar = Convert.ToDecimal(LBZ10.Text.Replace("Bs", ""))
            Else
                porlamar = 0
            End If
            Dim adicionales As Object
            If LBTotalAdic.Text <> "0 Bs." Then
                adicionales = Convert.ToDecimal(LBTotalAdic.Text.Replace("Bs", ""))
            Else
                adicionales = 0
            End If
            Dim tot_piezas As Integer = CInt(LBNBultos.Text)
            Dim peso As Object
            If LBTPeso.Text <> Nothing Then
                peso = Sumar_DataGridView("Peso", DataGridView)
            Else
                peso = 0
            End If
            Dim prec_vta As Object
            If LBTotal.Text <> Nothing Then
                prec_vta = Sumar_DataGridView("SubTotal", DataGridView)
            Else
                prec_vta = 0
            End If
            Dim prec_fpo As Object
            If LBFPO.Text <> Nothing Then
                prec_fpo = Sumar_DataGridView("FPO", DataGridView)
            Else
                prec_fpo = 0
            End If
            Dim tot_neto As Decimal
            tot_neto = prec_fpo + prec_vta + adicionales
            Dim nombrecons As String
            If TBNombreCliente.Text <> Nothing Then
                nombrecons = TBNombreCliente.Text
            Else
                nombrecons = ""
            End If
            Dim cicons As Integer
            If TBCCI.Text <> Nothing Then
                cicons = CInt(TBCI.Text)
            Else
                cicons = 0
            End If
            Dim textocons As String
            If TBTexto.Text <> Nothing Then
                textocons = CStr(TBTexto.Text)
            Else
                textocons = ""
            End If
            open_conection1()
            Dim cmd As New SqlClient.SqlCommand
            cmd = New SqlClient.SqlCommand("GUIA_ins_guia", cnn1)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@fact_num", SqlDbType.Int).Value = obj.fact_num
            cmd.Parameters.Add("@ped_num", SqlDbType.Int).Value = ped_num
            cmd.Parameters.Add("@ctrl_num", SqlDbType.Char).Value = TBNControl.Text
            cmd.Parameters.Add("@guia_num", SqlDbType.Int).Value = obj._guia_num
            cmd.Parameters.Add("@doc_num", SqlDbType.Int).Value = 0
            cmd.Parameters.Add("@fec_emis", SqlDbType.SmallDateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
            cmd.Parameters.Add("@co_dest", SqlDbType.Char).Value = CBDestinatario.SelectedValue
            cmd.Parameters.Add("@for_pago", SqlDbType.Char).Value = "Efectivo"
            cmd.Parameters.Add("@co_cond", SqlDbType.Char).Value = CBCPago.SelectedValue
            cmd.Parameters.Add("@co_tip_c", SqlDbType.Char).Value = CBCPago.SelectedValue
            cmd.Parameters.Add("@co_remit", SqlDbType.Char).Value = CBRemitente.SelectedValue
            cmd.Parameters.Add("@co_ven", SqlDbType.Char).Value = CBRecolector.SelectedValue
            cmd.Parameters.Add("@co_tran", SqlDbType.Char).Value = CBAyudante1.SelectedValue
            cmd.Parameters.Add("@co_ayu1", SqlDbType.Char).Value = CBAyudante2.SelectedValue
            cmd.Parameters.Add("@co_ayu2", SqlDbType.Char).Value = "NAPL"
            cmd.Parameters.Add("@co_proc", SqlDbType.Char).Value = CBContenido.SelectedValue
            cmd.Parameters.Add("@co_lin", SqlDbType.Char).Value = "CCS"
            cmd.Parameters.Add("@co_subl", SqlDbType.Char).Value = "108"
            cmd.Parameters.Add("@co_lin2", SqlDbType.Char).Value = obj._zonab.Substring(0, 3) '"MCY"
            cmd.Parameters.Add("@co_subl2", SqlDbType.Char).Value = obj._co_subl
            cmd.Parameters.Add("@tot_seguro", SqlDbType.Decimal).Value = tot_seg
            cmd.Parameters.Add("@status_seg", SqlDbType.Char).Value = ""
            cmd.Parameters.Add("@valor_cod", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@comision", SqlDbType.Decimal).Value = adicionales
            cmd.Parameters.Add("@sellada", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = nombrecons
            cmd.Parameters.Add("@cedula", SqlDbType.Int).Value = cicons
            cmd.Parameters.Add("@coment", SqlDbType.Text).Value = textocons
            cmd.Parameters.Add("@tot_piezas", SqlDbType.Decimal).Value = tot_piezas
            cmd.Parameters.Add("@empaque", SqlDbType.Char).Value = 1
            cmd.Parameters.Add("@empaque2", SqlDbType.Char).Value = 1
            cmd.Parameters.Add("@empaque3", SqlDbType.Char).Value = 1
            cmd.Parameters.Add("@empaque4", SqlDbType.Char).Value = 1
            cmd.Parameters.Add("@empaque5", SqlDbType.Char).Value = 1
            cmd.Parameters.Add("@empaque6", SqlDbType.Char).Value = 1
            cmd.Parameters.Add("@empaque7", SqlDbType.Char).Value = 1
            cmd.Parameters.Add("@empaque8", SqlDbType.Char).Value = 1
            cmd.Parameters.Add("@empaque9", SqlDbType.Char).Value = 1
            cmd.Parameters.Add("@kilos", SqlDbType.Decimal).Value = peso
            cmd.Parameters.Add("@kilos2", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@kilos3", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@kilos4", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@kilos5", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@kilos6", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@kilos7", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@kilos8", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@kilos9", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@volum", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@volum2", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@volum3", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@volum4", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@volum5", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@volum6", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@volum7", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@volum8", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@volum9", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@gramos", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@gramos2", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@gramos3", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@gramos4", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@gramos5", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@gramos6", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@gramos7", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@gramos8", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@gramos9", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@tot_bult", SqlDbType.Decimal).Value = tot_piezas
            cmd.Parameters.Add("@tot_paq", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@tot_sob", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@tot_kilo", SqlDbType.Decimal).Value = peso
            cmd.Parameters.Add("@tot_vol", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@tot_gr_paq", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@tot_gr_sob", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@flete", SqlDbType.Decimal).Value = prec_vta
            cmd.Parameters.Add("@guia", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@comision2", SqlDbType.Decimal).Value = adicionales
            cmd.Parameters.Add("@comi_cod", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@tot_bruto", SqlDbType.Decimal).Value = prec_vta
            cmd.Parameters.Add("@iva", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@seguro", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@franqueo", SqlDbType.Decimal).Value = prec_fpo
            cmd.Parameters.Add("@tot_neto", SqlDbType.Decimal).Value = tot_neto
            cmd.Parameters.Add("@co_us_in", SqlDbType.Char).Value = "C00013"
            cmd.Parameters.Add("@fe_us_in", SqlDbType.DateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
            cmd.Parameters.Add("@fe_us_mo", SqlDbType.DateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
            cmd.Parameters.Add("@fe_us_el", SqlDbType.DateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
            cmd.Parameters.Add("@co_sucu", SqlDbType.Char).Value = "CCS"
            cmd.Parameters.Add("@aux01", SqlDbType.Decimal).Value = 0
            cmd.Parameters.Add("@demp", SqlDbType.Int).Value = 1
            cmd.Parameters.Add("@status", SqlDbType.Char).Value = "0"
            cmd.Parameters.Add("@cortesia", SqlDbType.Bit).Value = 0
            cmd.Parameters.Add("@transfe", SqlDbType.Bit).Value = 0
            cmd.Parameters.Add("@dir_reparto", SqlDbType.Text).Value = ""
            cmd.Parameters.Add("@zon_reparto", SqlDbType.VarChar).Value = ""
            cmd.ExecuteNonQuery()
            cnn1.Close()
        End If
        'Catch ex As Exception
        '    MsgBoxError(ex.Message, titulo:="ERROR: No se puede insertar los datos de la Guía-Profit - Sistema")
        '    'Exit Sub
        'End Try
    End Sub
    Private Sub Insert_Documcc_Profit()
        Dim cobrar As String
        If RBRemitente.Checked Then
            cobrar = CBRemitente.SelectedValue
        ElseIf RBDestinatario.Checked Then
            cobrar = CBDestinatario.SelectedValue
        Else
            cobrar = ""
        End If
        Dim monto_bru As Object
        If LBTotal.Text <> Nothing Then
            monto_bru = Sumar_DataGridView("SubTotal", DataGridView)
        Else
            monto_bru = 0
        End If
        Dim mtotal As Object
        If LBTotal.Text <> "0 Bs." Then
            mtotal = Convert.ToDecimal(LBTotal.Text.Replace("Bs", ""))
        Else
            mtotal = 0
        End If
        Dim tasa As Decimal
        Dim monto As Object
        If obj.Miva = True Then
            monto = monto_bru
            tasa = 1
        Else
            monto = mtotal
            tasa = 6
        End If
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        cmd = New SqlClient.SqlCommand("GUIA_ins_docum_cc", cnn1)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@tipo_doc", SqlDbType.Char).Value = "FACT"
        cmd.Parameters.Add("@nro_doc", SqlDbType.Int).Value = obj.fact_num
        cmd.Parameters.Add("@anulado", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@movi", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@aut", SqlDbType.Bit).Value = 1
        cmd.Parameters.Add("@num_control", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@co_cli", SqlDbType.Char).Value = cobrar
        cmd.Parameters.Add("@contrib", SqlDbType.Bit).Value = 1
        cmd.Parameters.Add("@fec_emis", SqlDbType.SmallDateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@fec_venc", SqlDbType.SmallDateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@observa", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@doc_orig", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@nro_orig", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@co_ban", SqlDbType.Char).Value = 0
        cmd.Parameters.Add("@nro_che", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@co_ven", SqlDbType.Char).Value = CBRecolector.SelectedValue
        cmd.Parameters.Add("@tipo", SqlDbType.Char).Value = "1"
        cmd.Parameters.Add("@tasa", SqlDbType.Decimal).Value = tasa
        cmd.Parameters.Add("@moneda", SqlDbType.Char).Value = "BS"
        cmd.Parameters.Add("@monto_imp", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@monto_gen", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@monto_a1", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@monto_a2", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@monto_bru", SqlDbType.Decimal).Value = monto
        cmd.Parameters.Add("@descuentos", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@monto_des", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@recargo", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@monto_rec", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@monto_otr", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@monto_net", SqlDbType.Decimal).Value = monto
        cmd.Parameters.Add("@saldo", SqlDbType.Decimal).Value = monto
        cmd.Parameters.Add("@feccom", SqlDbType.SmallDateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@numcom", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@dis_cen", SqlDbType.Text).Value = "<IVA><E>" & monto & "</E> </IVA>"
        cmd.Parameters.Add("@comis1", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@comis2", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@comis3", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@comis4", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@adicional", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@campo1", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@campo2", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@campo3", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@campo4", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@campo5", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@campo6", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@campo7", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@campo8", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@co_us_in", SqlDbType.Char).Value = "C00013"
        cmd.Parameters.Add("@fe_us_in", SqlDbType.DateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@co_us_mo", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@fe_us_mo", SqlDbType.DateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@co_us_el", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@fe_us_el", SqlDbType.DateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@revisado", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@trasnfe", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@numcon", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@co_sucu", SqlDbType.Char).Value = "CCS"
        cmd.Parameters.Add("@mon_ilc", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@otros1", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@otros2", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@otros3", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@reng_si", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@comis5", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@comis6", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@aux01", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@aux02", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@salestax", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@origen", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@origen_d", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@fec_reg", SqlDbType.SmallDateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@prov_ter", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@reng_ter", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@impfis", SqlDbType.Char).Value = obj._impfis
        cmd.Parameters.Add("@impfisfac", SqlDbType.Char).Value = obj._impfisfac
        cmd.Parameters.Add("@imp_nro_z", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@ven_ter", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@fcomproban", SqlDbType.SmallDateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@PtoVta", SqlDbType.Bit).Value = 0
        cmd.ExecuteNonQuery()
        cnn1.Close()
    End Sub
    Private Sub Insert_Cobros_Profit()
        Buscar_CobroNum()
        Dim cobrar As String
        If RBRemitente.Checked Then
            cobrar = CBRemitente.SelectedValue
        ElseIf RBDestinatario.Checked Then
            cobrar = CBDestinatario.SelectedValue
        Else
            cobrar = ""
        End If
        Dim prec_vta As Object
        If LBTotal.Text <> Nothing Then
            prec_vta = Sumar_DataGridView("SubTotal", DataGridView)
        Else
            prec_vta = 0
        End If
        Dim prec_fpo As Object
        If LBFPO.Text <> Nothing Then
            prec_fpo = Sumar_DataGridView("FPO", DataGridView)
        Else
            prec_fpo = 0
        End If
        Dim adicionales As Object
        If LBTotalAdic.Text <> "0 Bs." Then
            adicionales = Convert.ToDecimal(LBTotalAdic.Text.Replace("Bs", ""))
        Else
            adicionales = 0
        End If
        Dim tot_neto As Decimal
        tot_neto = prec_fpo + prec_vta + adicionales
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        cmd = New SqlClient.SqlCommand("GUIA_ins_cobros", cnn1)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@cob_num", SqlDbType.Int).Value = obj._cobro_num
        cmd.Parameters.Add("@recibo", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@co_cli", SqlDbType.Char).Value = cobrar
        cmd.Parameters.Add("@co_ven", SqlDbType.Char).Value = "9999"
        cmd.Parameters.Add("@fec_cob", SqlDbType.SmallDateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@anulado", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@monto", SqlDbType.Decimal).Value = tot_neto
        cmd.Parameters.Add("@dppago", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@mont_ncr", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@ncr", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@tcomi_porc", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@tcomi_line", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@tcomi_art", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@tcomi_conc", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@feccom", SqlDbType.SmallDateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@tasa", SqlDbType.Decimal).Value = 1
        cmd.Parameters.Add("@moneda", SqlDbType.Char).Value = "BS"
        cmd.Parameters.Add("@numcom", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@dis_cen", SqlDbType.Text).Value = ""
        cmd.Parameters.Add("@campo1", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@campo2", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@campo3", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@campo4", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@campo5", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@campo6", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@campo7", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@campo8", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@co_us_in", SqlDbType.Char).Value = "C00013"
        cmd.Parameters.Add("@fe_us_in", SqlDbType.DateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@co_us_mo", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@fe_us_mo", SqlDbType.DateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@co_us_el", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@fe_us_el", SqlDbType.DateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@recargo", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@adel_num", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@revisado", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@trasnfe", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@co_sucu", SqlDbType.Char).Value = "CCS"
        cmd.Parameters.Add("@descrip", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@num_dev", SqlDbType.Char).Value = 0
        cmd.Parameters.Add("@devdinero", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@num_turno", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@aux01", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@aux02", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@origen", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@origen_d", SqlDbType.Char).Value = ""
        cmd.ExecuteNonQuery()
        cnn1.Close()
    End Sub
    Private Sub Insert_Reng_Cob_Profit()
        Dim prec_vta As Object
        If LBTotal.Text <> Nothing Then
            prec_vta = Sumar_DataGridView("SubTotal", DataGridView)
        Else
            prec_vta = 0
        End If
        Dim prec_fpo As Object
        If LBFPO.Text <> Nothing Then
            prec_fpo = Sumar_DataGridView("FPO", DataGridView)
        Else
            prec_fpo = 0
        End If
        Dim adicionales As Object
        If LBTotalAdic.Text <> "0 Bs." Then
            adicionales = Convert.ToDecimal(LBTotalAdic.Text.Replace("Bs", ""))
        Else
            adicionales = 0
        End If
        Dim tot_neto As Decimal
        tot_neto = prec_fpo + prec_vta + adicionales
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        cmd = New SqlClient.SqlCommand("GUIA_ins_reng_cob", cnn1)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@cob_num", SqlDbType.Int).Value = obj._cobro_num
        cmd.Parameters.Add("@reng_num", SqlDbType.Int).Value = 1
        cmd.Parameters.Add("@tp_doc_cob", SqlDbType.Char).Value = "FACT"
        cmd.Parameters.Add("@doc_num", SqlDbType.Int).Value = obj.fact_num
        cmd.Parameters.Add("@neto", SqlDbType.Decimal).Value = tot_neto
        cmd.Parameters.Add("@neto_tmp", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@dppago", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@dppago_tmp", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@reng_ncr", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@co_ven", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@comis1", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@comis2", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@comis3", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@comis4", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@sign_aju_c", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@porc_aju_c", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@por_cob", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@comi_cob", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@mont_cob", SqlDbType.Decimal).Value = tot_neto
        cmd.Parameters.Add("@sino_pago", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@sino_reten", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@monto_dppago", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@monto_reten", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@imp_pago", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@monto_obj", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@isv", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@nro_fact", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@moneda", SqlDbType.Char).Value = "BS"
        cmd.Parameters.Add("@tasa", SqlDbType.Decimal).Value = 1
        cmd.Parameters.Add("@numcon", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@sustraen", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@co_islr", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@fec_emis", SqlDbType.SmallDateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@fec_venc", SqlDbType.SmallDateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@comis5", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@comis6", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@fact_iva", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@ret_iva", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@porc_retn", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@porc_desc", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@aux01", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@aux02", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@prov_ter", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@reng_ter", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@fec_com", SqlDbType.SmallDateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.ExecuteNonQuery()
        cnn1.Close()
    End Sub
    Private Sub Insert_Mov_caj_Profit()
        Buscar_MovcajNum()
        Dim nombre As String
        If RBRemitente.Checked Then
            nombre = obj._cli_desR
        ElseIf RBDestinatario.Checked Then
            nombre = obj._cli_desD
        Else
            nombre = ""
        End If
        Dim prec_vta As Object
        If LBTotal.Text <> Nothing Then
            prec_vta = Sumar_DataGridView("SubTotal", DataGridView)
        Else
            prec_vta = 0
        End If
        Dim prec_fpo As Object
        If LBFPO.Text <> Nothing Then
            prec_fpo = Sumar_DataGridView("FPO", DataGridView)
        Else
            prec_fpo = 0
        End If
        Dim adicionales As Object
        If LBTotalAdic.Text <> "0 Bs." Then
            adicionales = Convert.ToDecimal(LBTotalAdic.Text.Replace("Bs", ""))
        Else
            adicionales = 0
        End If
        Dim tot_neto As Decimal
        tot_neto = prec_fpo + prec_vta + adicionales

        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        cmd = New SqlClient.SqlCommand("GUIA_ins_mov_caj", cnn1)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@mov_num", SqlDbType.Int).Value = obj.movcaj_num
        cmd.Parameters.Add("@codigo", SqlDbType.Char).Value = "01"
        cmd.Parameters.Add("@dep_num", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@reng_num", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@mov_afec", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@mon_dep", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@origen", SqlDbType.Char).Value = "COB"
        cmd.Parameters.Add("@tipo_op", SqlDbType.Char).Value = "I"
        cmd.Parameters.Add("@forma_pag", SqlDbType.Char).Value = "EF"
        cmd.Parameters.Add("@fecha", SqlDbType.SmallDateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@doc_num", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@descrip", SqlDbType.VarChar).Value = "Cobro: " & obj._cobro_num & " de " & nombre
        cmd.Parameters.Add("@monto_d", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@monto_h", SqlDbType.Decimal).Value = tot_neto
        cmd.Parameters.Add("@cta_egre", SqlDbType.Char).Value = "I001"
        cmd.Parameters.Add("@cob_pag", SqlDbType.Int).Value = obj.cobro_num
        cmd.Parameters.Add("@ori_dep", SqlDbType.Bit).Value = 1
        cmd.Parameters.Add("@dep_con", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@banc_tarj", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@cod_ingben", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@fecha_che", SqlDbType.SmallDateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@feccom", SqlDbType.SmallDateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@numcom", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@dis_cen", SqlDbType.Text).Value = ""
        cmd.Parameters.Add("@moneda", SqlDbType.Char).Value = "BS"
        cmd.Parameters.Add("@tasa", SqlDbType.Decimal).Value = 1
        cmd.Parameters.Add("@co_us_in", SqlDbType.Char).Value = "C00013"
        cmd.Parameters.Add("@fe_us_in", SqlDbType.DateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@co_us_mo", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@fe_us_mo", SqlDbType.DateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@co_us_el", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@fe_us_el", SqlDbType.DateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@revisado", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@trasnfe", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@co_sucu", SqlDbType.Char).Value = "CCS"
        cmd.Parameters.Add("@anulado", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@num_turno", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@movt_ori", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@movt_gen", SqlDbType.Int).Value = 0
        cmd.Parameters.Add("@tracaja", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@operador", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@clave", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@moneda2", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@tasa2", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@aux01", SqlDbType.Decimal).Value = 0
        cmd.Parameters.Add("@aux02", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@campo1", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@campo2", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@campo3", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@campo4", SqlDbType.VarChar).Value = ""
        cmd.Parameters.Add("@doc_sel", SqlDbType.Bit).Value = 0
        cmd.ExecuteNonQuery()
        cnn1.Close()
    End Sub
    Private Sub Insert_Reng_Tip_Profit()
        Dim tip_cob, num_doc As String
        If CBCheque.CheckState = CheckState.Checked Then
            tip_cob = "CHEQ"
            num_doc = TBCheque.Text
        Else
            tip_cob = "EFEC"
            num_doc = ""
        End If
        Dim prec_vta As Object
        If LBTotal.Text <> Nothing Then
            prec_vta = Sumar_DataGridView("SubTotal", DataGridView)
        Else
            prec_vta = 0
        End If
        Dim prec_fpo As Object
        If LBFPO.Text <> Nothing Then
            prec_fpo = Sumar_DataGridView("FPO", DataGridView)
        Else
            prec_fpo = 0
        End If
        Dim adicionales As Object
        If LBTotalAdic.Text <> "0 Bs." Then
            adicionales = Convert.ToDecimal(LBTotalAdic.Text.Replace("Bs", ""))
        Else
            adicionales = 0
        End If
        Dim tot_neto As Decimal
        tot_neto = prec_fpo + prec_vta + adicionales
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        cmd = New SqlClient.SqlCommand("GUIA_ins_reng_tip", cnn1)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@cob_num", SqlDbType.Int).Value = obj.cobro_num
        cmd.Parameters.Add("@reng_num", SqlDbType.Int).Value = 1
        cmd.Parameters.Add("@tip_cob", SqlDbType.Char).Value = tip_cob
        cmd.Parameters.Add("@movi", SqlDbType.Int).Value = obj.movcaj_num
        cmd.Parameters.Add("@num_doc", SqlDbType.Char).Value = num_doc
        cmd.Parameters.Add("@mont_doc", SqlDbType.Decimal).Value = tot_neto
        cmd.Parameters.Add("@mont_tmp", SqlDbType.Decimal).Value = tot_neto
        cmd.Parameters.Add("@moneda", SqlDbType.Char).Value = "BS"
        cmd.Parameters.Add("@banco", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@cod_caja", SqlDbType.Char).Value = "01"
        cmd.Parameters.Add("@des_caja", SqlDbType.Char).Value = "CAJA PRINCIPAL"
        cmd.Parameters.Add("@fec_cheq", SqlDbType.SmallDateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
        cmd.Parameters.Add("@nombre_ban", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@numero", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@devuelto", SqlDbType.Bit).Value = 0
        cmd.Parameters.Add("@operador", SqlDbType.Char).Value = ""
        cmd.Parameters.Add("@clave", SqlDbType.Char).Value = ""
        cmd.ExecuteNonQuery()
        cnn1.Close()
    End Sub
    'SISTEMA FLETESGAG
    Private Sub Insert_Guia()
        If obj.fact_num <> Nothing Then
            Dim facturar As String
            If RBRemitente.Checked = True Then
                facturar = "Remitente"
            ElseIf RBDestinatario.Checked = True Then
                facturar = "Destinatario"
            Else
                facturar = "NAPL"
            End If
            Dim devf As Boolean
            If CBDEVFF.Checked = True Then
                devf = 1
            Else
                devf = 0
            End If
            Dim despachada As Boolean
            Dim ped_num As Object
            If CBRecolecta.CheckState = CheckState.Checked Then
                ped_num = CBoRecolecta.SelectedValue
                despachada = 1
            Else
                ped_num = 0
                despachada = 0
            End If
            Dim GuiaCarga As Object
            If TBGuiaCarga.Text <> Nothing Then
                GuiaCarga = TBGuiaCarga.Text
            Else
                GuiaCarga = ""
            End If
            Dim seguro As Object
            If LBTotalSeguro.Text <> "0 Bs." Then
                seguro = Convert.ToDecimal(LBTotalSeguro.Text.Replace("Bs", ""))
            Else
                seguro = 0
            End If
            Dim comision As Object
            If LBTotalComi.Text <> "0 Bs." Then
                comision = Convert.ToDecimal(LBTotalComi.Text.Replace("Bs", ""))
            Else
                comision = 0
            End If
            Dim porlamar As Object
            If LBZ10.Text <> "0 Bs." Then
                porlamar = Convert.ToDecimal(LBZ10.Text.Replace("Bs", ""))
            Else
                porlamar = 0
            End If
            Dim adicionales As Object
            If LBTotalAdic.Text <> "0 Bs." Then
                adicionales = Convert.ToDecimal(LBTotalAdic.Text.Replace("Bs", ""))
            Else
                adicionales = 0
            End If
            Dim tot_piezas As Integer = CInt(LBNBultos.Text)
            Dim peso As Object
            If LBTPeso.Text <> Nothing Then
                peso = Sumar_DataGridView("Peso", DataGridView)
            Else
                peso = 0
            End If
            Dim SubTotal As Object
            If LBSubTotal.Text <> Nothing Then
                SubTotal = Sumar_DataGridView("SubTotal", DataGridView)
            Else
                SubTotal = 0
            End If
            Dim fpo As Object
            If LBFPO.Text <> Nothing Then
                fpo = Sumar_DataGridView("FPO", DataGridView)
            Else
                fpo = 0
            End If
            Dim iva As Object
            If LBIVA.Text <> Nothing Then
                iva = Sumar_DataGridView("IVA", DataGridView)
            Else
                iva = 0
            End If
            Dim mtotal As Object
            If LBTotal.Text <> "0 Bs." Then
                mtotal = Convert.ToDecimal(LBTotal.Text.Replace("Bs", ""))
            Else
                mtotal = 0
            End If
            Dim telef As String
            If obj._telefonosD <> Nothing Then
                telef = obj._telefonosD
            Else
                telef = ""
            End If
            Dim direc As String
            If obj._direc1D <> Nothing Then
                direc = obj._direc1D
            Else
                direc = ""
            End If
            Dim SCBRecolector As String = CBRecolector.Text
            Dim SCBAyudante1 As String = CBAyudante1.Text
            Dim SCBAyudante2 As String = CBAyudante2.Text
            open_conection2()
            Dim cmd As New SqlClient.SqlCommand
            cmd = New SqlClient.SqlCommand("SPINSGUIA", cnn2)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@NumGuia", SqlDbType.Int).Value = obj._guia_num
            cmd.Parameters.Add("@fact_num", SqlDbType.Int).Value = obj.fact_num
            cmd.Parameters.Add("@Recolecta", SqlDbType.Int).Value = ped_num
            cmd.Parameters.Add("@RecoleGuiaCarga", SqlDbType.NVarChar).Value = GuiaCarga
            cmd.Parameters.Add("@ctrl_num", SqlDbType.NVarChar).Value = TBNControl.Text
            cmd.Parameters.Add("@Remitente", SqlDbType.NVarChar).Value = CBRemitente.Text
            cmd.Parameters.Add("@Destinatario", SqlDbType.NVarChar).Value = CBDestinatario.Text
            cmd.Parameters.Add("@DirecDest", SqlDbType.Text).Value = direc
            cmd.Parameters.Add("@TelefDest", SqlDbType.NVarChar).Value = telef
            cmd.Parameters.Add("@CondPago", SqlDbType.NVarChar).Value = CBCPago.Text
            cmd.Parameters.Add("@Facturara", SqlDbType.NVarChar).Value = facturar
            cmd.Parameters.Add("@Contenido", SqlDbType.NVarChar).Value = CBContenido.Text
            cmd.Parameters.Add("@Chofer", SqlDbType.NVarChar).Value = SCBRecolector
            cmd.Parameters.Add("@Ayudante1", SqlDbType.NVarChar).Value = SCBAyudante1
            cmd.Parameters.Add("@Ayudante2", SqlDbType.NVarChar).Value = SCBAyudante2
            cmd.Parameters.Add("@NombreCon", SqlDbType.NVarChar).Value = TBNombreCliente.Text
            cmd.Parameters.Add("@CICon", SqlDbType.NVarChar).Value = TBCI.Text
            cmd.Parameters.Add("@NotasCon", SqlDbType.Text).Value = CStr(TBTexto.Text)
            cmd.Parameters.Add("@co_zona", SqlDbType.NChar).Value = CBTransporte.SelectedValue
            cmd.Parameters.Add("@zonas_des", SqlDbType.NVarChar).Value = CBTransporte.Text
            cmd.Parameters.Add("@CantPaque", SqlDbType.Int).Value = tot_piezas
            cmd.Parameters.Add("@Peso", SqlDbType.Decimal).Value = peso
            cmd.Parameters.Add("@Comision", SqlDbType.Decimal).Value = comision
            cmd.Parameters.Add("@ComisionChof", SqlDbType.Decimal).Value = obj._MontoChofer
            cmd.Parameters.Add("@ComisionOtrChof", SqlDbType.Decimal).Value = obj._MontoOtrChofer
            cmd.Parameters.Add("@ComisionChofFlete", SqlDbType.Decimal).Value = obj._MontoOtrChoferFlete
            cmd.Parameters.Add("@ComisionDevFact", SqlDbType.Decimal).Value = obj._MontoDevFact
            cmd.Parameters.Add("@Seguro", SqlDbType.Decimal).Value = seguro
            cmd.Parameters.Add("@Porlamar", SqlDbType.Decimal).Value = porlamar
            cmd.Parameters.Add("@SubTotal", SqlDbType.Decimal).Value = SubTotal
            cmd.Parameters.Add("@IVA", SqlDbType.Decimal).Value = iva
            cmd.Parameters.Add("@Franqueo", SqlDbType.Decimal).Value = fpo
            cmd.Parameters.Add("@Adicionales", SqlDbType.Decimal).Value = adicionales
            cmd.Parameters.Add("@MontoTotal", SqlDbType.Decimal).Value = mtotal
            cmd.Parameters.Add("@FECHA", SqlDbType.DateTime).Value = DTP.Value.ToString("yyyy-MM-dd")
            cmd.Parameters.Add("@DevFact", SqlDbType.Bit).Value = devf
            cmd.Parameters.Add("@Despachada", SqlDbType.Bit).Value = despachada
            cmd.Parameters.Add("@Observacion", SqlDbType.Text).Value = CStr(TBObservac.Text)
            cmd.ExecuteNonQuery()
            cnn2.Close()
        End If
    End Sub
    Private Sub Insert_Guia_Reng()
        If obj.fact_num <> Nothing Then
            'Insertar datos del detalle temporal en el original
            Dim cmd2 As New SqlClient.SqlCommand
            Dim da As New SqlClient.SqlDataAdapter
            Dim dt As New DataTable
            Dim sql As String = "select * from GUIATEMP where [NumGuia]='" & obj._OBNumGuiaAle & "'"
            cmd2 = New SqlClient.SqlCommand(sql, cnn2)
            da = New SqlClient.SqlDataAdapter(cmd2)
            da.Fill(dt)
            If (dt.Rows.Count > 0) Then
                Dim i As Integer
                Dim y As Integer = dt.Rows.Count - 1
                Dim row As DataRow
                Dim x As Integer
                Dim co_art As String
                Dim art_des As String
                Dim cantidad As Integer
                Dim peso As Double
                Dim iva As Double
                Dim fpo As Double
                Dim subtotal As Double
                For i = 0 To y 'ItemID codigo del primer insert
                    row = dt.Rows(i)
                    x = i + 1
                    co_art = row("co_art")
                    art_des = row("art_des")
                    cantidad = row("Cantidad")
                    peso = row("peso")
                    iva = row("iva")
                    fpo = row("fpo")
                    subtotal = row("subtotal")
                    open_conection2()
                    Dim cmd As New SqlClient.SqlCommand
                    cmd = New SqlClient.SqlCommand("SPINSGUIARENG", cnn2)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("@NumGuia", SqlDbType.Int).Value = obj._guia_num
                    cmd.Parameters.Add("@NumItem", SqlDbType.SmallInt).Value = x
                    cmd.Parameters.Add("@co_art", SqlDbType.NChar).Value = co_art
                    cmd.Parameters.Add("@art_des", SqlDbType.VarChar).Value = art_des
                    cmd.Parameters.Add("@Cantidad", SqlDbType.VarChar).Value = cantidad
                    cmd.Parameters.Add("@peso", SqlDbType.Decimal).Value = peso
                    cmd.Parameters.Add("@iva", SqlDbType.Money).Value = iva
                    cmd.Parameters.Add("@fpo", SqlDbType.Money).Value = fpo
                    cmd.Parameters.Add("@subtotal", SqlDbType.Money).Value = subtotal
                    cmd.ExecuteNonQuery()
                    cnn2.Close()
                Next
            End If
        End If
    End Sub
#End Region
#Region "Update"
    Private Sub Update_Almacen()
        Try
            open_conection1()
            Dim cmd As New SqlClient.SqlCommand
            Dim sql As String = "UPDATE almacen SET [fact_num]='" & obj.fact_num & "' WHERE [co_alma]='CCS'"
            cmd = New SqlClient.SqlCommand(sql, cnn1)
            cmd.ExecuteNonQuery()
            cnn1.Close()
        Catch ex As Exception
            MsgBoxError(mensaje:="No se pudieron actualizar los datos del almacén!", titulo:="ERROR: Actualizar datos a la Base de Datos PROFIT - Sistema")
        End Try
    End Sub
    Private Sub Update_pedidos_Profit()
        Try
            open_conection1()
            Dim cmd As New SqlClient.SqlCommand
            Dim sql As String = "UPDATE pedidos SET [status]='2' WHERE [fact_num]='" & CBoRecolecta.SelectedValue & "'"
            cmd = New SqlClient.SqlCommand(sql, cnn1)
            cmd.ExecuteNonQuery()
            cnn1.Close()
        Catch ex As Exception
            MsgBoxError(mensaje:="No se pudieron actualizar los datos del pedido!", titulo:="ERROR: Actualizar datos a la Base de Datos PROFIT - Sistema")
        End Try
    End Sub
#End Region
#Region "Delete"
    Private Sub Delete_ItemGuia()
        Try
            open_conection2()
            Dim cmd As New SqlClient.SqlCommand
            Dim sql As String = "DELETE FROM GUIATEMP where [NumGuia]='" & obj._OBNumGuiaAle & "' AND [NumItem]='" & obj._OBItemGuia & "'"
            cmd = New SqlClient.SqlCommand(sql, cnn2)
            cmd.ExecuteNonQuery()
            cnn2.Close()
        Catch ex As Exception
            MsgBoxError(mensaje:="No se pudo eliminar el Item de la Guia!", titulo:="ERROR: Eliminar datos a la Base de Datos - Sistema")
        End Try
    End Sub
    Private Sub Delete_Guiatemp()
        Try
            open_conection2()
            Dim cmd As New SqlClient.SqlCommand
            Dim sql As String = "DELETE FROM GUIATEMP where [NumGuia]='" & obj._OBNumGuiaAle & "'"
            cmd = New SqlClient.SqlCommand(sql, cnn2)
            cmd.ExecuteNonQuery()
            cnn2.Close()
        Catch ex As Exception
            MsgBoxError(mensaje:="No se pudo limpiar la Guia temporal!", titulo:="ERROR: Eliminar datos a la Base de Datos - Sistema")
        End Try
    End Sub
#End Region
#End Region
#Region "Validación de TextBox"
    'Private Function Validar_CantPaquetes() As Boolean
    '    obj.CantItemsDGV = Nothing
    '    obj.CantPaquetes = Nothing
    '    DataGridView.Update()
    '    obj.CantItemsDGV = DataGridView.RowCount - 1
    '    obj.CantPaquetes = CInt(LBNBultos.Text)
    '    If (obj.CantItemsDGV = obj.CantPaquetes) Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function
    Private Function Validar_TCB() As Boolean
        If Validar_CBComision() = True And Validar_CBSeguro() = True And Validar_TBNombreCliente() = True And Validar_TBCI() = True Then 'And Validar_CBRecolecta() = True
            Return True
        Else
            Return False
        End If
    End Function
    Private Function Validar_CBComision() As Boolean
        If CBComision.CheckState = CheckState.Checked Then
            If (String.IsNullOrEmpty(TBComision.Text)) Then
                Me.ErrorProvider.BlinkRate = 200
                Me.ErrorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink
                ErrorProvider.SetError(TBComision, "El monto de la comisión es obligatorio!")
                TBComision.Focus()
                Return False
            Else
                ErrorProvider.SetError(TBComision, "")
                Return True
            End If
        Else
            Return True
        End If
    End Function
    Private Function Validar_CBSeguro() As Boolean
        If CBSeguro.CheckState = CheckState.Checked Then
            If (String.IsNullOrEmpty(TBMontoSegu.Text)) Then
                Me.ErrorProvider.BlinkRate = 200
                Me.ErrorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink
                ErrorProvider.SetError(TBMontoSegu, "El monto del seguro es obligatorio!")
                TBMontoSegu.Focus()
                Return False
            Else
                ErrorProvider.SetError(TBMontoSegu, "")
                Return True
            End If
        Else
            Return True
        End If
    End Function
    Private Function Validar_CBRecolecta() As Boolean
        If CBRecolecta.CheckState = CheckState.Checked Then
            If (String.IsNullOrEmpty(TBGuiaCarga.Text)) Then
                Me.ErrorProvider.BlinkRate = 200
                Me.ErrorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink
                ErrorProvider.SetError(TBGuiaCarga, "El número de Recolecta obligatorio!")
                TBGuiaCarga.Focus()
                Return False
            Else
                ErrorProvider.SetError(TBGuiaCarga, "")
                Return True
            End If
        Else
            Return True
        End If
    End Function
    Private Function Validar_TBNombreCliente() As Boolean
        If CBActConsig.CheckState = CheckState.Checked Then
            If (String.IsNullOrEmpty(TBNombreCliente.Text)) Then
                Me.ErrorProvider.BlinkRate = 200
                Me.ErrorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink
                ErrorProvider.SetError(TBNombreCliente, "El Nombre es obligatorio!")
                TBNombreCliente.Focus()
                Return False
            Else
                ErrorProvider.SetError(TBNombreCliente, "")
                Return True
            End If
        Else
            Return True
        End If
    End Function
    Private Function Validar_TBCI() As Boolean
        If CBActConsig.CheckState = CheckState.Checked Then
            If (String.IsNullOrEmpty(TBCI.Text)) Then
                Me.ErrorProvider.BlinkRate = 200
                Me.ErrorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink
                ErrorProvider.SetError(TBCI, "La Cédula es obligatorio!")
                TBCI.Focus()
                Return False
            Else
                ErrorProvider.SetError(TBCI, "")
                Return True
            End If
        Else
            Return True
        End If
    End Function
    Private Function Validar_ClienteNuevo() As Boolean
        If Validar_TBCNombre() = True And Validar_TBCCI() = True And Validar_TBCTelefono() = True And Validar_TBCDirec1() = True Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function Validar_TBCNombre() As Boolean
        If (String.IsNullOrEmpty(TBCNombre.Text)) Then
            Me.ErrorProvider.BlinkRate = 200
            Me.ErrorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink
            ErrorProvider.SetError(TBCNombre, "El Nombre es obligatorio!")
            TBCNombre.Focus()
            Return False
        Else
            ErrorProvider.SetError(TBCNombre, "")
            Return True
        End If
    End Function
    Private Function Validar_TBCCI() As Boolean
        If (String.IsNullOrEmpty(TBCCI.Text)) Then
            Me.ErrorProvider.BlinkRate = 200
            Me.ErrorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink
            ErrorProvider.SetError(TBCCI, "La CI / RIF es obligatorio!")
            TBCCI.Focus()
            Return False
        Else
            ErrorProvider.SetError(TBCCI, "")
            Return True
        End If
    End Function
    Private Function Validar_TBCTelefono() As Boolean
        If (String.IsNullOrEmpty(TBCTelefono.Text)) Then
            Me.ErrorProvider.BlinkRate = 200
            Me.ErrorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink
            ErrorProvider.SetError(TBCTelefono, "El teléfono es obligatorio!")
            TBCTelefono.Focus()
            Return False
        Else
            ErrorProvider.SetError(TBCTelefono, "")
            Return True
        End If
    End Function
    Private Function Validar_TBCDirec1() As Boolean
        If (String.IsNullOrEmpty(TBCDirec1.Text)) Then
            Me.ErrorProvider.BlinkRate = 200
            Me.ErrorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink
            ErrorProvider.SetError(TBCDirec1, "La Dirección es obligatorio!")
            TBCDirec1.Focus()
            Return False
        Else
            ErrorProvider.SetError(TBCDirec1, "")
            Return True
        End If
    End Function
#End Region
#Region "KeyPress TextBox"
    Private Sub CITextBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TBCI.KeyPress
        TBCI.MaxLength = 9
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(NUMEROS(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TBGuiaCarga_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TBGuiaCarga.KeyPress
        TBGuiaCarga.MaxLength = 6
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(NUMEROS(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TBCCI_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TBCCI.KeyPress
        TBCCI.MaxLength = 12
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(VALIDA_RIF(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TBCTelefono_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TBCTelefono.KeyPress
        TBGuiaCarga.MaxLength = 30
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(VALIDA_telefono(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TBNControl_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TBNControl.KeyPress
        TBNControl.MaxLength = 10
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(NUMEROS(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TBMontoSegu_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TBMontoSegu.KeyPress
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(NUMEROSDEC(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TBComision_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TBComision.KeyPress
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(NUMEROSDEC(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TBCheque_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TBCheque.KeyPress
        TBCheque.MaxLength = 12
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(NUMEROS(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
#End Region
#Region "Funciones y Procedimientos"
#Region "Limpiar Formulario"
    Private Sub Limpiar_Formulario()
        reng_num = 1
        obj._OBIDZona = Nothing
        TBCI.Text = Nothing
        TBGuiaCarga.Text = Nothing
        TBTexto.Text = Nothing
        TBNombreCliente.Text = Nothing
        CBRecolecta.CheckState = CheckState.Unchecked
        RBRemitente.Checked = False
        RBDestinatario.Checked = False
        'CKBAyudante1.CheckState = CheckState.Unchecked
        'CKBAyudante2.CheckState = CheckState.Unchecked
        CBComision.CheckState = CheckState.Unchecked
        CBSeguro.CheckState = CheckState.Unchecked
        TBComision.Text = Nothing
        TBMontoSegu.Text = Nothing
        obj._OBComiSeguro = Nothing
        obj._OBComiChoferFletes = Nothing
        obj._OBComiOtrChofer = Nothing
        obj._OBComiZ10 = Nothing
        obj._MontoZ10 = Nothing

        DataGridView.DataSource = Nothing
        LBFPO.Text = "0 Bs."
        LBIVA.Text = "0 Bs."
        LBSubTotal.Text = "0 Bs."
        LBTotal.Text = "0 Bs."
        LBTotalComi.Text = "0 Bs."
        LBTotalAdic.Text = "0 Bs."
        LBTotalSeguro.Text = "0 Bs."
        LBTPeso.Text = "0 Kgs."
        LBZ10.Text = "0 Bs."
        'LBTotalComiChof.Text = "0 Bs."
        'LBTotalComiOtrChof.Text = "0 Bs."
        LBNBultos.Text = Nothing
        obj.CantPaquetes = Nothing
        CBActCliente.CheckState = CheckState.Unchecked
        CBDEVFF.CheckState = CheckState.Unchecked
        TLDEVFACTF.Text = "0 Bs."
        obj.MontoDevFact = Nothing
        TBObservac.Text = Nothing
        obj._zonab = Nothing
        TBNControl.Text = Nothing
        GBFPago.Enabled = False
        CBCheque.CheckState = CheckState.Unchecked
        CBEfectivo.CheckState = CheckState.Unchecked
        CBDeposito.CheckState = CheckState.Unchecked
        CBTarjeta.CheckState = CheckState.Unchecked
        obj.cobro_num = Nothing
        obj.movcaj_num = Nothing
        TBCheque.Text = Nothing
        obj._impfis = Nothing
        obj._impfisfac = Nothing
    End Sub
#End Region
#Region "Eventos Changed"
#Region "Recolecta"
    Private Sub CBRecolecta_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBRecolecta.CheckedChanged
        If CBRecolecta.CheckState = CheckState.Checked Then
            TBGuiaCarga.Enabled = True
            CBoRecolecta.Enabled = True
            L_CBRecolecta()
            CBRecolector.Enabled = False
            CBAyudante1.Enabled = False
            CBAyudante2.Enabled = False
        ElseIf CBRecolecta.CheckState = CheckState.Unchecked Then
            TBGuiaCarga.Enabled = False
            TBGuiaCarga.Text = Nothing
            CBoRecolecta.DataSource = Nothing
            CBoRecolecta.Enabled = False
            LRecolecta.Text = Nothing
            LRecolecta2.Text = Nothing
        End If
    End Sub
    Private Sub CBoRecolecta_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles CBoRecolecta.SelectionChangeCommitted
        Buscar_DatosRecolecta()
    End Sub
#End Region
#Region "Adicionales de la Guía"
    Private Sub CBComision_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBComision.CheckedChanged
        If CBComision.CheckState = CheckState.Checked Then
            TBComision.Enabled = True
        ElseIf CBComision.CheckState = CheckState.Unchecked Then
            TBComision.Enabled = False
            TBComision.Text = Nothing
        End If
    End Sub
    Private Sub CBSeguro_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBSeguro.CheckedChanged
        If CBSeguro.CheckState = CheckState.Checked Then
            TBMontoSegu.Enabled = True
        ElseIf CBSeguro.CheckState = CheckState.Unchecked Then
            TBMontoSegu.Enabled = False
            TBMontoSegu.Text = Nothing
        End If
    End Sub
#End Region
#Region "Facturar a Remitente o Destinatario"
    Private Sub RBDestinatario_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBDestinatario.CheckedChanged
        If RBDestinatario.Checked = True Then
            RBRemitente.Checked = False
        End If
    End Sub
    Private Sub RBRemitente_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RBRemitente.CheckedChanged
        If RBRemitente.Checked = True Then
            RBDestinatario.Checked = False
        End If
    End Sub
#End Region
#Region "Buscar la dirección del Remitente o Destinatario"
    Private Sub CBRemitente_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles CBRemitente.SelectionChangeCommitted
        Buscar_DirecRemitente()
    End Sub
    Private Sub CBDestinatario_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles CBDestinatario.SelectionChangeCommitted
        Buscar_DirecDestinatario()
    End Sub
#End Region
#Region "Activar Agregar datos de Nuevo Cliente Profit"
    Private Sub CBActCliente_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBActCliente.CheckedChanged
        If CBActCliente.CheckState = CheckState.Checked Then
            GBCliente.Enabled = True
        Else
            GBCliente.Enabled = False
            TBCCI.Text = Nothing
            TBCNombre.Text = Nothing
            TBCDirec1.Text = Nothing
            TBCTelefono.Text = Nothing
        End If
    End Sub
#End Region
#Region "Activar datos del Consignador"
    Private Sub CBActConsig_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBActConsig.CheckedChanged
        If CBActConsig.CheckState = CheckState.Checked Then
            GBConsignador.Enabled = True
        Else
            GBConsignador.Enabled = False
            TBNombreCliente.Text = Nothing
            TBCCI.Text = Nothing
            TBTexto.Text = Nothing
        End If
    End Sub
#End Region
#Region "Cobros Pagado Pagado"
    Private Sub CBCPago_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CBCPago.SelectedValueChanged
        Dim s As String = CBCPago.Text
        If (s.Contains("PAGADO PAGADO")) Then
            GBFPago.Enabled = True
            obj._RealizarCob = True
        ElseIf (s.Contains("CR")) Or (s.Contains("DE")) Or (s.Contains("PP")) Then
            obj.RealizarCob = False
            GBFPago.Enabled = False
            CBCheque.CheckState = CheckState.Unchecked
            CBEfectivo.CheckState = CheckState.Unchecked
            CBDeposito.CheckState = CheckState.Unchecked
            CBTarjeta.CheckState = CheckState.Unchecked
            TBCheque.Enabled = False
            TBCheque.Text = Nothing
        End If
    End Sub
    Private Sub CBEfectivo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBEfectivo.CheckedChanged
        If CBEfectivo.CheckState = CheckState.Checked Then
            CBCheque.CheckState = CheckState.Unchecked
            CBTarjeta.CheckState = CheckState.Unchecked
            CBDeposito.CheckState = CheckState.Unchecked
            TBCheque.Enabled = False
            TBCheque.Text = Nothing
        End If
    End Sub
    Private Sub CBCheque_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBCheque.CheckedChanged
        If CBCheque.CheckState = CheckState.Checked Then
            CBEfectivo.CheckState = CheckState.Unchecked
            CBTarjeta.CheckState = CheckState.Unchecked
            CBDeposito.CheckState = CheckState.Unchecked
            TBCheque.Enabled = True
        End If
    End Sub
    Private Sub CBTarjeta_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBTarjeta.CheckedChanged
        If CBTarjeta.CheckState = CheckState.Checked Then
            CBEfectivo.CheckState = CheckState.Unchecked
            CBCheque.CheckState = CheckState.Unchecked
            CBDeposito.CheckState = CheckState.Unchecked
            TBCheque.Enabled = False
            TBCheque.Text = Nothing
        End If
    End Sub
    Private Sub CBDeposito_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBDeposito.CheckedChanged
        If CBDeposito.CheckState = CheckState.Checked Then
            CBEfectivo.CheckState = CheckState.Unchecked
            CBTarjeta.CheckState = CheckState.Unchecked
            CBCheque.CheckState = CheckState.Unchecked
            TBCheque.Enabled = False
            TBCheque.Text = Nothing
        End If
    End Sub
#End Region
#Region "Detalle de la Guía"
    Private Sub DataGridView_DataSourceChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView.DataSourceChanged
        LBTPeso.Text = Format(Sumar_DataGridView("Peso", DataGridView), "#,#.00 Kgs").ToString()
        LBIVA.Text = Format(Sumar_DataGridView("IVA", DataGridView), "#,#.00 Bs").ToString()
        LBFPO.Text = Format(Sumar_DataGridView("FPO", DataGridView), "#,#.00 Bs").ToString()
        LBNBultos.Text = Sumar_DataGridView("Cantidad", DataGridView)
        LBSubTotal.Text = Format(Sumar_DataGridView("SubTotal", DataGridView), "#,#.00 Bs").ToString()
        If CBRecolecta.CheckState = CheckState.Checked Then
            Calcular_SubtotalGuia()
            Calcular_ComiChoferRecolecta()
            obj._MontoChofer = (obj._OBSubTotal * obj._OBComiChofer) / 100
            'LBTotalComiChof.Text = Format(comisionchofayud, "#,#.00 Bs").ToString()
        Else
            obj._MontoChofer = 0
        End If
        Calcular_SubtotalGuia()
        Calcular_ComiOtrChofer()
        Calcular_ComiChoferFletes()
        obj._MontoOtrChofer = (obj._OBSubTotal * obj._OBComiOtrChofer) / 100
        obj._MontoOtrChoferFlete = (obj._OBSubTotal * obj._OBComiChoferFletes) / 100
        'LBTotalComiOtrChof.Text = Format(comisionotrchof, "#,#.00 Bs").ToString()
        Dim comision As Double
        If CBComision.CheckState = CheckState.Checked Then
            If TBComision.Text <> 0 Then
                comision = CDbl(TBComision.Text)
                LBTotalComi.Text = Format(comision, "#,#.00 Bs").ToString()
            End If
        End If
        Dim seguro As Double
        If CBSeguro.CheckState = CheckState.Checked Then
            If TBMontoSegu.Text <> 0 Then
                Calcular_ComiSeguro()
                Dim seguropor As Double = CDbl(TBMontoSegu.Text)
                seguro = (seguropor * obj._OBComiSeguro) / 100
                LBTotalSeguro.Text = Format(seguro, "#,#.00 Bs").ToString()
            End If
        End If
        Dim s As String = CBTransporte.Text
        If (s.Contains("ZONA 10")) Then
            Calcular_ComiZona10()
            Calcular_SubtotalGuia()
            obj._MontoZ10 = (obj._OBSubTotal * obj._OBComiZ10) / 100
            LBZ10.Text = Format(obj._MontoZ10, "#,#.00 Bs").ToString()
        Else
            LBZ10.Text = "0 Bs."
        End If
        If CBDEVFF.CheckState = CheckState.Checked Then
            Buscar_MontoDevFactura()
            TLDEVFACTF.Text = Format(obj._MontoDevFact, "#,#.00 Bs").ToString()
        Else
            TLDEVFACTF.Text = "0 Bs."
            obj.MontoDevFact = 0
        End If
        Dim suma1, suma2, suma3 As Double
        suma2 = obj._MontoZ10 + seguro + comision + obj.MontoDevFact
        If obj.Miva = True Then
            suma1 = Sumar_DataGridView("FPO", DataGridView) + Sumar_DataGridView("IVA", DataGridView) + Sumar_DataGridView("SubTotal", DataGridView)
        ElseIf obj.Miva = False Then
            suma1 = Sumar_DataGridView("FPO", DataGridView) + Sumar_DataGridView("SubTotal", DataGridView)
        End If
        suma3 = suma1 + suma2
        LBTotalAdic.Text = Format(suma2, "#,#.00 Bs").ToString()
        LBTotal.Text = Format(suma3, "#,#.00 Bs").ToString()
    End Sub
#End Region
#End Region
#Region "Impresora Fiscal"
    Private Sub Imprimir_Fiscal()
        Dim nombre, NombCliL1, NombCliL2, rif, numero As String
        Dim direcc, direcc1, direcc2, telef, telef1 As String
        If RBRemitente.Checked Then
            If obj._cli_desR <> Nothing Then
                Dim nomb As String = obj._cli_desR.Substring(0, 71)
                nombre = nomb
            Else
                nombre = ""
            End If
            If obj._rifR <> Nothing Then
                rif = obj._rifR
            Else
                rif = ""
            End If
            If obj._direc1R <> Nothing Then
                Dim dir1 As String = obj._direc1R.Substring(0, 69)
                direcc = dir1
            Else
                direcc = ""
            End If
            If obj._telefonosR <> Nothing Then
                telef = obj._telefonosR
            Else
                telef = ""
            End If
        ElseIf RBDestinatario.Checked Then
            If obj._cli_desD <> Nothing Then
                Dim nomb As String = obj._cli_desD.Substring(0, 71)
                nombre = nomb
            Else
                nombre = ""
            End If
            If obj._rifD <> Nothing Then
                rif = obj._rifD
            Else
                rif = ""
            End If
            If obj._direc1D <> Nothing Then
                Dim dir1 As String = obj._direc1D.Substring(0, 69)
                direcc = dir1
            Else
                direcc = ""
            End If
            If obj._telefonosD <> Nothing Then
                telef = obj._telefonosD
            Else
                telef = ""
            End If
        Else
            nombre = ""
            rif = ""
            direcc = ""
            telef = ""
        End If
        If nombre <> "" Then
            Dim LengNomCli As Integer = nombre.Length
            If LengNomCli <= 71 And LengNomCli > 31 Then
                NombCliL1 = nombre.Substring(0, 31)
                NombCliL2 = nombre.Substring(31)
            Else
                NombCliL1 = nombre
                NombCliL2 = Nothing
            End If
        Else
            NombCliL1 = ""
            NombCliL2 = Nothing
        End If
        If direcc <> "" Then
            Dim LengDirCli As Integer = direcc.Length
            If LengDirCli <= 69 And LengDirCli > 29 Then
                direcc1 = direcc.Substring(0, 29)
                direcc2 = direcc.Substring(69)
            Else
                direcc1 = direcc.Substring(29)
                direcc2 = Nothing
            End If
        Else
            direcc1 = ""
            direcc2 = Nothing
        End If
        If telef <> "" Then
            Dim LengtelefCli As Integer = telef.Length
            If LengtelefCli > 30 Then
                telef1 = telef.Substring(0, 30)
            Else
                telef1 = telef
            End If
        Else
            telef1 = ""
        End If
        numero = CStr(obj.fact_num)
        Try
            OpenFpctrl("COM1")
            Dim lStatus As Integer = 6
            Dim lError As Integer = 5
            Dim lCadena As String
            If NombCliL2 <> Nothing Then
                lCadena = "i01" + "Cliente: " + NombCliL1
                SendCmd(lStatus, lError, lCadena)
                lCadena = "i02" + NombCliL2
                SendCmd(lStatus, lError, lCadena)
                lCadena = "i03" + "C.I./R.I.F.: " + rif
                SendCmd(lStatus, lError, lCadena)
                If direcc2 <> Nothing Then
                    lCadena = "i04" + "Dirc: " + direcc1
                    SendCmd(lStatus, lError, lCadena)
                    lCadena = "i05" + direcc2
                    SendCmd(lStatus, lError, lCadena)
                    lCadena = "i06" + "Tlf: " + telef1
                    SendCmd(lStatus, lError, lCadena)
                    lCadena = "i07" + "Numero: " + numero
                    SendCmd(lStatus, lError, lCadena)
                Else
                    lCadena = "i04" + "Dirc: " + direcc1
                    SendCmd(lStatus, lError, lCadena)
                    lCadena = "i05" + "Tlf: " + telef1
                    SendCmd(lStatus, lError, lCadena)
                    lCadena = "i06" + "Numero: " + numero
                    SendCmd(lStatus, lError, lCadena)
                End If
            Else
                lCadena = "i01" + "Cliente: " + NombCliL1
                SendCmd(lStatus, lError, lCadena)
                lCadena = "i02" + "C.I./R.I.F. : " + rif
                SendCmd(lStatus, lError, lCadena)
                If direcc2 <> Nothing Then
                    lCadena = "i03" + "Dirc: " + direcc1
                    SendCmd(lStatus, lError, lCadena)
                    lCadena = "i04" + direcc2
                    SendCmd(lStatus, lError, lCadena)
                    lCadena = "i05" + "Tlf: " + telef1
                    SendCmd(lStatus, lError, lCadena)
                    lCadena = "i06" + "Numero: " + numero
                    SendCmd(lStatus, lError, lCadena)
                Else
                    lCadena = "i03" + "Dirc: " + direcc1
                    SendCmd(lStatus, lError, lCadena)
                    lCadena = "i04" + "Tlf: " + telef1
                    SendCmd(lStatus, lError, lCadena)
                    lCadena = "i05" + "Numero: " + numero
                    SendCmd(lStatus, lError, lCadena)
                End If
            End If
            open_conection2()
            Dim cmd As New SqlClient.SqlCommand
            Dim da As New SqlClient.SqlDataAdapter
            Dim dt As New DataTable
            Dim sql As String = "select * from GUIARENG where NumGuia='" & obj._guia_num & "'"
            cmd = New SqlClient.SqlCommand(sql, cnn2)
            da = New SqlClient.SqlDataAdapter(cmd)
            da.Fill(dt)
            If (dt.Rows.Count > 0) Then
                Dim i As Integer
                Dim y As Integer = dt.Rows.Count - 1
                Dim row As DataRow
                Dim x As Integer
                Dim fpo As String
                Dim subtotal As String
                For i = 0 To y
                    row = dt.Rows(i)
                    x = i + 1
                    fpo = row("fpo")
                    subtotal = row("subtotal")
                    If fpo <> 0 Then
                        Dim fpoxx As String = fpo.ToString.Replace(".", "")
                        Dim fpox As String = fpoxx.ToString.Replace(",", "")
                        fpox = fpox.PadLeft(10, "0"c)
                        lCadena = " " + fpox + "00001000" + "Franqueo Postal"
                        SendCmd(lStatus, lError, lCadena)
                    ElseIf subtotal <> 0 Then
                        Dim subtotalxx As String = subtotal.ToString.Replace(".", "")
                        Dim subtotalx As String = subtotalxx.ToString.Replace(",", "")
                        subtotalx = subtotalx.PadLeft(10, "0"c)
                        lCadena = " " + subtotalx + "00001000" + "FLETE"
                        SendCmd(lStatus, lError, lCadena)
                    End If
                Next
            End If
            cnn2.Close()
            Dim adicionales As Object
            If LBTotalAdic.Text <> "0 Bs." Then
                adicionales = Convert.ToDecimal(LBTotalAdic.Text.Replace("Bs", ""))
            Else
                adicionales = 0
            End If
            Dim adicionalxx As String = adicionales.ToString.Replace(".", "")
            Dim adicionalx As String = adicionalxx.ToString.Replace(",", "")
            adicionalx = adicionalx.PadLeft(10, "0"c)
            If adicionalx <> 0 Then
                lCadena = " " + adicionalx + "00001000" + "Adicionales"
                SendCmd(lStatus, lError, lCadena)
            End If
            lCadena = "101"
            SendCmd(lStatus, lError, lCadena)
            CloseFpctrl()
        Catch ex As Exception
            MsgBoxInfo(mensaje:="No se pudo imprimir la Factura Fiscal!", titulo:="AVISO: Validación - Sistema")
        End Try
    End Sub
    Private Sub Imprimir_Fiscal_iva()
        Dim nombre, NombCliL1, NombCliL2, rif, numero As String
        Dim direcc, direcc1, direcc2, telef, telef1 As String
        If RBRemitente.Checked Then
            If obj._cli_desR <> Nothing Then
                Dim nomb As String = obj._cli_desR.Substring(0, 71)
                nombre = nomb
            Else
                nombre = ""
            End If
            If obj._rifR <> Nothing Then
                rif = obj._rifR
            Else
                rif = ""
            End If
            If obj._direc1R <> Nothing Then
                Dim dir1 As String = obj._direc1R.Substring(0, 69)
                direcc = dir1
            Else
                direcc = ""
            End If
            If obj._telefonosR <> Nothing Then
                telef = obj._telefonosR
            Else
                telef = ""
            End If
        ElseIf RBDestinatario.Checked Then
            If obj._cli_desD <> Nothing Then
                Dim nomb As String = obj._cli_desD.Substring(0, 71)
                nombre = nomb
            Else
                nombre = ""
            End If
            If obj._rifD <> Nothing Then
                rif = obj._rifD
            Else
                rif = ""
            End If
            If obj._direc1D <> Nothing Then
                Dim dir1 As String = obj._direc1D.Substring(0, 69)
                direcc = dir1
            Else
                direcc = ""
            End If
            If obj._telefonosD <> Nothing Then
                telef = obj._telefonosD
            Else
                telef = ""
            End If
        Else
            nombre = ""
            rif = ""
            direcc = ""
            telef = ""
        End If
        If nombre <> "" Then
            Dim LengNomCli As Integer = nombre.Length
            If LengNomCli <= 71 And LengNomCli > 31 Then
                NombCliL1 = nombre.Substring(0, 31)
                NombCliL2 = nombre.Substring(31)
            Else
                NombCliL1 = nombre
                NombCliL2 = Nothing
            End If
        Else
            NombCliL1 = ""
            NombCliL2 = Nothing
        End If
        If direcc <> "" Then
            Dim LengDirCli As Integer = direcc.Length
            If LengDirCli <= 69 And LengDirCli > 29 Then
                direcc1 = direcc.Substring(0, 29)
                direcc2 = direcc.Substring(69)
            Else
                direcc1 = direcc.Substring(29)
                direcc2 = Nothing
            End If
        Else
            direcc1 = ""
            direcc2 = Nothing
        End If
        If telef <> "" Then
            Dim LengtelefCli As Integer = telef.Length
            If LengtelefCli > 30 Then
                telef1 = telef.Substring(0, 30)
            Else
                telef1 = telef
            End If
        Else
            telef1 = ""
        End If
        numero = CStr(obj.fact_num)
        Try
            OpenFpctrl("COM1")
            Dim lStatus As Integer = 6
            Dim lError As Integer = 5
            Dim lCadena As String
            If NombCliL2 <> Nothing Then
                lCadena = "i01" + "Cliente: " + NombCliL1
                SendCmd(lStatus, lError, lCadena)
                lCadena = "i02" + NombCliL2
                SendCmd(lStatus, lError, lCadena)
                lCadena = "i03" + "C.I./R.I.F.: " + rif
                SendCmd(lStatus, lError, lCadena)
                If direcc2 <> Nothing Then
                    lCadena = "i04" + "Dirc: " + direcc1
                    SendCmd(lStatus, lError, lCadena)
                    lCadena = "i05" + direcc2
                    SendCmd(lStatus, lError, lCadena)
                    lCadena = "i06" + "Tlf: " + telef1
                    SendCmd(lStatus, lError, lCadena)
                    lCadena = "i07" + "Numero: " + numero
                    SendCmd(lStatus, lError, lCadena)
                Else
                    lCadena = "i04" + "Dirc: " + direcc1
                    SendCmd(lStatus, lError, lCadena)
                    lCadena = "i05" + "Tlf: " + telef1
                    SendCmd(lStatus, lError, lCadena)
                    lCadena = "i06" + "Numero: " + numero
                    SendCmd(lStatus, lError, lCadena)
                End If
            Else
                lCadena = "i01" + "Cliente: " + NombCliL1
                SendCmd(lStatus, lError, lCadena)
                lCadena = "i02" + "C.I./R.I.F. : " + rif
                SendCmd(lStatus, lError, lCadena)
                If direcc2 <> Nothing Then
                    lCadena = "i03" + "Dirc: " + direcc1
                    SendCmd(lStatus, lError, lCadena)
                    lCadena = "i04" + direcc2
                    SendCmd(lStatus, lError, lCadena)
                    lCadena = "i05" + "Tlf: " + telef1
                    SendCmd(lStatus, lError, lCadena)
                    lCadena = "i06" + "Numero: " + numero
                    SendCmd(lStatus, lError, lCadena)
                Else
                    lCadena = "i03" + "Dirc: " + direcc1
                    SendCmd(lStatus, lError, lCadena)
                    lCadena = "i04" + "Tlf: " + telef1
                    SendCmd(lStatus, lError, lCadena)
                    lCadena = "i05" + "Numero: " + numero
                    SendCmd(lStatus, lError, lCadena)
                End If
            End If
            open_conection2()
            Dim cmd As New SqlClient.SqlCommand
            Dim da As New SqlClient.SqlDataAdapter
            Dim dt As New DataTable
            Dim sql As String = "select * from GUIARENG where NumGuia='" & obj._guia_num & "'"
            cmd = New SqlClient.SqlCommand(sql, cnn2)
            da = New SqlClient.SqlDataAdapter(cmd)
            da.Fill(dt)
            If (dt.Rows.Count > 0) Then
                Dim i As Integer
                Dim y As Integer = dt.Rows.Count - 1
                Dim row As DataRow
                Dim x As Integer
                Dim fpo As String
                Dim subtotal As String
                For i = 0 To y
                    row = dt.Rows(i)
                    x = i + 1
                    fpo = row("fpo")
                    subtotal = row("subtotal")
                    If fpo <> 0 Then
                        Dim fpoxx As String = fpo.ToString.Replace(".", "")
                        Dim fpox As String = fpoxx.ToString.Replace(",", "")
                        fpox = fpox.PadLeft(10, "0"c)
                        lCadena = " " + fpox + "00001000" + "Franqueo Postal"
                        SendCmd(lStatus, lError, lCadena)
                    ElseIf subtotal <> 0 Then
                        Dim subtotalxx As String = subtotal.ToString.Replace(".", "")
                        Dim subtotalx As String = subtotalxx.ToString.Replace(",", "")
                        subtotalx = subtotalx.PadLeft(10, "0"c)
                        lCadena = "!" + subtotalx + "00001000" + "FLETE"
                        SendCmd(lStatus, lError, lCadena)
                    End If
                Next
            End If
            cnn2.Close()
            Dim adicionales As Object
            If LBTotalAdic.Text <> "0 Bs." Then
                adicionales = Convert.ToDecimal(LBTotalAdic.Text.Replace("Bs", ""))
            Else
                adicionales = 0
            End If
            Dim adicionalxx As String = adicionales.ToString.Replace(".", "")
            Dim adicionalx As String = adicionalxx.ToString.Replace(",", "")
            adicionalx = adicionalx.PadLeft(10, "0"c)
            If adicionalx <> 0 Then
                lCadena = " " + adicionalx + "00001000" + "Adicionales"
                SendCmd(lStatus, lError, lCadena)
            End If
            lCadena = "101"
            SendCmd(lStatus, lError, lCadena)
            CloseFpctrl()
        Catch ex As Exception
            MsgBoxInfo(mensaje:="No se pudo imprimir la Factura Fiscal!", titulo:="AVISO: Validación - Sistema")
        End Try
    End Sub
    Private Function Buscar_Ultima_Fac() As Boolean
        Try
            OpenFpctrl("COM1")
            Dim lStatus As Integer = 0
            Dim lError As Integer = 0
            Dim lCadena As String = "S1"
            UploadStatusCmd(lStatus, lError, lCadena, "C:\cadena.txt")
            Dim streamReader As StreamReader = New StreamReader("C:\cadena.txt")
            Dim text1 As String = Nothing
            Dim text2 As String = Nothing
            text1 = streamReader.ReadLine()
            text2 = text1.Substring(22, 7)
            obj._impfisfac = text2
            CloseFpctrl()
            Return True
        Catch ex As Exception
            MsgBoxInfo(mensaje:="No se puede obtener la última Factura!", titulo:="AVISO: Validación Impresora Fiscal - Sistema")
            Return False
        End Try
    End Function
    Public Function files_exist() As Boolean
        'Comprobar la existencia del archivo .TXT 
        Try
            If System.IO.File.Exists("C:\cadena.txt") Then
                Return True
            Else
                Dim fs As FileStream = File.Create("C:\cadena.txt")
                Return True
            End If
        Catch ex As Exception
            MsgBoxError(mensaje:="No se puede tener acceso a la ruta de los archivos .TXT", titulo:="ERROR: Impresora Fiscal - Sistema")
            Return False
        End Try
    End Function
#End Region
#End Region
End Class
