Public Class Guia_C
#Region "Guia Formulario"
    Public _OBNumGuiaAle As Integer  'ID de la guia aleatorio temporal
    Public Property OBNumGuiaAle() As Integer
        Get
            Return _OBNumGuiaAle
        End Get
        Set(ByVal value As Integer)
            _OBNumGuiaAle = value
        End Set
    End Property
    Public _OBIDZona As String  ' ID de la Zona
    Public Property OBIDZona() As String
        Get
            Return _OBIDZona
        End Get
        Set(ByVal value As String)
            _OBIDZona = value
        End Set
    End Property
    Public _OBSubTotal As Double ' Monto de la comision del chofer y ayudantes, Zonas, etc
    Public Property OBSubTotal() As Double
        Get
            Return _OBSubTotal
        End Get
        Set(ByVal value As Double)
            _OBSubTotal = value
        End Set
    End Property
    Public _CantPaquetes As Integer ' Cantidad de Paquetes
    Public Property CantPaquetes() As Integer
        Get
            Return _CantPaquetes
        End Get
        Set(ByVal value As Integer)
            _CantPaquetes = value
        End Set
    End Property
    Public _CantItemsDGV As Integer ' Cantidad de Items en la Guia
    Public Property CantItemsDGV() As Integer
        Get
            Return _CantItemsDGV
        End Get
        Set(ByVal value As Integer)
            _CantItemsDGV = value
        End Set
    End Property
    Public _OBComiZ10 As Double 'Monto factor de la comision Zona 10
    Public Property OBComiZ10() As Double
        Get
            Return _OBComiZ10
        End Get
        Set(ByVal value As Double)
            _OBComiZ10 = value
        End Set
    End Property
    Public _MontoZ10 As Decimal 'Monto de la comision
    Public Property MontoZ10() As Decimal
        Get
            Return _MontoZ10
        End Get
        Set(ByVal value As Decimal)
            _MontoZ10 = value
        End Set
    End Property
    Public _OBComiChofer As Double 'Monto factor de la comision Chofer
    Public Property OBComiChofer() As Double
        Get
            Return _OBComiChofer
        End Get
        Set(ByVal value As Double)
            _OBComiChofer = value
        End Set
    End Property
    Public _MontoChofer As Decimal 'Monto de la comision
    Public Property MontoChofer() As Decimal
        Get
            Return _MontoChofer
        End Get
        Set(ByVal value As Decimal)
            _MontoChofer = value
        End Set
    End Property
    Public _OBComiOtrChofer As Decimal 'Monto factor de la comision Otros Choferes
    Public Property OBComiOtrChofer() As Decimal
        Get
            Return _OBComiOtrChofer
        End Get
        Set(ByVal value As Decimal)
            _OBComiOtrChofer = value
        End Set
    End Property
    Public _MontoOtrChofer As Decimal 'Monto de la comision
    Public Property MontoOtrChofer() As Decimal
        Get
            Return _MontoOtrChofer
        End Get
        Set(ByVal value As Decimal)
            _MontoOtrChofer = value
        End Set
    End Property
    Public _OBComiChoferFletes As Decimal 'Monto factor de la comision Choferes Fletes
    Public Property OBComiChoferFletes() As Decimal
        Get
            Return _OBComiChoferFletes
        End Get
        Set(ByVal value As Decimal)
            _OBComiChoferFletes = value
        End Set
    End Property
    Public _MontoOtrChoferFlete As Decimal 'Monto de la comision Choferes Fletes
    Public Property MontoOtrChoferFlete() As Decimal
        Get
            Return _MontoOtrChoferFlete
        End Get
        Set(ByVal value As Decimal)
            _MontoOtrChoferFlete = value
        End Set
    End Property
    Public _OBComiSeguro As Double 'Monto factor de la comision Seguro
    Public Property OBComiSeguro() As Double
        Get
            Return _OBComiSeguro
        End Get
        Set(ByVal value As Double)
            _OBComiSeguro = value
        End Set
    End Property
    Public _MontoSeguro As Decimal 'Monto de la comision
    Public Property MontoSeguro() As Decimal
        Get
            Return _MontoSeguro
        End Get
        Set(ByVal value As Decimal)
            _MontoSeguro = value
        End Set
    End Property
    Public _MontoDevFact As Double ' Monto devolver factura firmada
    Public Property MontoDevFact() As Double
        Get
            Return _MontoDevFact
        End Get
        Set(ByVal value As Double)
            _MontoDevFact = value
        End Set
    End Property
    Public _empresaDB As String
    Public Property empresaDB() As String
        Get
            Return _empresaDB
        End Get
        Set(ByVal value As String)
            _empresaDB = value
        End Set
    End Property
    Public _zonab As String
    Public Property zonab() As String
        Get
            Return _zonab
        End Get
        Set(ByVal value As String)
            _zonab = value
        End Set
    End Property
    Public _Miva As Boolean 'Maneja IVA
    Public Property Miva() As Boolean
        Get
            Return _Miva
        End Get
        Set(ByVal value As Boolean)
            _Miva = value
        End Set
    End Property
    Public _Mfiscal As Boolean 'Maneja Impresora Fiscal
    Public Property Mfiscal() As Boolean
        Get
            Return _Mfiscal
        End Get
        Set(ByVal value As Boolean)
            _Mfiscal = value
        End Set
    End Property
#End Region
#Region "Insertar Profit"
#Region "Datos del Cliente"
    Public _co_cli As String
    Public Property co_cli() As String
        Get
            Return _co_cli
        End Get
        Set(ByVal value As String)
            _co_cli = value
        End Set
    End Property
    Public _tipo As String
    Public Property tipo() As String
        Get
            Return _tipo
        End Get
        Set(ByVal value As String)
            _tipo = value
        End Set
    End Property
    Public _cli_des As String
    Public Property cli_des() As String
        Get
            Return _cli_des
        End Get
        Set(ByVal value As String)
            _cli_des = value
        End Set
    End Property
    Public _rif As String
    Public Property rif() As String
        Get
            Return _rif
        End Get
        Set(ByVal value As String)
            _rif = value
        End Set
    End Property
    Public _nit As String
    Public Property nit() As String
        Get
            Return _nit
        End Get
        Set(ByVal value As String)
            _nit = value
        End Set
    End Property
    Public _direc1 As String
    Public Property direc1() As String
        Get
            Return _direc1
        End Get
        Set(ByVal value As String)
            _direc1 = value
        End Set
    End Property
    Public _telefonos As String
    Public Property telefonos() As String
        Get
            Return _telefonos
        End Get
        Set(ByVal value As String)
            _telefonos = value
        End Set
    End Property
    Public _contribu As Boolean
    Public Property contribu() As Boolean
        Get
            Return _contribu
        End Get
        Set(ByVal value As Boolean)
            _contribu = value
        End Set
    End Property
#End Region
#Region "Datos de la Factura"
    Public _fact_num As Integer
    Public Property fact_num() As Integer
        Get
            Return _fact_num
        End Get
        Set(ByVal value As Integer)
            _fact_num = value
        End Set
    End Property
    Public _impfis As String
    Public Property impfis() As String
        Get
            Return _impfis
        End Get
        Set(ByVal value As String)
            _impfis = value
        End Set
    End Property
    Public _impfisfac As String
    Public Property impfisfac() As String
        Get
            Return _impfisfac
        End Get
        Set(ByVal value As String)
            _impfisfac = value
        End Set
    End Property
#End Region
#Region "Datos de la Guia"
    Public _guia_num As Integer
    Public Property guia_num() As Integer
        Get
            Return _guia_num
        End Get
        Set(ByVal value As Integer)
            _guia_num = value
        End Set
    End Property
    Public _ZonaDes As String
    Public Property ZonaDes() As String
        Get
            Return _ZonaDes
        End Get
        Set(ByVal value As String)
            _ZonaDes = value
        End Set
    End Property
    Public _co_subl As String
    Public Property co_subl() As String
        Get
            Return _co_subl
        End Get
        Set(ByVal value As String)
            _co_subl = value
        End Set
    End Property
#End Region
#Region "Datos del Cobro"
    Public _RealizarCob As Boolean
    Public Property RealizarCob() As Boolean
        Get
            Return _RealizarCob
        End Get
        Set(ByVal value As Boolean)
            _RealizarCob = value
        End Set
    End Property
    Public _cobro_num As Integer
    Public Property cobro_num() As Integer
        Get
            Return _cobro_num
        End Get
        Set(ByVal value As Integer)
            _cobro_num = value
        End Set
    End Property
    Public _movcaj_num As Integer
    Public Property movcaj_num() As Integer
        Get
            Return _movcaj_num
        End Get
        Set(ByVal value As Integer)
            _movcaj_num = value
        End Set
    End Property
#End Region
#Region "Datos de la Recolecta"
    Public _co_cliRec As String
    Public Property co_cliRec() As String
        Get
            Return _co_cliRec
        End Get
        Set(ByVal value As String)
            _co_cliRec = value
        End Set
    End Property
    Public _cli_desRec As String
    Public Property cli_desRec() As String
        Get
            Return _cli_desRec
        End Get
        Set(ByVal value As String)
            _cli_desRec = value
        End Set
    End Property
    Public _rifRec As String
    Public Property rifRec() As String
        Get
            Return _rifRec
        End Get
        Set(ByVal value As String)
            _rifRec = value
        End Set
    End Property
    Public _direc1Rec As String
    Public Property direc1Rec() As String
        Get
            Return _direc1Rec
        End Get
        Set(ByVal value As String)
            _direc1Rec = value
        End Set
    End Property
    Public _telefonosRec As String
    Public Property telefonosRec() As String
        Get
            Return _telefonosRec
        End Get
        Set(ByVal value As String)
            _telefonosRec = value
        End Set
    End Property
    Public _co_venRec As String
    Public Property co_venRec() As String
        Get
            Return _co_venRec
        End Get
        Set(ByVal value As String)
            _co_venRec = value
        End Set
    End Property
    Public _co_tranRec As String
    Public Property co_tranRec() As String
        Get
            Return _co_tranRec
        End Get
        Set(ByVal value As String)
            _co_tranRec = value
        End Set
    End Property
    Public _forma_pagRec As String
    Public Property forma_pagRec() As String
        Get
            Return _forma_pagRec
        End Get
        Set(ByVal value As String)
            _forma_pagRec = value
        End Set
    End Property

#End Region
#End Region
#Region "Insertar Sistema"
#Region "Datos del Remitente"
    Public _co_cliR As String
    Public Property co_cliR() As String
        Get
            Return _co_cliR
        End Get
        Set(ByVal value As String)
            _co_cliR = value
        End Set
    End Property
    Public _cli_desR As String
    Public Property cli_desR() As String
        Get
            Return _cli_desR
        End Get
        Set(ByVal value As String)
            _cli_desR = value
        End Set
    End Property
    Public _rifR As String
    Public Property rifR() As String
        Get
            Return _rifR
        End Get
        Set(ByVal value As String)
            _rifR = value
        End Set
    End Property
    Public _nitR As String
    Public Property nitR() As String
        Get
            Return _nitR
        End Get
        Set(ByVal value As String)
            _nitR = value
        End Set
    End Property
    Public _direc1R As String
    Public Property direc1R() As String
        Get
            Return _direc1R
        End Get
        Set(ByVal value As String)
            _direc1R = value
        End Set
    End Property
    Public _telefonosR As String
    Public Property telefonosR() As String
        Get
            Return _telefonosR
        End Get
        Set(ByVal value As String)
            _telefonosR = value
        End Set
    End Property
#End Region
#Region "Datos del Destinatario"
    Public _co_cliD As String
    Public Property co_cliD() As String
        Get
            Return _co_cliD
        End Get
        Set(ByVal value As String)
            _co_cliD = value
        End Set
    End Property
    Public _cli_desD As String
    Public Property cli_desD() As String
        Get
            Return _cli_desD
        End Get
        Set(ByVal value As String)
            _cli_desD = value
        End Set
    End Property
    Public _rifD As String
    Public Property rifD() As String
        Get
            Return _rifD
        End Get
        Set(ByVal value As String)
            _rifD = value
        End Set
    End Property
    Public _nitD As String
    Public Property nitD() As String
        Get
            Return _nitD
        End Get
        Set(ByVal value As String)
            _nitD = value
        End Set
    End Property
    Public _direc1D As String
    Public Property direc1D() As String
        Get
            Return _direc1D
        End Get
        Set(ByVal value As String)
            _direc1D = value
        End Set
    End Property
    Public _telefonosD As String
    Public Property telefonosD() As String
        Get
            Return _telefonosD
        End Get
        Set(ByVal value As String)
            _telefonosD = value
        End Set
    End Property
#End Region
#Region "Datos de la Empresa"
    Public _NombreEmpresa As String
    Public Property NombreEmpresa() As String
        Get
            Return _NombreEmpresa
        End Get
        Set(ByVal value As String)
            _NombreEmpresa = value
        End Set
    End Property
    Public _Sucursal As String
    Public Property Sucursal() As String
        Get
            Return _Sucursal
        End Get
        Set(ByVal value As String)
            _Sucursal = value
        End Set
    End Property
    Public _RifEmpresa As String
    Public Property RifEmpresa() As String
        Get
            Return _RifEmpresa
        End Get
        Set(ByVal value As String)
            _RifEmpresa = value
        End Set
    End Property
    Public _NitEmpresa As String
    Public Property NitEmpresa() As String
        Get
            Return _NitEmpresa
        End Get
        Set(ByVal value As String)
            _NitEmpresa = value
        End Set
    End Property
    Public _DirecEmpresa As String
    Public Property DirecEmpresa() As String
        Get
            Return _DirecEmpresa
        End Get
        Set(ByVal value As String)
            _DirecEmpresa = value
        End Set
    End Property
    Public _TelefEmpresa As String
    Public Property TelefEmpresa() As String
        Get
            Return _TelefEmpresa
        End Get
        Set(ByVal value As String)
            _TelefEmpresa = value
        End Set
    End Property
#End Region
#End Region
#Region "Modulo Eliminar"
    Public _OBItemGuia As Integer  ' Item de la Guia para ser eliminado
    Public Property OBItemGuia() As Integer
        Get
            Return _OBItemGuia
        End Get
        Set(ByVal value As Integer)
            _OBItemGuia = value
        End Set
    End Property
#End Region
#Region "Modulo Agregar"
    Public _OBMontoFPO As Double ' Monto del Franqueo Postal
    Public Property OBMontoFPO() As Double
        Get
            Return _OBMontoFPO
        End Get
        Set(ByVal value As Double)
            _OBMontoFPO = value
        End Set
    End Property
    Public _OBPorcenFPO As Decimal ' porcentaje del Franqueo Postal
    Public Property OBPorcenFPO() As Decimal
        Get
            Return _OBPorcenFPO
        End Get
        Set(ByVal value As Decimal)
            _OBPorcenFPO = value
        End Set
    End Property
    Public _OBPesoFPO As Decimal ' Peso del Franqueo Postal
    Public Property OBPesoFPO() As Decimal
        Get
            Return _OBPesoFPO
        End Get
        Set(ByVal value As Decimal)
            _OBPesoFPO = value
        End Set
    End Property
    Public _OBFPOSub As Decimal ' Sub total Franqueo Postal
    Public Property OBFPOSub() As Decimal
        Get
            Return _OBFPOSub
        End Get
        Set(ByVal value As Decimal)
            _OBFPOSub = value
        End Set
    End Property
    Public _OBPrecioArt As Decimal ' Precio del articulo
    Public Property OBPrecioArt() As Decimal
        Get
            Return _OBPrecioArt
        End Get
        Set(ByVal value As Decimal)
            _OBPrecioArt = value
        End Set
    End Property
    Public _OBPorcenDec As Decimal ' porcentaje del Descuento
    Public Property OBPorcenDec() As Decimal
        Get
            Return _OBPorcenDec
        End Get
        Set(ByVal value As Decimal)
            _OBPorcenDec = value
        End Set
    End Property
    Public _OBDolar As Decimal ' Precio del dolar
    Public Property OBDolar() As Decimal
        Get
            Return _OBDolar
        End Get
        Set(ByVal value As Decimal)
            _OBDolar = value
        End Set
    End Property
    Public _OBIva As Decimal ' Precio del dolar
    Public Property OBIva() As Decimal
        Get
            Return _OBIva
        End Get
        Set(ByVal value As Decimal)
            _OBIva = value
        End Set
    End Property
    Public _OBIvaSub As Decimal ' Precio del dolar
    Public Property OBIvaSub() As Decimal
        Get
            Return _OBIvaSub
        End Get
        Set(ByVal value As Decimal)
            _OBIvaSub = value
        End Set
    End Property
    Public _OBPrecioSub As Decimal ' Precio del articulo
    Public Property OBPrecioSub() As Decimal
        Get
            Return _OBPrecioSub
        End Get
        Set(ByVal value As Decimal)
            _OBPrecioSub = value
        End Set
    End Property
    Public _OBPrecioDesc As Decimal ' Precio del articulo
    Public Property OBPrecioDesc() As Decimal
        Get
            Return _OBPrecioDesc
        End Get
        Set(ByVal value As Decimal)
            _OBPrecioDesc = value
        End Set
    End Property
    Public _OBPrecioTotal As Decimal ' Precio total del articulo
    Public Property OBPrecioTotal() As Decimal
        Get
            Return _OBPrecioTotal
        End Get
        Set(ByVal value As Decimal)
            _OBPrecioTotal = value
        End Set
    End Property
    Public _OBFPV As Decimal ' Factor del Peso Volumetrico
    Public Property OBFPV() As Decimal
        Get
            Return _OBFPV
        End Get
        Set(ByVal value As Decimal)
            _OBFPV = value
        End Set
    End Property



    Public _OBTotalFPO9 As Decimal ' Total de los FPO 9
    Public Property OBTotalFPO9() As Decimal
        Get
            Return _OBTotalFPO9
        End Get
        Set(ByVal value As Decimal)
            _OBTotalFPO9 = value
        End Set
    End Property
    Public _OBT9Z1 As Decimal 'Z1
    Public Property OBT9Z1() As Decimal
        Get
            Return _OBT9Z1
        End Get
        Set(ByVal value As Decimal)
            _OBT9Z1 = value
        End Set
    End Property
    Public _OBT9Z2 As Decimal 'Z2
    Public Property OBT9Z2() As Decimal
        Get
            Return _OBT9Z2
        End Get
        Set(ByVal value As Decimal)
            _OBT9Z2 = value
        End Set
    End Property
    Public _OBT9Z3 As Decimal 'Z3
    Public Property OBT9Z3() As Decimal
        Get
            Return _OBT9Z3
        End Get
        Set(ByVal value As Decimal)
            _OBT9Z3 = value
        End Set
    End Property
    Public _OBT9Z4 As Decimal 'Z4
    Public Property OBT9Z4() As Decimal
        Get
            Return _OBT9Z4
        End Get
        Set(ByVal value As Decimal)
            _OBT9Z4 = value
        End Set
    End Property
    Public _OBT9Z5 As Decimal 'Z5
    Public Property OBT9Z5() As Decimal
        Get
            Return _OBT9Z5
        End Get
        Set(ByVal value As Decimal)
            _OBT9Z5 = value
        End Set
    End Property
    Public _OBT9Z6 As Decimal 'Z6
    Public Property OBT9Z6() As Decimal
        Get
            Return _OBT9Z6
        End Get
        Set(ByVal value As Decimal)
            _OBT9Z6 = value
        End Set
    End Property
    Public _OBT9Z7 As Decimal 'Z7
    Public Property OBT9Z7() As Decimal
        Get
            Return _OBT9Z7
        End Get
        Set(ByVal value As Decimal)
            _OBT9Z7 = value
        End Set
    End Property
    Public _OBT9Z8 As Decimal 'Z8
    Public Property OBT9Z8() As Decimal
        Get
            Return _OBT9Z8
        End Get
        Set(ByVal value As Decimal)
            _OBT9Z8 = value
        End Set
    End Property
    Public _OBT9Z9 As Decimal 'Z9
    Public Property OBT9Z9() As Decimal
        Get
            Return _OBT9Z9
        End Get
        Set(ByVal value As Decimal)
            _OBT9Z9 = value
        End Set
    End Property

    Public _OBT9ZS1 As Decimal 'ZS1
    Public Property OBT9ZS1() As Decimal
        Get
            Return _OBT9ZS1
        End Get
        Set(ByVal value As Decimal)
            _OBT9ZS1 = value
        End Set
    End Property
    Public _OBT9ZS2 As Decimal 'ZS2
    Public Property OBT9ZS2() As Decimal
        Get
            Return _OBT9ZS2
        End Get
        Set(ByVal value As Decimal)
            _OBT9ZS2 = value
        End Set
    End Property
    Public _OBT9ZS3 As Decimal 'ZS3
    Public Property OBT9ZS3() As Decimal
        Get
            Return _OBT9ZS3
        End Get
        Set(ByVal value As Decimal)
            _OBT9ZS3 = value
        End Set
    End Property
    Public _OBT9ZS4 As Decimal 'ZS4
    Public Property OBT9ZS4() As Decimal
        Get
            Return _OBT9ZS4
        End Get
        Set(ByVal value As Decimal)
            _OBT9ZS4 = value
        End Set
    End Property
    Public _OBT9ZS5 As Decimal 'ZS5
    Public Property OBT9ZS5() As Decimal
        Get
            Return _OBT9ZS5
        End Get
        Set(ByVal value As Decimal)
            _OBT9ZS5 = value
        End Set
    End Property
    Public _OBT9ZS6 As Decimal 'ZS6
    Public Property OBT9ZS6() As Decimal
        Get
            Return _OBT9ZS6
        End Get
        Set(ByVal value As Decimal)
            _OBT9ZS6 = value
        End Set
    End Property
    Public _OBT9ZS7 As Decimal 'ZS7
    Public Property OBT9ZS7() As Decimal
        Get
            Return _OBT9ZS7
        End Get
        Set(ByVal value As Decimal)
            _OBT9ZS7 = value
        End Set
    End Property
    Public _OBT9ZS8 As Decimal 'ZS8
    Public Property OBT9ZS8() As Decimal
        Get
            Return _OBT9ZS8
        End Get
        Set(ByVal value As Decimal)
            _OBT9ZS8 = value
        End Set
    End Property
    Public _OBT9ZS9 As Decimal 'ZS9
    Public Property OBT9ZS9() As Decimal
        Get
            Return _OBT9ZS9
        End Get
        Set(ByVal value As Decimal)
            _OBT9ZS9 = value
        End Set
    End Property

#End Region
End Class
