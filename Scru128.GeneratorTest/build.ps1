param(
    [string] $ImageTag = 'scru128.generatortest')

Push-Location -LiteralPath $PSScriptRoot

docker build --tag $ImageTag -f ./Dockerfile ..

Pop-Location
