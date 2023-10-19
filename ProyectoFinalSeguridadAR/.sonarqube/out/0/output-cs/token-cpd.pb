ˆ
rC:\Users\arosales\source\GitHub\CursoSeguridad\ProyectoFinalSeguridadAR\ProyectoFinalSeguridadAR\Client\Program.cs
	namespace 	$
ProyectoFinalSeguridadAR
 "
." #
Client# )
{		 
public

 

class

 
Program

 
{ 
public 
static 
async 
Task  
Main! %
(% &
string& ,
[, -
]- .
args/ 3
)3 4
{ 	
var 
builder 
= "
WebAssemblyHostBuilder 0
.0 1
CreateDefault1 >
(> ?
args? C
)C D
;D E
builder 
. 
RootComponents "
." #
Add# &
<& '
App' *
>* +
(+ ,
$str, 2
)2 3
;3 4
builder 
. 
RootComponents "
." #
Add# &
<& '

HeadOutlet' 1
>1 2
(2 3
$str3 @
)@ A
;A B
builder 
. 
Services 
. 
	AddScoped &
(& '
_' (
=>) +
new, /

HttpClient0 :
{; <
BaseAddress= H
=I J
newK N
UriO R
(R S
builderS Z
.Z [
HostEnvironment[ j
.j k
BaseAddressk v
)v w
}x y
)y z
;z {
await 
builder 
. 
Build 
(  
)  !
.! "
RunAsync" *
(* +
)+ ,
;, -
} 	
} 
} ¾
zC:\Users\arosales\source\GitHub\CursoSeguridad\ProyectoFinalSeguridadAR\ProyectoFinalSeguridadAR\Client\WeatherForecast.cs
	namespace 	$
ProyectoFinalSeguridadAR
 "
." #
Client# )
{ 
public 

class 
WeatherForecast  
{ 
public 
DateTime 
Date 
{ 
get "
;" #
set$ '
;' (
}) *
public 
int 
TemperatureC 
{  !
get" %
;% &
set' *
;* +
}, -
public		 
int		 
TemperatureF		 
=>		  "
$num		# %
+		& '
(		( )
int		) ,
)		, -
(		- .
TemperatureC		. :
/		; <
$num		= C
)		C D
;		D E
public

 
string

 
?

 
Summary

 
{

  
get

! $
;

$ %
set

& )
;

) *
}

+ ,
} 
} 