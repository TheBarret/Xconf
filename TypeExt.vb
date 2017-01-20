Imports System.IO
Public Module TypeExt
	<System.Runtime.CompilerServices.Extension>
	Public Sub AddRange(src As Dictionary(Of String, Value), dest As Dictionary(Of String, Value))
		src.ForEach(Sub(key As String, value As Value) dest.Add(key, value))
	End Sub
	<System.Runtime.CompilerServices.Extension>
	Public Sub ForEach(src As Dictionary(Of String, Value), action As Action(Of String, Value))
		For Each item As KeyValuePair(Of String, Value) In src
			action.Invoke(item.Key, item.Value)
		Next
	End Sub
	<System.Runtime.CompilerServices.Extension>
	Public Function Checksum(fi As FileInfo) As Byte()
		If (fi.Exists) Then
			Using fs As New FileStream(fi.FullName, FileMode.Open, FileAccess.Read)
				Dim buffer() As Byte = New Byte(Convert.ToInt32(fs.Length)) {}
				fs.Read(buffer, 0, buffer.Length)
				Using sha256 As Security.Cryptography.SHA256 = Security.Cryptography.SHA256.Create()
					Return sha256.ComputeHash(buffer)
				End Using
			End Using
		End If
		Throw New Exception("File does not exist")
	End Function
End Module
