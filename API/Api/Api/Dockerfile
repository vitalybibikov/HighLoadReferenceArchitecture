FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine3.12 AS builder
WORKDIR /app

RUN echo -e "http://nl.alpinelinux.org/alpine/v3.12/main\nhttp://nl.alpinelinux.org/alpine/v3.12/community\nhttp://nl.alpinelinux.org/alpine/edge/testing" > /etc/apk/repositories
RUN apk update && apk add libgdiplus && apk --update add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine3.12 AS builder
LABEL stage="intermediate"
WORKDIR /src

COPY . .
WORKDIR "/src/Api"

FROM build AS publish
RUN dotnet publish "mitp.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
LABEL project="365scores"

ENTRYPOINT ["dotnet", "mitp.Api.dll"]