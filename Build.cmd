.nuget\NuGet.exe pack SharpRouting.Mvc4\SharpRouting.Mvc4.csproj -Build -OutputDirectory build -Properties Configuration=Release || goto done
.nuget\NuGet.exe pack SharpRouting.Mvc5\SharpRouting.Mvc5.csproj -Build -OutputDirectory build -Properties Configuration=Release || goto done

:done
@pause
