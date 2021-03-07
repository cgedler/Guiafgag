Public Class VGuiaImp
    Private Sub VGuiaImp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        open_conection2()
        Dim cmd As New SqlClient.SqlCommand
        Dim da As New SqlClient.SqlDataAdapter
        Dim dt As New DataTable
        Dim sql As String = "select * from VGuiaCompleta where [NumGuia]='" & obj._guia_num & "'"
        cmd = New SqlClient.SqlCommand(sql, cnn2)
        da = New SqlClient.SqlDataAdapter(cmd)
        da.Fill(dt)
        If (dt.Rows.Count > 0) Then
            Dim Report As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            Report.Load(path & "\Reports\GuiaImp.rpt")
            Report.SetDataSource(dt)
            CRVGuiaImp.ReportSource = Report
        Else
            Exit Sub
        End If
        cnn2.Close()
    End Sub
End Class