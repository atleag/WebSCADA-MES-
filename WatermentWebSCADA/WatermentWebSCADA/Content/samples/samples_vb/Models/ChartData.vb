Public Class ChartData
    Public Shared Function GetData() As List(Of ChartData)
        Dim data = New List(Of ChartData)()

        data.Add(New ChartData("A", 46, 78))
        data.Add(New ChartData("B", 35, 72))
        data.Add(New ChartData("C", 68, 86))
        data.Add(New ChartData("D", 30, 23))
        data.Add(New ChartData("E", 27, 70))
        data.Add(New ChartData("F", 85, 60))
        data.Add(New ChartData("D", 43, 88))
        data.Add(New ChartData("H", 29, 22))

        Return data
    End Function

    Public Sub New(label As String, value1 As Double, value2 As Double)
        Me.Label = label
        Me.Value1 = value1
        Me.Value2 = value2
    End Sub

    Public Property Label() As String
        Get
            Return m_Label
        End Get
        Set(value As String)
            m_Label = value
        End Set
    End Property
    Private m_Label As String
    Public Property Value1() As Double
        Get
            Return m_Value1
        End Get
        Set(value As Double)
            m_Value1 = value
        End Set
    End Property
    Private m_Value1 As Double
    Public Property Value2() As Double
        Get
            Return m_Value2
        End Get
        Set(value As Double)
            m_Value2 = value
        End Set
    End Property
    Private m_Value2 As Double
End Class
