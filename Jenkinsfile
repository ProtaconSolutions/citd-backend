node {
  stage 'Checkout'
    checkout scm

  stage 'Build'
    container('dotnet') {
      sh """
      dotnet restore
      dotnet build -c Release -o out
      """
    }
  stage 'Test'
    container('dotnet') {
      sh """
      dotnet test
      """
    }
  stage 'Package'
    kubernetes.image().withName("${env.PTCS_DOCKER_REGISTRY} + /citd-backend").build().frompath(".")
    kubernetes.image().withName("${env.PTCS_DOCKER_REGISTRY} + /citd-backend").push().withTag("dev").toRegistry()
}
