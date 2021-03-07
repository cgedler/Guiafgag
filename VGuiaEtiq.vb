Public Class VGuiaEtiq
    Private Sub VGuiaEtiq_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim Report As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Report.Load(path & "\Reports\5.rpt")
        Report.SetParameterValue("@valor", obj._guia_num)
        Report.SetParameterValue("@cantidad", obj.CantPaquetes)
        CRVEtiquetas.ReportSource = Report
    End Sub
End Class