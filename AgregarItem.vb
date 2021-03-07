Public Class AgregarItem
#Region "Al iniciar el Form"
    Private Sub AgregarItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Load_ComboBox()
        Buscar_Dolar()
        Buscar_Precio()
        Buscar_Descuento()
        GBPesoVol.Enabled = False
        CBDescuento.CheckState = CheckState.Checked
        MGB9.Enabled = False
        GB1.Enabled = False
        GB2.Enabled = False
        GB3.Enabled = False
        GB4.Enabled = False
        GB5.Enabled = False
        GB6.Enabled = False
        GB7.Enabled = False
        GB8.Enabled = False
        GB9.Enabled = False
        GBS1.Enabled = False
        GBS2.Enabled = False
        GBS3.Enabled = False
        GBS4.Enabled = False
        GBS5.Enabled = False
        GBS6.Enabled = False
        GBS7.Enabled = False
        GBS8.Enabled = False
        GBS9.Enabled = False
    End Sub
#End Region
#Region "Querys"
#Region "Load ComboBox"
    Private Sub Load_ComboBox()
        open_conection2()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim ds As New DataSet
        Dim sql As String = "select co_art, art_des from ARTICULOS order by co_art"
        cmd = New SqlClient.SqlCommand(sql, cnn2)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(ds)
        CBArt.DataSource = ds.Tables(0)
        CBArt.DisplayMember = ds.Tables(0).Columns(1).Caption.ToString
        CBArt.ValueMember = ds.Tables(0).Columns(0).Caption.ToString
        cnn2.Close()
    End Sub
#End Region
#Region "Buscar"
    Private Sub Buscar_Precio()
        open_conection2()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select monto from ARTICULOPREC where [co_art]='" & CBArt.SelectedValue & "' and [co_zona]='" & obj._OBIDZona & "'"
        cmd = New SqlClient.SqlCommand(sql, cnn2)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim row As DataRow
            row = dt.Rows(0)
            Dim valor As Decimal
            valor = row("monto")
            obj._OBPrecioArt = valor * obj._OBDolar
            LBPrecioBase.Text = obj._OBPrecioArt.ToString("#,#.00 Bs")
        End If
        cnn2.Close()
    End Sub
    Private Sub Buscar_Descuento()
        open_conection2()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select porcen from ARTICULODESC where [co_art]='" & CBArt.SelectedValue & "'"
        cmd = New SqlClient.SqlCommand(sql, cnn2)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim row As DataRow
            row = dt.Rows(0)
            Dim valor1, valor2 As Decimal
            valor1 = row("porcen")
            If valor1 <> 0 Then
                valor2 = (valor1 * obj._OBPrecioArt) / 100
                obj._OBPorcenDec = obj._OBPrecioArt - valor2
                LBPrecioBaseDec.Text = obj._OBPorcenDec.ToString("#,#.00 Bs")
            Else
                obj._OBPorcenDec = obj._OBPrecioArt
                LBPrecioBaseDec.Text = obj._OBPorcenDec.ToString("#,#.00 Bs")
            End If
        End If
        cnn2.Close()
    End Sub
    Private Sub Buscar_Dolar()
        open_conection2()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select monto from DOLAR"
        cmd = New SqlClient.SqlCommand(sql, cnn2)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim row As DataRow
            row = dt.Rows(0)
            obj._OBDolar = row("monto")
        End If
        cnn2.Close()
    End Sub
    Private Sub Buscar_FPO()
        open_conection2()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim x As String = obj.OBPesoFPO.ToString.Replace(",", ".")
        Dim sql As String = "select porcen from FPO where '" & x & "' BETWEEN [desde] and [hasta]"
        cmd = New SqlClient.SqlCommand(sql, cnn2)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim row As DataRow
            row = dt.Rows(0)
            obj._OBPorcenFPO = row("porcen")
        End If
        cnn2.Close()
    End Sub
    Private Function Buscar_FPO2(ByVal x As String) As Decimal
        Dim ValorRetorno As Decimal
        open_conection2()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        'Dim x2 As String = x.ToString.Replace(",", ".")
        Dim sql As String = "select porcen from FPO where '" & x & "' BETWEEN [desde] and [hasta]"
        cmd = New SqlClient.SqlCommand(sql, cnn2)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim row As DataRow
            row = dt.Rows(0)
            ValorRetorno = row("porcen")
            Return ValorRetorno
        End If
        cnn2.Close()
    End Function
    Private Sub Buscar_IVA()
        open_conection1()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim x As String = obj.OBPesoFPO.ToString.Replace(",", ".")
        Dim sql As String = "select tasa from tab_enc where [fecha]=(select MAX(fecha) FROM tab_enc)"
        cmd = New SqlClient.SqlCommand(sql, cnn1)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim row As DataRow
            row = dt.Rows(0)
            obj._OBIva = row("tasa")
        End If
        cnn1.Close()
    End Sub
    Private Sub Buscar_FPV()
        open_conection2()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select monto from FACTORPV"
        cmd = New SqlClient.SqlCommand(sql, cnn2)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim row As DataRow
            row = dt.Rows(0)
            obj._OBFPV = row("monto")
        End If
        cnn2.Close()
    End Sub
    Private Function Buscar_PosicionItem() As Integer
        open_conection2()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select MAX(NumItem) as Mayor from GUIATEMP where [NumGuia] = '" & obj._OBNumGuiaAle & "'"
        cmd = New SqlClient.SqlCommand(sql, cnn2)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim row As DataRow
            row = dt.Rows(0)
            If Not IsDBNull(row("Mayor")) Then
                Dim res As Integer = row("Mayor")
                Return res + 1
            Else
                Return 1
            End If
        End If
        cnn2.Close()
    End Function
#End Region
#Region "Insert"
    Private Sub Insertar_Guia()
        Dim item As Integer = Buscar_PosicionItem()
        Dim cantidad As Integer
        Dim fpo As Decimal
        If MGB9.Enabled = True Then
            obj.OBPesoFPO = 0
            cantidad = 0
            fpo = 0
        Else
            cantidad = CInt(TBNBultos.Text)
            fpo = Format(obj._OBFPOSub, "##0.00")
        End If
        Dim iva As Decimal = Format(obj.OBIvaSub, "##0.00")
        Dim subtotal As Decimal
        If CBDescuento.CheckState = CheckState.Checked Then
            subtotal = Format(obj._OBPrecioDesc, "##0.00")
        Else
            subtotal = Format(obj._OBPrecioSub, "##0.00")
        End If
        open_conection2()
        Dim cmd As New SqlClient.SqlCommand
        cmd = New SqlClient.SqlCommand("SPINSGUIATEMP", cnn2)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@NumGuia", SqlDbType.Int).Value = obj._OBNumGuiaAle
        cmd.Parameters.Add("@NumItem", SqlDbType.Int).Value = item
        cmd.Parameters.Add("@co_art", SqlDbType.NChar).Value = CBArt.SelectedValue
        cmd.Parameters.Add("@art_des", SqlDbType.NVarChar).Value = CBArt.Text
        cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = cantidad
        cmd.Parameters.Add("@Peso", SqlDbType.Decimal).Value = obj.OBPesoFPO
        cmd.Parameters.Add("@Iva", SqlDbType.Decimal).Value = iva
        cmd.Parameters.Add("@Fpo", SqlDbType.Decimal).Value = fpo
        cmd.Parameters.Add("@Subtotal", SqlDbType.Decimal).Value = subtotal
        cmd.ExecuteNonQuery()
        cnn2.Close()
    End Sub
    Private Sub Insertar_Guia_9()
        'Bultos
        If GB1.Enabled = True Then
            Dim peso As Decimal
            Dim item As Integer = Buscar_PosicionItem()
            Try
                peso = CDec(TB1.Text)
                open_conection2()
                Dim cmd As New SqlClient.SqlCommand
                cmd = New SqlClient.SqlCommand("SPINSGUIATEMP", cnn2)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@NumGuia", SqlDbType.Int).Value = obj._OBNumGuiaAle
                cmd.Parameters.Add("@NumItem", SqlDbType.Int).Value = item
                cmd.Parameters.Add("@co_art", SqlDbType.NChar).Value = "000"
                cmd.Parameters.Add("@art_des", SqlDbType.NVarChar).Value = "- Peso / Franqueo del Bulto"
                cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = 1
                cmd.Parameters.Add("@Peso", SqlDbType.Decimal).Value = peso
                cmd.Parameters.Add("@Iva", SqlDbType.Decimal).Value = 0
                cmd.Parameters.Add("@Fpo", SqlDbType.Decimal).Value = obj._OBT9Z1
                cmd.Parameters.Add("@Subtotal", SqlDbType.Decimal).Value = 0
                cmd.ExecuteNonQuery()
                cnn2.Close()
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede insertar los datos del Bulto 1!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        If GB2.Enabled = True Then
            Dim peso As Decimal
            Dim item As Integer = Buscar_PosicionItem()
            Try
                peso = CDec(TB2.Text)
                open_conection2()
                Dim cmd As New SqlClient.SqlCommand
                cmd = New SqlClient.SqlCommand("SPINSGUIATEMP", cnn2)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@NumGuia", SqlDbType.Int).Value = obj._OBNumGuiaAle
                cmd.Parameters.Add("@NumItem", SqlDbType.Int).Value = item
                cmd.Parameters.Add("@co_art", SqlDbType.NChar).Value = "000"
                cmd.Parameters.Add("@art_des", SqlDbType.NVarChar).Value = "- Peso / Franqueo del Bulto"
                cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = 1
                cmd.Parameters.Add("@Peso", SqlDbType.Decimal).Value = peso
                cmd.Parameters.Add("@Iva", SqlDbType.Decimal).Value = 0
                cmd.Parameters.Add("@Fpo", SqlDbType.Decimal).Value = obj._OBT9Z2
                cmd.Parameters.Add("@Subtotal", SqlDbType.Decimal).Value = 0
                cmd.ExecuteNonQuery()
                cnn2.Close()
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede insertar los datos del Bulto 2!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        If GB3.Enabled = True Then
            Dim peso As Decimal
            Dim item As Integer = Buscar_PosicionItem()
            Try
                peso = CDec(TB3.Text)
                open_conection2()
                Dim cmd As New SqlClient.SqlCommand
                cmd = New SqlClient.SqlCommand("SPINSGUIATEMP", cnn2)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@NumGuia", SqlDbType.Int).Value = obj._OBNumGuiaAle
                cmd.Parameters.Add("@NumItem", SqlDbType.Int).Value = item
                cmd.Parameters.Add("@co_art", SqlDbType.NChar).Value = "000"
                cmd.Parameters.Add("@art_des", SqlDbType.NVarChar).Value = "- Peso / Franqueo del Bulto"
                cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = 1
                cmd.Parameters.Add("@Peso", SqlDbType.Decimal).Value = peso
                cmd.Parameters.Add("@Iva", SqlDbType.Decimal).Value = 0
                cmd.Parameters.Add("@Fpo", SqlDbType.Decimal).Value = obj._OBT9Z3
                cmd.Parameters.Add("@Subtotal", SqlDbType.Decimal).Value = 0
                cmd.ExecuteNonQuery()
                cnn2.Close()
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede insertar los datos del Bulto 3!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        If GB4.Enabled = True Then
            Dim peso As Decimal
            Dim item As Integer = Buscar_PosicionItem()
            Try
                peso = CDec(TB4.Text)
                open_conection2()
                Dim cmd As New SqlClient.SqlCommand
                cmd = New SqlClient.SqlCommand("SPINSGUIATEMP", cnn2)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@NumGuia", SqlDbType.Int).Value = obj._OBNumGuiaAle
                cmd.Parameters.Add("@NumItem", SqlDbType.Int).Value = item
                cmd.Parameters.Add("@co_art", SqlDbType.NChar).Value = "000"
                cmd.Parameters.Add("@art_des", SqlDbType.NVarChar).Value = "- Peso / Franqueo del Bulto"
                cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = 1
                cmd.Parameters.Add("@Peso", SqlDbType.Decimal).Value = peso
                cmd.Parameters.Add("@Iva", SqlDbType.Decimal).Value = 0
                cmd.Parameters.Add("@Fpo", SqlDbType.Decimal).Value = obj._OBT9Z4
                cmd.Parameters.Add("@Subtotal", SqlDbType.Decimal).Value = 0
                cmd.ExecuteNonQuery()
                cnn2.Close()
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede insertar los datos del Bulto 4!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        If GB5.Enabled = True Then
            Dim peso As Decimal
            Dim item As Integer = Buscar_PosicionItem()
            Try
                peso = CDec(TB5.Text)
                open_conection2()
                Dim cmd As New SqlClient.SqlCommand
                cmd = New SqlClient.SqlCommand("SPINSGUIATEMP", cnn2)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@NumGuia", SqlDbType.Int).Value = obj._OBNumGuiaAle
                cmd.Parameters.Add("@NumItem", SqlDbType.Int).Value = item
                cmd.Parameters.Add("@co_art", SqlDbType.NChar).Value = "000"
                cmd.Parameters.Add("@art_des", SqlDbType.NVarChar).Value = "- Peso / Franqueo del Bulto"
                cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = 1
                cmd.Parameters.Add("@Peso", SqlDbType.Decimal).Value = peso
                cmd.Parameters.Add("@Iva", SqlDbType.Decimal).Value = 0
                cmd.Parameters.Add("@Fpo", SqlDbType.Decimal).Value = obj._OBT9Z5
                cmd.Parameters.Add("@Subtotal", SqlDbType.Decimal).Value = 0
                cmd.ExecuteNonQuery()
                cnn2.Close()
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede insertar los datos del Bulto 5!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        If GB6.Enabled = True Then
            Dim peso As Decimal
            Dim item As Integer = Buscar_PosicionItem()
            Try
                peso = CDec(TB6.Text)
                open_conection2()
                Dim cmd As New SqlClient.SqlCommand
                cmd = New SqlClient.SqlCommand("SPINSGUIATEMP", cnn2)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@NumGuia", SqlDbType.Int).Value = obj._OBNumGuiaAle
                cmd.Parameters.Add("@NumItem", SqlDbType.Int).Value = item
                cmd.Parameters.Add("@co_art", SqlDbType.NChar).Value = "000"
                cmd.Parameters.Add("@art_des", SqlDbType.NVarChar).Value = "- Peso / Franqueo del Bulto"
                cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = 1
                cmd.Parameters.Add("@Peso", SqlDbType.Decimal).Value = peso
                cmd.Parameters.Add("@Iva", SqlDbType.Decimal).Value = 0
                cmd.Parameters.Add("@Fpo", SqlDbType.Decimal).Value = obj._OBT9Z6
                cmd.Parameters.Add("@Subtotal", SqlDbType.Decimal).Value = 0
                cmd.ExecuteNonQuery()
                cnn2.Close()
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede insertar los datos del Bulto 6!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        If GB7.Enabled = True Then
            Dim peso As Decimal
            Dim item As Integer = Buscar_PosicionItem()
            Try
                peso = CDec(TB7.Text)
                open_conection2()
                Dim cmd As New SqlClient.SqlCommand
                cmd = New SqlClient.SqlCommand("SPINSGUIATEMP", cnn2)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@NumGuia", SqlDbType.Int).Value = obj._OBNumGuiaAle
                cmd.Parameters.Add("@NumItem", SqlDbType.Int).Value = item
                cmd.Parameters.Add("@co_art", SqlDbType.NChar).Value = "000"
                cmd.Parameters.Add("@art_des", SqlDbType.NVarChar).Value = "- Peso / Franqueo del Bulto"
                cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = 1
                cmd.Parameters.Add("@Peso", SqlDbType.Decimal).Value = peso
                cmd.Parameters.Add("@Iva", SqlDbType.Decimal).Value = 0
                cmd.Parameters.Add("@Fpo", SqlDbType.Decimal).Value = obj._OBT9Z7
                cmd.Parameters.Add("@Subtotal", SqlDbType.Decimal).Value = 0
                cmd.ExecuteNonQuery()
                cnn2.Close()
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede insertar los datos del Bulto 7!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        If GB8.Enabled = True Then
            Dim peso As Decimal
            Dim item As Integer = Buscar_PosicionItem()
            Try
                peso = CDec(TB8.Text)
                open_conection2()
                Dim cmd As New SqlClient.SqlCommand
                cmd = New SqlClient.SqlCommand("SPINSGUIATEMP", cnn2)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@NumGuia", SqlDbType.Int).Value = obj._OBNumGuiaAle
                cmd.Parameters.Add("@NumItem", SqlDbType.Int).Value = item
                cmd.Parameters.Add("@co_art", SqlDbType.NChar).Value = "000"
                cmd.Parameters.Add("@art_des", SqlDbType.NVarChar).Value = "- Peso / Franqueo del Bulto"
                cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = 1
                cmd.Parameters.Add("@Peso", SqlDbType.Decimal).Value = peso
                cmd.Parameters.Add("@Iva", SqlDbType.Decimal).Value = 0
                cmd.Parameters.Add("@Fpo", SqlDbType.Decimal).Value = obj._OBT9Z8
                cmd.Parameters.Add("@Subtotal", SqlDbType.Decimal).Value = 0
                cmd.ExecuteNonQuery()
                cnn2.Close()
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede insertar los datos del Bulto 8!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        If GB9.Enabled = True Then
            Dim peso As Decimal
            Dim item As Integer = Buscar_PosicionItem()
            Try
                peso = CDec(TB9.Text)
                open_conection2()
                Dim cmd As New SqlClient.SqlCommand
                cmd = New SqlClient.SqlCommand("SPINSGUIATEMP", cnn2)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@NumGuia", SqlDbType.Int).Value = obj._OBNumGuiaAle
                cmd.Parameters.Add("@NumItem", SqlDbType.Int).Value = item
                cmd.Parameters.Add("@co_art", SqlDbType.NChar).Value = "000"
                cmd.Parameters.Add("@art_des", SqlDbType.NVarChar).Value = "- Peso / Franqueo del Bulto"
                cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = 1
                cmd.Parameters.Add("@Peso", SqlDbType.Decimal).Value = peso
                cmd.Parameters.Add("@Iva", SqlDbType.Decimal).Value = 0
                cmd.Parameters.Add("@Fpo", SqlDbType.Decimal).Value = obj._OBT9Z9
                cmd.Parameters.Add("@Subtotal", SqlDbType.Decimal).Value = 0
                cmd.ExecuteNonQuery()
                cnn2.Close()
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede insertar los datos del Bulto 9!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        'Sobres
        If GBS1.Enabled = True Then
            Dim peso As Decimal
            Dim item As Integer = Buscar_PosicionItem()
            Try
                peso = CDec(TBS1.Text)
                open_conection2()
                Dim cmd As New SqlClient.SqlCommand
                cmd = New SqlClient.SqlCommand("SPINSGUIATEMP", cnn2)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@NumGuia", SqlDbType.Int).Value = obj._OBNumGuiaAle
                cmd.Parameters.Add("@NumItem", SqlDbType.Int).Value = item
                cmd.Parameters.Add("@co_art", SqlDbType.NChar).Value = "000"
                cmd.Parameters.Add("@art_des", SqlDbType.NVarChar).Value = "- Peso / Franqueo del Sobre o Paquete"
                cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = 1
                cmd.Parameters.Add("@Peso", SqlDbType.Decimal).Value = peso
                cmd.Parameters.Add("@Iva", SqlDbType.Decimal).Value = 0
                cmd.Parameters.Add("@Fpo", SqlDbType.Decimal).Value = obj._OBT9ZS1
                cmd.Parameters.Add("@Subtotal", SqlDbType.Decimal).Value = 0
                cmd.ExecuteNonQuery()
                cnn2.Close()
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede insertar los datos del Sobre o Paquete 1!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        If GBS2.Enabled = True Then
            Dim peso As Decimal
            Dim item As Integer = Buscar_PosicionItem()
            Try
                peso = CDec(TBS2.Text)
                open_conection2()
                Dim cmd As New SqlClient.SqlCommand
                cmd = New SqlClient.SqlCommand("SPINSGUIATEMP", cnn2)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@NumGuia", SqlDbType.Int).Value = obj._OBNumGuiaAle
                cmd.Parameters.Add("@NumItem", SqlDbType.Int).Value = item
                cmd.Parameters.Add("@co_art", SqlDbType.NChar).Value = "000"
                cmd.Parameters.Add("@art_des", SqlDbType.NVarChar).Value = "- Peso / Franqueo del Sobre o Paquete"
                cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = 1
                cmd.Parameters.Add("@Peso", SqlDbType.Decimal).Value = peso
                cmd.Parameters.Add("@Iva", SqlDbType.Decimal).Value = 0
                cmd.Parameters.Add("@Fpo", SqlDbType.Decimal).Value = obj._OBT9ZS2
                cmd.Parameters.Add("@Subtotal", SqlDbType.Decimal).Value = 0
                cmd.ExecuteNonQuery()
                cnn2.Close()
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede insertar los datos del Sobre o Paquete 2!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        If GBS3.Enabled = True Then
            Dim peso As Decimal
            Dim item As Integer = Buscar_PosicionItem()
            Try
                peso = CDec(TBS3.Text)
                open_conection2()
                Dim cmd As New SqlClient.SqlCommand
                cmd = New SqlClient.SqlCommand("SPINSGUIATEMP", cnn2)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@NumGuia", SqlDbType.Int).Value = obj._OBNumGuiaAle
                cmd.Parameters.Add("@NumItem", SqlDbType.Int).Value = item
                cmd.Parameters.Add("@co_art", SqlDbType.NChar).Value = "000"
                cmd.Parameters.Add("@art_des", SqlDbType.NVarChar).Value = "- Peso / Franqueo del Sobre o Paquete"
                cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = 1
                cmd.Parameters.Add("@Peso", SqlDbType.Decimal).Value = peso
                cmd.Parameters.Add("@Iva", SqlDbType.Decimal).Value = 0
                cmd.Parameters.Add("@Fpo", SqlDbType.Decimal).Value = obj._OBT9ZS3
                cmd.Parameters.Add("@Subtotal", SqlDbType.Decimal).Value = 0
                cmd.ExecuteNonQuery()
                cnn2.Close()
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede insertar los datos del Sobre o Paquete 3!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        If GBS4.Enabled = True Then
            Dim peso As Decimal
            Dim item As Integer = Buscar_PosicionItem()
            Try
                peso = CDec(TBS4.Text)
                open_conection2()
                Dim cmd As New SqlClient.SqlCommand
                cmd = New SqlClient.SqlCommand("SPINSGUIATEMP", cnn2)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@NumGuia", SqlDbType.Int).Value = obj._OBNumGuiaAle
                cmd.Parameters.Add("@NumItem", SqlDbType.Int).Value = item
                cmd.Parameters.Add("@co_art", SqlDbType.NChar).Value = "000"
                cmd.Parameters.Add("@art_des", SqlDbType.NVarChar).Value = "- Peso / Franqueo del Sobre o Paquete"
                cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = 1
                cmd.Parameters.Add("@Peso", SqlDbType.Decimal).Value = peso
                cmd.Parameters.Add("@Iva", SqlDbType.Decimal).Value = 0
                cmd.Parameters.Add("@Fpo", SqlDbType.Decimal).Value = obj._OBT9ZS4
                cmd.Parameters.Add("@Subtotal", SqlDbType.Decimal).Value = 0
                cmd.ExecuteNonQuery()
                cnn2.Close()
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede insertar los datos del Sobre o Paquete 4!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        If GBS5.Enabled = True Then
            Dim peso As Decimal
            Dim item As Integer = Buscar_PosicionItem()
            Try
                peso = CDec(TBS5.Text)
                open_conection2()
                Dim cmd As New SqlClient.SqlCommand
                cmd = New SqlClient.SqlCommand("SPINSGUIATEMP", cnn2)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@NumGuia", SqlDbType.Int).Value = obj._OBNumGuiaAle
                cmd.Parameters.Add("@NumItem", SqlDbType.Int).Value = item
                cmd.Parameters.Add("@co_art", SqlDbType.NChar).Value = "000"
                cmd.Parameters.Add("@art_des", SqlDbType.NVarChar).Value = "- Peso / Franqueo del Sobre o Paquete"
                cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = 1
                cmd.Parameters.Add("@Peso", SqlDbType.Decimal).Value = peso
                cmd.Parameters.Add("@Iva", SqlDbType.Decimal).Value = 0
                cmd.Parameters.Add("@Fpo", SqlDbType.Decimal).Value = obj._OBT9ZS5
                cmd.Parameters.Add("@Subtotal", SqlDbType.Decimal).Value = 0
                cmd.ExecuteNonQuery()
                cnn2.Close()
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede insertar los datos del Sobre o Paquete 5!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        If GBS6.Enabled = True Then
            Dim peso As Decimal
            Dim item As Integer = Buscar_PosicionItem()
            Try
                peso = CDec(TBS6.Text)
                open_conection2()
                Dim cmd As New SqlClient.SqlCommand
                cmd = New SqlClient.SqlCommand("SPINSGUIATEMP", cnn2)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@NumGuia", SqlDbType.Int).Value = obj._OBNumGuiaAle
                cmd.Parameters.Add("@NumItem", SqlDbType.Int).Value = item
                cmd.Parameters.Add("@co_art", SqlDbType.NChar).Value = "000"
                cmd.Parameters.Add("@art_des", SqlDbType.NVarChar).Value = "- Peso / Franqueo del Sobre o Paquete"
                cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = 1
                cmd.Parameters.Add("@Peso", SqlDbType.Decimal).Value = peso
                cmd.Parameters.Add("@Iva", SqlDbType.Decimal).Value = 0
                cmd.Parameters.Add("@Fpo", SqlDbType.Decimal).Value = obj._OBT9ZS6
                cmd.Parameters.Add("@Subtotal", SqlDbType.Decimal).Value = 0
                cmd.ExecuteNonQuery()
                cnn2.Close()
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede insertar los datos del Sobre o Paquete 6!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        If GBS7.Enabled = True Then
            Dim peso As Decimal
            Dim item As Integer = Buscar_PosicionItem()
            Try
                peso = CDec(TBS7.Text)
                open_conection2()
                Dim cmd As New SqlClient.SqlCommand
                cmd = New SqlClient.SqlCommand("SPINSGUIATEMP", cnn2)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@NumGuia", SqlDbType.Int).Value = obj._OBNumGuiaAle
                cmd.Parameters.Add("@NumItem", SqlDbType.Int).Value = item
                cmd.Parameters.Add("@co_art", SqlDbType.NChar).Value = "000"
                cmd.Parameters.Add("@art_des", SqlDbType.NVarChar).Value = "- Peso / Franqueo del Sobre o Paquete"
                cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = 1
                cmd.Parameters.Add("@Peso", SqlDbType.Decimal).Value = peso
                cmd.Parameters.Add("@Iva", SqlDbType.Decimal).Value = 0
                cmd.Parameters.Add("@Fpo", SqlDbType.Decimal).Value = obj._OBT9ZS7
                cmd.Parameters.Add("@Subtotal", SqlDbType.Decimal).Value = 0
                cmd.ExecuteNonQuery()
                cnn2.Close()
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede insertar los datos del Sobre o Paquete 7!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        If GBS8.Enabled = True Then
            Dim peso As Decimal
            Dim item As Integer = Buscar_PosicionItem()
            Try
                peso = CDec(TBS8.Text)
                open_conection2()
                Dim cmd As New SqlClient.SqlCommand
                cmd = New SqlClient.SqlCommand("SPINSGUIATEMP", cnn2)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@NumGuia", SqlDbType.Int).Value = obj._OBNumGuiaAle
                cmd.Parameters.Add("@NumItem", SqlDbType.Int).Value = item
                cmd.Parameters.Add("@co_art", SqlDbType.NChar).Value = "000"
                cmd.Parameters.Add("@art_des", SqlDbType.NVarChar).Value = "- Peso / Franqueo del Sobre o Paquete"
                cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = 1
                cmd.Parameters.Add("@Peso", SqlDbType.Decimal).Value = peso
                cmd.Parameters.Add("@Iva", SqlDbType.Decimal).Value = 0
                cmd.Parameters.Add("@Fpo", SqlDbType.Decimal).Value = obj._OBT9ZS8
                cmd.Parameters.Add("@Subtotal", SqlDbType.Decimal).Value = 0
                cmd.ExecuteNonQuery()
                cnn2.Close()
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede insertar los datos del Sobre o Paquete 8!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        If GBS9.Enabled = True Then
            Dim peso As Decimal
            Dim item As Integer = Buscar_PosicionItem()
            Try
                peso = CDec(TBS9.Text)
                open_conection2()
                Dim cmd As New SqlClient.SqlCommand
                cmd = New SqlClient.SqlCommand("SPINSGUIATEMP", cnn2)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add("@NumGuia", SqlDbType.Int).Value = obj._OBNumGuiaAle
                cmd.Parameters.Add("@NumItem", SqlDbType.Int).Value = item
                cmd.Parameters.Add("@co_art", SqlDbType.NChar).Value = "000"
                cmd.Parameters.Add("@art_des", SqlDbType.NVarChar).Value = "- Peso / Franqueo del Sobre o Paquete"
                cmd.Parameters.Add("@Cantidad", SqlDbType.Int).Value = 1
                cmd.Parameters.Add("@Peso", SqlDbType.Decimal).Value = peso
                cmd.Parameters.Add("@Iva", SqlDbType.Decimal).Value = 0
                cmd.Parameters.Add("@Fpo", SqlDbType.Decimal).Value = obj._OBT9ZS9
                cmd.Parameters.Add("@Subtotal", SqlDbType.Decimal).Value = 0
                cmd.ExecuteNonQuery()
                cnn2.Close()
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede insertar los datos del Sobre o Paquete 9!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
    End Sub
#End Region
    Private Function Validar_RangoPeso() As Boolean
        open_conection2()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim x As String = obj.OBPesoFPO.ToString.Replace(",", ".")
        Dim sql As String = "select * from ARTICULOS where [co_art]='" & CBArt.SelectedValue & "' and '" & x & "' BETWEEN [desde] and [hasta]"
        cmd = New SqlClient.SqlCommand(sql, cnn2)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Return True
        Else
            Return False
        End If
        cnn2.Close()
    End Function
#End Region
#Region "Botones"
    Private Sub CalcularButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CalcularButton.Click
        Validar_AntesCalcular()
        If Validar_AntesCalcular() = True Then
            Buscar_IVA()
            Dim peso As Decimal
            Try
                peso = CDec(TBPeso.Text)
                obj.OBPesoFPO = (Math.Round(peso, 2)).ToString
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede convertir el peso a un Valor calculable!", titulo:="AVISO: Validación - Sistema")
            End Try
            If CBCamion.CheckState = CheckState.Checked Then
                If CBDescuento.CheckState = CheckState.Checked Then
                    If obj.Miva = True Then
                        obj.OBIvaSub = (obj._OBPorcenDec * obj._OBIva) / 100
                    ElseIf obj.Miva = False Then
                        obj.OBIvaSub = 0
                    End If
                    obj._OBPrecioTotal = obj._OBPorcenDec + obj.OBIvaSub
                    LBSubTotal.Text = obj._OBPorcenDec.ToString("#,#.00 Bs")
                    LBIVA.Text = obj.OBIvaSub.ToString("#,#.00 Bs")
                    LBTotal.Text = obj._OBPrecioTotal.ToString("#,#.00 Bs")
                    obj._OBPrecioDesc = obj._OBPorcenDec
                ElseIf CBDescuento.CheckState = CheckState.Unchecked Or CBDescuento.CheckState = CheckState.Indeterminate Then
                    If obj.Miva = True Then
                        obj.OBIvaSub = (obj._OBPrecioArt * obj._OBIva) / 100
                    ElseIf obj.Miva = False Then
                        obj.OBIvaSub = 0
                    End If
                    obj._OBPrecioTotal = obj._OBPrecioArt + obj.OBIvaSub
                    LBSubTotal.Text = obj._OBPrecioArt.ToString("#,#.00 Bs")
                    LBIVA.Text = obj.OBIvaSub.ToString("#,#.00 Bs")
                    LBTotal.Text = obj._OBPrecioTotal.ToString("#,#.00 Bs")
                    obj._OBPrecioSub = obj._OBPrecioArt
                End If
            ElseIf CBCamion.CheckState = CheckState.Unchecked Or CBCamion.CheckState = CheckState.Indeterminate Then
                Validar_RangoPeso()
                If Validar_RangoPeso() = True Then
                    Buscar_FPO()
                    If CBDescuento.CheckState = CheckState.Checked Then
                        Calcular_FPO_BultosSobres()
                        Dim s As String = CBArt.Text
                        If (s.Contains("SOB")) Then
                            obj._OBPrecioDesc = obj._OBPorcenDec
                        Else
                            obj._OBPrecioDesc = obj._OBPesoFPO * obj._OBPorcenDec
                        End If
                        If obj._OBPorcenFPO = 0 Then
                            obj._OBFPOSub = 0
                        Else
                            obj._OBFPOSub = (obj._OBPorcenFPO * obj._OBPrecioDesc) / 100
                        End If
                        If obj.Miva = True Then
                            obj.OBIvaSub = (obj._OBPrecioDesc * obj._OBIva) / 100
                        ElseIf obj.Miva = False Then
                            obj.OBIvaSub = 0
                        End If
                        If obj._OBTotalFPO9 <> Nothing Then
                            obj._OBPrecioTotal = obj._OBPrecioDesc + obj.OBIvaSub + obj._OBTotalFPO9
                        Else
                            obj._OBPrecioTotal = obj._OBPrecioDesc + obj.OBIvaSub + obj._OBFPOSub
                        End If
                        'obj._OBPrecioTotal = obj._OBPrecioDesc + obj.OBIvaSub + obj._OBFPOSub
                        LBSubTotal.Text = obj._OBPrecioDesc.ToString("#,#.00 Bs")
                        LBIVA.Text = obj.OBIvaSub.ToString("#,#.00 Bs")
                        LBFPO.Text = obj._OBFPOSub.ToString("#,#.00 Bs")
                        LBTotal.Text = obj._OBPrecioTotal.ToString("#,#.00 Bs")
                    ElseIf CBDescuento.CheckState = CheckState.Unchecked Or CBDescuento.CheckState = CheckState.Indeterminate Then
                        Calcular_FPO_BultosSobres()
                        Dim s As String = CBArt.Text
                        If (s.Contains("SOB")) Then
                            obj._OBPrecioSub = obj._OBPrecioArt
                        Else
                            obj._OBPrecioSub = obj._OBPesoFPO * obj._OBPrecioArt
                        End If
                        If obj._OBPorcenFPO = 0 Then
                            obj._OBFPOSub = 0
                        Else
                            obj._OBFPOSub = (obj._OBPorcenFPO * obj._OBPrecioSub) / 100
                        End If
                        If obj.Miva = True Then
                            obj.OBIvaSub = (obj._OBPrecioSub * obj._OBIva) / 100
                        ElseIf obj.Miva = False Then
                            obj.OBIvaSub = 0
                        End If
                        If obj._OBTotalFPO9 <> Nothing Then
                            obj._OBPrecioTotal = obj._OBPrecioSub + obj.OBIvaSub + obj._OBTotalFPO9
                        Else
                            obj._OBPrecioTotal = obj._OBPrecioSub + obj.OBIvaSub + obj._OBFPOSub
                        End If
                        'obj._OBPrecioTotal = obj._OBPrecioSub + obj.OBIvaSub + obj._OBFPOSub
                        LBSubTotal.Text = obj._OBPrecioSub.ToString("#,#.00 Bs")
                        LBIVA.Text = obj.OBIvaSub.ToString("#,#.00 Bs")
                        LBFPO.Text = obj._OBFPOSub.ToString("#,#.00 Bs")
                        LBTotal.Text = obj._OBPrecioTotal.ToString("#,#.00 Bs")
                    End If
                Else
                    MsgBoxInfo(mensaje:="El peso no coincide con el Artículo seleccionado!", titulo:="AVISO: Validación - Sistema")
                    Exit Sub
                End If
            End If
        Else
            Exit Sub
        End If
    End Sub
    Private Sub Cerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cerrar.Click
        Limpiar_Formulario()
        Me.Close()
    End Sub
    Private Sub Cancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancelar.Click
        Limpiar_Formulario()
    End Sub
    Private Sub BCalcularPV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BCalcularPV.Click
        Validar_AntesCalcularPV()
        If Validar_AntesCalcularPV() = True Then
            Buscar_FPV()
            LBFPV.Text = obj.OBFPV.ToString("###,###.00")
            Dim largo, ancho, alto, result1, result2 As Decimal
            Try
                largo = CDec(TBLargo.Text)
                ancho = CDec(TBAncho.Text)
                alto = CDec(TBAlto.Text)
                result1 = (largo / 100) * (ancho / 100) * (alto / 100)
                result2 = result1 * obj._OBFPV
                LBPesoVol.Text = (Math.Round(result2, 2)).ToString
                TBPeso.Text = (Math.Round(result2, 2)).ToString
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede calcular el peso Volumetrico", titulo:="AVISO: Validación - Sistema")
            End Try
        Else
            Exit Sub
        End If
    End Sub
    Private Sub Agregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Agregar.Click
        ' Buscar_PosicionItem()
        If MGB9.Enabled = True Then
            Insertar_Guia_9()
        End If
        Insertar_Guia()
        Limpiar_Formulario()
        Select_DGV_Guia()
        CBArt.SelectedIndex = -1
    End Sub
#End Region
#Region "Funciones y Procedimientos"
    'Funciones y Procedimientos
    Private Sub Limpiar_Formulario()
        GBPesoVol.Enabled = False
        CBActPesoVolum.CheckState = CheckState.Unchecked
        CBCamion.CheckState = CheckState.Unchecked
        CBDescuento.CheckState = CheckState.Checked
        LBFPO.Text = "0 Bs."
        LBFPV.Text = Nothing
        LBPesoVol.Text = Nothing
        LBTotal.Text = "0 Bs."
        LBIVA.Text = "0 Bs."
        LBSubTotal.Text = "0 Bs."
        TBPeso.Text = Nothing
        obj.OBIvaSub = Nothing
        obj._OBPrecioSub = Nothing
        obj._OBPorcenDec = Nothing
        obj._OBPrecioArt = Nothing
        obj.OBPesoFPO = Nothing
        obj._OBPorcenFPO = Nothing
        obj._OBFPOSub = Nothing
        obj._OBPrecioTotal = Nothing

        obj._OBTotalFPO9 = Nothing
        TB1.Text = Nothing
        TB2.Text = Nothing
        TB3.Text = Nothing
        TB4.Text = Nothing
        TB5.Text = Nothing
        TB6.Text = Nothing
        TB7.Text = Nothing
        TB8.Text = Nothing
        TB9.Text = Nothing
        LB1.Text = Nothing
        LB2.Text = Nothing
        LB3.Text = Nothing
        LB4.Text = Nothing
        LB5.Text = Nothing
        LB6.Text = Nothing
        LB7.Text = Nothing
        LB8.Text = Nothing
        LB9.Text = Nothing

        TBS1.Text = Nothing
        TBS2.Text = Nothing
        TBS3.Text = Nothing
        TBS4.Text = Nothing
        TBS5.Text = Nothing
        TBS6.Text = Nothing
        TBS7.Text = Nothing
        TBS8.Text = Nothing
        TBS9.Text = Nothing
        LBS1.Text = Nothing
        LBS2.Text = Nothing
        LBS3.Text = Nothing
        LBS4.Text = Nothing
        LBS5.Text = Nothing
        LBS6.Text = Nothing
        LBS7.Text = Nothing
        LBS8.Text = Nothing
        LBS9.Text = Nothing
        TPeso9.Text = Nothing
        TFPO9.Text = Nothing

        'Bultos
        obj._OBT9Z1 = Nothing
        obj._OBT9Z2 = Nothing
        obj._OBT9Z3 = Nothing
        obj._OBT9Z4 = Nothing
        obj._OBT9Z5 = Nothing
        obj._OBT9Z6 = Nothing
        obj._OBT9Z7 = Nothing
        obj._OBT9Z8 = Nothing
        obj._OBT9Z9 = Nothing
        'Sobres
        obj._OBT9ZS1 = Nothing
        obj._OBT9ZS2 = Nothing
        obj._OBT9ZS3 = Nothing
        obj._OBT9ZS4 = Nothing
        obj._OBT9ZS5 = Nothing
        obj._OBT9ZS6 = Nothing
        obj._OBT9ZS7 = Nothing
        obj._OBT9ZS8 = Nothing
        obj._OBT9ZS9 = Nothing
        obj._OBTotalFPO9 = Nothing

        TBNBultos.Text = Nothing
        TBPaquetes.Text = Nothing
        TBSobres.Text = Nothing
        CB9.CheckState = CheckState.Unchecked

        CBArt.Focus()
    End Sub
#Region "Eventos Changed"
#Region "Peso Volumetrico"
    Private Sub CBPesoVolum_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBActPesoVolum.CheckedChanged
        If CBActPesoVolum.CheckState = CheckState.Checked Then
            GBPesoVol.Enabled = True
        ElseIf CBActPesoVolum.CheckState = CheckState.Unchecked Or CBActPesoVolum.CheckState = CheckState.Indeterminate Then
            GBPesoVol.Enabled = False
        End If
    End Sub
#End Region
    Private Sub CBArt_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles CBArt.SelectionChangeCommitted
        Buscar_Precio()
        Buscar_Descuento()
        Dim s As String = CBArt.Text
        If (s.Contains("CAM")) Then
            CBCamion.CheckState = CheckState.Checked
        Else
            CBCamion.CheckState = CheckState.Unchecked
        End If
    End Sub
#Region "Bultos y Sobres Rango de 9"
    Private Sub CB9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CB9.CheckedChanged
        If CB9.CheckState = CheckState.Checked Then
            MGB9.Enabled = True
        Else
            MGB9.Enabled = False
            TB1.Text = Nothing
            TB2.Text = Nothing
            TB3.Text = Nothing
            TB4.Text = Nothing
            TB5.Text = Nothing
            TB6.Text = Nothing
            TB7.Text = Nothing
            TB8.Text = Nothing
            TB9.Text = Nothing
            LB1.Text = Nothing
            LB2.Text = Nothing
            LB3.Text = Nothing
            LB4.Text = Nothing
            LB5.Text = Nothing
            LB6.Text = Nothing
            LB7.Text = Nothing
            LB8.Text = Nothing
            LB9.Text = Nothing
            TBS1.Text = Nothing
            TBS2.Text = Nothing
            TBS3.Text = Nothing
            TBS4.Text = Nothing
            TBS5.Text = Nothing
            TBS6.Text = Nothing
            TBS7.Text = Nothing
            TBS8.Text = Nothing
            TBS9.Text = Nothing
            LBS1.Text = Nothing
            LBS2.Text = Nothing
            LBS3.Text = Nothing
            LBS4.Text = Nothing
            LBS5.Text = Nothing
            LBS6.Text = Nothing
            LBS7.Text = Nothing
            LBS8.Text = Nothing
            LBS9.Text = Nothing
            TPeso9.Text = Nothing
            TFPO9.Text = Nothing
            TBPaquetes.Text = Nothing
            TBSobres.Text = Nothing
            GB1.Enabled = False
            GB2.Enabled = False
            GB3.Enabled = False
            GB4.Enabled = False
            GB5.Enabled = False
            GB6.Enabled = False
            GB7.Enabled = False
            GB8.Enabled = False
            GB9.Enabled = False
            GBS1.Enabled = False
            GBS2.Enabled = False
            GBS3.Enabled = False
            GBS4.Enabled = False
            GBS5.Enabled = False
            GBS6.Enabled = False
            GBS7.Enabled = False
            GBS8.Enabled = False
            GBS9.Enabled = False
            obj._OBT9Z1 = Nothing
            obj._OBT9Z2 = Nothing
            obj._OBT9Z3 = Nothing
            obj._OBT9Z4 = Nothing
            obj._OBT9Z5 = Nothing
            obj._OBT9Z6 = Nothing
            obj._OBT9Z7 = Nothing
            obj._OBT9Z8 = Nothing
            obj._OBT9Z9 = Nothing
            obj._OBT9ZS1 = Nothing
            obj._OBT9ZS2 = Nothing
            obj._OBT9ZS3 = Nothing
            obj._OBT9ZS4 = Nothing
            obj._OBT9ZS5 = Nothing
            obj._OBT9ZS6 = Nothing
            obj._OBT9ZS7 = Nothing
            obj._OBT9ZS8 = Nothing
            obj._OBT9ZS9 = Nothing
            obj._OBTotalFPO9 = Nothing
        End If
    End Sub
    Private Sub GB1_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GB1.EnabledChanged
        If GB1.Enabled = False Then
            TB1.Text = Nothing
            LB1.Text = Nothing
        End If
    End Sub
    Private Sub GB2_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GB2.EnabledChanged
        If GB2.Enabled = False Then
            TB2.Text = Nothing
            LB2.Text = Nothing
        End If
    End Sub
    Private Sub GB3_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GB3.EnabledChanged
        If GB3.Enabled = False Then
            TB3.Text = Nothing
            LB3.Text = Nothing
        End If
    End Sub
    Private Sub GB4_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GB4.EnabledChanged
        If GB4.Enabled = False Then
            TB4.Text = Nothing
            LB4.Text = Nothing
        End If
    End Sub
    Private Sub GB5_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GB5.EnabledChanged
        If GB5.Enabled = False Then
            TB5.Text = Nothing
            LB5.Text = Nothing
        End If
    End Sub
    Private Sub GB6_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GB6.EnabledChanged
        If GB6.Enabled = False Then
            TB6.Text = Nothing
            LB6.Text = Nothing
        End If
    End Sub
    Private Sub GB7_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GB7.EnabledChanged
        If GB7.Enabled = False Then
            TB7.Text = Nothing
            LB7.Text = Nothing
        End If
    End Sub
    Private Sub GB8_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GB8.EnabledChanged
        If GB8.Enabled = False Then
            TB8.Text = Nothing
            LB8.Text = Nothing
        End If
    End Sub
    Private Sub GB9_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GB9.EnabledChanged
        If GB9.Enabled = False Then
            TB9.Text = Nothing
            LB9.Text = Nothing
        End If
    End Sub
    Private Sub GBS1_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GBS1.EnabledChanged
        If GB1.Enabled = False Then
            TBS1.Text = Nothing
            LBS1.Text = Nothing
        End If
    End Sub
    Private Sub GBS2_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GBS2.EnabledChanged
        If GB2.Enabled = False Then
            TBS2.Text = Nothing
            LBS2.Text = Nothing
        End If
    End Sub
    Private Sub GBS3_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GBS3.EnabledChanged
        If GB3.Enabled = False Then
            TBS3.Text = Nothing
            LBS3.Text = Nothing
        End If
    End Sub
    Private Sub GBS4_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GBS4.EnabledChanged
        If GB4.Enabled = False Then
            TBS4.Text = Nothing
            LBS4.Text = Nothing
        End If
    End Sub
    Private Sub GBS5_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GBS5.EnabledChanged
        If GB5.Enabled = False Then
            TBS5.Text = Nothing
            LBS5.Text = Nothing
        End If
    End Sub
    Private Sub GBS6_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GBS6.EnabledChanged
        If GB6.Enabled = False Then
            TBS6.Text = Nothing
            LBS6.Text = Nothing
        End If
    End Sub
    Private Sub GBS7_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GBS7.EnabledChanged
        If GB7.Enabled = False Then
            TBS7.Text = Nothing
            LBS7.Text = Nothing
        End If
    End Sub
    Private Sub GBS8_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GBS8.EnabledChanged
        If GB8.Enabled = False Then
            TBS8.Text = Nothing
            LBS8.Text = Nothing
        End If
    End Sub
    Private Sub GBS9_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GBS9.EnabledChanged
        If GB9.Enabled = False Then
            TBS9.Text = Nothing
            LBS9.Text = Nothing
        End If
    End Sub
#End Region
#End Region
#End Region
#Region "Validación de TextBox"
#Region "KeyPress"
    Private Sub TBPeso_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TBPeso.KeyPress
        TBPeso.MaxLength = 10
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(NUMEROSDEC(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TBLargo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TBLargo.KeyPress
        TBLargo.MaxLength = 6
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(NUMEROSDEC(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TBAncho_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TBAncho.KeyPress
        TBAncho.MaxLength = 6
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(NUMEROSDEC(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TBAlto_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TBAlto.KeyPress
        TBAlto.MaxLength = 6
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(NUMEROSDEC(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TBPaquetes_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TBPaquetes.KeyPress
        TBPaquetes.MaxLength = 1
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(NUMEROS(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TBSobres_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TBSobres.KeyPress
        TBSobres.MaxLength = 1
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(NUMEROS(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TB1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TB1.KeyPress
        TB1.MaxLength = 6
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(NUMEROSDEC(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TB2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TB2.KeyPress
        TB2.MaxLength = 6
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(NUMEROSDEC(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TB3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TB3.KeyPress
        TB3.MaxLength = 6
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(NUMEROSDEC(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TB4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TB4.KeyPress
        TB4.MaxLength = 6
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(NUMEROSDEC(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TB5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TB5.KeyPress
        TB5.MaxLength = 6
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(NUMEROSDEC(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TB6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TB6.KeyPress
        TB6.MaxLength = 6
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(NUMEROSDEC(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TB7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TB7.KeyPress
        TB7.MaxLength = 6
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(NUMEROSDEC(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TB8_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TB8.KeyPress
        TB8.MaxLength = 6
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(NUMEROSDEC(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TB9_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TB9.KeyPress
        TB9.MaxLength = 6
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(NUMEROSDEC(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TBS1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TBS1.KeyPress
        TBS1.MaxLength = 6
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(NUMEROSDEC(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TBS2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TBS2.KeyPress
        TBS2.MaxLength = 6
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(NUMEROSDEC(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TBS3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TBS3.KeyPress
        TBS3.MaxLength = 6
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(NUMEROSDEC(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TBS4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TBS4.KeyPress
        TBS4.MaxLength = 6
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(NUMEROSDEC(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TBS5_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TBS5.KeyPress
        TBS5.MaxLength = 6
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(NUMEROSDEC(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TBS6_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TBS6.KeyPress
        TBS6.MaxLength = 6
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(NUMEROSDEC(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TBS7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TBS7.KeyPress
        TBS7.MaxLength = 6
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(NUMEROSDEC(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TBS8_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TBS8.KeyPress
        TBS8.MaxLength = 6
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(NUMEROSDEC(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TBS9_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TBS9.KeyPress
        TBS9.MaxLength = 6
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(NUMEROSDEC(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
    Private Sub TBNBultos_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TBNBultos.KeyPress
        TBNBultos.MaxLength = 6
        Dim KeyAscii As Short = CShort(Asc(e.KeyChar))
        KeyAscii = CShort(NUMEROS(KeyAscii))
        If KeyAscii = 0 Then
            e.Handled = True
        End If
    End Sub
#End Region
    Private Function Validar_AntesCalcular() As Boolean
        'Validar Espacios Vacios y TextBox en Blanco
        If (String.IsNullOrEmpty(TBPeso.Text)) Then
            Me.ErrorProvider.BlinkRate = 200
            Me.ErrorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink
            ErrorProvider.SetError(TBPeso, "Indique el peso!")
            TBPeso.Focus()
            Return False
        Else
            ErrorProvider.SetError(TBPeso, "")
        End If
        Return True
    End Function
    Private Function Validar_AntesCalcularPV() As Boolean
        If (String.IsNullOrEmpty(TBLargo.Text)) Then
            Me.ErrorProvider.BlinkRate = 200
            Me.ErrorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink
            ErrorProvider.SetError(TBLargo, "Indique la medida!")
            TBLargo.Focus()
            Return False
        Else
            ErrorProvider.SetError(TBLargo, "")
        End If
        If (String.IsNullOrEmpty(TBAncho.Text)) Then
            Me.ErrorProvider.BlinkRate = 200
            Me.ErrorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink
            ErrorProvider.SetError(TBAncho, "Indique la medida!")
            TBAncho.Focus()
            Return False
        Else
            ErrorProvider.SetError(TBAncho, "")
        End If
        If (String.IsNullOrEmpty(TBAlto.Text)) Then
            Me.ErrorProvider.BlinkRate = 200
            Me.ErrorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink
            ErrorProvider.SetError(TBAlto, "Indique la medida!")
            TBAlto.Focus()
            Return False
        Else
            ErrorProvider.SetError(TBAlto, "")
        End If
        Return True
    End Function
    Private Function Validar_CatidadBultos() As Boolean
        'Validar Espacios Vacios y TextBox en Blanco
        If (String.IsNullOrEmpty(TBPaquetes.Text)) Then
            Me.ErrorProvider.BlinkRate = 200
            Me.ErrorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink
            ErrorProvider.SetError(TBPaquetes, "Debe indicar una cantidad de Bultos!")
            TBPaquetes.Focus()
            Return False
        Else
            ErrorProvider.SetError(TBPaquetes, "")
        End If
        Return True
    End Function
    Private Function Validar_CatidadSobres() As Boolean
        'Validar Espacios Vacios y TextBox en Blanco
        If (String.IsNullOrEmpty(TBSobres.Text)) Then
            Me.ErrorProvider.BlinkRate = 200
            Me.ErrorProvider.BlinkStyle = ErrorBlinkStyle.AlwaysBlink
            ErrorProvider.SetError(TBSobres, "Debe indicar una cantidad de Sobres o Paquetes!")
            TBSobres.Focus()
            Return False
        Else
            ErrorProvider.SetError(TBSobres, "")
        End If
        Return True
    End Function
#End Region
#Region "Bultos y Sobres Rango de 9"
    Private Sub Activar_Bultos()
        Validar_CatidadBultos()
        If Validar_CatidadBultos() = True Then
            Dim x As Integer = TBPaquetes.Text
            Select Case x
                Case 1
                    GB1.Enabled = True
                    GB2.Enabled = False
                    GB3.Enabled = False
                    GB4.Enabled = False
                    GB5.Enabled = False
                    GB6.Enabled = False
                    GB7.Enabled = False
                    GB8.Enabled = False
                    GB9.Enabled = False
                Case 2
                    GB1.Enabled = True
                    GB2.Enabled = True
                    GB3.Enabled = False
                    GB4.Enabled = False
                    GB5.Enabled = False
                    GB6.Enabled = False
                    GB7.Enabled = False
                    GB8.Enabled = False
                    GB9.Enabled = False
                Case 3
                    GB1.Enabled = True
                    GB2.Enabled = True
                    GB3.Enabled = True
                    GB4.Enabled = False
                    GB5.Enabled = False
                    GB6.Enabled = False
                    GB7.Enabled = False
                    GB8.Enabled = False
                    GB9.Enabled = False
                Case 4
                    GB1.Enabled = True
                    GB2.Enabled = True
                    GB3.Enabled = True
                    GB4.Enabled = True
                    GB5.Enabled = False
                    GB6.Enabled = False
                    GB7.Enabled = False
                    GB8.Enabled = False
                    GB9.Enabled = False
                Case 5
                    GB1.Enabled = True
                    GB2.Enabled = True
                    GB3.Enabled = True
                    GB4.Enabled = True
                    GB5.Enabled = True
                    GB6.Enabled = False
                    GB7.Enabled = False
                    GB8.Enabled = False
                    GB9.Enabled = False
                Case 6
                    GB1.Enabled = True
                    GB2.Enabled = True
                    GB3.Enabled = True
                    GB4.Enabled = True
                    GB5.Enabled = True
                    GB6.Enabled = True
                    GB7.Enabled = False
                    GB8.Enabled = False
                    GB9.Enabled = False
                Case 7
                    GB1.Enabled = True
                    GB2.Enabled = True
                    GB3.Enabled = True
                    GB4.Enabled = True
                    GB5.Enabled = True
                    GB6.Enabled = True
                    GB7.Enabled = True
                    GB8.Enabled = False
                    GB9.Enabled = False
                Case 8
                    GB1.Enabled = True
                    GB2.Enabled = True
                    GB3.Enabled = True
                    GB4.Enabled = True
                    GB5.Enabled = True
                    GB6.Enabled = True
                    GB7.Enabled = True
                    GB8.Enabled = True
                    GB9.Enabled = False
                Case 9
                    GB1.Enabled = True
                    GB2.Enabled = True
                    GB3.Enabled = True
                    GB4.Enabled = True
                    GB5.Enabled = True
                    GB6.Enabled = True
                    GB7.Enabled = True
                    GB8.Enabled = True
                    GB9.Enabled = True
            End Select
        Else
            MsgBoxInfo(mensaje:="Debe indicar una cantidad de Bultos!", titulo:="AVISO: Validación - Sistema")
        End If
    End Sub
    Private Sub Activar_Sobres()
        Validar_CatidadSobres()
        If Validar_CatidadSobres() = True Then
            Dim x As Integer = TBSobres.Text
            Select Case x
                Case 1
                    GBS1.Enabled = True
                    GBS2.Enabled = False
                    GBS3.Enabled = False
                    GBS4.Enabled = False
                    GBS5.Enabled = False
                    GBS6.Enabled = False
                    GBS7.Enabled = False
                    GBS8.Enabled = False
                    GBS9.Enabled = False
                Case 2
                    GBS1.Enabled = True
                    GBS2.Enabled = True
                    GBS3.Enabled = False
                    GBS4.Enabled = False
                    GBS5.Enabled = False
                    GBS6.Enabled = False
                    GBS7.Enabled = False
                    GBS8.Enabled = False
                    GBS9.Enabled = False
                Case 3
                    GBS1.Enabled = True
                    GBS2.Enabled = True
                    GBS3.Enabled = True
                    GBS4.Enabled = False
                    GBS5.Enabled = False
                    GBS6.Enabled = False
                    GBS7.Enabled = False
                    GBS8.Enabled = False
                    GBS9.Enabled = False
                Case 4
                    GBS1.Enabled = True
                    GBS2.Enabled = True
                    GBS3.Enabled = True
                    GBS4.Enabled = True
                    GBS5.Enabled = False
                    GBS6.Enabled = False
                    GBS7.Enabled = False
                    GBS8.Enabled = False
                    GBS9.Enabled = False
                Case 5
                    GBS1.Enabled = True
                    GBS2.Enabled = True
                    GBS3.Enabled = True
                    GBS4.Enabled = True
                    GBS5.Enabled = True
                    GBS6.Enabled = False
                    GBS7.Enabled = False
                    GBS8.Enabled = False
                    GBS9.Enabled = False
                Case 6
                    GBS1.Enabled = True
                    GBS2.Enabled = True
                    GBS3.Enabled = True
                    GBS4.Enabled = True
                    GBS5.Enabled = True
                    GBS6.Enabled = True
                    GBS7.Enabled = False
                    GBS8.Enabled = False
                    GBS9.Enabled = False
                Case 7
                    GBS1.Enabled = True
                    GBS2.Enabled = True
                    GBS3.Enabled = True
                    GBS4.Enabled = True
                    GBS5.Enabled = True
                    GBS6.Enabled = True
                    GBS7.Enabled = True
                    GBS8.Enabled = False
                    GBS9.Enabled = False
                Case 8
                    GBS1.Enabled = True
                    GBS2.Enabled = True
                    GBS3.Enabled = True
                    GBS4.Enabled = True
                    GBS5.Enabled = True
                    GBS6.Enabled = True
                    GBS7.Enabled = True
                    GBS8.Enabled = True
                    GBS9.Enabled = False
                Case 9
                    GBS1.Enabled = True
                    GBS2.Enabled = True
                    GBS3.Enabled = True
                    GBS4.Enabled = True
                    GBS5.Enabled = True
                    GBS6.Enabled = True
                    GBS7.Enabled = True
                    GBS8.Enabled = True
                    GBS9.Enabled = True
            End Select
        Else
            MsgBoxInfo(mensaje:="Debe indicar una cantidad de Sobres o Paquetes!", titulo:="AVISO: Validación - Sistema")
        End If
    End Sub
    Private Sub Aplicar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Aplicar.Click
        Activar_Bultos()
    End Sub
    Private Sub Aplicar2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Aplicar2.Click
        Activar_Sobres()
    End Sub
    Private Sub Totalizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Totalizar.Click
        Dim pesoBulto1 As Decimal
        Dim pesoBulto2 As Decimal
        Dim pesoBulto3 As Decimal
        Dim pesoBulto4 As Decimal
        Dim pesoBulto5 As Decimal
        Dim pesoBulto6 As Decimal
        Dim pesoBulto7 As Decimal
        Dim pesoBulto8 As Decimal
        Dim pesoBulto9 As Decimal
        Dim totalBulto As Decimal
        Dim pesoSobre1 As Decimal
        Dim pesoSobre2 As Decimal
        Dim pesoSobre3 As Decimal
        Dim pesoSobre4 As Decimal
        Dim pesoSobre5 As Decimal
        Dim pesoSobre6 As Decimal
        Dim pesoSobre7 As Decimal
        Dim pesoSobre8 As Decimal
        Dim pesoSobre9 As Decimal
        Dim totalSobre As Decimal
        Dim stotal As Decimal
        Try
            If TB1.Text <> Nothing Then
                pesoBulto1 = CDec(TB1.Text)
            End If
            If TB2.Text <> Nothing Then
                pesoBulto2 = CDec(TB2.Text)
            End If
            If TB3.Text <> Nothing Then
                pesoBulto3 = CDec(TB3.Text)
            End If
            If TB4.Text <> Nothing Then
                pesoBulto4 = CDec(TB4.Text)
            End If
            If TB5.Text <> Nothing Then
                pesoBulto5 = CDec(TB5.Text)
            End If
            If TB6.Text <> Nothing Then
                pesoBulto6 = CDec(TB6.Text)
            End If
            If TB7.Text <> Nothing Then
                pesoBulto7 = CDec(TB7.Text)
            End If
            If TB8.Text <> Nothing Then
                pesoBulto8 = CDec(TB8.Text)
            End If
            If TB9.Text <> Nothing Then
                pesoBulto9 = CDec(TB9.Text)
            End If
            If TBS1.Text <> Nothing Then
                pesoSobre1 = CDec(TBS1.Text)
            End If
            If TBS2.Text <> Nothing Then
                pesoSobre2 = CDec(TBS2.Text)
            End If
            If TBS3.Text <> Nothing Then
                pesoSobre3 = CDec(TBS3.Text)
            End If
            If TBS4.Text <> Nothing Then
                pesoSobre4 = CDec(TBS4.Text)
            End If
            If TBS5.Text <> Nothing Then
                pesoSobre5 = CDec(TBS5.Text)
            End If
            If TBS6.Text <> Nothing Then
                pesoSobre6 = CDec(TBS6.Text)
            End If
            If TBS7.Text <> Nothing Then
                pesoSobre7 = CDec(TBS7.Text)
            End If
            If TBS8.Text <> Nothing Then
                pesoSobre8 = CDec(TBS8.Text)
            End If
            If TBS9.Text <> Nothing Then
                pesoSobre9 = CDec(TBS9.Text)
            End If
            totalBulto = pesoBulto1 + pesoBulto2 + pesoBulto3 + pesoBulto4 + pesoBulto5 + pesoBulto6 + pesoBulto7 + pesoBulto8 + pesoBulto9
            totalSobre = pesoSobre1 + pesoSobre2 + pesoSobre3 + pesoSobre4 + pesoSobre5 + pesoSobre6 + pesoSobre7 + pesoSobre8 + pesoSobre9
            stotal = totalBulto + totalSobre
            TPeso9.Text = Math.Round(stotal, 2).ToString
            TBPeso.Text = (Math.Round(stotal, 2)).ToString
            Dim paq As Integer = 0
            Dim sob As Integer = 0
            Dim tbul As Integer
            If TBPaquetes.Text <> Nothing Then
                paq = CInt(TBPaquetes.Text)
            End If
            If TBSobres.Text <> Nothing Then
                sob = CInt(TBSobres.Text)
            End If
            tbul = paq + sob
            TBNBultos.Text = CStr(tbul)
        Catch ex As Exception
            MsgBoxInfo(mensaje:="No se puede convertir el peso a un Valor calculable!", titulo:="AVISO: Validación - Sistema")
        End Try
    End Sub
    Private Sub Calcular_FPO_BultosSobres()
        'Bultos
        If GB1.Enabled = True Then
            Dim peso As Decimal
            Dim y, i, z, w As Decimal
            Try
                peso = CDec(TB1.Text)
                y = (Math.Round(peso, 2)).ToString
                If y <= 30 Then
                    Dim x As String = y.ToString.Replace(",", ".")
                    i = Buscar_FPO2(x)
                    'If CBDescuento.CheckState = CheckState.Checked Then

                    'End If
                    w = (i * Buscar_Precio_Bultos()) / 100
                    z = w * y
                    obj._OBT9Z1 = z
                    LB1.Text = z.ToString("#,#.00 Bs")
                Else
                    LB1.Text = Nothing
                End If
            Catch ex As Exception
            MsgBoxInfo(mensaje:="No se puede convertir el peso a un Valor calculable!", titulo:="AVISO: Validación - Sistema")
        End Try
        End If
        If GB2.Enabled = True Then
            Dim peso As Decimal
            Dim y, i, z, w As Decimal
            Try
                peso = CDec(TB2.Text)
                y = (Math.Round(peso, 2)).ToString
                If y <= 30 Then
                    Dim x As String = y.ToString.Replace(",", ".")
                    i = Buscar_FPO2(x)
                    w = (i * Buscar_Precio_Bultos()) / 100
                    z = w * y
                    obj._OBT9Z2 = z
                    LB2.Text = z.ToString("#,#.00 Bs")
                Else
                    LB2.Text = Nothing
                End If
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede convertir el peso a un Valor calculable!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        If GB3.Enabled = True Then
            Dim peso As Decimal
            Dim y, i, z, w As Decimal
            Try
                peso = CDec(TB3.Text)
                y = (Math.Round(peso, 2)).ToString
                If y <= 30 Then
                    Dim x As String = y.ToString.Replace(",", ".")
                    i = Buscar_FPO2(x)
                    w = (i * Buscar_Precio_Bultos()) / 100
                    z = w * y
                    obj._OBT9Z3 = z
                    LB3.Text = z.ToString("#,#.00 Bs")
                Else
                    LB3.Text = Nothing
                End If
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede convertir el peso a un Valor calculable!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        If GB4.Enabled = True Then
            Dim peso As Decimal
            Dim y, i, z, w As Decimal
            Try
                peso = CDec(TB4.Text)
                y = (Math.Round(peso, 2)).ToString
                If y <= 30 Then
                    Dim x As String = y.ToString.Replace(",", ".")
                    i = Buscar_FPO2(x)
                    w = (i * Buscar_Precio_Bultos()) / 100
                    z = w * y
                    obj._OBT9Z4 = z
                    LB4.Text = z.ToString("#,#.00 Bs")
                Else
                    LB4.Text = Nothing
                End If
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede convertir el peso a un Valor calculable!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        If GB5.Enabled = True Then
            Dim peso As Decimal
            Dim y, i, z, w As Decimal
            Try
                peso = CDec(TB5.Text)
                y = (Math.Round(peso, 2)).ToString
                If y <= 30 Then
                    Dim x As String = y.ToString.Replace(",", ".")
                    i = Buscar_FPO2(x)
                    w = (i * Buscar_Precio_Bultos()) / 100
                    z = w * y
                    obj._OBT9Z5 = z
                    LB5.Text = z.ToString("#,#.00 Bs")
                Else
                    LB5.Text = Nothing
                End If
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede convertir el peso a un Valor calculable!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        If GB6.Enabled = True Then
            Dim peso As Decimal
            Dim y, i, z, w As Decimal
            Try
                peso = CDec(TB6.Text)
                y = (Math.Round(peso, 2)).ToString
                If y <= 30 Then
                    Dim x As String = y.ToString.Replace(",", ".")
                    i = Buscar_FPO2(x)
                    w = (i * Buscar_Precio_Bultos()) / 100
                    z = w * y
                    obj._OBT9Z6 = z
                    LB6.Text = z.ToString("#,#.00 Bs")
                Else
                    LB6.Text = Nothing
                End If
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede convertir el peso a un Valor calculable!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        If GB7.Enabled = True Then
            Dim peso As Decimal
            Dim y, i, z, w As Decimal
            Try
                peso = CDec(TB7.Text)
                y = (Math.Round(peso, 2)).ToString
                If y <= 30 Then
                    Dim x As String = y.ToString.Replace(",", ".")
                    i = Buscar_FPO2(x)
                    w = (i * Buscar_Precio_Bultos()) / 100
                    z = w * y
                    obj._OBT9Z7 = z
                    LB7.Text = z.ToString("#,#.00 Bs")
                Else
                    LB7.Text = Nothing
                End If
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede convertir el peso a un Valor calculable!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        If GB8.Enabled = True Then
            Dim peso As Decimal
            Dim y, i, z, w As Decimal
            Try
                peso = CDec(TB8.Text)
                y = (Math.Round(peso, 2)).ToString
                If y <= 30 Then
                    Dim x As String = y.ToString.Replace(",", ".")
                    i = Buscar_FPO2(x)
                    w = (i * Buscar_Precio_Bultos()) / 100
                    z = w * y
                    obj._OBT9Z8 = z
                    LB8.Text = z.ToString("#,#.00 Bs")
                Else
                    LB8.Text = Nothing
                End If
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede convertir el peso a un Valor calculable!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        If GB9.Enabled = True Then
            Dim peso As Decimal
            Dim y, i, z, w As Decimal
            Try
                peso = CDec(TB9.Text)
                y = (Math.Round(peso, 2)).ToString
                If y <= 30 Then
                    Dim x As String = y.ToString.Replace(",", ".")
                    i = Buscar_FPO2(x)
                    w = (i * Buscar_Precio_Bultos()) / 100
                    z = w * y
                    obj._OBT9Z9 = z
                    LB9.Text = z.ToString("#,#.00 Bs")
                Else
                    LB9.Text = Nothing
                End If
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede convertir el peso a un Valor calculable!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        'Sobres y paquetes
        If GBS1.Enabled = True Then
            Dim peso As Decimal
            Dim y, i, z, w As Decimal
            Try
                peso = CDec(TBS1.Text)
                y = (Math.Round(peso, 2)).ToString
                If y <= 30 Then
                    Dim x As String = y.ToString.Replace(",", ".")
                    i = Buscar_FPO2(x)
                    w = (i * Buscar_Precio_Sobres()) / 100
                    z = w '* y
                    obj._OBT9ZS1 = z
                    LBS1.Text = z.ToString("#,#.00 Bs")
                Else
                    LBS1.Text = Nothing
                End If
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede convertir el peso a un Valor calculable!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        If GBS2.Enabled = True Then
            Dim peso As Decimal
            Dim y, i, z, w As Decimal
            Try
                peso = CDec(TBS2.Text)
                y = (Math.Round(peso, 2)).ToString
                If y <= 30 Then
                    Dim x As String = y.ToString.Replace(",", ".")
                    i = Buscar_FPO2(x)
                    w = (i * Buscar_Precio_Sobres()) / 100
                    z = w '* y
                    obj._OBT9ZS2 = z
                    LBS2.Text = z.ToString("#,#.00 Bs")
                Else
                    LBS2.Text = Nothing
                End If
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede convertir el peso a un Valor calculable!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        If GBS3.Enabled = True Then
            Dim peso As Decimal
            Dim y, i, z, w As Decimal
            Try
                peso = CDec(TBS3.Text)
                y = (Math.Round(peso, 2)).ToString
                If y <= 30 Then
                    Dim x As String = y.ToString.Replace(",", ".")
                    i = Buscar_FPO2(x)
                    w = (i * Buscar_Precio_Sobres()) / 100
                    z = w '* y
                    obj._OBT9ZS3 = z
                    LBS3.Text = z.ToString("#,#.00 Bs")
                Else
                    LBS3.Text = Nothing
                End If
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede convertir el peso a un Valor calculable!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        If GBS4.Enabled = True Then
            Dim peso As Decimal
            Dim y, i, z, w As Decimal
            Try
                peso = CDec(TBS4.Text)
                y = (Math.Round(peso, 2)).ToString
                If y <= 30 Then
                    Dim x As String = y.ToString.Replace(",", ".")
                    i = Buscar_FPO2(x)
                    w = (i * Buscar_Precio_Sobres()) / 100
                    z = w '* y
                    obj._OBT9ZS4 = z
                    LBS4.Text = z.ToString("#,#.00 Bs")
                Else
                    LBS4.Text = Nothing
                End If
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede convertir el peso a un Valor calculable!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        If GBS5.Enabled = True Then
            Dim peso As Decimal
            Dim y, i, z, w As Decimal
            Try
                peso = CDec(TBS5.Text)
                y = (Math.Round(peso, 2)).ToString
                If y <= 30 Then
                    Dim x As String = y.ToString.Replace(",", ".")
                    i = Buscar_FPO2(x)
                    w = (i * Buscar_Precio_Sobres()) / 100
                    z = w '* y
                    obj._OBT9ZS5 = z
                    LBS5.Text = z.ToString("#,#.00 Bs")
                Else
                    LBS5.Text = Nothing
                End If
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede convertir el peso a un Valor calculable!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        If GBS6.Enabled = True Then
            Dim peso As Decimal
            Dim y, i, z, w As Decimal
            Try
                peso = CDec(TBS6.Text)
                y = (Math.Round(peso, 2)).ToString
                If y <= 30 Then
                    Dim x As String = y.ToString.Replace(",", ".")
                    i = Buscar_FPO2(x)
                    w = (i * Buscar_Precio_Sobres()) / 100
                    z = w '* y
                    obj._OBT9ZS6 = z
                    LBS6.Text = z.ToString("#,#.00 Bs")
                Else
                    LBS6.Text = Nothing
                End If
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede convertir el peso a un Valor calculable!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        If GBS7.Enabled = True Then
            Dim peso As Decimal
            Dim y, i, z, w As Decimal
            Try
                peso = CDec(TBS7.Text)
                y = (Math.Round(peso, 2)).ToString
                If y <= 30 Then
                    Dim x As String = y.ToString.Replace(",", ".")
                    i = Buscar_FPO2(x)
                    w = (i * Buscar_Precio_Sobres()) / 100
                    z = w '* y
                    obj._OBT9ZS7 = z
                    LBS7.Text = z.ToString("#,#.00 Bs")
                Else
                    LBS7.Text = Nothing
                End If
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede convertir el peso a un Valor calculable!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        If GBS8.Enabled = True Then
            Dim peso As Decimal
            Dim y, i, z, w As Decimal
            Try
                peso = CDec(TBS8.Text)
                y = (Math.Round(peso, 2)).ToString
                If y <= 30 Then
                    Dim x As String = y.ToString.Replace(",", ".")
                    i = Buscar_FPO2(x)
                    w = (i * Buscar_Precio_Sobres()) / 100
                    z = w '* y
                    obj._OBT9ZS8 = z
                    LBS8.Text = z.ToString("#,#.00 Bs")
                Else
                    LBS8.Text = Nothing
                End If
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede convertir el peso a un Valor calculable!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        If GBS9.Enabled = True Then
            Dim peso As Decimal
            Dim y, i, z, w As Decimal
            Try
                peso = CDec(TBS9.Text)
                y = (Math.Round(peso, 2)).ToString
                If y <= 30 Then
                    Dim x As String = y.ToString.Replace(",", ".")
                    i = Buscar_FPO2(x)
                    w = (i * Buscar_Precio_Sobres()) / 100
                    z = w '* y
                    obj._OBT9ZS9 = z
                    LBS9.Text = z.ToString("#,#.00 Bs")
                Else
                    LBS9.Text = Nothing
                End If
            Catch ex As Exception
                MsgBoxInfo(mensaje:="No se puede convertir el peso a un Valor calculable!", titulo:="AVISO: Validación - Sistema")
            End Try
        End If
        Dim TBultos As Decimal
        Dim TSobres As Decimal
        TBultos = obj._OBT9Z1 + obj._OBT9Z2 + obj._OBT9Z3 + obj._OBT9Z4 + obj._OBT9Z5 + obj._OBT9Z6 + obj._OBT9Z7 + obj._OBT9Z8 + obj._OBT9Z9
        TSobres = obj._OBT9ZS1 + obj._OBT9ZS2 + obj._OBT9ZS3 + obj._OBT9ZS4 + obj._OBT9ZS5 + obj._OBT9ZS6 + obj._OBT9ZS7 + obj._OBT9ZS8 + obj._OBT9ZS9
        obj._OBTotalFPO9 = TBultos + TSobres
        TFPO9.Text = obj._OBTotalFPO9.ToString("#,#.00 Bs")
    End Sub
    Private Function Buscar_Precio_Bultos() As Decimal
        Dim resultado As Decimal
        open_conection2()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select monto from ARTICULOPREC where [co_art]='002' and [co_zona]='" & obj._OBIDZona & "'"
        cmd = New SqlClient.SqlCommand(sql, cnn2)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim row As DataRow
            row = dt.Rows(0)
            Dim valor As Decimal
            valor = row("monto")
            resultado = valor * obj._OBDolar
            Return resultado
        End If
        cnn2.Close()
    End Function
    Private Function Buscar_Precio_Sobres() As Decimal
        Dim resultado As Decimal
        open_conection2()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select monto from ARTICULOPREC where [co_art]='001' and [co_zona]='" & obj._OBIDZona & "'"
        cmd = New SqlClient.SqlCommand(sql, cnn2)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim row As DataRow
            row = dt.Rows(0)
            Dim valor As Decimal
            valor = row("monto")
            resultado = valor * obj._OBDolar
            Return resultado
        End If
        cnn2.Close()
    End Function
#End Region
End Class