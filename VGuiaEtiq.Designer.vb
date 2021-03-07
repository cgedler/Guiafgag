<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VGuiaEtiq
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
        Me.CRVEtiquetas = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.SuspendLayout()
        '
        'CRVEtiquetas
        '
        Me.CRVEtiquetas.ActiveViewIndex = -1
        Me.CRVEtiquetas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CRVEtiquetas.DisplayGroupTree = False
        Me.CRVEtiquetas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CRVEtiquetas.Location = New System.Drawing.Point(0, 0)
        Me.CRVEtiquetas.Name = "CRVEtiquetas"
        Me.CRVEtiquetas.SelectionFormula = ""
        Me.CRVEtiquetas.Size = New System.Drawing.Size(943, 476)
        Me.CRVEtiquetas.TabIndex = 0
        Me.CRVEtiquetas.ViewTimeSelectionFormula = ""
        '
        'VGuiaEtiq
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(943, 476)
        Me.Controls.Add(Me.CRVEtiquetas)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "VGuiaEtiq"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Guía Etiquetas"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CRVEtiquetas As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
