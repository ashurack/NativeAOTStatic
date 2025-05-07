FROM mcr.microsoft.com/dotnet/nightly/sdk:9.0-alpine-aot
RUN apk add --no-cache jq
WORKDIR /app