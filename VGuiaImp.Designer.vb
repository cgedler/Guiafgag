<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VGuiaImp
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.CRVGuiaImp = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.SuspendLayout()
        '
        'CRVGuiaImp
        '
        Me.CRVGuiaImp.ActiveViewIndex = -1
        Me.CRVGuiaImp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CRVGuiaImp.DisplayGroupTree = False
        Me.CRVGuiaImp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CRVGuiaImp.Location = New System.Drawing.Point(0, 0)
        Me.CRVGuiaImp.Name = "CRVGuiaImp"
        Me.CRVGuiaImp.Size = New System.Drawing.Size(1035, 546)
        Me.CRVGuiaImp.TabIndex = 0
        '
        'VGuiaImp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1035, 546)
        Me.Controls.Add(Me.CRVGuiaImp)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "VGuiaImp"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Guía Impresión"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CRVGuiaImp As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
