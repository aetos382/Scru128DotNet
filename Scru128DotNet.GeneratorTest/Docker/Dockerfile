FROM --platform="linux/${TARGETARCH}" ubuntu:24.04 AS base

RUN apt-get update && \
    apt-get upgrade --yes --no-install-recommends --no-install-suggests --auto-remove --purge && \
    apt-get install --yes --no-install-recommends --no-install-suggests \
        ca-certificates

FROM --platform="linux/${TARGETARCH}" base AS build

RUN apt-get install --yes --no-install-recommends --no-install-suggests \
        dotnet-sdk-8.0

ARG TARGETARCH

WORKDIR /src

COPY [ \
    "Scru128DotNet/Scru128DotNet.csproj", \
    "Scru128DotNet/packages.lock.json", \
    "Scru128DotNet/" \
    ]

COPY [ \
    "Scru128DotNet.GeneratorTest/Scru128DotNet.GeneratorTest.csproj", \
    "Scru128DotNet.GeneratorTest/" \
    ]

COPY [ \
    "Directory.Build.props", \
    "NuGet.config", \
    "./" \
    ]

RUN DOTNET_NOLOGO=1 \
    dotnet restore \
    Scru128DotNet.GeneratorTest/Scru128DotNet.GeneratorTest.csproj \
    -p:DockerTargetPlatform=${TARGETPLATFORM}

COPY [".", "."]

RUN DOTNET_NOLOGO=1 \
    dotnet publish \
    Scru128DotNet.GeneratorTest/Scru128DotNet.GeneratorTest.csproj \
    --configuration Release \
    --output /publish \
    --no-restore \
    -p:DockerTargetPlatform=${TARGETPLATFORM}

###

FROM --platform="linux/${TARGETARCH}" base AS run-test

RUN apt-get install --yes --no-install-recommends --no-install-suggests \
        clang \
        zlib1g-dev \
        dotnet-runtime-8.0 \
        rustup

ENV RUSTUP_HOME=/usr/local/rustup \
    CARGO_HOME=/usr/local/cargo \
    PATH=/usr/local/cargo/bin:$PATH

RUN rustup default stable

RUN cargo install --git https://github.com/scru128/gen_test.git

WORKDIR /app
COPY --from=build /publish/* .

ENTRYPOINT ./Scru128DotNet.GeneratorTest | scru128-test
