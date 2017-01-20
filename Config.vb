Public Class Config
	Public Const CONFIG_LINE As String = "^([a-z0-9_.]*).*?\:.*?(\bbyte\b|\bint16\b|\bint32\b|\bint64\b|\bstring\b|\bboolean\b|\bfloat\b).*?\:.*?(\bfalse\b|\btrue\b|[-+]?\d*\.\d+([eE][-+]?\d+)?|[-+]?\d*([eE]?\d+)|'.*?'|\""(.*?)\"")\;"
End Class
