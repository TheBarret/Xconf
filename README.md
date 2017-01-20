# Xconf

- Project: Xconf

Library for parsing a config file with strong typed defined values.

- Provider Class

The provider class uses the base principles from the Dict(Of TKey, TValue) using the 'Key', it also
keeps track if the config file has been changed externally, if this happens it will automaticly update itself.

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
dim table as Xconf.provider = Xconf.Parser.Create({filename})
```

- Methods

Cast object without type check, throws exception if type mismatched
```
table({key}).Cast(Of T)() as T
table({key}).ToByte() as Byte
table({key}).ToInt16() as Int16
table({key}).ToInt32() as Int32
table({key}).ToInt64() as Int64
table({key}).ToDouble() as Double
table({key}).ToBoolean() as Boolean
table({key}).ToString() as String
```

Cast object with type check, throws no exception but returns nothing if fails
```
table({key}).TryCast(Of T)() As T
```
