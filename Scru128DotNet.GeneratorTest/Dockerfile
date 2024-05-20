FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY . .

RUN dotnet publish Scru128DotNet.GeneratorTest/Scru128DotNet.GeneratorTest.csproj --configuration Release --output /publish

###

FROM rust:slim

RUN cargo install --git https://github.com/scru128/gen_test.git

WORKDIR /app
COPY --from=build /publish/* .

ENTRYPOINT ./Scru128DotNet.GeneratorTest | scru128-test
