# Source: https://stackoverflow.com/a/62060315
# Generate self-signed certificate to be used by IdentityServer.
# When using localhost - API cannot see the IdentityServer from within the docker-compose'd network.
# You have to run this script as Administrator (open Powershell by right click -> Run as Administrator).

$ErrorActionPreference = "Stop"

$rootCN = "IdentityServerDockerDemoRootCert"
$identityServerCNs = "psp_auth", "localhost"
$gatewayCNs = "psp_gateway", "localhost"
$dataApiCNs = "psp_data", "localhost"
$routeApiCNs = "psp_route", "localhost"

$alreadyExistingCertsRoot = Get-ChildItem -Path Cert:\LocalMachine\My -Recurse | Where-Object {$_.Subject -eq "CN=$rootCN"}
$alreadyExistingCertsIdentityServer = Get-ChildItem -Path Cert:\LocalMachine\My -Recurse | Where-Object {$_.Subject -eq ("CN={0}" -f $identityServerCNs[0])}
$alreadyExistingCertsGateway = Get-ChildItem -Path Cert:\LocalMachine\My -Recurse | Where-Object {$_.Subject -eq ("CN={0}" -f $gatewayCNs[0])}
$alreadyExistingCertsDataApi = Get-ChildItem -Path Cert:\LocalMachine\My -Recurse | Where-Object {$_.Subject -eq ("CN={0}" -f $dataApiCNs[0])}
$alreadyExistingCertsRouteApi = Get-ChildItem -Path Cert:\LocalMachine\My -Recurse | Where-Object {$_.Subject -eq ("CN={0}" -f $routeApiCNs[0])}

if ($alreadyExistingCertsRoot.Count -eq 1) {
    Write-Output "Skipping creating Root CA certificate as it already exists."
    $testRootCA = [Microsoft.CertificateServices.Commands.Certificate] $alreadyExistingCertsRoot[0]
} else {
    $testRootCA = New-SelfSignedCertificate -Subject $rootCN -KeyUsageProperty Sign -KeyUsage CertSign -CertStoreLocation Cert:\LocalMachine\My
}

if ($alreadyExistingCertsIdentityServer.Count -eq 1) {
    Write-Output "Skipping creating Identity Server certificate as it already exists."
    $identityServerCert = [Microsoft.CertificateServices.Commands.Certificate] $alreadyExistingCertsIdentityServer[0]
} else {
    # Create a SAN cert for both identity-server and localhost.
    $identityServerCert = New-SelfSignedCertificate -DnsName $identityServerCNs -Signer $testRootCA -CertStoreLocation Cert:\LocalMachine\My
}

#api

if ($alreadyExistingCertsGateway.Count -eq 1) {
    Write-Output "Skipping creating API certificate as it already exists."
    $gatewayCert = [Microsoft.CertificateServices.Commands.Certificate] $alreadyExistingCertsGateway[0]
} else {
    # Create a SAN cert for both web-api and localhost.
    $gatewayCert = New-SelfSignedCertificate -DnsName $gatewayCNs -Signer $testRootCA -CertStoreLocation Cert:\LocalMachine\My
}

if ($alreadyExistingCertsDataApi.Count -eq 1) {
    Write-Output "Skipping creating API certificate as it already exists."
    $webDataApiCert = [Microsoft.CertificateServices.Commands.Certificate] $alreadyExistingCertsDataApi[0]
} else {
    # Create a SAN cert for both web-api and localhost.
    $webDataApiCert = New-SelfSignedCertificate -DnsName $dataApiCNs -Signer $testRootCA -CertStoreLocation Cert:\LocalMachine\My
}

if ($alreadyExistingCertsRouteApi.Count -eq 1) {
    Write-Output "Skipping creating API certificate as it already exists."
    $webRouteApiCert = [Microsoft.CertificateServices.Commands.Certificate] $alreadyExistingCertsRouteApi[0]
} else {
    # Create a SAN cert for both web-api and localhost.
    $webRouteApiCert = New-SelfSignedCertificate -DnsName $routeApiCNs -Signer $testRootCA -CertStoreLocation Cert:\LocalMachine\My
}

# Export it for docker container to pick up later.
$password = ConvertTo-SecureString -String "1703" -Force -AsPlainText

$rootCertPathPfx = "certs"
$identityServerCertPath = "src/IdentityServer/certs"
$webGatewayCertCertPath = "src/Gateway/certs"
$webDataApiCertPath = "src/DataApi/certs"
$webRouteApiCertPath = "src/RouteApi/certs"

[System.IO.Directory]::CreateDirectory($rootCertPathPfx) | Out-Null
[System.IO.Directory]::CreateDirectory($identityServerCertPath) | Out-Null
[System.IO.Directory]::CreateDirectory($webGatewayCertCertPath) | Out-Null
[System.IO.Directory]::CreateDirectory($webDataApiCertPath) | Out-Null
[System.IO.Directory]::CreateDirectory($webRouteApiCertPath) | Out-Null

Export-PfxCertificate -Cert $testRootCA -FilePath "$rootCertPathPfx/aspnetapp-root-cert.pfx" -Password $password | Out-Null
Export-PfxCertificate -Cert $identityServerCert -FilePath "$identityServerCertPath/aspnetapp-identity-server.pfx" -Password $password | Out-Null
Export-PfxCertificate -Cert $gatewayCert -FilePath "$webGatewayCertCertPath/aspnetapp-gateway-api.pfx" -Password $password | Out-Null
Export-PfxCertificate -Cert $webDataApiCert -FilePath "$webDataApiCertPath/aspnetapp-data-api.pfx" -Password $password | Out-Null
Export-PfxCertificate -Cert $webRouteApiCert -FilePath "$webRouteApiCertPath/aspnetapp-route-api.pfx" -Password $password | Out-Null

# Export .cer to be converted to .crt to be trusted within the Docker container.
$rootCertPathCer = "certs/aspnetapp-root-cert.cer"
Export-Certificate -Cert $testRootCA -FilePath $rootCertPathCer -Type CERT | Out-Null

# Trust it on your host machine.
$store = New-Object System.Security.Cryptography.X509Certificates.X509Store "Root","LocalMachine"
$store.Open("ReadWrite")

$rootCertAlreadyTrusted = ($store.Certificates | Where-Object {$_.Subject -eq "CN=$rootCN"} | Measure-Object).Count -eq 1

if ($rootCertAlreadyTrusted -eq $false) {
    Write-Output "Adding the root CA certificate to the trust store."
    $store.Add($testRootCA)
}

$store.Close()