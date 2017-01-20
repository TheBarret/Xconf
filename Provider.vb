Imports System.IO
Imports System.Threading
Public NotInheritable Class Provider
	Inherits Dictionary(Of String, Value)
	Implements IDisposable
	Public Event ProviderModifiedValues()
	Sub New(Filename As String)
		Me.Filename = Filename
		Me.ResetEvent = New ManualResetEvent(False)
		Me.Checksum = New FileInfo(Filename).Checksum
		BackgroundWorker.Run(Sub() Me.Idler())
	End Sub
	Public Function TryGet(Of T)(key As String) As T
		If (Me.ContainsKey(key) AndAlso TypeOf Me(key).Value Is T) Then
			Return Me(key).Cast(Of T)()
		Else
			Return Nothing
		End If
	End Function
	Private Sub Idler()
		Try
			If (Me.Active) Then
				Me.Active = False
				Me.ResetEvent.WaitOne()
			End If
			Me.Active = True
			Me.ResetEvent.Reset()
			Do
				Delayed.Run(New Delayed.Task(AddressOf Me.Checkup), 1000).WaitOne()
			Loop While Me.Active
		Finally
			Me.ResetEvent.Set()
		End Try
	End Sub
	Private Sub Checkup(ByRef Finished As ManualResetEvent)
		Try
			If (Not New FileInfo(Me.Filename).Checksum.SequenceEqual(Me.Checksum)) Then
				Me.Checksum = New FileInfo(Me.Filename).Checksum
				Me.Clear()
				Me.AddRange(Parser.Create(Me.Filename))
				RaiseEvent ProviderModifiedValues()
			End If
		Finally
			Finished.Set()
		End Try
	End Sub
	Private Property Active As Boolean
	Private Property Filename As String
	Private Property Checksum As Byte()
	Private Property ResetEvent As ManualResetEvent
#Region "IDisposable Support"
	Private disposedValue As Boolean
	Protected Sub Dispose(disposing As Boolean)
		If Not Me.disposedValue Then
			If disposing Then
				If (Me.Active) Then
					Me.Active = False
					Me.ResetEvent.WaitOne()
				End If
				Me.Clear()
			End If
		End If
		Me.disposedValue = True
	End Sub
	Public Sub Dispose() Implements IDisposable.Dispose
		Dispose(True)
		GC.SuppressFinalize(Me)
	End Sub
#End Region
End Class
