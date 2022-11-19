FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY . .

RUN dotnet publish Scru128.GeneratorTest/Scru128.GeneratorTest.csproj --configuration Release --output /publish

###

FROM rust:slim

RUN cargo install --git https://github.com/scru128/gen_test.git

WORKDIR /app
COPY --from=build /publish/* .

ENTRYPOINT ./Scru128.GeneratorTest | scru128-test
