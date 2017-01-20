# Xconf

- Project: Xconf

Library for parsing a config file with strong typed values and keeps track of modifications if changed externally

- Config format:
```
{key}       :{type}   : {value};

test.num    : int32   : 100;
test.float  : float   : 3.14;
test.bool   : boolean : True;
test.string : string  : "Hello, World!";  
```

- Accepted key formats
```
Key      = a-z 0-9 . _
```

Accepted type formats
```
type     = byte, int16, int32, int64, float, string, boolean
```

- Setup
```
dim xconf as xconf.provider = Parser.Create({filename})
```

- Methods

Cast object without type check, throws exception if type mismatched
```
xconf({key}).Cast(Of T)() as T
xconf({key}).ToByte() as Byte
xconf({key}).ToInt16() as Int16
xconf({key}).ToInt32() as Int32
xconf({key}).ToInt64() as Int64
xconf({key}).ToDouble() as Double
xconf({key}).ToBoolean() as Boolean
xconf({key}).ToString() as String
```

Cast object with type check, throws no exception but returns nothing if fails
```
xconf({key}).TryCast(Of T)() As T
```
