<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Dim Label21 As System.Windows.Forms.Label
        Dim Label19 As System.Windows.Forms.Label
        Me.LBTotalComiOtrChof = New System.Windows.Forms.Label
        Me.LBTotalComiChof = New System.Windows.Forms.Label
        Me.CBComisionChoferOtros = New System.Windows.Forms.CheckBox
        Me.Button1 = New System.Windows.Forms.Button
        Label21 = New System.Windows.Forms.Label
        Label19 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label21
        '
        Label21.AutoSize = True
        Label21.Location = New System.Drawing.Point(622, 86)
        Label21.Name = "Label21"
        Label21.Size = New System.Drawing.Size(111, 13)
        Label21.TabIndex = 266
        Label21.Text = "Comisión Otros Chof, :"
        '
        'Label19
        '
        Label19.AutoSize = True
        Label19.Location = New System.Drawing.Point(622, 57)
        Label19.Name = "Label19"
        Label19.Size = New System.Drawing.Size(110, 13)
        Label19.TabIndex = 264
        Label19.Text = "Comisión Chof, Ayud :"
        '
        'LBTotalComiOtrChof
        '
        Me.LBTotalComiOtrChof.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LBTotalComiOtrChof.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBTotalComiOtrChof.ForeColor = System.Drawing.SystemColors.WindowText
        Me.LBTotalComiOtrChof.Location = New System.Drawing.Point(738, 82)
        Me.LBTotalComiOtrChof.Name = "LBTotalComiOtrChof"
        Me.LBTotalComiOtrChof.Size = New System.Drawing.Size(154, 20)
        Me.LBTotalComiOtrChof.TabIndex = 267
        Me.LBTotalComiOtrChof.Text = "0 Bs."
        Me.LBTotalComiOtrChof.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LBTotalComiChof
        '
        Me.LBTotalComiChof.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LBTotalComiChof.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBTotalComiChof.ForeColor = System.Drawing.SystemColors.WindowText
        Me.LBTotalComiChof.Location = New System.Drawing.Point(738, 53)
        Me.LBTotalComiChof.Name = "LBTotalComiChof"
        Me.LBTotalComiChof.Size = New System.Drawing.Size(154, 20)
        Me.LBTotalComiChof.TabIndex = 265
        Me.LBTotalComiChof.Text = "0 Bs."
        Me.LBTotalComiChof.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CBComisionChoferOtros
        '
        Me.CBComisionChoferOtros.AutoSize = True
        Me.CBComisionChoferOtros.Location = New System.Drawing.Point(403, 203)
        Me.CBComisionChoferOtros.Name = "CBComisionChoferOtros"
        Me.CBComisionChoferOtros.Size = New System.Drawing.Size(130, 17)
        Me.CBComisionChoferOtros.TabIndex = 268
        Me.CBComisionChoferOtros.Text = "Comisión Chofer Otros"
        Me.CBComisionChoferOtros.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(385, 75)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 269
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(937, 422)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.CBComisionChoferOtros)
        Me.Controls.Add(Me.LBTotalComiOtrChof)
        Me.Controls.Add(Label21)
        Me.Controls.Add(Label19)
        Me.Controls.Add(Me.LBTotalComiChof)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LBTotalComiOtrChof As System.Windows.Forms.Label
    Friend WithEvents LBTotalComiChof As System.Windows.Forms.Label
    Friend WithEvents CBComisionChoferOtros As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
