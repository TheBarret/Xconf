Imports System.Threading
Public Class Delayed
	Public Delegate Sub Task(ByRef done As ManualResetEvent)
	Public Shared Function Run(action As Task, DelayInMilliseconds As Double) As ManualResetEvent
		Dim done As New ManualResetEvent(False)
		Using Timer As New Timers.Timer(DelayInMilliseconds) With {.AutoReset = False}
			Try
				AddHandler Timer.Elapsed, Sub(obj, args) action.Invoke(done)
				Timer.Start()
				Return done
			Finally
				done.WaitOne()
				Timer.Stop()
				Timer.Dispose()
			End Try
		End Using
	End Function
End Class
